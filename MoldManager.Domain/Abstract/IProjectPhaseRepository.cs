
/*
 * Create By:lechun1
 * 
 * Description: public interface for repository of TechnikSys.MoldManager.Domain.Entity.ProjectPhase 
 * 
 */

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TechnikSys.MoldManager.Domain.Entity;

namespace TechnikSys.MoldManager.Domain.Abstract
{
    public interface IProjectPhaseRepository
    {
        IQueryable<ProjectPhase> ProjectPhases { get; }

        int Save(ProjectPhase ProjectPhases);

        int Save(int ProjectPhaseID, DateTime PhaseTime, int ProjectID = 0, int PhaseID = 0);

        IEnumerable<ProjectPhase> GetProjectPhases(int ProjectID);

        ProjectPhase GetProjectPhase(int ProjectID, int PhaseID);

        void ModifyPhase(int ProjectPhaseID, DateTime ModifyDate);

        ProjectPhase GetProjectPhase(int ProjectPhaseID);

        DateTime GetCurrentPlan(int ProjectPhaseID);

        DateTime GetCurrentPlan(int ProjectID, int PhaseID);

        void FinishPhase(int ProjectID, int PhaseID);


    }
}
