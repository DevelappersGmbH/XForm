using Foundation;
using UIKit;
using XForm.Ios.ContentViews.Bases;
using XForm.Ios.ContentViews.Interfaces;

namespace XForm.Ios.ContentViews
{
    public partial class TitleButtonFieldContent : FieldContent, ITitleButtonFieldContent
    {
        public TitleButtonFieldContent() : base(UINib.FromName(nameof(TitleButtonFieldContent), NSBundle.MainBundle))
        {
        }

        public UILabel TitleLabel => TitleLabel_;
        public UIButton Button => Button_;
    }
}

