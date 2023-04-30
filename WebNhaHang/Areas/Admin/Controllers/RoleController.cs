using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebNhaHang.Models;

namespace WebNhaHang.Areas.Admin.Controllers
{
    [Authorize(Roles ="Admin")]
    public class RoleController : Controller
    {
        // GET: Admin/Role
        ApplicationDbContext db = new ApplicationDbContext();
        public ActionResult Index()
        {
            var items = db.Roles.ToList();
            return View(items);
        }
        public ActionResult Edit(int id)
        {
            var items = db.Roles.Find(id);
            return View(items);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(IdentityRole model)
        {
            if (ModelState.IsValid)
            {
                var roleManager = new RoleManager<IdentityRole>(new RoleStore<IdentityRole>(db));
                roleManager.Update(model);
                return RedirectToAction("index");
            }
            return View(model);
        }
        public ActionResult Create( )
        {
        
            return View(  );
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(IdentityRole model)
        {
            if (ModelState.IsValid)
            {
                var roleManager = new RoleManager<IdentityRole>(new RoleStore<IdentityRole>(db));
                roleManager.Create(model);
                return RedirectToAction("index");
            }
            return View(model);
        }
    }
}