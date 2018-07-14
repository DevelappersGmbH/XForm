using System;
using System.ComponentModel;
using UIKit;
using XForm.Fields.Interfaces;
using XForm.FieldViews;

namespace XForm.Ios.FieldViews.Bases
{
    public abstract class FieldView : UITableViewCell, IFieldView
    {
        protected FieldView(IntPtr handle) : base(handle)
        {
        }

        public virtual void BindTo(IField field)
        {
        }

        public override void AwakeFromNib()
        {
            base.AwakeFromNib();

            SelectionStyle = UITableViewCellSelectionStyle.None;
        }

        public event EventHandler ContentHeightChanged;

        public void NotifyContentHeightChanged()
        {
            ContentHeightChanged?.Invoke(this, new EventArgs());
        }
    }
    
    public abstract class FieldView<TField> : FieldView where TField: IField
    {
        protected FieldView(IntPtr handle) : base(handle)
        {
        }

        ~FieldView()
        {
            if (Field != null)
                Field.PropertyChanged -= FieldPropertyChanged;
        }

        public TField Field { get; private set; }

        public override void BindTo(IField field)
        {
            base.BindTo(field);
            
            BindTo((TField) field);
        }

        protected virtual void BindTo(TField field)
        {
            if (Equals(Field, field))
                return;

            if (Field != null) 
                Field.PropertyChanged -= FieldPropertyChanged;
            
            Field = field;

            if (Field != null)
                Field.PropertyChanged += FieldPropertyChanged;

            TitleChanged(Field?.Title);
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
            }
        }

        public virtual void TitleChanged(string value)
        {
        }
    }
}