using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using System.Data.Entity;
using DoAnWebTuyenDung.Models;

namespace DoAnWebTuyenDung.Controllers
{
    public class JobCategoriesController : Controller
    {
        private DoAnEntities db = new DoAnEntities();

        // GET: JobCategories
        public ActionResult Index()
        {
            // Lấy dữ liệu từ bảng Job_Categories và truyền vào view
            var jobs = db.Jobs.Include(j => j.Company).Include(j => j.Job_Categories).AsNoTracking().ToList();
            return View(jobs.ToList());
        }
        // GET: User/JobCategories/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Job job = db.Jobs.Find(id);
            if (job == null)
            {
                return HttpNotFound();
            }
            return View(job);
        }

    }
}
