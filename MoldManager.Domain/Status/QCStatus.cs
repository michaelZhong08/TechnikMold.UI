using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TechnikSys.MoldManager.Domain.Status
{
    public enum QCStatus
    {
        未发布=-99, 
        暂停 = -1 ,
        准备 = 0 ,
        测量结束 = 1 ,        
        合格 = 2 ,
        返工 = 4, 
        有条件利用 = 10, 
        取消 = 200,
        任务取消 = 201 
    }
}