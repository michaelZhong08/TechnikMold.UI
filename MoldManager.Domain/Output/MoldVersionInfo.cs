using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TechnikSys.MoldManager.Domain.Output
{
    public class MoldVersionInfo
    {
        /// <summary>
        /// 模具编号
        /// </summary>
        public string MoldNumber { get; set; }
        /// <summary>
        /// 模具ID
        /// </summary>
        public int ProjectID { get; set; }
        /// <summary>
        /// 版本号
        /// </summary>
        public int Version { get; set; }

        /// <summary>
        /// true：可编辑 false：不可编辑
        /// </summary>
        public bool IsEdit { get; set; }
        /// <summary>
        /// BomID
        /// </summary>
        public int PartListID { get; set; }
        /// <summary>
        /// 创建时间
        /// </summary>
        public DateTime CreateDate { get; set; }
        /// <summary>
        /// 发布时间
        /// </summary>
        public DateTime ReleaseDate { get; set; }
        /// <summary>
        /// 更新日期
        /// </summary>
        public DateTime UpdateDate { get; set; }
    }
}
