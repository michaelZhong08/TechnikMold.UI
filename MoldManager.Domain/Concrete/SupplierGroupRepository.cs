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
                _supplierGroup.MailList = model.MailList?? _supplierGroup.MailList;
                _supplierGroup.active = model.active;
                _supplierGroup.SupplierIDs = model.SupplierIDs?? _supplierGroup.SupplierIDs;
            }
            _context.SaveChanges();
            return model.ID;
        }
        public List<SupplierGroup> QuerySGList()
        {
            return _context.SupplierGroups.Where(s => s.active == true).OrderBy(s=>s.ID).ToList();
        }
        public SupplierGroup QueryByID(int _sgID)
        {
            return _context.SupplierGroups.Where(s => s.active == true && s.ID==_sgID).FirstOrDefault();
        }
        public List<Supplier> QuerySuppliersByGroupID(int _groupID)
        {
            SupplierGroup _supGroup = QueryByID(_groupID);
            List<Supplier> _suppliers = new List<Supplier>();
            string _supplierIDs = _supGroup.SupplierIDs;
            if (!string.IsNullOrEmpty(_supplierIDs))
            {
                if (!string.IsNullOrEmpty(_supplierIDs.Trim()))
                {
                    var _supidArray = _supplierIDs.Split(';');
                    foreach (var _id in _supidArray)
                    {
                        if (!string.IsNullOrEmpty(_id.ToString()))
                        {
                            Supplier _supplier = _context.Suppliers.ToList().Where(s => s.SupplierID == Convert.ToInt32(_id)).FirstOrDefault();
                            if (_supplier != null)
                            {
                                _suppliers.Add(_supplier);
                            }
                        }
                    }
                }               
            }
            
            return _suppliers;
        }
        public int Delete(int _sgID)
        {
            SupplierGroup _sgObj = _context.SupplierGroups.Where(s => s.ID == _sgID && s.active == true).FirstOrDefault();
            if (_sgObj != null)
            {
                _sgObj.active = false;
                _context.SaveChanges();
                return 1;
            }
            return -99;
        }
    }
}
