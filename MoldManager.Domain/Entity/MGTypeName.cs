using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TechnikSys.MoldManager.Domain.Entity
{
    public class MGTypeName
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public string Note { get; set; }
        public decimal Price { get; set; }
        public bool active { get; set; }
    }
}
