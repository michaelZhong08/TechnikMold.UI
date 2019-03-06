/*
 * Create By:lechun1
 * 
 * Description:
 * 
 */



using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TechnikSys.MoldManager.Domain.Abstract;
using TechnikSys.MoldManager.Domain.Entity;
using TechnikSys.MoldManager.Domain.Output;
using TechnikSys.MoldManager.Domain.Status;

namespace TechnikSys.MoldManager.Domain.Concrete
{
    public class ProjectRepository:IProjectRepository
    {
        private EFDbContext _context = new EFDbContext();

        public IQueryable<Project> Projects
        {
            get { 
                return _context.Projects; 
            }
        }

        public int Save(Project Project)
        {

            Project.CreateTime = Project.CreateTime == new DateTime(0001, 1, 1) ? new DateTime(1900, 1, 1) : Project.CreateTime;
            Project.FinishTime = Project.FinishTime == new DateTime(0001, 1, 1) ? new DateTime(1900, 1, 1) : Project.FinishTime;
            if (Project.ProjectID == 0)
            {
                Project.OldID = -1;
                Project.ProjectStatus = 0;
                Project.CreateTime = DateTime.Now;
                Project.FinishTime = new DateTime(1900, 1, 1); 
                _context.Projects.Add(Project);
            }
            else
            {
                Project _dbEntry = _context.Projects.Find(Project.ProjectID);
                #region 更新主项目下所有子项目客户
                if (Project.MoldNumber == "---")
                {
                    if (_dbEntry.CustomerID != Project.CustomerID)
                    {
                        IQueryable<Project> _projects = _context.Projects.Where(p => p.ProjectNumber == Project.ProjectNumber);
                        foreach(var p in _projects)
                        {
                            p.CustomerID = Project.CustomerID;
                            p.CustomerName = Project.CustomerName;
                        }
                    }
                }
                #endregion
                if (_dbEntry != null)
                {
                    _dbEntry.Name = Project.Name;
                    _dbEntry.ProjectNumber = Project.ProjectNumber;
                    _dbEntry.Type = Project.Type;
                    _dbEntry.MoldNumber = Project.MoldNumber;
                    _dbEntry.CustomerID = Project.CustomerID;
                    _dbEntry.CustomerName = Project.CustomerName;
                    _dbEntry.ProjectStatus = Project.ProjectStatus;
                    _dbEntry.Memo = Project.Memo == null ? "" : Project.Memo;
                    _dbEntry.Attachment = Project.Attachment == null ? "" : Project.Attachment;
                    _dbEntry.ParentID = Project.ParentID;
                    _dbEntry.Enabled = Project.Enabled;
                    _dbEntry.OldID = Project.OldID;
                    _dbEntry.CreateTime = Project.CreateTime;
                    _dbEntry.FinishTime = Project.FinishTime;
                    _dbEntry.FixMoldType = Project.FixMoldType;
                    _dbEntry.MainPhaseChange = Project.MainPhaseChange;
                }
            }
            
            //IEnumerable<Project> _subs = _context.Projects.Where(p => p.ParentID == Project.ProjectID);
            //if (_subs.Count() > 0)
            //{
            //    foreach (Project _sub in _subs)
            //    {
            //        _sub.CustomerID = Project.CustomerID;
            //        _sub.CustomerName = Project.CustomerName;                    
            //    }
            //}
            _context.SaveChanges();
            return Project.ProjectID;
        }

        

        /// <summary>
        /// Query project by keyword
        /// </summary>
        /// <param name="Keyword">query keyword</param>
        /// <returns></returns>
        public IQueryable<Project> Query(string Keyword)
        {
            IQueryable<Project> _dbEntry = _context.Projects.Where(p => p.Name.Contains(Keyword));
            return _dbEntry;
        }

        public Project QueryByMoldNumber(string MoldNumber, int Type=-1)
        {
            Project _dbEntry;
            if (Type > 0)
            {
                _dbEntry = _context.Projects.Where(p => p.MoldNumber == MoldNumber)
                    .Where(p => p.Type == Type)
                    //胡工要求
                    .Where(p => p.Enabled == true)
                    .OrderByDescending(p => p.Version)
                    .FirstOrDefault();
            }
            else
            {
                _dbEntry = _context.Projects
                    .Where(p => p.MoldNumber == MoldNumber)
                    .Where(p => p.Enabled == true)
                    .OrderByDescending(p => p.Version)
                    .FirstOrDefault();
            }
            return _dbEntry;
        }

        public void AddMemo(int ProjectID, string Memo)
        {
            Project _project = QueryByID(ProjectID);
            _project.Memo =Memo;
            _context.SaveChanges();            
        }

