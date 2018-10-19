using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TechnikSys.MoldManager.Domain.Status
{
    public enum WEDMStatus
    {
        未发布 = -99, 
        已收到 = -3,
        暂停 = -1,
        等待 = 0,
        开始 = 1,
        外发 = 2,
        结束 = 3,
        取消 = 4,
        删除 = 5
    }
}