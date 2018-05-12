using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using TechnikSys.MoldManager.Domain.Entity;

namespace MoldManager.WebUI.Models.GridRowModel
{
    public class PurchaseRequestGridRowModel
    {
        public string[] cell;

        public PurchaseRequestGridRowModel(PurchaseRequest PurchaseRequest)
        {
            cell = new string[10];
            cell[0] = PurchaseRequest.PurchaseRequestID.ToString();
            cell[1] = PurchaseRequest.PurchaseRequestNumber;
        }
    }
}