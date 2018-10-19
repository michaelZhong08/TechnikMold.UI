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
    public class PurhaseOrderRepository:IPurchaseOrderRepository
    {
        private EFDbContext _context = new EFDbContext();
        public IQueryable<PurchaseOrder> PurchaseOrders
        {
            get 
            {
                return _context.PurchaseOrders;
            }
        }

        public int Save(PurchaseOrder PurchaseOrder)
        {

            if (PurchaseOrder.PurchaseOrderID == 0)
            {
                PurchaseOrder.CreateDate = DateTime.Now;
                _context.PurchaseOrders.Add(PurchaseOrder);
            }
            else
            {
                PurchaseOrder _dbEntry = _context.PurchaseOrders.Find(PurchaseOrder.PurchaseOrderID);
                if (_dbEntry != null)
                {
                    _dbEntry.PurchaseOrderNumber = PurchaseOrder.PurchaseOrderNumber;
                    _dbEntry.State = PurchaseOrder.State;
                    _dbEntry.UserID = PurchaseOrder.UserID;
                    _dbEntry.Responsible = PurchaseOrder.Responsible;
                    _dbEntry.Approval = PurchaseOrder.Approval;
                    _dbEntry.ProjectID = PurchaseOrder.ProjectID;
                    _dbEntry.ReleaseDate = PurchaseOrder.ReleaseDate;
                    _dbEntry.FinishDate = PurchaseOrder.FinishDate;
                    _dbEntry.TotalPrice = PurchaseOrder.TotalPrice;
                    _dbEntry.SupplierID = PurchaseOrder.SupplierID;
                    _dbEntry.Memo = PurchaseOrder.Memo;
                    _dbEntry.DueDate = PurchaseOrder.DueDate;
                    _dbEntry.QuotationRequestID = PurchaseOrder.QuotationRequestID;
                    _dbEntry.Currency = PurchaseOrder.Currency;
                    _dbEntry.TaxRate = PurchaseOrder.TaxRate;
                    _dbEntry.PurchaseType = PurchaseOrder.PurchaseType;
                    _dbEntry.SupplierName = PurchaseOrder.SupplierName;
                }
            }
            
            _context.SaveChanges();
            return PurchaseOrder.PurchaseOrderID;
        }

        

        /// <summary>
        /// Find Purchase Order by request id
        /// </summary>
        /// <param name="PurchaseRequestID"></param>
        /// <returns></returns>
        public PurchaseOrder QueryByPRID(int PurchaseRequestID)
        {
            return _context.PurchaseOrders.Where(p => p.PurchaseRequestID == PurchaseRequestID).FirstOrDefault();
            
        }

        /// <summary>
        /// Finde purchase order by order id
        /// </summary>
        /// <param name="PurchaseOrderID"></param>
        /// <returns></returns>
        public PurchaseOrder QueryByID(int PurchaseOrderID)
        {
            return _context.PurchaseOrders.Find(PurchaseOrderID);
        }


        public void ClosePurchaseOrder(int PurchaseOrderID)
        {
            PurchaseOrder _order = QueryByID(PurchaseOrderID);
            _order.State = 5;
            _context.SaveChanges();
        }

        public void PartialClosePO(int PurchaseOrderID)
        {
            PurchaseOrder _order = QueryByID(PurchaseOrderID);
            _order.State = 4;
            _context.SaveChanges();
        }

        public void ModifyDueDate(int PurchaseOrderID, DateTime DueDate)
        {
            PurchaseOrder _order = QueryByID(PurchaseOrderID);
            _order.DueDate = DueDate;
            _context.SaveChanges();
        }


        public void Submit(int PurchaseOrderID, int State, string Memo)
        {
            PurchaseOrder _order = QueryByID(PurchaseOrderID);
            _order.State = State;
            if (Memo != "") { 
                _order.Memo = Memo;
            }
            if ((State == 3)||(State==10))
            {
                _order.ReleaseDate = DateTime.Now;
            }
            _context.SaveChanges();
        }

    }
}
