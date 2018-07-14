using System;
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
        
        public abstract void BindTo(IField field);
    }
    
    public abstract class FieldView<TField> : FieldView
    {
        protected FieldView(IntPtr handle) : base(handle)
        {
        }

        public override void BindTo(IField field)
        {
            BindTo((TField) field);
        }

        public abstract void BindTo(TField field);
    }
}