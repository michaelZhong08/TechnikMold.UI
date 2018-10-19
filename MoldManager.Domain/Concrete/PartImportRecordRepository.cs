using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TechnikSys.MoldManager.Domain.Abstract;
using TechnikSys.MoldManager.Domain.Entity;

namespace TechnikSys.MoldManager.Domain.Concrete
{
    public class PartImportRecordRepository:IPartImportRecordRepository
    {
        private EFDbContext _context = new EFDbContext();

        public IQueryable<PartImportRecord> PartImportRecords
        {
            get { return _context.PartImportRecords; }
        }

        public int Save(String Data, int PartID)
        {
            PartImportRecord _record = new PartImportRecord();
            _record.DataContent=Data;
            _record.ImportDate=DateTime.Now;
            _record.PartID = PartID;
            _context.PartImportRecords.Add(_record);
            _context.SaveChanges();
            return _record.PartImportRecordID;
        }
    }
}
