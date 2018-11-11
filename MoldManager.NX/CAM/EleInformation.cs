using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
//using System.Threading.Tasks;
using TechnikSys.MoldManager.Domain.Entity;
using TechnikSys.MoldManager.NX.Common;
using Newtonsoft.Json;
using TechnikSys.MoldManager.Domain.Status;

namespace TechnikSys.MoldManager.NX.CAM
{
    public  class EleInformation
    {

        private WebServer _server;

        public EleInformation(WebServer Server)
        {
            _server = Server;
        }

        public EleInformation(string ServerName, string Port)
        {
            _server = new WebServer(ServerName, Port, "Le Chunming", "1qaz@WSX");
        }

        #region 电极图纸
        /// <summary>
        /// PR_CAMInsertOrUpdateEleInformation_R02  
        /// Create or update EleInformation(CNCMachInfoes table records）
        /// Create or update Ele related tasks(Tasks table record)
        /// </summary>
        /// <param name="Task"></param>
        /// <param name="MachInfo"></param>
        public void Update(Task Task,
            CNCMachInfo MachInfo, 
            int NcProgrameOption, 
            int ElePosOption, 
            ref string Position, 
            ref int Ele_index, 
            ref bool NCFile, 
            string CADUser, 
            string CAMUser) 
        {

            string _url = "/Task/SaveUGTask";

            Task.DrawingFile = Task.DrawingFile == null ? "" : Task.DrawingFile;
            //if ((Task.TaskName == null) || (Task.TaskName == ""))
            //{
            //    Task.TaskName = Task.ProcessName;
            //}
            //else
            //{
            //    Task.TaskName = Task.TaskName;
            //}
            Task.ProcessName = MachInfo.MachType;
            Task.HRC = Task.HRC == null ? "" : Task.HRC;
            Task.Memo = Task.Memo == null ? "" : Task.Memo;
            Task.StateMemo = Task.StateMemo == null ? "" : Task.StateMemo;
            //181016 MoldNumber由NX端传值
            //Task.MoldNumber = Task.TaskName.Substring(0, Task.TaskName.IndexOf('_'));
            Task.TaskType = 1;
            Task.State = (int)TaskStatus.未发布;
            Task.Enabled = true;
            UserInfo _userInfo = new UserInfo(_server);
            Task.CADUser = _userInfo.GetUserID(CADUser);
            Task.CAMUser = _userInfo.GetUserID(CAMUser);

            string _return = _server.SendObject(_url, "CAMTask", Task);


             _url = "/Task/SaveCNCMachInfo";
            double _safeHeight = GetSafeHeight(Task.Raw);

            bool _qcPoint = QCPointProgramExist(Task.Model, Task.Version);


            if (MachInfo.Model == null)
            {
                MachInfo.Model = Task.Model;
            }

            MachInfo.QCPoint = _qcPoint;
            MachInfo.SafetyHeight = _safeHeight;
            string data = _server.SendObject(_url, "MachInfo", MachInfo);
            CNCMachInfo _machInfo = JsonConvert.DeserializeObject<CNCMachInfo>(data);
            Position = _machInfo.Position;
            Ele_index = _machInfo.DrawIndex;            
            
            if ((_machInfo.RoughName == "") && (_machInfo.FinishName == ""))
            {
                NCFile = false;
            }
        }

        /// <summary>
        /// Select *from SystemConfig where SettingName='ELEPATH'
        /// </summary>
        /// <returns></returns>
        public   string ElePath() {
            string _url = "/Task/GetSetting?Name=ELEPATH";
            string _path = _server.ReceiveStream(_url);
            return _path;
        }

        /// <summary>
        /// MACH_AskEleDrawLockStatus_R01
        /// Check whether the ele is locked
        /// </summary>
        /// <param name="DrawName"></param>
        /// <param name="DrawVersion"></param>
        /// <param name="EleVersion"></param>
        /// <returns></returns>
        public  int EleStatus(string DrawName, int DrawVersion, int EleVersion){
            DrawName = DrawName.Replace("+", "%2B");
            string _url = "/Task/GetLockStatus?Model=" + DrawName + "&Version=" + EleVersion + "&DrawRev=" + DrawVersion;

            string _result = _server.ReceiveStream(_url);

            return Convert.ToInt16(_result);
        }
        #endregion

        #region PRIVATE DB Operation

        private bool QCPointProgramExist(string Part3DName, int Version)
        {
            string _url = "/Task/GetQCProgram?Part3DName="+Part3DName+"&Version="+Version;

            QCSteelPoint _program = JsonConvert.DeserializeObject<QCSteelPoint>( _server.ReceiveStream(_url));

            if (_program == null)
            {
                return true;
            }
            else
            {
                return false;
            }
        }


        #endregion

        #region Private Methods

        private double GetSafeHeight(string Raw)
        {
            try
            {
                int _begin = Raw.IndexOf('(') + 1;
                int _end = Raw.IndexOf(')');
                string _sh = Raw.Substring(_begin, _end - _begin);
                return double.Parse(_sh);
            }
            catch
            {
                return 0;
            }
        }
        #endregion
    }
}
