
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebNhaHang.Models;
using OfficeOpenXml;
using OfficeOpenXml.Style;
namespace WebNhaHang.Areas.Admin.Controllers
{
    public class StatisticController : Controller
    {
        // GET: Admin/Statistic
        private ApplicationDbContext db = new ApplicationDbContext();
        public ActionResult Index(DateTime? date = null, string month = null)
        {
            if (date == null && string.IsNullOrEmpty(month))
            {
                date = DateTime.Today;
                month = date.Value.ToString("yyyy-MM");
            }

            if (month != null)
            {
                var year = int.Parse(month.Substring(0, 4));
                var monthNumber = int.Parse(month.Substring(5, 2));

                var orders = db.Orders
                    .Where(o => o.CreateDate.Year == year && o.CreateDate.Month == monthNumber)
                    .ToList();

                var totalRevenue = orders.Sum(o => o.TotalAmount);

                ViewBag.DateMonth = new DateTime(year, monthNumber, 1).ToString("MM/yyyy");
                ViewBag.TotalRevenueMonth = totalRevenue.ToString("#,##0 VND");
            }

            if (date != null)
            {
                var orders = db.Orders
                    .Where(o => o.CreateDate.Year == date.Value.Year && o.CreateDate.Month == date.Value.Month && o.CreateDate.Day == date.Value.Day)
                    .ToList();

                var totalRevenue = orders.Sum(o => o.TotalAmount);

                ViewBag.DateDay = date.Value.ToString("dd/MM/yyyy");
                ViewBag.TotalRevenueDay = totalRevenue.ToString("#,##0 VND");
            }

            int count = db.Orders.Count(o => o.TypePayment == 1);
            int countuser = db.Userss.Count();
            ViewBag.CountUsers = countuser;
            ViewBag.CountOrder = count;
            return View();
        }

        
    }
}
