using Foundation;
using System;
using UIKit;
using ApparatusAPI = OnDuty.Core.API.Apparatus;
using System.Collections.Generic;
using OnDuty.Core.Model.Entity;
using System.Threading.Tasks;

namespace OnDuty.iOS
{
    public partial class UIVehiclesList : UITableView
    {
        ApparatusAPI API;
        UIActivityIndicatorView VehicleLoader;
        VehiclesListTableSource ListDataSource;

        public UIVehiclesList (IntPtr handle) : base (handle)
        {
			API = new ApparatusAPI("http://localhost/app_dev.php/api");
			VehicleLoader = new UIActivityIndicatorView(UIActivityIndicatorViewStyle.WhiteLarge);
			RefreshVehiclesList();

		}

        public async Task<List<Apparatus>> RefreshVehiclesList() {


            UIActivityIndicatorView loader = UIFactory.UiElementFactoryHelper.CreateLoader();
            AddSubview(loader);
            BringSubviewToFront(loader);
            loader.StartAnimating();

            List<Apparatus> vehiclesList = await API.All();

            ListDataSource = new VehiclesListTableSource(this, vehiclesList);
            Source = ListDataSource;
            ReloadData();


            loader.StopAnimating();


            return vehiclesList;
        }

   

       


    }
}