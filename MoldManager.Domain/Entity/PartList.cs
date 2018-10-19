using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TechnikSys.MoldManager.Domain.Entity
{
    public class PartList
    {
        DateTime createDate= new DateTime(1900, 1, 1);
        DateTime releaseDate = new DateTime(1900, 1, 1);
        public int PartListID { get; set; }
        public string MoldNumber { get; set; }
        public int Version { get; set; }
        public bool Released { get; set; }
        public bool Enabled { get; set; }
        public int PrevVersion { get; set; }
        public bool Latest { get; set; }
        public int ProjectID { get; set; }
        /// <summary>
        /// 创建时间
        /// </summary>
        public DateTime CreateDate
        {
            get
            {
                return createDate;
            }

            set
            {
                createDate = value==null? new DateTime(1900, 1, 1): value;
            }
        }
        /// <summary>
        /// 发布时间
        /// </summary>
        public DateTime ReleaseDate
        {
            get
            {
                return releaseDate;
            }

            set
            {
                releaseDate = value == null ? new DateTime(1900, 1, 1) : value;
            }
        }

        public PartList()
        {
            PartListID = 0;
            MoldNumber = "";
            Version = 1;
            Released = false;
            Enabled = true;
            PrevVersion = 0;
            Latest = true;
            ProjectID = 0;
            CreateDate = createDate;
            ReleaseDate = releaseDate;
        }
    }
}
