using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using TechnikSys.MoldManager.Domain.Entity;
using MoldManager.WebUI.Models.Helpers;


namespace MoldManager.WebUI.Models.GridRowModel
{
    public class PRListGridRowModel
    {
        public string[] cell;

        public PRListGridRowModel(PurchaseRequest PurchaseRequest, string UserName, 
            string PurchaseType, string Department,string SubmitUser, string ReviewUser,string ApprovalFullName,bool ERPPartStatus)
        {
            cell = new string[13];
            cell[0] = PurchaseRequest.PurchaseRequestID.ToString();
            cell[1] = PurchaseRequest.PurchaseRequestNumber;
            cell[2] = UserName;
            cell[3] = Department;
            cell[4] = PurchaseRequest.CreateDate.ToString("yyyy-MM-dd");
            cell[5] = SubmitUser;
            cell[6] = ReviewUser;
            cell[7] = PurchaseRequest.ApprovalDate==new DateTime(1900,1,1)?"-": PurchaseRequest.ApprovalDate.ToString("yyyy-MM-dd");
            cell[8] = PurchaseType;
            cell[9] = Enum.GetName(typeof(PurchaseRequestStatus), PurchaseRequest.State);
            cell[10] = PurchaseRequest.Memo;
            cell[11] = ApprovalFullName;
            cell[12] = ERPPartStatus.ToString();
        }
    }
}