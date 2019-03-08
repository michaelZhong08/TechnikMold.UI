using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using TechnikSys.MoldManager.Domain.Entity;
using MoldManager.WebUI.Models.GridRowModel;
using MoldManager.WebUI.Models.Helpers;
using TechnikSys.MoldManager.Domain.Abstract;


namespace MoldManager.WebUI.Models.GridViewModel
{
    public class POListGridViewModel
    {
        public List<POListGridRowModel> rows;
        public int Page;
        public int Total;
        public int Records;

        public POListGridViewModel(List<PurchaseOrder> Orders,
            IProjectRepository ProjectRepository,
            ISupplierRepository SupplierRepository, 
            IPurchaseTypeRepository PurchaseTypeRepository, 
            IUserRepository UserRepository)
        {
            rows = new List<POListGridRowModel>();
            string _status;
            int count = Orders.Count();
            if (count > 0)
            {
                List<PurchaseType> _types = PurchaseTypeRepository.PurchaseTypes.ToList();
                List<Supplier> _sups = SupplierRepository.Suppliers.ToList();
                List<User> _users = UserRepository.Users.ToList();
                foreach (PurchaseOrder _order in Orders)
                {
                    string Supplier = "";
                    if (SupplierRepository.QueryByID(_order.SupplierID) != null)
                        Supplier = (_sups.Where(s => s.SupplierID == _order.SupplierID && s.Enabled == true).FirstOrDefault() ?? new TechnikSys.MoldManager.Domain.Entity.Supplier()).Name;//SupplierRepository.QueryByID(_order.SupplierID).Name;
                    string PurchaseType;
                    try
                    {
                        PurchaseType = (_types.Where(t => t.Enabled == true && t.PurchaseTypeID == _order.PurchaseType).FirstOrDefault() ?? new TechnikSys.MoldManager.Domain.Entity.PurchaseType()).Name;//PurchaseTypeRepository.QueryByID(_order.PurchaseType).Name;
                    }
                    catch
                    {
                        PurchaseType = "";
                    }
                    _status = Enum.GetName(typeof(PurchaseOrderStatus), _order.State);
                    string _user = "";
                    try
                    {
                        _user = (_users.Where(u => u.Enabled && u.UserID == _order.UserID).FirstOrDefault() ?? new User()).FullName;// UserRepository.GetUserByID(_order.UserID).FullName;
                    }
                    catch
                    {
                        _user = "";
                    }
                    rows.Add(new POListGridRowModel(_order, Supplier, _status, PurchaseType, _user));
                }
            }
            
            Page = 1;
            Total = Orders.Count();
            Records = Orders.Count(); ;
        }
    }
}