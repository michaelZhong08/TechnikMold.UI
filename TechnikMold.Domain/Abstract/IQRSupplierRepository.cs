
/*
 * Create By:lechun1
 * 
 * Description: public interface for repository of TechnikSys.MoldManager.Domain.Entity.Supplier 
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
    public interface IQRSupplierRepository
    {
        IQueryable<QRSupplier> QRSuppliers { get; }

        int Save(QRSupplier QRSupplier);

        IEnumerable<QRSupplier> QueryByQRID(int QuotationRequestID);

        void Quotation(int QuotationRequestID, int SupplierID, DateTime QuotationDate, DateTime ValidDate, double TaxRate, int TaxInclude, int ContactID);

        void Delete(int QRSupplierID);

        QRSupplier Query(int QuotationRequestID, int SupplierID);

    }
}
