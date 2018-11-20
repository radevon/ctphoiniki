using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using DapperAbstract;
using System.Configuration;

namespace CtpView.Controllers
{
    public class HomeController : Controller
    {
        CtpSqlRepository repo = new CtpSqlRepository(ConfigurationManager.ConnectionStrings["CtpData"].ConnectionString);
        public ActionResult Index()
        {
            IEnumerable<CtpParameters> elems = repo.GetCtpParameters();
            return View(elems);
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}