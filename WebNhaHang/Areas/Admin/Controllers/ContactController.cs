using PagedList;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebNhaHang.Models;

namespace WebNhaHang.Areas.Admin.Controllers
{
    [Authorize(Roles = "Admin,Employee")]
    public class ContactController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();
         
        public ActionResult Index(int? page)
        {
            var items = db.Contacts.OrderByDescending(x => x.CreateDate).ToList();
            if (page == null)
            {
                page = 1;

            }
            var pageNumber = page ?? 1;
            var pageSize = 10;
            ViewBag.Page = pageNumber;
            ViewBag.PageSize = pageSize;
            return View(items.ToPagedList(pageNumber, pageSize));
        }
        public ActionResult View(int id)
        {
            var item = db.Contacts.Find(id);
            return View(item);
        }
        [HttpPost]
        public ActionResult UpdateTT(int id, int trangthai)
        {
            var item = db.Contacts.Find(id);
            if (item != null)
            {
                db.Contacts.Attach(item);
                item.Isread = trangthai;
                db.Entry(item).Property(x => x.Isread).IsModified = true;
                db.SaveChanges();
                return Json(new { message = "Success", Success = true });
            }
            return Json(new { message = "UnSuccess", Success = false });
        }

    }
}