using System;
using Foundation;
using UIKit;
using XForm.Fields;
using XForm.Ios.FieldViews.Bases;

namespace XForm.Ios.FieldViews
{
    public partial class LabelFieldView : ValueFieldView<LabelField, string>
    {
        public static readonly UINib Nib;

        static LabelFieldView()
        {
            Nib = UINib.FromName("LabelFieldView", NSBundle.MainBundle);
        }

        protected LabelFieldView(IntPtr handle) : base(handle)
        {
        }

        public override void TitleChanged(string value)
        {
            base.TitleChanged(value);

            TitleLabel.Text = value;
        }

        protected override void ValueChanged(string value)
        {
            base.ValueChanged(value);

            ValueLabel.Text = value;
            
            NotifyContentHeightChanged();
        }
    }
}
