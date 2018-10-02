using System;
using System.Collections.Generic;
using System.Text;

namespace HexMaster.BuildingBlocks.EventBus.Configuration
{
    public class EventBusSettings
    {
        public bool AzureServiceBusEnabled { get; set; }
        public string SubscriptionClientName { get; set; }
        public string EventBusConnection { get; set; }
        public string EventBusUserName { get; set; }
        public string EventBusPassword { get; set; }
        public int EventBusRetryCount { get; set; }

    }
}
