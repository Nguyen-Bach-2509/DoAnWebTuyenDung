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
    public class JobCategoryMappingsController : Controller
    {
        private DoAnWebEntities3 db = new DoAnWebEntities3();

        // GET: Admin/JobCategoryMappings
        public ActionResult Index()
        {
            return View(db.JobCategoryMappings.ToList());
        }

        // GET: Admin/JobCategoryMappings/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            JobCategoryMapping jobCategoryMapping = db.JobCategoryMappings.Find(id);
            if (jobCategoryMapping == null)
            {
                return HttpNotFound();
            }
            return View(jobCategoryMapping);
        }

        // GET: Admin/JobCategoryMappings/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Admin/JobCategoryMappings/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "JobID,CategoryID")] JobCategoryMapping jobCategoryMapping)
        {
            if (ModelState.IsValid)
            {
                db.JobCategoryMappings.Add(jobCategoryMapping);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(jobCategoryMapping);
        }

        // GET: Admin/JobCategoryMappings/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            JobCategoryMapping jobCategoryMapping = db.JobCategoryMappings.Find(id);
            if (jobCategoryMapping == null)
            {
                return HttpNotFound();
            }
            return View(jobCategoryMapping);
        }

        // POST: Admin/JobCategoryMappings/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "JobID,CategoryID")] JobCategoryMapping jobCategoryMapping)
        {
            if (ModelState.IsValid)
            {
                db.Entry(jobCategoryMapping).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(jobCategoryMapping);
        }

        // GET: Admin/JobCategoryMappings/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            JobCategoryMapping jobCategoryMapping = db.JobCategoryMappings.Find(id);
            if (jobCategoryMapping == null)
            {
                return HttpNotFound();
            }
            return View(jobCategoryMapping);
        }

        // POST: Admin/JobCategoryMappings/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            JobCategoryMapping jobCategoryMapping = db.JobCategoryMappings.Find(id);
            db.JobCategoryMappings.Remove(jobCategoryMapping);
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
