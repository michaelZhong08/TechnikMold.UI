using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using TechnikSys.MoldManager.Domain.Abstract;
using TechnikSys.MoldManager.Domain.Entity;
using MoldManager.WebUI.Models.GridRowModel;

namespace MoldManager.WebUI.Models.GridViewModel
{
    public class OutStockGridViewModel
    {
        public List<OutStockGridRowModel> rows = new List<OutStockGridRowModel>();
        public int Page;
        public int Total;
        public int Records;

        public OutStockGridViewModel(IEnumerable<OutStockItem> Items,
            IOutStockFormRepository FormRepo, IUserRepository UserRepo)
        {
            OutStockForm _form;
            string RequestUser, WarehouseUser;
            foreach (OutStockItem _item in Items)
            {
                _form = FormRepo.QueryByID(_item.OutStockFormID);
                WarehouseUser = UserRepo.GetUserByID(_form.WHUserID).FullName;
                try
                {
                    RequestUser = UserRepo.GetUserByID(_form.UserID).FullName;
                }
                catch
                {
                    RequestUser = "";
                }
                
                //OutStockGridRowModel _row = new OutStockGridRowModel(_item, FormRepo, RequestUser, WarehouseUser);
                rows.Add(new OutStockGridRowModel(_item, _form, RequestUser,WarehouseUser));

            }
        }
    }
}