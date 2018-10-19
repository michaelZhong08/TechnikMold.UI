using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TechnikSys.MoldManager.Domain.Entity;

namespace TechnikSys.MoldManager.Domain.Abstract
{
    public interface ISteelGroupProgramRepository
    {
        IQueryable<SteelGroupProgram> SteelGroupPrograms { get; }

        IEnumerable<SteelGroupProgram> QueryByID(int ID);

        int Save(SteelGroupProgram GroupProgram);

        SteelGroupProgram QueryByGroupName(string GroupName, int DrawingID);

        IEnumerable<SteelGroupProgram> QueryByNCID(int NCID);
    }
}
