using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using TechnikSys.MoldManager.Domain.Entity;

namespace TechnikMold.UI.Models.GridRowModel
{
    public class WHReportRowModelA
    {
        public string[] cell;
        /// <summary>
        /// 
        /// </summary>
        /// <param name="model">工时记录</param>
        /// <param name="mName">机器名称</param>
        /// <param name="tyName">任务类型名称</param>
        public WHReportRowModelA(TaskHour model,string taskName,string mName,string tyName)
        {
            cell =new string[14];
            cell[0] = model.TaskHourID.ToString();
            cell[1] = model.TaskID.ToString();
            cell[2] = taskName;
            cell[3] = model.MoldNumber;
            cell[4] = model.MachineCode;
            cell[5] = mName;

            cell[6] = model.Operater;
            //0正常开始  1暂停后重启  -1取消/删除任务 2 外发 3返工
            string rType = string.Empty;
            switch (model.RecordType)
            {
                case 0:
                    rType = "正常开始";
                    break;
                case 1:
                    rType = "暂停重启";
                    break;
                case -1:
                    rType = "任务取消";
                    break;
                case 2:
                    rType = "外发工时";
                    break;
                case 3:
                    rType = "返工工时";
                    break;
            }
            cell[7] = rType;
            cell[8] = model.TaskType.ToString();
            cell[9] = tyName;
            cell[10] = model.SemiTaskFlag;
            cell[11] = model.Time.ToString();
            cell[12] = model.StartTime.ToString("yyyy-MM-dd HH:mm");
            cell[13] = Toolkits.CheckZero(model.FinishTime)?"-" : model.FinishTime.ToString("yyyy-MM-dd HH:mm");
        }
    }
}