using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TechnikSys.MoldManager.Domain.Abstract;
using TechnikSys.MoldManager.Domain.Entity;

namespace TechnikSys.MoldManager.Domain.Concrete
{
    public class MachinesInfoRepository:IMachinesInfoRepository
    {
        private EFDbContext _context = new EFDbContext();
        public IQueryable<MachinesInfo> MachinesInfo
        {
            get { return _context.MachinesInfo; }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="model"></param>
        /// <returns>-2 设备名存在;-1 设备代码存在;0 成功保存</returns>
        public int Save(MachinesInfo model)
        {
            MachinesInfo dbEntry = _context.MachinesInfo.Where(m => m.MachineCode.ToUpper() == model.MachineCode.ToUpper() && m.IsActive==true).FirstOrDefault();
            //新增
            if (dbEntry==null)
            {
                //验证是否失效Code -99
                if (!IsInvalidMInfo(model.MachineCode))
                {
                    //验证Code -1、Name -2是否都存在
                    if (IsNullMachinesInfo(model) != -1 && IsNullMachinesInfo(model) != -2)
                    {
                        model.IsActive = true;
                        _context.MachinesInfo.Add(model);
                    }
                    else
                        return IsNullMachinesInfo(model);
                }
                else
                {
                    return -99;
                }
            }
            //编辑
            else
            {
                dbEntry.MachineName = model.MachineName;
                dbEntry.DepartmentID = model.DepartmentID;
                dbEntry.Capacity = model.Capacity;
                dbEntry.Downtime = model.Downtime;
                dbEntry.EquipBrand = model.EquipBrand;
                dbEntry.Cost = model.Cost;
                dbEntry.IsActive = model.IsActive;
                dbEntry.Status = model.Status;
                dbEntry.TaskType = model.TaskType;
                dbEntry.Stype = model.Stype;
            }
            _context.SaveChanges();
            return 0;
        }
        public MachinesInfo GetMInfoByCode(string MachineCode)
        {
            MachinesInfo dbEntry = _context.MachinesInfo.Where(m => m.MachineCode == MachineCode).FirstOrDefault();
            if (dbEntry != null)
                return dbEntry;
            return null;
        }
        public MachinesInfo GetMInfoByKeyWord(string KeyWord)
        {
            MachinesInfo dbEntry;
            dbEntry = _context.MachinesInfo.Where(m => m.MachineCode.ToUpper().Contains(KeyWord.ToUpper())).FirstOrDefault();
            if (dbEntry != null)
                return dbEntry;
            dbEntry = _context.MachinesInfo.Where(m => m.MachineName.ToUpper().Contains(KeyWord.ToUpper())).FirstOrDefault();
            if (dbEntry != null)
                return dbEntry;
            return null;
        }
        /// <summary>
        /// 检查设备Code、Name
        /// </summary>
        /// <param name="model">设备对象</param>
        /// <returns>res="" 全新设备</returns>
        public string CheckExistMachinesInfo(MachinesInfo model)
        {
            string res = "";
            MachinesInfo dbEntry;
            dbEntry = _context.MachinesInfo.Where(m => m.MachineCode.ToUpper() == model.MachineCode.ToUpper()).FirstOrDefault();
            if (dbEntry != null)
            {
                res = res + dbEntry.MachineCode + ";";
            }
            dbEntry = _context.MachinesInfo.Where(m => m.MachineName.ToUpper() == model.MachineName.ToUpper()).FirstOrDefault();
            if (dbEntry != null)
            {
                res = res + dbEntry.MachineName + ";";
            }
            return res;
        }
        public int IsNullMachinesInfo(MachinesInfo model)
        {
            //设备代码
            if (string.IsNullOrEmpty(model.MachineCode))
                return -1;
            //设备名称
            if (string.IsNullOrEmpty(model.MachineName))
                return -2;
            ////品牌
            //if (string.IsNullOrEmpty(model.EquipBrand))
            //    return -3;
            ////部门
            //if (model.DepartmentID==0)
            //    return -4;
            //工艺类型
            if (string.IsNullOrEmpty(model.TaskType))
                return -5;
            return 0;
        }
        public bool IsInvalidMInfo(string MCode)
        {
            MachinesInfo dbEntry = _context.MachinesInfo.Where(m => m.MachineCode.ToUpper() == MCode.ToUpper() && m.IsActive == false).FirstOrDefault();
            if (dbEntry != null)
                return true;
            return false;
        }
        public string GenerateCode(string TaskType, string _FirstLetter="")
        {
            string _Code = "";
            if (string.IsNullOrEmpty(_FirstLetter))
            {
                if (TaskType.Contains("1") || TaskType.Contains("4"))
                    _FirstLetter = "C";
                else if (TaskType.Contains("2"))
                    _FirstLetter = "E";
                else if (TaskType.Contains("3"))
                    _FirstLetter = "W";
                else if (TaskType.Contains("6"))
                    _FirstLetter = "G";
                else if (TaskType.Contains("100"))
                    _FirstLetter = "Q";
                else
                    _FirstLetter = "Oth";
            }            
            int _maxNum=0;
            int _curNum=0;
             MachinesInfo dbEntry= _context.MachinesInfo.Where(m=>m.MachineCode.Contains(_FirstLetter)).OrderByDescending(m => m.MachineCode).FirstOrDefault();
            try
            {
                if (dbEntry != null)
                    _maxNum = Convert.ToInt32(dbEntry.MachineCode.Substring(1));
            }
            catch
            {
                _maxNum = 100;
            }
            _curNum = _maxNum + 1;
            if (_curNum < 10)
                _Code = _FirstLetter + "0" + _curNum.ToString();
            else
                _Code = _FirstLetter + _curNum.ToString();
            return _Code;
        }
        public List<MachinesInfo> GetMInfoByTaskType(int TaskType)
        {
            var _mInfos = (_context.MachinesInfo).ToList().Where(m => m.TaskType.Split(',').Contains(TaskType.ToString())).ToList();
            return _mInfos;
        }
    }
}
