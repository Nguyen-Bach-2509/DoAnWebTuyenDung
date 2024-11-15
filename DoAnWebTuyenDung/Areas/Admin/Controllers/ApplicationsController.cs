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
    public class ApplicationsController : Controller
    {
        private DoAnEntities db = new DoAnEntities();
        [Authorize(Roles = "Admin")]
        // GET: Admin/Applications
        public ActionResult Index()
        {
            var applications = db.Applications.Include(a => a.Candidate).Include(a => a.Job);
            return View(applications.ToList());
        }

        // GET: Admin/Applications/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Application application = db.Applications.Find(id);
            if (application == null)
            {
                return HttpNotFound();
            }
            return View(application);
        }

        // GET: Admin/Applications/Create
        public ActionResult Create()
        {
            ViewBag.candidate_id = new SelectList(db.Candidates, "candidate_id", "full_name");
            ViewBag.job_id = new SelectList(db.Jobs, "job_id", "title");
            return View();
        }

        // POST: Admin/Applications/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "application_id,job_id,candidate_id,status,applied_at,updated_at,notes")] Application application)
        {
            if (ModelState.IsValid)
            {
                db.Applications.Add(application);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.candidate_id = new SelectList(db.Candidates, "candidate_id", "full_name", application.candidate_id);
            ViewBag.job_id = new SelectList(db.Jobs, "job_id", "title", application.job_id);
            return View(application);
        }

        // GET: Admin/Applications/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Application application = db.Applications.Find(id);
            if (application == null)
            {
                return HttpNotFound();
            }
            ViewBag.candidate_id = new SelectList(db.Candidates, "candidate_id", "full_name", application.candidate_id);
            ViewBag.job_id = new SelectList(db.Jobs, "job_id", "title", application.job_id);
            return View(application);
        }

        // POST: Admin/Applications/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "application_id,job_id,candidate_id,status,applied_at,updated_at,notes")] Application application)
        {
            if (ModelState.IsValid)
            {
                db.Entry(application).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.candidate_id = new SelectList(db.Candidates, "candidate_id", "full_name", application.candidate_id);
            ViewBag.job_id = new SelectList(db.Jobs, "job_id", "title", application.job_id);
            return View(application);
        }

        // GET: Admin/Applications/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Application application = db.Applications.Find(id);
            if (application == null)
            {
                return HttpNotFound();
            }
            return View(application);
        }

        // POST: Admin/Applications/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Application application = db.Applications.Find(id);
            db.Applications.Remove(application);
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
