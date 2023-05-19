using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebNhaHang.Models;

namespace WebNhaHang.Areas.Admin.Controllers
{
    //[Authorize(Roles = "Admin,Employee")]
    public class HomeController : Controller
    {
        // GET: Admin/Home
        private ApplicationDbContext db = new ApplicationDbContext();
        public ActionResult Index()
        {
            return View();
        }
         
    }
}