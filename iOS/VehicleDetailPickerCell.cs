using Foundation;
using System;
using UIKit;
using PickerCells;
using System.ComponentModel;
using System.Linq;
using System.Collections.Generic;

namespace OnDuty.iOS
{
    public partial class VehicleDetailPickerCell : PickerViewCell
    {
	
        public VehicleDetailPickerCell(List<string> Options) : base() {
            SetItems(Options);
        }

        public static List<string> CreateNumberRangeList(int MaxCount) {
            List<string> PickerOptions = new List<string>();
            for (int i = 0; i <= MaxCount; i++) {
                PickerOptions.Add(i.ToString());
            }

            return PickerOptions;
        }

        public static PickerViewCell Create(List<string> Options) {
            return new PickerViewCell(Options);
        }
		public static PickerViewCell Create(int MaxCount) {
            return new PickerViewCell (CreateNumberRangeList(MaxCount));
		}

    }
}