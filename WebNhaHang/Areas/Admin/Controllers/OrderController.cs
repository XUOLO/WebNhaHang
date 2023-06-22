using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebNhaHang.Models;
using PagedList;
using OfficeOpenXml;
using OfficeOpenXml.Style;
using MigraDoc.DocumentObjectModel;
using MigraDoc.DocumentObjectModel.Tables;
using System.IO;
using MigraDoc.Rendering;
using WebNhaHang.Models.EF;

namespace WebNhaHang.Areas.Admin.Controllers
{
    [Authorize(Roles = "Admin,Employee")]
    public class OrderController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();
        // GET: Admin/Order
        public ActionResult Index(string SearchString, int? page)
        {
            
            if(page==null)
            {
                page = 1;

            }

            IEnumerable<Order> items = db.Orders.OrderByDescending(x => x.CreateDate).ToList();

            if (!string.IsNullOrEmpty(SearchString))
            {
                items = items.Where(x => x.Code.Contains(SearchString) || x.CustomerName.Contains(SearchString) || x.Phone.Contains(SearchString) || x.Mail.Contains(SearchString)).ToList();
            }
            var pageNumber = page ?? 1;
            var pageSize = 10;
            ViewBag.Page = pageNumber;
            ViewBag.PageSize = pageSize;
            return View(items.ToPagedList(pageNumber,pageSize));
        }
        public ActionResult View(int id)
        {
            var item = db.Orders.Find(id);
            return View(item);
        }
        public ActionResult Partial_SanPham(int id)
        {
            var items = db.OrderDetails.Where(x => x.OrderId == id).ToList();
            return PartialView(items);
        }

        [HttpPost]
        public ActionResult UpdateTT(int id,int trangthai)
        {
            var item = db.Orders.Find(id);
            if (item != null)
            {
                db.Orders.Attach(item);
                item.TypePayment = trangthai;
                db.Entry(item).Property(x => x.TypePayment).IsModified = true;
                db.SaveChanges();
                return Json(new { message = "Success", Success = true });
            }
            return Json(new { message = "UnSuccess", Success = false });
        }
        public ActionResult ExportToExcel()
        {
            var orders = db.Orders.ToList();

            using (var package = new ExcelPackage())
            {
                var worksheet = package.Workbook.Worksheets.Add("Orders");

                // Add header row
                worksheet.Cells[1, 1].Value = "Code";
                worksheet.Cells[1, 2].Value = "Customer Name";
                worksheet.Cells[1, 3].Value = "Phone";
                worksheet.Cells[1, 4].Value = "Address";
                worksheet.Cells[1, 5].Value = "Mail";
                worksheet.Cells[1, 6].Value = "Total Amount (VND)";
                worksheet.Cells[1, 7].Value = "Quantity";
                worksheet.Cells[1, 8].Value = "Type Payment";
                worksheet.Cells[1, 9].Value = "Date Created";

                // Apply styling to header row
                using (var range = worksheet.Cells[1, 1, 1, 9])
                {
                    range.Style.Font.Bold = true;
                    range.Style.Fill.PatternType = ExcelFillStyle.Solid;
                    range.Style.Fill.BackgroundColor.SetColor(System.Drawing.ColorTranslator.FromHtml("#007bff"));
                    range.Style.Font.Color.SetColor(System.Drawing.Color.White);
                }

                // Add data rows
                for (int i = 0; i < orders.Count; i++)
                {
                    worksheet.Cells[i + 2, 1].Value = orders[i].Code;
                    worksheet.Cells[i + 2, 2].Value = orders[i].CustomerName;
                    worksheet.Cells[i + 2, 3].Value = orders[i].Phone;
                    worksheet.Cells[i + 2, 4].Value = orders[i].Address;
                    worksheet.Cells[i + 2, 5].Value = orders[i].Mail;
                    worksheet.Cells[i + 2, 6].Value = orders[i].TotalAmount;
                    worksheet.Cells[i + 2, 7].Value = orders[i].Quantity;

                    // Convert TypePayment value to string
                    string typePaymentString;
                    switch (orders[i].TypePayment)
                    {
                        case 1:
                            typePaymentString = "Pending payment";
                            break;
                        case 2:
                            typePaymentString = "Paid";
                            break;
                        case 0:
                            typePaymentString = "Cancelled";
                            break;
                        default:
                            typePaymentString = "";
                            break;
                    }
                    worksheet.Cells[i + 2, 8].Value = typePaymentString;

                    worksheet.Cells[i + 2, 9].Value = orders[i].CreateDate.ToString("dd/MM/yyyy HH:mm:ss");

                    // Apply number format to "Total Amount" column
                    worksheet.Cells[i + 2, 6].Style.Numberformat.Format = "_(* #,##0_);_(* (#,##0);_(* \"-\"??_);_(@_)";
                }

                // Auto-fit columns


                // Add export date row
                int exportDateRowIndex = orders.Count + 3; // Add 3 to skip header row and data rows
                worksheet.Cells[exportDateRowIndex, 1].Value = "Exported on:";
                worksheet.Cells[exportDateRowIndex, 2].Value = DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss");

                // Set styling for export date row
                using (var range = worksheet.Cells[exportDateRowIndex, 1, exportDateRowIndex, 2])
                {
                    range.Style.Font.Bold = true;
                    range.Style.HorizontalAlignment = ExcelHorizontalAlignment.Right;


                }
                worksheet.Cells.AutoFitColumns();
                // Set the filename and content type for the response
                string filename = "Orders_" + DateTime.Now.ToString("dd/MM/yyyy") + ".xlsx";
                var contentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";

                // Return the Excel file as a byte array
                return File(package.GetAsByteArray(), contentType, filename);
            }
        }
        //public ActionResult ExportToPdf()
        //{
        //    var orders = db.Orders.ToList();

