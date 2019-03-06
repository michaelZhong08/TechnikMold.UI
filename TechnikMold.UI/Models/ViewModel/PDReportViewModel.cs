using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using TechnikMold.UI.Models.Attribute;

namespace TechnikMold.UI.Models.ViewModel
{
    public class PDReportViewModel
    {
        [ExcelFieldAttribute(1)]
        public string 下单日期 { get; set; }
        public string 事业部 { get; set; }
        [ExcelFieldAttribute(2)]
        public string 成本中心 { get; set; }
        [ExcelFieldAttribute(5)]
        /// <summary>
        /// 制程分类
        /// </summary>
        public string 制程分类 { get; set; }
        [ExcelFieldAttribute(3)]
        /// <summary>
        /// 一级分类
        /// </summary>
        public string 一级分类 { get; set; }
        [ExcelFieldAttribute(4)]
        /// <summary>
        /// 二级分类
        /// </summary>
        public string 二级分类 { get; set; }
        [ExcelFieldAttribute(6)]
        public string 采购类型 { get; set; }
        [ExcelFieldAttribute(7)]
        public string 供应商编码 { get; set; }
        /// <summary>
        /// 供应商全称
        /// </summary>
        public string 供应商全称 { get; set; }
        [ExcelFieldAttribute(8)]
        /// <summary>
        /// 模号
        /// </summary>
        public string 模号 { get; set; }
        [ExcelFieldAttribute(9)]
        /// <summary>
        /// 规格
        /// </summary>
        public string 规格 { get; set; }
        [ExcelFieldAttribute(10)]
        public double 数量 { get; set; }
        [ExcelFieldAttribute(11)]
        public string 单位 { get; set; }
        [ExcelFieldAttribute(12)]
        public double 含税单价 { get; set; }
        public double 含税总价 { get; set; }
        [ExcelFieldAttribute(14)]
        public double 税率 { get; set; }
        [ExcelFieldAttribute(13)]
        public double 未税单价 { get; set; }
        public double 未税总价 { get; set; }
        [ExcelFieldAttribute(15)]
        public string 订单 { get; set; }
        [ExcelFieldAttribute(16)]
        public string 请购单 { get; set; }
        [ExcelFieldAttribute(17)]
        public string 税组 { get; set; }
        [ExcelFieldAttribute(18)]
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