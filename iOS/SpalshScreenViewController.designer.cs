// WARNING
//
// This file has been generated automatically by Visual Studio from the outlets and
// actions declared in your storyboard file.
// Manual changes to this file will not be maintained.
//
using Foundation;
using System;
using System.CodeDom.Compiler;
using UIKit;

namespace OnDuty.iOS
{
    [Register ("SpalshScreenViewController")]
    partial class SpalshScreenViewController
    {
		[Outlet]
		UIKit.UIButton Button { get; set; }

		[Outlet]
		[GeneratedCode("iOS Designer", "1.0")]
		UIKit.UIImageView SplashImage { get; set; }

		[Outlet]
		[GeneratedCode("iOS Designer", "1.0")]
		UIKit.UIView SplashScreen { get; set; }

		void ReleaseDesignerOutlets()
		{
			if (SplashImage != null)
			{
				SplashImage.Dispose();
				SplashImage = null;
			}

			if (SplashScreen != null)
			{
				SplashScreen.Dispose();
				SplashScreen = null;
			}
		}
    }
}