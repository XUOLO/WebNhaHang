using PagedList;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebNhaHang.Models;

namespace WebNhaHang.Areas.Admin.Controllers
{
    public class RecuitmentController : Controller
    {
        // GET: Admin/Recuitment

        private ApplicationDbContext db = new ApplicationDbContext();
        public ActionResult Index(int? page)
        {
            var items = db.Aplications.OrderByDescending(x => x.CreateDate).ToList();
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
    }
}