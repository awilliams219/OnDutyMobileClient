using Foundation;
using System;
using UIKit;
using ApparatusAPI = OnDuty.Core.API.Apparatus;
using OnDuty.Core.Model.Entity;
using System.Threading.Tasks;
using System.Collections.Generic;
using OnDuty.Core.Event.Bus;

namespace OnDuty.iOS
{
    public partial class VehiclesListController : UITableViewController
    {
        ApparatusAPI API;
        LoadingOverlay spinner;
        bool useRefreshControl = false;
        new UIRefreshControl RefreshControl;
        bool isInitializing = true;

        public VehiclesListController (IntPtr handle) : base (handle)
        {
			API = new ApparatusAPI(OnDuty.Core.Manager.SettingsManager.ApiRootPath);
            EventBus.Attach("api.update.vehicle.status.before", ShowUpdateSpinner); 
            EventBus.Attach("api.update.vehicle.status.after", forceRefresh);
        }

        public async void forceRefresh() {
			if (!isInitializing)
			{
				await RefreshVehiclesListAsync(true);
			}
        }

        public override async void ViewDidLoad() {
            base.ViewDidLoad();
            await RefreshVehiclesListAsync();
            AddRefreshControl();
            if (useRefreshControl) TableView.Add(RefreshControl);
            isInitializing = false;
		}

        private void AddRefreshControl() {
			if (UIDevice.CurrentDevice.CheckSystemVersion(6, 0))
			{
				// the refresh control is available, let's add it  
				RefreshControl = new UIRefreshControl();
				RefreshControl.ValueChanged += async (sender, e) =>
				{
                    await RefreshVehiclesListAsync();
				};
                useRefreshControl = true;
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

        protected async Task GetDataFromApi(){
			List<Apparatus> vehiclesList = await API.All();
            if (vehiclesList.Count > 0)
            {
                VehiclesListTableSource ListDataSource = new VehiclesListTableSource(this, vehiclesList);
                TableView.Source = ListDataSource;
                TableView.ReloadData();
            }
            else {

                UIAlertController _error = UIAlertController.Create("Error", "Could not load vehicles list.  Please check your server settings.  If problem persists, contact your administrator.", UIAlertControllerStyle.Alert);
                _error.AddAction(UIAlertAction.Create("OK", UIAlertActionStyle.Default, (action) => { }));
                this.PresentViewController(_error, true, null);
            }
        }


        public async Task RefreshVehiclesListAsync(bool showSpinner = false)
		{
            BeginRefresh(showSpinner);

            await GetDataFromApi();

            EndRefresh(showSpinner);
            isInitializing = false;
   		}
	
		protected void BeginRefresh (bool showSpinner) {
            if (useRefreshControl && (! showSpinner))
			{
				RefreshControl.BeginRefreshing();
			}
			else
			{
				ShowSpinner();
			}
		}

        protected void EndRefresh(bool showSpinner) {
			if (useRefreshControl && (! showSpinner))
			{
				RefreshControl.EndRefreshing();
			}
			else
			{
				HideSpinner();
			}
        }

        public void ShowUpdateSpinner() {
            ShowSpinner("Updating Data...");
        }

        protected void ShowSpinner(string LoadingText = "Loading Data...") {
            if (spinner == default(LoadingOverlay))
            {
                var bounds = UIScreen.MainScreen.Bounds;
                spinner = new LoadingOverlay(bounds, LoadingText);
                View.Add(spinner);
            }
        }

        protected void HideSpinner() {
            spinner.Hide();
            spinner = null;
        }
    }
}