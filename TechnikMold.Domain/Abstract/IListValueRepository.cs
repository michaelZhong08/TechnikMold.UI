
/*
 * Create By:lechun1
 * 
 * Description: public interface for repository of TechnikSys.MoldManager.Domain.Entity.ListValue 
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
    public interface IListValueRepository
    {
        IQueryable<ListValue> ListValues { get; }

        int Save(ListValue ListValue);

        int Delete(int ListValueID);
    }
}
