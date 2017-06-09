// Copyright (C)2017 RescueCoder - All Rights Reserved
// Unauthorized copying of this file, via any medium is strictly prohibited
// Proprietary and confidential
// Written by Adam Williams <adam.williams@rescuecoder.com>, 6/2017
//
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace OnDuty.Core.API.Interface
{
    public interface ApiInterface<T>
    {
        Task<List<T>> All();
        Task<T> Read(int id);
        Task<HttpResult> Update(T UpdatedObject);
        Task<HttpResult> Create(T NewObject);
        Task<HttpResult> Delete(T Object);
    }
}
