using System.Web.Hosting;
using System.Web.Mvc;
using System.Web.Routing;

namespace ViewsFromMongoDB
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            HostingEnvironment.RegisterVirtualPathProvider(new CustomVirtualPathProvider());
        }
    }
}
