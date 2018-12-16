using Foundation;
using UIKit;
using XForm.Ios.ContentViews.Bases;
using XForm.Ios.ContentViews.Interfaces;

namespace XForm.Ios.ContentViews
{
    public partial class ButtonFieldContent : FieldContent, IButtonFieldContent
    {
        public ButtonFieldContent() : base(UINib.FromName(nameof(ButtonFieldContent), NSBundle.MainBundle))
        {
        }

        public UIButton Button => Button_;
    }
}
