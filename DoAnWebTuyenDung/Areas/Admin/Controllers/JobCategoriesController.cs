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
    public class JobCategoriesController : Controller
    {
        private DoAnWebEntities2 db = new DoAnWebEntities2();

        // GET: Admin/JobCategories
        public ActionResult Index(string search)
        {
            var jobCategories = db.JobCategories.Include(j => j.Jobs).AsQueryable();

            if (!string.IsNullOrEmpty(search))
            {
                jobCategories = jobCategories.Where(c => c.CategoryName.Contains(search));
            }

            return View(jobCategories.ToList());
        }

        // GET: Admin/JobCategories/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            JobCategory jobCategory = db.JobCategories.Find(id);
            if (jobCategory == null)
            {
                return HttpNotFound();
            }
            return View(jobCategory);
        }

        // GET: Admin/JobCategories/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Admin/JobCategories/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "CategoryID,CategoryName")] JobCategory jobCategory)
        {
            if (ModelState.IsValid)
            {
                db.JobCategories.Add(jobCategory);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(jobCategory);
        }

        // GET: Admin/JobCategories/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            JobCategory jobCategory = db.JobCategories.Find(id);
            if (jobCategory == null)
            {
                return HttpNotFound();
            }
            return View(jobCategory);
        }

        // POST: Admin/JobCategories/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "CategoryID,CategoryName")] JobCategory jobCategory)
        {
            if (ModelState.IsValid)
            {
                db.Entry(jobCategory).State = EntityState.Modified;
                db.SaveChanges();
                TempData["SuccessMessage"] = "Danh mục đã được cập nhật thành công!";
                return RedirectToAction("Index");
            }
            return View(jobCategory);
        }

        // GET: Admin/JobCategories/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            JobCategory jobCategory = db.JobCategories.Find(id);
            if (jobCategory == null)
            {
                return HttpNotFound();
            }
            return View(jobCategory);
        }

        // POST: Admin/JobCategories/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            JobCategory jobCategory = db.JobCategories.Find(id);
            db.JobCategories.Remove(jobCategory);
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
