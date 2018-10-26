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
    /// 工时工作类型实体
    /// </summary>
    [Table("WH_WorkType")]
    public class WH_WorkType
    {
        [Key]
        public string WorkTypeCode { get; set; }
        public string ShortName { get; set; }
        public string Name { get; set; }
        public string Department { get; set; }
        public bool MoldNoRequired { get; set; }
        public bool TaskRequired { get; set; }
        public bool IsShared { get; set; }
        public string ShareRule { get; set; }
        public bool EquipmentRequired { get; set; }
        public bool ChkRequired { get; set; }
        public decimal Cost { get; set; }
        public bool Enabled { get; set; }
    }
}
