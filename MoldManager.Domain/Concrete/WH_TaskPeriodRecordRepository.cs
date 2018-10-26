using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TechnikSys.MoldManager.Domain.Abstract;
using TechnikSys.MoldManager.Domain.Entity;

namespace TechnikSys.MoldManager.Domain.Concrete
{
    public class WH_TaskPeriodRecordRepository:IWH_TaskPeriodRecordRepository
    {
        private EFDbContext _contexts = new EFDbContext();
        public IQueryable<WH_TaskPeriodRecord> WH_TaskPeriodRecords
        {
            get
            {
                return _contexts.WH_TaskPeriodRecords;
            }
        }
        public int Save(WH_TaskPeriodRecord model)
        {
            WH_TaskPeriodRecord _dbentry = _contexts.WH_TaskPeriodRecords.Where(h => h.TaskHourID == model.TaskHourID && h.TypeCode == model.TypeCode).FirstOrDefault();
            if (_dbentry == null)
            {
                model.Enabled = true;
                _contexts.WH_TaskPeriodRecords.Add(model);
            }
            else
            {
                _dbentry.Time = model.Time;
                _dbentry.Cost = model.Cost;
            }
            _contexts.SaveChanges();
            return 0;
        }
    }
}
