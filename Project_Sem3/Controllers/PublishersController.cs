using Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Project_Sem3.Controllers
{
    public class PublishersController : Controller
    {
        ProjectDbContext db = new ProjectDbContext();
        // GET: Publishers
        public ActionResult ShowPublishers()
        {
            var data = db.Publishers.ToList();
            return PartialView(data);
        }
    }
}