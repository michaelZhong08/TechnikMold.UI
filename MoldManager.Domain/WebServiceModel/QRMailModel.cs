using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TechnikSys.MoldManager.Domain.WebServiceModel
{
    public class QRMailModel
    {
        public int QRContent { get; set; }
        public int PurchaseItemID { get; set; }
        public string PartName { get; set; }
        public string PartNum { get; set; }        
        
        public string Specification { get; set; }
        public string Materials { get; set; }
        public string Hardness { get; set; }
        public string Brand { get; set; }
        public int Qty { get; set; }
        public string unit { get; set; }
        /// <summary>
        /// 附图
        /// </summary>
        public string IsAttach { get; set; }
        public string RequiredDate { get; set; }
        /// <summary>
        /// 备注
        /// </summary>
        public string Memo { get; set; }
        public string QRAttachFilePath { get; set; }
        public string ItemAttachPath { get; set; }
        /// <summary>
        /// 密送地址
        /// </summary>
        public string SecretMailAddr { get; set; }
        /// <summary>
        /// 主题
        /// </summary>
        public string Subject { get; set; }
        public string QRcMemo { get; set; }
    }
}
