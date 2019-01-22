using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
//using System.Threading.Tasks;
using TechnikSys.MoldManager.Domain.Entity;
using TechnikSys.MoldManager.Domain.Output;
using TechnikSys.MoldManager.NX.Common;
using Newtonsoft.Json;

namespace TechnikSys.MoldManager.NX.CAM
{
    public  class SteelInformation
    {
        private WebServer _server;

        public SteelInformation(WebServer Server)
        {
            _server = Server;
        }

        public SteelInformation(string ServerName, string Port)
        {
            _server = new WebServer(ServerName, Port, "Le Chunming", "1qaz@WSX");
        }

        #region 铁件图纸
        /// <summary>
        /// PR_ConfirmLastestTaskFinished
        /// 
        /// </summary>
        /// <param name="DrawName"></param>
        /// <param name="Version"></param>
        /// <returns>
        /// 0--无上一个版本任务 
        /// 1--上一个版本的任务没有结束
        /// </returns>
        public  int LastestTaskFinished(string DrawName, int Version)
        {
            DrawName = DrawName.Replace("+", "%2B");
            string _url = "/Task/LastSteelFinished?DrawName=" + DrawName + "&Version=" + Version;
            string _result = _server.ReceiveStream(_url);
            return Convert.ToInt16(_result);
        }

        /// <summary>
        /// Select *from SystemConfig where SettingName='STEELPATH'
        /// GetNCFileServer
        /// </summary>
        /// <returns></returns>
        public  string GetPath()
        {
            string _url = "/Task/GetSetting?Name=STEELPATH";
            string _path = _server.ReceiveStream(_url);
            return _path.Trim();
        }


        /// <summary>
        /// PR_InsertOrUpdateSteelDrawing
        /// </summary>
        /// <param name="FullPartName"></param>
        /// <param name="DrawName"></param>
        /// <param name="Version"></param>
        /// <param name="CADPartName"></param>
        /// <param name="MoldName"></param>
        /// <param name="Programmer"></param>
        /// <param name="UpdateProgram"></param>
        /// <returns></returns>
        public  int SaveSteelDrawing(string FullPartName, string DrawName, int Version,
            string CADPartName, string MoldName, string Programmer, bool UpdateProgram,double Time, ref int ID)
        {
            DrawName = DrawName.Replace("+", "%2B");
            string _url = "/Task/UpdateSteelDrawing?FullPartName=" + FullPartName + "&DrawName=" + DrawName
                +"&Version="+Version+"&CADPartName="+CADPartName+"&MoldName="+MoldName+"&Programmer="+Programmer
                +"&UpdateProgram="+UpdateProgram+ "&Time="+ Time;
            string _result = _server.ReceiveStream(_url);

            string[] _content = _result.Split(',');
            ID = Convert.ToInt16( _content[1]);
            return Convert.ToInt16(_content[0]);
        }

        /// <summary>
        /// PR_InsertSteelGroupProgramme
        /// </summary>
        /// <param name="DrawingID"></param>
        /// <param name="GroupName"></param>
        /// <param name="Time"></param>
        /// <returns></returns>
        public  int SaveSteelGroupProgram(int DrawingID, string GroupName, double Time)
        {
            string _url = "/Task/SaveSteelGroupProgram?DrawingID=" + DrawingID + "&GroupName=" + GroupName + "&Time=" + Time;

            int _result = Convert.ToInt32( _server.ReceiveStream(_url));
            return _result;
        }

        /// <summary>
        /// PR_InsertSteelProgrammeList
        /// </summary>
        /// <param name="GroupID"></param>
        /// <param name="ProgramName"></param>
        /// <param name="FileName"></param>
        /// <param name="ToolName"></param>
        /// <param name="Time"></param>
        /// <param name="Depth"></param>
        /// <param name="Sequence"></param>
        /// <param name="HaveFile"></param>
        public  void SaveSteelItemProgram(int GroupID, string ProgramName, string FileName,
            string ToolName, double Time, double Depth, int Sequence, bool HaveFile)
        {
            string _url = "/Task/SaveSteelProgram?GroupID=" + GroupID + "&ProgramName=" + ProgramName + "&FileName=" + FileName + "&ToolName=" + ToolName
                + "&Time=" + Time + "&Depth=" + Depth + "&Sequence=" + Sequence + "&HaveFile=" + HaveFile;
            int _result = Convert.ToInt32(_server.ReceiveStream(_url));
            
        }

