// Copyright (C)2017 RescueCoder - All Rights Reserved
// Unauthorized copying of this file, via any medium is strictly prohibited
// Proprietary and confidential
// Written by Adam Williams <adam.williams@rescuecoder.com>, 8/2017
//
using System;
using OnDuty.Core.Model.Abstract;

namespace OnDuty.Core.Model.Entity
{
    public class NullApparatus : Apparatus
    {
        public NullApparatus()
        {
            Name = "";
            Seats = 0;
            Vin = "";
            Type = "";
            Id = -1;
            Status = new NullApparatusStatus();
        }
    }
}
