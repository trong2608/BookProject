using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Model;
using PagedList;

namespace Project_Sem3.Areas.Admin_quanli.Controllers
{
    public class OdersController : Controller
    {
        private ProjectDbContext db = new ProjectDbContext();

        // GET: Admin_quanli/Oders
        public ActionResult Index(int? page, string authorid, string search)
        { 
        int pageSize = 3;
        int pageNumber = page ?? 1;
        var data = db.Oderss.ToList();
            if (!string.IsNullOrEmpty(search))
            {
                data = data.Where(x => x.fullname.ToLower().Contains(search.ToLower())).OrderByDescending(x => x.Oders_id).ToList();
    }
    ViewBag.search = search;
            return View(data.OrderByDescending(x => x.Oders_id).ToPagedList(pageNumber, pageSize));
        }

// GET: Admin_quanli/Oders/Details/5
public ActionResult Details(string id)
{
    if (id == null)
    {
        return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
    }
    Oders oders = db.Oderss.Find(id);
    if (oders == null)
    {
        return HttpNotFound();
    }
    return View(oders);
}

// GET: Admin_quanli/Oders/Create
public ActionResult Create()
{
    ViewBag.cus_id = new SelectList(db.Customers, "cus_id", "cus_name");
    return View();
}

// POST: Admin_quanli/Oders/Create
// To protect from overposting attacks, please enable the specific properties you want to bind to, for 
// more details see http://go.microsoft.com/fwlink/?LinkId=317598.
[HttpPost]
[ValidateAntiForgeryToken]
public ActionResult Create([Bind(Include = "Oders_id,cus_id,total_amount,payment_method,Description,fullname,address,phone,create_date")] Oders oders)
{
    if (ModelState.IsValid)
    {
        db.Oderss.Add(oders);
        db.SaveChanges();
        return RedirectToAction("Index");
    }

    ViewBag.cus_id = new SelectList(db.Customers, "cus_id", "cus_name", oders.cus_id);
    return View(oders);
}

// GET: Admin_quanli/Oders/Edit/5
public ActionResult Edit(string id)
{
    if (id == null)
    {
        return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
    }
    Oders oders = db.Oderss.Find(id);
    if (oders == null)
    {
        return HttpNotFound();
    }
    ViewBag.cus_id = new SelectList(db.Customers, "cus_id", "cus_name", oders.cus_id);
    return View(oders);
}

// POST: Admin_quanli/Oders/Edit/5
// To protect from overposting attacks, please enable the specific properties you want to bind to, for 
// more details see http://go.microsoft.com/fwlink/?LinkId=317598.
[HttpPost]
[ValidateAntiForgeryToken]
public ActionResult Edit([Bind(Include = "Oders_id,cus_id,total_amount,payment_method,Description,fullname,address,phone,create_date")] Oders oders)
{
    if (ModelState.IsValid)
    {
        db.Entry(oders).State = EntityState.Modified;
        db.SaveChanges();
        return RedirectToAction("Index");
    }
    ViewBag.cus_id = new SelectList(db.Customers, "cus_id", "cus_name", oders.cus_id);
    return View(oders);
}

// GET: Admin_quanli/Oders/Delete/5
public ActionResult Delete(string id)
{
    if (id == null)
    {
        return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
    }
    Oders oders = db.Oderss.Find(id);
    if (oders == null)
    {
        return HttpNotFound();
    }
    return View(oders);
}

// POST: Admin_quanli/Oders/Delete/5
[HttpPost, ActionName("Delete")]
[ValidateAntiForgeryToken]
public ActionResult DeleteConfirmed(string id)
{
    Oders oders = db.Oderss.Find(id);
    db.Oderss.Remove(oders);
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
