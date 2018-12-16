using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TechnikSys.MoldManager.Domain.Entity
{
    public class PurItemChangeDateRecord
    {
        public int Id { get; set; }
        public int PurchaseItemID { get; set; }
        public DateTime PlanAJDate { get; set; }
        [NotMapped]
        public string PlanAJDateStr { get; set; }
        public string UserName { get; set; }
        public DateTime CreDate { get; set; }
        [NotMapped]
        public string CreDateStr { get; set; }
        public string Memo { get; set; }
    }
}
