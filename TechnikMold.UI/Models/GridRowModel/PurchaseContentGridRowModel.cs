using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using TechnikSys.MoldManager.Domain.Entity;
using TechnikSys.MoldManager.Domain.Abstract;

namespace MoldManager.WebUI.Models.GridRowModel
{
    public class PurchaseContentGridRowModel
    {
        public string[] cell;
        //新建PR单
        public PurchaseContentGridRowModel(Part Part, string MoldNumber)
        {
            cell = new string[22];
            cell[0] = "";
            cell[1] = Part.PartID.ToString();
            cell[2] = "0";
            cell[3] = "0";
            cell[4] = Part.ShortName;
            cell[5] = (Part.Quantity + Part.AppendQty).ToString();
            cell[6] = Part.PartNumber;
            cell[7] = Part.Specification;
            cell[8] = Part.MaterialName;
            cell[9] = "";//Part.Hardness;
            cell[10]= Part.JobNo==null?"":Part.JobNo;
            cell[11] = Part.BrandName;
            cell[12] =Part.SupplierName;
            cell[13] = Part.PurchaseDrawing.ToString();
            
            cell[14] = "新建";
            cell[15] = "0";
            cell[16] = "";
            cell[17] = MoldNumber;
            cell[18] = "";
            cell[19] = "";
            cell[20] = "";//
            cell[21] = Part.Memo;
        }
        //外发任务
        public PurchaseContentGridRowModel(Task Task, IProjectPhaseRepository ProjectPhaseRepository, ISteelCAMDrawingRepository SteelDrawingRepo)
        {
            int _phaseID = 0;
            cell = new string[22];
            cell[0] = "";
            cell[1] = "0";
            cell[2] = Task.TaskID.ToString();
            cell[3] = "0";
            //零件名 taskname+processname
            cell[4] = Task.TaskName+"_"+Task.ProcessName;
            //数量
            if (Task.TaskType == 1)
            {
                cell[5] = (Task.R + Task.F).ToString();
            }
            else
            {
                cell[5] = Task.Quantity.ToString();
            }
            //物料编号 模具号-taskid
            cell[6] = Task.MoldNumber+"-"+Task.TaskID.ToString();
            //规格
            cell[7] = Task.Raw;
            //if (Task.TaskType == 4)
            //{
            //    SteelCAMDrawing _steeldrawing = SteelDrawingRepo.QueryByFullName(Task.TaskName);
            //    if (_steeldrawing != null)
            //    {
            //        cell[7] = _steeldrawing.CADPartName;
            //    }
            //    else
            //    {
            //        cell[7] = "";
            //    }

            //}
            //else
            //{
            //    cell[7] = Task.Raw;
            //}
            //材料
            cell[8] = Task.Material;
            //硬度
            cell[9] = Task.HRC;
            //零件号
            cell[10]= Task.TaskID.ToString();
            cell[11] = "";
            cell[12] = "";
            cell[13] = "true";
            
            cell[14] = "新建";
            cell[15] = "0";
            //switch (Task.TaskType)
            //{
            //    case 1:
            //        _phaseID = 8;
            //        break;
            //    case 2:
            //        _phaseID = 9;
            //        break;
            //    case 3:
            //        _phaseID = 10;
            //        break;
            //    case 4:
            //        _phaseID = 8;
            //        break;
            //    case 6:
            //        _phaseID = 7;
            //        break;
            //    default:
            //        _phaseID = 8;
            //        break;
            //}

            //try
            //{
            //    ProjectPhase _phase = ProjectPhaseRepository.GetProjectPhases(Task.ProjectID).Where(p => p.PhaseID == _phaseID).FirstOrDefault();
            //    cell[16] = _phase.PlanCFinish == new DateTime(1, 1, 1) ? _phase.PlanFinish.ToString("yyyy-MM-dd") :
            //        _phase.PlanCFinish.ToString("yyyy-MM-dd");
            //}
            //catch
            //{
            //    cell[16] = "-";
            //}
            cell[16] = Task.PlanTime.ToString("yyyy-MM-dd");
            cell[17] = Task.MoldNumber;
            cell[18] = "";
            cell[19] = "";
            cell[20] = "";
            cell[21] = Task.Memo;
        }
        //库存新增
        public PurchaseContentGridRowModel(WarehouseStock StockItem)
        {
            cell = new string[22];
            cell[0] = "";
            cell[1] = "0";
            cell[2] = "0";
            cell[3] = StockItem.WarehouseStockID.ToString();
            cell[4] = StockItem.Name;
            int _qty = StockItem.PlanQty - StockItem.Quantity>0? StockItem.PlanQty - StockItem.Quantity:0;
            
            cell[5] = _qty.ToString();
            cell[6] = StockItem.MaterialNumber;
            cell[7] = StockItem.Specification;
            cell[8] = StockItem.Material;
            cell[9] = "";
            cell[10] = "";
            cell[11] = "";
            cell[12] = StockItem.SupplierName;
            cell[13] = "false";
            
            cell[14] = "新建";
            cell[15] = "0";
            cell[16] = "";
            cell[17] = "XX";
            cell[18] = "";
            cell[19] = "";
            cell[20] = "";
            cell[21] = "";
        }
        //编辑PR单
        public PurchaseContentGridRowModel(PRContent PRContent, string State, string CostCenter, string ERPNo)
        {
            cell = new string[22];
            cell[0] = PRContent.PRContentID.ToString();
            cell[1] = PRContent.PartID.ToString();
            cell[2] = PRContent.TaskID.ToString();
            cell[3] = PRContent.WarehouseStockID.ToString();
            //获取零件短名
            string name = PRContent.PartName??"";
            //name = name.Substring(name.IndexOf('_') + 1, name.LastIndexOf('_') - name.IndexOf('_') - 1);
            //name = name.Substring(0, name.LastIndexOf('_') - 1);
            //
            cell[4] = string.IsNullOrEmpty(name) ? PRContent.PartName : name;
            cell[5] = PRContent.Quantity.ToString();
            cell[6] = PRContent.PartNumber;
            cell[7] = PRContent.PartSpecification;
            cell[8] = PRContent.MaterialName;
            cell[9] = PRContent.Hardness??"";
            cell[10]= PRContent.JobNo;
            cell[11] = PRContent.BrandName;
            cell[12] = PRContent.SupplierName;
            cell[13] = PRContent.PurchaseDrawing.ToString();
                      
            cell[14] = State;
            cell[15] = PRContent.PurchaseItemID.ToString();
            cell[16] = PRContent.RequireTime == new DateTime(1900, 1, 1) ? "-" : PRContent.RequireTime.ToString("yyyy-MM-dd");
            cell[17] = PRContent.MoldNumber;
            cell[18] = PRContent.CostCenterID.ToString();
            cell[19] = CostCenter;
            cell[20] = ERPNo;
            cell[21] = PRContent.Memo;
        }        
    }
}