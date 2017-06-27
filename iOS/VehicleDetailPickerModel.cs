using Foundation;
using System;
using UIKit;
using System.Collections.Generic;

namespace OnDuty.iOS
{
    public partial class VehicleDetailPickerModel : UIPickerViewModel
    {
        public delegate void SelectDelegate(string response);
        SelectDelegate ReturnSelection;
        List<string> SelectionItems;


        public VehicleDetailPickerModel (SelectDelegate selectionHandler, int Max) {
            ReturnSelection = selectionHandler;
            SelectionItems = new List<string>();

            for (int i = 0; i <= Max; i++)
            {
                SelectionItems.Add(i.ToString());
            }
        }

		public VehicleDetailPickerModel(SelectDelegate selectionHandler, List<string> Options)
		{
            ReturnSelection = selectionHandler;
            SelectionItems = Options;
		}

		public override void Selected(UIPickerView pickerView, nint row, nint component)
		{
            this.ReturnSelection(SelectionItems[(int)row]);
		}
		
        public override nint GetRowsInComponent(UIPickerView pickerView, nint component)
		{
			return SelectionItems.Count;
		}

		public override nint GetComponentCount(UIPickerView picker)
		{
			return 1;
		}
		
        public override string GetTitle(UIPickerView pickerView, nint row, nint component)
		{
			return SelectionItems[(int)row];
		}

	}
}