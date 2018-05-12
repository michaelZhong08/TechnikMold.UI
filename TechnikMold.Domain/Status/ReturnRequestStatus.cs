using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TechnikSys.MoldManager.Domain.Status
{
    public enum ReturnRequestStatus
    {
        取消=-99,
        新建=-90,
        待审批=0,	//已提交
        拒绝 = 10,   //已审批
        通过=20,
        已关闭=100
    }
}
