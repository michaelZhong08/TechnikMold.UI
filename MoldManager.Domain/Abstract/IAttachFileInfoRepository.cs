using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TechnikSys.MoldManager.Domain.Entity;

namespace TechnikSys.MoldManager.Domain.Abstract
{
    public interface IAttachFileInfoRepository
    {
        IQueryable<AttachFileInfo> AttachFileInfos { get; }
        int Save(AttachFileInfo model);
        int Delete(AttachFileInfo model);
        IQueryable<AttachFileInfo> GetAttachByObj(string ObjID, string ObjType);
        AttachFileInfo GetAttachModel(string ObjID, string ObjType, string FileName, string FileType);
    }
}
