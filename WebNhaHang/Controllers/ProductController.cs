using PagedList;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebNhaHang.Models;
using WebNhaHang.Models.EF;

namespace WebNhaHang.Controllers
{
    public class ProductController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();
        // GET: Product
        public ActionResult Index( )
        {
            var products = db.Products.ToList();
            return View(products);
        }
         
  
            
        public ActionResult Detail(string alias, int id)
        {
            var items = db.Products.Find(id);
            return View(items);

        }
        public ActionResult ProductCategory(string alias, int id, int? page)
        {
            IEnumerable<Product> items = db.Products.OrderByDescending(x => x.id);
            
            if (id > 0)
            {
                items = items.Where(x => x.ProductCategoryID == id).ToList();

            }
            var cate = db.ProductCategories.Find(id);
            if (cate != null)
            {
                ViewBag.CateName = cate.Title;
            }
            ViewBag.CateId = id;
            


            


            var pageSize = 3;
            if (page == null)
            {
                page = 1;
            }
            var pageIndex = page.HasValue ? Convert.ToInt32(page) : 1;
            items = items.ToPagedList(pageIndex, pageSize);
            ViewBag.PageSize = pageSize;
            ViewBag.Page = page;
            return View(items);



        }
        public ActionResult partial_ItemsByCateId()
        {
            var items = db.Products.Where(x => x.IsHome && x.IsActive).Take(24).ToList();
            return PartialView(items);
        }
        public ActionResult partial_ProductHot()
        {
            var items = db.Products.Where(x => x.IsSale && x.IsActive && x.IsHome).Take(4).ToList();
            return PartialView(items);
        }
    }
}