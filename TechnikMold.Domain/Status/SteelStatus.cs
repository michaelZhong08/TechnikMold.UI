using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TechnikSys.MoldManager.Domain.Status
{
    public enum SteelStatus
    {
        未发布 = -99, 
        已接收 = -3,
        外发 = -2,
        暂停 = -1,
        等待 = 0,
        开始 = 1,
        结束 = 2,
        升版 = 3,
        CAM取消 = 200
    }
}