        //    // Create a new PDF document
        //    var document = new Document();

        //    // Add a new section to the document
        //    var section = document.AddSection();

        //    // Define column widths
        //    var codeColumnWidth = Unit.FromCentimeter(2);
        //    var customerNameColumnWidth = Unit.FromCentimeter(5);
        //    var phoneColumnWidth = Unit.FromCentimeter(3);
        //    var addressColumnWidth = Unit.FromCentimeter(7);
        //    var mailColumnWidth = Unit.FromCentimeter(6);
        //    var totalAmountColumnWidth = Unit.FromCentimeter(4);
        //    var quantityColumnWidth = Unit.FromCentimeter(3);
        //    var typePaymentColumnWidth = Unit.FromCentimeter(3);
        //    var dateCreatedColumnWidth = Unit.FromCentimeter(5);

        //    // Create a table with 9 columns
        //    var table = section.AddTable();
        //    table.AddColumn(codeColumnWidth);
        //    table.AddColumn(customerNameColumnWidth);
        //    table.AddColumn(phoneColumnWidth);
        //    table.AddColumn(addressColumnWidth);
        //    table.AddColumn(mailColumnWidth);
        //    table.AddColumn(totalAmountColumnWidth);
        //    table.AddColumn(quantityColumnWidth);
        //    table.AddColumn(typePaymentColumnWidth);
        //    table.AddColumn(dateCreatedColumnWidth);

        //    // Add header row
        //    var headerRow = table.AddRow();
        //    headerRow.Format.Font.Bold = true;
        //    headerRow.Cells[0].AddParagraph("Code");
        //    headerRow.Cells[1].AddParagraph("Customer Name");
        //    headerRow.Cells[2].AddParagraph("Phone");
        //    headerRow.Cells[3].AddParagraph("Address");
        //    headerRow.Cells[4].AddParagraph("Mail");
        //    headerRow.Cells[5].AddParagraph("Total Amount (VND)");
        //    headerRow.Cells[6].AddParagraph("Quantity");
        //    headerRow.Cells[7].AddParagraph("Type Payment");
        //    headerRow.Cells[8].AddParagraph("Date Created");

        //    // Add data rows
        //    foreach (var order in orders)
        //    {
        //        var row = table.AddRow();
        //        row.Cells[0].AddParagraph(order.Code);
        //        row.Cells[1].AddParagraph(order.CustomerName);
        //        row.Cells[2].AddParagraph(order.Phone);
        //        row.Cells[3].AddParagraph(order.Address);
        //        row.Cells[4].AddParagraph(order.Mail);
        //        row.Cells[5].AddParagraph(order.TotalAmount.ToString("#,##0"));
        //        row.Cells[6].AddParagraph(order.Quantity.ToString());
        //        string typePaymentString;
        //        switch (order.TypePayment)
        //        {
        //            case 1:
        //                typePaymentString = "Pending payment";
        //                break;
        //            case 2:
        //                typePaymentString = "Paid";
        //                break;
        //            case 0:
        //                typePaymentString = "Cancelled";
        //                break;
        //            default:
        //                typePaymentString = "";
        //                break;
        //        }
        //        row.Cells[7].AddParagraph(typePaymentString);
        //        row.Cells[8].AddParagraph(order.CreateDate.ToString("dd/MM/yyyy HH:mm:ss"));
        //    }

        //    // Set table borders
        //    table.SetEdge(0, 0, 9, orders.Count + 1, Edge.Box, BorderStyle.Single, 1.5);

        //    // Add export date row
        //    var exportDateParagraph = section.AddParagraph();
        //    exportDateParagraph.Format.Alignment = ParagraphAlignment.Right;
        //    exportDateParagraph.AddFormattedText("Exported on: ", TextFormat.Bold);
        //    exportDateParagraph.AddText(DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss"));

        //    // Set the filename and content type for the response
        //    string filename = "Orders_" + DateTime.Now.ToString("dd/MM/yyyy") + ".pdf";
        //    var contentType = "application/pdf";

        //    // Create a memory stream to hold the PDF data
        //    var stream = new MemoryStream();

        //    // Save the PDF document to the memory stream
        //    var renderer = new PdfDocumentRenderer();
        //    renderer.Document = document;
        //    renderer.RenderDocument();
        //    renderer.PdfDocument.Save(stream, false);

        //    // Reset the stream position to the beginning
        //    stream.Position = 0;

        //    // Return the PDF file as a FileStreamResult
        //    return new FileStreamResult(stream, contentType)
        //    {
        //        FileDownloadName = filename
        //    };
        //}
    }
}