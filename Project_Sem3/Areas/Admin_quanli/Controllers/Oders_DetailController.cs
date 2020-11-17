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
    public class Oders_DetailController : Controller
    {
        private ProjectDbContext db = new ProjectDbContext();

        // GET: Admin_quanli/Oders_Detail
        public ActionResult Index(int? page,  string search)
        {
            int pageSize = 3;
            int pageNumber = page ?? 1;
            var data = db.Oders_Details.ToList();
            if (!string.IsNullOrEmpty(search))
            {
                data = data.Where(x => x.Oders_id.ToLower().Contains(search.ToLower())).OrderByDescending(x => x.Order_Detail).ToList();
            }
            ViewBag.search = search;
            return View(data.OrderByDescending(x => x.Order_Detail).ToPagedList(pageNumber, pageSize));
        }

        // GET: Admin_quanli/Oders_Detail/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Oders_Detail oders_Detail = db.Oders_Details.Find(id);
            if (oders_Detail == null)
            {
                return HttpNotFound();
            }
            return View(oders_Detail);
        }

        // GET: Admin_quanli/Oders_Detail/Create
        public ActionResult Create()
        {
            ViewBag.Oders_id = new SelectList(db.Oderss, "Oders_id", "cus_id");
            ViewBag.pro_id = new SelectList(db.Products, "pro_id", "pro_name");
            return View();
        }

        // POST: Admin_quanli/Oders_Detail/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Order_Detail,Oders_id,pro_id,quantity,detail_price,detail_sale_price,total_price")] Oders_Detail oders_Detail)
        {
            if (ModelState.IsValid)
            {
                db.Oders_Details.Add(oders_Detail);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.Oders_id = new SelectList(db.Oderss, "Oders_id", "cus_id", oders_Detail.Oders_id);
            ViewBag.pro_id = new SelectList(db.Products, "pro_id", "pro_name", oders_Detail.pro_id);
            return View(oders_Detail);
        }

        // GET: Admin_quanli/Oders_Detail/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Oders_Detail oders_Detail = db.Oders_Details.Find(id);
            if (oders_Detail == null)
            {
                return HttpNotFound();
            }
            ViewBag.Oders_id = new SelectList(db.Oderss, "Oders_id", "cus_id", oders_Detail.Oders_id);
            ViewBag.pro_id = new SelectList(db.Products, "pro_id", "pro_name", oders_Detail.pro_id);
            return View(oders_Detail);
        }

        // POST: Admin_quanli/Oders_Detail/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Order_Detail,Oders_id,pro_id,quantity,detail_price,detail_sale_price,total_price")] Oders_Detail oders_Detail)
        {
            if (ModelState.IsValid)
            {
                db.Entry(oders_Detail).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.Oders_id = new SelectList(db.Oderss, "Oders_id", "cus_id", oders_Detail.Oders_id);
            ViewBag.pro_id = new SelectList(db.Products, "pro_id", "pro_name", oders_Detail.pro_id);
            return View(oders_Detail);
        }

        // GET: Admin_quanli/Oders_Detail/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Oders_Detail oders_Detail = db.Oders_Details.Find(id);
            if (oders_Detail == null)
            {
                return HttpNotFound();
            }
            return View(oders_Detail);
        }

        // POST: Admin_quanli/Oders_Detail/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Oders_Detail oders_Detail = db.Oders_Details.Find(id);
            db.Oders_Details.Remove(oders_Detail);
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
