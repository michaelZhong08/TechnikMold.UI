using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TechnikSys.MoldManager.Domain.Entity;
using TechnikSys.MoldManager.Domain.Abstract;

namespace TechnikSys.MoldManager.Domain.Concrete
{
    public class SupplierGroupRepository:ISupplierGroupRepository
    {
        private EFDbContext _context = new EFDbContext();
        public IQueryable<SupplierGroup> SupplierGroup
        {
            get { return _context.SupplierGroups; }
        }
        public int Save(SupplierGroup model)
        {
            SupplierGroup _supplierGroup = _context.SupplierGroups.Where(s => s.ID == model.ID).FirstOrDefault();
            if (_supplierGroup==null)
            {
                model.active = true;
                _context.SupplierGroups.Add(model);
            }
            else
            {
                _supplierGroup.GroupName = model.GroupName;
                _supplierGroup.MailList = model.MailList;
                _supplierGroup.active = model.active;
            }
            _context.SaveChanges();
            return model.ID;
        }
        public List<SupplierGroup> QuerySGList()
        {
            return _context.SupplierGroups.Where(s => s.active == true).ToList();
        }
        public SupplierGroup QueryByID(int _sgID)
        {
            return _context.SupplierGroups.Where(s => s.active == true && s.ID==_sgID).FirstOrDefault();
        }
    }
}
