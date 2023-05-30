using PagedList;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Validation;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using WebNhaHang.Models;
using WebNhaHang.Models.EF;
using Facebook;
using System.Text.RegularExpressions;

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

        //public ActionResult Register()
        //{
        //    return View();
        //}

        //POST: Register
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public ActionResult Register(User _user)
        //{
        //    Session["thongbao_register"] = "";
        //    string thongbao = "";
        //    if (ModelState.IsValid)
        //    {
        //        var check = db.Userss.FirstOrDefault(s => s.Email == _user.Email);
        //        if (check == null)
        //        {
        //            _user.Password = GetMD5(_user.Password);
        //            db.Configuration.ValidateOnSaveEnabled = false;
        //            db.Userss.Add(_user);
        //            db.SaveChanges();
        //            return RedirectToAction("Index");
        //        }
        //        else
        //        {
        //           thongbao = "Email already exists";
        //            return RedirectToAction("Login");
        //        }


        //    }
        //    TempData["thongbao_register"] = thongbao;
        //    return RedirectToAction("Index");


        //}

        public ActionResult Login()
        {
            return View();
        }

        public ActionResult ThongTinTaiKhoan()
        {

            List<User> users = db.Userss.ToList();
            return View(users);

        }
        public ActionResult InfoReservation(int? page)
        {

            var items = db.Reservations.OrderByDescending(x => x.CreateDate).ToList();
            if (page == null)
            {
                page = 1;

            }
            var pageNumber = page ?? 1;
            var pageSize = 10;
            ViewBag.Page = pageNumber;
            ViewBag.PageSize = pageSize;
            return View(items.ToPagedList(pageNumber, pageSize));

            
        }
 

        //public ActionResult ChangePassword(string id, string currentPassword, string newPassword, string confirmPassword)
        //{
        //    Session["thongbao_password"] = "";
        //    string thongbao = "";
        //    if (currentPassword == null || newPassword == null)
        //    {
        //        thongbao = "You have not entered information";
        //    }



        //    if (newPassword == confirmPassword)
        //    {
        //        var f_password = GetMD5(currentPassword);
        //        var user = db.Userss.Where(s => s.Email == id && s.Password == f_password).FirstOrDefault();
        //        if (user != null)
        //        {
        //            user.Password = newPassword;
        //            if (ModelState.IsValid)
        //            {
        //                user.Password = GetMD5(user.Password);
        //                db.Configuration.ValidateOnSaveEnabled = false;

        //                db.SaveChanges();

        //                thongbao = "Password changed";



        //            }
        //        }


        //        else
        //        {
        //            thongbao = "Wrong password";
        //        }


        //    }
        //    else
        //    {
        //        thongbao = "Passwords are not the same";
        //    }
        //    TempData["thongbao_password"] = thongbao;
        //    return RedirectToAction("ThongTinTaiKhoan");
        //}

        public ActionResult ChangePassword(string id, string currentPassword, string newPassword, string confirmPassword)
        {
            Session["thongbao_password"] = "";
            string thongbao = "";
            string errorMessage = ValidatePassword(currentPassword, newPassword);
            if (!string.IsNullOrEmpty(errorMessage))
            {
                thongbao = errorMessage;
            }
            else if (newPassword == confirmPassword)
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

                        thongbao = "Password changed";
                    }
                }
                else
                {
                    thongbao = "Wrong password";
                }
            }
            else
            {
                thongbao = "Passwords are not the same";
            }
            TempData["thongbao_password"] = thongbao;
            return RedirectToAction("ThongTinTaiKhoan");
        }

        private string ValidatePassword(string currentPassword, string newPassword)
        {
            if (currentPassword == null || newPassword == null)
            {
                return "You have not entered information";
            }
            else if (newPassword.Length < 8)
            {
                return "New password must be at least 8 characters long";
            }
            else if (!Regex.IsMatch(newPassword, "^(?=.*[a-z])(?=.*[A-Z])(?=.*\\d).{8,15}$"))
            {
                return "New password must contain at least one lowercase letter, one uppercase letter, and one digit, and must be between 8 and 15 characters long";
            }
            else
            {
                return "";
            }
        }


        //Logout
        public ActionResult Logout()
        {
            Session.Clear();//remove session
            return RedirectToAction("index");
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



        // Đã đặt bàn
        public ActionResult CheckDatBan(string id, string check)
        {
            if (id == null || check == null || id == "" || check == "")
            {
                return View("error");
            }
            else
            {
                var tr = db.Reservations.Where(s => s.Code == id).FirstOrDefault();
                if (tr != null && check == "300")
                {
                    tr.Status = -1; //Hủy
                }

                if (ModelState.IsValid)
                {
                    db.SaveChanges();
                }
                return RedirectToAction("InfoReservation");

            }






        }

        // Đã đặt bàn
        public ActionResult CheckNhanHang(string id, string check)
        {
            if (id == null || check == null || id == "" || check == "")
            {
                return View("error");
            }
            else
            {
                var tr = db.Orders.Where(s => s.Code == id).FirstOrDefault();
                if (tr != null && check == "300")
                {
                    tr.TypePayment = 0; //Hủy
                } else if (tr != null && check == "200")
                {
                    tr.TypePayment = 0; //da xac nhan
                }

                else if (tr != null && check == "000")
                {
                    tr.TypePayment = 1; //Mua lại
                }
                if (ModelState.IsValid)
                {
                    db.SaveChanges();
                }
                return RedirectToAction("index","order");

            }

        }



        public ActionResult FacebookRedirect(string code)
        {
            var fb = new FacebookClient();
            dynamic result = fb.Get("/oauth/access_token", new
            {
                client_id = "596973132440118",
                client_scret = "f2510918c6235351e7eb47ff808c9c63",
                redirect_uri = "https://localhost:44339/Home/FacebookRedirect",
                code = code
            });
            fb.AccessToken = result.access_token;
            dynamic me = fb.Get("/me?fields=name,email");
            string name = me.name;
            string email = me.email;
            return RedirectToAction("loginurl");
        }
    [HttpPost]
        public ActionResult loginurl()
        {
            var fb = new FacebookClient();
            var loginUrl = fb.GetLoginUrl(new
            {
                client_id = "596973132440118",
                redirect_url = "https://localhost:44339/Home/FacebookRedirect",
                scope = "public_profile,email"
            });

            ViewBag.loginUrl = loginUrl;
            return View();
        }
        public ActionResult Confirmemailfailed()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Login(string email, string password)
        {

           

            if (ModelState.IsValid)
            { 
                 


                var f_password = GetMD5(password);
                var user = db.Userss.FirstOrDefault(s => s.Email.Equals(email) && s.Password.Equals(f_password));
                if (user != null)
                {
                    if (user.IsEmailVerified == false)
                    {
                        TempData["thongbao_login"] = "Your email has not been verified yet. Please check your email.";
                        
                        return RedirectToAction("login","home");
                    }

                    //add session
                    Session["FullName"] = user.FirstName + " " + user.LastName;
                    Session["Email"] = user.Email;
                    Session["idUser"] = user.idUser;

                    return RedirectToAction("Index");
                }
                else
                {
                    TempData["thongbao_login"] = "Login failed";
                    return RedirectToAction("Login");
                }
            }
       
            return View();
        }

        public ActionResult ConfirmEmailPage()
        {
            return View();
        }
        [AllowAnonymous]
        public ActionResult Register()
        {
            return View();
        }
        [HttpPost]

        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Register(User _user)
        {
            
            if (ModelState.IsValid)
            {
                var check = db.Userss.FirstOrDefault(s => s.Email == _user.Email);
                if (check == null)
                {
                    _user.IsEmailVerified = false;
                    _user.ActivationCode = Guid.NewGuid();

                    // Add user to database



                    // Send email verification
                    SendVerificationEmail(_user);

                    TempData["thongbao_register"] = "Registration successful. Please check your email to confirm.";



                    _user.Password = GetMD5(_user.Password);
                    db.Configuration.ValidateOnSaveEnabled = false;
                    db.Userss.Add(_user);
                    await db.SaveChangesAsync();
                    return RedirectToAction("ConfirmEmailPage","home");
                }
                else
                {
                    TempData["thongbao_register"] = "Email already exists";
                    return RedirectToAction("Login");
                }


            }
         
            return RedirectToAction("Index");


        }


     
        public ActionResult ConfirmEmailSuccess()
        {
            return View();
        }

        public ActionResult VerifyEmail(string id)
        {
            try
            {
                bool IsVerified = false;
                db.Configuration.ValidateOnSaveEnabled = false; // This line avoids validation of entity at SaveChanges
                var user = db.Userss.Where(u => u.ActivationCode == new Guid(id)).FirstOrDefault();
                if (user != null)
                {
                    user.IsEmailVerified = true;
                    db.SaveChanges();
                    IsVerified = true;
                }

                if (IsVerified)
                {
                    TempData["SuccessMessage"] = "Your email address has been verified successfully.";
                    return RedirectToAction("ConfirmEmailSuccess", "Home");
                }
                else
                {
                    TempData["ErrorMessage"] = "Invalid verification code. Please try again.";
                    return RedirectToAction("Index", "Home");
                }
            }
            catch (Exception e)
            {
                Console.WriteLine("Error Message: " + e.Message);
                Console.WriteLine("Inner Exception: " + e.InnerException?.Message);
                Console.WriteLine("StackTrace: " + e.StackTrace);
                throw;
            }
        }



        //// doi mat khau
        public ActionResult ResetPasswordUser()
        {
            return View();
        }
        public ActionResult RsPasswordUser()
        {
           
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> RsPasswordUser( string email )
        {
            if (ModelState.IsValid)
            {
                var user = db.Userss.FirstOrDefault(s => s.Email.Equals(email));
                if (user == null)
                {
                    ViewBag.error = "Email does not exist.";
                    return View();
                }
                ViewBag.SuccessMessage = "Your password has been reset. Please check your email for the new password.";
                var newPassword = GenerateRandomPassword();
                user.Password = GetMD5(newPassword);

                string contentCustomer = System.IO.File.ReadAllText(Server.MapPath("/Content/template/sendrs.html"));
              
                 
                contentCustomer = contentCustomer.Replace("{{newpass}}", newPassword);
              
                WebNhaHang.Common.Common.SendMail("BBQ Restaurant XuoLo", "New Password", contentCustomer.ToString(), user.Email);
                db.Configuration.ValidateOnSaveEnabled = false;


                await db.SaveChangesAsync();




               

            } return View();
        }
        //[ValidateAntiForgeryToken]
        //[HttpPost]
        //public async Task<ActionResult> ResetPasswordUser(string email)
        //{
        //    // Kiểm tra xem email có tồn tại trong cơ sở dữ liệu hay không
        //    var user = db.Userss.FirstOrDefault(s => s.Email.Equals(email));
        //    if (user == null)
        //    {
        //        ViewBag.error = "Email does not exist.";
        //        return View();
        //    }
        //    var newPassword = GenerateRandomPassword();

        //    user.Password = GetMD5(newPassword);
        //    // Send email verification
        //    SendPasswordResetEmail(user, newPassword);

        //    ViewBag.SuccessMessage = "Your password has been reset. Please check your email for the new password.";




         

        //    // Lưu mật khẩu mới vào cơ sở dữ liệu
            

        //    db.Configuration.ValidateOnSaveEnabled = false;


        //    await db.SaveChangesAsync();


          
                
        //    return View();

        //}
        private void SendVerificationEmail(User modelUser)
        {
            var verifyUrl = "/Home/VerifyEmail/" + modelUser.ActivationCode.ToString();
            var link = Request.Url.AbsoluteUri.Replace(Request.Url.PathAndQuery, verifyUrl);

            var fromEmail = new MailAddress("xuanloc290901@gmail.com", "BBQ Restaurant XuoLo");
            var toEmail = new MailAddress(modelUser.Email);
            var fromEmailPassword = "anvawekvdmnwjcwh";

            string subject = "Your account is successfully created";

            string body = "<br/><br/>We are excited to tell you that your account is successfully created. Please click on the below link to verify your account" +
                            "<br/><br/><a href='" + link + "'>" + link + "</a>";

            var smtp = new SmtpClient
            {
                Host = "smtp.gmail.com",
                Port = 587,
                EnableSsl = true,
                DeliveryMethod = SmtpDeliveryMethod.Network,
                UseDefaultCredentials = false,
                Credentials = new NetworkCredential(fromEmail.Address, fromEmailPassword)
            };

            using (var message = new MailMessage(fromEmail, toEmail)
            {
                Subject = subject,
                Body = body,
                IsBodyHtml = true
            })
            {
                smtp.Send(message);
            }
        }

        private void SendPasswordResetEmail(User user, string newPassword)
        {
            var verifyUrl = "/Home/login/";
            var link = Request.Url.AbsoluteUri.Replace(Request.Url.PathAndQuery, verifyUrl);

            var fromEmail = new MailAddress("final0167@gmail.com", "BBQ Restaurant XuoLo");
            var toEmail = new MailAddress(user.Email);
            var fromEmailPassword = "fczprexnbisgxzpg";

            string subject = "Your password has been reset";

            string body = "<p>Hello,"  + user.FirstName + "" + user.LastName +"</p>"+
                             "<p>Your password has been reset successfully. Your new password is: <strong>" + newPassword + "</strong></p>" +
                             "<p>Please use this password to log in to your account and then change your password for security reasons.</p>" +
                             "<br/><br/><a href=''> Login </a>";

            var smtp = new SmtpClient
            {
                Host = "smtp.gmail.com",
                Port = 587,
                EnableSsl = true,
                DeliveryMethod = SmtpDeliveryMethod.Network,
                UseDefaultCredentials = false, // set this to false before assigning credentials
                Credentials = new NetworkCredential(fromEmail.Address, fromEmailPassword)
            };

            using (var message = new MailMessage(fromEmail, toEmail)
            {
                Subject = subject,
                Body = body,
                IsBodyHtml = true
            })
            {
                smtp.Send(message);
            }

        }


        private string GenerateRandomPassword()
        {
            // Tạo một chuỗi ngẫu nhiên có 8 ký tự
            const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789";
            var random = new Random();
            var password = new string(
               Enumerable.Repeat(chars, 10)
               .Select(s => s[random.Next(s.Length)])
               .ToArray());

            return password;
        }

        



    }
}