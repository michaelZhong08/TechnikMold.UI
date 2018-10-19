using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TechnikSys.MoldManager.Domain.Entity;

namespace TechnikSys.MoldManager.Domain.Abstract
{
    public interface ICNCMachInfoRepository
    {
        IQueryable<CNCMachInfo> CNCMachInfoes { get; }

        int Save(CNCMachInfo CNCMachInfo);

        CNCMachInfo QueryByELEIndex(int ELEIndex);

        CNCMachInfo QueryByNameVersion(string EleName, int Version);

        int GetNextDrawIndex();
    }
}
