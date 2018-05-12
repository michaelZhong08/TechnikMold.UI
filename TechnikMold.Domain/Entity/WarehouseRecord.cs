/*
 * Create By:lechun1
 * 
 * Description:
 * 
 */



using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TechnikSys.MoldManager.Domain.Entity
{
    public class WarehouseRecord
    {
        public int WarehouseRecordID { get; set; }
        public int UserID { get; set; }
        /// <summary>
        /// 仓库操作类型:
        /// 1:入库
        /// 2:出库
        /// 3:退货
        /// </summary>
        public int RecordType { get; set; }
        public int Quantity { get; set; }
        public int PurchaseOrderID { get; set; }
        public int POContentID { get; set; }
        public DateTime Date { get; set; }
        public string Memo { get; set; }
        public string Name { get; set; }
        public string Specification { get; set; }

        //public string ToString(string UserName, string PurchaseOrderNo="")
        //{
        //    StringBuilder _val = new StringBuilder();
        //    _val.Append(Date.ToString("yyyy-MM-dd hh:mm")+"   ");
        //    _val.Append(UserName);
        //    _val.Append(RecordType == 1 ? "入库" : "出库");
        //    _val.Append(Name + ",数量:" + Quantity);
        //    _val.Append(",备注:" + Memo);
        //    return _val.ToString();
        //}
    }
}
