using System.Collections.ObjectModel;
using System.Collections.Specialized;
using Android.Support.V7.Widget;
using Android.Views;
using XForm.Android.FieldViews.Bases;
using XForm.Android.Forms;
using XForm.Android.FormViews;
using XForm.Fields.Interfaces;

namespace XForm.Android.Adapters
{
    public class FormAdapter : RecyclerView.Adapter
    {
        private readonly FormView _formView;
        private readonly FieldViewCreator _fieldViewCreator;
        
        private ObservableCollection<IField> _fields;

        public FormAdapter(FormView formView)
        {
            _formView = formView;
            _fieldViewCreator = new FieldViewCreator();
        }

        public ObservableCollection<IField> Fields
        {
            get => _fields;
            set
            {
                if (_fields == value)
                    return;
                
                if (_fields != null)
                    _fields.CollectionChanged -= FieldsCollectionChanged;
                
                _fields = value;
                _fields.CollectionChanged += FieldsCollectionChanged;
                FieldsCollectionChanged(this, null);
            }
        }

        #region Adapter

        public override int ItemCount => Fields?.Count ?? 0;

        public override int GetItemViewType(int position)
        {
            var viewType = _formView.FieldViewLocator.ViewTypeForField(FieldAt(position));
            return _fieldViewCreator.ItemViewType(viewType);
        }

        public override RecyclerView.ViewHolder OnCreateViewHolder(ViewGroup parent, int viewType)
        {
            return _fieldViewCreator.CreateFieldView(parent, viewType);
        }

        public override void OnBindViewHolder(RecyclerView.ViewHolder holder, int position)
        {
            var field = FieldAt(position);
            ((FieldView) holder).BindTo(field);
        }      

        #endregion  

        #region Event handlers

        private void FieldsCollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            NotifyDataSetChanged();
        }

        #endregion

        private IField FieldAt(int position)
        {
            return Fields[position];
        }
    }
}