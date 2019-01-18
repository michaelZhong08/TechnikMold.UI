using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TechnikSys.MoldManager.Domain.Abstract;
using TechnikSys.MoldManager.Domain.Entity;

namespace TechnikSys.MoldManager.Domain.Concrete
{
    public class WH_TaskPeriodTypeRepository:IWH_TaskPeriodTypeRepository
    {
        private EFDbContext _context = new EFDbContext();
        public IQueryable<WH_TaskPeriodType> WH_TaskPeriodTypes
        {
            get
            {
                return _context.WH_TaskPeriodType;
            }            
        }
        public int Save(WH_TaskPeriodType model)
        {
            WH_TaskPeriodType _dbentry = _context.WH_TaskPeriodType.Where( h=> h.Name.ToUpper() == model.Name.ToUpper()).FirstOrDefault();
            if (_dbentry == null)
            {
                _dbentry = new WH_TaskPeriodType();
                _dbentry.Code = model.Code;
                _dbentry.Name = model.Name;
                _dbentry.Dep = model.Dep;
                _dbentry.Cost = model.Cost;
                _dbentry.Enabled = true;
                _dbentry.ContainEmp = model.ContainEmp;
                _context.WH_TaskPeriodType.Add(_dbentry);
            }
            else
            {
                _dbentry.Dep = model.Dep;
                _dbentry.Cost = model.Cost;
                _dbentry.ContainEmp = model.ContainEmp;
                _dbentry.Enabled = model.Enabled;
            }
            _context.SaveChanges();
            return 0;
        }
    }
}
