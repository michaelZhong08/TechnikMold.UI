using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TechnikSys.MoldManager.Domain.Abstract;
using TechnikSys.MoldManager.Domain.Entity;

namespace TechnikSys.MoldManager.Domain.Concrete
{
    public class OutStockItemRepository:IOutStockItemRepository
    {
        private EFDbContext _context = new EFDbContext();

        public IQueryable<OutStockItem> OutStockItems
        {
            get { return _context.OutStockItems; }
        }

        public int Save(OutStockItem Item)
        {
            if (Item.OutStockItemID == 0)
            {
                Item.PartNumber=Item.PartNumber == null ? "" : Item.PartNumber;
                Item.Specification = Item.Specification == null ? "" : Item.Specification;

                Item.OutDate = DateTime.Now;
                _context.OutStockItems.Add(Item);
            }
            else
            {
                OutStockItem _dbEntry = _context.OutStockItems.Find(Item.OutStockItemID);
                if (_dbEntry != null)
                {
                    _dbEntry.WHRequestID        =Item.WHRequestID;
                    _dbEntry.PartName           =Item.PartName;
                    _dbEntry.PartNumber         = Item.PartNumber == null ? "" : Item.PartNumber;
                    _dbEntry.Specification      = Item.Specification == null ? "" : Item.Specification;
                    _dbEntry.Quantity           = Item.Quantity;
                    _dbEntry.OutStockFormID     = Item.OutStockFormID;
                    _dbEntry.OutDate            = DateTime.Now;
                    _dbEntry.WHStockID          = Item.WHStockID;
                    _dbEntry.MoldNumber = Item.MoldNumber;
                }
            }
            _context.SaveChanges();
            return Item.OutStockItemID;
        }

        public IEnumerable<OutStockItem> QueryByFormID(int FormID)
        {
            return _context.OutStockItems.Where(o => o.OutStockFormID == FormID);
        }

        public IEnumerable<OutStockItem> QueryByRequestID(int RequestID)
        {
            return _context.OutStockItems.Where(o => o.WHRequestID == RequestID);
        }
    }
}
