using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using DoAnWebTuyenDung.Models;

namespace DoAnWebTuyenDung.Areas.Employers.Controllers
{
    public class NotificationsController : Controller
    {
        private DoAnEntities db = new DoAnEntities();

        // GET: Employers/Notifications
        public ActionResult Index()
        {
            int userId = (int)Session["UserId"];
            var notifications = db.Notifications.Where(n => n.user_id == userId).OrderByDescending(n => n.created_at).ToList();
            return View(notifications);
        }

        // GET: Employers/Notifications/MarkAsRead/5
        public ActionResult MarkAsRead(int id)
        {
            var notification = db.Notifications.Find(id);
            if (notification != null)
            {
                notification.is_read = true;
                db.SaveChanges();
            }
            return RedirectToAction("Index");
        }

        // GET: Employers/Notifications/Delete/5
        public ActionResult Delete(int id)
        {
            var notification = db.Notifications.Find(id);
            if (notification != null)
            {
                db.Notifications.Remove(notification);
                db.SaveChanges();
            }
            return RedirectToAction("Index");
        }
    }
}
