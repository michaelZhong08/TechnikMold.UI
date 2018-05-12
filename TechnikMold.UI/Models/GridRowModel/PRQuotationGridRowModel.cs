using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using TechnikSys.MoldManager.Domain.Entity;

namespace MoldManager.WebUI.Models.GridRowModel
{
    public class PRQuotationGridRowModel
    {
        public string[] cell;

        public PRQuotationGridRowModel(PRContent PRContent, string[] Quotations)
        {
            cell = new string[3 + Quotations.Length];
            cell[0] = PRContent.PartID.ToString();
            cell[1] = PRContent.PartName;
            cell[2] = PRContent.PartNumber;
            for (int i = 0; i < Quotations.Length; i++)
            {
                cell[2 + i] = Quotations[i];
            }
        }
    }
}