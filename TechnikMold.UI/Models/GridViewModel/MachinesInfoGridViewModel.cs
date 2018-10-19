using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using TechnikSys.MoldManager.Domain.Entity;
using TechnikSys.MoldManager.Domain.Abstract;
using MoldManager.WebUI.Models.Helpers;
using TechnikMold.UI.Models.GridRowModel;

namespace TechnikMold.UI.Models.GridViewModel
{
    public class MachinesInfoGridViewModel
    {
        public List<MachinesInfoGridRowModel> rows = new List<MachinesInfoGridRowModel>();
        public int Page;
        public int Total;
        public int Records;
        public MachinesInfoGridViewModel(List<MachinesInfo> _machinesInfos,IDepartmentRepository IDepartment,IMachinesInfoRepository IMachinesInfo)
        {             
            foreach(var m in _machinesInfos)
            {
                string _typeName="";
                string _depName="";
                #region 构造任务类型 表内容
                if (m.TaskType.Contains("1"))
                {
                    _typeName = "电极;";
                }
                if (m.TaskType.Contains("2"))
                {
                    _typeName = _typeName + "EDM;";
                }
                if (m.TaskType.Contains("3"))
                {
                    _typeName = _typeName + "WEDM;";
                }
                if (m.TaskType.Contains("4"))
                {
                    _typeName = _typeName + "CNC;";
                }
                if (m.TaskType.Contains("6"))
                {
                    if (m.Stype.Contains("0"))
                        _typeName = _typeName + "MG;";
                    if (m.Stype.Contains("1"))
                        _typeName = _typeName + "铣床;";
                    if (m.Stype.Contains("2"))
                        _typeName = _typeName + "磨床;";
                    if (m.Stype.Contains("3"))
                        _typeName = _typeName + "车;";
                }
                if (m.TaskType.Contains("100"))
                {
                    _typeName = _typeName + "QC;";
                }
                #endregion
                if (IDepartment.GetByID(m.DepartmentID) != null)
                {
                    _depName = IDepartment.GetByID(m.DepartmentID).Name ?? "-";
                }
                else
                    _depName = "-";
                rows.Add(new MachinesInfoGridRowModel(_typeName, _depName, m));
            }
        }
    }
}