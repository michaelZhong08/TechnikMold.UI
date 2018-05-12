using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TechnikSys.MoldManager.Domain.Entity;

namespace TechnikSys.MoldManager.Domain.Abstract
{
    public interface IPOContentRepository
    {
        IQueryable<POContent> POContents { get; }

        int Save(POContent POContent);

        int Receive(int POContentID, int Quantity, string Memo);

        POContent QueryByPRContentID(int PRContentID);

        POContent QueryByID (int POContentID);

        IEnumerable<POContent> QueryByPOID(int PurchaseOrderID);

        void BatchCreate(List<POContent> POContents);

        void Delete(int POContentID);

    }
}
