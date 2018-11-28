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
    public class PRContentRepository:IPRContentRepository
    {
        private EFDbContext _context = new EFDbContext();
        public IQueryable<PRContent> PRContents
        {
            get 
            {
                return _context.PRContents;
            }
        }

        public int Save(PRContent PRContent)
        {
            if (PRContent.PRContentID == 0)
            {
                PRContent.RequireTime= PRContent.RequireTime == new DateTime() ? new DateTime(1900, 1, 1) : PRContent.RequireTime;
                PRContent.MoldNumber = PRContent.MoldNumber == null ? "其他采购" : PRContent.MoldNumber;
                PRContent.ERPPartID = PRContent.ERPPartID ?? "";
                _context.PRContents.Add(PRContent);
            }
            else
            {
                PRContent _dbEntry = _context.PRContents.Find(PRContent.PRContentID);
                if (_dbEntry != null)
                {
                    _dbEntry.PartID = PRContent.PartID;
                    _dbEntry.TaskID = PRContent.TaskID;
                    _dbEntry.PartName = PRContent.PartName;
                    _dbEntry.PartNumber = PRContent.PartNumber;
                    _dbEntry.PartSpecification = PRContent.PartSpecification;
                    _dbEntry.Quantity = PRContent.Quantity;
                    _dbEntry.PartTypeID = PRContent.PartTypeID;
                    _dbEntry.PurchaseRequestID = PRContent.PurchaseRequestID;
                    _dbEntry.UnitPrice = PRContent.UnitPrice;
                    _dbEntry.SubTotal = PRContent.SubTotal;
                    _dbEntry.MaterialName = PRContent.MaterialName;
                    _dbEntry.Hardness = PRContent.Hardness;
                    _dbEntry.JobNo = PRContent.JobNo;
                    _dbEntry.BrandName = PRContent.BrandName;
                    _dbEntry.PurchaseDrawing = PRContent.PurchaseDrawing;
                    _dbEntry.Memo = PRContent.Memo;
                    _dbEntry.EstimatePrice = PRContent.EstimatePrice;
                    //_dbEntry.Enabled = true ;
                    _dbEntry.SupplierName = PRContent.SupplierName;
                    _dbEntry.WarehouseStockID = PRContent.WarehouseStockID;
                    _dbEntry.MoldNumber = PRContent.MoldNumber==null?"其他采购":PRContent.MoldNumber;
                    _dbEntry.RequireTime = PRContent.RequireTime == new DateTime(1900, 1, 1) ? new DateTime(1900, 1, 1) : PRContent.RequireTime;
                    _dbEntry.CostCenterID = PRContent.CostCenterID;
                    _dbEntry.ERPPartID = PRContent.ERPPartID ?? "";
                    _dbEntry.Enabled = PRContent.Enabled;
                    _dbEntry.unit = PRContent.unit ?? "件";
                }
            }
            _context.SaveChanges();
            return PRContent.PRContentID;
        }

        public IQueryable<PRContent> QueryByName(string Name)
        {
            return _context.PRContents.Where(p => p.PartName.Contains(Name));
        }

        public IQueryable<PRContent> QueryBySpecification(string Specification)
        {
            throw new NotImplementedException();
        }


        public IQueryable<PRContent> QueryByRequestID(int PurchaseRequestID)
        {
            return _context.PRContents.Where(p => p.PurchaseRequestID == PurchaseRequestID).Where(p=>p.Enabled==true);
        }


        public PRContent QueryByID(int PRContentID)
        {
            return _context.PRContents.Find(PRContentID);
        }

        public void Delete(int PRContentID)
        {
            PRContent content = QueryByID(PRContentID);
            if (content != null) { 
                content.Enabled = false;
            }
            _context.SaveChanges();

        }
    }
}
