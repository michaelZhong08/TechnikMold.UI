using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TechnikSys.MoldManager.Domain.Entity
{
    [Table("Relation_PhaseTaskType")]
    public class PhaseTaskType
    {
        [Key, Column(Order = 0)]
        public int PhaseID { get; set; }
        [Key, Column(Order = 1)]
        public int TaskID { get; set; }
        public bool Enable { get; set; }
    }
}
