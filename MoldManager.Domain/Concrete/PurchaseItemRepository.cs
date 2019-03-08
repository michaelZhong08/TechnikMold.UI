using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TechnikSys.MoldManager.Domain.Abstract;
using TechnikSys.MoldManager.Domain.Entity;
using TechnikSys.MoldManager.Domain.Status;

namespace TechnikSys.MoldManager.Domain.Concrete
{
    public class PurchaseItemRepository:IPurchaseItemRepository
    {

        private EFDbContext _context = new EFDbContext();
        public IQueryable<PurchaseItem> PurchaseItems
        {
            get { return _context.PurchaseItems; }
        }

        public int Save(PurchaseItem PurchaseItem)
        {
            PurchaseItem _dbEntry;
            if (PurchaseItem.PurchaseItemID == 0)
            {
                PurchaseItem.Specification = PurchaseItem.Specification == null ? "" : PurchaseItem.Specification;
                PurchaseItem.Memo = PurchaseItem.Memo == null ? "" : PurchaseItem.Memo;
                PurchaseItem.PartNumber = PurchaseItem.PartNumber == null ? "" : PurchaseItem.PartNumber;
                PurchaseItem.Material = PurchaseItem.Material == null ? "" : PurchaseItem.Material;
                PurchaseItem.MoldNumber = PurchaseItem.MoldNumber == null ? "其他采购" : PurchaseItem.MoldNumber;
                _context.PurchaseItems.Add(PurchaseItem);
            }
            else
            {
                _dbEntry = _context.PurchaseItems.Find(PurchaseItem.PurchaseItemID);

                _dbEntry.PartID = PurchaseItem.PartID;
                _dbEntry.TaskID = PurchaseItem.TaskID;
                _dbEntry.Name = PurchaseItem.Name;
                _dbEntry.PartNumber = PurchaseItem.PartNumber==null?"":PurchaseItem.PartNumber;
                _dbEntry.Specification = PurchaseItem.Specification;
                _dbEntry.Material = PurchaseItem.Material==null?"":PurchaseItem.Material;
                _dbEntry.Quantity = PurchaseItem.Quantity;
                _dbEntry.SupplierName = PurchaseItem.SupplierName;
                _dbEntry.SupplierID = PurchaseItem.SupplierID;
                _dbEntry.PlanTime = PurchaseItem.PlanTime;
                _dbEntry.CreateTime = PurchaseItem.CreateTime;
                _dbEntry.DeliveryTime = PurchaseItem.DeliveryTime;
                _dbEntry.State = PurchaseItem.State;
                _dbEntry.PurchaseRequestID = PurchaseItem.PurchaseRequestID;
                _dbEntry.QuotationRequestID = PurchaseItem.QuotationRequestID;
                _dbEntry.PurchaseOrderID = PurchaseItem.PurchaseOrderID;
                _dbEntry.UnitPrice = PurchaseItem.UnitPrice;
                _dbEntry.TotalPrice = PurchaseItem.TotalPrice;
                _dbEntry.Memo = PurchaseItem.Memo==null?"":PurchaseItem.Memo;
                _dbEntry.PurchaseUserID = PurchaseItem.PurchaseUserID;
                _dbEntry.InStockQty = PurchaseItem.InStockQty;
                _dbEntry.OutStockQty = PurchaseItem.OutStockQty;
                _dbEntry.RequestUserID = PurchaseItem.RequestUserID;
                _dbEntry.PurchaseType = PurchaseItem.PurchaseType;
                _dbEntry.MoldNumber = PurchaseItem.MoldNumber == null ? "其他采购" : PurchaseItem.MoldNumber;
                _dbEntry.RequireTime = PurchaseItem.RequireTime;
                _dbEntry.UnitPriceWT = PurchaseItem.UnitPriceWT;
                _dbEntry.TotalPriceWT = PurchaseItem.TotalPriceWT;
                _dbEntry.TaxRate = PurchaseItem.TaxRate;
                _dbEntry.CostCenterID = PurchaseItem.CostCenterID;
                _dbEntry.AttachObjID = PurchaseItem.AttachObjID?? _dbEntry.AttachObjID;
                _dbEntry.unit = PurchaseItem.unit??"件";
                //_dbEntry.Time = PurchaseItem.Time;
            }
            _context.SaveChanges();
            return PurchaseItem.PurchaseItemID;
        }

        public void Delete(int PurchaseItemID)
        {
            PurchaseItem _dbEntry = _context.PurchaseItems.Find(PurchaseItemID);
            _dbEntry.State=(int)PurchaseItemStatus.取消;
            _context.SaveChanges();
        }

        public void ChangeState(int PurchaseItemID, int State)
        {
            PurchaseItem _dbEntry = _context.PurchaseItems.Find(PurchaseItemID);
            _dbEntry.State=State;
            _context.SaveChanges();
        }


        public PurchaseItem QueryByID(int PurchaseItemID)
        {
            return _context.PurchaseItems.Find(PurchaseItemID);
        }

