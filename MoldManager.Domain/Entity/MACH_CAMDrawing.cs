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
    public class MACH_CAMDrawing
    {
        public int ID{ get; set; }
        public string DrawingName{ get; set; }
        public string MoldName{ get; set; }

        //field name is lock
        public bool lock_tag{ get; set; }
        public DateTime CreateDate{ get; set; }
        public string CreateBy{ get; set; }
        public DateTime ReleaseDate{ get; set; }
        public string ReleaseBy{ get; set; }
        public bool active{ get; set; }
    }
}
