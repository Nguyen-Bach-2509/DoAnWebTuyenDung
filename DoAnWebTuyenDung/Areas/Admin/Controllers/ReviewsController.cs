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
    public class ReviewsController : Controller
    {
        private DoAnEntities db = new DoAnEntities();


        // GET: Admin/Reviews
        public ActionResult Index()
        {
            var reviews = db.Reviews.Include(r => r.Candidate).Include(r => r.Job);
            return View(reviews.ToList());
        }

        // GET: Admin/Reviews/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Review review = db.Reviews.Find(id);
            if (review == null)
            {
                return HttpNotFound();
            }
            return View(review);
        }

        // GET: Admin/Reviews/Create
        public ActionResult Create()
        {
            ViewBag.candidate_id = new SelectList(db.Candidates, "candidate_id", "full_name");
            ViewBag.job_id = new SelectList(db.Jobs, "job_id", "title");
            return View();
        }

        // POST: Admin/Reviews/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "job_id, candidate_id, rating, review_text")] Review review)
        {
            if (ModelState.IsValid)
            {
                review.created_at = DateTime.Now; // Gán thời gian tạo đánh giá
                db.Reviews.Add(review); // Thêm vào cơ sở dữ liệu
                db.SaveChanges(); // Lưu thay đổi
                TempData["SuccessMessage"] = "Đánh giá của bạn đã được gửi thành công!";
                return RedirectToAction("AllReviews");
            }

            TempData["ErrorMessage"] = "Có lỗi xảy ra khi gửi đánh giá.";
            return RedirectToAction("AllReviews");
        }

        // GET: Admin/Reviews/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Review review = db.Reviews.Find(id);
            if (review == null)
            {
                return HttpNotFound();
            }
            ViewBag.candidate_id = new SelectList(db.Candidates, "candidate_id", "full_name", review.candidate_id);
            ViewBag.job_id = new SelectList(db.Jobs, "job_id", "title", review.job_id);
            return View(review);
        }

        // POST: Admin/Reviews/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "review_id,job_id,candidate_id,rating,review_text,created_at")] Review review)
        {
            if (ModelState.IsValid)
            {
                db.Entry(review).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.candidate_id = new SelectList(db.Candidates, "candidate_id", "full_name", review.candidate_id);
            ViewBag.job_id = new SelectList(db.Jobs, "job_id", "title", review.job_id);
            return View(review);
        }

        // GET: Admin/Reviews/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Review review = db.Reviews.Find(id);
            if (review == null)
            {
                return HttpNotFound();
            }
            return View(review);
        }

        // POST: Admin/Reviews/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Review review = db.Reviews.Find(id);
            db.Reviews.Remove(review);
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
        // GET: Reviews/AllReviews
        public ActionResult AllReviews()
        {
            // Lấy tất cả các đánh giá cùng thông tin ứng viên và công việc
            var reviews = db.Reviews.Include("Candidate").Include("Job").OrderByDescending(r => r.created_at).ToList();
            return View(reviews);
        }
    }
}
