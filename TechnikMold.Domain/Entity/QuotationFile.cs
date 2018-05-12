using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TechnikSys.MoldManager.Domain.Entity
{
    public class QuotationFile
    {
        public int QuotationFileID { get; set; }
        public int SupplierID { get; set; }
        public int QuotationRequestID { get; set; }
        public string FileName { get; set; }
        public string SysFileName { get; set; }
        public DateTime CreateDate { get; set; }
        public bool Enabled { get; set; }
        public string MimeType { get; set; }

        public QuotationFile()
        {
            QuotationFileID = 0;
            SupplierID = 0;
            QuotationRequestID = 0;
            FileName = "";
            SysFileName = "";
            CreateDate = DateTime.Now;
            Enabled = true;
            MimeType = "";
        }

        public QuotationFile(int RequestID, int Supplier, string File, string Mime)
        {

            QuotationFileID = 0;
            SupplierID = Supplier;
            QuotationRequestID = RequestID;
            FileName = File;
            SysFileName = Math.Abs((File + "yyyyMMddHHmm").GetHashCode()).ToString();
            CreateDate = DateTime.Now;
            Enabled = true;
            MimeType = Mime;
        }
    }
}
