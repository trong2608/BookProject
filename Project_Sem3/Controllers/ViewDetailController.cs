using Model;
using Model.DataModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Project_Sem3.Controllers
{
    public class ViewDetailController : Controller
    {
        ProjectDbContext db = new ProjectDbContext();
        // GET: ViewDetail
        public ActionResult Index(string id)
        {
            if (id == null)
            {
                return HttpNotFound();

            }
            Product product = db.Products.Find(id);
            if (product==null)
            {
                return HttpNotFound();
            }
            return View(product);
        }
    }
}