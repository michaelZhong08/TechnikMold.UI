using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TechnikSys.MoldManager.Domain.Entity;

namespace TechnikSys.MoldManager.Domain.Abstract
{
    public interface IMachinesInfoRepository
    {
        IQueryable<MachinesInfo> MachinesInfo { get; }

        int Save(MachinesInfo model);
        MachinesInfo GetMInfoByCode(string MachineCode);
        MachinesInfo GetMInfoByKeyWord(string KeyWord);
        string CheckExistMachinesInfo(MachinesInfo model);
        int IsNullMachinesInfo(MachinesInfo model);
        string GenerateCode(string TaskType);
        List<MachinesInfo> GetMInfoByTaskType(int TaskType);
    }
}
