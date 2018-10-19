using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TechnikSys.MoldManager.Domain.Abstract;
using TechnikSys.MoldManager.Domain.Entity;

namespace TechnikSys.MoldManager.Domain.Concrete
{
    public class SystemConfigRepository:ISystemConfigRepository
    {
        private EFDbContext _context = new EFDbContext();
        public IQueryable<SystemConfig> SystemConfigs
        {
            get { return _context.SystemConfigs; }
        }

        public string GetConfigValue(string Name)
        {
            SystemConfig _config = _context.SystemConfigs.Where(c => c.SettingName == Name).FirstOrDefault();
            return _config.Value;
        }


        public int Save(string Name, string Value)
        {
            SystemConfig _config = _context.SystemConfigs.Where(c => c.SettingName == Name).FirstOrDefault();
            if (_config == null)
            {
                _config = new SystemConfig();
                _config.SettingName = Name;
                _config.Value = Value;
                _context.SystemConfigs.Add(_config);
            }
            else
            {
                _config.Value = Value;
            }
            _context.SaveChanges();
            return _config.SystemConfigID;
        }
    }
}
