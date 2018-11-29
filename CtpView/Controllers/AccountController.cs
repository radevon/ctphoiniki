using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebMatrix.WebData;

namespace CtpView.Controllers
{
    [AllowAnonymous]
    public class AccountController : Controller
    {
        //
        // GET: /Account/
        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Login(LoginModel user)
        {
            if (ModelState.IsValid)
            {
                WebSecurity.Logout();
                if (WebSecurity.Login(user.UserName, user.Password, user.RememberMe))
                {
                    return RedirectToAction("Overview", "Ctp");
                }
                else
                {
                    ModelState.AddModelError("", "Ошибка авторизации! Проверьте правильность введенных данных!");
                }
            }
            return View(user);
        }

	}
}