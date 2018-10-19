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
    public class QCInfo
    {
        public int QCInfoID { get; set; }
        public int TaskID { get; set; }
        public int ItemID { get; set; }
        public string QCPoints { get; set; }
    }
}
