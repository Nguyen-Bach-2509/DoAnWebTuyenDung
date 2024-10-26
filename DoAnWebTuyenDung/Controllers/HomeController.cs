using DoAnWebTuyenDung.Data;
using DoAnWebTuyenDung.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace DoAnWebTuyenDung.Controllers
{
    public class HomeController : Controller
    {
        private DoAnWebEntities4 db = new DoAnWebEntities4();
        public ActionResult Index()
        {
            using (var context = new ApplicationDbContext())
            {
                // 
            }
                return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";
            
            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
        public ActionResult TrangChu()
        {
            return View();
        }
        public ActionResult TrangChu2()
        {
            return View();
        }
    }
}