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
    public class CostCenter
    {
        public int CostCenterID { get; set; }
        public string Name { get; set; }
        public bool Enabled { get; set; }
    }
}
