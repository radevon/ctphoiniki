using DapperAbstract;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CtpView.Controllers
{
    public class CtpController : Controller
    {
        CtpSqlRepository repo = new CtpSqlRepository(ConfigurationManager.ConnectionStrings["CtpData"].ConnectionString);
        //
        // GET: /Ctp/
        public ActionResult CtpList()
        {
            IEnumerable<Address> addrs = repo.GetCtpAddresses().OrderBy(x => x.ObjectName);
            return PartialView(addrs);
        }
	}
}