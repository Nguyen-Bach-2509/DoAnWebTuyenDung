using DoAnWebTuyenDung.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data;
using System.Data.Entity;
using System.Net;
using DoAnWebTuyenDung.Models;

namespace DoAnWebTuyenDung.Controllers
{
    public class HomeController : Controller
    {
        private DoAnEntities db = new DoAnEntities();

        public ActionResult Index()
        {
            using (var context = new ApplicationDbContext())
            {
                // 
            }
                return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";
            
            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
        public ActionResult JobApply()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
        public ActionResult TrangChu()
        {
            var reviews = db.Reviews.Include(r => r.Candidate).OrderByDescending(r => r.created_at).Take(6).ToList();
            ViewBag.Jobs = db.Jobs.ToList();
            return View(reviews);
        }
        // GET: Home/AddReview
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult AddReview(int job_id, string candidate_name, string review_text, int rating)
        {
            if (ModelState.IsValid)
            {
                var candidate = db.Candidates.FirstOrDefault(c => c.full_name == candidate_name);

                // Nếu ứng viên không tồn tại, tạo ứng viên mới (tuỳ theo logic bạn muốn)
                if (candidate == null)
                {
                    candidate = new Candidate
                    {
                        full_name = candidate_name
                    };
                    db.Candidates.Add(candidate);
                    db.SaveChanges();
                }

                // Tạo review mới
                var review = new Review
                {
                    job_id = job_id,
                    candidate_id = candidate.candidate_id,
                    review_text = review_text,
                    rating = rating,
                    created_at = DateTime.Now
                };

                db.Reviews.Add(review);
                db.SaveChanges();

                TempData["SuccessMessage"] = "Đánh giá của bạn đã được gửi thành công!";
                return RedirectToAction("TrangChu");
            }

            TempData["ErrorMessage"] = "Có lỗi xảy ra khi gửi đánh giá.";
            return RedirectToAction("TrangChu");
        }

    }
}