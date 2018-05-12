using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TechnikSys.MoldManager.Domain.Entity;

namespace TechnikSys.MoldManager.NX.Common
{
    public class MoldInfo
    {
        private WebServer _server;

        public MoldInfo(WebServer Server)
        {
            _server = Server;
        }

        public string GetProjectNumber(string MoldNumber)
        {
            string _url = "/Project/GetProjectNumber?MoldNumber="+MoldNumber;
            string _return = _server.ReceiveStream(_url);
            return _return;
        }

        /// <summary>
        /// 获取2D图纸配置
        /// </summary>
        /// <returns></returns>
        public string GetDrawingSetting()
        {
            string _url = "/Task/GetSetting?Name=2D_Drw_Type";
            string _path = _server.ReceiveStream(_url);
            return _path;
        }

        /// <summary>
        /// 设置2D图纸配置
        /// </summary>
        /// <param name="Value"></param>
        /// <returns></returns>
        public int SetDrawingSetting(string Value)
        {
            string _url = "/Task/SaveSetting?Name=2D_Drw_Type&Value=" + Value;
            int _result = Convert.ToInt32(_server.ReceiveStream(_url));
            return _result;
        }


        /// <summary>
        /// 获取QC文件类型
        /// </summary>
        /// <returns></returns>
        public string GetQCFileType()
        {
            string _url = "/Task/GetSetting?Name=QC_File_Type";
            string _path = _server.ReceiveStream(_url);
            return _path;
        }

        /// <summary>
        /// 设置QC文件类型，目前为PRT/STP
        /// </summary>
        /// <param name="Value"></param>
        /// <returns></returns>
        public int SetQCFileType(string Value)
        {
            string _url = "/Task/SaveSetting?Name=QC_File_Type&Value=" + Value;
            int _result = Convert.ToInt32(_server.ReceiveStream(_url));
            return _result;
        }


    }
}
