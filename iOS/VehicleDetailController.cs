using Foundation;
using System;
using UIKit;
using OnDuty.Core.Model.Entity;
using CoreDutyStatus = OnDuty.Core.Model.Abstract.DutyStatus;
using CoreGraphics;
using System.Collections.Generic;
using static OnDuty.iOS.VehicleDetailPickerModel;

namespace OnDuty.iOS
{
    public partial class VehicleDetailController : UITableViewController
    {
        UIColor ActiveBlue = UIColor.FromRGB(29, 118, 252);
        UIColor OOSColor = UIColor.FromRGB(237, 61, 56);
        UIColor TimeTravelColor = UIColor.DarkGray;
        UIPickerView currentPicker;

        bool OOS = false;

        private void SetTimeTravelMode(bool TimeTravel) {
            
            if (TimeTravel) {
                setInputColors(TimeTravelColor, true);
                setDisclosureMarks(false, true);
            }
            else {
                if (OOS) {
                    setOOSInputMode();
                    Post.TextColor = ActiveBlue;
                    TableView.VisibleCells[4].Accessory = UITableViewCellAccessory.DisclosureIndicator;

                }
                else {
                    setInputColors(ActiveBlue, true);
                    setDisclosureMarks(true, true);
                }
				TimeTravelLabelInstance.Text = "Now";
			}
        }

        private void setInputColors(UIColor color, bool includePostField) {
			PersonnelCount.TextColor = color;

			MedicalLevel.TextColor = color;
			OnDutyTime.TextColor = color;
			OffDutyTime.TextColor = color;
            if (includePostField) { Post.TextColor = color; }
			DutyStatus.TintColor = color;
			TimeTravelSliderInstance.MaximumTrackTintColor = color;
			TimeTravelSliderInstance.MinimumTrackTintColor = color;
			TimeTravelLabelInstance.TextColor = color;
           			
        }

        private void setDisclosureMarks(bool Enabled, bool IncludePostField) {
            int MaxField = (IncludePostField) ? 4 : 3;
            UITableViewCellAccessory accessory = (Enabled) ? UITableViewCellAccessory.DisclosureIndicator : UITableViewCellAccessory.None;

            for (int i = 0; i <= MaxField; i++)
			{
				TableView.VisibleCells[i].Accessory = accessory;
			}
        }


        private void EnableOutOfServiceMode() {
            OOS = true;
            StatusDetail.Text = "OOS - " + currentVehicle.Status.ToString(false);
            setOOSInputMode();
        }

        private void setOOSInputMode() {
			setInputColors(OOSColor, false);
			setDisclosureMarks(false, false);
        }

        partial void UpdateLabel(TimeTravelSlider sender)
        {
            
			int TimeDelta = (int)sender.Value;
			DateTime now = DateTime.Now;
			DateTime displayedTime = now.AddHours(TimeDelta);


            switch (TimeDelta) {
                case 0:
                    SetTimeTravelMode(false);
                    break;
                default:
                    SetTimeTravelMode(true);
                    TimeTravelLabelInstance.Text = displayedTime.ToString("ddd MM/dd HH:mm");
                    break;
            }
			
		}

        Apparatus currentVehicle { get; set; }
        public VehiclesListTableSource Delegate { get; set; }

        public VehicleDetailController (IntPtr handle) : base (handle)
        {
        }


        public override void ViewWillAppear(bool animated)
        {
            PersonnelCount.Text = currentVehicle.Status.PersonnelCount.ToString();
            MedicalLevel.Text = currentVehicle.Status.MedicalLevel;
            OnDutyTime.Text = currentVehicle.Status.OnDutyTime.ToString("ddd, MM/dd HH:mm");
            OffDutyTime.Text = currentVehicle.Status.OffDutyTime.ToString("ddd, MM/dd HH:mm");
            StatusDetail.Text = currentVehicle.Status.ToString(false);

            Post.Text = currentVehicle.Status.Post;

            switch (currentVehicle.Status.DutyStatus) {
                case CoreDutyStatus.ON_DUTY:
                    DutyStatus.SelectedSegment = 0;
                    break;
                case CoreDutyStatus.OFF_DUTY:
                    DutyStatus.SelectedSegment = 1;
                    break;
                default:
                    DutyStatus.SelectedSegment = 2;
                    break;
            }

            TimeTravelSliderInstance.SetValue(0, false);
			TimeTravelSliderInstance.MaximumTrackTintColor = UIColor.DarkGray;
			TimeTravelSliderInstance.MinimumTrackTintColor = UIColor.DarkGray;
			TimeTravelLabelInstance.TextColor = UIColor.DarkGray;
			TimeTravelLabelInstance.Text = "Now"; ;
        }

        public override void ViewDidAppear(bool animated) {
            if (currentVehicle.Status.DutyStatus == CoreDutyStatus.OOS) {
                EnableOutOfServiceMode();
            }
        }

        public void SetVehicle (VehiclesListTableSource d, Apparatus vehicle) {
            Delegate = d;
            currentVehicle = vehicle;
        }

        public override void RowSelected(UITableView tableView, NSIndexPath indexPath) {
            switch (indexPath.Row) {
                case 0:
                    updatePersonnelCount();
                    break;
                case 1:
                    updateMedicalLevel();
                    break;
            }
        }

        private void updatePersonnelCount() {
            BuildPicker(handlePersonnelUpdate, currentVehicle.Seats);
        }

		private void updateMedicalLevel()
		{
            BuildPicker(handleMedicalLevelUpdate, new List<string>(new string[] {
                "Non-emergency",
                "EMR",
                "BLS",
                "ALS"
            }));
		}

        private void BuildPicker(SelectDelegate SelectionHandler, object PickerData) {
            
			UIPickerView customPickerView = new UIPickerView(CGRect.Empty)
			{
				AutoresizingMask = UIViewAutoresizing.FlexibleWidth,
                Model = InstantiatePicker(SelectionHandler, PickerData),
				ShowSelectionIndicator = true,
				BackgroundColor = UIColor.White,
				Hidden = true
			};
			customPickerView.Frame = PickerFrameWithSize(customPickerView.SizeThatFits(CGSize.Empty));
			View.AddSubview(customPickerView);
			customPickerView.Hidden = false;
			currentPicker = customPickerView;
        }

        private VehicleDetailPickerModel InstantiatePicker(SelectDelegate Handler, object PickerData) {
            if (PickerData is int)
            {
                return new VehicleDetailPickerModel(Handler, (int)PickerData);
            }
            else if (PickerData is List<string>)
            {
                return new VehicleDetailPickerModel(Handler, (List<string>)PickerData);
            }
            else throw new Exception("Invalid Picker Data Set");
        }

		

		CGRect PickerFrameWithSize(CGSize size)
		{
			var screenRect = UIScreen.MainScreen.ApplicationFrame;
			return new CGRect(0f, screenRect.Height - 84f - size.Height, size.Width, size.Height);
		}
      
        public void handlePersonnelUpdate(string newPersonnelCount) {
            int count = int.Parse(newPersonnelCount);
            currentVehicle.Status.PersonnelCount = count;
            PersonnelCount.Text = newPersonnelCount;
            currentPicker.Hidden = true;
        }
		public void handleMedicalLevelUpdate(string newMedicalLevel)
		{
			currentVehicle.Status.MedicalLevel = newMedicalLevel;
			MedicalLevel.Text= newMedicalLevel;
			currentPicker.Hidden = true;
		}
    }
}