using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TechnikSys.MoldManager.Domain.Entity
{
    public class QCCmmFileSetting
    {
        public int QCCmmFileSettingID { get; set; }
        public string ComputerName { get; set; }
        public string FileAddress { get; set; }
        public string BackupDir { get; set; }
        public string TemplatePath { get; set; }
        public string SteelTemplatePath { get; set; }
        public string COMIndex { get; set; }
    }
}
