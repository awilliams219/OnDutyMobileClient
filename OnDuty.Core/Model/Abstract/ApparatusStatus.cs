// Copyright (C)2017 RescueCoder - All Rights Reserved
// Unauthorized copying of this file, via any medium is strictly prohibited
// Proprietary and confidential
// Written by Adam Williams <adam.williams@rescuecoder.com>, 6/2017
//
using System;
namespace OnDuty.Core.Model.Abstract
{
    public enum DutyStatus { ON_DUTY, OFF_DUTY, OOS };

    public class ApparatusStatus
    {
        public int PersonnelCount { get; set; }
        public string MedicalLevel { get; set; }
        public DateTime OffDutyTime { get; set; }
        public DateTime OnDutyTime { get; set; }
        public string Post { get; set;}
        public DutyStatus DutyStatus { get; set; }
        public string OOSReason { get; set; }
         
        public ApparatusStatus()
        {
        }

        public override string ToString() {
            return ToString(true);
        }

        public string ToString(bool IncludePreamble) {
            string preamble;

            switch (DutyStatus) {
                case DutyStatus.OFF_DUTY:
                    return "Off Duty";
                case DutyStatus.OOS:
                    preamble = "Out of Service" + " - ";
                    return (IncludePreamble ? preamble : "") + OOSReason;
                default:
                    string postSuffix = "";
                    preamble = "On Duty ";
                    if (Post != "") {
                        postSuffix = " / " + Post;
                    }
                    return (IncludePreamble ? preamble : "") + "C" + PersonnelCount.ToString() + "0 " + MedicalLevel + " until " + OffDutyTime.ToString("HH:mm") + postSuffix;
                
            }
        }
    }
}
