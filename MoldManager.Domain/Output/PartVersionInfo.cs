using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TechnikSys.MoldManager.Domain.Output
{
    public class PartVersionInfo
    {
        /// <summary>
        /// 零件ID
        /// </summary>
        public int PartID { get; set; }
        /// <summary>
        /// 模具ID
        /// </summary>
        public int ProjectID { get; set; }
        /// <summary>
        /// 零件名称（含版本）
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// 版本号
        /// </summary>
        public string Version { get; set; }

        /// <summary>
        /// true:可编辑 false:不可编辑
        /// </summary>
        public bool IsEdit { get; set; }
        public int PartListID { get; set; }
    }
}
