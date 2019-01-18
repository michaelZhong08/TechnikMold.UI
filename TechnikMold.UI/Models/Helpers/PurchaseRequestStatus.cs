using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MoldManager.WebUI.Models.Helpers
{
    public enum PurchaseRequestStatus
    {
        新建 = 1,
        待审批 = 5,
        审批通过 = 10,
        已同步=20,//HR用
        完成 = 100,
        拒绝=-99
    }
}