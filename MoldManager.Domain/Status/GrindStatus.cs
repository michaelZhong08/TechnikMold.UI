using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TechnikSys.MoldManager.Domain.Status
{
    public enum GrindStatus
    {
        CAM取消 = -200,
        未发布 = -99, 
        暂停 =-1,
        等待 = 0,
        已接收 = 1,
        正在加工 = 10,
        外发 = 11,
        完成 = 100,
        已升版 = 200          
    }
}
