using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TechnikSys.MoldManager.Domain.Entity;

namespace TechnikSys.MoldManager.Domain.Abstract
{
    public interface ISteel_GroupProgramme_listRepository
    {
        IQueryable<Steel_GroupProgramme_list> Steel_GroupProgramme_lists { get;  }
    }
}
