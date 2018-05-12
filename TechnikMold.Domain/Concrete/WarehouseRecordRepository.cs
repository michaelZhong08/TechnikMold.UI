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
    public class WarehouseRecordRepository:IWarehouseRecordRepository
    {
        private EFDbContext _context = new EFDbContext();
        public IQueryable<WarehouseRecord> WarehouseRecords
        {
            get { return _context.WarehouseRecords; }
        }

        public int Save(WarehouseRecord WarehouseRecord)
        {
            if (WarehouseRecord.WarehouseRecordID == 0)
            {
                
                _context.WarehouseRecords.Add(WarehouseRecord);
            }
            else
            {
                WarehouseRecord _dbEntry = _context.WarehouseRecords.Find(WarehouseRecord.WarehouseRecordID);
                if (_dbEntry != null)
                {
                    _dbEntry.UserID = WarehouseRecord.UserID;
                    _dbEntry.RecordType = WarehouseRecord.RecordType;
                    _dbEntry.Quantity = WarehouseRecord.Quantity;
                    _dbEntry.PurchaseOrderID = WarehouseRecord.PurchaseOrderID;
                    _dbEntry.POContentID = WarehouseRecord.POContentID;
                    _dbEntry.Date = DateTime.Now;
                    _dbEntry.Memo = WarehouseRecord.Memo;
                    _dbEntry.Name = WarehouseRecord.Name;
                    _dbEntry.Specification = WarehouseRecord.Specification;
                }
            }
            _context.SaveChanges();
            return WarehouseRecord.WarehouseRecordID;
        }
    }
}
