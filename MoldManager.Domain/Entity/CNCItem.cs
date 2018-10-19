/*
 * Create By:lechun1
 * 
 * Description:
 * 
 */



using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TechnikSys.MoldManager.Domain.Entity
{
    public class CNCItem
    {
        public int CNCItemID { get; set; }
        public int TaskID { get; set; }
        public string LabelName { get; set; }
        public string Material { get; set; }
        public double OffsetX { get; set; }
        public double OffsetY { get; set; }
        public double OffsetZ { get; set; }
        public double OffsetC { get; set; }
        public double GapCompensation { get; set; }
        public double ZCompensation { get; set; }
        public int Status { get; set; }
        public DateTime CreateTime { get; set; }
        public DateTime CNCStartTime { get; set; }
        public DateTime CNCFinishTime { get; set; }
        public string CNCMachine { get; set; }
        public bool Destroy { get; set; }
        public DateTime DestroyTime { get; set; }
        public double Gap { get; set; }
        public bool EleType { get; set; }
        public double HeightMax { get; set; }
        public double HeightMin { get; set; }
        public double GapMax { get; set; }
        public double GapMin { get; set; }
        public bool LabelToPrint { get; set; }
        public bool LabelPrinted { get; set; }
        public double SafetyHeight { get; set; }
        public int ELE_INDEX { get; set; }
        public bool Ready { get; set; }
        public bool Required { get; set; }
        public bool Finished { get; set; }
        public string PartPosition { get; set; }
        public int CNCMachMethod { get; set; }
        public string MoldNumber { get; set; }
        public DateTime QCFinishTime { get; set; }

        public CNCItem()
        {
            CNCItemID = 0;
            TaskID = 0;
            LabelName = "";
            Material = "";
            OffsetX = 0;
            OffsetY = 0;
            OffsetZ = 0;
            OffsetC = 0;
            GapCompensation = 0;
            ZCompensation = 0;
            Status = 0;
            CreateTime = DateTime.Now;
            CNCStartTime = new DateTime(1900, 1, 1);
            CNCFinishTime = new DateTime(1900, 1, 1);
            CNCMachine = "";
            Destroy = false;
            DestroyTime = new DateTime(1900, 1, 1);
            Gap = 0;
            EleType = true;
            HeightMax = 0;
            HeightMin = 0;
            GapMax = 0;
            GapMin = 0;
            LabelToPrint = false;
            LabelPrinted = false;
            SafetyHeight = 0;
            ELE_INDEX = 0;
            Ready = false;
            Required = true;
            Finished = false;
            PartPosition = "";
            CNCMachMethod = 0;
            QCFinishTime = new DateTime(1900, 1, 1);
        }
    }


}
