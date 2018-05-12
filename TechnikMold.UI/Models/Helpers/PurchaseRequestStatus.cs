using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MoldManager.WebUI.Models.Helpers
{
    public enum PurchaseRequestStatus
    {
        新建 = 1,
        待审批 = 2,
        审批通过 = 3,
        完成 = 4,
        拒绝=99
    }
}