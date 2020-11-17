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
    public class AuthorsController : Controller
    {
        private ProjectDbContext db = new ProjectDbContext();

        // GET: Admin_quanli/Authors
        public ActionResult Index(int? page, string authorid, string search)
        {
            int pageSize = 3;
            int pageNumber = page ?? 1;
            var data = db.Authors.ToList();
            if (!string.IsNullOrEmpty(search))
            {
                data = data.Where(x => x.author_name.ToLower().Contains(search.ToLower())).OrderByDescending(x => x.author_id).ToList();
            }
            ViewBag.search = search;
            return View(data.OrderByDescending(x => x.author_id).ToPagedList(pageNumber, pageSize));
        }

        // GET: Admin_quanli/Authors/Details/5
        public ActionResult Details(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Author author = db.Authors.Find(id);
            if (author == null)
            {
                return HttpNotFound();
            }
            return View(author);
        }
        

        // GET: Admin_quanli/Authors/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Admin_quanli/Authors/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Author author, HttpPostedFileBase fileUpload)
        {
            if (ModelState.IsValid)
            {
                if (fileUpload != null)
                {
                    fileUpload.SaveAs(Server.MapPath("~/Areas/Admin_quanli/Assest/anhsanpham/") + fileUpload.FileName);
                    author.image = "/Areas/Admin_quanli/Assest/anhsanpham/" + fileUpload.FileName;
                }
                var au = db.Authors.Where(x => x.author_id.Equals(author.author_id)).ToList();
                if (au.Count>0 )
                {
                    ViewBag.msg = "\" " + author.author_id + "\" đã tồn tại ";
                    return View();
                }
                db.Authors.Add(author);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(author);
        }

        // GET: Admin_quanli/Authors/Edit/5
        public ActionResult Edit(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Author author = db.Authors.Find(id);
            if (author == null)
            {
                return HttpNotFound();
            }
            return View(author);
        }

        // POST: Admin_quanli/Authors/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Author author, HttpPostedFileBase fileUpload)
        {
            if (ModelState.IsValid)
            {
                if (fileUpload != null)
                {
                    fileUpload.SaveAs(Server.MapPath("~/Areas/Admin_quanli/Assest/anhsanpham/") + fileUpload.FileName);
                    author.image = "/Areas/Admin_quanli/Assest/anhsanpham/" + fileUpload.FileName;
                }
                db.Entry(author).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(author);
        }

        // GET: Admin_quanli/Authors/Delete/5
        public ActionResult Delete(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Author author = db.Authors.Find(id);
            if (author == null)
            {
                return HttpNotFound();
            }
            return View(author);
        }

        // POST: Admin_quanli/Authors/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(string id)
        {
            Author author = db.Authors.Find(id);
            db.Authors.Remove(author);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
