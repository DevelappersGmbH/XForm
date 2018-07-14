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
	[Register ("TextInputFieldView")]
	partial class TextInputFieldView
	{
		[Outlet]
		UIKit.UILabel TitleLabel { get; set; }

		[Outlet]
		UIKit.UITextField ValueTextField { get; set; }
		
		void ReleaseDesignerOutlets ()
		{
			if (TitleLabel != null) {
				TitleLabel.Dispose ();
				TitleLabel = null;
			}

			if (ValueTextField != null) {
				ValueTextField.Dispose ();
				ValueTextField = null;
			}
		}
	}
}
