/*
 * Create By:lechun1
 * 
 * Description:Data represents part brand
 * 
 */



using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TechnikSys.MoldManager.Domain.Entity
{
    public class Brand
    {
        public int BrandID { get; set; }
        public string Name { get; set; }
        public bool Enabled { get; set; }
        /// <summary>
        /// 模具材料;生产耗材
        /// </summary>
        public string Type { get; set; }
    }
}
