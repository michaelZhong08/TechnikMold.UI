using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TechnikSys.MoldManager.Domain.Entity;

namespace TechnikSys.MoldManager.Domain.Output
{
    public class MaterialHardness
    {
        public int MaterialID { get; set; }
        public int HardnessID { get; set; }
        public string MaterialName { get; set; }
        public string HardnessName { get; set; }
        public string FullValue { get; set; }

        public MaterialHardness()
        {
            MaterialID = 0;
            HardnessID = 0;
            MaterialName = "";
            HardnessName = "";
            FullValue = "";
        }

        public MaterialHardness(Material Material, Hardness Hardness)
        {

            MaterialID = Material.MaterialID;            
            MaterialName = Material.Name;
            if (Hardness != null)
            {
                HardnessID = Hardness.HardnessID;
                HardnessName = Hardness.Value;
                FullValue = MaterialName + "@" + HardnessName;
            }
            else
            {
                HardnessID = 0;
                HardnessName = "";
                FullValue = MaterialName + "@";
            }
            
        }
    }
}
