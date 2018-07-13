using System.Collections.ObjectModel;
using System.Collections.Specialized;
using Android.Support.V7.Widget;
using Android.Views;
using XForm.Android.FieldViews;
using XForm.Fields;
using XForm.Fields.Interfaces;

namespace XForm.Android.Adapters
{
    public class Adapter : RecyclerView.Adapter
    {
        private ObservableCollection<IField> _fields;

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

        public override RecyclerView.ViewHolder OnCreateViewHolder(ViewGroup parent, int viewType)
        {
            return LabelFieldView.Create(parent);
        }

        public override void OnBindViewHolder(RecyclerView.ViewHolder holder, int position)
        {
            var item = Fields[position];

            ((LabelFieldView) holder).BindTo((LabelField) item);
        }      

        #endregion  

        #region Event handlers

        private void FieldsCollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            NotifyDataSetChanged();
        }

        #endregion
    }
}