        /// <summary>
        /// Query keyword contained in name, partnumber, specification and supplier name
        /// </summary>
        /// <param name="Keyword"></param>
        /// <returns></returns>
        public IEnumerable<PurchaseItem> QueryByKeyword(string Keyword, int State=100)
        {
            IEnumerable<PurchaseItem> _items;
            if (Keyword == "")
            {
                _items = _context.PurchaseItems.Where(p => p.State >= (int)PurchaseItemStatus.需求待审批).Distinct();
            }
            else
            {
                _items = _context.PurchaseItems.Where(p => p.Name.Contains(Keyword))
               .Union(_context.PurchaseItems.Where(p => p.PartNumber.Contains(Keyword)))
               .Union(_context.PurchaseItems.Where(p => p.Specification.Contains(Keyword)))
               .Union(_context.PurchaseItems.Where(p => p.SupplierName.Contains(Keyword)))
               .Where(p => p.State >= (int)PurchaseItemStatus.需求待审批)
               .Where(p => p.State <= State)
               .Distinct();            
            }
            
            return _items;
        }


        public IEnumerable<PurchaseItem> QueryBySupplier(int SupplierID)
        {
            IEnumerable<PurchaseItem> _items = _context.PurchaseItems.Where(p => p.SupplierID == SupplierID)
                .Union(_context.PurchaseItems.Where(p => p.SupplierID == 0)).OrderBy(p => p.Name);
            return _items;
        }


        public IEnumerable<PurchaseItem> QueryByPurchaseRequestID(int PurchaseRequestID)
        {
            return _context.PurchaseItems.Where(p => p.PurchaseRequestID == PurchaseRequestID)
                .Where(p=>p.State!=(int)PurchaseItemStatus.取消);
        }

        public IEnumerable<PurchaseItem> QueryByQuotationRequestID(int QuotationRequestID)
        {
            return _context.PurchaseItems.Where(p => p.QuotationRequestID == QuotationRequestID)
                .Where(p=>p.State!=(int)PurchaseItemStatus.取消);
        }

        public IEnumerable<PurchaseItem> QueryByPurchaseOrderID(int PurchaseOrderID)
        {
            return _context.PurchaseItems.Where(p => p.PurchaseOrderID == PurchaseOrderID)
                .Where(p=>p.State!=(int)PurchaseItemStatus.取消);
        }


        public void ChangeState(int PurchaseRequestID, int QuotationRequestID, int PurchaseOrderID, int State)
        {
            IEnumerable<PurchaseItem> _items = null;


            if (PurchaseRequestID > 0)
            {
                _items = QueryByPurchaseRequestID(PurchaseRequestID);
            }
            else if (QuotationRequestID > 0)
            {
                _items = QueryByQuotationRequestID(QuotationRequestID);
            }
            else if (PurchaseOrderID > 0)
            {
                _items = QueryByPurchaseOrderID(PurchaseOrderID);
            }

            foreach (PurchaseItem _item in _items)
            {
                _item.State = State;
            }
            _context.SaveChanges();
        }
        public void PlanDateAdjust(int purchaseitemID,DateTime planDate)
        {
            PurchaseItem dbEntry = _context.PurchaseItems.Where(p => p.PurchaseItemID == purchaseitemID && p.State < (int)PurchaseItemStatus.完成).FirstOrDefault();
            if (dbEntry.PlanAJTime.ToString("yyyy-MM-dd") == "1900-01-01")
            {
                dbEntry.Memo = dbEntry.Memo + "\r\n原计划到货日期：" + dbEntry.PlanTime.ToString("yyyy-MM-dd");
            }
            dbEntry.PlanTime = planDate;
            dbEntry.PlanAJTime = planDate;
            _context.SaveChanges();
            
        }
        public void PlanDateAdjustRecordSave(PurItemChangeDateRecord model)
        {
            //PurchaseItem dbEntry = _context.PurchaseItems.Where(p => p.PurchaseItemID == purchaseitemID && p.State < (int)PurchaseItemStatus.完成).FirstOrDefault();
            //dbEntry.PlanAJTime = planDate;
            _context.PurItemChangeDateRecords.Add(model);
            _context.SaveChanges();
        }
        public IEnumerable<PurItemChangeDateRecord> PurItemChangeDateRecords
        {
            get { return _context.PurItemChangeDateRecords; }
        }
        public List<PurItemChangeDateRecord> GetPurItemChangeDateRecords(List<PurItemChangeDateRecord> _itemCDs ,int PurchaseRequestID)
        {
            return _itemCDs.Where(p => p.PurchaseItemID == PurchaseRequestID).OrderByDescending(p => p.CreDate).ToList();
        }

        public int UpdateItemTime(int purItemID,double time)
        {
            PurchaseItem _purItem = _context.PurchaseItems.Where(p => p.PurchaseItemID == purItemID).FirstOrDefault();
            _purItem.Time = time;
            _context.SaveChanges();
            return _purItem.PurchaseItemID;
        }
    }
}
