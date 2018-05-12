using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TechnikSys.MoldManager.Domain.Entity;
using TechnikSys.MoldManager.Domain.Abstract;


namespace TechnikSys.MoldManager.Domain.Concrete
{
    public class ReturnItemRepository:IReturnItemRepository
    {
        private EFDbContext _context = new EFDbContext();
        public IQueryable<ReturnItem> ReturnItems
        {
            get { return _context.ReturnItems; }
        }

        public int Save(ReturnItem ReturnItem)
        {
            ReturnItem _dbEntry;
            bool _isNew = false;
            if (ReturnItem.ReturnItemID == 0)
            {
                _dbEntry = QueryByPOItem(ReturnItem.PurchaseItemID);
                if (_dbEntry == null)
                {
                    _isNew = true;
                    ReturnItem.Memo = ReturnItem.Memo == null ? "" : ReturnItem.Memo;
                    _context.ReturnItems.Add(ReturnItem);
                }
                else
                {
                    _dbEntry.Name = ReturnItem.Name;
                    _dbEntry.MaterialNumber = ReturnItem.MaterialNumber;
                    _dbEntry.Specification = ReturnItem.Specification;
                    _dbEntry.Quantity = ReturnItem.Quantity;
                    _dbEntry.ReturnRequestID = ReturnItem.ReturnRequestID;
                    _dbEntry.WarehouseItemID = ReturnItem.WarehouseItemID;
                    _dbEntry.PurchaseItemID = ReturnItem.PurchaseItemID;
                    _dbEntry.Enabled = ReturnItem.Enabled;
                    _dbEntry.Memo = ReturnItem.Memo == null ? "" : ReturnItem.Memo;
                }
                
            }
            else
            {
                _dbEntry = _context.ReturnItems.Find(ReturnItem.ReturnItemID);
                if (_dbEntry != null)
                {
                    _dbEntry.Name = ReturnItem.Name;
                    _dbEntry.MaterialNumber = ReturnItem.MaterialNumber;
                    _dbEntry.Specification = ReturnItem.Specification;
                    _dbEntry.Quantity = ReturnItem.Quantity;
                    _dbEntry.ReturnRequestID = ReturnItem.ReturnRequestID;
                    _dbEntry.WarehouseItemID = ReturnItem.WarehouseItemID;
                    _dbEntry.PurchaseItemID = ReturnItem.PurchaseItemID;
                    _dbEntry.Memo = ReturnItem.Memo==null?"":ReturnItem.Memo;
                    _dbEntry.Enabled = ReturnItem.Enabled;
                }
            }
            _context.SaveChanges();
            if (_isNew)
            {
                return ReturnItem.ReturnItemID;
            }
            else
            {
                return _dbEntry.ReturnItemID;
            }
        }

        public void Delete(int ReturnItemID)
        {
            ReturnItem _item = QueryByID(ReturnItemID);
            _item.Enabled = false;
            _context.SaveChanges();
        }

        public ReturnItem QueryByPOItem(int PurchaseItemID)
        {
            return _context.ReturnItems.Where(r => r.PurchaseItemID == PurchaseItemID).Where(r => r.Enabled == true).FirstOrDefault();
        }

        public ReturnItem QueryByID(int ReturnItemID)
        {
            return _context.ReturnItems.Find(ReturnItemID);
        }



        public IEnumerable<ReturnItem> QueryByRequest(int ReturnRequestID)
        {
            return _context.ReturnItems.Where(r => r.ReturnRequestID == ReturnRequestID).Where(r=>r.Enabled==true);
        }
    }
}
