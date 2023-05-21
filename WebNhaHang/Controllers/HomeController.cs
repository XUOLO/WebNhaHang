using System;
using System.Collections.Generic;
using System.Data.Entity.Validation;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Web;
using System.Web.Mvc;
using WebNhaHang.Models;
using WebNhaHang.Models.EF;

namespace WebNhaHang.Controllers
{
    public class HomeController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();
        private DB_Entities _db = new DB_Entities();
        // GET: Home
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult About()
        {
            return View();
        }
        //GET: Register

        public ActionResult Register()
        {
            return View();
        }

        //POST: Register
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Register(User _user)
        {
            if (ModelState.IsValid)
            {
                var check = db.Userss.FirstOrDefault(s => s.Email == _user.Email);
                if (check == null)
                {
                    _user.Password = GetMD5(_user.Password);
                    db.Configuration.ValidateOnSaveEnabled = false;
                    db.Userss.Add(_user);
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }
                else
                {
                    ViewBag.error = "Email already exists";
                    return RedirectToAction("Login");
                }


            }
            return RedirectToAction("Index");


        }

        public ActionResult Login()
        {
            return View();
        }

        public ActionResult ThongTinTaiKhoan()
        {
 
            List<User> users =db.Userss.ToList();
            return View(users);

        }
        public ActionResult InfoReservation()
        {


            List<Reservation> reservations = db.Reservations.ToList();
            return View(reservations);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Login(string email, string password)
        {
            if (ModelState.IsValid)
            {


                var f_password = GetMD5(password);
                var data = db.Userss.Where(s => s.Email.Equals(email) && s.Password.Equals(f_password)).ToList();
                if (data.Count() > 0)
                {
                    //add session
                    Session["FullName"] = data.FirstOrDefault().FirstName + " " + data.FirstOrDefault().LastName;
                    Session["Email"] = data.FirstOrDefault().Email;
                    Session["idUser"] = data.FirstOrDefault().idUser;
               
              
                    return RedirectToAction("Index");
                }
                else
                {
                    ViewBag.error = "Login failed";
                    return RedirectToAction("Login");
                }
            }
            return View();
        }
        public ActionResult ChangePassword(string id, string currentPassword, string newPassword, string confirmPassword)
        {
            Session["thongbao_password"] = "";
            string thongbao = "";
            if (currentPassword == null || newPassword == null)
            {
                thongbao = "Bạn chưa nhập thông tin";
            }

            if (newPassword == confirmPassword)
            {
                var f_password = GetMD5(currentPassword);
                var user = db.Userss.Where(s => s.Email == id && s.Password == f_password).FirstOrDefault();
                if (user != null)
                {
                    user.Password = newPassword;
                    if (ModelState.IsValid)
                    {
                        user.Password = GetMD5(user.Password);
                        db.Configuration.ValidateOnSaveEnabled = false;
           
                        db.SaveChanges();

                        thongbao = "Đã thay đổi mật khẩu";

   

                    }
                }
  

                else
                {
                    thongbao = "Mật khẩu sai";
                }


            }
            else
            {
                thongbao = "Mật khẩu không giống nhau";
            }
            TempData["thongbao_password"] = thongbao;
            return RedirectToAction("ThongTinTaiKhoan");
        }

        //Logout
        public ActionResult Logout()
        {
            Session.Clear();//remove session
            return RedirectToAction("index");
        }
        public ActionResult Order()
        {
            var products = db.Products.ToList();
            return View(products);
            
        }

        //create a string MD5
        public static string GetMD5(string str)
        {
            MD5 md5 = new MD5CryptoServiceProvider();
            byte[] fromData = Encoding.UTF8.GetBytes(str);
            byte[] targetData = md5.ComputeHash(fromData);
            string byte2String = null;

            for (int i = 0; i < targetData.Length; i++)
            {
                byte2String += targetData[i].ToString("x2");

            }
            return byte2String;
        }

    }
}