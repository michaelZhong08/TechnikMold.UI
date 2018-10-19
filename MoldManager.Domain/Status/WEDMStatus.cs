using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TechnikSys.MoldManager.Domain.Status
{
    public enum WEDMStatus
    {
        已接收 = 1,
        CAM取消 = -200,
        未发布 = -99,
        暂停 = -1,
        等待 = 0,
        正在加工 = 10,
        外发 = 11,
        完成 = 100,
        已升版 = 200
    }
}