using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TechnikSys.MoldManager.Domain.Entity
{
    [Table("MachinesInfo")]
    public class MachinesInfo
    {
        [Key]
        public string MachineCode { get; set; }
        public string MachineName { get; set; }
        public string EquipBrand { get; set; }
        public int DepartmentID { get; set; }
        public string TaskType { get; set; }
        public string Stype { get; set; }
        public double Capacity { get; set; }
        /// <summary>
        /// 计划停机时间
        /// </summary>
        public decimal Downtime { get; set; }
        /// <summary>
        ///正常 1 检修 -1 损坏 -99
        /// </summary>
        public int Status { get; set; }
        public decimal Cost { get; set; }
        public bool IsActive { get; set; }
    }
}
