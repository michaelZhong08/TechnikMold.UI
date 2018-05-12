using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using TechnikSys.MoldManager.Domain.Entity;
using TechnikSys.MoldManager.Domain.Abstract;
using TechnikSys.MoldManager.Domain.Status;


namespace MoldManager.WebUI.Models.GridRowModel
{
    public class ReturnRequestGridRowModel
    {
        public string[] cell;

        public ReturnRequestGridRowModel(ReturnRequest Request,
            IPurchaseOrderRepository PORepository, 
            IUserRepository UserRepository)
        {
            cell = new string[10];
            cell[0] = Request.ReturnRequestID.ToString();
            cell[1] = Request.ReturnRequestNumber;
            cell[2] = PORepository.QueryByID(Request.PurchaseOrderID).PurchaseOrderNumber;
            cell[3] = Request.SupplierName;
            cell[4] = Enum.GetName(typeof(ReturnRequestStatus), Request.State);
            cell[5] = Request.CreateDate.ToString("yyyy-MM-dd HH:mm");
            cell[6] = UserRepository.GetUserByID(Request.WarehouseUserID).FullName;
            cell[7] = Request.ApprovalDate == new DateTime(1900, 1, 1) ? "" : Request.ApprovalDate.ToString("yyyy-MM-dd  HH:mm");
            try
            {
                cell[8] = UserRepository.GetUserByID(Request.ApprovalUserID).FullName;
            }
            catch
            {
                cell[8] = "";
            }
            cell[9] = Request.ReturnDate == new DateTime(1900, 1, 1) ? "" : Request.ReturnDate.ToString("yyyy-MM-dd  HH:mm");
            
            
        }
    }
}