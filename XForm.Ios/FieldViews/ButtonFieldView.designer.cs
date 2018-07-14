// WARNING
//
// This file has been generated automatically by Visual Studio to store outlets and
// actions made in the UI designer. If it is removed, they will be lost.
// Manual changes to this file may not be handled correctly.
//
using Foundation;
using System.CodeDom.Compiler;

namespace XForm.Ios.FieldViews
{
	[Register ("ButtonFieldView")]
	partial class ButtonFieldView
	{
		[Outlet]
		UIKit.UIButton Button { get; set; }
		
		void ReleaseDesignerOutlets ()
		{
			if (Button != null) {
				Button.Dispose ();
				Button = null;
			}
		}
	}
}
