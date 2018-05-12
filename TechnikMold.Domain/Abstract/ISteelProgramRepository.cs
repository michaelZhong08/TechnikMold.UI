using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TechnikSys.MoldManager.Domain.Entity;

namespace TechnikSys.MoldManager.Domain.Abstract
{
    public interface ISteelProgramRepository
    {
        IQueryable<SteelProgram> SteelPrograms { get; }

        //IEnumerable<SteelProgram> QueryByID(int ID);

        IEnumerable<SteelProgram> QueryByGroupID(int GroupID);

        SteelProgram QueryByID(int ID);

        int Save(SteelProgram Program);

    }
}
