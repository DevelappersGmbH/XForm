using Foundation;
using UIKit;
using XForm.Ios.ContentViews.Bases;
using XForm.Ios.ContentViews.Interfaces;

namespace XForm.Ios.ContentViews
{
    public partial class TextFieldContent : FieldContent, ITextFieldContent 
    {
        public TextFieldContent() : base(UINib.FromName(nameof(TextFieldContent), NSBundle.MainBundle))
        {
        }

        public UILabel TitleLabel => TitleLabel_;
        
        public UITextField ValueTextField => ValueTextField_;
    }
}
