using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TechnikSys.MoldManager.Domain.Entity;
using TechnikSys.MoldManager.Domain.Output;
using MoldManager.QC.Common;
using Newtonsoft.Json;

namespace MoldManager.QC.QCCheck
{
    public class QCInformation
    {
        private WebServer _server;

        public QCInformation(WebServer WebServer)
        {
            _server = WebServer;
        }


        /// <summary>
        /// Private Sub RefereshTiejianList(ByVal FilterStr As String)
        /// Private Sub RefereshDianjiList(ByVal FilterStr As String)
        /// Private Sub RefereshELECheckList(ByVal FilterStr As String)
        /// Private Sub RefereshEleHistory(ByVal FilterStr As String)
        /// Private Sub RefereshSteelHistory(ByVal FilterStr As String)
        /// 
        /// </summary>
        /// <param name="TaskType">
        /// 1:电极任务
        /// >1 铣铁，放电
        /// </param>
        /// <param name="Keyword">查询关键字， 不带*号</param>
        /// <returns></returns>
        public List<string> GetMoldNumber(int TaskType, string Keyword = "", string States="")
        {
            string _url = "/task/GetQCMoldNumber?TaskType=" + TaskType.ToString() + "&Keyword=" + Keyword + "&States="+States;
            List<string> _data = JsonConvert.DeserializeObject<List<string>>(_server.ReceiveStream(_url));

            return _data;
        }

        /// <summary>
        /// Private Sub ReTiejianLabel(ByVal LStr As String, ByVal Fstr As String)
        /// Private Sub ReSteelLabelHistory(ByVal MoldName As String, ByVal FilterStr As String)
        /// 显示铁件/放电检测任务列表
        /// </summary>
        /// <param name="Keyword">查询关键字， 不带*号</param>
        /// <returns>QC任务集合</returns>
        public List<QCTask> QueryQCTask(string MoldNumber, string Keyword, string State)
        {
            string _url = "/Task/GetQCTasks?MoldNumber=" + MoldNumber + "&TaskType=2&Keyword=" + Keyword + "&State=" + State;
            List<QCTask> _qcTasks = JsonConvert.DeserializeObject<List<QCTask>>(_server.ReceiveStream(_url));
            foreach (QCTask _task in _qcTasks)
            {
                _task.FinishTime = _task.FinishTime.AddHours(8);
            }
            return _qcTasks;
        }

        /// <summary>
        /// Private Sub ReDianjiLabel(ByVal Lstr As String, ByVal Fstr As String)
        /// 显示电极检测任务列表
        /// </summary>
        /// <param name="Keyword">关键字</param>
        /// <returns></returns>
        public List<QCTask> QueryEleQCTask(string MoldNumber, string Keyword, string State)
        {
            string _url = "/Task/GetQCTasks?MoldNumber=" + MoldNumber + "&TaskType=1&Keyword=" + Keyword + "&State=" + State;
            List<QCTask> _qcTasks = JsonConvert.DeserializeObject<List<QCTask>>(_server.ReceiveStream(_url));
            return _qcTasks;
        }

        /// <summary>
        /// Private Sub ReELECheckLabel(ByVal Lstr As String)
        /// 根据模具号获取已检测完成电极任务
        /// </summary>
        /// <param name="MoldNumber"></param>
        /// <returns></returns>
        public List<CNCItem> GetFinishedTask(string MoldNumber, string State, string Keyword)
        {
            string _url = "/Task/GetFinishedQCTasks?MoldNumber=" + MoldNumber+"&State="+State+"&Keyword="+Keyword;
            List<CNCItem> _qcTasks = JsonConvert.DeserializeObject<List<CNCItem>>(_server.ReceiveStream(_url));
            return _qcTasks;
        }

        /// <summary>
        /// Private Sub ELECheckGridView_CellContentClick
        /// Private Sub ELEHGridView_CellContentClick
        /// Private Sub LkOpen_Click
        /// 获取具体某一电极对应的电极加工任务名、版本和模具号
        /// </summary>
        /// <param name="CNCItemID">电极表ID号</param>
        /// <returns></returns>
        public QCTask GetEleDrawing(int QCTaskID)
        {
            string _url = "/Task/GetTaskByQCTaskID?QCTaskID=" + QCTaskID;
            QCTask _qctask = JsonConvert.DeserializeObject<QCTask>(_server.ReceiveStream(_url));
            return _qctask;
        }

