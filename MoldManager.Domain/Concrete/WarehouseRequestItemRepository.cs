using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TechnikSys.MoldManager.Domain.Abstract;
using TechnikSys.MoldManager.Domain.Entity;

namespace TechnikSys.MoldManager.Domain.Concrete
{
    public class WarehouseRequestItemRepository:IWarehouseRequestItemRepository
    {
        private EFDbContext _context = new EFDbContext();
        public IQueryable<WarehouseRequestItem> WarehouseRequestItems
        {
            get { return _context.WarehouseRequestItems; }
        }

        public int Save(WarehouseRequestItem Item)
        {
            //bool _isNew = false;
            WarehouseRequestItem _dbEntry = _context.WarehouseRequestItems.Find(Item.WarehouseRequestItemID);
            if (_dbEntry==null)
            {
                    _context.WarehouseRequestItems.Add(Item);
            }
            else
            {                
                //_dbEntry.WarehouseRequestID = Item.WarehouseRequestID;
                //_dbEntry.PartName = Item.PartName;
                //_dbEntry.PartNumber = Item.PartNumber;
                //_dbEntry.Specification = Item.Specification;
                //_dbEntry.PartID = Item.PartID;                
                _dbEntry.Quantity = Item.Quantity;
                _dbEntry.ReceivedQuantity = Item.ReceivedQuantity;
                _dbEntry.ReceiveDate = Item.ReceiveDate;
                _dbEntry.Received = Item.Received;
                _dbEntry.WarehouseStockID = Item.WarehouseStockID;
                _dbEntry.ShortQty = Item.ShortQty;
            }
            _context.SaveChanges();
            
                return Item.WarehouseRequestItemID;
            
        }

        public int Receive(int WarehouseRequestItemID, int Quantity)
        {
            WarehouseRequestItem _item = _context.WarehouseRequestItems.Find(WarehouseRequestItemID);
            _item.ReceivedQuantity = _item.ReceivedQuantity+Quantity;
            _context.SaveChanges();
            return _item.WarehouseRequestID;
        }





        public IEnumerable<WarehouseRequestItem> QueryByRequestID(int WarehouseRequestID)
        {
            return _context.WarehouseRequestItems.Where(w => w.WarehouseRequestID == WarehouseRequestID);
        }

        public WarehouseRequestItem QueryByName(string PartName, string PartNumber)
        {
            WarehouseRequestItem _item = _context.WarehouseRequestItems.Where(w => w.PartName == PartName)
                .Where(w => w.PartNumber == PartNumber).FirstOrDefault();
            return _item;
        }




        public WarehouseRequestItem QueryByID(int WarehouseRequestItemID)
        {
            return _context.WarehouseRequestItems.Find(WarehouseRequestItemID);
        }
    }
}
