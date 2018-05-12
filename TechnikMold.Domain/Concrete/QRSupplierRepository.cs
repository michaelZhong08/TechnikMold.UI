/*
 * Create By:lechun1
 * 
 * Description:
 * 
 */



using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TechnikSys.MoldManager.Domain.Abstract;
using TechnikSys.MoldManager.Domain.Entity;



namespace TechnikSys.MoldManager.Domain.Concrete
{
    public class QRSupplierRepository:IQRSupplierRepository
    {
        private EFDbContext _context = new EFDbContext();


        public IQueryable<QRSupplier> QRSuppliers
        {
            get 
            {
                return _context.QRSuppliers;
            }
        }

        public int Save(QRSupplier QRSupplier)
        {
            if (QRSupplier.QRSupplierID == 0)
            {
                QRSupplier _dbEntry = _context.QRSuppliers.Where(q => q.QuotationRequestID == QRSupplier.QuotationRequestID)
                    .Where(q => q.SupplierID == QRSupplier.SupplierID).FirstOrDefault();
                if (_dbEntry == null)
                {
                    QRSupplier.QuotationState = false;
                    QRSupplier.RequestDate = DateTime.Now;
                    QRSupplier.Enabled = true;
                    _context.QRSuppliers.Add(QRSupplier);
                }
                else
                {
                    _dbEntry.QuotationRequestID = QRSupplier.QuotationRequestID;
                    _dbEntry.SupplierID = QRSupplier.SupplierID;
                    _dbEntry.RequestDate = QRSupplier.RequestDate;
                    _dbEntry.TaxInclude = QRSupplier.TaxInclude;
                    _dbEntry.TaxRate = QRSupplier.TaxRate;
                    _dbEntry.Enabled = true;
                }
                
            }
            else
            {
                QRSupplier _dbEntry = _context.QRSuppliers.Find(QRSupplier.QRSupplierID);
                if (_dbEntry != null)
                {
                    _dbEntry.QuotationRequestID = QRSupplier.QuotationRequestID;
                    _dbEntry.SupplierID = QRSupplier.SupplierID;
                    _dbEntry.RequestDate = QRSupplier.RequestDate;
                    _dbEntry.TaxRate = QRSupplier.TaxRate;
                    _dbEntry.Enabled = true;
                }
            }
            _context.SaveChanges();
            return QRSupplier.QRSupplierID;
        }

        public IEnumerable<QRSupplier> QueryByQRID(int QuotationRequestID)
        {
            IEnumerable<QRSupplier> _QRSuppliers = _context.QRSuppliers.Where(p => p.QuotationRequestID == QuotationRequestID)
                .Where(p=>p.Enabled==true);
            return _QRSuppliers;
        }


        /// <summary>
        /// Update the quotation state field to verify whether the quotation is input or not
        /// </summary>
        /// <param name="PurchaseRequestID"></param>
        /// <param name="SupplierID"></param>
        public void Quotation(int QuotationRequestID, 
            int SupplierID, 
            DateTime QuotationDate, 
            DateTime ValidDate, 
            double TaxRate, 
            int TaxInclude, 
            int ContactID)
        {
            QRSupplier _dbEntry = _context.QRSuppliers.Where(p => p.SupplierID == SupplierID).Where(p => p.QuotationRequestID == QuotationRequestID).FirstOrDefault();
            if (_dbEntry != null)
            {
                _dbEntry.QuotationState = true;
                _dbEntry.QuotationDate = QuotationDate;
                _dbEntry.ValidDate = ValidDate;
                _dbEntry.TaxRate = TaxRate;
                
                if (TaxInclude == 0)
                {
                    _dbEntry.TaxInclude = false;
                }
                else
                {
                    _dbEntry.TaxInclude = true;
                }
                _dbEntry.ContactID = ContactID;                
            }
            _context.SaveChanges();
        }


        public void Delete(int QRSupplierID)
        {
            QRSupplier _qrSupplier = _context.QRSuppliers.Find(QRSupplierID);
            _qrSupplier.Enabled = false;
            _context.SaveChanges();
        }


        public QRSupplier Query(int QuotationRequestID, int SupplierID)
        {
            return _context.QRSuppliers.Where(q => q.QuotationRequestID == QuotationRequestID)
                .Where(q => q.SupplierID == SupplierID).Where(q => q.Enabled == true).FirstOrDefault();
        }
    }
}