        public QCTask GetEleDrawing(string LabelName)
        {
            string _url = "/Task/GetTaskByLabelName?LabelName=" + LabelName;
            QCTask _qctask = JsonConvert.DeserializeObject<QCTask>(_server.ReceiveStream(_url));
            return _qctask;
        }
        /// <summary>
        /// 通过电极索引索引 Get LabelName
        /// </summary>
        /// <param name="ELE_IndexCode"></param>
        /// <returns></returns>
        public string GetLabelNameByELE_Index(string ELE_IndexCode)
        {
            string _url = "/Task/GetLabelNameByELE_Index?ELE_IndexCode=" + ELE_IndexCode;
            string LabelName = _server.ReceiveStream(_url);
            return LabelName ?? "";
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="ELE_IndexCode"></param>
        /// <returns></returns>
        public CNCItem GetCNCItemsByELE_Index(string ELE_IndexCode)
        {
            string _url = "/Task/GetCNCItemsByELE_Index?ELE_IndexCode=" + ELE_IndexCode;
            CNCItem _item = JsonConvert.DeserializeObject<CNCItem>(_server.ReceiveStream(_url));
            return _item;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="ELE_IndexCode"></param>
        /// <returns></returns>
        public QCTask GetEleDrawingByELE_Index(string ELE_IndexCode)
        {
            string _url = "/Task/GetEleDrawingByELE_Index?ELE_IndexCode=" + ELE_IndexCode;
            QCTask _item = JsonConvert.DeserializeObject<QCTask>(_server.ReceiveStream(_url));
            return _item;
        }
        /// <summary>
        /// Private Sub ReSteelLabelHistory(ByVal MoldName As String, ByVal FilterStr As String)
        /// 获取铁件检测任务信息
        /// </summary>
        /// <param name="MoldNumber"></param>
        /// <param name="Keyword"></param>
        /// <returns></returns>
        public List<CNCItem> GetSteelTasks(string MoldNumber = "", string Keyword = "")
        {
            string _url = "/Task/GetEleInfo?MoldNumber=" + MoldNumber + "&Keyword=" + Keyword;
            List<CNCItem> _items = JsonConvert.DeserializeObject<List<CNCItem>>(_server.ReceiveStream(_url));
            return _items;
        }




        /// <summary>
        /// Private Sub TS_TJ_PRIORITY_Click
        /// Private Sub TS_DJ_PRIORITY_Click
        /// 更新任务优先级
        /// </summary>
        /// <param name="QCTaskID"></param>
        /// <param name="Priority"></param>
        public string UpdatePriority(string QCTaskIDs, int Priority)
        {
            string _url = "/Task/UpdatePriority?QCTaskIDs=" + QCTaskIDs + "&Priority=" + Priority;
            string _error = _server.ReceiveStream(_url);
            return _error;
        }
        /// <summary>
        /// Private Sub TS_TJ_FINISH_Click
        /// 
        /// </summary>
        /// <param name="UserName"></param>
        /// <param name="QCTaskIDs"></param>
        public void FinishSteelQCTask(string UserName, string QCTaskIDs)
        {
            string _url = "/Task/FinishQCTask?QCTaskIDs=" + QCTaskIDs + "&QCUserName=" + UserName;
            string _errors = _server.ReceiveStream(_url);
        }


        /// <summary>
        /// Private Sub ELECheckFinish_Click
        /// Private Function SetTaskNotOKFlat(ByVal strName As String) As Boolean
        /// 设置电极检测任务完成并发送到EDM部门
        /// </summary>
        /// <param name="LabelName"></param>
        /// <param name="UserName"></param>
        /// <param name="Status">
        /// 2:合格
        /// 4：返工
        /// 10：EDM确认
        /// </param>
        public void FinishEleQCTask(string LabelName, string UserName, int Status)
        {
            string _url = "/Task/ConfirmEleQCTask?LabelName=" + LabelName + "&UserName=" + UserName + "&Status=" + Status;
            _server.ReceiveStream(_url);
        }


        /// <summary>
        /// Private Function RestartTask(ByVal TaskID As Integer) As Boolean
        /// 重新开始任务
        /// </summary>
        /// <param name="QCTaskID"></param>
        public int RestartQCTask(int QCTaskID, string UserName)
        {
            string _url = "/Task/RestartTask?QCTaskID=" + QCTaskID.ToString() + "&UserName=" + UserName;
            int _result = Convert.ToInt16(_server.ReceiveStream(_url));
            return _result;
        }

        /// <summary>
        /// Public Function getpDFServerPath() As String
        /// 获取图纸保存位置信息
        /// </summary>
        /// <returns></returns>
        public string GetPDFServerPath()
        {
            string _url = "/Task/GetSetting?Name=PDFDrawing";
            string _path = _server.ReceiveStream(_url);
            return _path.Trim();
        }

        /// <summary>
        /// 获取QC文件类型
        /// </summary>
        /// <returns></returns>
        public string GetQCFileType()
        {
            string _url = "/Task/GetSetting?Name=QC_File_Type";
            string _path = _server.ReceiveStream(_url);
            return _path.Trim();
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

        /// <summary>
        /// Public Function GetQCReportsServerPath() As String
        /// 获取QC报告保存位置
        /// </summary>
        /// <returns></returns>
        public string GetQCReportsServerPath()
        {

            string _url = "/Task/GetSetting?Name=QCReport";
            string _path = _server.ReceiveStream(_url);
            return _path.Trim();
        }



        /// <summary>
        /// Private Sub Search_Scaner_Ele(ByVal strID As String)
        /// 获取电极信息
        /// </summary>
        /// <param name="CNCItemID"></param>
        /// <returns></returns>
        public CNCItem GetCNCItem(int CNCItemID)
        {
            string _url = "/Task/JsonCNCItem?CNCItemID=" + CNCItemID;
            CNCItem _item = JsonConvert.DeserializeObject<CNCItem>(_server.ReceiveStream(_url));
            return _item;
        }

        public CNCItem GetCNCItem(string LabelName)
        {
            string _url = "/Task/JsonCNCItemLabel?LabelName=" + LabelName;
            CNCItem _item = JsonConvert.DeserializeObject<CNCItem>(_server.ReceiveStream(_url));
            return _item;
        }
        

        /// <summary>
        ///     Private Sub BtnSave_Click
        ///     保存电极检测结果
        /// </summary>
        /// <param name="CNCItem"></param>
        public void SaveQCResult(string LabelName,
            double X,
            double Y,
            double Z,
            double C,
            double HeightMax,
            double HeightMin,
            double GapMax,
            double GapMin,
            double GapCompensation,
            double ZCompensation)
        {
            string _url = "/Task/SaveQCResult?LabelName=" + LabelName +
                "&X=" + X.ToString() + "&Y=" + Y.ToString() + "&Z=" + Z.ToString() +
                "&C=" + C.ToString() + "&HeightMax=" + HeightMax + "&HeightMin=" + HeightMin +
                "&GapMax=" + GapMax.ToString() + "&GapMin=" + GapMin.ToString() +
                "&GapCompensation=" + GapCompensation.ToString() + "&ZCompensation=" + ZCompensation.ToString();
            _server.ReceiveStream(_url);
        }


        /// <summary>
        /// QCControl_Load  Private Sub ReadSetting
        /// 读取QC针对当前电脑的配置信息
        /// </summary>
        /// <param name="ComputerName"></param>
        /// <returns></returns>
        public QCCmmFileSetting GetQCSetting(string ComputerName)
        {
            string _url = "/Task/GetQCSetting?ComputerName=" + ComputerName;
            QCCmmFileSetting _setting = JsonConvert.DeserializeObject<QCCmmFileSetting>(_server.ReceiveStream(_url));
            return _setting;
        }


        /// <summary>
        /// Private Sub FOKButton_Click
        /// 更新检测设定
        /// </summary>
        public void SaveQCSetting(string ComputerName, string CMMFile, string BackDir, string ReportsServer, string TemplatePath, string SteelTemplatePath)
        {
            string _url = "/Task/SaveQCSetting?ComputerName=" + ComputerName +
                        "&CMMFile=" + CMMFile +
                        "&BackDir=" + BackDir +
                        "&ReportsServer=" + ReportsServer +
                        "&TemplatePath=" + TemplatePath +
                        "&SteelTemplatePath=" + SteelTemplatePath;
            _server.ReceiveStream(_url);
        }

        /// <summary>
        /// Private Sub MEASURE_Load
        /// 
        /// </summary>
        /// <param name="QCTaskID"></param>
        /// <returns></returns>
        public string GetCADList(int QCTaskID)
        {
            string _url = "/Task/GetCADList?QCTaskID=" + QCTaskID;

            string _cad = _server.ReceiveStream(_url);
            return _cad;
        }

        /// <summary>
        /// Private Sub SetTaskStartFlat(TaskID As Integer)
        /// 设置QC任务开始
        /// </summary>
        /// <param name="QCTaskID"></param>
        public void SetQCTaskStart(int QCTaskID,string StartTime)
        {
            string _url = "/Task/SetQCTaskStart?QCTaskID=" + QCTaskID+ "&StartTime="+ StartTime;
            _server.ReceiveStream(_url);
        }

        /// <summary>
        /// Private Function SaveToDatabase(PartName As String) As Boolean
        /// Private Function SaveToDatabase(strName As String) As Boolean
        /// </summary>
        /// <param name="QCTaskID"></param>
        /// <param name="ReportName"></param>
        /// <param name="UserName"></param>
        /// <param name="ComputerName"></param>
        /// <param name="ReportType">
        /// 0：CMM报告
        /// 1:图片
        /// </param>
        public void SaveSteelResult(int QCTaskID, string ReportName, string UserName, string ComputerName, int ReportType = 0)
        {
            string _url = "/Task/SaveSteelReport?QCTaskID=" + QCTaskID + "&ReportName=" + ReportName + "&UserName=" + UserName
                + "&ComputerName=" + ComputerName + "&ReportType=" + ReportType;
            _server.ReceiveStream(_url);
        }

        /// <summary>
        /// Private Sub LoadReportsList()
        /// Private Sub STHGridView_CellContentClick
        /// 获取铁件QC报告
        /// <param name="QCTaskID"></param>
        /// <param name="Type">
        /// 0:CMM报告
        /// 1:图片
        /// </param>
        /// <returns></returns>
        public List<QCCmmReport> GetSteelReport(int QCTaskID, int Type)
        {
            string _url = "/Task/GetQCReport?QCTaskID=" + QCTaskID + "&ReportType=" + Type;
            return JsonConvert.DeserializeObject<List<QCCmmReport>>(_server.ReceiveStream(_url));
        }

        /// <summary>
        /// Private Sub ProDelete_Click
        /// Private Sub PicDelete_Click</summary>
        /// 清除检测报告/图片
        /// <param name="QCCmmReportID"></param>
        public void DeleteQCReport(int QCCmmReportID)
        {
            string _url = "/Task/DeleteQCReport?QCCmmReportID=" + QCCmmReportID;
            _server.ReceiveStream(_url);
        }

        /// <summary>
        /// Public Function StartPCDims() As Boolean
        /// 
        /// </summary>
        /// <param name="CNCItemID"></param>
        /// <returns></returns>
        public EleQCInformation GetPointInformation(int CNCItemID)
        {
            string _url = "/Task/GetPointInformation?CNCItemID=" + CNCItemID.ToString();
            EleQCInformation _qcInformation = JsonConvert.DeserializeObject<EleQCInformation>(_server.ReceiveStream(_url));
            return _qcInformation;
        }

        /// <summary>
        /// 
        /// 获取QC点文件保存主路径
        /// </summary>
        /// <returns></returns>
        public string GetQCPointPath()
        {
            string _url = "/Task/GetSetting?Name=QCPointPath";
            string _path = _server.ReceiveStream(_url);
            return _path.Trim();
        }

        /// <summary>
        /// Public Sub New(TaskID As Long, strTemplatePath As String, TaskName As String, sconn As SqlClient.SqlConnection)
        /// </summary>
        /// <param name="QCTaskID"></param>
        /// <returns></returns>
        public QCSteelPoint GetQCSteelPoint(int QCTaskID)
        {
            string _url = "/Task/GetQCSteelPoint?QCTaskID=" + QCTaskID.ToString();
            QCSteelPoint _qcSteelPoint = JsonConvert.DeserializeObject<QCSteelPoint>(_server.ReceiveStream(_url));
            return _qcSteelPoint;
        }


        /// <summary>
        /// TS_DJ_Del_Click
        /// 
        /// 删除电极
        /// </summary>
        /// <param name="CNCItemID"></param>
        public void DeleteElectrode(int QCTaskID)
        {
            string _url = "/Task/DeleteElectrode?QCTaskID=" + QCTaskID;
            _server.ReceiveStream(_url);
        }
        /// <summary>
        /// 测量部门人员
        /// </summary>
        /// <returns></returns>
        public List<User> GetQCUsers()
        {
            string _url = "/User/GetUsersByDepartment?DepartmentName=测量";
            return JsonConvert.DeserializeObject<List<User>>(_server.ReceiveStream(_url));
        }
    }
}
