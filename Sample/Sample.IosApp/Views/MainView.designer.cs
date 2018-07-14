// WARNING
//
// This file has been generated automatically by Visual Studio to store outlets and
// actions made in the UI designer. If it is removed, they will be lost.
// Manual changes to this file may not be handled correctly.
//
using Foundation;
using System.CodeDom.Compiler;
using XForm.Ios.FormViews;

namespace Sample.IosApp.Views
{
	[Register ("MainView")]
	partial class MainView
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
