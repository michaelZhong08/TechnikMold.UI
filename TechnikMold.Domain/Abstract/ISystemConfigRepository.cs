﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TechnikSys.MoldManager.Domain.Entity;

namespace TechnikSys.MoldManager.Domain.Abstract
{
    public interface ISystemConfigRepository
    {
        IQueryable<SystemConfig> SystemConfigs { get; }

        string GetConfigValue(string Name);

        int Save(string Name, string Value);
    }
}
