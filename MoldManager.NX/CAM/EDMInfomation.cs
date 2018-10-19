using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TechnikSys.MoldManager.Domain.Entity;
using TechnikSys.MoldManager.NX.Common;
using Newtonsoft.Json;
namespace TechnikSys.MoldManager.NX.CAM
{
    public  class EDMInfomation
    {

        private WebServer _server;

        public EDMInfomation(WebServer Server)
        {
            _server = Server;
        }

        public EDMInfomation(string ServerName, string Port)
        { 
            _server = new WebServer(ServerName, Port, "Le Chunming", "1qaz@WSX");
        }
        #region 放电图纸
        /// <summary>
        /// insert_update_ELE_SETTING_TABLE
        /// </summary>
        /// <param name="EDMSetting"></param>
        public  int SaveEDMSetting(EDMDetail EDMDetail)
        {

            string _url = "/Task/SaveEDMDetail";

            EDMDetail.Lock = 0;
            EDMDetail.Expire = false;
            EDMDetail.TaskID = 0;
            //EDMDetail.CreateDate = DateTime.Now;


            string _result = _server.SendObject(_url, "SettingData", EDMDetail);

            return Convert.ToInt16(_result);
        }

        /// <summary>
        /// select * from ELE_SETTING_TABLE where EDM_SETTING_NAME='{0}' and SETING_REV={1}
        /// </summary>
        /// <param name="SettingName"></param>
        /// <param name="Version"></param>
        /// <returns></returns>
        public  EDMDetail GetSetting(String SettingName, int Version)
        {
            string _url = "/Task/GetEDMDetail?SettingName=" + SettingName + "&Version=" + Version;

            string _result = _server.ReceiveStream(_url);

            EDMDetail _setting =JsonConvert.DeserializeObject<EDMDetail>(_result);

            return _setting;
        }
        #endregion

        #region 电极跑位检查
        /// <summary>
        /// Select * from ELE_GENERAL_TABLE where ELE_REV='{0}' AND ELE_NAME ='{1}' 
        /// </summary>
        /// <param name="EleName"></param>
        /// <param name="Version"></param>
        /// <returns></returns>
        public  List<CNCMachInfo> GetEDMTasks(string EleName, int Version) 
        {
            string _url = "/Task/GetMachInfo?EleName=" + EleName + "&Version=" + Version;
            
            string _data = _server.ReceiveStream(_url);
            List<CNCMachInfo> _machInfoes = JsonConvert.DeserializeObject<List<CNCMachInfo>>(_data);
            return _machInfoes;
        }
        #endregion
    }
}
