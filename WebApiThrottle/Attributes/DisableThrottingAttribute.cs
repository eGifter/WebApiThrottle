using System.Web.Http.Filters;

namespace WebApiThrottle
{
    public class DisableThrottingAttribute : ActionFilterAttribute, IActionFilter
    {
    }
}
