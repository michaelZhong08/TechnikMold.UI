using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using TechnikSys.MoldManager.Domain.Entity;
using TechnikSys.MoldManager.Domain.Abstract;
using TechnikMold.UI.Models.ViewModel;

namespace MoldManager.WebUI.Models.GridRowModel
{
    public class PurchaseContentGridRowModel
    {
        public string[] cell;
        //新建PR单
        public PurchaseContentGridRowModel(Part Part,string mrPurDate, string MoldNumber)
        {
            cell = new string[26];
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
            cell[16] = mrPurDate;
            cell[17] = MoldNumber;
            cell[18] = "";
            cell[19] = "";
            cell[20] = "";//
            cell[21] = Part.Memo;
            cell[22] = "";
            cell[23] = "";
            cell[24] = "";
            cell[25] = Part.TotalQty.ToString();
        }
        //外发任务
        public PurchaseContentGridRowModel(Task Task, SetupTaskStart _setuptaskStart,string mrPurDate, IProjectPhaseRepository ProjectPhaseRepository, ISteelCAMDrawingRepository SteelDrawingRepo
            ,IWHPartRepository WHPartRepository,IPurchaseTypeRepository PurchaseTypeRepository)
        {
            int _phaseID = 0;
            string _partNum = WHPartRepository.GetwfTaskPartNum(Task.TaskID);
            cell = new string[27];
            cell[0] = "";
            cell[1] = "0";
            cell[2] = Task.TaskID.ToString();
            cell[3] = "0";
            //零件名 taskname+processname
            cell[4] = Task.TaskName+"_"+Task.ProcessName+"(V"+string.Format("{0:00}", Task.Version) +")";
            //数量
            if (Task.TaskType == 1)
            {
                cell[5] = (Task.R + Task.F).ToString();
            }
            else
            {
                //cell[5] = Task.Quantity.ToString();
                cell[5] = _setuptaskStart.Qty.ToString();
            }
            ////物料编号 模具号-taskid
            cell[6] = _partNum;//Task.MoldNumber+"-"+Task.TaskID.ToString();
            //规格
            cell[7] = Task.Raw;
            //材料
            cell[8] = Task.Material;
            //硬度
            cell[9] = Task.HRC;
            //零件号
            cell[10]= _partNum.Split('-')[1];
            cell[11] = "";
            cell[12] = _setuptaskStart.MachinesName ?? "";
            cell[13] = "true";
            
            cell[14] = "新建";
            cell[15] = "0";
            cell[16] = mrPurDate??Task.PlanTime.ToString("yyyy-MM-dd");
            cell[17] = Task.MoldNumber;
            cell[18] = "";
            cell[19] = "";
            cell[20] = "";
            cell[21] = Task.Memo;
            cell[22] = _setuptaskStart.UserName ?? "";
            cell[23] = _setuptaskStart.MachinesName ?? "";
            cell[24] = _setuptaskStart.MachinesCode ?? "";
            cell[25] = Task.Quantity.ToString();
            int _purchaseType=0;
            if (Task.TaskType == 6)
            {
                switch (Task.OldID)//0 铣/1 磨/4 全加工/3 车
                {
                    case 0:
                        _purchaseType = PurchaseTypeRepository.QueryByName("铣床外发").PurchaseTypeID;
                        break;
                    case 1:
                        _purchaseType = PurchaseTypeRepository.QueryByName("磨床外发").PurchaseTypeID;
                        break;
                    case 3:
                        _purchaseType = PurchaseTypeRepository.QueryByName("车外发").PurchaseTypeID;
                        break;
                    case 4:
                        _purchaseType = PurchaseTypeRepository.QueryByName("全加工外发").PurchaseTypeID;
                        break;
                    default:
                        _purchaseType = PurchaseTypeRepository.QueryByName("铣磨外发").PurchaseTypeID;
                        break;
                }
            }
            else
            {
                _purchaseType = PurchaseTypeRepository.PurchaseTypes.ToList().Where(t => Task.TaskType.Equals(Convert.ToInt32(t.TaskType))).FirstOrDefault().PurchaseTypeID;
            }           
            cell[26] = _purchaseType.ToString();
        }
        //库存新增
        public PurchaseContentGridRowModel(string mrPurDate, WarehouseStock StockItem)
        {
            cell = new string[26];
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
            cell[16] = mrPurDate;
            cell[17] = "XX";
            cell[18] = "";
            cell[19] = "";
            cell[20] = "";
            cell[21] = "";
            cell[22] = "";
            cell[23] = "";
            cell[24] = "";
            cell[25] = "0";
        }
        //编辑PR单
        public PurchaseContentGridRowModel(PRContent PRContent, string State, string CostCenter, string ERPNo,SetupTaskStart _setupTask)
        {
            cell = new string[27];
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
            cell[22] = _setupTask.UserName ?? "";
            cell[23] = _setupTask.MachinesName ?? "";
            cell[24] = _setupTask.MachinesCode ?? "";
            cell[25] = PRContent.Quantity.ToString();
            cell[26] = PRContent.PurchaseTypeID.ToString();
        }        
    }
}