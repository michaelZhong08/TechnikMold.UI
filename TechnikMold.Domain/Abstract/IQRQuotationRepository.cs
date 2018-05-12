
/*
 * Create By:lechun1
 * 
 * Description: public interface for repository of TechnikSys.MoldManager.Domain.Entity.PRQuotation 
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
    public interface IQRQuotationRepository
    {
        IQueryable<QRQuotation> QRQuotations { get; }

        int Save(QRQuotation QRQuotation);

        IEnumerable<QRQuotation> QueryByQRID(int QuotationRequestID);

        void Disable(int QuotationRequestID, int SupplierID);
    }
}
