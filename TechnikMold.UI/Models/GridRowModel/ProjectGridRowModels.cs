using System;
using System.Collections.Generic;
//using System.Data;
using System.Linq;
using System.Web;
using TechnikSys.MoldManager.Domain.Entity;
using TechnikSys.MoldManager.Domain.Status;

namespace MoldManager.WebUI.Models.GridRowModel
{
    public class ProjectGridRowModels
    {
        public ProjectGridRowModel[] ProjectRows;
        //public ProjectGridRowModels(DataRow dr)
        //{
        //    ProjectRows=new ProjectGridRowModel[1];
        //    ProjectRows[0] = new ProjectGridRowModel(dr);
        //}

        public ProjectGridRowModels(Project Project, IQueryable<ProjectPhase> ProjectPhase, List<ProjectRole> Flitter, List<Phase> Phases, int attQty,string _mainProJName)
        {
            ProjectRows = new ProjectGridRowModel[3];
            #region 遍历项目各阶段判断是否可以被关闭
            bool Closed = true;
            List<ProjectPhase> _proJh1 = ProjectPhase.ToList().Where(p => p.ProjectID == Project.ProjectID).ToList();
            foreach(var h in _proJh1)
            {
                //源计划存在 实际结束不存在
                if(!CheckZero(h.PlanFinish) && CheckZero(h.ActualFinish))
                {
                    Closed = false;
                }
            }
            //List<ProjectPhase> _proJh1 = ProjectPhase.ToList().Where(p => p.ProjectID == Project.ProjectID && !CheckZero(p.PlanFinish)).ToList();
            //if (_proJh1.Count > 0)
            //{
            //    foreach (var p in Phases)
            //    {
            //        ProjectPhase _proJh = ProjectPhase.ToList().Where(h => h.ProjectID == Project.ProjectID && !CheckZero(h.PlanFinish)).FirstOrDefault();
            //        if (_proJh != null)
            //        {
            //            if (CheckZero(_proJh.ActualFinish))
            //            {
            //                Closed = false;
            //            }
            //        }
            //    }
            //}
            //else
            //{
            //    Closed = false;
            //}
            #endregion
            string _cell2 = BuildCell2(Project, Flitter, attQty, Closed);
            for (int i = 0; i < 3; i++)
            {
                ProjectRows[i] = new ProjectGridRowModel(Phases.Count, i + 1);
                ProjectRows[i].cell[0] = Project.ProjectID.ToString();
                ProjectRows[i].cell[1] = "<label>"+Project.ProjectNumber+ "</label>" + "<br>"+ _mainProJName + "<br>" + Project.CustomerName;
                if ((Project.ProjectStatus < 0) && (Project.ParentID == 0))
                {
                    ProjectRows[i].cell[1] = "<p style='background-color:#ff0000'>" + ProjectRows[i].cell[1] + "</p>";
                }
                ProjectRows[i].cell[2] = _cell2;
            }

            int _phaseSeq = 0;

            foreach (Phase _phase in Phases)
            {
                DateTime _dateval;
                int _datediff = 0;
                int _datediffAc = 0;
                ProjectPhase _prjPhase = ProjectPhase.Where(p => p.PhaseID == _phase.PhaseID).FirstOrDefault();
                if (_prjPhase != null)
                {
                    _dateval = CheckZero(_prjPhase.PlanCFinish) ? _prjPhase.PlanFinish : _prjPhase.PlanCFinish;
                    _datediff = (_dateval - DateTime.Now).Days;

                    ProjectRows[0].cell[_phaseSeq + 4] = CheckZero(_prjPhase.PlanFinish) ? "" : _prjPhase.PlanFinish.ToString("yy/MM/dd");
                    ProjectRows[1].cell[_phaseSeq + 4] = CheckZero(_prjPhase.PlanCFinish) ? "" : _prjPhase.PlanCFinish.ToString("yy/MM/dd");
                    ProjectRows[2].cell[_phaseSeq + 4] = CheckZero(_prjPhase.ActualFinish) ? "" : _prjPhase.ActualFinish.ToString("yy/MM/dd");
                    if (CheckZero(_prjPhase.ActualFinish))
                    {
                        if ((_datediff >= 0) && (_datediff < 3))
                        {
                            //"<p style='background-color:#00ff00;'>" + cell[_phaseSeq + 4] + "</p>";
                            ProjectRows[0].cell[_phaseSeq + 4] = "<p class='td_planDate' style='background: linear-gradient(rgba(255,127,36,1), rgba(255,127,36,0.1) 50% ,rgba(255,127,36,1)   );'>" + ProjectRows[0].cell[_phaseSeq + 4] + "</p>";
                            if (!CheckZero(_prjPhase.PlanCFinish))
                                ProjectRows[1].cell[_phaseSeq + 4] = "<p class='td_planDate' style='background: linear-gradient(rgba(255,127,36,1), rgba(255,127,36,0.1) 50% ,rgba(255,127,36,1)   );'>" + ProjectRows[1].cell[_phaseSeq + 4] + "</p>";
                            //ProjectRows[2].cell[_phaseSeq + 4] = "<p style='background: linear-gradient(rgba(51,153,0,1), rgba(0,255,0,0.1) 50% ,rgba(51,153,0,1)   );'>" + ProjectRows[2].cell[_phaseSeq + 4] + "</p>";
                        }
                        else if (_datediff < 0)
                        {
                            //"<p style='background-color:#ff0000'>" + cell[_phaseSeq + 4] + "</p>";
                            ProjectRows[0].cell[_phaseSeq + 4] = "<p class='td_planDate' style='background: linear-gradient(rgba(255,0,0,1), rgba(255,0,0,0.1) 50% ,rgba(255,0,0,1)   );'>" + ProjectRows[0].cell[_phaseSeq + 4] + "</p>";
                            if (!CheckZero(_prjPhase.PlanCFinish))
                                ProjectRows[1].cell[_phaseSeq + 4] = "<p class='td_planDate' style='background: linear-gradient(rgba(255,0,0,1), rgba(255,0,0,0.1) 50% ,rgba(255,0,0,1)   );'>" + ProjectRows[1].cell[_phaseSeq + 4] + "</p>";
                            //ProjectRows[2].cell[_phaseSeq + 4] = "<p style='background: linear-gradient(rgba(255,0,0,1), rgba(255,0,0,0.1) 50% ,rgba(255,0,0,1)   );'>" + ProjectRows[2].cell[_phaseSeq + 4] + "</p>";
                        }
                    }
                    else
                    {
                        _datediffAc = (_dateval - _prjPhase.ActualFinish).Days;
                        if (_datediffAc >= 0)
                        {
                            ProjectRows[2].cell[_phaseSeq + 4] = "<p class='td_planDate' style='background: linear-gradient(rgba(51,153,0,1), rgba(0,255,0,0.1) 50% ,rgba(51,153,0,1)   );'>" + ProjectRows[2].cell[_phaseSeq + 4] + "</p>";
                        }
                        else
                        {
                            ProjectRows[2].cell[_phaseSeq + 4] = "<p class='td_planDate' style='background: linear-gradient(rgba(255,0,0,1), rgba(255,0,0,0.1) 50% ,rgba(255,0,0,1)   );'>" + ProjectRows[2].cell[_phaseSeq + 4] + "</p>";
                        }
                    }
                }
                else
                {
                    ProjectRows[0].cell[_phaseSeq + 4] = "";
                    ProjectRows[1].cell[_phaseSeq + 4] = "";
                    ProjectRows[2].cell[_phaseSeq + 4] = "";
                }
                _phaseSeq++;

            }

            for (int i = 0; i < 3; i++)
            {
                ProjectRows[i].cell[ProjectRows[i].cell.Length - 1] = Project.Memo;
            }

        }

