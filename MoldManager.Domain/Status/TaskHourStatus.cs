using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TechnikSys.MoldManager.Domain.Status
{
    public enum TaskHourStatus
    {
        开始=0,
        外发=11,
        任务等待 = 80,
        暂停 =90,
        完成=100,
        完成记录 = 110,//工时period设置好之后
        取消 =-99
    }
}
