using DapperAbstract;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;

namespace CtpView
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            BundleTable.EnableOptimizations = true;
            AreaRegistration.RegisterAllAreas();
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);

            //CtpSqlRepository repo = new CtpSqlRepository(ConfigurationManager.ConnectionStrings["CtpData"].ConnectionString);
            //repo.InsertCtpParameter(new CtpParameters() { BindingId=Guid.NewGuid().ToString(),RecvDate=DateTime.Now, Temp1=2.45, Temp2=34.5, Temp3=45, Pressure1=22.345, Pressure2=67.0992, Pressure3=2.1, PumpStatus=false, ValveStatus=0});
        }
    }
}
