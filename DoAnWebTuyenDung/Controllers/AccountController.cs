using System;
using System.Linq;
using System.Web.Mvc;
using DoAnWebTuyenDung.Models;

namespace DoAnWebTuyenDung.Controllers
{
    public class AccountController : Controller
    {
        private DoAnEntities db = new DoAnEntities();

        // GET: Register
        public ActionResult Register()
        {
            return View();
        }

        // POST: Register
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Register([Bind(Include = "username,password,email")] User user)
        {
            if (ModelState.IsValid)
            {
                // Kiểm tra xem tên người dùng đã tồn tại trong cơ sở dữ liệu chưa
                if (db.Users.Any(u => u.username == user.username))
                {
                    ModelState.AddModelError("username", "Tên người dùng đã tồn tại. Vui lòng chọn tên khác.");
                    return View(user);
                }

                // Gán các giá trị mặc định cho người dùng mới
                user.role = "User";
                user.created_at = DateTime.Now;
                user.updated_at = DateTime.Now;

                db.Users.Add(user);
                db.SaveChanges();

                // Sử dụng TempData để thông báo về sự thành công của việc đăng ký
                TempData["SuccessMessage"] = "Đăng ký thành công! Bạn có thể đăng nhập ngay bây giờ.";

                // Điều hướng tới trang đăng nhập hoặc trang xác nhận
                return RedirectToAction("LoginLogout", "Account"); // Giả sử trang Login nằm trong AccountController
            }

            return View(user);
        }

        // GET: Register/RegisterSuccess
        public ActionResult RegisterSuccess()
        {
            return View();
        }
    }
}
