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
    [Register ("VehicleDetailController")]
    partial class VehicleDetailController
    {
        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.UISegmentedControl DutyStatus { get; set; }

        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.UILabel MedicalLevel { get; set; }

        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.UILabel OffDutyTime { get; set; }

        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.UILabel OnDutyTime { get; set; }

        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.UILabel PersonnelCount { get; set; }

        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.UILabel Post { get; set; }

        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.UILabel StatusDetail { get; set; }

        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.UILabel StatusLabel { get; set; }

        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        OnDuty.iOS.TimeTravelLabel TimeTravelLabelInstance { get; set; }

        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        OnDuty.iOS.TimeTravelSlider TimeTravelSliderInstance { get; set; }

        [Action ("UpdateLabel:")]
        [GeneratedCode ("iOS Designer", "1.0")]
        partial void UpdateLabel (OnDuty.iOS.TimeTravelSlider sender);

        void ReleaseDesignerOutlets ()
        {
            if (DutyStatus != null) {
                DutyStatus.Dispose ();
                DutyStatus = null;
            }

            if (MedicalLevel != null) {
                MedicalLevel.Dispose ();
                MedicalLevel = null;
            }

            if (OffDutyTime != null) {
                OffDutyTime.Dispose ();
                OffDutyTime = null;
            }

            if (OnDutyTime != null) {
                OnDutyTime.Dispose ();
                OnDutyTime = null;
            }

            if (PersonnelCount != null) {
                PersonnelCount.Dispose ();
                PersonnelCount = null;
            }

            if (Post != null) {
                Post.Dispose ();
                Post = null;
            }

            if (StatusDetail != null) {
                StatusDetail.Dispose ();
                StatusDetail = null;
            }

            if (StatusLabel != null) {
                StatusLabel.Dispose ();
                StatusLabel = null;
            }

            if (TimeTravelLabelInstance != null) {
                TimeTravelLabelInstance.Dispose ();
                TimeTravelLabelInstance = null;
            }

            if (TimeTravelSliderInstance != null) {
                TimeTravelSliderInstance.Dispose ();
                TimeTravelSliderInstance = null;
            }
        }
    }
}