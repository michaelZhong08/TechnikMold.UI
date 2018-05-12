using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MoldManager.WebUI.Models.Helpers
{
    public enum PurchaseOrderStatus
    {
        
        取消=-99,
        新建 = 1,
        待审批 = 2,
        发布 = 3,
        部分收货 = 4,
        完成 = 5,
        拒绝=99,
        外发待出库=10

    }
}