using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using TechnikSys.MoldManager.Domain.Entity;

namespace MoldManager.WebUI.Models.ViewModel
{
    public class QuotationFileViewModel
    {
        public int QuotationFileID { get; set; }
        public string SupplierName { get; set; }
        public string UploadDate { get; set; }
        public string FileLink { get; set; }

        public QuotationFileViewModel(QuotationFile File, string Supplier)
        {
            QuotationFileID = File.QuotationFileID;
            SupplierName = Supplier;
            UploadDate = File.CreateDate.ToString("yyyy-MM-dd");
            FileLink = "/Purchase/GetQuotationFile?QuotationFileID=" + File.QuotationFileID;
        }
    }
}