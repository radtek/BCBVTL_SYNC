using log4net.Config;
using Quartz;
using Quartz.Impl;
using Sync.Web.ScheduleTasks;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;

namespace Sync.Web
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);

            JobScheduler.StartAllAsync();
            XmlConfigurator.ConfigureAndWatch(new FileInfo(Server.MapPath("~/") + "App_Start/logging.config"));
        }
    }
}
