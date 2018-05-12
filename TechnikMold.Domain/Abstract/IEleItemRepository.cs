using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TechnikSys.MoldManager.Domain.Entity;

namespace TechnikSys.MoldManager.Domain.Abstract
{
    public interface IEleItemRepository
    {
        IQueryable<EleItem> EleItems { get; }

        int Save(EleItem EleItem);

        IEnumerable<EleItem> QueryByTaskID(int TaskID);

        IEnumerable<EleItem> QueryByEDMItemID(int EDMItemID);

        EleItem GetByID(int EleItemID);
    }
}
