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
    public class Steel_GroupProgramme_list
    {
        public int ID { get; set; }
        public int NCID { get; set; }
        public string GroupName { get; set; }
        public double Time { get; set; }
        public bool activ { get; set; }
    }
}
