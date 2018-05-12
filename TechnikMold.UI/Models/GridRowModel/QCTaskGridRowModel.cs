using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using TechnikSys.MoldManager.Domain.Entity;

namespace MoldManager.WebUI.Models.GridRowModel
{
    public class QCTaskGridRowModel
    {
        public string[] cell;
        public QCTaskGridRowModel(QCTask QCTask)
        {
            cell = new string[10];
            cell[0] = QCTask.QCTaskID.ToString();
            cell[1] = QCTask.DrawingFile==""? "":"<a href='/File/"+QCTask.DrawingFile+"/"+QCTask.DrawingFile+"'>Open</a>";
            cell[2] = QCTask.TaskName;
            cell[3] = QCTask.Version.ToString();
            cell[4] = QCTask.Priority.ToString();

        }
    }
}