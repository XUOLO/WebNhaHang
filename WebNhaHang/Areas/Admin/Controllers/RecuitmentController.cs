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
    [Authorize(Roles = "Admin")]
    public class RecuitmentController : Controller
    {
        // GET: Admin/Recuitment

        private ApplicationDbContext db = new ApplicationDbContext();
        public ActionResult Index(string SearchString, int? page)
        {
             
            if (page == null)
            {
                page = 1;

            }
            IEnumerable<Aplication> items = db.Aplications.OrderByDescending(x => x.CreateDate).ToList();

            if (!string.IsNullOrEmpty(SearchString))
            {
                items = items.Where(x => x.Code.Contains(SearchString) || x.FullName.Contains(SearchString) || x.Phone.Contains(SearchString) || x.Email.Contains(SearchString)).ToList();
            }
            var pageNumber = page ?? 1;
            var pageSize = 10;
            ViewBag.Page = pageNumber;
            ViewBag.PageSize = pageSize;
            return View(items.ToPagedList(pageNumber, pageSize));
        }
        public ActionResult Reports(string ReportType)
        {
            LocalReport localreport = new LocalReport();
            localreport.ReportPath = Server.MapPath("~/report/ReportRecruitment.rdlc");

            ReportDataSource reportDataSource = new ReportDataSource();
            reportDataSource.Name = "DataSetRecruitment";
            reportDataSource.Value = db.Aplications.ToList();
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
            renderedByte = localreport.Render(reportType,"",out mimeType, out encoding, out fileNameExtension,out streams,out warnings);
            Response.AddHeader("content-disposition", "attachment;filename= Recruitment_report." + fileNameExtension);
            return File(renderedByte, fileNameExtension);
 
        }


    }
}