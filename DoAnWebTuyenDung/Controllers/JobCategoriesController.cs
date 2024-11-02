using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
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
            var jobCategories = db.Job_Categories.ToList();
            return View(jobCategories);
        }
        // GET: User/JobCategories/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            Job_Categories JobCategories = db.Job_Categories.Find(id);
            if (JobCategories == null)
            {
                return HttpNotFound();
            }

            var jobs = db.Jobs.Where(j => j.category_id == id).ToList(); // Lấy danh sách công việc thuộc danh mục
            ViewBag.Jobs = jobs; // Lưu danh sách công việc vào ViewBag

            return View(JobCategories); // Trả về view chi tiết với thông tin danh mục
        }

    }
}
