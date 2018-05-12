using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using TechnikSys.MoldManager.Domain.Entity;
using TechnikSys.MoldManager.Domain.Abstract;
using MoldManager.WebUI.Models.GridRowModel;
using MoldManager.WebUI.Models.Helpers;

namespace MoldManager.WebUI.Models.GridViewModel
{
    public class CAMTaskGridViewModel
    {
        public List<CAMTaskGridRowModel> rows = new List<CAMTaskGridRowModel>();
        public int Page;
        public int Total;
        public int Records;
        public CAMTaskGridViewModel(IEnumerable<CAMTask> CAMTasks, IUserRepository Users)
        {
            string _user;
            string _cadUser;
            string _releaseUser;
            TaskType _type = new TaskType();
            foreach (CAMTask _task in CAMTasks)
            {
                try
                {
                    _user = Users.GetUserByID(_task.UserID).FullName;
                }
                catch
                {
                    _user = ""; 
                }
                try
                {
                     _cadUser = Users.GetUserByID(_task.CADUserID).FullName;
                }
                catch
                {
                    _cadUser = "";
                }
                try
                {
                    _releaseUser = Users.GetUserByID(_task.ReleaseUserID).FullName;
                }
                catch
                {
                    _releaseUser = "";
                }
                //_taskType = _type.GetTypeName(_task.TaskType);
                //rows.Add(new CAMTaskGridRowModel(_task, _user, _cadUser, _releaseUser, _taskType));
            }
        }
    }
}