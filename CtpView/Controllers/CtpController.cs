using DapperAbstract;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CtpView.Controllers
{
    [ApplicationAuthorize]
    public class CtpController : Controller
    {
        CtpSqlRepository repo = new CtpSqlRepository(ConfigurationManager.ConnectionStrings["CtpData"].ConnectionString);
        //
        // GET: /Ctp/
        public ActionResult CtpList()
        {
            IEnumerable<Address> addrs = Enumerable.Empty<Address>();
            try
            {
                addrs = repo.GetCtpAddresses().OrderBy(x => x.ObjectName);
            }
            catch (Exception ex)
            {
                // лог
            } 
            return PartialView(addrs);
        }

        public ActionResult Overview()
        {
            return View();
        }

        public ActionResult LoadLastData()
        {
            IEnumerable<AddressCtpData> elems = repo.GetLastDataByObjects();
            return PartialView(elems);
        }

        public ActionResult DetailsAddress(int Id)
        {
            Address addr = repo.GetCtpAddress(Id);
            if (addr == null)
            {
                return View("DisplayMessage", (object)"<span class='text-danger'>Объект с таким идентификатором не обнаружен в базе данных!</span>");
            }
            return View(addr);
        }

        public ActionResult CtpData(string BindingId,DateTime from, DateTime to)
        {
            List<CtpParameters> data = repo.GetCtpParameters(BindingId, from,to).ToList();
            return Json(data, JsonRequestBehavior.AllowGet);
        }
	}
}