        /// <summary>
        /// select *from Steel_CAM_drawing_table where DrawName='{0}' and DrawREV={1} and active=1
        /// 根据图纸名和版本查询系统中是否存在图纸
        /// </summary>
        /// <param name="DrawName"></param>
        /// <param name="Version"></param>
        /// <returns></returns>
        public  IEnumerable<SteelCAMDrawing> QuerySteelDrawing(string DrawName, int Version)
        {
            DrawName = DrawName.Replace("+", "%2B");
            string _url = "/Task/GetSteelDrawing?DrawName=" + DrawName + "&DrawRev=" + Version;
            string _return = _server.ReceiveStream(_url);
            IEnumerable<SteelCAMDrawing> _camDrawings = JsonConvert.DeserializeObject<IEnumerable<SteelCAMDrawing>>(_return);
            return _camDrawings;
        }

        /// <summary>
        /// PR_AddSteelTask_R01
        /// 添加铣铁任务
        /// 0--成功 1--已有该任务 2--失败
        /// </summary>
        /// <param name="GroupID"></param>
        /// <param name="Note"></param>
        /// <param name="CreateBy"></param>
        /// <returns></returns>
        public  int CreateSteelTask(int GroupID, string Note, string CreateBy)
        {
            //UserInfo  _userInfo = new UserInfo(_server);
            //int _userid = _userInfo.GetUserID(CreateBy);
            string _url = "/Task/CreateSteelTask?GroupID=" + GroupID + "&Note=" + Note + "&CreateBy=" + CreateBy;//_userid;
            string _result = _server.ReceiveStream(_url);
            return Convert.ToInt16(_result);
        }

        /// <summary>
        /// select *from Steel_CNC_Task where status<2 and GroupID={0} and active=1
        /// 查询当前铣铁程序是否已经有对应的有效任务
        /// </summary>
        /// <param name="GroupID"></param>
        /// <returns></returns>
        public  IEnumerable<Task> GetTask(int GroupID)
        {
            string _url = "/Task/GetTaskByGroupID?GroupID=" + GroupID;
            string _return = _server.ReceiveStream(_url);

            IEnumerable<Task> _tasks = JsonConvert.DeserializeObject<IEnumerable<Task>>(_return);
            return null;
        }


        /// <summary>
        /// SELECT Steel_GroupProgramme_list.GroupName, Steel_GroupProgramme_list.Time, Steel_CAM_drawing_table.IssueDate, 
        /// Steel_CAM_drawing_table.DrawLock, Steel_CAM_drawing_table.LastestFlat, Steel_GroupProgramme_list.ID
        /// FROM Steel_CAM_drawing_table INNER JOIN
        ///  Steel_GroupProgramme_list ON Steel_CAM_drawing_table.NCID = Steel_GroupProgramme_list.NCID
        /// WHERE (Steel_CAM_drawing_table.DrawName = '{0}') 
        /// AND (Steel_CAM_drawing_table.DrawREV = {1}) AND (Steel_GroupProgramme_list.Time>0) AND (Steel_CAM_drawing_table.active = 1)
        /// 查询铣铁任务信息
        /// ‘</summary>
        /// <param name="DrawName">图纸名</param>
        /// <param name="Version"></param>
        /// <returns></returns>
        public  List<SteelDrawing> GetSteelTaskInfo(string DrawName, int Version)
        {
            DrawName = DrawName.Replace("+", "%2B");
            string _url = "/Task/SteelProgramInfo?DrawName=" + DrawName + "&DrawRev=" + Version;
            string _return = _server.ReceiveStream(_url);
            List<SteelDrawing> _steelDrawings = JsonConvert.DeserializeObject<List<SteelDrawing>>(_return);
            return _steelDrawings;
        }

        #endregion

    }
}
