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
         
        public ApparatusStatus()
        {
        }
    }
}
