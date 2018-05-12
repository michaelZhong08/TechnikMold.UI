using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using TechnikSys.MoldManager.Domain.Entity;

namespace MoldManager.WebUI.Models.GridRowModel
{
    public class PartGridRowModel
    {
        public string[] cell;

        public PartGridRowModel(int Index, Part Part,int Stock=0)
        {
            
            cell = new string[19];
            string _indexNo = "";
            if (Index < 10)
            {
                _indexNo = "00" + Index.ToString();
            }
            else if (Index<100)
            {
                _indexNo = "0" + Index.ToString();
            }
            else
            {
                _indexNo = Index.ToString();
            }
            cell[0] = _indexNo;
            cell[1] = Part.PartID.ToString();
            cell[2] = Part.Name;
            cell[3] = Part.PartNumber;
            cell[4] = Part.Specification;
            cell[5] = Part.MaterialName;
            cell[6] = Part.Quantity.ToString();
            cell[7] = Part.AppendQty.ToString();
            cell[8] = Stock.ToString();
            cell[9] = Part.Hardness;
            cell[10]= Part.BrandName;
            cell[11]= Part.DetailDrawing.ToString();
            cell[12] = Part.BriefDrawing.ToString();
            cell[13] = Part.PurchaseDrawing.ToString();
            cell[14] = Part.ExtraMaching.ToString();
            cell[15] = Part.Memo;
            cell[16] = Part.CreateDate.ToString("yyyy-MM-dd");   
            cell[17] = Part.FromUG.ToString();
            cell[18] = Part.InPurchase.ToString();
        }
    }
}