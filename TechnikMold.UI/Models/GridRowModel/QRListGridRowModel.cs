using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using MoldManager.WebUI.Models.GridRowModel;
using TechnikSys.MoldManager.Domain.Entity;
using TechnikSys.MoldManager.Domain.Abstract;
using MoldManager.WebUI.Models.Helpers;

namespace MoldManager.WebUI.Models.GridRowModel
{
    public class QRListGridRowModel
    {
        public string[] cell;
        public QRListGridRowModel(QuotationRequest Request,string ProjectNumber,  string MoldNumber, string User, string Suppliers,
            string QuotedSuppliers)
        {
            cell = new string[8];
            cell[0] = Request.QuotationRequestID.ToString();
            cell[1] = Request.QuotationNumber;
            cell[2] = User;
            cell[3] = Request.CreateDate.ToString("yyyy-MM-dd");
            cell[4] = Suppliers;
            cell[5] = QuotedSuppliers;
            cell[6] = Request.DueDate == new DateTime(1900, 1, 1) ? "" : Request.DueDate.ToString("yyyy-MM-dd");
            cell[7] = Enum.GetName(typeof(QuotationRequestStatus), Request.State);

        }
    }
}