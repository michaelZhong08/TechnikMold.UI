using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using TechnikSys.MoldManager.Domain.Entity;

namespace TechnikMold.UI.Models.GridRowModel
{
    public class MachinesInfoGridRowModel
    {
        public string[] cell ;
        public MachinesInfoGridRowModel(string TaskTypeName,string DepName ,MachinesInfo _machinesinfo)
        {
            cell = new string[10];
            cell[0] = _machinesinfo.MachineCode;
            cell[1] = _machinesinfo.MachineName;
            cell[2] = _machinesinfo.EquipBrand;
            cell[3] = DepName;
            cell[4] = TaskTypeName;
            cell[5] = _machinesinfo.Capacity.ToString();
            cell[6] = _machinesinfo.Downtime.ToString();
            cell[7] = _machinesinfo.Cost.ToString();
            cell[8] = _machinesinfo.Status > 0 ? "true" : "false";
            cell[9] = _machinesinfo.DepartmentID.ToString();
        }
    }
}