using System;
using UIKit;
using XForm.Fields;
using XForm.Ios.ContentViews;
using XForm.Ios.ContentViews.Interfaces;
using XForm.Ios.FieldViews.Bases;

namespace XForm.Ios.FieldViews
{
    public class LabelFieldView : ValueFieldView<LabelField, ILabelFieldContent, string>
    {
        public LabelFieldView(IntPtr handle) : base(handle)
        {
        }

        internal override Func<ILabelFieldContent> DefaultContentCreator { get; } = () => new LabelFieldContent();
        public UILabel TitleLabel => Content.TitleLabel;

        public UILabel ValueLabel => Content.ValueLabel;
        
        protected override void TitleChanged(string value)
        {
            base.TitleChanged(value);

            TitleLabel.Text = value;
        }

        protected override void ValueChanged(string value)
        {
            base.ValueChanged(value);

            ValueLabel.Text = value;
            
            NotifyContentHeightChanged();
        }
    }
}