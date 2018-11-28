using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TechnikMold.UI.Models.ViewModel
{
    public class PDReportViewModel
    {        
        public string 事业部 { get; set; }
        public string 成本中心 { get; set; }
        /// <summary>
        /// 制程分类
        /// </summary>
        public string 制程分类 { get; set; }
        /// <summary>
        /// 一级分类
        /// </summary>
        public string 一级分类 { get; set; }
        /// <summary>
        /// 二级分类
        /// </summary>
        public string 二级分类 { get; set; }
        /// <summary>
        /// 供应商全称
        /// </summary>
        public string 供应商全称 { get; set; }
        /// <summary>
        /// 模号
        /// </summary>
        public string 模号 { get; set; }
        /// <summary>
        /// 规格
        /// </summary>
        public string 规格 { get; set; }
        public double 数量 { get; set; }
        public string 单位 { get; set; }
        public double 含税单价 { get; set; }
        public double 含税总价 { get; set; }
        public double 税率 { get; set; }
        public double 未税总价 { get; set; }
        public string 订单 { get; set; }
        public string 请购单 { get; set; }
        public string 备注 { get; set; }
        public string 其它 { get; set; }
        public int PurchaseItemID { get; set; }
        public string 物料编号 { get; set; }
        public string 供应商 { get; set; }
        public DateTime 实际到达日期 { get; set; }
        public DateTime 生成时间 { get; set; }
        public DateTime 预计交货日期 { get; set; }
    }
}