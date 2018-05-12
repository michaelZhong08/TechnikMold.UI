using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using TechnikSys.MoldManager.Domain.Entity;
using TechnikSys.MoldManager.Domain.Abstract;
using MoldManager.WebUI.Models.GridRowModel;

namespace MoldManager.WebUI.Models.GridViewModel
{
    public class ReturnRequestGridViewModel
    {
        public List<ReturnRequestGridRowModel> rows;
        public int Page;
        public int Total;
        public int Records;

        public ReturnRequestGridViewModel(IEnumerable<ReturnRequest> ReturnRequests, 
            IPurchaseOrderRepository PORepository, 
            IUserRepository UserRepository)
        {
            rows = new List<ReturnRequestGridRowModel>();
            
            foreach (ReturnRequest _request in ReturnRequests)
            {
                rows.Add(new ReturnRequestGridRowModel(_request, PORepository, UserRepository));
            }
            
        }
    }
}