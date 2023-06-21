using Microsoft.Reporting.WebForms;
using PagedList;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebNhaHang.Models;
using OfficeOpenXml;
using OfficeOpenXml.Style;
namespace WebNhaHang.Areas.Admin.Controllers
{
    [Authorize(Roles = "Admin,Employee")]
    public class ReservationController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();
        // GET: Admin/Order
        public ActionResult Index(int? page)
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
        public ActionResult View(int id)
        {
            var item = db.Reservations.Find(id);
            return View(item);
        }
        public ActionResult Partial_SanPham(int id)
        {
            var items = db.OrderDetails.Where(x => x.OrderId == id).ToList();
            return PartialView(items);
        }
        public ActionResult ExportToExcel()
        {
            var reservations = db.Reservations.ToList();

            using (var package = new ExcelPackage())
            {
                var worksheet = package.Workbook.Worksheets.Add("Reservations");

                // Add header row
                worksheet.Cells[1, 1].Value = "NO.";
                worksheet.Cells[1, 2].Value = "Code";
                worksheet.Cells[1, 3].Value = "Name";
                worksheet.Cells[1, 4].Value = "Email";
                worksheet.Cells[1, 5].Value = "Phone";
                worksheet.Cells[1, 6].Value = "Note";
                worksheet.Cells[1, 7].Value = "Room";
                worksheet.Cells[1, 8].Value = "Status";
                worksheet.Cells[1, 9].Value = "Number of People";
                worksheet.Cells[1, 10].Value = "Arrival time";

                // Apply styling to header row
                using (var range = worksheet.Cells[1, 1, 1, 10])
                {
                    range.Style.Font.Bold = true;
                    range.Style.Fill.PatternType = ExcelFillStyle.Solid;
                    range.Style.Fill.BackgroundColor.SetColor(System.Drawing.ColorTranslator.FromHtml("#007bff"));
                    range.Style.Font.Color.SetColor(System.Drawing.Color.White);
                }

                // Add data rows
                for (int i = 0; i < reservations.Count; i++)
                {
                    worksheet.Cells[i + 2, 1].Value = i + 1; // Số thứ tự là i + 1
                    worksheet.Cells[i + 2, 2].Value = reservations[i].Code;
                    worksheet.Cells[i + 2, 3].Value = reservations[i].Name;
                    worksheet.Cells[i + 2, 4].Value = reservations[i].Email;
                    worksheet.Cells[i + 2, 5].Value = reservations[i].Phone;
                    worksheet.Cells[i + 2, 6].Value = reservations[i].Note;
                    worksheet.Cells[i + 2, 7].Value = reservations[i].Room;

                    // Convert Status value to string
                    string statusString;
                    switch (reservations[i].Status)
                    {
                        case (-1):
                            statusString = "Cancelled";
                            break;
                        case 1:
                            statusString = "Approved";
                            break;
                        case 0:
                            statusString = "unapproved";
                            break;
                        default:
                            statusString = "";
                            break;
                    }
                    worksheet.Cells[i + 2, 8].Value = statusString;

                    worksheet.Cells[i + 2, 9].Value = reservations[i].NumberOfPeople;
                    worksheet.Cells[i + 2, 10].Value = reservations[i].DateTime.ToString("dd/MM/yyyy HH:mm:ss");
                }

             

                // Add export date row
                int exportDateRowIndex = reservations.Count + 3; // Add 3 to skip header row, data rows, and numbering row
                worksheet.Cells[exportDateRowIndex, 1].Value = "Exported on:";
                worksheet.Cells[exportDateRowIndex, 2].Value = DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss");

                // Set styling for export date row
                using (var range = worksheet.Cells[exportDateRowIndex, 1, exportDateRowIndex, 2])
                {
                    range.Style.Font.Bold = true;
                    range.Style.HorizontalAlignment = ExcelHorizontalAlignment.Right;
                }
                // Auto-fit columns
                worksheet.Cells.AutoFitColumns();
                // Set the filename and content type for the response
                string filename = "Reservations_" + DateTime.Now.ToString("dd/MM/yyyy") + ".xlsx";
                var contentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";

                // Return the Excel file as a byte array
                return File(package.GetAsByteArray(), contentType, filename);
            }
        }
        [HttpPost]
        public ActionResult UpdateTT(int id, int trangthai)
        {
            var item = db.Reservations.Find(id);
            if (item != null)
            {
                db.Reservations.Attach(item);
                item.Status = trangthai;
                db.Entry(item).Property(x => x.Status).IsModified = true;
                db.SaveChanges();
                return Json(new { message = "Success", Success = true });
            }
            return Json(new { message = "UnSuccess", Success = false });
        }
    }
}