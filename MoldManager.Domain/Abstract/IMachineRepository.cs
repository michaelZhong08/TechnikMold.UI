using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TechnikSys.MoldManager.Domain.Entity;

namespace TechnikSys.MoldManager.Domain.Abstract
{
    public interface IMachineRepository
    {
        IQueryable<Machine> Machines { get; }

        int Save(Machine Machine);

        Machine QueryByName(string Name);

        Machine QueryByID(int MachineID);

        int Delete(int MachineID);
        List<Machine> GetMachinesByTaskType(int TaskType);

    }
}
