using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TechnikSys.MoldManager.Domain.Abstract;
using TechnikSys.MoldManager.Domain.Entity;

namespace TechnikSys.MoldManager.Domain.Concrete
{
    public class OutStockFormRepository:IOutStockFormRepository
    {
        private EFDbContext _context = new EFDbContext();



        public IQueryable<OutStockForm> OutStockForms
        {
            get { return _context.OutStockForms; }
        }

        public int Save(OutStockForm OutStockForm)
        {
            if (OutStockForm.OutStockFormID == 0)
            {
                _context.OutStockForms.Add(OutStockForm);
            }
            else
            {
                OutStockForm _dbEntry = _context.OutStockForms.Find(OutStockForm.OutStockFormID);
                if (_dbEntry != null)
                {
                    _dbEntry.WHUserID    =OutStockForm.WHUserID;
                    _dbEntry.FormName    =OutStockForm.FormName;
                    _dbEntry.CreateTime = OutStockForm.CreateTime;
                    _dbEntry.UserID = OutStockForm.UserID;
                }
            }
            _context.SaveChanges();
            return OutStockForm.OutStockFormID;
        }


        public OutStockForm QueryByID(int OutStockFormID)
        {
            return _context.OutStockForms.Find(OutStockFormID);
        }
    }
}
