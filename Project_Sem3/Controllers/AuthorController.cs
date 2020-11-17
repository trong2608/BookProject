using Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Project_Sem3.Controllers
{
    public class AuthorController : Controller
    {
        ProjectDbContext db = new ProjectDbContext();
        // GET: Author
        public ActionResult ShowAuthor()
        {
            var data = db.Authors.ToList();
            return PartialView(data);
        }
    }
}