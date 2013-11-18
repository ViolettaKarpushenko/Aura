using System.Web;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using Ninject;

namespace Aura.Web
{
    public class MvcApplication : HttpApplication
    {
        private static IKernel _kernel;

        protected void Application_Start()
        {
            RouteTable.Routes.Clear();

            RouteTable.Routes.IgnoreRoute("{resource}.axd/{*pathInfo}");
            RouteTable.Routes.IgnoreRoute("{resource}.ashx/{*pathInfo}");

            RouteTable.Routes.MapRoute("Default", "{controller}/{action}", new { controller = "Home", action = "Index" });

            RegisterGlobalDependancies();

            BundleTable.Bundles.Add(new LessBundle("~/Content/less/styles.css").Include("~/Content/less/main.less", "~/Content/less/responsive.less"));
            BundleTable.Bundles.Add(new ScriptBundle("~/Scripts/scripts.js").Include(
                "~/Scripts/jquery-{version}.js",
                "~/Scripts/jquery-ui-{version}.custom.js",
                "~/Scripts/bootstrap.js",
                "~/Scripts/underscore.js",
                "~/Scripts/backbone.js",
                "~/Scripts/modules.js",
                "~/Scripts/views/view.*"));
        }

        private static void RegisterGlobalDependancies()
        {
            _kernel = new StandardKernel(new AuraModule());
            ControllerBuilder.Current.SetControllerFactory(new AuraControllerFactory(_kernel));
        }
    }
}