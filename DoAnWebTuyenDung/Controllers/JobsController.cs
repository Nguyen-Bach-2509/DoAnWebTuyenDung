using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using DoAnWebTuyenDung.Models;

namespace DoAnWebTuyenDung.Controllers
{
    public class JobsController : Controller
    {
        private DoAnEntities db = new DoAnEntities();

        // GET: UserJobs
        public ActionResult Index()
        {
            // Hiển thị danh sách các công việc
            var jobs = db.Jobs.Include(j => j.Company).Include(j => j.Job_Categories);
            return View(jobs.ToList());
        }

        // GET: UserJobs/Details/5
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

        // GET: UserJobs/Search
        [HttpGet]
        public ActionResult Search(string keyword, string location, int? categoryId)
        {
            // Tìm kiếm công việc theo từ khóa, vị trí và loại công việc
            var jobs = db.Jobs.Include(j => j.Company).Include(j => j.Job_Categories);

            if (!string.IsNullOrEmpty(keyword))
            {
                jobs = jobs.Where(j => j.title.Contains(keyword) || j.description.Contains(keyword));
            }

            if (!string.IsNullOrEmpty(location))
            {
                jobs = jobs.Where(j => j.location.Contains(location));
            }

            if (categoryId.HasValue)
            {
                jobs = jobs.Where(j => j.category_id == categoryId);
            }

            ViewBag.keyword = keyword;
            ViewBag.location = location;
            ViewBag.categoryId = new SelectList(db.Job_Categories, "category_id", "category_name", categoryId);

            return View(jobs.ToList());
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
    
