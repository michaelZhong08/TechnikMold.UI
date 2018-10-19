using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TechnikSys.MoldManager.Domain.Entity;


namespace TechnikSys.MoldManager.Domain.Abstract
{
    public interface IPurchaseTypeRepository
    {
        IQueryable<PurchaseType> PurchaseTypes { get; }

        int Save(PurchaseType PurchaseType);

        List<PurchaseType> QueryByParentName(string ParentName, bool ContainParent = true);

        PurchaseType QueryByID(int PurchaseTypeID);

        IEnumerable<PurchaseType> QueryByParentID(int ParentID);

        PurchaseType QueryByName(string Name);

        List<PurchaseType> PurchaseTypeTree(int PurchaseTypeID = 0);

        void DeletePurchaseType(int PurchaseTypeID);
    }
}
