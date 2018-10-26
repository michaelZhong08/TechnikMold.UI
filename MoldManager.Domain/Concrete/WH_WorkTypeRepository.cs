using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TechnikSys.MoldManager.Domain.Abstract;
using TechnikSys.MoldManager.Domain.Entity;

namespace TechnikSys.MoldManager.Domain.Concrete
{
    public class WH_WorkTypeRepository:IWH_WorkTypeRepository
    {
        private EFDbContext _context = new EFDbContext();
        public IQueryable<WH_WorkType> WH_WorkTypes
        {
            get
            {
                return _context.WH_WorkType;
            }
        }
        public int Save(WH_WorkType model)
        {
            WH_WorkType _dbentry = _context.WH_WorkType.Where(h => h.Name.ToUpper() == model.Name.ToUpper()).FirstOrDefault();
            if (_dbentry == null)
            {
                _dbentry = new WH_WorkType();
                _dbentry.WorkTypeCode = model.WorkTypeCode;
                _dbentry.Name = model.Name;
                _dbentry.ShortName = model.ShortName;
                _dbentry.Department = model.Department;
                _dbentry.MoldNoRequired = model.MoldNoRequired;
                _dbentry.TaskRequired = model.TaskRequired;
                _dbentry.IsShared = model.IsShared;
                _dbentry.ShareRule = model.ShareRule;
                _dbentry.EquipmentRequired = model.EquipmentRequired;
                _dbentry.ChkRequired = model.ChkRequired;
                _dbentry.Cost = model.Cost;
                _dbentry.Enabled = true;
                _context.WH_WorkType.Add(_dbentry);
            }
            else
            {
                _dbentry.ShortName = model.ShortName;
                _dbentry.Department = model.Department;
                _dbentry.MoldNoRequired = model.MoldNoRequired;
                _dbentry.TaskRequired = model.TaskRequired;
                _dbentry.IsShared = model.IsShared;
                _dbentry.ShareRule = model.ShareRule;
                _dbentry.EquipmentRequired = model.EquipmentRequired;
                _dbentry.ChkRequired = model.ChkRequired;
                _dbentry.Cost = model.Cost;
                _dbentry.Enabled = model.Enabled;
            }
            _context.SaveChanges();
            return 0;
        }
    }
}
