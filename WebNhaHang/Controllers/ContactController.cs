using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebNhaHang.Models;
using WebNhaHang.Models.EF;

namespace WebNhaHang.Controllers
{
    public class ContactController : Controller
    {
        // GET: Contact
        ApplicationDbContext db = new ApplicationDbContext();

        public ActionResult Index()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Index(Contact model)
        {
           
            var course = new Contact()
            {
                Name = model.Name,
                Email = model.Email,
                Phone = model.Phone,
                Message = model.Message,
               
                 
            };
            
            course.CreateDate = DateTime.Now;
            course.ModifieDate = DateTime.Now;
            db.Contacts.Add(course);
            db.SaveChanges();
            return View();
        }
    }
}