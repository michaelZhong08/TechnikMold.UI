/*
 * Create By:lechun1
 * 
 * Description:data represents part type
 * 
 */



using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TechnikSys.MoldManager.Domain.Entity
{
    public class PartType
    {
        public int PartTypeID { get; set; }
        public string Name { get; set; }
        public int Parent { get; set; }
    }
}
