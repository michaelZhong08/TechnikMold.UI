using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TechnikSys.MoldManager.Domain.Entity;

namespace TechnikSys.MoldManager.Domain.Abstract
{
    public interface ISteel_Programme_listRepository
    {
        IQueryable<Steel_Programme_list> Steel_Programme_list { get; }
    }
}
