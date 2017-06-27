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
    [Register ("VehiclesListController")]
    partial class VehiclesListController
    {
        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.UITableView VehiclesList { get; set; }

        void ReleaseDesignerOutlets ()
        {
            if (VehiclesList != null) {
                VehiclesList.Dispose ();
                VehiclesList = null;
            }
        }
    }
}