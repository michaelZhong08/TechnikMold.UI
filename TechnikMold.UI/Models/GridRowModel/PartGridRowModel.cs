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
            
            cell = new string[22];
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
            cell[2] = Part.ShortName==null?"" : Part.ShortName;
            cell[3] = Part.Version.ToString();
            cell[4] = Part.PartNumber;
            cell[5] = Part.Specification;
            cell[6] = Part.JobNo;
            cell[7] = Part.MaterialName;
            cell[8] = Part.Quantity.ToString();
            cell[9] = Part.AppendQty.ToString();
            cell[10] = Stock.ToString();
            cell[11] = Part.Hardness;
            cell[12] = Part.BrandName;
            cell[13] = Part.DetailDrawing.ToString();
            cell[14] = Part.BriefDrawing.ToString();
            cell[15] = Part.PurchaseDrawing.ToString();
            cell[16] = Part.ExtraMaching.ToString();
            cell[17] = Part.Memo;
            cell[18] = Part.CreateDate.ToString("yyyy-MM-dd");
            cell[19] = Part.FromUG.ToString();
            cell[20] = Part.InPurchase.ToString();
            cell[21] = Part.ERPPartID==null?"": Part.ERPPartID;
        }
    }
}