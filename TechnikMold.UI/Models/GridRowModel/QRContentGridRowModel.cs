using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using TechnikSys.MoldManager.Domain.Entity;

namespace MoldManager.WebUI.Models.GridRowModel
{
    public class QRContentGridRowModel
    {
        public string[] cell;

        public QRContentGridRowModel(PRContent PRContent)
        {
            cell = new string[12];
            cell[0] = "";
            cell[1] = PRContent.PartName;
            cell[2] = PRContent.PartNumber;
            cell[3] = PRContent.Quantity.ToString();
            cell[4] = PRContent.PartSpecification;
            cell[5] = PRContent.MaterialName;
            cell[6] = PRContent.Hardness;
            cell[7] = PRContent.BrandName;
            cell[8] = PRContent.PurchaseDrawing.ToString();
            cell[9] = PRContent.PRContentID.ToString();
            cell[10] = PRContent.PurchaseItemID.ToString();
            cell[11] = PRContent.RequireTime == new DateTime(1900, 1, 1) ? "-" : PRContent.RequireTime.ToString("yyyy-MM-dd");
        }

        public QRContentGridRowModel(QRContent QRContent)
        {
            cell = new string[12];
            cell[0] = QRContent.QRContentID.ToString();
            cell[1] = QRContent.PartName;
            cell[2] = QRContent.PartNumber;
            cell[3] = QRContent.Quantity.ToString();
            cell[4] = QRContent.PartSpecification;
            cell[5] = QRContent.MaterialName;
            cell[6] = QRContent.Hardness;
            cell[7] = QRContent.BrandName;
            cell[8] = QRContent.PurchaseDrawing.ToString();
            cell[9] = QRContent.PRContentID.ToString();
            cell[10] = QRContent.PurchaseItemID.ToString();
            cell[11] = QRContent.RequireDate == new DateTime(1900, 1, 1) ? "-" : QRContent.RequireDate.ToString("yyyy-MM-dd");
        }

        public QRContentGridRowModel(PurchaseItem Item, PRContent PRContent)
        {
            cell = new string[12];
            cell[0] = "";
            cell[1] = Item.Name;
            cell[2] = Item.PartNumber;
            cell[3] = Item.Quantity.ToString();
            cell[4] = Item.Specification;
            cell[5] = Item.Material;
            cell[6] = PRContent==null?"":PRContent.Hardness;
            cell[7] = PRContent==null?"":PRContent.BrandName;
            cell[8] = PRContent == null ? "" : PRContent.PurchaseDrawing.ToString();
            cell[9] = PRContent.PRContentID.ToString();
            cell[10] = Item.PurchaseItemID.ToString();
            cell[11] = Item.RequireTime == new DateTime(1900, 1, 1) ? "-" : Item.RequireTime.ToString("yyyy-MM-dd");
        }


    }
}