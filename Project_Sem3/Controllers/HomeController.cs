using Model;
using Model.DataModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace Project_Sem3.Controllers
{
    public class HomeController : Controller
    {
        ProjectDbContext db = new ProjectDbContext();
        public ActionResult Index()
        {
            return View(db.Products.ToList());
        }
        public ActionResult Login()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Login(Customer cus)
        {
            //kiểm tra xem tài khoan  có tồn tại hay không 


            var acc = db.Customers.AsEnumerable().SingleOrDefault(x => x.email.Equals(cus.email) && x.password.Equals(cus.password));
            if (acc != null)
            {
                Session["admin"] = acc.cus_id;
                Session["ten"] = acc.email;
                return RedirectToAction("Index", "Home");

            }
            ViewBag.msg = "Tài khoản hoặc mật khẩu chưa đúng ";
           

            return View();
        }

        public ActionResult Logout()
        {
            FormsAuthentication.SignOut();
            Session.Abandon(); // it will clear the session at the end of request
            return RedirectToAction("Index", "Home");
        }
        public ActionResult SignUp()
        {
            return View();
        }
    }
}