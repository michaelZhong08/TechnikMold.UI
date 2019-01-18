using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TechnikSys.MoldManager.Domain.Entity
{
    /// <summary>
    /// 任务阶段类型实体
    /// </summary>
    [Table("WH_TaskPeriodType")]
    public class WH_TaskPeriodType
    {
        [Key]
        public string Code { get; set; }
        public string Name { get; set; }
        public string Dep { get; set; }
        public decimal Cost { get; set; }
        public bool Enabled { get; set; }
        public bool ContainEmp { get; set; }
    }
}
