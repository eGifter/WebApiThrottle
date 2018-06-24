using System;

namespace WebApiThrottle
{
    [Serializable]
    public class ThrottlePolicyRule
    {
        public long LimitPerSecond { get; set; }

        public long LimitPerMinute { get; set; }

        public long LimitPerHour { get; set; }

        public long LimitPerDay { get; set; }

        public long LimitPerWeek { get; set; }

        public long SuspendTime { get; set; }

        public string Entry { get; set; }

        public ThrottlePolicyType PolicyType { get; set; }
    }
}
