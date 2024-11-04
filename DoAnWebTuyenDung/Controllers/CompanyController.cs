using DoAnWebTuyenDung.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data;
using System.Data.Entity;
using System.Net;
using DoAnWebTuyenDung.Models;

namespace DoAnWebTuyenDung.Controllers
{
    public class CompanyController : Controller
    {
        private DoAnEntities db = new DoAnEntities();

        // GET: Companies
        public ActionResult Index()
        {
            // Lấy tất cả danh sách công ty
            var companies = db.Companies.ToList(); 
            return View(companies);
        }

        // GET: Companies/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Company company = db.Companies.Find(id);
            if (company == null)
            {
                return HttpNotFound();
            }
            return View(company);
        }
    }
}
