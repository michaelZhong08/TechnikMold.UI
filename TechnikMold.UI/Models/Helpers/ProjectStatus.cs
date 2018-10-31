using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MoldManager.WebUI.Models.Helpers
{
    public enum ProjectStatus
    {
        CAD新建=0,
        //设置好全部Plan
        新建=1, 
        启动=2,
        暂停=3, 
        完成=4
    }
}