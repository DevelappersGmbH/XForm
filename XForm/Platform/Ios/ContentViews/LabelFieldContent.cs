using Foundation;
using UIKit;
using XForm.Ios.ContentViews.Bases;
using XForm.Ios.ContentViews.Interfaces;

namespace XForm.Ios.ContentViews
{
    public partial class LabelFieldContent : FieldContent, ILabelFieldContent
    {
        public LabelFieldContent() : base(UINib.FromName(nameof(LabelFieldContent), NSBundle.MainBundle))
        {
        }

        public UILabel TitleLabel => TitleLabel_;
        
        public UILabel ValueLabel => ValueLabel_;
    }
}
