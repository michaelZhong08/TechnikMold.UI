using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using TechnikSys.MoldManager.Domain.Entity;

namespace MoldManager.WebUI.Models.GridRowModel
{
    public class ProjectGridRowModel
    {
        public string[] cell;

        //Constructor 
        public ProjectGridRowModel(int PhaseCount, int PhaseType)
        {
            cell = new string[PhaseCount + 5];
            switch (PhaseType)
            {
                case 1:
                    cell[3] = "原计划";
                    break;
                case 2:
                    cell[3] = "调整计划";
                    break;
                case 3:
                    cell[3] = "实际完成";
                    break;
            }
        }

        public ProjectGridRowModel(Project Project, IEnumerable<ProjectPhase> ProjectPhase, int PhaseType, string Flitter, List<Phase> Phases)
        {
            int _phaseCount = Phases.Count()+5;
            int _phaseSeq = 0;
            cell = new string[_phaseCount];
            
            cell[0] = Project.ProjectID.ToString();
            
            cell[1] = Project.ProjectNumber+"<br>"+Project.CustomerName;
            if ((Project.ProjectStatus < 0)&&(Project.ParentID==0))
            {
                
                cell[1] = "<p style='background-color:#ff0000'>" + cell[1] + "</p>";
            }

            if (Project.MoldNumber != "---")
            {
                string _version = Project.Version < 10 ? "0" + Project.Version.ToString() : Project.Version.ToString();
                cell[2] = Project.MoldNumber + "(" + _version + ")" + "<br>钳工:" + Flitter;
            }
            else
            {
                cell[2] = Project.MoldNumber + "(" + Project.Version + ")";
            }
            string button = Project.Attachment;
            if ((Project.Attachment != "")&&(Project.Attachment != null))
            {            
                try
                {
                    button = button.Replace("\\", "\\\\");
                }
                catch
                {

                }

                string _ext = button.Substring(button.LastIndexOf('.')+1).ToLower();
                string _icon = "";
                switch (_ext)
                {
                    case "pdf":
                        _icon = "pdf";
                        break;
                    case "ppt":
                        _icon = "ppt";
                        break;
                    case "pptx":
                        _icon = "ppt";
                        break;
                    case "doc":
                        _icon = "word";
                        break;
                    case "docx":
                        _icon = "word";
                        break;
                    case "xls":
                        _icon = "excel";
                        break;
                    case "xlsx":
                        _icon = "excel";
                        break;
                    default:
                        _icon = "doc";
                        break;

                }
                //button = "<br><button class='btn' onclick='location.href=\"/Project/ProjectFile?ProjectID=" + Project.ProjectID + "\"'>附件</button>";
                button = "<br><a href='/Project/ProjectFile?ProjectID=" + Project.ProjectID + "'><img src='/Images/"+_icon+".png'></a>";
            }
            if (Project.ProjectStatus == 1)
            {
                cell[2] = "<font color='#ff0000'>" + cell[2] + "</font>" + button;
            }
            else
            {
                cell[2] = cell[2] + button;
                if (Project.MainPhaseChange) {
                    cell[2] = "<font color='#ff6a00'>" + cell[2] + "</font>";
                }
                
            }

            
            
            DateTime zero = new DateTime(1, 1, 1);
            
            
            foreach (Phase _phase in Phases)
            {
                ProjectPhase _projectPhase = ProjectPhase.Where(p => p.PhaseID == _phase.PhaseID).FirstOrDefault();
                if (_projectPhase == null)
                {
                    cell[_phaseSeq + 4] = "";
                }
                else
                {
                    
                    string _date;
                    DateTime _dateVal;
                    int _datediff = 0;
                    if (_projectPhase.PlanCFinish == new DateTime(1, 1, 1))
                    {
                        _dateVal = _projectPhase.PlanFinish;
                        
                    }
                    else
                    {
                        _dateVal = _projectPhase.PlanCFinish;
                    }

                    _datediff = (_dateVal-DateTime.Now).Days;

                    switch (PhaseType)
                    {
                        case 1:
                            _date = _projectPhase.PlanFinish.ToString("yy-MM-dd");
                            cell[3] = "原计划";
                            cell[_phaseSeq + 4] = _date == "01-01-01" ? "" : _date;
                            break;
                        case 2:
                            _date = _projectPhase.PlanCFinish.ToString("yy-MM-dd");
                            cell[3] = "调整计划";
                            cell[_phaseSeq + 4] = _date == "01-01-01" ? "" : _date;
                            break;
                        case 3:
                            _date = _projectPhase.ActualFinish.ToString("yy-MM-dd");
                            cell[3] = "实际完成";
                            cell[_phaseSeq + 4] = _date == "01-01-01" ? "" : _date;
                            break;
                    }

                    bool _phaseFinish = PhaseFinish(_projectPhase);


                    //Define cell font color if the main project phase is modified
                    if (_projectPhase.MainChange)
                    {
                        cell[_phaseSeq + 4] = "<font color='#0080FF'>" + cell[_phaseSeq + 4] + "</font>";
                    }


                    ///Define cell background
                    if (_projectPhase.ActualFinish == new DateTime(1,1,1))
                    {

                    
                        if ((_datediff >= 0) && (_datediff < 3) )//&& (!_phaseFinish))
                        {
                            cell[_phaseSeq + 4] = "<p style='background-color:#00ff00;'>" + cell[_phaseSeq + 4] + "</p>";
                        }
                        else if (_datediff < 0)//&& (!_phaseFinish))
                        {
                            cell[_phaseSeq + 4] ="<p style='background-color:#ff0000'>" + cell[_phaseSeq + 4] + "</p>";
                        }
                    }

                    
                }
                _phaseSeq = _phaseSeq + 1;
            }
            cell[cell.Length - 1] = Project.Memo;
        }

        private bool PhaseFinish(ProjectPhase Phase){
            
            if (Phase.ActualFinish.ToString("yyyy-MM-dd") == "0001-01-01")
            {
                return false;
            }
            else
            {
                return true;
            }
        }
    }
}