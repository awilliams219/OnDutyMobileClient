﻿// Copyright (C)2017 RescueCoder - All Rights Reserved
// Unauthorized copying of this file, via any medium is strictly prohibited
// Proprietary and confidential
// Written by Adam Williams <adam.williams@rescuecoder.com>, 8/2017
//
using System;
using System.Linq;
using System.Collections.Generic;

namespace OnDuty.Core.Event.Bus
{
    public static class EventBus
    {
        static Dictionary<string, List<Action>> Bus = new Dictionary<string, List<Action>>();

        public static void Attach(string Event, Action callback) {
            if (Bus.ContainsKey(Event)) {
                Bus[Event].Add(callback);
            }
            else {
                List<Action> newEventList = new List<Action>();
                newEventList.Add(callback);
                Bus.Add(Event, newEventList);
            }
        }

        public static void Dispatch(string Event) {
            string[] eventNamespaces = Event.Split('.');
            List<string> affectedEvents = new List<string>();
            string namespaceBuilder = "";
            bool first = true;

            foreach (string nameSpace in eventNamespaces)
            {
                if (!first) namespaceBuilder += ".";
                first = false;

                namespaceBuilder += nameSpace;
                affectedEvents.Add(namespaceBuilder);
            }

            affectedEvents.Reverse();
            foreach (string affectedEvent in affectedEvents)
            {
                if (Bus.ContainsKey(affectedEvent)) {
                    foreach (Action callback in Bus[affectedEvent])
                    {
                        callback();
                    }
                }
                        
            }
        }



    }
}
