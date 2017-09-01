// Copyright (C)2017 RescueCoder - All Rights Reserved
// Unauthorized copying of this file, via any medium is strictly prohibited
// Proprietary and confidential
// Written by Adam Williams <adam.williams@rescuecoder.com>, 5/2017
//
using System;
using System.Collections.Generic;
using System.Linq;
using API = OnDuty.Core.API;
using ApparatusModel = OnDuty.Core.Model.Entity.Apparatus;

using Foundation;
using UIKit;
using System.Threading.Tasks;

namespace OnDuty.iOS
{
    public class Application
    {
        // This is the main entry point of the application.
        static void Main(string[] args)
        {
            // if you want to use a different Application Delegate class from "AppDelegate"
            // you can specify it here.

            UIApplication.Main(args, null, "AppDelegate");
        }
    }
}
