using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebNhaHang.Models;
using WebNhaHang.Models.EF;

namespace WebNhaHang.Controllers
{
    public class RecruitmentController : Controller
    {
        ApplicationDbContext db = new ApplicationDbContext();
        // GET: Recruitment
        public ActionResult Index()
        {
            return View(db.Aplications.ToList());
        }
        public RecruitmentController() { }

        public ActionResult Application()
        {
            List<SelectListItem> positions = new List<SelectListItem>();
            positions.Add(new SelectListItem() { Text = "Business Administration Director", Value = "Business Administration Director" });
            positions.Add(new SelectListItem() { Text = "Sales staff", Value = "Sales staff" });
            positions.Add(new SelectListItem() { Text = "Receptionist", Value = "Receptionist" });
            positions.Add(new SelectListItem() { Text = "Service Staff", Value = "Service Staff" });

            ViewBag.Positions = positions;

            List<SelectListItem> experiences = new List<SelectListItem>();
            experiences.Add(new SelectListItem() { Text = "1 year", Value = "1 year" });
            experiences.Add(new SelectListItem() { Text = "2 years", Value = "2 years" });
            experiences.Add(new SelectListItem() { Text = "not yet", Value = "not yet" });

            ViewBag.Experience = experiences;

            return View(new ApplicationViewModel());
         
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Application(ApplicationViewModel model)
        {
            

            if (!ModelState.IsValid)
            {
                model.Aplications = db.Aplications.ToList();
                return View("Application", model);
            }
            var course = new Aplication()
            {
                FullName = model.FullName,
                Email = model.Email,
                Phone = model.Phone,
                Address = model.Address,
               
                Position = model.Position,
                Experience = model.Experience,
            };
            Random rd = new Random();
            course.Code = "MS" + rd.Next(0, 9) + rd.Next(0, 9) + rd.Next(0, 9) + rd.Next(0, 9);
            course.CreateDate = DateTime.Now;
            course.ModifieDate = DateTime.Now;
            db.Aplications.Add(course);
            db.SaveChanges();
            //gui kh
            string contentCustomer = System.IO.File.ReadAllText(Server.MapPath("~/Content/template/send1.html"));
            contentCustomer = contentCustomer.Replace("{{TenKhachHang}}", course.FullName);
            contentCustomer = contentCustomer.Replace("{{Phone}}", course.Phone);
            contentCustomer = contentCustomer.Replace("{{Email}}", course.Email);
            contentCustomer = contentCustomer.Replace("{{DiaChi}}", course.Address);
            contentCustomer = contentCustomer.Replace("{{ViTri}}", course.Position);
            contentCustomer = contentCustomer.Replace("{{KinhNghiem}}", course.Experience);
            WebNhaHang.Common.Common.SendMail("XuoLo BBQ Restaurant", "Code #" + course.Code, contentCustomer.ToString(), course.Email);

            //gui admin
            string contentAdmin = System.IO.File.ReadAllText(Server.MapPath("~/Content/template/send2.html"));
            contentAdmin = contentAdmin.Replace("{{TenKhachHang}}", course.FullName);
            contentAdmin = contentAdmin.Replace("{{ViTri}}", course.Position);
            contentAdmin = contentAdmin.Replace("{{Knowledge}}", course.Experience);
            contentAdmin = contentAdmin.Replace("{{Phone}}", course.Phone);
            contentAdmin = contentAdmin.Replace("{{Email}}", course.Email);
            contentAdmin = contentAdmin.Replace("{{DiaChi}}", course.Address);
            WebNhaHang.Common.Common.SendMail("XuoLo BBQ Restaurant", "Code #" + course.Code, contentAdmin.ToString(), ConfigurationManager.AppSettings["EmailAdmin"]);
            return RedirectToAction("Application_Success");


        }
        public ActionResult Application_Success()
        {
            return View();
        }
    }


}