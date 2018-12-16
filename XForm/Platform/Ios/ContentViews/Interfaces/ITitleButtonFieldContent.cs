using UIKit;

namespace XForm.Ios.ContentViews.Interfaces
{
    public interface ITitleButtonFieldContent : IFieldContent
    {
        UILabel TitleLabel { get; }
        UIButton Button { get; }
    }
}