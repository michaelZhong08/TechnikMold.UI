using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TechnikSys.MoldManager.Domain.Entity
{
    /// <summary>
    /// 备库类型表
    /// </summary>
    public class StockType
    {
        public int StockTypeID { get; set; }
        public string Name { get; set; }
        public bool Enabled { get; set; }
    }
}
