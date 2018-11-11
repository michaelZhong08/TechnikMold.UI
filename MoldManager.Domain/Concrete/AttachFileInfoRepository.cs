using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TechnikSys.MoldManager.Domain.Abstract;
using TechnikSys.MoldManager.Domain.Entity;

namespace TechnikSys.MoldManager.Domain.Concrete
{
    public class AttachFileInfoRepository:IAttachFileInfoRepository
    {
        private EFDbContext _context = new EFDbContext();
        public IQueryable<AttachFileInfo> AttachFileInfos { get { return _context.AttachFileInfos; } }
        public int Save(AttachFileInfo model)
        {
            AttachFileInfo _attach = _context.AttachFileInfos.Where(t => t.ObjID == model.ObjID && t.ObjType == model.ObjType && t.FileName == model.FileName && t.FileType==model.FileType ).FirstOrDefault();
            if (_attach == null)
            {
                model.CreateTime = DateTime.Now;
                model.Enable = true;
                _context.AttachFileInfos.Add(model);
            }
            else
            {
                //_attach.FileStream = model.FileStream;
                _attach.Creator = model.Creator;
                _attach.CreateTime = DateTime.Now;
                _attach.FileSize = _attach.FileSize;
                _attach.Enable = true;
            }
            _context.SaveChanges();
            return 1;
        }
        public int Delete(AttachFileInfo model)
        {
            if (model.Enable == true)
            {
                model.Enable = false;
                _context.SaveChanges();
                return 1;
            }
            return -1;
        }
        public IQueryable<AttachFileInfo> GetAttachByObj(string ObjID,string ObjType)
        {
            return _context.AttachFileInfos.Where(t => t.ObjID == ObjID && t.ObjType == ObjType && t.Enable == true);
        }
        public AttachFileInfo GetAttachModel(string ObjID, string ObjType, string FileName, string FileType)
        {
            return _context.AttachFileInfos.Where(t => t.ObjID == ObjID && t.ObjType == ObjType && t.FileName== FileName && t.FileType== FileType && t.Enable == true).FirstOrDefault();
        }
    }
}
