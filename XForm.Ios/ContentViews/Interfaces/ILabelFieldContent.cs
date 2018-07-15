using UIKit;

namespace XForm.Ios.ContentViews.Interfaces
{
    public interface ILabelFieldContent : IFieldContent
    {
        UILabel TitleLabel { get; }
        UILabel ValueLabel { get; }
    }
}