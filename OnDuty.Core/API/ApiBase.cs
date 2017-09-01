// Copyright (C)2017 RescueCoder - All Rights Reserved
// Unauthorized copying of this file, via any medium is strictly prohibited
// Proprietary and confidential
// Written by Adam Williams <adam.williams@rescuecoder.com>, 6/2017
//
using System;
using System.IO;
using System.Net.Http;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace OnDuty.Core.API
{
    public enum HttpResult { SUCCESS, FAIL }

    public abstract class ApiBase
    {
		protected string ApiPath { get; set; }

  		protected string RootPath;
        protected static HttpClient Server;

        public ApiBase(string ApiURL)
        {
            RootPath = ApiURL;
            if (Server == null) {
                Server = new HttpClient();
            }
        }

        protected JObject Parse (string JSON) {
            JsonReader reader = new JsonTextReader(new StringReader(JSON));
            reader.DateParseHandling = DateParseHandling.None;
            return JObject.Load(reader);
        }




    }
}
