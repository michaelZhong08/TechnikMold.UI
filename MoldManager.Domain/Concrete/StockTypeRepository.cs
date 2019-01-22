using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TechnikSys.MoldManager.Domain.Entity;
using TechnikSys.MoldManager.Domain.Abstract;

namespace TechnikSys.MoldManager.Domain.Concrete
{
    public class StockTypeRepository:IStockTypeRepository
    {
        private EFDbContext _context = new EFDbContext();

        public IEnumerable<StockType> StockTypes
        {
            get { return _context.StockTypes; }
        }

        public StockType QueryByID(int StockTypeID)
        {
            return _context.StockTypes.Find(StockTypeID);
        }


        public StockType QueryByName(string Name)
        {
            return _context.StockTypes.Where(s => s.Name == Name).Where(s=>s.Enabled==true).FirstOrDefault();
        }


        public int Save(StockType _model)
        {
            StockType _stockType = _context.StockTypes.Where(s => s.StockTypeID == _model.StockTypeID).FirstOrDefault();
            if (_stockType == null)
            {
                StockType _stockType1 = _context.StockTypes.Where(s => (s.Code == _model.Code || s.Name == _model. Name) && s.Parent== _model.Parent).FirstOrDefault();
                if (_stockType1 != null)
                {
                    _model.Enabled = true;
                }
                else
                {
                    _context.StockTypes.Add(_model);
                }
            }
            else
            {
                _stockType.Name = _model.Name;
                _stockType.Code = _model.Code ?? _stockType.Code;
                _stockType.Parent = _model.Parent?? _stockType.Parent;
                _stockType.PurchaseType = _model.PurchaseType ?? _stockType.PurchaseType;
                _stockType.Enabled = _model.Enabled;
            }            
            _context.SaveChanges();
            return _model.StockTypeID;
        }


        public void Delete(int StockTypeID)
        {
            StockType _stockType = QueryByID(StockTypeID);
            if (_stockType != null)
            {
                _stockType.Enabled = false;
            }
            _context.SaveChanges();
        }
        /// <summary>
        /// 获取耗材/备库类型列表
        /// </summary>
        /// <param name="Parent">生产耗材 / 模具耗材备库</param>
        /// <returns></returns>
        public List<StockType> GetTypeList(string Parent)
        {
            List<StockType> _stypes = _context.StockTypes.Where(s => s.Parent == Parent && s.Enabled == true && (s.Code!="" && s.Code!=null)).ToList();
            return _stypes;
        }
    }
}
