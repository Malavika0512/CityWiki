using System;
using System.Web;
using System.Web.Optimization;
using System.Web.Routing;

namespace CityWiki
{
    public class Global : HttpApplication
    {
        void Application_Start(object sender, EventArgs e)
        {
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
            Application["AccessRequest"] = 0;
            Application["StartTime"] = DateTime.Now.ToString();
            // Response.Write("Welcome!!");
        }

        void Application_End(object sender, EventArgs e)
        {
            // Response.Write("<hr />This page was last accessed at " + DateTime.Now.ToString());
            Application.Lock();
            Application["AccessRequest"] = (int)Application["AccessRequest"] - 1;
            Application.UnLock();
        }

        void Application_BeginRequest(object sender, EventArgs e)
        {
            Application.Lock();
            Application["AccessRequest"] = (int)Application["AccessRequest"] + 1;
            Application.UnLock();
        }
    }
}