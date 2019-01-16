// WARNING
//
// This file has been generated automatically by Visual Studio from the outlets and
// actions declared in your storyboard file.
// Manual changes to this file will not be maintained.
//
using Foundation;
using System;
using System.CodeDom.Compiler;

namespace Sample.IosApp.Views
{
    [Register ("MenuFormView")]
    partial class MenuFormView
    {
        [Outlet]
        XForm.Ios.FormViews.FormView FormView { get; set; }

        void ReleaseDesignerOutlets ()
        {
            if (FormView != null) {
                FormView.Dispose ();
                FormView = null;
            }
        }
    }
}