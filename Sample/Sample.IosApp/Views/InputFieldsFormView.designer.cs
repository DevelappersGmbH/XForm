// WARNING
//
// This file has been generated automatically by Visual Studio from the outlets and
// actions declared in your storyboard file.
// Manual changes to this file will not be maintained.
//
using Foundation;
using System;
using System.CodeDom.Compiler;
using XForm.Ios.FormViews;

namespace Sample.IosApp.Views
{
    [Register ("InputFieldsFormView")]
    partial class InputFieldsFormView
    {
        [Outlet]
        FormView FormView { get; set; }

        void ReleaseDesignerOutlets ()
        {
            if (FormView != null) {
                FormView.Dispose ();
                FormView = null;
            }
        }
    }
}