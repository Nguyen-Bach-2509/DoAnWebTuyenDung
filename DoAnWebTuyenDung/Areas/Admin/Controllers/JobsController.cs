using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using DoAnWebTuyenDung.Models;

namespace DoAnWebTuyenDung.Areas.Admin.Controllers
{
    public class JobsController : Controller
    {
        private DoAnEntities db = new DoAnEntities();

        // GET: Admin/Jobs
        public ActionResult Index()
        {
            var jobs = db.Jobs.Include(j => j.Company).Include(j => j.Job_Categories);
            return View(jobs.ToList());
        }

        // GET: Admin/Jobs/Details/5
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

        // GET: Admin/Jobs/Create
        public ActionResult Create()
        {
            ViewBag.company_id = new SelectList(db.Companies, "company_id", "company_name");
            ViewBag.category_id = new SelectList(db.Job_Categories, "category_id", "category_name");
            return View();
        }

        // POST: Admin/Jobs/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "job_id,company_id,category_id,title,description,requirements,location,salary,employment_type,posted_at,deadline,is_featured,views,application_count")] Job job)
        {
            if (ModelState.IsValid)
            {
                db.Jobs.Add(job);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.company_id = new SelectList(db.Companies, "company_id", "company_name", job.company_id);
            ViewBag.category_id = new SelectList(db.Job_Categories, "category_id", "category_name", job.category_id);
            return View(job);
        }

        // GET: Admin/Jobs/Edit/5
        public ActionResult Edit(int? id)
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
            ViewBag.company_id = new SelectList(db.Companies, "company_id", "company_name", job.company_id);
            ViewBag.category_id = new SelectList(db.Job_Categories, "category_id", "category_name", job.category_id);
            return View(job);
        }

        // POST: Admin/Jobs/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "job_id,company_id,category_id,title,description,requirements,location,salary,employment_type,posted_at,deadline,is_featured,views,application_count")] Job job)
        {
            if (ModelState.IsValid)
            {
                db.Entry(job).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.company_id = new SelectList(db.Companies, "company_id", "company_name", job.company_id);
            ViewBag.category_id = new SelectList(db.Job_Categories, "category_id", "category_name", job.category_id);
            return View(job);
        }

        // GET: Admin/Jobs/Delete/5
        public ActionResult Delete(int? id)
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

        // POST: Admin/Jobs/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Job job = db.Jobs.Find(id);
            db.Jobs.Remove(job);
            db.SaveChanges();
            return RedirectToAction("Index");
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
