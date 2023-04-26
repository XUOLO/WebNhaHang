using PagedList;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebNhaHang.Models;

namespace WebNhaHang.Areas.Admin.Controllers
{
    public class ReservationController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();
        // GET: Admin/Order
        public ActionResult Index(int? page)
        {
            var items = db.Reservations.OrderByDescending(x => x.CreateDate).ToList();
            if (page == null)
            {
                page = 1;

            }
            var pageNumber = page ?? 1;
            var pageSize = 2;
            ViewBag.Page = pageNumber;
            ViewBag.PageSize = pageSize;
            return View(items.ToPagedList(pageNumber, pageSize));
        }
        public ActionResult View(int id)
        {
            var item = db.Reservations.Find(id);
            return View(item);
        }
        public ActionResult Partial_SanPham(int id)
        {
            var items = db.OrderDetails.Where(x => x.OrderId == id).ToList();
            return PartialView(items);
        }

        [HttpPost]
        public ActionResult UpdateTT(int id, int trangthai)
        {
            var item = db.Orders.Find(id);
            if (item != null)
            {
                db.Orders.Attach(item);
                item.TypePayment = trangthai;
                db.Entry(item).Property(x => x.TypePayment).IsModified = true;
                db.SaveChanges();
                return Json(new { message = "Success", Success = true });
            }
            return Json(new { message = "UnSuccess", Success = false });
        }
    }
}