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
            #region UNC拼接
            string uncPanfu= _config.Value.Substring(0, _config.Value.IndexOf("\\"));
            SystemConfig _configPanfu = _context.SystemConfigs.Where(c => c.SettingName == uncPanfu).FirstOrDefault();
            if (_configPanfu != null)
            {
                string uncPath = _configPanfu.Value + _config.Value.Substring(_config.Value.IndexOf("\\"), _config.Value.Length - _config.Value.IndexOf("\\"));
                return uncPath;
            }
            return null;
            #endregion
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
