// WARNING
//
// This file has been generated automatically by Visual Studio from the outlets and
// actions declared in your storyboard file.
// Manual changes to this file will not be maintained.
//
using Foundation;
using System;
using System.CodeDom.Compiler;

namespace OnDuty.iOS
{
    [Register ("SettingsScene")]
    partial class SettingsScene
    {
        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.UITextField ApiAddress { get; set; }

        void ReleaseDesignerOutlets ()
        {
            if (ApiAddress != null) {
                ApiAddress.Dispose ();
                ApiAddress = null;
            }
        }
    }
}