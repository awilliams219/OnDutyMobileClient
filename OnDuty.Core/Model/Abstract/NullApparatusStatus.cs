﻿// Copyright (C)2017 RescueCoder - All Rights Reserved
// Unauthorized copying of this file, via any medium is strictly prohibited
// Proprietary and confidential
// Written by Adam Williams <adam.williams@rescuecoder.com>, 8/2017
//
using System;
namespace OnDuty.Core.Model.Abstract
{
    public class NullApparatusStatus : ApparatusStatus
    {
        public NullApparatusStatus()
        {

            PersonnelCount = 0;
            MedicalLevel = "None";
            OffDutyTime = new DateTime();
            OnDutyTime = new DateTime();
            Post = "";
            DutyStatus = DutyStatus.OOS;
            OOSReason = "Error loading apparatus status";
        }
    }
}
