using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MoldManager.WebUI.Models.Helpers
{
    public class TaskStatus
    {
        private Dictionary<int, string> Status;

        public TaskStatus()
        {
            Status = new Dictionary<int, string>();
            //Task Created by CAM
            Status.Add(1, "新建");
            //Task released by CAM
            Status.Add(2, "接受");
            //Task accepted by operator
            Status.Add(3, "发布");     
            //Task in queue
            Status.Add(4, "等待");
            //Task in progress
            Status.Add(5, "进行中");
            //Task paused
            Status.Add(6, "暂停");
            //Task outsourcing
            Status.Add(7, "外发");

            //Task in QC
            Status.Add(8, "质检");
            //QCPass
            Status.Add(9, "质检完成");
            //Item Accepted
            Status.Add(10, "已接收");

            //Task Finished
            Status.Add(90, "完成");
            //PO Rejected by PU
            Status.Add(99, "取消");
        }

        public string GetStatusName(int StatusID)
        {
            return Status[StatusID];
        }
    }
}