using System.Configuration;

namespace WebApiThrottle
{
    public class ThrottlePolicyWhitelistConfigurationElement : ConfigurationElement
    {
        [ConfigurationProperty("entry", IsRequired = true)]
        public string Entry
        {
            get
            {
                return this["entry"] as string;
            }
        }

        [ConfigurationProperty("policyType", IsRequired = true)]
        public int PolicyType
        {
            get
            {
                return (int)this["policyType"];
            }
        }
    }
}
