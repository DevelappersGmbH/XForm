using System;
using UIKit;
using XForm.Fields.Bases;
using XForm.Fields.Interfaces;
using XForm.FieldViews;

namespace XForm.Ios.FieldViews.Bases
{
    public abstract class FieldView : UITableViewCell, IFieldView
    {
        protected FieldView(IntPtr handle) : base(handle)
        {
        }
        
        public abstract void BindTo(IField field);

        public override void AwakeFromNib()
        {
            base.AwakeFromNib();

            SelectionStyle = UITableViewCellSelectionStyle.None;
        }
    }
    
    public abstract class FieldView<TField> : FieldView where TField: IField
    {
        protected FieldView(IntPtr handle) : base(handle)
        {
        }
        
        public TField Field { get; private set; }

        public override void BindTo(IField field)
        {
            var typedField = (TField) field;

            Field = typedField;
            BindTo(typedField);
        }

        public abstract void BindTo(TField field);
    }
}