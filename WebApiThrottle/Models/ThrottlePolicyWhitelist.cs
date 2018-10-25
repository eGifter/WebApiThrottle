using System;

namespace WebApiThrottle
{
    [Serializable]
    public class ThrottlePolicyWhitelist
    {
        public string Entry { get; set; }

        public ThrottlePolicyType PolicyType { get; set; }
    }
}
