// Copyright (C)2017 RescueCoder - All Rights Reserved
// Unauthorized copying of this file, via any medium is strictly prohibited
// Proprietary and confidential
// Written by Adam Williams <adam.williams@rescuecoder.com>, 6/2017
//
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Net.Http;
using Newtonsoft.Json.Linq;
using OnDuty.Core.API.Interface;
using OnDuty.Core.Model.Abstract;
using ApparatusModel = OnDuty.Core.Model.Entity.Apparatus;
using OnDuty.Core.Helper;
using System.Globalization;

namespace OnDuty.Core.API
{
    public class Apparatus : ApiBase, ApiInterface<ApparatusModel>, ApparatusApiInterface
    {
        protected HttpClient HttpClient;

        /** Constructor **/
        public Apparatus(string ApiURL) : base(ApiURL)
        {
            ApiPath = RootPath + "/apparatus/";
        }

        /** Get All as a List **/
        public async Task<List<ApparatusModel>> All()
        {
            var result = await Server.GetAsync(ApiPath);
            string jsonResult = await result.Content.ReadAsStringAsync();
            return GetApparatusListFromJson(jsonResult);
        }

        /** Get one as an Apparatus object **/
        public async Task<ApparatusModel> Read(int id)
        {
            var result = await Server.GetAsync(ApiPath + id.ToString());
            string jsonResult = await result.Content.ReadAsStringAsync();
            return ConvertJsonToApparatus(jsonResult);
        }

        /** Update status of indicated apparatus **/
        public async Task<HttpResult> UpdateStatus(int id, ApparatusStatus newStatus)
        {
            var encodedStatus = new FormUrlEncodedContent(new[] {
                new KeyValuePair<string, string>("personnel", newStatus.PersonnelCount.ToString()),
                new KeyValuePair<string, string>("level", newStatus.MedicalLevel),
                new KeyValuePair<string, string>("post", newStatus.Post),
                new KeyValuePair<string, string>("offduty", newStatus.OffDutyTime.ToString()),
                new KeyValuePair<string, string>("onduty", newStatus.OnDutyTime.ToString()),
                new KeyValuePair<string, string>("status", newStatus.DutyStatus == DutyStatus.ON_DUTY ? "onduty" : "offduty")
            });

            var result = await Server.PostAsync(ApiPath + "/status/" + id.ToString(), encodedStatus);
            var HttpResponseMessage = await result.Content.ReadAsStringAsync();
            var rObject = JObject.Parse(HttpResponseMessage);
            return rObject["status"].Value<string>() == "ok" ? HttpResult.SUCCESS : HttpResult.FAIL;
        }

        /** Query status of a particular apparatus **/
        public async Task<ApparatusStatus> GetStatus(int id)
        {
            ApparatusModel apparatus = await Read(id);
            return apparatus.Status;
        }



        /** Private Methods **/
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
            status.DutyStatus = ConversionHelper.StringToDutyStatus(jStatus["dutyStatus"].ToString());

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
                string JsonStatus = entry["status"].ToString();
                apparatus.Status = GetStatusObject(JsonStatus);
                ResultList.Add(apparatus);
            }

            return ResultList;
        }

        private ApparatusStatus GetStatusObject(string JsonStatus) {
            var StatusToken = JObject.Parse(JsonStatus);

            ApparatusStatus status = new ApparatusStatus();
            status.PersonnelCount = StatusToken["personnelCount"].ToObject<int>();
            status.DutyStatus = ConversionHelper.StringToDutyStatus(StatusToken["dutyStatus"].ToString());
            status.MedicalLevel = StatusToken["medicalLevel"].ToString();
			status.OnDutyTime = DateTime.Parse(StatusToken["onDutyTime"].ToString(), CultureInfo.CurrentCulture, DateTimeStyles.AssumeUniversal);
            status.OffDutyTime = DateTime.Parse(StatusToken["offDutyTime"].ToString(), CultureInfo.CurrentCulture, DateTimeStyles.AssumeUniversal);
            status.Post = StatusToken["post"].ToString();
            status.OOSReason = StatusToken["oosReason"].ToString();
            return status;
        }





        /**
         * Features we don't actually want it to have but the Interface Requires
         * 
         * These are specified by the ApiInterface, but we don't want users to actually be 
         * able to modify vehicle definitions from the mobile app.  This should only be
         * editable from the admin console on the server.
         * 
         * This may change later.  If so, define these methods and remove this notice.
         * 
         * Warning suppressed because we're not planning to implement these features and 
         * the warning is just unnecessary noise.
         * 
         */
        #pragma warning disable CS1998 // Async method lacks 'await' operators and will run synchronously

        public async Task<HttpResult> Update(ApparatusModel UpdatedObject)
        {
            throw new NotImplementedException();
        }

        public async Task<HttpResult> Create(ApparatusModel newApparatus)
        {
            throw new NotImplementedException();
        }

        public async Task<HttpResult> Delete(ApparatusModel apparatus)
        {
            throw new NotImplementedException();
        }

        #pragma warning restore CS1998 // Async method lacks 'await' operators and will run synchronously

    }
}
