using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TechnikSys.MoldManager.Domain.Status;

namespace TechnikSys.MoldManager.Domain.Entity
{
    public class WarehouseRequest
    {
        public int WarehouseRequestID { get; set; }
        public string RequestNumber { get; set; }
        //public string MoldNumber { get; set; }
        public int RequestUserID { get; set; }
        public int WarehouseUserID { get; set; }
        public int ApprovalUserID { get; set; }
        public DateTime CreateDate { get; set; }
        public DateTime ApprovalDate { get; set; }
        public DateTime WarehouseDate { get; set; }
        public int State { get; set; }
        public bool Enabled { get; set; }


        public WarehouseRequest()
        {
            WarehouseRequestID = 0;
            RequestNumber = "";
            //MoldNumber = "";
            RequestUserID = 0;
            WarehouseUserID = 0;
            ApprovalUserID = 0;
            CreateDate = DateTime.Now;
            ApprovalDate = new DateTime(1900, 1,1 );
            WarehouseDate=new DateTime(1900,1, 1);
            State=(int)WarehouseRequestStatus.编辑;
            Enabled = true;
        }
        
    }
}
