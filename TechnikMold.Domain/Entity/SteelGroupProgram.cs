using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace TechnikSys.MoldManager.Domain.Entity
{
    public class SteelGroupProgram
    {

        public int SteelGroupProgramID { get; set; }
        public int NCID { get; set; }
        public string GroupName { get; set; }
        public double Time { get; set; }
        public bool Enabled { get; set; }

    }
}
