using Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Project_Sem3.Controllers
{
    public class CategoryController : Controller
    {
        ProjectDbContext db = new ProjectDbContext();
             
       
        public PartialViewResult ShowCategory()
        {
            var data = db.Categorys.ToList();
            return PartialView(data);
        }

    }
}