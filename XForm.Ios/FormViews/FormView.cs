﻿using System;
using CoreGraphics;
using Foundation;
using UIKit;
using XForm.Forms;
using XForm.Ios.Sources;

namespace XForm.Ios.FormViews
{
    [Register("FormView")]
    public class FormView: UITableView
    {
        private FormTableViewSource _source;
        
        private Form _form;
        private FieldViewLocator _fieldViewLocator;

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
                _source.Fields = value?.Fields;
            }
        }

        public FieldViewLocator FieldViewLocator
        {
            get => _fieldViewLocator ?? Form?.FieldViewLocator;
            set => _fieldViewLocator = value;
        }

        private void Setup()
        {
            _source = new FormTableViewSource(this);
            Source = _source;
        }
    }
}
