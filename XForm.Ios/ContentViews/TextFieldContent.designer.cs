// WARNING
//
// This file has been generated automatically by Visual Studio to store outlets and
// actions made in the UI designer. If it is removed, they will be lost.
// Manual changes to this file may not be handled correctly.
//
using Foundation;
using System.CodeDom.Compiler;

namespace XForm.Ios.ContentViews
{
	[Register ("TextFieldContent")]
	partial class TextFieldContent
	{
		[Outlet]
		UIKit.UILabel TitleLabel_ { get; set; }

		[Outlet]
		UIKit.UITextField ValueTextField_ { get; set; }
		
		void ReleaseDesignerOutlets ()
		{
			if (TitleLabel_ != null) {
				TitleLabel_.Dispose ();
				TitleLabel_ = null;
			}

			if (ValueTextField_ != null) {
				ValueTextField_.Dispose ();
				ValueTextField_ = null;
			}
		}
	}
}
