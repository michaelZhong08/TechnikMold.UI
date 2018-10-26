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
        暂停=99,
        完成=100,
        取消=-99
    }
}
