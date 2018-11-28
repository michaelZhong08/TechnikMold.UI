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
    public class POContentRepository:IPOContentRepository
    {
        private EFDbContext _context = new EFDbContext();



        public IQueryable<POContent> POContents
        {
            get { return _context.POContents; }
        }

        /// <summary>
        /// Create/Update PO content record
        /// </summary>
        /// <param name="POContent"></param>
        /// <returns></returns>
        public int Save(POContent POContent)
        {
            if (POContent.POContentID == 0)
            {
                POContent.Enabled = true;
                _context.POContents.Add(POContent);
            }
            else
            {
                POContent _dbEntry = _context.POContents.Find(POContent.POContentID);
                if (_dbEntry != null)
                {
                    _dbEntry.PRContentID = POContent.PRContentID;
                    _dbEntry.PartName = POContent.PartName;
                    _dbEntry.PartNumber = POContent.PartNumber;
                    _dbEntry.PartSpecification = POContent.PartSpecification;
                    _dbEntry.Quantity = POContent.Quantity;
                    _dbEntry.PurchaseOrderID = POContent.PurchaseOrderID;
                    _dbEntry.UnitPrice = POContent.UnitPrice;
                    _dbEntry.SubTotal = POContent.SubTotal;
                    _dbEntry.BrandName = POContent.BrandName;
                    _dbEntry.Memo = POContent.Memo;
                    _dbEntry.ReceivedQty = POContent.ReceivedQty;
                    _dbEntry.State = POContent.State;
                    _dbEntry.unit = POContent.unit ?? "件";
                    _dbEntry.Enabled = true;

                }
            }
            _context.SaveChanges();
            return POContent.POContentID;
        }


        //Update the received quantity to identify the PO complete rate
        public int Receive(int POContentID, int Quantity, string Memo)
        {
            POContent _dbEntry = _context.POContents.Find(POContentID);
            if (_dbEntry != null)
            {
                _dbEntry.ReceivedQty = _dbEntry.ReceivedQty + Quantity;                
                _dbEntry.Memo = Memo;
            }
            _context.SaveChanges();
            return _dbEntry.POContentID;
        }


        public POContent QueryByPRContentID(int PRContentID)
        {
            return _context.POContents.Where(p => p.PRContentID == PRContentID).Where(p=>p.Enabled==true).FirstOrDefault();
        }

        public POContent QueryByID(int POContentID)
        {
            return _context.POContents.Find(POContentID);
        }

        public IEnumerable<POContent> QueryByPOID(int PurchaseOrderID)
        {
            return _context.POContents.Where(p => p.PurchaseOrderID==PurchaseOrderID).Where(p=>p.Enabled==true);
        }


        public void BatchCreate(List<POContent> POContents)
        {
            _context.POContents.AddRange(POContents);
            _context.SaveChanges();
        }


        public void Delete(int POContentID)
        {
            POContent _dbEntry = QueryByID(POContentID);
            _dbEntry.Enabled = false;
            _context.SaveChanges();
        }
    }
}
