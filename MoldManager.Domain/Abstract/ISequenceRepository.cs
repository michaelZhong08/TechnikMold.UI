using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TechnikSys.MoldManager.Domain.Entity;

namespace TechnikSys.MoldManager.Domain.Abstract
{
    public interface ISequenceRepository
    {
        IQueryable<Sequence> Sequences { get; }

        int Save(Sequence Sequence);

        string GetNextNumber(string Type);
    }
}
