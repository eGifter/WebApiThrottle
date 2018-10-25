using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.ServiceModel.Channels;
using System.Web;

namespace WebApiThrottle.Net
{
    public static class HttpRequestExtensions
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public static string GetClientIpAddress(this HttpRequestMessage request)
        {
            // get the CF-Connecting-IP headers (should only really be one)
            if (request.Headers.TryGetValues("CF-Connecting-IP", out IEnumerable<string> cfConnectionIps))
            {
                //Request going through Cloudflare, use the IP they send
                return cfConnectionIps.First();
            }

            // Always return all zeroes for any failure (my calling code expects it)
            var ipAddress = "0.0.0.0";

            if (request.Properties.ContainsKey("MS_HttpContext"))
            {
                ipAddress = ((HttpContextBase)request.Properties["MS_HttpContext"]).Request.UserHostAddress;
            }
            else if (request.Properties.ContainsKey(RemoteEndpointMessageProperty.Name))
            {
                ipAddress = ((RemoteEndpointMessageProperty)request.Properties[RemoteEndpointMessageProperty.Name]).Address;
            }

            if (request.Properties.ContainsKey("MS_OwinContext"))
            {
                ipAddress = ((Microsoft.Owin.OwinContext) request.Properties["MS_OwinContext"]).Request.RemoteIpAddress;
            }

            // get the X-Forward-For headers (should only really be one)
            if (!request.Headers.TryGetValues("X-Forwarded-For", out IEnumerable<string> xForwardForList))
            {
                return ipAddress;
            }

            var xForwardedFor = xForwardForList.FirstOrDefault();

            // check that we have a value
            if (string.IsNullOrEmpty(xForwardedFor))
            {
                return ipAddress;
            }

            // Get a list of public ip addresses in the X_FORWARDED_FOR variable
            var publicForwardingIps = xForwardedFor.Split(',').Where(ip => !IpAddressUtil.IsPrivateIpAddress(ip)).ToList();

            // If we found any, return the first one, otherwise return the user host address
            return publicForwardingIps.Count > 0 ? publicForwardingIps.First() : ipAddress;
        }
    }
}