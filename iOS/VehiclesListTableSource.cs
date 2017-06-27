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

namespace OnDuty.iOS
{
    public class VehiclesListTableSource : UITableViewSource
    {
        List<Apparatus> TableItems;
		string CellIdentifier = "VehicleListCell";
        UITableViewController parent;


		public VehiclesListTableSource(UITableViewController parent, List<Apparatus> vehiclesList)
        {
            TableItems = vehiclesList;
            this.parent = parent;
        }

        public override UITableViewCell GetCell(UITableView tableView, NSIndexPath indexPath)
        {
            UITableViewCell cell = tableView.DequeueReusableCell(CellIdentifier);
            Apparatus item = TableItems[indexPath.Row];

			if (cell == null)
			{ cell = new UITableViewCell(UITableViewCellStyle.Subtitle, CellIdentifier); }

            cell.TextLabel.Text = item.Name;
            cell.DetailTextLabel.Text = item.Status.ToString();
            if (item.Status.DutyStatus == Core.Model.Abstract.DutyStatus.OOS) { cell.DetailTextLabel.TextColor = UIColor.FromRGB(237, 61, 56); }
            cell.Accessory = UITableViewCellAccessory.DisclosureIndicator;


            return cell;
        }

        public override nint RowsInSection(UITableView tableview, nint section)
        {
            return TableItems.Count;
        }

        public Apparatus GetItem(int index) {
            return TableItems[index];
        }

        public override void RowSelected(UITableView tableView, NSIndexPath indexPath)
        {
            //base.RowSelected(tableView, indexPath);
            VehicleDetailController detailController = (OnDuty.iOS.VehicleDetailController)parent.Storyboard.InstantiateViewController("VehicleDetailController");
            detailController.SetVehicle(this, this.TableItems[indexPath.Row]);
            parent.NavigationController.PushViewController(detailController, true);
        }

       
       
    }
}
