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
using System.Data.Common;
using System.Data.SqlClient;



namespace TechnikSys.MoldManager.Domain.Concrete
{
    public class ProjectPhaseRepository:IProjectPhaseRepository
    {
        private EFDbContext _context = new EFDbContext();

        public IQueryable<ProjectPhase> ProjectPhases
        {

            get
            {
                return _context.ProjectPhases;
            }
        }

        public int Save(ProjectPhase ProjectPhase)
        {
            if (ProjectPhase.ProjectPhaseID == 0)
            {
                _context.ProjectPhases.Add(ProjectPhase);
            }
            else
            {
                int _id = ProjectPhase.ProjectPhaseID;
                ProjectPhase _dbEntry = _context.ProjectPhases.Find(_id);
                if (_dbEntry != null)
                {
                    _dbEntry.ProjectID = ProjectPhase.ProjectID;
                    _dbEntry.PhaseID = ProjectPhase.PhaseID;

                    DateTime _value = ProjectPhase.PlanCFinish == new DateTime(1, 1, 1) ? ProjectPhase.PlanFinish : ProjectPhase.PlanCFinish;

                    if (_dbEntry.PlanFinish == new DateTime(1, 1, 1))
                    {
                        _dbEntry.PlanFinish = _value;//ProjectPhase.PlanCFinish;
                    }
                    else
                    {
                        _dbEntry.PlanCFinish = _value;//ProjectPhase.PlanCFinish;
                    }
                    _dbEntry.ActualFinish = ProjectPhase.ActualFinish;
                    _dbEntry.MainChange = ProjectPhase.MainChange;
                }
            }
            _context.SaveChanges();
            return ProjectPhase.ProjectPhaseID;
        }

        public int Save(int ProjectPhaseID, DateTime PhaseTime, int ProjectID = 0, int PhaseID = 0)
        {
            ProjectPhase _dbEntry = _context.ProjectPhases.Find(ProjectPhaseID);
            if (_dbEntry != null)
            {
                if (_dbEntry.PlanFinish == new DateTime(1, 1, 1))
                {
                    _dbEntry.PlanFinish = PhaseTime;//ProjectPhase.PlanCFinish;
                }
                else
                {
                    _dbEntry.PlanCFinish = PhaseTime;//ProjectPhase.PlanCFinish;
                }
                _context.SaveChanges();
            }
            else
            {
                _dbEntry = new ProjectPhase();
                _dbEntry.ProjectID = ProjectID;
                _dbEntry.PhaseID = PhaseID;
                _dbEntry.PlanFinish = PhaseTime;
                _dbEntry.PlanCFinish = new DateTime(1, 1, 1);
                _dbEntry.ActualFinish = new DateTime(1, 1, 1);
                _context.ProjectPhases.Add(_dbEntry);
            }
            _context.SaveChanges();
            return _dbEntry.ProjectPhaseID;
        }


        public IQueryable<ProjectPhase> GetProjectPhases(int ProjectID)
        {
            return _context.ProjectPhases.Where(p => p.ProjectID == ProjectID);
        }


        public ProjectPhase GetProjectPhase(int ProjectID, int PhaseID)
        {
            return GetProjectPhases(ProjectID).Where(p => p.PhaseID == PhaseID).FirstOrDefault();
        }


        public void ModifyPhase(int ProjectPhaseID, DateTime ModifyDate)
        {
            ProjectPhase _projectPhase = _context.ProjectPhases.Find(ProjectPhaseID);
            _projectPhase.PlanCFinish = ModifyDate;
            _context.SaveChanges();
        }

        /// <summary>
        /// Get the latest modifcation by ProjectPhaseID
        /// </summary>
        /// <param name="ProjectPhaseID"></param>
        /// <returns></returns>
        public DateTime GetCurrentPlan(int ProjectPhaseID)
        {
            ProjectPhase _projectPhase = _context.ProjectPhases.Find(ProjectPhaseID);

            return GetCurrent(_projectPhase);
        }

        /// <summary>
        /// Get the latest modifcation by ProjectID and PhaseID
        /// </summary>
        /// <param name="ProjectID"></param>
        /// <param name="PhaseID"></param>
        /// <returns></returns>
        public DateTime GetCurrentPlan(int ProjectID, int PhaseID)
        {
            ProjectPhase _projectPhase = GetProjectPhase(ProjectID, PhaseID);
            return GetCurrent(_projectPhase);
        }

        private DateTime GetCurrent(ProjectPhase ProjectPhase)
        {
            try
            {
                if (ProjectPhase.PlanCFinish.Year == 1)
                {
                    return ProjectPhase.PlanFinish;
                }
                else
                {
                    return ProjectPhase.PlanCFinish;
                }
            }
            catch
            {
                return new DateTime(1, 1, 1);
            }

        }


        public void FinishPhase(int ProjectID, int PhaseID)
        {

            ProjectPhase _phase = GetProjectPhase(ProjectID, PhaseID);
            _phase.ActualFinish = DateTime.Now;
            _context.SaveChanges();
        }





        ProjectPhase IProjectPhaseRepository.GetProjectPhase(int ProjectPhaseID)
        {
            return _context.ProjectPhases.Find(ProjectPhaseID);
        }
    }
}
