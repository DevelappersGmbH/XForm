using System;

using Foundation;
using UIKit;
using XForm.Fields;

namespace XForm.Ios.FieldViews
{
    public partial class LabelFieldView : UITableViewCell
    {
        public static readonly NSString Key = new NSString("LabelFieldView");
        public static readonly UINib Nib;

        static LabelFieldView()
        {
            Nib = UINib.FromName("LabelFieldView", NSBundle.MainBundle);
        }

        protected LabelFieldView(IntPtr handle) : base(handle)
        {
        }

        public void BindToField(LabelField field)
        {
            TitleLabel.Text = field.Title;
            ValueLabel.Text = field.Value;
        }
    }
}
