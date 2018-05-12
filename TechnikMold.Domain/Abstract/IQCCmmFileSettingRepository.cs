using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TechnikSys.MoldManager.Domain.Entity;

namespace TechnikSys.MoldManager.Domain.Abstract
{
    public interface IQCCmmFileSettingRepository
    {
        IQueryable<QCCmmFileSetting> QCCmmFileSettings { get; }

        int Save(QCCmmFileSetting QCCmmFileSetting);

        QCCmmFileSetting QueryByComputer(string ComputerName);
    }
}
