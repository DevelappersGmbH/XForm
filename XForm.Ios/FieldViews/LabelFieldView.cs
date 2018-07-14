using System;
using Foundation;
using UIKit;
using XForm.Fields;
using XForm.Ios.FieldViews.Bases;

namespace XForm.Ios.FieldViews
{
    public partial class LabelFieldView : FieldView<LabelField>
    {
        public static readonly UINib Nib;

        static LabelFieldView()
        {
            Nib = UINib.FromName("LabelFieldView", NSBundle.MainBundle);
        }

        protected LabelFieldView(IntPtr handle) : base(handle)
        {
        }

        public override void BindTo(LabelField field)
        {
            TitleLabel.Text = field.Title;
            ValueLabel.Text = field.Value;
        }
    }
}
