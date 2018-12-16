using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TechnikSys.MoldManager.Domain.Entity;

namespace TechnikSys.MoldManager.Domain.Abstract
{
    public interface ISupplierGroupRepository
    {
        IQueryable<SupplierGroup> SupplierGroup { get; }
        int Save(SupplierGroup model);
        List<SupplierGroup> QuerySGList();
        SupplierGroup QueryByID(int _sgID);
        List<Supplier> QuerySuppliersByGroupID(int _groupID);
        int Delete(int _sgID);
        SupplierGroup QueryByName(string _sgName);
    }
}
