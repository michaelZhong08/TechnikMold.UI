using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using TechnikSys.MoldManager.Domain.Entity;
using TechnikSys.MoldManager.Domain.Abstract;
using MoldManager.WebUI.Models.Helpers;
using TechnikMold.UI.Models.GridRowModel;

namespace TechnikMold.UI.Models.GridViewModel
{
    public class MGUptVerGridViewModel
    {
        public List<MGUptVerGridRowModel> rows = new List<MGUptVerGridRowModel>();
        public int Page;
        public int Total;
        public int Records;
        public MGUptVerGridViewModel(string TaskIDs,IUserRepository Users,ITaskRepository Tasks)
        {
            string[] _taskIDs = TaskIDs.Split(',');
            if (_taskIDs.Length > 0 && _taskIDs[0]!="undefined" && _taskIDs[0]!="")
            {
                for (int i = 0; i < _taskIDs.Length; i++)
                {
                    int _id = Convert.ToInt32(_taskIDs[i]);
                    //MGSetting mgsetting = MGSettings.QueryByTaskID(_id) ?? new MGSetting();
                    Task task = Tasks.QueryByTaskID(_id) ?? new Task();
                    string DrawingFile= Tasks.GetMaxVerDrawFile(task.TaskName);
                    string CAM = Users.GetUserByID(task.CAMUser).FullName ?? "";
                    rows.Add(new MGUptVerGridRowModel(task, DrawingFile, CAM));
                }
            }           
        }
    }
}