        public Project QueryByID(int ProjectID)
        {
            Project _dbEntry = _context.Projects.Find(ProjectID);
            return _dbEntry;
        }


        public int PauseProject(int ProjectID, string Memo, bool PauseSubs)
        {
            Project _dbEntry = QueryByID(ProjectID);
            string _memoTitle=DateTime.Now.ToString("yyyy-MM-dd")+" "+Memo;
            if (_dbEntry.ProjectStatus == 0)
            {
                _dbEntry.ProjectStatus = (int)ProjectStatus.暂停;
                _memoTitle = "项目暂停:<br>"+_memoTitle;
            }
            else
            {
                _dbEntry.ProjectStatus = (int)ProjectStatus.启动;
                _memoTitle = "项目重启:<br>"+_memoTitle ;
            }
            if (PauseSubs)
            {
                IEnumerable<Project> _subs = QueryByMainProject(ProjectID);
                foreach (Project _sub in _subs)
                {
                    _sub.ProjectStatus = _dbEntry.ProjectStatus;
                }
            }
            _dbEntry.Memo = _memoTitle;
            _context.SaveChanges();
            return _dbEntry.ProjectStatus;
        }

        public int CloseProject(int ProjectID)
        {
            Project _project = QueryByID(ProjectID);
            _project.ProjectStatus = (int)ProjectStatus.完成;
            _context.SaveChanges();
            return ProjectID;
        }

        public IEnumerable<Project> QueryByMainProject(int ProjectID)
        {
            IEnumerable<Project> _subProjects = _context.Projects.Where(p => p.ParentID == ProjectID && p.Enabled==true);
            return  _subProjects;
        }


        public int DeleteProject(int ProjectID, string Memo)
        {
            Project _project = QueryByID(ProjectID);
            if (_project != null)
            {
                _project.Enabled = false;
                _project.ProjectStatus= (int)ProjectStatus.删除;
                _project.Memo = Memo;
            }
            _context.SaveChanges();
            return ProjectID;
        }


        public Project QueryByProjectNumber(string ProjectNumber)
        {
            Project _project = _context.Projects.Where(p => p.ProjectNumber == ProjectNumber).FirstOrDefault();
            return _project;
        }


        public Project GetByID(int ProjectID)
        {
            Project _project = _context.Projects.Find(ProjectID);
            return _project;
        }

        public void AddAttachment(int ProjectID, string Attachment)
        {
            Project _project = _context.Projects.Find(ProjectID);
            _project.Attachment = Attachment;
            _context.SaveChanges();
        }


        public Project QueryActiveByMoldNumber(string MoldNumber, int Type = -1, int Status = 0)
        {
            Project _project;
            if (Type > 0)
            {
                _project = _context.Projects.Where(p => p.MoldNumber == MoldNumber)
                .Where(p => p.Type == Type)
                .Where(p => p.ProjectStatus == Status).FirstOrDefault();
            }
            else
            {
                _project = _context.Projects.Where(p => p.MoldNumber == MoldNumber)
                .Where(p => p.ProjectStatus == Status).FirstOrDefault();
            }
            
            return _project;
        }


        public Project GetLatestActiveProject(string MoldNumber)
        {
            Project _project = _context.Projects.Where(p => p.MoldNumber == MoldNumber).Where(p => p.Enabled == true)
                .Where(p => p.ProjectStatus < 4).OrderByDescending(p=>p.ProjectID).FirstOrDefault();
            return _project;
        }



        /// <summary>
        /// 获取某project的所有版本清单
        /// </summary>
        /// <param name="MoldNumber"></param>
        /// <returns></returns>
        public List<MoldVersionInfo> GetProjectVerList(string MoldNumber)
        {
            List<Project> _projectList = _context.Projects
                .Where(p => p.MoldNumber == MoldNumber)
                .OrderByDescending(p => p.Version).ToList();
            List<MoldVersionInfo> list = new List<MoldVersionInfo>();
            foreach (Project prj in _projectList)
            {
                MoldVersionInfo mvi = new Output.MoldVersionInfo();
                if (prj.IsPublish)
                {
                    mvi.IsEdit = false;//发布的，不能编辑
                }
                else
                    mvi.IsEdit = true;//未发布，可编辑
                mvi.ProjectID = prj.ProjectID;
                mvi.Version = prj.Version;
                mvi.MoldNumber = prj.MoldNumber;
                list.Add(mvi);
            }

            return list;
        }

