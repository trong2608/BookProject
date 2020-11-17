using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Model;
using Model.DataModel;
using PagedList;

namespace Project_Sem3.Areas.Admin_quanli.Controllers
{
    public class AdminsController : Controller
    {
        private ProjectDbContext db = new ProjectDbContext();

        // GET: Admin_quanli/Admins
        public ActionResult Index(int? page, string id, string search)
        {
            int pageSize = 3;
            int pageNumber = page ?? 1;
            var data = db.Admins.ToList();
            if (!string.IsNullOrEmpty(search))
            {
                data = data.Where(x => x.Username.ToLower().Contains(search.ToLower())).OrderByDescending(x => x.id).ToList();
            }
            ViewBag.search = search;
            return View(data.OrderByDescending(x => x.id).ToPagedList(pageNumber, pageSize));
        }
        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Login(Admin adm)
        {
            //kiểm tra xem tài khoan  có tồn tại hay không 


            var acc = db.Admins.AsEnumerable().SingleOrDefault(x => x.Username.Equals(adm.Username) && x.Password.Equals(adm.Password));
            if (acc != null)
            {
                Session["admin"] = acc;


                return RedirectToAction("Index", "Home");

            }
            ViewBag.msg = "Tài khoản hoặc mật khẩu chưa đúng ";

            return View();


        }
        public ActionResult LogOut()
        {
            Session.Clear();
            return RedirectToAction("Login", "Admins");
        }

        // GET: Admin_quanli/Admins/Details/5
        //public ActionResult Details(string id)
        //{
        //    if (id == null)
        //    {
        //        return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
        //    }
        //    Admin admin = db.Admins.Find(id);
        //    if (admin == null)
        //    {
        //        return HttpNotFound();
        //    }
        //    return View(admin);
        //}

        //GET: Admin_quanli/Admins/Create
        //public ActionResult Create()
        //{
        //    return View();
        //}

        //POST: Admin_quanli/Admins/Create
        //To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public ActionResult Create([Bind(Include = "id,Username,Password")] Admin admin)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        db.Admins.Add(admin);
        //        db.SaveChanges();
        //        return RedirectToAction("Index");
        //    }

        //    return View(admin);
        //}

        //GET: Admin_quanli/Admins/Edit/5
        //public ActionResult Edit(string id)
        //{
        //    if (id == null)
        //    {
        //        return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
        //    }
        //    Admin admin = db.Admins.Find(id);
        //    if (admin == null)
        //    {
        //        return HttpNotFound();
        //    }
        //    return View(admin);
        //}

        //POST: Admin_quanli/Admins/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public ActionResult Edit([Bind(Include = "id,Username,Password")] Admin admin)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        db.Entry(admin).State = EntityState.Modified;
        //        db.SaveChanges();
        //        return RedirectToAction("Index");
        //    }
        //    return View(admin);
        //}

        //GET: Admin_quanli/Admins/Delete/5
        //public ActionResult Delete(string id)
        //{
        //    if (id == null)
        //    {
        //        return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
        //    }
        //    Admin admin = db.Admins.Find(id);
        //    if (admin == null)
        //    {
        //        return HttpNotFound();
        //    }
        //    return View(admin);
        //}

        //POST: Admin_quanli/Admins/Delete/5
        //[HttpPost, ActionName("Delete")]
        //[ValidateAntiForgeryToken]
        //public ActionResult DeleteConfirmed(string id)
        //{
        //    Admin admin = db.Admins.Find(id);
        //    db.Admins.Remove(admin);
        //    db.SaveChanges();
        //    return RedirectToAction("Index");
        //}

        //protected override void Dispose(bool disposing)
        //{
        //    if (disposing)
        //    {
        //        db.Dispose();
        //    }
        //    base.Dispose(disposing);
        //}
    }
}
