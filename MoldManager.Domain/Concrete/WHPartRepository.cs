using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TechnikSys.MoldManager.Domain.Abstract;
using TechnikSys.MoldManager.Domain.Entity;

namespace TechnikSys.MoldManager.Domain.Concrete
{
    public class WHPartRepository: IWHPartRepository
    {
        public EFDbContext _context = new EFDbContext();
        public IQueryable<WHPart> WHParts { get { return _context.WHParts; } }
        public int Save(WHPart model)
        {
            WHPart dbEntry = _context.WHParts.Where(p => p.PartNum == model.PartNum).FirstOrDefault();
            if (dbEntry == null)
            {
                model.Enable = true;
                _context.WHParts.Add(model);
            }
            else
            {
                dbEntry.PartNum = model.PartNum;
                dbEntry.PartName = model.PartName;
                dbEntry.Specification = model.Specification;
                dbEntry.SafeQuantity = model.SafeQuantity;
                dbEntry.Materials = model.Materials;
                dbEntry.StockTypes = model.StockTypes;
                dbEntry.PurchaseType = model.PurchaseType;
                dbEntry.MoldNumber = model.MoldNumber;
                dbEntry.Enable = model.Enable;
                dbEntry.PlanQty = model.PlanQty;
            }
            _context.SaveChanges();
            return 1;
        }
        public int Delete(WHPart model)
        {
            WHPart _part = _context.WHParts.Where(p => p.PartNum == model.PartNum && p.Enable==true).FirstOrDefault();
            if (_part != null)
            {
                _part.Enable = false;
                _context.SaveChanges();
                return 1;
            }
            return -99;
        }
        public WHPart GetPart(string _partNum,int PartID=0)
        {
            WHPart _part = _context.WHParts.Where(p => p.PartNum == _partNum && p.PartID== PartID && p.Enable == true).FirstOrDefault();
            return _part;
        }
        /// <summary>
        /// 获取零件号
        /// </summary>
        /// <param name="_stockType">备库/耗材 类型 83G...</param>
        /// <returns></returns>
        public string GetPartNum(string _stockType)
        {
            var _sObj = _context.Sequences.Where(s => s.Name == "WHPart").FirstOrDefault();
            if (_sObj != null)
            {
                string _fLetter = "";//_sObj.NameConvension.Substring(0,1);//Z
                //int length = _sObj.NameConvension.Length - 1;
                string _ZeroList = _sObj.NameConvension.Substring(0, _sObj.NameConvension.Length);//0000
                int num = _context.WHParts.Where(p => p.PartNum.Contains( _stockType)).Count()+1;//序列号
                string _strNum = string.Format("{0:"+ _ZeroList + "}", num);//0001
                return _stockType + "-" + _fLetter + _strNum;//xxxxxx-Z0001
            }
            return null;
        }
        public List<WHPart> GetPartsByMold(string _mold)
        {
            List<WHPart> _parts = _context.WHParts.Where(p => p.MoldNumber == _mold).ToList();
            return _parts;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="ParentType">模具耗材备库/生产耗材</param>
        /// <returns></returns>
        public List<WHPart> GetPartsByType(string ParentType)
        {
            //IQueryable<WHPart> _whparts = from _parts in _context.WHParts
            //                              join _type in _context.StockTypes on Convert.ToInt32(_parts.StockTypes) equals _type.StockTypeID
            //                              where _type.Parent == ParentType
            //                              select _parts;
            List<int> _stypes = _context.StockTypes.Where(t => t.Parent == ParentType).Select(t=>t.StockTypeID).ToList();
            List<WHPart> _whparts = _context.WHParts.ToList().Where(p => _stypes.Contains(Convert.ToInt32(p.StockTypes))).ToList();
            return _whparts;
        }
        /// <summary>
        /// 根据任务获取该任务 外发零件 PartName唯一
        /// </summary>
        /// <param name="_taskID"></param>
        /// <returns></returns>
        public WHPart GetwfTaskPart(int _taskID)
        {
            Entity.Task _task = _context.Tasks.Where(t => t.TaskID == _taskID).FirstOrDefault();
            string _wfPartname = _task.TaskName + "_" + _task.ProcessName + "(V" + string.Format("{0:00}", _task.Version) + ")";
            WHPart _whpart = _context.WHParts.Where(p => p.PartName == _wfPartname && p.Enable == true).FirstOrDefault();
            if (_whpart != null)
            {
                return _whpart;
            }
            return null;
        }
        /// <summary>
        /// 外发任务 物料编号 生成外发采购物料时引用
        /// </summary>
        /// <param name="_taskID"></param>
        /// <returns></returns>
        public string GetwfTaskPartNum(int _taskID)
        {
            WHPart _wfwhPart = GetwfTaskPart(_taskID);
            Entity.Task _task = _context.Tasks.Where(t => t.TaskID == _taskID).FirstOrDefault();
            var _sObj = _context.Sequences.Where(s => s.Name == "WFPartNum").FirstOrDefault();
            if (_sObj != null)
            {
                if (_wfwhPart != null)
                {
                    return _wfwhPart.PartNum;
                }
                else
                {
                    List<WHPart> _wfparts = GetwfWHPartByMoldNum(_task.MoldNumber).OrderBy(p=>p.CreDate).ToList();
                    int num;
                    if (_wfparts.Count>0)
                    {
                        string _partNums = _wfparts.Select(p => p.PartNum).Max();
                        var _partNumArry = _partNums.Split('-');
                        num = Convert.ToInt32(_partNumArry[1].Substring(1, _partNumArry[1].Length - 1))+1;
                    }
                    else
                    {
                        num = 1;
                    }
                    string _fLetter = _sObj.NameConvension.Substring(0, 1);//Z
                    string _ZeroList = _sObj.NameConvension.Substring(1, _sObj.NameConvension.Length - 1);//000
                    string _strNum = string.Format("{0:" + _ZeroList + "}", num);//001
                    return _task.MoldNumber + "-" + _fLetter + _strNum;//xxxxxx-Z001
                }
            }
            return null;
        }
        public string GetwfTaskPartNum(string MooldNum)
        {
            //WHPart _wfwhPart = GetwfTaskPart(_taskID);
            //Entity.Task _task = _context.Tasks.Where(t => t.TaskID == _taskID).FirstOrDefault();
            var _sObj = _context.Sequences.Where(s => s.Name == "WFPartNum").FirstOrDefault();
            if (_sObj != null)
            {
                //if (_wfwhPart != null)
                //{
                //    return _wfwhPart.PartNum;
                //}
                //else
                //{
                    List<WHPart> _wfparts = GetwfWHPartByMoldNum(MooldNum).OrderBy(p => p.CreDate).ToList();
                    int num;
                    if (_wfparts.Count > 0)
                    {
                        string _partNums = _wfparts.Select(p => p.PartNum).Max();
                        var _partNumArry = _partNums.Split('-');
                        num = Convert.ToInt32(_partNumArry[1].Substring(1, _partNumArry[1].Length - 1)) + 1;
                    }
                    else
                    {
                        num = 1;
                    }
                    string _fLetter = _sObj.NameConvension.Substring(0, 1);//Z
                    string _ZeroList = _sObj.NameConvension.Substring(1, _sObj.NameConvension.Length - 1);//000
                    string _strNum = string.Format("{0:" + _ZeroList + "}", num);//001
                    return MooldNum + "-" + _fLetter + _strNum;//xxxxxx-Z001
                //}
            }
            return null;
        }
        public List<WHPart> GetwfWHPartByMoldNum(string _moldNum)
        {
            Sequence _seq = _context.Sequences.Where(s => s.Name == "WFPartNum").FirstOrDefault();
            string _fLetter = _seq.NameConvension.Substring(0, 1);//Z
            List<WHPart> _wfparts = _context.WHParts.Where(p => p.MoldNumber == _moldNum && p.PartNum.Contains(_fLetter) && p.Enable == true).ToList();//&& p.TaskID > 0 
            return _wfparts;
        }
    }
}
