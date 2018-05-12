using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TechnikSys.MoldManager.Domain.Entity
{
    public class SteelProgram
    {
        public int SteelProgramID { get; set; }
        public int GroupID { get; set; }
        public string ProgramName { get; set; }
        public string FileName { get; set; }
        public string ToolName { get; set; }
        public double Time { get; set; }
        public double Depth { get; set; }
        public bool HaveFile { get; set; }
        public int Sequence { get; set; }
        public bool Enabled { get; set; }

    }
}
