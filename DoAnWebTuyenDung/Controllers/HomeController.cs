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
        public ActionResult TrangChu()
        {
            
            return View();
        }
        // GET: Home/AddReview
        public ActionResult AddReview()
        {
            ViewBag.candidate_id = new SelectList(db.Candidates, "candidate_id", "full_name");
            ViewBag.job_id = new SelectList(db.Jobs, "job_id", "title");
            return View();
        }

        // POST: Home/AddReview
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult AddReview([Bind(Include = "job_id,candidate_id,rating,review_text")] Review review)
        {
            if (ModelState.IsValid)
            {
                review.created_at = DateTime.Now; // Gán thời gian tạo cho đánh giá
                db.Reviews.Add(review);
                db.SaveChanges();
                TempData["SuccessMessage"] = "Đánh giá đã được thêm thành công!";
                return RedirectToAction("TrangChu");
            }

            ViewBag.candidate_id = new SelectList(db.Candidates, "candidate_id", "full_name", review.candidate_id);
            ViewBag.job_id = new SelectList(db.Jobs, "job_id", "title", review.job_id);
            return View(review);
        }
    }
}