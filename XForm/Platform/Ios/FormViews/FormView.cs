using System;
using CoreGraphics;
using Foundation;
using UIKit;
using XForm.Forms;
using XForm.Ios.ContentViews.Bases;
using XForm.Ios.ContentViews.Interfaces;
using XForm.Ios.Forms;
using XForm.Ios.Sources;

namespace XForm.Ios.FormViews
{
    [Register("FormView")]
    public class FormView: UITableView
    {
        private FormTableViewSource _source;
        
        private Form _form;
        private FieldViewLocator _fieldViewLocator;
        private FieldViewCreator _fieldViewCreator;

        public FormView(CGRect frame) : base(frame)
        {
            Setup();
        }

        public FormView(IntPtr handle) : base(handle)
        {
            Setup();
        }

        public Form Form
        {
            get => _form;
            set
            {
                _form = value;
                _source.Fields = value?.VisibleFields;
            }
        }

        public FieldViewLocator FieldViewLocator
        {
            get => _fieldViewLocator ?? Form?.FieldViewLocator;
            set => _fieldViewLocator = value;
        }

        private void Setup()
        {
            RowHeight = AutomaticDimension;
            EstimatedRowHeight = 70;

            _fieldViewCreator = new FieldViewCreator(this);
            _source = new FormTableViewSource(this, _fieldViewCreator);
            Source = _source;
        }

        public void CreateContent<TFieldContent>(Func<FieldContent> fieldContentCreator) where TFieldContent : IFieldContent
        {
            _fieldViewCreator.RegisterFieldContentCreator<TFieldContent>(fieldContentCreator);
        }
    }
}