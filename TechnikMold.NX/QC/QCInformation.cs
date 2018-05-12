using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TechnikSys.MoldManager.Domain.Entity;
using TechnikSys.MoldManager.NX.Common;
using Newtonsoft.Json;

namespace TechnikSys.MoldManager.NX.QC
{
    public  class QCInformation
    {
        private WebServer _server;

        public QCInformation(WebServer Server)
        {
            _server = Server;
        }

        public QCInformation(string ServerName, string Port)
        {
            _server = new WebServer(ServerName, Port, "Le Chunming", "1qaz@WSX");
        }
        #region 电极测点
        /// <summary>
        /// PR_AddOrUpdateSteelPoint
        /// </summary>
        /// <param name="QCSteelPoint"></param>
        /// <returns></returns>
        public  int SaveSteelPoint(QCSteelPoint QCSteelPoint)
        {
            string _url = "/Task/SaveSteelPoint";
            string _value= _server.SendObject(_url, "SteelPoint", QCSteelPoint);
            int _id = Convert.ToInt32(_value);
            return _id;
        }


        /// <summary>
        /// SELECT *FROM SystemConfig WHERE SettingName='QCPointPath'
        /// </summary>
        /// <returns></returns>
        public  string GetPath() {
            string _url = "/Task/GetSetting?Name=QCPointPath";
            string _path = _server.ReceiveStream(_url);
            return _path;
        }

        /// <summary>
        /// PR_AddOrUpdateQCPointProgram
        /// </summary>
        /// <param name="QCProgram"></param>
        /// <returns></returns>
        public  bool SaveQCPointProgram(QCPointProgram QCProgram)
        {
            string _url = "/Task/SaveQCPointProgram";
            try
            {
                string _id = _server.SendObject(_url, "QCPointProgram", QCProgram);
                return true;
            }
            catch
            {
                return false;
            }
        }

        /// <summary>
        /// Select *from QC_SteelPoint where PartName='{0}' and Rev<={1} and active=1 Order by Rev desc
        /// </summary>
        /// <param name="PartName"></param>
        /// <param name="Version"></param>
        /// <returns></returns>
        public  List<QCSteelPoint> GetQCSteelPoints(string PartName, int Version)
        {
            string _url = "/Task/GetQCSteelPoints?PartName=" + PartName + "&Version=" + Version;
            List<QCSteelPoint> _result;
            try
            {
                string _data = _server.ReceiveStream(_url);
                _result = JsonConvert.DeserializeObject<List<QCSteelPoint>>(_data);
                return _result;
            }
            catch
            {
                return null;
            }
            
        }

        /// <summary>
        /// select XYZFlieName from QC_PointProgram where [3DPart]='{0}' and [3DPartRev]={1}
        /// </summary>
        /// <param name="PartName3D"></param>
        /// <param name="Version"></param>
        /// <returns></returns>
        public  string GetXYZFileNames(string PartName3D, int Version)
        {
            string _url = "/Task/GetXYZFile?PartName3D=" + PartName3D + "&Version=" + Version;
            string _xyzFile = "";
            try
            {
                _xyzFile =  _server.ReceiveStream(_url);
                return _xyzFile;
            }
            catch
            {
                return "";
            }
            
        }

        /// <summary>
        /// Update ELE_GENERAL_TABLE Set PosCheck=1 WHERE (ELE_NAME = '{0}') AND (DRAW_REV = {1})
        /// </summary>
        /// <param name="EleName"></param>
        /// <param name="Version"></param>
        /// <returns></returns>
        public int UpdatePosFinishFlag(string EleName, int Version)
        {
            string _url = "/Task/UpdatePosFinishFlag?EleName=" + EleName + "&Version=" + Version;
            string _return = _server.ReceiveStream(_url);
            return Convert.ToInt16(_return);
        }

        #endregion

    }
}
