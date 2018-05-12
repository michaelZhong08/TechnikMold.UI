using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace TechnikSys.MoldManager.Domain.Entity
{
    public class SteelDrawingCADPart
    {
        [Key]
        public int SteelDrawingCADPartID { get; set; }
	    public string CADPartName { get; set; }
	    public int SteelDrawingID { get; set; }
        public bool active { get; set; }
    }
}
