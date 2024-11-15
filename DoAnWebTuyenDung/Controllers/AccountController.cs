using System;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using DoAnWebTuyenDung.Models;

namespace DoAnWebTuyenDung.Controllers
{
    public class AccountController : Controller
    {
        private DoAnEntities db = new DoAnEntities();

        // GET: Account/Register
        public ActionResult Register()
        {
            ViewBag.Roles = new SelectList(new[] { "CANDIDATE", "EMPLOYER" });
            return View();
        }

        // POST: Account/Register
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Register([Bind(Include = "username,password,email,role")] User user, string confirmPassword, string fullName, string phone, string companyName, string companyIndustry, string companyDescription, string companyLocation, string companyLogo)
        {
            if (ModelState.IsValid)
            {
                if (db.Users.Any(u => u.username == user.username))
                {
                    ModelState.AddModelError("username", "Tên người dùng đã tồn tại. Vui lòng chọn tên khác.");
                    ViewBag.Roles = new SelectList(new[] { "CANDIDATE", "EMPLOYER" });
                    return View(user);
                }

                if (db.Users.Any(u => u.email == user.email))
                {
                    ModelState.AddModelError("email", "Email đã được sử dụng. Vui lòng chọn email khác.");
                    ViewBag.Roles = new SelectList(new[] { "CANDIDATE", "EMPLOYER" });
                    return View(user);
                }

                if (user.password != confirmPassword)
                {
                    ModelState.AddModelError("confirmPassword", "Mật khẩu và xác nhận mật khẩu không khớp.");
                    ViewBag.Roles = new SelectList(new[] { "CANDIDATE", "EMPLOYER" });
                    return View(user);
                }

                user.created_at = DateTime.Now;
                user.updated_at = DateTime.Now;
                db.Users.Add(user);
                db.SaveChanges();

                if (user.role == "CANDIDATE")
                {
                    var candidate = new Candidate
                    {
                        user_id = user.user_id,
                        full_name = fullName,
                        phone = phone,
                        email = user.email
                    };
                    db.Candidates.Add(candidate);
                    db.SaveChanges();
                }

                if (user.role == "EMPLOYER")
                {
                    var company = new Company
                    {
                        company_name = companyName,
                        industry = companyIndustry,
                        description = companyDescription,
                        location = companyLocation,
                        company_logo = companyLogo
                    };
                    db.Companies.Add(company);
                    db.SaveChanges();

                    var employer = new Employer
                    {
                        user_id = user.user_id,
                        company_id = company.company_id,
                        role = "EMPLOYER"
                    };
                    db.Employers.Add(employer);
                    db.SaveChanges();
                }

                TempData["SuccessMessage"] = "Đăng ký tài khoản thành công! Bạn có thể đăng nhập ngay bây giờ.";
                return RedirectToAction("Login", "Account");
            }

            ViewBag.Roles = new SelectList(new[] { "CANDIDATE", "EMPLOYER" });
            return View(user);
        }

        // GET: Account/Login
        public ActionResult Login()
        {
            return View();
        }

        // POST: Account/Login
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Login(string username, string password)
        {
            var user = db.Users.FirstOrDefault(u => u.username == username && u.password == password);

            if (user != null)
            {
                Session["UserId"] = user.user_id;
                Session["Username"] = user.username;
                Session["Role"] = user.role;

                if (user.role == "EMPLOYER")
                {
                    return RedirectToAction("Index", "Home", new { area = "Employers" });
                }
                else if (user.role == "ADMIN")
                {
                    return RedirectToAction("Index", "Home", new { area = "Admin" });
                }
                else if (user.role == "CANDIDATE")
                {
                    return RedirectToAction("TrangChu", "Home");
                }
                return RedirectToAction("TrangChu", "Home");
            }

            ViewBag.ErrorMessage = "Tên người dùng hoặc mật khẩu không đúng.";
            return View();
        }

        // GET: Account/Logout
        public ActionResult Logout()
        {
            Session.Clear();
            return RedirectToAction("TrangChu", "Home");
        }

        // GET: Account/ChangePassword
        public ActionResult ChangePassword()
        {
            if (Session["UserId"] == null)
            {
                return RedirectToAction("Login", "Account");
            }
            return View();
        }

        // POST: Account/ChangePassword
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult ChangePassword(string currentPassword, string newPassword, string confirmPassword)
        {
            if (Session["UserId"] == null)
            {
                return RedirectToAction("Login", "Account");
            }

            int userId = (int)Session["UserId"];
            var user = db.Users.Find(userId);

            if (user != null)
            {
                if (user.password != currentPassword)
                {
                    ViewBag.Error = "Mật khẩu hiện tại không đúng.";
                    return View();
                }

                if (newPassword != confirmPassword)
                {
                    ViewBag.Error = "Mật khẩu mới và xác nhận mật khẩu không khớp.";
                    return View();
                }

                user.password = newPassword;
                user.updated_at = DateTime.Now;
                db.SaveChanges();

                TempData["SuccessMessage"] = "Đổi mật khẩu thành công!";
                return RedirectToAction("TrangChu", "Home");
            }

            ViewBag.Error = "Đã xảy ra lỗi. Vui lòng thử lại.";
            return View();
        }
    }
}
