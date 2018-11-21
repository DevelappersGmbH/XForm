using System.ComponentModel;
using Android.Support.V7.Widget;
using Android.Views;
using XForm.Fields.Interfaces;
using XForm.FieldViews;

namespace XForm.Droid.FieldViews.Bases
{
    public abstract class FieldView : RecyclerView.ViewHolder, IFieldView
    {
        private static View CreateViewItem(ViewGroup parent, int layout)
        {
            return LayoutInflater
                   .From(parent.Context)
                   .Inflate(layout, parent, false);
        }

        protected FieldView(ViewGroup parent, int layoutToInflate) : base(CreateViewItem(parent, layoutToInflate))
        {
        }
        
        public abstract void BindTo(IField field);
    }

    public abstract class FieldView<TField> : FieldView 
        where TField : class, IField
    {
        protected FieldView(ViewGroup parent, int layoutToInflate) : base(parent, layoutToInflate)
        {
        }
        
        ~FieldView()
        {
            if (Field != null) 
                Field.PropertyChanged -= FieldPropertyChanged;
        }

        public TField Field { get; private set; }

        public sealed override void BindTo(IField field)
        {
            BindTo((TField) field);
        }

        protected virtual void BindTo(TField field)
        {
            if (Equals(Field, field))
                return;

            if (Field != null) 
                Field.PropertyChanged -= FieldPropertyChanged;
            
            Field = field;

            if (Field == null)
                return;

            Field.PropertyChanged += FieldPropertyChanged;
            TitleChanged(Field.Title);
            EnabledChanged(Field.Enabled);
        }

        private void FieldPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            FieldPropertyChanged(e.PropertyName);
        }

        protected virtual void FieldPropertyChanged(string propertyName)
        {
            switch (propertyName)
            {
                case nameof(Field.Title):
                    TitleChanged(Field?.Title);
                    break;
                case nameof(Field.Enabled):
                    EnabledChanged(Field.Enabled);
                    break;
            }
        }

        protected virtual void TitleChanged(string value)
        {
        }
        
        protected virtual void EnabledChanged(bool value)
        {
        }
    }
}