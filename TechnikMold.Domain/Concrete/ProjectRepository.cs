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
                if (Project.Type == 2)
                {
                    Project.Version = GetLatestActiveProject(Project.MoldNumber).Version + 1;
                }
                _context.Projects.Add(Project);
            }
            else
            {
                Project _dbEntry = _context.Projects.Find(Project.ProjectID);
                if (_dbEntry != null)
                {
                     _dbEntry.Name =Project.Name;
                     _dbEntry.ProjectNumber = Project.ProjectNumber;
                     _dbEntry.Type =Project.Type;
                     _dbEntry.MoldNumber =Project.MoldNumber;
                     _dbEntry.CustomerID =Project.CustomerID;
                     _dbEntry.CustomerName =Project.CustomerName;
                     _dbEntry.ProjectStatus =Project.ProjectStatus;
                    _dbEntry.Memo = Project.Memo==null?"":Project.Memo;
                    _dbEntry.Attachment=Project.Attachment==null?"":Project.Attachment;
                     _dbEntry.ParentID = Project.ParentID;
                     _dbEntry.Enabled = Project.Enabled;
                     _dbEntry.OldID = Project.OldID;
                     _dbEntry.CreateTime = Project.CreateTime;
                     _dbEntry.FinishTime = Project.FinishTime;
                     _dbEntry.FixMoldType = Project.FixMoldType;
                     _dbEntry.MainPhaseChange = Project.MainPhaseChange;
                }
            }

            

            IEnumerable<Project> _subs = _context.Projects.Where(p => p.ParentID == Project.ProjectID);
            if (_subs.Count() > 0)
            {
                foreach (Project _sub in _subs)
                {
                    _sub.CustomerID = Project.CustomerID;
                    _sub.CustomerName = Project.CustomerName;
                    
                }
            }
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
                    .Where(p=>p.Enabled==true)
                    .FirstOrDefault();
            }
            else
            {
                _dbEntry = _context.Projects.Where(p => p.MoldNumber == MoldNumber).Where(p=>p.Enabled==true).FirstOrDefault();
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
                _dbEntry.ProjectStatus = 1;
                _memoTitle = "项目暂停:<br>"+_memoTitle;
            }
            else
            {
                _dbEntry.ProjectStatus = 0;
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
            _project.ProjectStatus = 90;
            _context.SaveChanges();
            return ProjectID;
        }

        public IEnumerable<Project> QueryByMainProject(int ProjectID)
        {
            IEnumerable<Project> _subProjects = _context.Projects.Where(p => p.ParentID == ProjectID).Where(p=>p.Enabled);
            return  _subProjects;
        }


        public int DeleteProject(int ProjectID, string Memo)
        {
            Project _project = QueryByID(ProjectID);
            if (_project != null)
            {
                _project.Enabled = false;
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



        
    }
}
