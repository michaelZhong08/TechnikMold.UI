using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using TechnikSys.MoldManager.Domain.Entity;

namespace MoldManager.WebUI.Models.GridRowModel
{
    public class POContentGridRowModel
    {
        public string[] cell;
        public POContentGridRowModel(POContent POContent,PurchaseItem _puritem, string ETA, string PRNumber,double time)
        {
            cell = new string[14];
            cell[0] = POContent.POContentID.ToString();
            cell[1] = POContent.PartName;
            cell[2] = POContent.PartNumber;
            cell[3] = POContent.PartSpecification;
            cell[4] = POContent.Quantity.ToString();
            //计算数量
            cell[5] = _puritem.TaskID > 0 ? POContent.POContentID > 0 ? _puritem.Time.ToString() : time.ToString() : "1";
            cell[6] = _puritem.SupplierID == 0 ? "0" : _puritem.UnitPrice.ToString();
            //未税金额
            cell[7] = _puritem.SupplierID == 0 ? "0" : _puritem.TotalPrice.ToString();//Math.Round(POContent.Quantity * _puritem.UnitPrice, 2).ToString();
            cell[8] = _puritem.SupplierID == 0 ? "0" : _puritem.UnitPriceWT.ToString();
            //含税金额
            cell[9] = _puritem.SupplierID == 0 ? "0" : _puritem.TotalPriceWT.ToString();//Math.Round(POContent.Quantity * _puritem.UnitPriceWT, 2).ToString();

            cell[10] = POContent.RequireTime.ToString("yyyy-MM-dd");
            cell[11] = POContent.Memo;
            cell[12] = PRNumber;
            cell[13] = _puritem.PurchaseItemID.ToString();
        }
    }
}