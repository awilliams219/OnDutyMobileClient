// Copyright (C)2017 RescueCoder - All Rights Reserved
// Unauthorized copying of this file, via any medium is strictly prohibited
// Proprietary and confidential
// Written by Adam Williams <adam.williams@rescuecoder.com>, 6/2017
//
using System;
using Foundation;
using UIKit;
using System.Collections.Generic;
using OnDuty.Core.Model.Entity;
using OnDuty.Core.Model.Abstract;
using PickerCells;

namespace OnDuty.iOS
{
	public class VehiclesDetailTableSource : UITableViewSource
	{
        Apparatus CurrentApparatus;
		string CellIdentifier = "VehicleDetailCell";
        List<UITableViewCell> DetailItems;
        UITableView Delegate;
        VehicleDetailController Parent;


		public VehiclesDetailTableSource(Apparatus Apparatus, UITableView TableView, VehicleDetailController Caller)
		{
            CurrentApparatus = Apparatus;
            DetailItems = new List<UITableViewCell>();
            Delegate = TableView;
            Parent = Caller;
            BuildTable();
		}

        protected void BuildTable() {
            string[] MedicalLevels = new string[] {
                "None",
                "Nonemergency",
                "EMR",
                "BLS",
                "ALS"
            };


            PickerViewCell PersonnelCell = VehicleDetailPickerCell.Create(CurrentApparatus.Seats);
            PersonnelCell.TextLabel.Text = "Personnel Count";
            PersonnelCell.RightLabelTextAlignment = UITextAlignment.Right;
            PersonnelCell.Accessory = UITableViewCellAccessory.DisclosureIndicator;
            PersonnelCell.SelectedObject = new object[] { CurrentApparatus.Status.PersonnelCount };
            PersonnelCell.OnItemChanged += (object sender, PickerCellArgs e) =>
            {
                Parent.UpdatePersonnel(Convert.ToInt32(e.Items[0]));
            };

            PickerViewCell MedicalLevelCell = VehicleDetailPickerCell.Create(new List<string>(MedicalLevels));
            MedicalLevelCell.TextLabel.Text = "Medical Level";
            MedicalLevelCell.RightLabelTextAlignment = UITextAlignment.Right;
            MedicalLevelCell.Accessory = UITableViewCellAccessory.DisclosureIndicator;
            MedicalLevelCell.SelectedObject = new object[] { System.Array.IndexOf(MedicalLevels, CurrentApparatus.Status.MedicalLevel) };
			MedicalLevelCell.OnItemChanged += (object sender, PickerCellArgs e) =>
			{
                Parent.UpdateMedicalLevel((string) e.Items[0]);
				
			};

            DatePickerCell OnDutyTimeCell = new DatePickerCell(UIDatePickerMode.DateAndTime, CurrentApparatus.Status.OnDutyTime);
            OnDutyTimeCell.TextLabel.Text = "On Duty Time";
			OnDutyTimeCell.RightLabelTextAlignment = UITextAlignment.Right;
			OnDutyTimeCell.Accessory = UITableViewCellAccessory.DisclosureIndicator;
			OnDutyTimeCell.OnItemChanged += (object sender, PickerCellArgs e) =>
			{
                Parent.UpdateOnDutyTime((DateTime)e.Items[0]);
				
			};


            DatePickerCell OffDutyTimeCell = new DatePickerCell(UIDatePickerMode.DateAndTime, CurrentApparatus.Status.OffDutyTime);
			OffDutyTimeCell.TextLabel.Text = "Off Duty Time";
			OffDutyTimeCell.RightLabelTextAlignment = UITextAlignment.Right;
			OffDutyTimeCell.Accessory = UITableViewCellAccessory.DisclosureIndicator;
			OffDutyTimeCell.OnItemChanged += (object sender, PickerCellArgs e) =>
			{
                Parent.UpdateOffDutyTime((DateTime) e.Items[0]);

			};

            Parent.UpdatePost("Test Post, Please Ignore");


			DetailItems.Add(PersonnelCell);
            DetailItems.Add(MedicalLevelCell);
            DetailItems.Add(OnDutyTimeCell);
            DetailItems.Add(OffDutyTimeCell);
        }

		public override nfloat GetHeightForRow(UITableView tableView, NSIndexPath indexPath)
		{

			var aCell = GetCell(tableView, indexPath);

			if (aCell is BasePickerCell)
			{
				var datePickerTableViewCell = aCell as BasePickerCell;

				return datePickerTableViewCell.PickerHeight;

			}

			return 44.0f;

		}

		public override UITableViewCell GetCell(UITableView tableView, NSIndexPath indexPath)
		{
            return DetailItems[indexPath.Row];
		}

		public override nint RowsInSection(UITableView tableview, nint section)
		{
			return DetailItems.Count;
		}

	
		public override void RowSelected(UITableView tableView, NSIndexPath indexPath)
		{
			var aCell = GetCell(tableView, indexPath);

			if (aCell is BasePickerCell)
			{
				var datePickerTableViewCell = aCell as BasePickerCell;

				datePickerTableViewCell.SelectedInTableView(tableView);

				tableView.DeselectRow(indexPath, true);
			}
		}
	}
}
