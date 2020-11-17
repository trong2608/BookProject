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
    public class CategoriesController : Controller
    {
        private ProjectDbContext db = new ProjectDbContext();

        // GET: Admin_quanli/Categories
        public ActionResult Index(int? page, string search)
        {
            int pageSize = 3;
            int pageNumber = page ?? 1;
            var data = db.Categorys.ToList();
            if (!string.IsNullOrEmpty(search))
            {
                data = data.Where(x => x.cat_name.ToLower().Contains(search.ToLower())).OrderByDescending(x => x.cat_id).ToList();
            }
            ViewBag.search = search;
            return View(data.OrderByDescending(x => x.cat_id).ToPagedList(pageNumber, pageSize));
        }

        // GET: Admin_quanli/Categories/Details/5
        public ActionResult Details(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Category category = db.Categorys.Find(id);
            if (category == null)
            {
                return HttpNotFound();
            }
            return View(category);
        }

        // GET: Admin_quanli/Categories/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Admin_quanli/Categories/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "cat_id,cat_name,status,Description")] Category category)
        {
            if (ModelState.IsValid)
            {
                var cat = db.Categorys.Where(x => x.cat_id.Equals(category.cat_id)).ToList();
                if (cat.Count > 0)
                {
                    ViewBag.msg = "\" " + category.cat_id + "\" đã tồn tại ";
                    return View();
                }
                db.Categorys.Add(category);

                db.SaveChanges();

                return RedirectToAction("Index");

            }

            return View(category);
        }

        // GET: Admin_quanli/Categories/Edit/5
        public ActionResult Edit(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Category category = db.Categorys.Find(id);
            if (category == null)
            {
                return HttpNotFound();
            }
            return View(category);
        }

        // POST: Admin_quanli/Categories/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "cat_id,cat_name,status,Description")] Category category)
        {
            if (ModelState.IsValid)
            {
                db.Entry(category).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(category);
        }

        // GET: Admin_quanli/Categories/Delete/5
        public ActionResult Delete(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Category category = db.Categorys.Find(id);
            if (category == null)
            {
                return HttpNotFound();
            }
            return View(category);
        }

        // POST: Admin_quanli/Categories/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(string id)
        {
            Category category = db.Categorys.Find(id);
            db.Categorys.Remove(category);
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
