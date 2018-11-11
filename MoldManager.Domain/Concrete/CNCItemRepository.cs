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
using TechnikSys.MoldManager.Domain.Entity;
using TechnikSys.MoldManager.Domain.Abstract;
using TechnikSys.MoldManager.Domain.Status;

namespace TechnikSys.MoldManager.Domain.Concrete
{
    public class CNCItemRepository:ICNCItemRepository
    {
        private EFDbContext _context = new EFDbContext();

        public IQueryable<CNCItem> CNCItems
        {
            get { return _context.CNCItems; }
        }

        public int Save(CNCItem CNCItem)
        {
            bool _isNew = false;
            CNCItem _dbEntry = QueryByID(CNCItem.CNCItemID);
            if (_dbEntry == null)
            {
                _dbEntry = QueryByLabel(CNCItem.LabelName);
                if (_dbEntry == null)
                {
                    _context.CNCItems.Add(CNCItem);
                    _isNew = true;
                }
                else
                {
                    _dbEntry.TaskID = CNCItem.TaskID;
                    _dbEntry.LabelName = CNCItem.LabelName;
                    _dbEntry.Material = CNCItem.Material;
                    _dbEntry.OffsetX = CNCItem.OffsetX;
                    _dbEntry.OffsetY = CNCItem.OffsetY;
                    _dbEntry.OffsetZ = CNCItem.OffsetZ;
                    _dbEntry.OffsetC = CNCItem.OffsetC;
                    _dbEntry.GapCompensation = CNCItem.GapCompensation;
                    _dbEntry.ZCompensation = CNCItem.ZCompensation;
                    _dbEntry.Status = CNCItem.Status;
                    _dbEntry.CreateTime = CNCItem.CreateTime;
                    _dbEntry.CNCStartTime = CNCItem.CNCStartTime;
                    _dbEntry.CNCFinishTime = CNCItem.CNCFinishTime;
                    _dbEntry.CNCMachine = CNCItem.CNCMachine;
                    _dbEntry.Destroy = CNCItem.Destroy;
                    _dbEntry.DestroyTime = CNCItem.DestroyTime;
                    _dbEntry.Gap = CNCItem.Gap;
                    _dbEntry.EleType = CNCItem.EleType;
                    _dbEntry.HeightMax = CNCItem.HeightMax;
                    _dbEntry.HeightMin = CNCItem.HeightMin;
                    _dbEntry.GapMax = CNCItem.GapMax;
                    _dbEntry.GapMin = CNCItem.GapMin;
                    _dbEntry.LabelToPrint = CNCItem.LabelToPrint;
                    _dbEntry.LabelPrinted = CNCItem.LabelPrinted;
                    _dbEntry.SafetyHeight = CNCItem.SafetyHeight;
                    _dbEntry.ELE_INDEX = CNCItem.ELE_INDEX;
                    _dbEntry.CNCMachMethod = CNCItem.CNCMachMethod;
                    _dbEntry.MoldNumber = CNCItem.MoldNumber;
                    _dbEntry.QCFinishTime = CNCItem.QCFinishTime;
                }

            }
            else
            {
                _dbEntry.TaskID = CNCItem.TaskID;
                _dbEntry.LabelName = CNCItem.LabelName;
                _dbEntry.Material = CNCItem.Material;
                _dbEntry.OffsetX = CNCItem.OffsetX;
                _dbEntry.OffsetY = CNCItem.OffsetY;
                _dbEntry.OffsetZ = CNCItem.OffsetZ;
                _dbEntry.OffsetC = CNCItem.OffsetC;
                _dbEntry.GapCompensation = CNCItem.GapCompensation;
                _dbEntry.ZCompensation = CNCItem.ZCompensation;
                _dbEntry.Status = CNCItem.Status;
                _dbEntry.CreateTime = CNCItem.CreateTime;
                _dbEntry.CNCStartTime = CNCItem.CNCStartTime;
                _dbEntry.CNCFinishTime = CNCItem.CNCFinishTime;
                _dbEntry.CNCMachine = CNCItem.CNCMachine;
                _dbEntry.Destroy = CNCItem.Destroy;
                _dbEntry.DestroyTime = CNCItem.DestroyTime;
                _dbEntry.Gap = CNCItem.Gap;
                _dbEntry.EleType = CNCItem.EleType;
                _dbEntry.HeightMax = CNCItem.HeightMax;
                _dbEntry.HeightMin = CNCItem.HeightMin;
                _dbEntry.GapMax = CNCItem.GapMax;
                _dbEntry.GapMin = CNCItem.GapMin;
                _dbEntry.LabelToPrint = CNCItem.LabelToPrint;
                _dbEntry.LabelPrinted = CNCItem.LabelPrinted;
                _dbEntry.SafetyHeight = CNCItem.SafetyHeight;
                _dbEntry.ELE_INDEX = CNCItem.ELE_INDEX;
                _dbEntry.CNCMachMethod = CNCItem.CNCMachMethod;
                _dbEntry.MoldNumber = CNCItem.MoldNumber;
                _dbEntry.QCFinishTime = CNCItem.QCFinishTime;

            }

            _context.SaveChanges();
            if (_isNew)
            {
                return CNCItem.CNCItemID;
            }
            else
            {
                return _dbEntry.CNCItemID;
            }

        }

