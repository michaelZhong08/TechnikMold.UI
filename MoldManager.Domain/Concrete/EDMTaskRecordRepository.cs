using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TechnikSys.MoldManager.Domain.Abstract;
using TechnikSys.MoldManager.Domain.Entity;

namespace TechnikSys.MoldManager.Domain.Concrete
{
    public class EDMTaskRecordRepository:IEDMTaskRecordRepository
    {

        private EFDbContext _context = new EFDbContext();

        public IEnumerable<EDMTaskRecord> EDMTaskRecords
        {
            get { return _context.EDMTaskRecords; }
        }

        public int Save(EDMTaskRecord EDMTaskRecord)
        {
            if (EDMTaskRecord.EDMTaskRecordID == 0)
            {
                _context.EDMTaskRecords.Add(EDMTaskRecord);
            }
            else
            {
                EDMTaskRecord _dbEntry = _context.EDMTaskRecords.Find(EDMTaskRecord.EDMTaskRecordID);
                if (_dbEntry != null)
                {
                    _dbEntry.TaskID = EDMTaskRecord.TaskID;
                    _dbEntry.ElectrodeName = EDMTaskRecord.ElectrodeName;
                    _dbEntry.EDMPartName = EDMTaskRecord.EDMPartName;
                    _dbEntry.Finished = EDMTaskRecord.Finished;
                }
            }
            return EDMTaskRecord.EDMTaskRecordID;
        }

        public IEnumerable<EDMTaskRecord> QueryByTaskID(int TaskID)
        {
            return _context.EDMTaskRecords.Where(e => e.TaskID == TaskID);
        }


        
    }
}
