
/*
 * Create By:lechun1
 * 
 * Description: public interface for repository of TechnikSys.MoldManager.Domain.Entity.PRContent 
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
    public interface IPRContentRepository
    {
        IQueryable<PRContent> PRContents { get; }

        int Save(PRContent PRContent);

        IQueryable<PRContent> QueryByName(string Name);

        IQueryable<PRContent> QueryBySpecification(string Specification);

        IQueryable<PRContent> QueryByRequestID(int PurchaseRequestID);

        PRContent QueryByID(int PRContentID);

        void Delete(int PRContentID);

    }
}
