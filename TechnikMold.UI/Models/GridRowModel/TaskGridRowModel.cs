using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using TechnikSys.MoldManager.Domain.Entity;
using MoldManager.WebUI.Models.Helpers;
using TechnikSys.MoldManager.Domain.Status;

namespace MoldManager.WebUI.Models.GridRowModel
{
    public class TaskGridRowModel
    {
        public string[] cell;
        string _dateVal = "";
        public TaskGridRowModel(Task Task, string CAD, string CAM, string Workshop, string QC, string FilePath, string PlanDate, CNCMachInfo Machinfo)
        {
            TaskStatus _status = new TaskStatus();
            cell = new string[33];

            cell[0] = Task.TaskID.ToString();
            if (Task.DrawingFile != "")
            {
                cell[1] = "<a href='/File"+FilePath+Task.MoldNumber+"/"+Task.DrawingFile+"' target='_blank'>Open</a>";
                //cell[1] = Task.DrawingFile;
            }
            
            cell[2] = Task.TaskName;
            string _version = Task.Version.ToString();
            cell[3] = _version.Length == 1 ? "V0" + _version : "V" + _version;
            cell[4] = Task.ProcessName;
            cell[5] = Task.Model;
            cell[6] = Task.R.ToString();
            cell[7] = Task.F.ToString();
            cell[8] = Task.HRC;
            cell[9] = Task.Material;
            cell[10] = Task.Time.ToString();

            switch (Task.TaskType)
            {
                case 1:
                    cell[11] = Enum.GetName(typeof(CNCStatus), Task.State);
                    break;
                case 2:
                    cell[11] = Enum.GetName(typeof(EDMStatus), Task.State);
                    break;
                case 3:
                    cell[11] = Enum.GetName(typeof(WEDMStatus), Task.State);
                    break;
                case 4:
                    cell[11] = Enum.GetName(typeof(SteelStatus), Task.State);
                    break;
                case 6:
                    cell[11] = Enum.GetName(typeof(GrindStatus), Task.State);
                    break;
            }
            
            cell[12] = Task.Raw;
            cell[13] = Task.Prepared.ToString();
            cell[14] = Task.Priority.ToString();
            cell[15] = Task.Quantity.ToString();

            _dateVal = Task.CreateTime.ToString("yyyy-MM-dd hh:mm:ss"); ;
            cell[16] = (_dateVal == "1900-01-01"||_dateVal=="0001-01-01") ? "-" : _dateVal;

            _dateVal = Task.ForecastTime.ToString("yyyy-MM-dd"); ;
            cell[17] = (_dateVal == "1900-01-01" || _dateVal == "0001-01-01") ? "-" : _dateVal;

            _dateVal = Task.AcceptTime.ToString("yyyy-MM-dd hh:mm:ss"); ;
            cell[18] = (_dateVal == "1900-01-01" || _dateVal == "0001-01-01") ? "-" : _dateVal;

            _dateVal = Task.ReleaseTime.ToString("yyyy-MM-dd hh:mm:ss"); ;
            cell[19] = (_dateVal == "1900-01-01" || _dateVal == "0001-01-01") ? "-" : _dateVal;

            //_dateVal = Task.PlanTime.ToString("yyyy-MM-dd"); ;
            cell[20] = (PlanDate == "1900-01-01" || PlanDate == "0001-01-01") ? "-" : PlanDate;

            _dateVal = Task.StartTime.ToString("yyyy-MM-dd hh:mm:ss"); ;
            cell[21] = (_dateVal == "1900-01-01" || _dateVal == "0001-01-01") ? "-" : _dateVal;
            cell[22] = Task.Memo;
            cell[23] = Task.StateMemo;
            cell[24] = CAD;
            cell[25] = CAM;
            cell[26] = Workshop;
            cell[27] = QC;
            cell[28] = Task.PositionFinish.ToString() ;
            cell[29] = Task.QCInfoFinish.ToString() ;

            if (Machinfo != null)
            {
                cell[30] = Machinfo.Surface;
                cell[31] = Machinfo.ObitType;
                cell[32] = Machinfo.Position;
            }
            else
            {
                cell[30] = "";
                cell[31] = "";
                cell[32] = "";
            }
        }
    }
}