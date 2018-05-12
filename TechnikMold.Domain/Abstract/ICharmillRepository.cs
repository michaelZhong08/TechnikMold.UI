using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TechnikSys.MoldManager.Domain.Entity;

namespace TechnikSys.MoldManager.Domain.Abstract
{
    public interface ICharmillRepository
    {
        IQueryable<Charmill> Charmills { get; }

        int QueryProgram(string Name, string Type, string Surface, string Material, double Gap, string Obit = "");
    }
}
