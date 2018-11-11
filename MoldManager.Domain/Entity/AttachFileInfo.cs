using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TechnikSys.MoldManager.Domain.Entity
{
    [Table("Relation_AttachFileInfo")]
    public class AttachFileInfo
    {
        [Key, Column(Order = 0)]
        public string ObjID { get; set; }
        [Key, Column(Order = 1)]
        public string ObjType { get; set; }
        [Key, Column(Order = 2)]
        public string FileName { get; set; }
        public string FileType { get; set; }
        public byte[] FileStream { get; set; }
        public string FilePath { get; set; }
        public DateTime CreateTime { get; set; }
        public bool Enable { get; set; }
        public string Creator { get; set; }
        public double FileSize { get; set; }
    }
}
