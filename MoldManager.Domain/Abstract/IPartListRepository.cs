using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TechnikSys.MoldManager.Domain.Entity;

namespace TechnikSys.MoldManager.Domain.Abstract
{
    public interface IPartListRepository
    {
        IQueryable<PartList> PartLists { get; }

        int Save(PartList PartList);

        PartList Query(int PartListID);

        IEnumerable<PartList> QueryByMoldNumber(string MoldNumber, bool Latest = true, int Version = -1);
    }
}
