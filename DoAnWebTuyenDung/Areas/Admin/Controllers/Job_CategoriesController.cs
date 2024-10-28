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
    public class Job_CategoriesController : Controller
    {
        private DoAnEntities db = new DoAnEntities();

        // GET: Admin/Job_Categories
        public ActionResult Index()
        {
            return View(db.Job_Categories.ToList());
        }

        // GET: Admin/Job_Categories/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Job_Categories job_Categories = db.Job_Categories.Find(id);
            if (job_Categories == null)
            {
                return HttpNotFound();
            }
            return View(job_Categories);
        }

        // GET: Admin/Job_Categories/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Admin/Job_Categories/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "category_id,category_name,description")] Job_Categories job_Categories)
        {
            if (ModelState.IsValid)
            {
                db.Job_Categories.Add(job_Categories);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(job_Categories);
        }

        // GET: Admin/Job_Categories/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Job_Categories job_Categories = db.Job_Categories.Find(id);
            if (job_Categories == null)
            {
                return HttpNotFound();
            }
            return View(job_Categories);
        }

        // POST: Admin/Job_Categories/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "category_id,category_name,description")] Job_Categories job_Categories)
        {
            if (ModelState.IsValid)
            {
                db.Entry(job_Categories).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(job_Categories);
        }

        // GET: Admin/Job_Categories/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Job_Categories job_Categories = db.Job_Categories.Find(id);
            if (job_Categories == null)
            {
                return HttpNotFound();
            }
            return View(job_Categories);
        }

        // POST: Admin/Job_Categories/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Job_Categories job_Categories = db.Job_Categories.Find(id);
            db.Job_Categories.Remove(job_Categories);
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
