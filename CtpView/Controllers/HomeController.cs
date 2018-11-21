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
            IEnumerable<CtpParameters> elems = repo.GetCtpParameters("b64c3925-7fe4-4932-b549-280d5e6e8026", DateTime.Now);
            return View(elems);
        }

       
    }
}