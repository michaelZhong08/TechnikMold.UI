using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TechnikSys.MoldManager.Domain.WebServiceModel
{
    public class POMailModel
    {
        public int POContentID { get; set; }
        public int PurchaseItemID { get; set; }
        public string PartName { get; set; }
        public string PartNum { get; set; }
        public string Specification { get; set; }
        public int Qty { get; set; }
        public decimal UnitPrice { get; set; }
        public decimal Total { get; set; }
        public string RequestTime { get; set; }
        public string Memo { get; set; }

        public string POAttachFilePath { get; set; }
        public string ItemAttachPath { get; set; }
        /// <summary>
        /// 密送地址
        /// </summary>
        public string SecretMailAddr { get; set; }
        /// <summary>
        /// 主题
        /// </summary>
        public string Subject { get; set; }
    }
}
