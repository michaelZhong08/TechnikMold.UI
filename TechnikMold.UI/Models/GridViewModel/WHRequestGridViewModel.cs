using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using TechnikSys.MoldManager.Domain.Entity;
using MoldManager.WebUI.Models.GridRowModel;
using TechnikSys.MoldManager.Domain.Abstract;

namespace MoldManager.WebUI.Models.GridViewModel
{
    public class WHRequestGridViewModel
    {
        public List<WHRequestGridRowModel> rows = new List<WHRequestGridRowModel>();
        public int Page;
        public int Total;
        public int Records;

        public WHRequestGridViewModel(IEnumerable<WarehouseRequest> Requests, IUserRepository Users)
        {
            string _user;
            string _approvalUser;
            foreach (WarehouseRequest _request in Requests)
            {
                _user = (Users.GetUserByID(_request.RequestUserID)??new User()).FullName;
                _approvalUser = (Users.GetUserByID(_request.ApprovalUserID)??new User()).FullName;
                WHRequestGridRowModel _row = new WHRequestGridRowModel(_request, _user, _approvalUser);
                rows.Add(_row);
            }
        }
    }
}