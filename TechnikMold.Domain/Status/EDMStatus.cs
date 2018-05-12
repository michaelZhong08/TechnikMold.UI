using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TechnikSys.MoldManager.Domain.Status
{
    public enum EDMStatus
    {
        未发布 = -99, 
        已接收 = -3,
        等待中 = -2,
        等待 = 0,
        正在加工 = 1,
        完成 = 2,
        升版 = 3,
        CAM取消 = 200
    }
}