        private bool CheckZero(DateTime _date)
        {
            DateTime _datezero = new DateTime(1, 1, 1);
            if (_date < DateTime.Parse("2000/01/01"))
            {
                return true;
            }
            if (_date == _datezero)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        private string BuildCell2(Project Project, List<ProjectRole> Flitter, int attQty,bool Closed)
        {
            string _cellContent = "";
            if (Project.MoldNumber != "---")
            {
                string _version = Project.Version < 10 ? "0" + Project.Version.ToString() : Project.Version.ToString();
                string _title = "";
                _title = "模具号："+Project.MoldNumber + "(" +"版本："+ _version + ")" + "&#10;";
                _title = _title + "模具名："+ Project.Name + "&#10;";
                for (var i = 1; i <= Flitter.Count; i++)
                {
                    string _name = "";
                    if (i == 1)
                        _name = "项目经理：";
                    else if(i==2)
                        _name = "技术负责人：";
                    else if(i==3)
                        _name = "钳工：";
                    _title = _title+ _name + Flitter[i - 1].UserName + "&#10;";
                }
                //样式备注：文字块占上方64% 与上方留余10px空间; button占下方34% 并贴底;
                _cellContent = "<div style='font-size:10px;line-height:10px;height:100%!important;padding-top:10px!important;'><div style='top:5px;height:62%!important;'title='" + _title + "'><label>" + Project.MoldNumber + "(" + _version + ")" + "</label>" + "<br>" + "<label>" + Project.Name + "</label>" + "<br><label >钳工:" + Flitter.Where(p => p.RoleID == 3).FirstOrDefault().UserName + "</label></div>";
            }
            else
            {
                string _version = Project.Version < 10 ? "0" + Project.Version.ToString() : Project.Version.ToString();
                _cellContent = "<div style='font-size:10px;line-height:10px;height:100%!important;padding-top:10px!important;'><div style='top:5px;height:62%!important;'>"+ Project.MoldNumber+"(" + _version + ")"+" </div>";//Project.MoldNumber;//+ "(" + Project.Version + ")";
            }
            #region region
            //string button = Project.Attachment;
            //if ((Project.Attachment != "")&&(Project.Attachment != null))
            //{            
            //    try
            //    {
            //        button = button.Replace("\\", "\\\\");
            //    }
            //    catch
            //    {

            //    }

            //    string _ext = button.Substring(button.LastIndexOf('.')+1).ToLower();
            //    string _icon = "";
            //    switch (_ext)
            //    {
            //        case "pdf":
            //            _icon = "pdf";
            //            break;
            //        case "ppt":
            //            _icon = "ppt";
            //            break;
            //        case "pptx":
            //            _icon = "ppt";
            //            break;
            //        case "doc":
            //            _icon = "word";
            //            break;
            //        case "docx":
            //            _icon = "word";
            //            break;
            //        case "xls":
            //            _icon = "excel";
            //            break;
            //        case "xlsx":
            //            _icon = "excel";
            //            break;
            //        default:
            //            _icon = "doc";
            //            break;

            //    }
            //    //button = "<br><button class='btn' onclick='location.href=\"/Project/ProjectFile?ProjectID=" + Project.ProjectID + "\"'>附件</button>";
            //    button = "<br><a href='/Project/ProjectFile?ProjectID=" + Project.ProjectID + "'><img src='/Images/"+_icon+".png'></a>";
            //}
            #endregion
            string btnColor="";
            if (attQty == 0)
            {
                btnColor = "btn-warning";
            }
            else
            {
                btnColor = "btn-success";
            }

            string button = "<button  id='" + Project.ProjectID.ToString() + "' class='attachbtn  " + btnColor + "'  style='font-size:10px;width: 100%; height: auto;float:right!important;height:38%!important;' onclick='ShowProjectFile(" + Project.ProjectID.ToString() + ")'><span class='glyphicon glyphicon-paperclip'></span> 附件(" + attQty.ToString() + ")</button></div>";

            #region 颜色设置
            if (Project.ProjectStatus == (int)ProjectStatus.完成)
            {
                _cellContent = "<font color='#0080FF'>" + _cellContent + "</font>" + button;
            }
            else
            {
                if (Closed)
                {
                    _cellContent = "<font color='#00BB00'>" + _cellContent + "</font>" + button;
                }
                else
                {
                    _cellContent = _cellContent + button;
                }
            }
            #endregion
            
            return _cellContent;
        }


    }
}