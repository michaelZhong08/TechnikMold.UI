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
    public class QRQuotationRepository:IQRQuotationRepository
    {
        private EFDbContext _context = new EFDbContext();
        public IQueryable<QRQuotation> QRQuotations
        {
            get 
            {
                return _context.QRQuotations;
            }
        }

        public int Save(QRQuotation QRQuotation)
        {
            if (QRQuotation.QRQuotationID == 0)
            {
                _context.QRQuotations.Add(QRQuotation);
            }
            else
            {
                QRQuotation _dbEntry = _context.QRQuotations.Find(QRQuotation.QRQuotationID);
                if (_dbEntry != null)
                {
                    _dbEntry.QRContentID = QRQuotation.QRContentID;
                    _dbEntry.SupplierID = QRQuotation.SupplierID;
                    _dbEntry.QuotationRequestID = QRQuotation.QuotationRequestID;
                    _dbEntry.UnitPrice = QRQuotation.UnitPrice;
                    _dbEntry.TotalPrice = QRQuotation.TotalPrice;
                    _dbEntry.Quantity = QRQuotation.Quantity;
                    _dbEntry.QuotationDate = QRQuotation.QuotationDate;
                    _dbEntry.UnitPriceWT = QRQuotation.UnitPriceWT;
                    _dbEntry.TotalPriceWT = QRQuotation.TotalPriceWT;
                    _dbEntry.TaxRate = QRQuotation.TaxRate;
                    _dbEntry.Enabled = true;
                }
            }
            _context.SaveChanges();
            return QRQuotation.QRQuotationID;
        }


        public IEnumerable<QRQuotation> QueryByQRID(int QuotationRequestID)
        {
            IEnumerable<QRQuotation> _quotations = _context.QRQuotations.Where(p => p.QuotationRequestID == QuotationRequestID).Where(p => p.Enabled == true);
            return _quotations;
        }

        public void UpdateState(int QRQuotationID)
        {
            QRQuotation _dbEntry = _context.QRQuotations.Find(QRQuotationID);
            _dbEntry.Enabled = false;
            _context.SaveChanges();
        }




        public void Disable(int QuotationRequestID, int SupplierID)
        {
            IEnumerable<QRQuotation> _QRQuotations = _context.QRQuotations.Where(p => p.QuotationRequestID == QuotationRequestID).Where(p => p.SupplierID == SupplierID);
            foreach (QRQuotation _dbEntry in _QRQuotations)
            {
                _dbEntry.Enabled = false;
            }
            _context.SaveChanges();
        }
    }
}
