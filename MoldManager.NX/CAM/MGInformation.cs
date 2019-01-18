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
    public class MGInformation
    {
        private WebServer _server;

        public MGInformation(WebServer Server)
        {
            _server = Server;
        }

        public MGInformation(string ServerName, string Port)
        {
            _server = new WebServer(ServerName, Port, "Le Chunming", "1qaz@WSX");
        }
        #region MG CAMSetting
        public int AddOrUpdateMGDrawing(MGSetting entity)
        {
            try
            {
                string _url = "/Task/SaveService_MGCAMSetting";
                string _return = _server.SendObject(_url, "entity", entity);
                return Convert.ToInt32(_return);
            }
            catch
            {
                return 2;
            }
        }
        public bool DeleteSettingByName(string partname, int rev)
        {
            try
            {
                string _url = "/Task/DelByNameService_MGCAMSetting?partname=" + partname + "&rev=" + rev.ToString();
                bool res = JsonConvert.DeserializeObject<bool>(_server.ReceiveStream(_url));
                return res;
            }
            catch
            {
                return false;
            }
        }
        public int ReleaseMGDrawing(int DrawIndex, string ReleaseBy,string TaskName,string Memo="")
        {
            try
            {
                string _url = "/Task/ReleaseMGDrawingService?DrawIndex=" + DrawIndex.ToString() + "&ReleaseBy=" + ReleaseBy+ "&TaskName="+ TaskName+"&Memo="+ Memo;
                int res = JsonConvert.DeserializeObject<int>(_server.ReceiveStream(_url));
                return res;
            }
            catch
            {
                return 1;
            }
        }
        public List<MGSetting> AskMGPartListByMold(string MoldNo, bool bRelease)
        {
            try
            {
                string _url = "/Task/GetService_MGTypeMold?MoldNo=" + MoldNo + "&bRelease=" + bRelease.ToString();
                List<MGSetting> res = JsonConvert.DeserializeObject<List<MGSetting>>(_server.ReceiveStream(_url));
                return res;

            }
            catch (Exception ex)
            {
                return null;
            }
        }
        public string GetDrawFileByDrawName(string DrawName, bool IsContain2D,string DrawType)
        {
            try
            {
                string _url = "/Task/GetDrawFileByDrawName?DrawName=" + DrawName + "&IsContain2D=" + IsContain2D.ToString()+ "&DrawType="+ DrawType;
                string res = JsonConvert.DeserializeObject<string>(_server.ReceiveStream(_url));
                return res;
            }
            catch
            {
                return "";
            }
        }
        public bool IsLatestDrawFile(string DrawName, bool IsContain2D,string DrawType)
        {
            try
            {
                string _url = "/Task/IsLatestDrawFile?DrawName=" + DrawName + "&IsContain2D=" + IsContain2D.ToString()+ "&DrawType="+ DrawType;
                bool res = JsonConvert.DeserializeObject<bool>(_server.ReceiveStream(_url));
                return res;
            }
            catch
            {
                return false;
            }
        }
        #endregion
        #region MG TypeName
        public List<MGTypeName> AskMGTypeName()
        {
            try
            {
                string _url = "/Task/GetService_MGTypeName";
                string _data = _server.ReceiveStream(_url);
                List<MGTypeName> _mgtypeNames = JsonConvert.DeserializeObject<List<MGTypeName>>(_data);
                return _mgtypeNames;

            }
            catch
            {
                return null;
            }
        }

        #endregion
    }
}
