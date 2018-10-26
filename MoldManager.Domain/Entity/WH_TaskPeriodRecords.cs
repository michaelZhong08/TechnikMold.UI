using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TechnikSys.MoldManager.Domain.Entity
{
    public class WH_TaskPeriodRecord
    {
        [Key ,Column(Order = 0)]
        public int TaskHourID { get; set; }
        [Key, Column(Order = 1)]
        public string TypeCode { get; set; }
        public decimal Time { get; set; }
        public decimal Cost { get; set; }
        public bool Enabled { get; set; }

    }
}
