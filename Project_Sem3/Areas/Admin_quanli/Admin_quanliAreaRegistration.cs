using System.Web.Mvc;

namespace Project_Sem3.Areas.Admin_quanli
{
    public class Admin_quanliAreaRegistration : AreaRegistration 
    {
        public override string AreaName 
        {
            get 
            {
                return "Admin_quanli";
            }
        }

        public override void RegisterArea(AreaRegistrationContext context) 
        {
            context.MapRoute(
                "Admin_quanli_default",
                "Admin_quanli/{controller}/{action}/{id}",
                new { action = "Index", id = UrlParameter.Optional },
                new string[] { "Project_Sem3.Areas.Admin_quanli.Controllers" }
            );
        }
    }
}