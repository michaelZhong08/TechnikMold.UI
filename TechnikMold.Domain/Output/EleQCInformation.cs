using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace TechnikSys.MoldManager.Domain.Output
{
    public class EleQCInformation
    {
        public string ElectrodeName { get; set; }
        public double Clearance { get; set; }
        public double X { get; set; }
        public double Y { get; set; }
        public string PartPath { get; set; }
        public string XYZFileName { get; set; }
        public string PartName3D { get; set; }
        public double Gap { get; set; }
        public int CNCMachMethod { get; set; }
        public string LabelName { get; set; }

        public EleQCInformation()
        {
            ElectrodeName = "";
            Clearance = 0;
            X = 0;
            Y = 0;
            PartPath = "";
            XYZFileName = "";
            PartName3D = "";
            Gap = 0;
            CNCMachMethod = 0;
            LabelName = "";
        }
    }
}
