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
    [Register ("UiVehiclesController")]
    partial class UiVehiclesController
    {
        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.UIView VehiclesScene { get; set; }

        void ReleaseDesignerOutlets ()
        {
            if (VehiclesScene != null) {
                VehiclesScene.Dispose ();
                VehiclesScene = null;
            }
        }
    }
}