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
    public class ProductsController : Controller
    {
        private ProjectDbContext db = new ProjectDbContext();

        // GET: Admin_quanli/Products
        public ActionResult Index(int? page, string search, string author_id)
        {
           

            ViewBag.author_id = new SelectList(db.Authors, "author_id", "author_name");
            //ViewBag.cat_id = new SelectList(db.Categorys, "cat_id", "cat_name");
            //ViewBag.pub_id = new SelectList(db.Publishers, "pub_id", "pub_name");

            int pageSize = 3;
            int pageNumber = page ?? 1;
            var data = db.Products.ToList();

            if (!string.IsNullOrEmpty(author_id))
            {
                data = data.Where(x => x.author_id.Equals(author_id)).ToList();
                
            }
            if (!string.IsNullOrEmpty(search))
            {
                data = data.Where(x => x.pro_name.ToLower().Contains(search.ToLower())).ToList();
            }
            
            ViewBag.Search = search;
            ViewBag.au = author_id;
            
            return View(data.OrderByDescending(x => x.pro_id).ToPagedList(pageNumber, pageSize));
        }

        // GET: Admin_quanli/Products/Details/5
        public ActionResult Details(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Product product = db.Products.Find(id);
            if (product == null)
            {
                return HttpNotFound();
            }
            return View(product);
        }

        // GET: Admin_quanli/Products/Create
        public ActionResult Create()
        {
            ViewBag.author_id = new SelectList(db.Authors, "author_id", "author_name");
            ViewBag.cat_id = new SelectList(db.Categorys, "cat_id", "cat_name");
            ViewBag.pub_id = new SelectList(db.Publishers, "pub_id", "pub_name");
            return View();
        }

        // POST: Admin_quanli/Products/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Product product, HttpPostedFileBase fileUpload)
        {
            if (ModelState.IsValid)
            {
                if (fileUpload != null)
                {
                    fileUpload.SaveAs(Server.MapPath("~/Areas/Admin_quanli/Assest/anhtacgia/") + fileUpload.FileName);
                    product.pro_image = "/Areas/Admin_quanli/Assest/anhtacgia/" + fileUpload.FileName;
                }
                var pro = db.Products.Where(x => x.pro_id.Equals(product.cat_id)).ToList();
                if (pro.Count > 0)
                {
                    ViewBag.msg = "\" " + product.pro_id + "\" đã tồn tại ";
                    return View();
                }
                db.Products.Add(product);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.author_id = new SelectList(db.Authors, "author_id", "author_name", product.author_id);
            ViewBag.cat_id = new SelectList(db.Categorys, "cat_id", "cat_name", product.cat_id);
            ViewBag.pub_id = new SelectList(db.Publishers, "pub_id", "pub_name", product.pub_id);
            return View(product);
        }

        // GET: Admin_quanli/Products/Edit/5
        public ActionResult Edit(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Product product = db.Products.Find(id);
            if (product == null)
            {
                return HttpNotFound();
            }
            ViewBag.author_id = new SelectList(db.Authors, "author_id", "author_name", product.author_id);
            ViewBag.cat_id = new SelectList(db.Categorys, "cat_id", "cat_name", product.cat_id);
            ViewBag.pub_id = new SelectList(db.Publishers, "pub_id", "pub_name", product.pub_id);
            return View(product);
        }

        // POST: Admin_quanli/Products/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Product product, HttpPostedFileBase fileUpload)
        {
            if (ModelState.IsValid)
            {
                if (fileUpload != null)
                {
                    fileUpload.SaveAs(Server.MapPath("~/Areas/Admin_quanli/Assest/anhtacgia/") + fileUpload.FileName);
                    product.pro_image = "/Areas/Admin_quanli/Assest/anhtacgia/" + fileUpload.FileName;
                }
                db.Entry(product).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.author_id = new SelectList(db.Authors, "author_id", "author_name", product.author_id);
            ViewBag.cat_id = new SelectList(db.Categorys, "cat_id", "cat_name", product.cat_id);
            ViewBag.pub_id = new SelectList(db.Publishers, "pub_id", "pub_name", product.pub_id);
            return View(product);
        }

        // GET: Admin_quanli/Products/Delete/5
        public ActionResult Delete(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Product product = db.Products.Find(id);
            if (product == null)
            {
                return HttpNotFound();
            }
            return View(product);
        }

        // POST: Admin_quanli/Products/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(string id)
        {
            Product product = db.Products.Find(id);
            db.Products.Remove(product);
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
