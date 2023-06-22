using Microsoft.Reporting.WebForms;
using PagedList;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebNhaHang.Models;
using WebNhaHang.Models.EF;
using OfficeOpenXml;
using OfficeOpenXml.Style;

namespace WebNhaHang.Areas.Admin.Controllers
{
    [Authorize(Roles = "Admin,Employee")]
    public class ProductController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();
        // GET: Admin/Product
        public ActionResult Index(string SearchString, int? page)
        {
            
            var pageSize = 10;
            if (page == null)
            {
                page = 1;
            }
            IEnumerable<Product> items = db.Products.OrderByDescending(x => x.id).ToList();
            if (!string.IsNullOrEmpty(SearchString))
            {
                items = items.Where(x =>
                    x.Title.IndexOf(SearchString, StringComparison.OrdinalIgnoreCase) >= 0 ||
                    x.Alias.IndexOf(SearchString, StringComparison.OrdinalIgnoreCase) >= 0 ||
                    x.ProductCategory.Title.IndexOf(SearchString, StringComparison.OrdinalIgnoreCase) >= 0 ||
                    x.ProductCategory.Alias.IndexOf(SearchString, StringComparison.OrdinalIgnoreCase) >= 0
                ).ToList();
            }
            var pageIndex = page.HasValue ? Convert.ToInt32(page) : 1;
            items = items.ToPagedList(pageIndex, pageSize);
            ViewBag.PageSize = pageSize;
            ViewBag.Page = page;
            return View(items);
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
        public ActionResult ExportToExcel()
        {
            var products = db.Products.ToList();

            using (var package = new ExcelPackage())
            {
                var worksheet = package.Workbook.Worksheets.Add("Products");

                // Add header row
                worksheet.Cells[1, 1].Value = "No.";
                worksheet.Cells[1, 2].Value = "Title";
                worksheet.Cells[1, 3].Value = "Product Category";
                worksheet.Cells[1, 4].Value = "Description";
                worksheet.Cells[1, 5].Value = "Detail";
                worksheet.Cells[1, 6].Value = "Image";
                worksheet.Cells[1, 7].Value = "Price";
                worksheet.Cells[1, 8].Value = "Quantity";

                // Apply styling to header row
                using (var range = worksheet.Cells[1, 1, 1, 8])
                {
                    range.Style.Font.Bold = true;
                    range.Style.Fill.PatternType = ExcelFillStyle.Solid;
                    range.Style.Fill.BackgroundColor.SetColor(System.Drawing.ColorTranslator.FromHtml("#007bff"));
                    range.Style.Font.Color.SetColor(System.Drawing.Color.White);
                }

                // Add data rows
                for (int i = 0; i < products.Count; i++)
                {
                    worksheet.Cells[i + 2, 1].Value = i + 1; // Số thứ tự tăng dần
                    worksheet.Cells[i + 2, 2].Value = products[i].Title;
                    worksheet.Cells[i + 2, 3].Value = products[i].ProductCategory.Title;
                    worksheet.Cells[i + 2, 4].Value = products[i].Description;
                    worksheet.Cells[i + 2, 5].Value = products[i].Detail;
                    worksheet.Cells[i + 2, 6].Value = products[i].Image;
                    worksheet.Cells[i + 2, 7].Value = products[i].Price;
                    worksheet.Cells[i + 2, 8].Value = products[i].Quantity;
                }

                // Insert column "No." at position 1
                worksheet.InsertColumn(1, 1);

                // Set header style for column "No."
                using (var range = worksheet.Cells[1, 1])
                {
                    range.Style.Font.Bold = true;
                    range.Style.Fill.PatternType = ExcelFillStyle.Solid;
                    range.Style.Fill.BackgroundColor.SetColor(System.Drawing.ColorTranslator.FromHtml("#007bff"));
                    range.Style.Font.Color.SetColor(System.Drawing.Color.White);
                    range.Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                }

                // Add export date row
                int exportDateRowIndex = products.Count + 3; // Add 3 to skip header row and data rows
                worksheet.Cells[exportDateRowIndex, 1].Value = "Exported on:";
                worksheet.Cells[exportDateRowIndex, 2].Value = DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss");

                // Set styling for export date row
                using (var range = worksheet.Cells[exportDateRowIndex, 1, exportDateRowIndex, 2])
                {
                    range.Style.Font.Bold = true;
                    range.Style.HorizontalAlignment = ExcelHorizontalAlignment.Right;
                }

                // Set auto-fit columns
                worksheet.Cells.AutoFitColumns();

                // Set the filename and content type for the response
                string filename = "Products_" + DateTime.Now.ToString("dd/MM/yyyy") + ".xlsx";
                var contentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";

                // Return the Excel file as a byte array
                return File(package.GetAsByteArray(), contentType, filename);
            }
        }
    }
}