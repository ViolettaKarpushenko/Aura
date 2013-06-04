using System.Globalization;
using System.Threading;
using System.Web.Mvc;
using System.Web.Routing;

namespace Aura.Web
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            RouteTable.Routes.Clear();

            RouteTable.Routes.IgnoreRoute("{resource}.axd/{*pathInfo}");
            RouteTable.Routes.IgnoreRoute("{resource}.ashx/{*pathInfo}");

            RouteTable.Routes.MapRoute(
                "Default",
                "{controller}/{action}",
                new { controller = "Home", action = "Index" });
        }
    }
}