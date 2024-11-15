using DoAnWebTuyenDung.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace DoAnWebTuyenDung.Controllers
{
    public class ReviewsController : Controller
    {
        private DoAnEntities db = new DoAnEntities();

        // GET: Reviews
        public ActionResult Index()
        {
            // Hiển thị tất cả các đánh giá kèm thông tin công việc và ứng viên
            var reviews = db.Reviews.Include("Candidate").Include("Job").OrderByDescending(r => r.created_at).ToList();
            return View(reviews);
        }

        // GET: UserReviews/Create
        public ActionResult Create()
        {
            ViewBag.job_id = new SelectList(db.Jobs, "job_id", "title"); // Dropdown các công việc
            return View();
        }

        // POST: UserReviews/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "job_id,rating,review_text")] Review review)
        {
            if (Session["CandidateId"] == null)
            {
                TempData["ErrorMessage"] = "Bạn cần đăng nhập để thực hiện đánh giá.";
                return RedirectToAction("Login", "Account"); // Redirect đến trang đăng nhập
            }

            if (ModelState.IsValid)
            {
                // Lấy candidate_id từ session
                review.candidate_id = Convert.ToInt32(Session["CandidateId"]);
                review.created_at = DateTime.Now;

                db.Reviews.Add(review);
                db.SaveChanges();

                TempData["SuccessMessage"] = "Đánh giá của bạn đã được gửi thành công!";
                return RedirectToAction("Index");
            }

            ViewBag.job_id = new SelectList(db.Jobs, "job_id", "title", review.job_id);
            TempData["ErrorMessage"] = "Có lỗi xảy ra khi gửi đánh giá.";
            return View(review);
        }

    }
}
