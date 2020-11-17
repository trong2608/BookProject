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
    public class PublishersController : Controller
    {
        private ProjectDbContext db = new ProjectDbContext();

        // GET: Admin_quanli/Publishers
        public ActionResult Index(int? page, string authorid, string search)
        {
            int pageSize = 3;
            int pageNumber = page ?? 1;
            var data = db.Publishers.ToList();
            if (!string.IsNullOrEmpty(search))
            {
                data = data.Where(x => x.pub_name.ToLower().Contains(search.ToLower())).OrderByDescending(x => x.pub_id).ToList();
            }
            ViewBag.search = search;
            return View(data.OrderByDescending(x => x.pub_id).ToPagedList(pageNumber, pageSize));
        }

        // GET: Admin_quanli/Publishers/Details/5
        public ActionResult Details(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Publisher publisher = db.Publishers.Find(id);
            if (publisher == null)
            {
                return HttpNotFound();
            }
            return View(publisher);
        }

        // GET: Admin_quanli/Publishers/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Admin_quanli/Publishers/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "pub_id,pub_name,Description")] Publisher publisher)
        {
            if (ModelState.IsValid)
            {
                var pub = db.Publishers.Where(x => x.pub_id.Equals(publisher.pub_id)).ToList();
                if (pub.Count > 0)
                {
                    ViewBag.msg = "\" " + publisher.pub_id + "\" đã tồn tại ";
                    return View();
                }
                db.Publishers.Add(publisher);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(publisher);
        }

        // GET: Admin_quanli/Publishers/Edit/5
        public ActionResult Edit(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Publisher publisher = db.Publishers.Find(id);
            if (publisher == null)
            {
                return HttpNotFound();
            }
            return View(publisher);
        }

        // POST: Admin_quanli/Publishers/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "pub_id,pub_name,Description")] Publisher publisher)
        {
            if (ModelState.IsValid)
            {
                db.Entry(publisher).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(publisher);
        }

        // GET: Admin_quanli/Publishers/Delete/5
        public ActionResult Delete(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Publisher publisher = db.Publishers.Find(id);
            if (publisher == null)
            {
                return HttpNotFound();
            }
            return View(publisher);
        }

        // POST: Admin_quanli/Publishers/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(string id)
        {
            Publisher publisher = db.Publishers.Find(id);
            db.Publishers.Remove(publisher);
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
