using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using TechnikSys.MoldManager.Domain.Entity;
using MoldManager.WebUI.Models.GridRowModel;

namespace MoldManager.WebUI.Models.GridViewModel
{
    public class UserGridViewModel
    {
        public List<UserGridRowModel> rows;
        public int Page;
        public int Total;
        public int Records;

        public UserGridViewModel(IEnumerable<User> UserList, IEnumerable<Department> DepartmentList, IEnumerable<Position> PositionList,  int TotalCount)
        {
            rows = new List<UserGridRowModel>();
            Page = 1;
            Total = TotalCount;
            foreach (User _user in UserList)
            {
                string _dept = DepartmentList.Where(d=>d.DepartmentID == _user.DepartmentID).Select(d=>d.Name).FirstOrDefault();
                string _pos = PositionList.Where(p => p.PositionID == _user.PositionID).Select(p => p.Name).FirstOrDefault();
                UserGridRowModel _userModel = new UserGridRowModel(_user, _dept, _pos);
                rows.Add(_userModel);
            }
            Records = 20;

        }
    }
}