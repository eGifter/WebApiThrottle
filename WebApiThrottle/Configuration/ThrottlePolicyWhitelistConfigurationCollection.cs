using System.Configuration;

namespace WebApiThrottle
{
    public class ThrottlePolicyWhitelistConfigurationCollection : ConfigurationElementCollection
    {
        protected override ConfigurationElement CreateNewElement()
        {
            return new ThrottlePolicyWhitelistConfigurationElement();
        }

        protected override object GetElementKey(ConfigurationElement element)
        {
            return ((ThrottlePolicyWhitelistConfigurationElement)element).Entry;
        }
    }
}
