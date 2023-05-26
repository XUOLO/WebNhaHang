using Microsoft.Reporting.WebForms;
using PagedList;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebNhaHang.Models;
using WebNhaHang.Models.EF;
 

namespace WebNhaHang.Areas.Admin.Controllers
{
    [Authorize(Roles = "Admin,Employee")]
    public class ProductController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();
        // GET: Admin/Product
        public ActionResult Index(int? page)
        {
            IEnumerable<Product> items = db.Products.OrderByDescending(x=>x.id);
            var pageSize = 10;
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
        public ActionResult Reports(string ReportType)
        {
            LocalReport localreport = new LocalReport();
            localreport.ReportPath = Server.MapPath("~/report/ReportProduct.rdlc");

            ReportDataSource reportDataSource = new ReportDataSource();
            reportDataSource.Name = "DataSetProduct";
            reportDataSource.Value = db.Products.ToList();
            localreport.DataSources.Add(reportDataSource);
            string reportType = ReportType;
            string mimeType;
            string encoding;
            string fileNameExtension;
            if (reportType == "Excel")
            {
                fileNameExtension = "xlsx";
            }
            if (reportType == "Word")
            {
                fileNameExtension = "docx";
            }
            if (reportType == "PDF")
            {
                fileNameExtension = "pdf";
            }
            else
            {
                fileNameExtension = "jpg";
            }
            string[] streams;
            Warning[] warnings;
            byte[] renderedByte;
            renderedByte = localreport.Render(reportType, "", out mimeType, out encoding, out fileNameExtension, out streams, out warnings);
            Response.AddHeader("content-disposition", "attachment;filename= Product_report." + fileNameExtension);
            return File(renderedByte, fileNameExtension);

        }
        public ActionResult Add()
        {
            ViewBag.ProductCategory = new SelectList(db.ProductCategories.ToList(), "Id", "Title");
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Add(Product model,List<string> Images,List<int> rDefault)
        {
              if (ModelState.IsValid)
              {
                if (Images != null && Images.Count > 0)
                {
                    for(int i = 0; i < Images.Count; i++)
                    {
                        if (i + 1 == rDefault[0])
                        {
                            model.Image = Images[i];
                            model.ProductImage.Add(new ProductImage
                            {
                                ProductId = model.id,
                                Image=Images[i],
                                IsDefault=true
                            });
                        }
                        else
                        {
                            model.ProductImage.Add(new ProductImage
                            {
                                ProductId = model.id,
                                Image = Images[i],
                                IsDefault = false
                            });
                        }
                    }
                }
                model.CreateDate = DateTime.Now;
                model.ModifieDate = DateTime.Now;
                if (string.IsNullOrEmpty(model.SeoTitle))
                {
                    model.SeoTitle = model.Title;
                }
                    if (string.IsNullOrEmpty(model.Alias))
                model.Alias = WebNhaHang.Models.Common.Filter.FilterChar(model.Title);
                db.Products.Add(model);
                db.SaveChanges();
                return RedirectToAction("Index");
              }
            ViewBag.ProductCategory = new SelectList(db.ProductCategories.ToList(), "Id", "Title");
            return View(model);
        }
        public ActionResult Edit(int id)
        {
            ViewBag.ProductCategory = new SelectList(db.ProductCategories.ToList(), "Id", "Title");
            var item = db.Products.Find(id);
            return View(item);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Product model)
        {
            if (ModelState.IsValid)
            {
                model.ModifieDate = DateTime.Now;
                model.Alias = WebNhaHang.Models.Common.Filter.FilterChar(model.Title);
                db.Products.Attach(model);
                db.Entry(model).State = System.Data.Entity.EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(model);
        }

        [HttpPost]
        public ActionResult Delete(int id)
        {
            var item = db.Products.Find(id);
            if (item != null)
            {
                db.Products.Remove(item);
                db.SaveChanges();
                return Json(new { success = true });
            }
            return Json(new { success = false });
        }

        [HttpPost]
        public ActionResult IsActive(int id)
        {
            var item = db.Products.Find(id);
            if (item != null)
            {
                item.IsActive = !item.IsActive;
                db.Entry(item).State = System.Data.Entity.EntityState.Modified;
                db.SaveChanges();
                return Json(new { success = true, isActive = item.IsActive });
            }
            return Json(new { success = false });
        }
        [HttpPost]
        public ActionResult IsHome(int id)
        {
            var item = db.Products.Find(id);
            if (item != null)
            {
                item.IsHome= !item.IsHome;
                db.Entry(item).State = System.Data.Entity.EntityState.Modified;
                db.SaveChanges();
                return Json(new { success = true, isHome = item.IsHome });
            }
            return Json(new { success = false });
        }
        [HttpPost]
        public ActionResult IsSale(int id)
        {
            var item = db.Products.Find(id);
            if (item != null)
            {
                item.IsSale = !item.IsSale;
                db.Entry(item).State = System.Data.Entity.EntityState.Modified;
                db.SaveChanges();
                return Json(new { success = true, isSale = item.IsSale });
            }
            return Json(new { success = false });
        }

    }
}