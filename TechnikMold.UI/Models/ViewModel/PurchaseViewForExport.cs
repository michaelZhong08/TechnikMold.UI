using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TechnikSys.MoldManager.UI.Models.ViewModel
{
    public class PurchaseViewForExport
    {

        public string 行号 { get; set; } = "";
        public string 单据号_FBillno { get; set; } = "";
        public string 单据号_FTrantype { get; set; } = "";
        public string 物料代码_FNumber { get; set; } = "";
        public string 物料代码_FName { get; set; } = "";
        public string 物料代码_FModel { get; set; } = "";
        public string 辅助属性_FNumber { get; set; } = "";
        public string 辅助属性_FName { get; set; } = "";
        public string 辅助属性_FClassName { get; set; } = "";
        public string 单位_FNumber { get; set; } = "";
        public string 单位_FName { get; set; } = "";
        public string 数量 { get; set; } = "";
        public string 建议采购日期 { get; set; } = "";
        public string BOM编号 { get; set; } = "";
        public string 用途 { get; set; } = "";
        public string 基本单位数量 { get; set; } = "";
        public string 供应商_FNumber { get; set; } = "";
        public string 供应商_FName { get; set; } = "";
        public string 到货日期 { get; set; } = "";
        public string 计划订单号 { get; set; } = "";
        public string 换算率 { get; set; } = "";
        public string 辅助数量 { get; set; } = "";
        public string 源单单号 { get; set; } = "";
        public string 源单类型 { get; set; } = "";
        public string 源单内码 { get; set; } = "";
        public string 源单分录 { get; set; } = "";
        public string MRP计算标记 { get; set; } = "";
        public string 计划模式_FID { get; set; } = "";
        public string 计划模式_FName { get; set; } = "";
        public string 计划模式_FTypeID { get; set; } = "";
        public string 计划跟踪号 { get; set; } = "";
        public string 是否询价 { get; set; } = "";
        public string BOM类別_FID { get; set; } = "";
        public string BOM类別_FName { get; set; } = "";
        public string BOM类別_FTypeID { get; set; } = "";
        public string 模具号 { get; set; } = "";
        public string 零件号 { get; set; } = "";
        public string 订单BOM行号 { get; set; } = "";
        public string 规格 { get; set; } = "";

        public string 备注1 { get; set; } = "";
        public string 件数 { get; set; } = "";
        public string 到货日期2 { get; set; } = "";
    }
}