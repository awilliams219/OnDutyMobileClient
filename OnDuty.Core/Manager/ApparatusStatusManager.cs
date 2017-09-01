// Copyright (C)2017 RescueCoder - All Rights Reserved
// Unauthorized copying of this file, via any medium is strictly prohibited
// Proprietary and confidential
// Written by Adam Williams <adam.williams@rescuecoder.com>, 7/2017
//
using System;
using OnDuty.Core.Model.Abstract;
using OnDuty.Core.Model.Entity;
using ApparatusApi = OnDuty.Core.API.Apparatus;
using OnDuty.Core.Event.Bus;

namespace OnDuty.Core.Manager
{
    public static class ApparatusStatusManager
    {
        private static ApparatusStatus NewApparatusStatus = new ApparatusStatus();
        private static bool NeedsCommit = false;
		


		public static void Init(ApparatusStatus InitialStatus) {
            PersonnelCount = InitialStatus.PersonnelCount;
            MedicalLevel = InitialStatus.MedicalLevel;
            OnDutyTime = InitialStatus.OnDutyTime.ToUniversalTime();
            OffDutyTime = InitialStatus.OffDutyTime.ToUniversalTime();
            DutyStatus = InitialStatus.DutyStatus;
            Post = InitialStatus.Post;

		    NeedsCommit = false;
	}

        public static int PersonnelCount
        {
            get
            {
                return NewApparatusStatus.PersonnelCount;
            }
            set
            {
                NewApparatusStatus.PersonnelCount = value;
                NeedsCommit = true;
            }
        }

        public static String TextStatus
        {
            get
            {
                switch(NewApparatusStatus.DutyStatus) {
                    case DutyStatus.ON_DUTY:
                        return "On Duty";
                    case DutyStatus.OOS:
                        return "Out of Service";
                    default:
						return "Off Duty";
                }
            }
            set
            {
                switch (value) {
                    case "On Duty" :
                        NewApparatusStatus.DutyStatus = DutyStatus.ON_DUTY;
                        break;
                    case "OOS" :
                        NewApparatusStatus.DutyStatus = DutyStatus.OOS;
                        break;
                    default:
                        NewApparatusStatus.DutyStatus = DutyStatus.OFF_DUTY;
                        break;
                }

                NeedsCommit = true;
            }
        }

        public static DutyStatus DutyStatus {
            get {
                return NewApparatusStatus.DutyStatus;
            }
            set {
                NewApparatusStatus.DutyStatus = value;
            }
        }

        public static string MedicalLevel
        {
            get
            {
                return NewApparatusStatus.MedicalLevel;
            }
            set
            {
                NewApparatusStatus.MedicalLevel = value;
                NeedsCommit = true;
            }
        }

        public static DateTime OnDutyTime
        {
            get
            {
                return NewApparatusStatus.OnDutyTime;
            }
            set
            {
                NewApparatusStatus.OnDutyTime = value;
                NeedsCommit = true;
            }
        }

        public static DateTime OffDutyTime
        {
            get
            {
                return NewApparatusStatus.OffDutyTime;
            }
            set
            {
                NewApparatusStatus.OffDutyTime = value;
                NeedsCommit = true;
            }
        }

        public static string Post
        {
            get
            {
                return NewApparatusStatus.Post;
            }
            set
            {
                NewApparatusStatus.Post = value;
                NeedsCommit = true;
            }
        }

        public static Apparatus Apparatus{
            get; set;
        }
                


        public static void Clear() {
            NewApparatusStatus = new ApparatusStatus();
            Apparatus = default(Apparatus);
            NeedsCommit = false;

        }

        public static async void Commit() {
            if (NeedsCommit)
            {
                int id = Apparatus.Id;
                ApparatusApi api = new ApparatusApi(OnDuty.Core.Manager.SettingsManager.ApiRootPath);

                await api.UpdateStatus(id, NewApparatusStatus);
            }

            Clear(); 
        }
    }
}
