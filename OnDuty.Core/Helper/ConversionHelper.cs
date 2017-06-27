// Copyright (C)2017 RescueCoder - All Rights Reserved
// Unauthorized copying of this file, via any medium is strictly prohibited
// Proprietary and confidential
// Written by Adam Williams <adam.williams@rescuecoder.com>, 6/2017
//
using System;
using OnDuty.Core.Model.Abstract;

namespace OnDuty.Core.Helper
{
    public class ConversionHelper
    {
        public ConversionHelper()
        {
        }

        public static DutyStatus StringToDutyStatus(string Input) {
            switch (Input) {
                case "On Duty":
                    return DutyStatus.ON_DUTY;
                case "Off Duty":
                    return DutyStatus.OFF_DUTY;
                case "Out of Service":
                    return DutyStatus.OOS;
                default:
                    throw new Exception("Invalid Duty Status Type");
            }
        }

        public static string DutyStatusToString(DutyStatus Input) {
            switch (Input) {
                case DutyStatus.ON_DUTY:
                    return "On Duty";
                case DutyStatus.OFF_DUTY:
                    return "Off Duty";
                case DutyStatus.OOS:
                    return "Out of Service";
                default:
                    throw new Exception("Invalid Duty Status Value");
            }
        }
    }
}
