using System;
using PagedList;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WebNhaHang.Models;
using System.Web.Mvc;
using WebNhaHang.Areas.Admin.ViewModel;

namespace WebNhaHang.Controllers
{
 
    public class RevenueController : Controller
    {
        // GET: Revenue
        private ApplicationDbContext db = new ApplicationDbContext();



        // GET: Admin/Index

        [HttpGet]

        public ActionResult Index(int? pageMonAn, int? pageDanhThu)
        {
            var numPageMonAn = pageMonAn ?? 1;
            var numPageDanhThu = pageDanhThu ?? 1;
            var result = (from h in db.OrderDetails.AsNoTracking()
                          join p in db.Products on h.ProductId equals p.id
                          group h by new { h.ProductId, p.Title } into hh
                          select new EM_TotalPrice()
                          {
                              id = hh.Key.ProductId,
                              TotalPrice = hh.Sum(s => s.Price),
                              name = hh.Key.Title,
                          }).OrderByDescending(i => i.TotalPrice).ToList().ToPagedList(numPageMonAn, 3);
            ViewBag.result = result;
            var resultMonth = (from h in db.OrderDetails.AsNoTracking()
                               join p in db.Orders on h.OrderId equals p.Id
                               group h by new { p.CreateDate } into hh
                               select new VM_Revenue()
                               {
                                   createAt = hh.Key.CreateDate,
                                   TotalPrice = hh.Sum(s => s.Price),

                               }).OrderByDescending(i => i.createAt).ToList().ToPagedList(numPageDanhThu, 3);
            ViewBag.resultMonth = resultMonth;


            return View();

        }
 
        public ActionResult AjaxHandle(int id)
        {

            var resultMonth = (from h in db.OrderDetails.AsNoTracking()
                               join p in db.Orders on h.OrderId equals p.Id
                               where p.CreateDate.Month == id
                               group h by new { p.CreateDate } into hh
                               select new VM_Revenue()
                               {
                                   createAt = hh.Key.CreateDate,
                                   TotalPrice = hh.Sum(s => s.Price),

                               }).OrderByDescending(i => i.createAt).ToList();
            return PartialView(resultMonth);
        }
    }
}