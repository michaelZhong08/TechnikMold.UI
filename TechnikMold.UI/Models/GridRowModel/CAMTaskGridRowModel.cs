using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using TechnikSys.MoldManager.Domain.Entity;
namespace MoldManager.WebUI.Models.GridRowModel
{
    public class CAMTaskGridRowModel
    {
        public string[] cell;

        public CAMTaskGridRowModel(CAMTask CAMTask, string UserName, string CADUserName, string ReleaseUserName, string TaskType)
        {
            string _dateVal = "";
            cell=new string[12];


            //TaskID
            cell[0]= CAMTask.CAMTaskID.ToString();
            
            //Drawing file link
            if (CAMTask.DrawingFile == "")
            {
                cell[1] = "<a href='' target='new'>打开</a>";
            }
            else
            {
                cell[1] = "<a href='file://"+CAMTask.DrawingFile+"' target='new'>打开</a>";
            }
            

            //Task Name
            cell[2] = CAMTask.TaskName;

            //Mold Number
            cell[3] = CAMTask.MoldNumber;

            //Task Type
            cell[4] = TaskType;

            //Task Version
            cell[5] = "V" + CAMTask.Version;

            //CAD User
            cell[6] = CADUserName;

            //Create Date
            _dateVal = CAMTask.CreateDate.ToString("yyyy-MM-dd");
            cell[7] = _dateVal == "0001-01-01" ? "-" : _dateVal;

            //Accept User
            cell[8] = UserName;
           

            //Accept date
            _dateVal = CAMTask.AcceptDate.ToString("yyyy-MM-dd");
            cell[9]=_dateVal == "0001-00-01" ? "-" : _dateVal;
            

            //Release User
            cell[10] = ReleaseUserName;
            //Release Date
            _dateVal = CAMTask.ReleaseDate.ToString("yyyy-MM-dd");
            cell[11] = _dateVal == "0001-01-01" ? "-" : _dateVal;
            
            
        }

    }
}