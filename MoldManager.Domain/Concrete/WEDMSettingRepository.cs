using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
//using System.Threading.Tasks;
using TechnikSys.MoldManager.Domain.Entity;
using TechnikSys.MoldManager.Domain.Abstract;
using TechnikSys.MoldManager.Domain.Output;
using TechnikSys.MoldManager.Domain.Status;

namespace TechnikSys.MoldManager.Domain.Concrete
{
    public class WEDMSettingRepository : IWEDMSettingRepository
    {
        private EFDbContext _context = new EFDbContext();
        public IQueryable<WEDMSetting> WEDMSettings
        {
            get { return _context.WEDMSettings; }
        }

        public WEDMSetting QueryByTaskID(int TaskID)
        {
            Task task = _context.Tasks.Where(t => t.TaskID == TaskID).FirstOrDefault() ?? new Task();
            WEDMSetting wedmsetting = _context.WEDMSettings.Where(w => w.ID == task.ProgramID).FirstOrDefault() ?? new WEDMSetting();
            return wedmsetting;
        }
        /// <summary>
        /// 保存
        /// </summary>
        /// <param name="entity"></param>
        /// <returns>return ID= 成功 -1--已发布 -2--失败 -3--已有更新版本 -4 上一版本没有发布</returns>
        public int Save(WEDMSetting entity)
        {
            bool IsNew=false;
            try
            {
                #region 返回错误代码
                WEDMSetting ws = _context.WEDMSettings.Where(w => w.DrawName == entity.DrawName && w.Rev == entity.Rev).FirstOrDefault() ?? new WEDMSetting();
                if (ws.ReleaseFlag)
                    return -1;
                WEDMSetting ws3 = _context.WEDMSettings.Where(w => w.DrawName == entity.DrawName && w.Rev == entity.Rev && w.Rev > entity.Rev && w.active == true).FirstOrDefault() ?? new WEDMSetting();
                if (ws3.ID > 0)
                    return -3;
                WEDMSetting ws4 = _context.WEDMSettings.Where(w => w.DrawName == entity.DrawName && w.Rev == entity.Rev && w.Rev < entity.Rev && w.active == true && w.ReleaseFlag == false).FirstOrDefault() ?? new WEDMSetting();
                if (ws4.ID > 0)
                    return -4;
                #endregion
                #region Add
                if (ws.ID == 0)
                {
                    IsNew = true;
                    entity.LastestFlag = true;
                    entity.ReleaseFlag = false;
                    entity.CreateDate = DateTime.Now;
                    entity.active = true;
                    entity.DeleteDate = DateTime.Parse("1900/1/1");
                    entity.ReleaseDate = DateTime.Parse("1900/1/1");
                    _context.WEDMSettings.Add(entity);                                      
                }
                #endregion
                #region Update
                else
                {
                    ws.Precision = entity.Precision;
                    ws.Note = entity.Note;
                    ws.FeatureCount = entity.FeatureCount;
                    ws.Length = entity.Length;
                    ws.Thickness = entity.Thickness;
                    ws.LastestFlag = true;
                    ws.active = true;
                    ws.QcPoint = entity.QcPoint;
                    ws.CADPartName = entity.CADPartName;
                    ws.Time = entity.Time;
                    ws.ThreeDPartName = entity.ThreeDPartName;
                }
                #endregion
                _context.SaveChanges();
                if (IsNew)
                {
                    if (entity.ID > 0)
                    {
                        ReleaseWEDMDrawing(entity.ID, "", 1);
                    }
                    return entity.ID;
                }                    
                else
                    return ws.ID;
            }
            catch
            {
                return -2;
            }

        }
        public bool DeleteSettingByName(string partname, int rev)
        {
            WEDMSetting dbentity = _context.WEDMSettings.Where(m => m.DrawName == partname).Where(m => m.Rev == rev).FirstOrDefault() ?? new WEDMSetting();
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
        /// 发布
        /// </summary>
        /// <param name="DrawIndex"></param>
        /// <param name="ReleaseBy"></param>
        /// <param name="Qty"></param>
        /// <returns>0，成功；1，失败；2，已发布，无法保存；</returns>
        public int ReleaseWEDMDrawing(int DrawIndex, string ReleaseBy, int Qty = 1)
        {
            WEDMSetting ws = _context.WEDMSettings.Where(w => w.ID == DrawIndex && w.active == true).FirstOrDefault() ?? new WEDMSetting();
            #region 返回错误代码
            if (ws.ReleaseFlag)
                return 2;
            if (ws.ID == 0)
                return 1;
            #endregion
            var tran = _context.Database.BeginTransaction();  //开启事务
            try
            {
                #region 版本 >0
                string FullPartName = "";
                if (ws.Rev > 0)
                {
                    //TaskType->3 WEDM State->7 外发
                    var mgtasks = from t in _context.Tasks
                                  join m in _context.WEDMSettings
                                  on t.ProgramID equals m.ID
                                  where m.DrawName == ws.DrawName && m.Rev < ws.Rev && t.TaskType == 3 && t.Enabled == true && t.State >=(int)CNCStatus.等待 && t.State <= (int)CNCStatus.正在加工
                                  select t;
                    if (mgtasks != null)
                    {
                        foreach (var mgt in mgtasks)
                        {
                            //任务状态设置为 完成
                            mgt.State = (int)CNCStatus.完成;
                        }
                    }
                    if (ws.Rev < 10)
                    {
                        FullPartName = ws.DrawName + "R0" + ws.Rev.ToString();
                    }
                    else
                    {
                        FullPartName = ws.DrawName + "R" + ws.Rev.ToString();
                    }
                    List<CAMDrawing> cds = _context.CAMDrawings.Where(c => c.DrawingName == FullPartName).ToList() ?? new List<CAMDrawing>();
                    if (cds.Count > 0)
                    {
                        foreach (var cd in cds)
                        {
                            cd.Lock = true;
                            cd.ReleaseDate = DateTime.Now;
                            cd.ReleaseBy = ReleaseBy;
                        }
                    }
                }
                #endregion
                #region 生成WEDM加工任务
                DateTime PlanDate;
                Project proj = _context.Projects.Where(p => p.MoldNumber == ws.MoldName && p.Enabled == true).FirstOrDefault() ?? new Project();
                ProjectPhase ph10 = _context.ProjectPhases.Where(p => p.ProjectID == proj.ProjectID && p.PhaseID == 10).FirstOrDefault() ?? new ProjectPhase();//WEDM阶段
                if (ph10.PlanFinish == null)
                {
                    PlanDate = DateTime.Parse("1900/1/1");
                }
                else
                {
                    PlanDate = ph10.PlanFinish;
                }
                #region WEDMSetting 状态更新
                ws.ReleaseFlag = true;
                ws.ReleaseDate = DateTime.Now;
                #endregion
                User user = _context.Users.Where(u => u.FullName == ReleaseBy && u.Enabled == true).FirstOrDefault() ?? new User();
                Task WEDMtask = new Task { TaskName = ws.DrawName,Version = ws.Rev, ProgramID = ws.ID, Creator = user.UserID, CreateTime = DateTime.Now, Enabled = true, Priority = 0, State = (int)CNCStatus.未发布, Memo = "Create by CAM", Quantity = Qty, PlanTime = PlanDate, StartTime = DateTime.Now, ProjectID = proj.ProjectID, TaskType = 3, MoldNumber = ws.MoldName, ProcessName = ws.DrawName.Substring(ws.DrawName.IndexOf('(') + 1, ws.DrawName.Length - ws.DrawName.IndexOf('(') - 2), Time = Convert.ToDouble(ws.Time)};
                _context.Tasks.Add(WEDMtask);
                #endregion
                _context.SaveChanges();
                tran.Commit();//提交事务
                return 0;
            }
            catch
            {
                tran.Rollback();    //回滚
                return 1;
            }
        }
        public List<Task> GetWEDMTaskByMoldAndStatus(string MoldNo, int Status = -2, int PlanID = 0)
        {
            List<Task> tlist = new List<Task>();
            if (PlanID == 0)
            {
                //所有未完成且没有暂停的WEDM任务
                if (Status == -1 && MoldNo != "所有新任务")
                {
                    tlist = _context.Tasks.Where(t => t.TaskType == 3 && t.Enabled == true && t.State < 90 && t.State != 6 && t.MoldNumber == MoldNo).ToList();
                }
                else if (Status == -1 && MoldNo == "所有新任务")
                {
                    tlist = _context.Tasks.Where(t => t.TaskType == 3 && t.Enabled == true && t.State < 90 && t.State != 6).ToList();
                }
                else if (Status == -2)
                {
                    tlist = _context.Tasks.Where(t => t.TaskType == 3 && t.Enabled == true && t.MoldNumber == MoldNo).ToList();
                }
                else
                {
                    tlist = _context.Tasks.Where(t => t.TaskType == 3 && t.Enabled == true && t.MoldNumber == MoldNo && t.State == Status).ToList();
                }
            }
            else
            {
                //所有未完成且没有暂停的WEDM任务
                if (Status == -1 && MoldNo != "所有新任务")
                {
                    tlist = _context.Tasks.Where(t => t.TaskType == 3 && t.Enabled == true && t.State < 90 && t.State != 6 && t.MoldNumber == MoldNo && t.ProjectID == PlanID).ToList();
                }
                else if (Status == -1 && MoldNo == "所有新任务")
                {
                    tlist = _context.Tasks.Where(t => t.TaskType == 3 && t.Enabled == true && t.State < 90 && t.State != 6 && t.ProjectID == PlanID).ToList();
                }
                else if (Status == -2)
                {
                    tlist = _context.Tasks.Where(t => t.TaskType == 3 && t.Enabled == true && t.MoldNumber == MoldNo && t.ProjectID == PlanID).ToList();
                }
                else
                {
                    tlist = _context.Tasks.Where(t => t.TaskType == 3 && t.Enabled == true && t.MoldNumber == MoldNo && t.State == Status && t.ProjectID == PlanID).ToList();
                }
            }
            return tlist;
        }
        public WEDMCutSpeed GetWDMCutSpeed(double Thickness, int CutTypeID)
        {
            WEDMCutSpeed wcs = _context.WEDMCutSpeeds.Where(w => w.TypeID == CutTypeID && w.Thickness == Thickness && w.active == true).FirstOrDefault() ?? _context.WEDMCutSpeeds.Where(w => w.TypeID == CutTypeID && w.active == true).FirstOrDefault();
            return wcs;
        }
        public List<WEDMPrecision> GetWEDMPrecision()
        {
            return _context.WEDMPrecisions.Where(w => w.active == true).ToList() ?? new List<WEDMPrecision>();
        }
        public string Get3DDrawingServerPath()
        {
            SystemConfig sys = _context.SystemConfigs.Where(s => s.SettingName == "WEDM3DPATH").FirstOrDefault() ?? new SystemConfig();
            string path = sys.Value.Substring(2, sys.Value.Length - 2).Replace("\\", "/") + "/";
            return path;
        }
        public List<WEDMTaskInfo> GetWEDMTaskInfoByMoldAndStatus(string MoldNo, int Status = -2, int PlanID = 0)
        {
            List<WEDMTaskInfo> AskTaskinfo = new List<WEDMTaskInfo>();
            List<WEDMTaskInfo> AllTaskinfo = GetWEDMTaskInfo() ?? new List<WEDMTaskInfo>();
            if (AllTaskinfo.Count > 0)
            {
                if (PlanID == 0)
                {
                    //所有未完成且没有暂停的WEDM任务
                    if (Status == -1 && MoldNo != "所有新任务")
                    {
                        AskTaskinfo = AllTaskinfo.Where(t => t.State < 90 && t.State != 6 && t.MoldNumber == MoldNo).ToList();
                    }
                    else if (Status == -1 && MoldNo == "所有新任务")
                    {
                        AskTaskinfo = AllTaskinfo.Where(t => t.State < 90 && t.State != 6).ToList();
                    }
                    else if (Status == -2)
                    {
                        AskTaskinfo = AllTaskinfo.Where(t =>t.MoldNumber == MoldNo).ToList();
                    }
                    else
                    {
                        AskTaskinfo = AllTaskinfo.Where(t => t.MoldNumber == MoldNo && t.State == Status).ToList();
                    }
                }
                else
                {
                    //所有未完成且没有暂停的WEDM任务
                    if (Status == -1 && MoldNo != "所有新任务")
                    {
                        AskTaskinfo = AllTaskinfo.Where(t =>  t.State < 90 && t.State != 6 && t.MoldNumber == MoldNo && t.Plan == PlanID).ToList();
                    }
                    else if (Status == -1 && MoldNo == "所有新任务")
                    {
                        AskTaskinfo = AllTaskinfo.Where(t => t.State < 90 && t.State != 6 && t.Plan == PlanID).ToList();
                    }
                    else if (Status == -2)
                    {
                        AskTaskinfo = AllTaskinfo.Where(t => t.MoldNumber == MoldNo && t.Plan == PlanID).ToList();
                    }
                    else
                    {
                        AskTaskinfo = AllTaskinfo.Where(t => t.MoldNumber == MoldNo && t.State == Status && t.Plan == PlanID).ToList();
                    }
                }
                return AskTaskinfo;
            }
            return null;

        }
        public List<WEDMTaskInfo> GetWEDMTaskInfo()
        {
            DateTime InitialTime = DateTime.Parse("1900/1/1");
            try
            {
                var taskinfo = from t in _context.Tasks
                               join w in _context.WEDMSettings on t.ProgramID equals w.ID into tmp1
                               from a in tmp1.DefaultIfEmpty()
                               join u1 in _context.Users on t.Creator equals u1.UserID into tmp2
                               from b in tmp2.DefaultIfEmpty()
                               join u2 in _context.Users.DefaultIfEmpty() on t.FinishBy equals u2.UserID into tmp3
                               from c in tmp3.DefaultIfEmpty()
                               where t.TaskType==3 && t.Enabled==true
                               select new WEDMTaskInfo
                               {
                                   MoldNumber=t.MoldNumber,
                                   TaskID = t.TaskID,
                                   TaskName = t.TaskName,
                                   rev = t.Version,
                                   Priority = t.Priority,
                                   Note = t.Memo,
                                   CreateBy = b.FullName,
                                   CreateDate = t.CreateTime,
                                   StartDate = t.StartTime,
                                   FinishDate = t.FinishTime,
                                   Qty = t.Quantity,
                                   FinishBy = c.FullName,
                                   HaveISO = false,
                                   ISOName = "",
                                   SettingID = t.ProgramID,
                                   PlanDate = t.PlanTime,
                                   PlanDate1 = InitialTime,
                                   PlanDate2 = InitialTime,
                                   PlanNote = "",
                                   MachineID = 0,
                                   RecieveDate = t.AcceptTime,
                                   StatusName = "",//Enum.GetName(typeof(WEDMStatus), t.State)
                                   Time = a.Time,
                                   ThreeDPartName = a.ThreeDPartName,
                                   Plan=0
                               };
                return taskinfo.ToList();
            }
            catch(Exception ex)
            {               
                return null;
            }
        }
    }
}
