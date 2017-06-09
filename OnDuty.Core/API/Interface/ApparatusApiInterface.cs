// Copyright (C)2017 RescueCoder - All Rights Reserved
// Unauthorized copying of this file, via any medium is strictly prohibited
// Proprietary and confidential
// Written by Adam Williams <adam.williams@rescuecoder.com>, 6/2017
//
using System;
using System.Threading.Tasks;
using OnDuty.Core.Model.Abstract;

namespace OnDuty.Core.API.Interface
{
    public interface ApparatusApiInterface
    {
        Task<HttpResult> UpdateStatus(ApparatusStatus newStatus);
        ApparatusStatus GetStatus(int id);
    }
}
