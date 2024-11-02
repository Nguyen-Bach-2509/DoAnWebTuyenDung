using System;
using System.Linq;
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
            return View();
        }

        // POST: Account/Register
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Register([Bind(Include = "username,password,email,role")] User user)
        {
            
            if (!ModelState.IsValid)
            {
                foreach (var error in ModelState.Values.SelectMany(v => v.Errors))
                {
                    Console.WriteLine(error.ErrorMessage);
                }
            }
            if (ModelState.IsValid)
            {
                // Kiểm tra xem tên người dùng đã tồn tại hay chưa
                if (db.Users.Any(u => u.username == user.username))
                {
                    ModelState.AddModelError("username", "Tên người dùng đã tồn tại. Vui lòng chọn tên khác.");
                    return View(user);
                }

                // Gán các giá trị mặc định cho người dùng mới
                user.created_at = DateTime.Now;
                user.updated_at = DateTime.Now;
                user.role = "EMPLOYER";
                db.Users.Add(user);
                db.SaveChanges();

                // Thông báo đăng ký thành công
                TempData["SuccessMessage"] = "Đăng ký thành công! Bạn có thể đăng nhập ngay bây giờ.";
                return RedirectToAction("Login", "Account");
            }

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
                // Đăng nhập thành công, lưu thông tin vào Session
                Session["UserId"] = user.user_id;
                Session["Username"] = user.username;
                Session["Role"] = user.role;

                return RedirectToAction("Index", "Home");
            }

            else
            {
                ViewBag.ErrorMessage = "Tên người dùng hoặc mật khẩu không đúng.";
                return View();
            }

        }

        // GET: Account/Logout
        public ActionResult Logout()
        {
            Session.Clear();
            return RedirectToAction("Login", "Account");
        }
    }
}
