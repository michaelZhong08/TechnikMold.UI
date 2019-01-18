using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TechnikSys.MoldManager.Domain.Status
{
    public enum ProjectStatus
    {
        删除=-99,
        CAD新建 = 0,
        //设置好全部Plan
        新建 = 1,
        启动 = 2,
        暂停 = 3,
        完成 = 4
    }
}
