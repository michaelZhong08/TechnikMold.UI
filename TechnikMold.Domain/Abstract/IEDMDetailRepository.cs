using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TechnikSys.MoldManager.Domain.Entity;

namespace TechnikSys.MoldManager.Domain.Abstract
{
    public interface IEDMDetailRepository
    {
        IQueryable<EDMDetail> EDMDetails { get; }
        
        int Save(EDMDetail EDMDetail);

        EDMDetail QueryByTaskID(int TaskID);

        EDMDetail QueryBySetting(string SettingName, int Version);

        int AddEDMDetail(EDMDetail EDMDetail);

        void Lock(int TaskID, string TaskName, int Version);

    }
}
