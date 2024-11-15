using System.Web.Mvc;

namespace DoAnWebTuyenDung.Areas.Employers
{
    public class EmployersAreaRegistration : AreaRegistration 
    {
        public override string AreaName 
        {
            get 
            {
                return "Employers";
            }
        }

        public override void RegisterArea(AreaRegistrationContext context) 
        {
            context.MapRoute(
                "Employers_default",
                "Employers/{controller}/{action}/{id}",
                new { area = "Employers", action = "Index", id = UrlParameter.Optional },
                  new[] { "DoAnWebTuyenDung.Areas.Employers.Controllers" }

            );
        }
    }
}