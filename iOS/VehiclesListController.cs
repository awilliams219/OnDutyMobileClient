using Foundation;
using System;
using UIKit;
using ApparatusAPI = OnDuty.Core.API.Apparatus;
using OnDuty.Core.Model.Entity;
using System.Threading.Tasks;
using System.Collections.Generic;

namespace OnDuty.iOS
{
    public partial class VehiclesListController : UITableViewController
    {
        ApparatusAPI API;
        LoadingOverlay spinner;

        public VehiclesListController (IntPtr handle) : base (handle)
        {
			API = new ApparatusAPI("http://localhost/app_dev.php/api");
            RefreshVehiclesList();
        }

        public override void ViewWillAppear(bool animated) {
            RefreshVehiclesList();
        }

        private void HandlePullRefresh(object sender, EventArgs e) {
            RefreshVehiclesList();
        }

        private void AddRefreshControl() {
			if (UIDevice.CurrentDevice.CheckSystemVersion(6, 0))
			{
				// the refresh control is available, let's add it  
				RefreshControl = new UIRefreshControl();
				RefreshControl.ValueChanged += async (sender, e) =>
				{
                    await RefreshVehiclesList();
				};
			    TableView.Add(RefreshControl);	
			}

		}

       

		public override void PrepareForSegue(UIStoryboardSegue segue, NSObject sender)
		{
			//if (segue.Identifier == "VehicleDetailSegue")
			//{ // set in Storyboard
			//	var navctlr = segue.DestinationViewController as VehicleDetailController;
			//	if (navctlr != null)
			//	{
			//		var source = TableView.Source as VehiclesListTableSource;
			//		var rowPath = TableView.IndexPathForSelectedRow;
			//		var item = source.GetItem(rowPath.Row);
			//		navctlr.SetVehicle(this, item); // to be defined on the TaskDetailViewController
			//	}
			//}
		}


		public async Task<List<Apparatus>> RefreshVehiclesList()
		{
            
            var bounds = UIScreen.MainScreen.Bounds;
            spinner = new LoadingOverlay(bounds);
            View.Add(spinner);
            RefreshControl.BeginRefreshing();

			List<Apparatus> vehiclesList = await API.All();



			VehiclesListTableSource ListDataSource = new VehiclesListTableSource(this, vehiclesList);
			TableView.Source = ListDataSource;
			TableView.ReloadData();

            spinner.Hide();
            RefreshControl.EndRefreshing();




			return vehiclesList;
		}
    }
}