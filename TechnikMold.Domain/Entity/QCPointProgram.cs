using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TechnikSys.MoldManager.Domain.Entity
{
    public class QCPointProgram
    {
        public int QCPointProgramID { get; set; }
        public string ElectrodeName { get; set; }
        public int Rev { get; set; }
        public bool LatestFlat { get; set; }
        public int Clearance { get; set; }
        public double X { get; set; }
        public double Y { get; set; }
        public string PartPath { get; set; }
        public string XYZFlieName { get; set; }
        public string PartName3D { get; set; }
        public string Part3D { get; set; }
        public int Part3DRev { get; set; }
        public string CreateBy { get; set; }
        public DateTime CreateDate { get; set; }
        public string CreateComputer { get; set; }
        public bool Enabled { get; set; }
        public int OldID { get; set; }
    }
}
