using Microsoft.Reporting.WebForms;
using PagedList;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebNhaHang.Models;

namespace WebNhaHang.Areas.Admin.Controllers
{
    public class RecuitmentController : Controller
    {
        // GET: Admin/Recuitment

        private ApplicationDbContext db = new ApplicationDbContext();
        public ActionResult Index(int? page)
        {
            var items = db.Aplications.OrderByDescending(x => x.CreateDate).ToList();
            if (page == null)
            {
                page = 1;

            }
            var pageNumber = page ?? 1;
            var pageSize = 2;
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