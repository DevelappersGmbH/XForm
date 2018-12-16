using UIKit;

namespace XForm.Ios.ContentViews.Interfaces
{
    public interface ITextFieldContent : IFieldContent
    {
        UILabel TitleLabel { get; }
        UITextField ValueTextField { get; }
    }
}