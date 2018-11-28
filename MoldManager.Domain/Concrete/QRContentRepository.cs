using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TechnikSys.MoldManager.Domain.Abstract;
using TechnikSys.MoldManager.Domain.Entity;

namespace TechnikSys.MoldManager.Domain.Concrete
{
    public class QRContentRepository:IQRContentRepository
    {
        private EFDbContext _context = new EFDbContext();
        public IQueryable<QRContent> QRContents
        {
            get { return _context.QRContents; }
        }

        public int Save(QRContent QRContent)
        {
            if (QRContent.QRContentID == 0)
            {
                QRContent.SupplierID = -1;
                _context.QRContents.Add(QRContent);
            }
            else
            {
                QRContent _dbEntry = _context.QRContents.Find(QRContent.QRContentID);
                if (_dbEntry != null)
                {
                    _dbEntry.PRContentID = QRContent.PRContentID;
                    _dbEntry.PartID = QRContent.PartID;
                    _dbEntry.PartName = QRContent.PartName;
                    _dbEntry.PartNumber = QRContent.PartNumber;
                    _dbEntry.PartSpecification = QRContent.PartSpecification;
                    _dbEntry.Quantity = QRContent.Quantity;
                    _dbEntry.PurchaseRequestID = QRContent.PurchaseRequestID;
                    _dbEntry.QuotationRequestID = QRContent.QuotationRequestID;
                    _dbEntry.MaterialName = QRContent.MaterialName;
                    _dbEntry.Hardness = QRContent.Hardness;
                    _dbEntry.BrandName = QRContent.BrandName;
                    _dbEntry.PurchaseDrawing = QRContent.PurchaseDrawing;
                    _dbEntry.Memo = QRContent.Memo;
                    _dbEntry.Enabled = QRContent.Enabled;
                    _dbEntry.SupplierID = QRContent.SupplierID;
                    _dbEntry.QRcMemo = QRContent.QRcMemo;
                    _dbEntry.RequireDate = QRContent.RequireDate;
                }
            }
            _context.SaveChanges();
            return QRContent.QRContentID;
        }

        public void Delete(int QRContentID)
        {
            QRContent _dbEntry = GetByID(QRContentID);
            if (_dbEntry != null)
            {
                _dbEntry.Enabled = false;
            }
            _context.SaveChanges();
        }

        public void DeleteByQRID(int QRID)
        {
            IEnumerable<QRContent> _contents = QueryByQRID(QRID);
            foreach (QRContent _content in _contents)
            {
                _content.Enabled = false;
            }
            _context.SaveChanges();
        }

        public QRContent GetByID(int QRContentID)
        {
            return _context.QRContents.Find(QRContentID);
        }

        public IEnumerable<QRContent> QueryByQRID(int QuotationRequestID)
        {
            IEnumerable<QRContent> _contents = _context.QRContents.Where(q => q.QuotationRequestID == QuotationRequestID).Where(q=>q.Enabled==true);
            return _contents;
        }

        public IEnumerable<QRContent> Query(string Keyword)
        {
            string _keyword= Keyword.ToLower();
            IEnumerable<QRContent> _contents = _context.QRContents.Where(q => q.PartName.ToLower().Contains(_keyword))
                .Union(_context.QRContents.Where(q => q.PartNumber.ToLower().Contains(_keyword)))
                .Union(_context.QRContents.Where(q => q.PartName.ToLower().Contains(_keyword)))
                .Where(q => q.Enabled == true);
            return _contents;
        }
    }
}
