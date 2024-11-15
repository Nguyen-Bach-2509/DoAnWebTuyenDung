using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web.Mvc;
using DoAnWebTuyenDung.Models;

namespace DoAnWebTuyenDung.Areas.Employers.Controllers
{
    public class JobsController : Controller
    {
        private DoAnEntities db = new DoAnEntities();

        // GET: Employers/Jobs
        public ActionResult Index()
        {
            // Lấy user_id của employer từ Session
            if (Session["UserId"] == null)
                return RedirectToAction("Login", "Account", new { area = "" });

            int employerId = (int)Session["UserId"];
            var jobs = db.Jobs.Where(j => j.Company.Employers.Any(e => e.user_id == employerId))
                              .Include(j => j.Job_Categories);
            return View(jobs.ToList());
        }


        // GET: Employers/Jobs/Create
        // GET: Employers/Jobs/Create
        public ActionResult Create()
        {

            if (Session["UserId"] == null)
                return RedirectToAction("Login", "Account", new { area = "" });

            int employerId = (int)Session["UserId"];
            var company = db.Employers.FirstOrDefault(e => e.user_id == employerId)?.Company;

            // Nếu không tìm thấy công ty, thông báo lỗi
            if (company == null)
            {
                TempData["ErrorMessage"] = "Không tìm thấy công ty liên kết với tài khoản của bạn.";
                return RedirectToAction("Index");
            }

            ViewBag.category_id = new SelectList(db.Job_Categories, "category_id", "category_name");
            return View();
        }

        // POST: Employers/Jobs/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "title,description,requirements,location,salary,employment_type,deadline,category_id")] Job job)
        {
            if (Session["UserId"] == null)
                return RedirectToAction("Login", "Account", new { area = "" });

            int employerId = (int)Session["UserId"];
            var company = db.Employers.FirstOrDefault(e => e.user_id == employerId)?.Company;

            if (company == null)
            {
                TempData["ErrorMessage"] = "Không tìm thấy công ty liên kết với tài khoản của bạn.";
                ViewBag.category_id = new SelectList(db.Job_Categories, "category_id", "category_name", job.category_id);
                return View(job);
            }

            if (ModelState.IsValid)
            {
                job.company_id = company.company_id; // Gán công ty hiện tại
                job.posted_at = DateTime.Now;
                db.Jobs.Add(job);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.category_id = new SelectList(db.Job_Categories, "category_id", "category_name", job.category_id);
            return View(job);
        }


        // GET: Employers/Jobs/Edit/5
        public ActionResult Edit(int? id)
        {
            if (Session["UserId"] == null)
                return RedirectToAction("Login", "Account", new { area = "" });

            if (id == null)
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);

            // Find the job by ID
            Job job = db.Jobs.Find(id);
            if (job == null)
                return HttpNotFound();

            // Ensure the job belongs to the current employer
            int employerId = (int)Session["UserId"];
            if (!job.Company.Employers.Any(e => e.user_id == employerId))
            {
                TempData["ErrorMessage"] = "Bạn không có quyền chỉnh sửa công việc này.";
                return RedirectToAction("Index");
            }

            // Populate the dropdown for categories
            ViewBag.category_id = new SelectList(db.Job_Categories, "category_id", "category_name", job.category_id);

            return View(job);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "job_id,title,description,requirements,location,salary,employment_type,deadline,category_id")] Job job)
        {
            if (Session["UserId"] == null)
                return RedirectToAction("Login", "Account", new { area = "" });

            if (!ModelState.IsValid)
            {
                // Repopulate dropdown in case of validation errors
                ViewBag.category_id = new SelectList(db.Job_Categories, "category_id", "category_name", job.category_id);
                return View(job);
            }

            int employerId = (int)Session["UserId"];
            var existingJob = db.Jobs.Include(j => j.Company.Employers)
                                     .FirstOrDefault(j => j.job_id == job.job_id);

            if (existingJob == null || !existingJob.Company.Employers.Any(e => e.user_id == employerId))
            {
                TempData["ErrorMessage"] = "Bạn không có quyền chỉnh sửa công việc này.";
                return RedirectToAction("Index");
            }

            // Update job details
            existingJob.title = job.title;
            existingJob.description = job.description;
            existingJob.requirements = job.requirements;
            existingJob.location = job.location;
            existingJob.salary = job.salary;
            existingJob.employment_type = job.employment_type;
            existingJob.deadline = job.deadline;
            existingJob.category_id = job.category_id;

            db.Entry(existingJob).State = EntityState.Modified;
            db.SaveChanges();

            TempData["SuccessMessage"] = "Công việc đã được chỉnh sửa thành công.";
            return RedirectToAction("Index");
        }



        // GET: Employers/Jobs/Delete/5
        public ActionResult Delete(int? id)
        {
            if (Session["UserId"] == null)
                return RedirectToAction("Login", "Account", new { area = "" });

            if (id == null)
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);

            Job job = db.Jobs.Find(id);
            if (job == null)
                return HttpNotFound();

            // Đảm bảo job thuộc về công ty của employer
            int employerId = (int)Session["UserId"];
            if (!job.Company.Employers.Any(e => e.user_id == employerId))
                return new HttpStatusCodeResult(HttpStatusCode.Forbidden);

            return View(job);
        }

        // POST: Employers/Jobs/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            if (Session["UserId"] == null)
                return RedirectToAction("Login", "Account", new { area = "" });

            Job job = db.Jobs.Find(id);
            if (job == null)
                return HttpNotFound();

            // Đảm bảo job thuộc về công ty của employer
            int employerId = (int)Session["UserId"];
            if (!job.Company.Employers.Any(e => e.user_id == employerId))
                return new HttpStatusCodeResult(HttpStatusCode.Forbidden);

            db.Jobs.Remove(job);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
                db.Dispose();
            base.Dispose(disposing);
        }


    }
}
