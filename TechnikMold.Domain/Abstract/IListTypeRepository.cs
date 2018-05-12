
/*
 * Create By:lechun1
 * 
 * Description: public interface for repository of TechnikSys.MoldManager.Domain.Entity.ListType 
 * 
 */

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TechnikSys.MoldManager.Domain.Entity;
namespace TechnikSys.MoldManager.Domain.Abstract
{
    public interface IListTypeRepository
    {
        IQueryable<ListType> ListTypes { get; }

        int Save(ListType ListType);

        int Delete(int ListTypeID);
    }
}
