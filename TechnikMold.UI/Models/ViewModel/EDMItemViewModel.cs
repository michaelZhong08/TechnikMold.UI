using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MoldManager.WebUI.Models.ViewModel
{
    public class EDMItemViewModel
    {
        public int ID { get; set; }
        public string ELEName { get; set; }
        public string LableName { get; set; }
        public string Position { get; set; }
        public double Gap { get; set; }
        public double OffsetX { get; set; }
        public double OffsetY { get; set; }
        public double OffsetZ { get; set; }
        public double OffsetC { get; set; }
        public double GapCompensate { get; set; }
        public double ZCompensate { get; set; }
        public string Surface { get; set; }
        public string Obit { get; set; }
        public string Material { get; set; }
        public string ElePoints { get; set; }
        public int EleType { get; set; }
        public double StockGap { get; set; }
        public int CNCMachMethod { get; set; }
    }
}