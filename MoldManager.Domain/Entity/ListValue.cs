/*
 * Create By:lechun1
 * 
 * Description:data represent 
 * 
 */



using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TechnikSys.MoldManager.Domain.Entity
{
    public class ListValue
    {
        public int ListValueID { get; set; }
        public int ListTypeID { get; set; }
        public string Name { get; set; }
        public bool Enabled { get; set; }
    }
}
