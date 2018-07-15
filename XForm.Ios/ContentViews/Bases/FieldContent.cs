using System.Linq;
using UIKit;
using XForm.Ios.ContentViews.Interfaces;

namespace XForm.Ios.ContentViews.Bases
{
    public abstract class FieldContent : UIView, IFieldContent
    {
        protected FieldContent(UINib nib)
        {
            InstantiateFromNib(nib);  
        }

        public UIView ContentView => this;
        
        private void InstantiateFromNib(UINib nib)
        {
            var view = (UIView) nib.Instantiate(this, null).First();

            view.PreservesSuperviewLayoutMargins = true;
            view.TranslatesAutoresizingMaskIntoConstraints = false;
            
            AddSubview(view);
            NSLayoutConstraint.ActivateConstraints(new[]
            {
                LeftAnchor.ConstraintEqualTo(view.LeftAnchor),
                RightAnchor.ConstraintEqualTo(view.RightAnchor),
                TopAnchor.ConstraintEqualTo(view.TopAnchor),
                BottomAnchor.ConstraintEqualTo(view.BottomAnchor)
            });
        }
    }
}