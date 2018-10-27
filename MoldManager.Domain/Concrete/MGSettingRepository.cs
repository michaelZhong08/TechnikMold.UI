using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
//using System.Threading.Tasks;
using TechnikSys.MoldManager.Domain.Entity;
using TechnikSys.MoldManager.Domain.Abstract;
using TechnikSys.MoldManager.Domain.Status;

namespace TechnikSys.MoldManager.Domain.Concrete
{
    public class MGSettingRepository : IMGSettingRepository
    {
        private EFDbContext _context = new EFDbContext();
        public IQueryable<MGSetting> MGSettings
        {
            get { return _context.MGSettings; }
        }

        public MGSetting QueryByTaskID(int TaskID)
        {
            Task task = _context.Tasks.Where(t => t.TaskID == TaskID).FirstOrDefault() ?? new Task();
            MGSetting mgsetting = _context.MGSettings.Where(m => m.ID == task.ProgramID).FirstOrDefault() ?? new MGSetting();
            return mgsetting;
        }
        /// <summary>
        /// 保存
        /// </summary>
        /// <param name="entity"></param>
        /// <returns>return ID= 成功 -1--已发布 -2--失败 -3--已有更新版本 -4 上一版本没有发布</returns>
        public int Save(MGSetting entity,bool ForUG=true)
        {
            bool IsNew = false;
            try
            {
                MGSetting _dbentity = new MGSetting();
                _dbentity = _context.MGSettings.Where(m => m.DrawName == entity.DrawName).Where(m => m.Rev == entity.Rev).FirstOrDefault() ?? new MGSetting();
                if (ForUG)
                {                    
                    if (_dbentity.ReleaseFlag)
                        return -1;
                    MGSetting _dbentity2 = new MGSetting();
                    _dbentity2 = _context.MGSettings.Where(m => m.DrawName == entity.DrawName).Where(m => m.Rev > entity.Rev).Where(m => m.active == true).FirstOrDefault() ?? new MGSetting();
                    if (_dbentity2.ID > 0)
                        return -3;
                    _dbentity2 = _context.MGSettings.Where(m => m.DrawName == entity.DrawName).Where(m => m.Rev < entity.Rev).Where(m => m.ReleaseFlag == false).Where(m => m.active == true).FirstOrDefault() ?? new MGSetting();
                    if (_dbentity2.ID > 0)
                        return -4;
                }               
                #region Add 
                if (_dbentity.ID == 0)
                {
                    IsNew = true;
                    entity.LastestFlag = true;
                    entity.ReleaseFlag = false;
                    entity.CreateDate = DateTime.Now;
                    //NUll值会造成 数据长度溢出问题 datetime2->datetime 类型异常
                    entity.ReleaseDate = DateTime.Parse("1900/1/1");
                    entity.DeleteDate = DateTime.Parse("1900/1/1");
                    entity.active = true;
                    entity.State = (int)MGSettingStatus.新建;
                    _context.MGSettings.Add(entity);
                }
                #endregion
                #region Update
                else
                {
                    _dbentity.CADNames = entity.CADNames;
                    _dbentity.Qty = entity.Qty;
                    _dbentity.ProcessType = entity.ProcessType;
                    _dbentity.Time = entity.Time;
                    _dbentity.FeatureNote = entity.FeatureNote;
                    _dbentity.LastestFlag = true;
                    _dbentity.active = true;
                    _dbentity.Material = entity.Material;
                    _dbentity.HRC = entity.HRC;
                    _dbentity.Rev = entity.Rev;
                    _dbentity.RawSize = entity.RawSize;
                    _dbentity.MoldName = entity.MoldName;
                    _dbentity.State = entity.State;
                }
                #endregion
                _context.SaveChanges();
                if (IsNew)
                    return entity.ID;
                else
                    return _dbentity.ID;
            }
            catch (Exception ex)
            {
                return -2;
            }
        }
        public bool DeleteSettingByName(string partname, int rev)
        {
            MGSetting dbentity = _context.MGSettings.Where(m => m.DrawName == partname).Where(m => m.Rev == rev).FirstOrDefault() ?? new MGSetting();
            if (dbentity.ID > 0)
            {
                dbentity.active = false;
                dbentity.DeleteDate = DateTime.Now;
                _context.SaveChanges();
                return true;
            }
            else
            {
                return false;
            }
        }
        /// <summary>
        /// 发布MG任务 启用事务
        /// </summary>
        /// <param name="DrawIndex"></param>
        /// <param name="ReleaseBy"></param>
        /// <returns>return value 0 --成功 1--失败 2--已发布</returns>
        public int ReleaseMGDrawing(int DrawIndex, string ReleaseBy, string TaskName)
        {
            #region 返回失败代码
            MGSetting dbentity = _context.MGSettings.Where(m => m.ID == DrawIndex).Where(m => m.active == true).FirstOrDefault() ?? new MGSetting();
            if (dbentity.ID == 0 || dbentity.ProcessType==0)
                return 1;
            MGSetting dbentity2 = _context.MGSettings.Where(m => m.ID == DrawIndex).Where(m => m.active == true).Where(m => m.ReleaseFlag == true).FirstOrDefault() ?? new MGSetting();
            if (dbentity2.ID > 0)
                return 2;
            #endregion
            var tran = _context.Database.BeginTransaction();  //开启事务
            try
            {
                #region 更新 MGSetting
                dbentity.ReleaseFlag = true;
                dbentity.ReleaseDate = DateTime.Now;
                dbentity.ReleaseBy = ReleaseBy;
                dbentity.State = (int)MGSettingStatus.已发布但任务未发布;
                #endregion
                MGTypeName typename = _context.MGTypeNames.Where(m => m.ID == dbentity.ProcessType).FirstOrDefault() ?? new MGTypeName();
                WarehouseRecord whrecord = _context.WarehouseRecords.Where(w => w.Name == dbentity.ItemNO).Where(w => w.Quantity > 0).FirstOrDefault() ?? new WarehouseRecord();
                #region 版本号>0
                if (dbentity.Rev > 0)
                {
                    if (IsMGTaskReleasedByName(TaskName))
                        return 2;
                    List<MGSetting> mgsettings = _context.MGSettings.Where(m => m.DrawName == dbentity.DrawName).Where(m => m.Rev < dbentity.Rev).ToList();
                    if (mgsettings != null && mgsettings.Count > 0)
                    {
                        foreach (var ms in mgsettings)
                        {
                            ms.LastestFlag = false;
                        }
                    }
                    //TaskType->6 铣磨 State->7 外发
                    var mgtasks = from t in _context.Tasks
                                  join m in _context.MGSettings
                                  on t.ProgramID equals m.ID
                                  where m.DrawName == dbentity.DrawName && m.Rev < dbentity.Rev && t.TaskType == 6 && t.Enabled == true && t.State < 7
                                  select t;
                    if (mgtasks != null && mgtasks.Count() > 0)
                    {
                        foreach (var mgt in mgtasks)
                        {
                            //任务状态设置为 完成
                            mgt.State = 90;
                        }
                    }
                }
                #endregion
                #region 计划时间
                Project proj = _context.Projects.Where(p => p.MoldNumber == dbentity.MoldName && p.Enabled == true).FirstOrDefault() ?? new Project();
                ProjectPhase ph4 = _context.ProjectPhases.Where(p => p.ProjectID == proj.ProjectID && p.PhaseID == 4).FirstOrDefault() ?? new ProjectPhase();//M铣床
                ProjectPhase ph7 = _context.ProjectPhases.Where(p => p.ProjectID == proj.ProjectID && p.PhaseID == 7).FirstOrDefault() ?? new ProjectPhase();//G磨床
                #endregion
                #region 生成加工任务
                int lenNote = typename.Note.Length;
                int? Process = null;
                string str = "";
                int strStart = 1;
                DateTime PlanDate;
                for (int i = 1; i <= lenNote; i++)
                {
                    if (typename.Note.Substring(i - 1, 1) == "," || i == lenNote)
                    {
                        if (typename.Note.Substring(i - 1, 1) == ",")
                        {
                            str = typename.Note.Substring(strStart - 1, i - strStart);
                        }
                        else if (i == lenNote)
                        {
                            str = typename.Note.Substring(strStart - 1, i - strStart + 1);
                        }
                        strStart = i + 1;
                    }
                    try
                    {
                        Process = Convert.ToInt32(str);
                    }
                    catch { }
                    if (Process != null && Process == 0)
                    {
                        PlanDate = ph4.PlanFinish;
                    }
                    else
                    {
                        PlanDate = ph7.PlanFinish;
                    }
                    if (PlanDate == null)
                    {
                        PlanDate = DateTime.Parse("1900/1/1");
                    }
                    User user = _context.Users.Where(u => u.FullName == ReleaseBy).FirstOrDefault() ?? new User();
                    #region 检索图纸
                    string flag = _context.SystemConfigs.Where(s => s.SettingName == "2D_Drw_Type").FirstOrDefault().Value ?? "";
                    string DrawType = flag == "Outside" ? "2D" : "";
                    string sDrawName = TaskName + "_" + DrawType;
                    string DrawName="";
                    List<CAMDrawing> _draws = _context.CAMDrawings.Where(d => d.DrawingName.Contains(sDrawName)).ToList();
                    Dictionary<int, int> _dics = new Dictionary<int, int>();
                    if (_draws.Count > 0)
                    {
                        foreach(var d in _draws)
                        {
                            int _ver = Convert.ToInt32(d.DrawingName.Substring(d.DrawingName.LastIndexOf('_') + 2, 2));
                            _dics.Add(d.CAMDrawingID, _ver);
                        }
                        Dictionary<int, int> _dics_SortByKey = _dics.OrderByDescending(d => d.Value).ToDictionary(d => d.Key, v => v.Value);
                        DrawName=_draws.Where(d => d.CAMDrawingID == _dics_SortByKey.FirstOrDefault().Key).FirstOrDefault().DrawingName;
                    }
                    #endregion
                    //关键 TaskType->6 铣磨任务; OldID字段用于记录旧表(MG_Task)Process信息-> 0:铣床任务 1:磨床任务
                    MGTypeName _mgtype = _context.MGTypeNames.Where(t => t.Note == Process.ToString()).FirstOrDefault() ?? new MGTypeName();
                    Task mgtask = new Task { TaskName = TaskName,DrawingFile= DrawName, Version = dbentity.Rev, ProgramID = dbentity.ID, Creator = user.UserID, CreateTime = DateTime.Now, Enabled = true, Priority = 0, State = (int)CNCStatus.未发布,PrevState= (int)CNCStatus.未发布, Memo = "Create by CAM", Quantity = dbentity.Qty, OldID = Convert.ToInt32(Process), PlanTime = PlanDate, StartTime = DateTime.Now, ProjectID = proj.ProjectID, TaskType = 6, MoldNumber = dbentity.MoldName, HRC = dbentity.HRC, ProcessName = _mgtype.Name, Time = Convert.ToDouble(dbentity.Time),Material=dbentity.Material,Raw=dbentity.RawSize ,CAMUser=0};
                    if (Process != null)
                    {
                        _context.Tasks.Add(mgtask);
                    }
                }
                #endregion
                _context.SaveChanges();
                tran.Commit();//提交事务
                return 0;
            }
            catch (Exception ex)
            {
                tran.Rollback();    //回滚
                return 1;
            }
        }
        public List<MGSetting> GetMGPartListByMold(string MoldNo, bool bRelease)
        {
            return _context.MGSettings.Where(m => m.MoldName == MoldNo && m.ReleaseFlag == bRelease && m.active == true).ToList() ?? new List<MGSetting>();
        }
        /// <summary>
        /// 若库中有TaskName的记录并且已经发布 该函数返回2(已发布)
        /// </summary>
        /// <param name="TaskName"></param>
        /// <returns></returns>
        public bool IsMGTaskReleasedByName(string TaskName)
        {
            DateTime IntialTime = DateTime.Parse("1900/1/1");
            Task mgTask = _context.Tasks.Where(t => t.TaskType == 6 && t.Enabled == true && t.TaskName == TaskName && t.State!=-99).FirstOrDefault() ?? new Task();
            if (mgTask.TaskID > 0)
                return true;
            return false;
        }
        /// <summary>
        /// 检索之前相同DrawName的任务中最近一次的DrawFile
        /// </summary>
        /// <param name="DrawName"></param>
        /// <returns></returns>
        public List<Task> GetTaskByDrawName(string DrawName,bool IsContain2D, string DrawType)
        {
            string _taskName;
            if (IsContain2D)
            {
                //111111_MG_2D_V00
                _taskName = DrawName.Substring(0, DrawName.LastIndexOf('_'));
                _taskName = _taskName.Substring(0, _taskName.LastIndexOf('_'));
            }
            else
            {
                //111111_MG_V00
                _taskName = DrawName.Substring(0, DrawName.LastIndexOf('_'));
            }
            int _version = Convert.ToInt16(DrawName.Substring(DrawName.LastIndexOf('_') + 2, 2));

            List<Task> _tasks = new List<Task>();
            if (DrawType == "CAM")
            {
                _tasks = _context.Tasks.Where(t => t.TaskName == _taskName && t.Version == _version && t.TaskType == 6 && t.Enabled == true).ToList() ?? new List<Task>();
            }
            else if (DrawType == "CAD")
            {
                _tasks = _context.Tasks.Where(t => t.TaskName == _taskName && t.TaskType == 6 && t.Enabled == true).ToList() ?? new List<Task>();
            }
            return _tasks;
        }
        /// <summary>
        /// 返回DrawingFile；若不存在，则返回空值
        /// </summary>
        /// <param name="DrawName"></param>
        /// <param name="IsContain2D"></param>
        /// <returns></returns>
        public string GetDrawFileByDrawName(string DrawName, bool IsContain2D, string DrawType)
        {
            try
            {
                List<Task> mgTask = GetTaskByDrawName(DrawName, IsContain2D, DrawType);
                if (mgTask.Count > 0)
                {
                    if (mgTask[0].TaskID > 0)
                        return mgTask[0].DrawingFile ?? "";
                }
            }
            catch { }        
            return "";
        }
        /// <summary>
        /// 判断图纸当前最新版；若数据存在，则证明图纸不是最新版。
        /// </summary>
        /// <param name="DrawName"></param>
        /// <param name="IsContain2D"></param>
        /// <returns></returns>
        public bool IsLatestDrawFile(string DrawName, bool IsContain2D, string DrawType)
        {
            try
            {
                List<Task> mgTask = GetTaskByDrawName(DrawName, IsContain2D, DrawType);
                string _dbDrawFile = "";
                if (mgTask.Count > 0)
                {
                    if (mgTask[0].TaskID > 0)
                        _dbDrawFile = mgTask[0].DrawingFile ?? "";
                    #region 比较版本
                    int objVer= Convert.ToInt32( DrawName.Substring(DrawName.LastIndexOf('_') + 2, 2));
                    int dbVer = 0;
                    if(_dbDrawFile!="")
                        dbVer= Convert.ToInt32(_dbDrawFile.Substring(_dbDrawFile.LastIndexOf('_') + 2, 2));
                    if (objVer > dbVer)
                        return true;
                    #endregion
                }
            }
            catch { }
            return false;
        }
        #region MG TypeName
        public List<MGTypeName> GetMGTypeName()
        {
            List<MGTypeName> MGTypeNameList = _context.MGTypeNames.Where(m => m.active == true).ToList() ?? new List<MGTypeName>();
            return MGTypeNameList;
        }
        #endregion
    }
}