        public Project GetProjectByMoldNumberVer(string MoldNumber, int ver = -1)
        {
            Project _project = null;
            if (ver == -1)
            {
                _project = QueryByMoldNumber(MoldNumber, -1);
            }
            else
            {
                _project = _context.Projects
                   .Where(p => p.MoldNumber == MoldNumber)
                   .Where(p => p.Version == ver)
                   .FirstOrDefault();
            }
            return _project;
        }

        public IQueryable<Project> GetProjectsByDep(int Department,bool isDepFinished)
        {
            DateTime iniDate = new DateTime(1, 1, 1);
            DateTime iniDate1 = new DateTime(1900, 1, 1);
            List<string> depNames = new List<string> { "管理", "项目" };
            IEnumerable<int> _proJIDList;
            List<int> _proJIDList1=new List<int>();
            IQueryable<Project> _projects;
            List<int> depIds = _context.Departments.Where(d => depNames.Contains(d.Name) && d.Enabled==true).Select(d => d.DepartmentID).ToList();
            //var _projects = from pj in _context.Projects
            //                 join ph in _context.ProjectPhases on pj.ProjectID equals ph.ProjectID into _p1
            //                where pj.ProjectNumber != "Sinnotech" && pj.ProjectStatus >= 0 && pj.Enabled == true
            //                from _p2 in _p1.DefaultIfEmpty()
            //                join dp in _context.Base_DepPhases on _p2.PhaseID equals dp.PhaseId into _p3
            //                from _p4 in _p3.DefaultIfEmpty()
            //                where ((_p4.DepId == Department && iniDate.Equals(_p2.ActualFinish)) || Department == 1) 
            //                select pj;
            if (!isDepFinished)
            {
                _proJIDList = (from _p1 in _context.ProjectPhases
                               join _p2 in _context.Base_DepPhases on _p1.PhaseID equals _p2.PhaseId
                               where (_p2.DepId == Department && _p2.Enable == true && (iniDate.Equals(_p1.ActualFinish) || iniDate1.Equals(_p1.ActualFinish))) || depIds.Contains(Department)
                               select _p1.ProjectID).Distinct();
                _proJIDList1.AddRange(_proJIDList);
            }
            else
            {
                //_proJIDList = _context.Projects.Where(p => p.Enabled).Select(p => p.ProjectID).Distinct();
                //_proJIDList = (from _p1 in _context.ProjectPhases
                //               join _p2 in _context.Base_DepPhases on _p1.PhaseID equals _p2.PhaseId
                //               where (_p2.DepId == Department && _p2.Enable == true && (!iniDate.Equals(_p1.ActualFinish) && !iniDate1.Equals(_p1.ActualFinish))) || depIds.Contains(Department)
                //               select _p1.ProjectID).Distinct();
                List<int> _phaseList1 = _context.Base_DepPhases.Where(d => d.DepId == Department && d.Enable).Select(d => d.PhaseId).ToList();
                //筛选 当且仅当部门所涉及的所有阶段均结束的项目
                foreach (var _p in _context.Projects)
                {
                    bool isFinished = true;
                    foreach (var _ph in _context.ProjectPhases.Where(p => p.ProjectID == _p.ProjectID))
                    {
                        if (_phaseList1.Contains(_ph.PhaseID))
                        {
                            if(iniDate.Equals(_ph.ActualFinish) || iniDate1.Equals(_ph.ActualFinish))
                            {
                                isFinished = false;
                            }
                        }
                    }
                    if (isFinished)
                    {
                        _proJIDList1.Add(_p.ProjectID);
                    }
                }

                //筛选 项目状态已完成
                _proJIDList = _context.Projects.Where(p => p.ProjectStatus == 4 && p.Enabled).Select(p => p.ProjectID);
                _proJIDList1.AddRange(_proJIDList);
                _proJIDList1 = _proJIDList1.Distinct().ToList();
            }

            if (!isDepFinished)
            {
                _projects = _context.Projects.Where(p => (p.ProjectStatus >= (int)ProjectStatus.CAD新建 && p.ProjectStatus < (int)ProjectStatus.完成) && p.Enabled == true && _proJIDList1.Contains(p.ProjectID));
            }
            else
            {
                if (depIds.Contains(Department))
                {
                    _projects = _context.Projects.Where(p => p.ProjectStatus == 4 && p.Enabled);
                }
                else
                {
                    _projects = _context.Projects.Where(p => (p.ProjectStatus >= (int)ProjectStatus.CAD新建 && p.ProjectStatus <= (int)ProjectStatus.完成) && p.Enabled == true && _proJIDList1.Contains(p.ProjectID));
                }
                
            }   
            return _projects.Distinct();
        }
    }
}
