using Microsoft.Reporting.WebForms;
using System;
using System.Collections.Generic;
using System.Configuration;
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
        public ActionResult InfoReservation()
        {


            List<Reservation> reservations = db.Reservations.ToList();
            return View(reservations);
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
                Note =model.Note,
                DateTime = model.GetDateTime(),
                Room = model.Room,
                NumberOfPeople = model.NumberOfPeople,
            };
            Random rd = new Random();
            course.Code = "MS" + rd.Next(0, 9) + rd.Next(0, 9) + rd.Next(0, 9) + rd.Next(0, 9);
            
            course.CreateDate = DateTime.Now;
            course.ModifieDate = DateTime.Now;
            db.Reservations.Add(course);
            db.SaveChanges();
            string contentCustomer = System.IO.File.ReadAllText(Server.MapPath("~/Content/template/send3.html"));
            contentCustomer = contentCustomer.Replace("{{TenKhachHang}}", course.Name);
            contentCustomer = contentCustomer.Replace("{{MaDon}}", course.Code);
            contentCustomer = contentCustomer.Replace("{{Phone}}", course.Phone);
            contentCustomer = contentCustomer.Replace("{{Email}}", course.Email);
            contentCustomer = contentCustomer.Replace("{{GhiChu}}", course.Note);
            contentCustomer = contentCustomer.Replace("{{Phong}}", course.Room);
            contentCustomer = contentCustomer.Replace("{{SoNguoi}}", course.NumberOfPeople);
            contentCustomer = contentCustomer.Replace("{{NgayDat}}", DateTime.Now.ToString("dd/MM/yyyy"));
            contentCustomer = contentCustomer.Replace("{{ThoiGian}}", course.DateTime.ToString());
             
            WebNhaHang.Common.Common.SendMail("Nhà hàng BBQ", "Mã đặt bàn #" + course.Code, contentCustomer.ToString(), course.Email);

            //gui admin
            string contentAdmin = System.IO.File.ReadAllText(Server.MapPath("~/Content/template/send4.html"));
            contentAdmin = contentAdmin.Replace("{{TenKhachHang}}", course.Name);
            contentAdmin = contentAdmin.Replace("{{NgayDat}}", DateTime.Now.ToString("dd/MM/yyyy"));
            contentAdmin = contentAdmin.Replace("{{MaDon}}", course.Code);
            contentAdmin = contentAdmin.Replace("{{Phone}}", course.Phone);
            contentAdmin = contentAdmin.Replace("{{Email}}", course.Email);
            contentAdmin = contentAdmin.Replace("{{GhiChu}}", course.Note);
            contentAdmin = contentAdmin.Replace("{{Phong}}", course.Room);
            contentAdmin = contentAdmin.Replace("{{SoNguoi}}", course.NumberOfPeople);
            contentAdmin = contentAdmin.Replace("{{ThoiGian}}", course.DateTime.ToString());
            WebNhaHang.Common.Common.SendMail("Đơn đặt bàn nhà hàng BBQ", "Mã đặt bàn #" + course.Code, contentAdmin.ToString(), ConfigurationManager.AppSettings["EmailAdmin"]);
           
            return RedirectToAction("CheckOut_Success");  
     
       
        }
       
    }

}