using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using TechnikSys.MoldManager.Domain.Entity;
using MoldManager.WebUI.Models.Helpers;
using TechnikSys.MoldManager.Domain.Status;
using TechnikMold.UI.Models.ViewModel;

namespace MoldManager.WebUI.Models.GridRowModel
{
    public class TaskGridRowModel
    {
        public string[] cell;
        string _dateVal = "";
        public TaskGridRowModel(Task Task, string CAD, string CAM, string Workshop, string QC, string FilePath, string PlanDate, SetupTaskStart _setupTask, CNCMachInfo Machinfo,WEDMSetting wedmsetting,MGSetting mgsetting)
        {
            Helpers.TaskStatus _status = new Helpers.TaskStatus();
            cell = new string[42];
            //TaskID
            cell[0] = Task.TaskID.ToString();
            //图纸
            if (Task.DrawingFile != "")
            {
                cell[1] = "<a href='/File"+FilePath+Task.MoldNumber+"/"+Task.DrawingFile+ ".pdf" + "' target='_blank'>Open</a>";
            }
            //任务名
            cell[2] = Task.TaskName;
            //版本
            string _version = Task.Version.ToString();
            cell[3] = _version.Length == 1 ? "V0" + _version : "V" + _version;
            //CAD文档
            switch (Task.TaskType)
            {
                case 3:
                    //CAD文档
                    cell[4] = wedmsetting.CADPartName ?? "";
                    break;
                case 6:
                    cell[4] = mgsetting.CADNames ?? "";
                    break;
                default:
                    cell[4] = "";
                    break;
            }
            //CAD人员FullName
            cell[5] = CAD;
            //备注
            cell[6] = Task.Memo;
            switch (Task.TaskType)
            {
                case 3:
                    //加工精度
                    cell[7] = wedmsetting.Precision ?? "";
                    //特征数量
                    cell[8] = wedmsetting.FeatureCount.ToString();
                    //长度
                    cell[9] = wedmsetting.Length.ToString();
                    //厚度
                    cell[10] = wedmsetting.Thickness.ToString();
                    break;
                case 6:
                    cell[7] = "";
                    cell[8] = mgsetting.FeatureNote ?? "";
                    cell[9] = "";
                    cell[10] = "";
                    break;
                default:
                    cell[7] = "";
                    cell[8] = "";
                    cell[9] = "";
                    cell[10] = "";
                    break;
            }

            //时间
            cell[11] = Task.Time.ToString();
            //状态
            string stateName;
            //switch (Task.TaskType)
            //{
            //    case 1:
            //        stateName = Enum.GetName(typeof(TechnikSys.MoldManager.Domain.Status.TaskStatus), Task.State);
            //        break;
            //    case 2:
            //        stateName = Enum.GetName(typeof(EDMStatus), Task.State);
            //        break;
            //    case 3:
            //        stateName = Enum.GetName(typeof(WEDMStatus), Task.State);
            //        break;
            //    case 4:
            //        stateName = Enum.GetName(typeof(SteelStatus), Task.State);
            //        break;
            //    case 6:
            //        stateName = Enum.GetName(typeof(GrindStatus), Task.State);
            //        break;
            //    default:
            //        stateName = "";
            //        break;
            //}
            stateName = Enum.GetName(typeof(TechnikSys.MoldManager.Domain.Status.TaskStatus), Task.State);
            cell[12] = stateName;
            //状态备注
            cell[13] = Task.StateMemo;

            //毛坯(规格)
            cell[14] = Task.Raw;
            //型号
            cell[15] = Task.Model;
            if (Machinfo != null)
            {
                //表面
                cell[16] = Machinfo.Surface;
                //平动
                cell[17] = Machinfo.ObitType;
                //电极位置
                cell[18] = Machinfo.Position;
            }
            else
            {
                cell[16] = "";
                cell[17] = "";
                cell[18] = "";
            }
            //R
            cell[19] = Task.R.ToString();
            //F
            cell[20] = Task.F.ToString();
            //数量
            cell[21] = Task.Quantity.ToString();
            //备料
            cell[22] = Task.Prepared.ToString();

            //材料
            cell[23] = Task.Material;
            //HRC
            cell[24] = Task.HRC;

            //工艺
            cell[25] = Task.ProcessName;
            //实际工时
            cell[26] = _setupTask.TotalTime.ToString();
            //优先
            cell[27] = Task.Priority.ToString();

            //创建日期
            _dateVal = Task.CreateTime.ToString("yyyy-MM-dd HH:mm:ss");
            cell[28] = (_dateVal == "1900-01-01"||_dateVal=="0001-01-01") ? "-" : _dateVal;
            //计划日期
            cell[29] = (PlanDate == "1900-01-01" || PlanDate == "0001-01-01") ? "-" : PlanDate;
            //接收日期
            _dateVal = Task.AcceptTime.ToString("yyyy-MM-dd HH:mm:ss");
            cell[30] = (_dateVal == "1900-01-01" || _dateVal == "0001-01-01") ? "-" : _dateVal;
            //发布日期
            _dateVal = Task.ReleaseTime.ToString("yyyy-MM-dd HH:mm:ss");
            cell[31] = (_dateVal == "1900-01-01" || _dateVal == "0001-01-01") ? "-" : _dateVal;
            //开始日期
            _dateVal = Task.StartTime.ToString("yyyy-MM-dd HH:mm:ss");
            cell[32] = (_dateVal == "1900-01-01" || _dateVal == "0001-01-01") ? "-" : _dateVal;
            //结束时间
            _dateVal = Task.FinishTime.ToString("yyyy-MM-dd HH:mm:ss");
            cell[33] = (_dateVal == "1900-01-01" || _dateVal == "0001-01-01") ? "-" : _dateVal;
            //预计日期
            _dateVal = Task.ForecastTime.ToString("yyyy-MM-dd"); ;
            cell[34] = (_dateVal == "1900-01-01" || _dateVal == "0001-01-01") ? "-" : _dateVal;

            //CADM人员FullName
            cell[35] = CAM;
            //QC点
            cell[36] = Task.QCInfoFinish.ToString();
            //跑位检查
            cell[37] = Task.PositionFinish.ToString();
            //QC
            cell[38] = QC;
            //加工人员FullName
            cell[39] = Workshop;
            //机器号
            cell[40] = _setupTask.MachinesName;
            //操作人员
            cell[41] = _setupTask.UserName;
        }

        public TaskGridRowModel(Task Task, string FilePath, string Creator)
        {
            cell = new string[5];
            //图纸
            if (Task.DrawingFile != "")
            {
                cell[0] = "<a href='/File" + FilePath + Task.MoldNumber + "/" + Task.DrawingFile + ".pdf" + "' target='_blank'>Open</a>";
            }
            //任务名
            cell[1] = Task.TaskName;
            //版本
            string _version = Task.Version.ToString();
            cell[2] = _version.Length == 1 ? "V0" + _version : "V" + _version;
            cell[3] = string.IsNullOrEmpty(Creator) ? "-" : Creator;
            cell[4] = Task.TaskID.ToString();
        }
    }
}