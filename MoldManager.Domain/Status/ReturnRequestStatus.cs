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
        新建=1,
        待审批=5,	//已提交
        拒绝 = -100,   //已审批
        通过=20,
        已关闭=-110
    }
}
