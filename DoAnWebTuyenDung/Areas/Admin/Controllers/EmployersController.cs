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
    public class EmployersController : Controller
    {
        private DoAnEntities db = new DoAnEntities();

        // GET: Admin/Employers
        public ActionResult Index()
        {
            var employers = db.Employers.Include(e => e.Company).Include(e => e.User);
            return View(employers.ToList());
        }

        // GET: Admin/Employers/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Employer employer = db.Employers.Find(id);
            if (employer == null)
            {
                return HttpNotFound();
            }
            return View(employer);
        }

        // GET: Admin/Employers/Create
        public ActionResult Create()
        {
            ViewBag.company_id = new SelectList(db.Companies, "company_id", "company_name");
            ViewBag.user_id = new SelectList(db.Users, "user_id", "username");
            return View();
        }

        // POST: Admin/Employers/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "employer_id,user_id,company_id,role")] Employer employer)
        {
            if (ModelState.IsValid)
            {
                db.Employers.Add(employer);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.company_id = new SelectList(db.Companies, "company_id", "company_name", employer.company_id);
            ViewBag.user_id = new SelectList(db.Users, "user_id", "username", employer.user_id);
            return View(employer);
        }

        // GET: Admin/Employers/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Employer employer = db.Employers.Find(id);
            if (employer == null)
            {
                return HttpNotFound();
            }
            ViewBag.company_id = new SelectList(db.Companies, "company_id", "company_name", employer.company_id);
            ViewBag.user_id = new SelectList(db.Users, "user_id", "username", employer.user_id);
            return View(employer);
        }

        // POST: Admin/Employers/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "employer_id,user_id,company_id,role")] Employer employer)
        {
            if (ModelState.IsValid)
            {
                db.Entry(employer).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.company_id = new SelectList(db.Companies, "company_id", "company_name", employer.company_id);
            ViewBag.user_id = new SelectList(db.Users, "user_id", "username", employer.user_id);
            return View(employer);
        }

        // GET: Admin/Employers/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Employer employer = db.Employers.Find(id);
            if (employer == null)
            {
                return HttpNotFound();
            }
            return View(employer);
        }

        // POST: Admin/Employers/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Employer employer = db.Employers.Find(id);
            db.Employers.Remove(employer);
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
