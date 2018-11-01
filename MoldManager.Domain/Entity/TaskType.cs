using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TechnikSys.MoldManager.Domain.Entity
{
    [Table("Base_TaskType")]
    public class TaskType
    {
        [Key]
        public int TaskID { get; set; }
        public string ShortName { get; set; }
        public string Name { get; set; }
        public string Des { get; set; }
        public int ParentID { get; set; }
        public bool Enable { get; set; }
    }
}
