using Model;
using Model.DataModel;
using Project_Sem3.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;


namespace Project_Sem3.Controllers
{
    public class UserController : Controller
    {
        ProjectDbContext db = new ProjectDbContext();
        // GET: User
        public ActionResult Register()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Register(RegisterViewModel model)
        {
            if (ModelState.IsValid)
            {
                var dao = new UserDAO();
              
                 if (dao.CheckEmail(model.Email))
                {
                    ModelState.AddModelError("email", "Email đã tồn tại");
                }
                else if (dao.CheckPhone(model.Phone))
                {
                    ModelState.AddModelError("phone", "Số điện thoại đã tồn tại");
                }
                else
                {
                    var customer = new Customer();
                    customer.cus_id = DateTime.Now.ToString("ddMMyyhhmmss");
                    customer.email = model.Email;
                    customer.cus_name = model.UserName;
                    customer.password = model.PassWord;
                    customer.phone = model.Phone;
                    customer.address = model.Address;
                    customer.create_date = DateTime.Now;
                    customer.status = true;
                    var result = dao.Insert(customer);
                    if (!String.IsNullOrEmpty(result))
                    {
                        ViewBag.Success = "Đăng ký thành công!";
                        model = new RegisterViewModel();
                        return RedirectToAction("Login", "Home");
                    }
                    else
                    {
                        ModelState.AddModelError("", "Đăng ký không thành công!");
                    }
                }
            }
            return View(model);
        }

        public ActionResult UserInfo()
        {
            return View();
        }
    }
}