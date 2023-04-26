using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebNhaHang.Models;
using WebNhaHang.Models.EF;

namespace WebNhaHang.Areas.Admin.Controllers
{
    public class ProductImageController : Controller
    {
        // GET: Admin/ProductImage
        private ApplicationDbContext db = new ApplicationDbContext();
        public ActionResult Index(int id)
        {
            ViewBag.ProductId = id;
            var items = db.ProductImages.Where(x => x.ProductId == id).ToList();
            return View(items   );
        }
        [HttpPost]
        public ActionResult Delete(int id)
        {
            var item = db.ProductImages.Find(id);
            db.ProductImages.Remove(item);
            db.SaveChanges();
            return Json(new { success =true});
        }
        [HttpPost]
        public ActionResult AddImage(int productId, string url)
        {
            db.ProductImages.Add(new ProductImage
            {
                ProductId = productId,
                Image = url,
                IsDefault=false
            }) ;
            db.SaveChanges();
            return Json(new { success = true });
        }
    }
}