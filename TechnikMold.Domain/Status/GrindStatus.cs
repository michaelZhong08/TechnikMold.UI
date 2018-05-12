using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TechnikSys.MoldManager.Domain.Status
{
    public enum GrindStatus
    {
        未发布 = -99,
        等待 = 0,
        正在加工 = 1,
        完成 = 2,
        升版 = 3,
        CAM取消 = 200
    }
}
