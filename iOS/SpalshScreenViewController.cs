using Foundation;
using System;
using System.Threading;
using UIKit;

namespace OnDuty.iOS
{
    public partial class SpalshScreenViewController : UIViewController
    {
        public SpalshScreenViewController (IntPtr handle) : base (handle)
        {
          
        }

        public override void ViewDidAppear(bool animated) {
            base.ViewDidAppear(animated);
            Thread.Sleep(3000);
            SegueToMenu();
        }
        public void SegueToMenu() {

            PerformSegue("LoadRootSegue", this);

        }
    }
}