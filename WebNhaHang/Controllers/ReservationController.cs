using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using WebNhaHang.Models;
using WebNhaHang.Models.EF;

namespace WebNhaHang.Controllers
{
    public class ReservationController : Controller
    {
        // GET: Reservation
        ApplicationDbContext db = new ApplicationDbContext();
       
        public ReservationController()
        {
            
        } 
        public ActionResult Index()
        {
            return View(db.Reservations.ToList());
        }
        public ActionResult CheckOut_Success()
        {
            return View();
        }
        public ActionResult Create()
        {
            List<SelectListItem> rooms = new List<SelectListItem>();
            rooms.Add(new SelectListItem() { Text = "Bàn", Value = "Bàn" });
            rooms.Add(new SelectListItem() { Text = "Bàn đơn", Value = "Bàn đơn" });
            rooms.Add(new SelectListItem() { Text = "Bàn đôi", Value = "Bàn đôi" });
            rooms.Add(new SelectListItem() { Text = "Bàn 6 người", Value = "Bàn 6 người" });
            rooms.Add(new SelectListItem() { Text = "Bàn dài", Value = "Dàn dài" });
           
            ViewBag.Rooms = rooms;
            List<SelectListItem> peoples = new List<SelectListItem>();
            peoples.Add(new SelectListItem() { Text = "Người", Value = "0" });
            peoples.Add(new SelectListItem() { Text = "1 Người", Value = "1" });
            peoples.Add(new SelectListItem() { Text = "2 Người", Value = "2" });
            peoples.Add(new SelectListItem() { Text = "3 Người", Value = "3" });
            peoples.Add(new SelectListItem() { Text = "4 Người", Value = "4" });
            peoples.Add(new SelectListItem() { Text = "5 Người", Value = "5" });
            peoples.Add(new SelectListItem() { Text = "6 Người", Value = "6" });
            peoples.Add(new SelectListItem() { Text = "More", Value = "12" });
            ViewBag.Peoples = peoples;

            return View(new ReservationViewModel());
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(ReservationViewModel model)
        {
            if (!ModelState.IsValid)
            {
                model.Reservations = db.Reservations.ToList();
                return View("Create", model);
            }
            var course = new Reservation()
            {
                Name = model.Name,
                Email = model.Email,
                Phone = model.Phone,
                Address =model.Address,
                DateTime = model.GetDateTime(),
                Room = model.Room,
                NumberOfPeople = model.NumberOfPeople,
            };
            course.CreateDate = DateTime.Now;
            course.ModifieDate = DateTime.Now;
            db.Reservations.Add(course);
            db.SaveChanges();

            return RedirectToAction("CheckOut_Success");  
     
       
        }
       
    }

}