using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
//using System.Threading.Tasks;
using TechnikSys.MoldManager.Domain.Entity;
using TechnikSys.MoldManager.NX.Common;
using Newtonsoft.Json;
using TechnikSys.MoldManager.Domain.Output;

namespace TechnikSys.MoldManager.NX.CAM
{
    public class WEDMInformation
    {
        private WebServer _server;

        public WEDMInformation(WebServer Server)
        {
            _server = Server;
        }

        public WEDMInformation(string ServerName, string Port)
        {
            _server = new WebServer(ServerName, Port, "Le Chunming", "1qaz@WSX");
        }
        public int AddOrUpdateWEDMDrawing(WEDMSetting entity)
        {
            try
            {
                string _url = "/Task/SaveService_WEDMCAMSetting";
                string _return = _server.SendObject(_url, "entity", entity);
                return Convert.ToInt32(_return);
            }
            catch
            {
                return 2;
            }
        }
        public int ReleaseWEDMDrawing(int DrawIndex, string ReleaseBy, int Qty = 1)
        {
            try
            {
                string _url = "/Task/ReleaseWEDMDrawingService?DrawIndex=" + DrawIndex.ToString() + "&ReleaseBy=" + ReleaseBy + "&Qty=" + Qty.ToString();
                int res = JsonConvert.DeserializeObject<int>(_server.ReceiveStream(_url));
                return res;
            }
            catch
            {
                return 1;
            }
        }
        public bool DeleteSettingByName(string partname, int rev)
        {
            try
            {
                string _url = "/Task/DelByNameService_WEDMCAMSetting?partname=" + partname + "&rev=" + rev.ToString();
                bool res = JsonConvert.DeserializeObject<bool>(_server.ReceiveStream(_url));
                return res;
            }
            catch
            {
                return false;
            }
        }
        public List<WEDMTaskInfo> AskWEDMTaskByMoldAndStatus(string MoldNo, int Status = -2, int PlanID = 0)
        {
            try
            {
                string _url = "/Task/GetService_WEDMTaskByMoldAndStatus?MoldNo=" + MoldNo + "&Status=" + Status.ToString() + "&PlanID=" + PlanID.ToString();
                List<WEDMTaskInfo> wss = JsonConvert.DeserializeObject<List<WEDMTaskInfo>>(_server.ReceiveStream(_url));
                return wss;
            }
            catch
            {
                return null;
            }
        }
        public WEDMCutSpeed GetCutSpeed(double Thickness, int CutTypeID)
        {
            try
            {
                string _url = "/Task/GetService_WDMCutSpeed?Thickness=" + Thickness + "&CutTypeID=" + CutTypeID.ToString();
                WEDMCutSpeed wcs = JsonConvert.DeserializeObject<WEDMCutSpeed>(_server.ReceiveStream(_url));
                return wcs;
            }
            catch
            {
                return null;
            }
        }
        public List<WEDMPrecision> GetTypeList()
        {
            try
            {
                string _url = "/Task/GetService_Precision";
                List<WEDMPrecision> wps = JsonConvert.DeserializeObject<List<WEDMPrecision>>(_server.ReceiveStream(_url));
                return wps;
            }
            catch
            {
                return null;
            }
        }
        public string Get3DDrawingServerPath()
        {
            try
            {
                string _url = "/Task/GetService_3DDrawingServerPath";
                string path = JsonConvert.DeserializeObject<string>(_server.ReceiveStream(_url));
                return path;
            }
            catch
            {
                return null;
            }
        }
    }
}