        public CNCItem QueryByID(int CNCItemID)
        {
            return _context.CNCItems.Find(CNCItemID);
        }

        public IEnumerable<CNCItem> QueryByTaskID(int TaskID)
        {
            return _context.CNCItems.Where(c => c.TaskID == TaskID);
        }


        public void SetPrinted(int CNCItemID)
        {
            CNCItem _item = QueryByID(CNCItemID);
            _item.LabelPrinted = true;
            _context.SaveChanges();
        }


        public string SetToPrint(int CNCItemID, bool Reprint = false)
        {

            CNCItem _item = QueryByID(CNCItemID);
            _item.Status = (int)CNCItemStatus.备料;
            if (Reprint)
            {
                _item.LabelPrinted = false;
                _item.LabelToPrint = true;
                _context.SaveChanges();
                return "";
            }
            else
            {
                if (!_item.LabelPrinted)
                {
                    _item.LabelToPrint = true;
                    _context.SaveChanges();
                    return "";
                }
                else
                {
                    return _item.LabelName;
                }
            }

        }



        public CNCItem QueryByLabel(string Label)
        {
            string _label = Label.Trim();
            return _context.CNCItems.Where(c => c.LabelName == _label).FirstOrDefault();
        }
        public CNCItem QueryByELE_IndexCode(string ELE_IndexCode)
        {
            ELE_IndexCode = ELE_IndexCode.Trim();
            //string ELE_IndexCode = "*EI0000018013*";
            ELE_IndexCode = ELE_IndexCode.Replace("*", "");
            ELE_IndexCode = ELE_IndexCode.Replace("EI", "");
            int ELE_Index = Convert.ToInt32(ELE_IndexCode);
            CNCItem _cncItem = _context.CNCItems.Where(c => c.ELE_INDEX == ELE_Index).FirstOrDefault();
            return _cncItem;
        }

        public void SetPosition(string Label, string Position)
        {
            CNCItem _item = QueryByLabel(Label);
            _item.PartPosition = Position;
            _item.Finished = true;
            _context.SaveChanges();
        }

        public void SetFinish(int CNCItemID)
        {
            CNCItem _item = QueryByID(CNCItemID);
            _item.Finished = true;
            _context.SaveChanges();
        }


        public void SetReady(int CNCItemID)
        {
            CNCItem _item = QueryByID(CNCItemID);
            _item.Ready = true;
            _context.SaveChanges();
        }


        public void SetRequired(int CNCItemID)
        {
            CNCItem _item = QueryByID(CNCItemID);
            _item.Required = true;
            _context.SaveChanges();
        }

        public void SetUnRequired(int CNCItemID)
        {
            CNCItem _item = QueryByID(CNCItemID);
            _item.Required = false;
            _context.SaveChanges();
        }



    }
}
