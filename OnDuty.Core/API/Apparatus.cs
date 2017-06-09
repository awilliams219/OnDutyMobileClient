// Copyright (C)2017 RescueCoder - All Rights Reserved
// Unauthorized copying of this file, via any medium is strictly prohibited
// Proprietary and confidential
// Written by Adam Williams <adam.williams@rescuecoder.com>, 6/2017
//
using System;
using System.Collections.Generic;
using ApparatusModel = OnDuty.Core.Model.Entity.Apparatus;
using System.Net.Http;
using Newtonsoft.Json.Linq;
using OnDuty.Core.API.Interface;
using OnDuty.Core.Model.Entity;
using System.Threading.Tasks;
using OnDuty.Core.Model.Abstract;
using System.Diagnostics;

namespace OnDuty.Core.API
{
    public class Apparatus : ApiBase, ApiInterface<ApparatusModel>, ApparatusApiInterface
    {
        protected HttpClient HttpClient;

        public Apparatus(string ApiURL) : base(ApiURL)
        {
            ApiPath = RootPath + "/apparatus/";
        }

        public async Task<List<ApparatusModel>> All()
        {
            var result = await Server.GetAsync(ApiPath);
            string jsonResult = await result.Content.ReadAsStringAsync();
            return GetApparatusListFromJson(jsonResult);
        }

        public async Task<HttpResult> Create(ApparatusModel newApparatus)
        {
            throw new NotImplementedException();
        }

        public async Task<HttpResult> Delete(ApparatusModel apparatus)
        {
            throw new NotImplementedException();
        }

        public async Task<ApparatusModel> Read(int id)
        {
            var result = await Server.GetAsync(ApiPath + id.ToString());
            string jsonResult = await result.Content.ReadAsStringAsync();
            return ConvertJsonToApparatus(jsonResult);
        }

        private ApparatusModel ConvertJsonToApparatus(string jsonResult)
        {
            ApparatusModel apparatus = new ApparatusModel();
            JToken payload = GetPayload(jsonResult);

            apparatus.Name = payload["name"].ToString();
            apparatus.Seats = payload["seats"].ToObject<int>();
            apparatus.Vin = payload["vin"].ToString();
            apparatus.Type = payload["type"].ToString();
            apparatus.Status = ProcessStatus(payload["status"]);

            return apparatus;
        }

        private ApparatusStatus ProcessStatus(JToken jStatus)
        {
            ApparatusStatus status = new ApparatusStatus();
            status.MedicalLevel = jStatus["medicalLevel"].ToString();
            status.PersonnelCount = jStatus["personnelCount"].ToObject<int>();
            status.Post = jStatus["post"].ToString();
            status.OnDutyTime = GetDateTimeFromJToken(jStatus["onDutyTime"]);
            status.OffDutyTime = GetDateTimeFromJToken(jStatus["offDutyTime"]);

            switch (jStatus["dutyStatus"].ToString()) {
                case "On Duty": 
                    status.DutyStatus = DutyStatus.ON_DUTY;
                    break;
                case "Off Duty":
                    status.DutyStatus = DutyStatus.OFF_DUTY;
                    break;
                case "Out of Service":
                    status.DutyStatus = DutyStatus.OOS;
                    break;
                default:
                    status.DutyStatus = DutyStatus.OOS;
                    break;
            }

            return status;
        }

        private DateTime GetDateTimeFromJToken(JToken jToken)
        {
            string dateString = jToken.ToString();
            return DateTime.Parse(dateString);
        }

        private JToken GetPayload(string jsonResult)
        {
            JObject parsedJson = JObject.Parse(jsonResult);
            return parsedJson.GetValue("payload");
        }

        public async Task<HttpResult> Update(ApparatusModel UpdatedObject)
        {
            throw new NotImplementedException();
        }

        private List<ApparatusModel> GetApparatusListFromJson(string Json)
        {
            List<ApparatusModel> ResultList = new List<ApparatusModel>();

            var result = JObject.Parse(Json);
            var Payload = result.GetValue("payload");

            foreach (var entry in Payload) {
                ApparatusModel apparatus = new ApparatusModel();
                apparatus.Name = entry["name"].ToString();
                apparatus.Type = entry["type"].ToString();
                apparatus.Seats = entry["seats"].ToObject<int>();
                apparatus.Vin = entry["vin"].ToString();
                apparatus.Status = CreateNullStatusValue(entry["status"].ToString());
                ResultList.Add(apparatus);
            }

            return ResultList;
        }

        private ApparatusStatus CreateNullStatusValue(string StatusString) {
            ApparatusStatus status = new ApparatusStatus();
            status.PersonnelCount = 0;
            status.DutyStatus = DutyStatus.OFF_DUTY;
            status.MedicalLevel = "NONE";
            status.OnDutyTime = new DateTime();
            status.OffDutyTime = new DateTime();
            status.Post = "Station";
            return status;
        }

        public async Task<HttpResult> UpdateStatus(ApparatusStatus newStatus)
        {
            throw new NotImplementedException();
        }

        public ApparatusStatus GetStatus(int id)
        {
            throw new NotImplementedException();
        }
    }
}
