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
    public class Sequence
    {
        public int SequenceID { get; set; }
        public string Name { get; set; }
        public int Current { get; set; }
        public string NameConvension { get; set; }
    }
}
