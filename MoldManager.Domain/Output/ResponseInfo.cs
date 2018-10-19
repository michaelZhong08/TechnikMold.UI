using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TechnikSys.MoldManager.Domain.Entity;

namespace TechnikSys.MoldManager.Domain.Output
{
    public class ResponseInfo
    { 
        /// <summary>
        /// 状态
        /// </summary>
        public int Status { get; set; }
        /// <summary>
        /// 消息内容
        /// </summary>
        public string Message { get; set; }

        public List<Part> Data { get; set; }
    }
}
