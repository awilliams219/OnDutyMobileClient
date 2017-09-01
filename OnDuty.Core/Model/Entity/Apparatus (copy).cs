// Copyright (C)2017 RescueCoder - All Rights Reserved
// Unauthorized copying of this file, via any medium is strictly prohibited
// Proprietary and confidential
// Written by Adam Williams <adam.williams@rescuecoder.com>, 6/2017
//
using System;
using OnDuty.Core.Model.Abstract;

namespace OnDuty.Core.Model.Entity
{
    public class Apparatus
    {
        public string Name { get; set; }
        public int Seats { get; set; }
        public string Vin { get; set; }
        public string Type { get; set; }
        public int Id { get; set; }
        public ApparatusStatus Status { get; set; }

        public Apparatus()
        {
        }
    }
}
