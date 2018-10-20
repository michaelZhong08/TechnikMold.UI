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
using TechnikSys.MoldManager.Domain.Abstract;
using TechnikSys.MoldManager.Domain.Entity;
using TechnikSys.MoldManager.Domain.Output;

namespace TechnikSys.MoldManager.Domain.Concrete
{
    public class PartRepository : IPartRepository
    {
        private EFDbContext _context = new EFDbContext();
        public IQueryable<Part> Parts
        {
            get
            {
                return _context.Parts;
            }
        }

        public int Save(Part Part)
        {
            Part _dbEntry;
            bool _newpart = false;
            if (Part.PartID == 0)
            {
                _dbEntry = QueryByName(Part.PartNumber);
                if (_dbEntry == null)
                {
                    _newpart = true;
                    _context.Parts.Add(Part);
                }
                else
                {
                    if ((_dbEntry.FromUG == Part.FromUG) || (_dbEntry.FromUG == true))
                    {
                        #region value
                        _dbEntry.ProjectID = Part.ProjectID;
                        _dbEntry.ShortName = Part.ShortName;
                        _dbEntry.Name = Part.Name;
                        _dbEntry.PartNumber = Part.PartNumber;
                        _dbEntry.Specification = Part.Specification;
                        _dbEntry.MaterialID = Part.MaterialID;
                        _dbEntry.MaterialName = Part.MaterialName;
                        _dbEntry.Hardness = Part.Hardness;
                        _dbEntry.BrandID = Part.BrandID;
                        _dbEntry.BrandName = Part.BrandName == null ? "" : Part.BrandName;
                        _dbEntry.SupplierName = Part.SupplierName == null ? "" : Part.SupplierName;
                        _dbEntry.SupplierID = Part.SupplierID;
                        _dbEntry.DetailDrawing = Part.DetailDrawing;
                        _dbEntry.BriefDrawing = Part.BriefDrawing;
                        _dbEntry.PurchaseDrawing = Part.PurchaseDrawing;
                        _dbEntry.ExtraMaching = Part.ExtraMaching;
                        _dbEntry.Memo = Part.Memo;
                        _dbEntry.CreateDate = Part.CreateDate;
                        _dbEntry.ReleaseDate = Part.ReleaseDate;
                        _dbEntry.Quantity = Part.Quantity;
                        _dbEntry.Version = Part.Version;
                        _dbEntry.Enabled = Part.Enabled;
                        _dbEntry.ModelPath = Part.ModelPath == null ? "" : Part.ModelPath;
                        _dbEntry.DrawingPath = Part.DrawingPath == null ? "" : Part.DrawingPath;
                        _dbEntry.CatalogSpec = Part.CatalogSpec == null ? "" : Part.CatalogSpec;
                        _dbEntry.Status = Part.Status;
                        _dbEntry.JobNo = Part.JobNo == null ? "" : Part.JobNo;
                        _dbEntry.AppendQty = Part.AppendQty;
                        _dbEntry.FromUG = Part.FromUG;
                        _dbEntry.InPurchase = Part.InPurchase;
                        _dbEntry.CreateDate = Part.CreateDate;
                        #endregion
                    }
                    else
                    {
                        return -1;
                    }
                }

            }
            else
            {
                _dbEntry = _context.Parts.Find(Part.PartID);
                if (_dbEntry != null)
                {
                    #region 锁定状态的零件关键信息不可变更 michael                   
                    if (_dbEntry.Locked == true)
                    {
                        _dbEntry.ProjectID = Part.ProjectID;
                        _dbEntry.ShortName = Part.ShortName;
                        //_dbEntry.Name = Part.Name;
                        //_dbEntry.PartNumber = Part.PartNumber;
                        //_dbEntry.Specification = Part.Specification;
                        //_dbEntry.MaterialID = Part.MaterialID;
                        //_dbEntry.MaterialName = Part.MaterialName;
                        //_dbEntry.Hardness = Part.Hardness;
                        _dbEntry.BrandID = Part.BrandID;
                        _dbEntry.BrandName = Part.BrandName;
                        _dbEntry.SupplierName = Part.SupplierName;
                        _dbEntry.SupplierID = Part.SupplierID;
                        _dbEntry.DetailDrawing = Part.DetailDrawing;
                        _dbEntry.BriefDrawing = Part.BriefDrawing;
                        _dbEntry.PurchaseDrawing = Part.PurchaseDrawing;
                        _dbEntry.ExtraMaching = Part.ExtraMaching;
                        _dbEntry.Memo = Part.Memo;
                        //_dbEntry.CreateDate = Part.CreateDate;
                        //_dbEntry.ReleaseDate = Part.ReleaseDate;
                        //_dbEntry.Quantity = Part.Quantity;
                        _dbEntry.Version = Part.Version;
                        //_dbEntry.Enabled = Part.Enabled;
                        _dbEntry.ModelPath = Part.ModelPath;
                        _dbEntry.DrawingPath = Part.DrawingPath;
                        _dbEntry.CatalogSpec = Part.CatalogSpec;
                        //_dbEntry.Status = Part.Status;
                        _dbEntry.JobNo = Part.JobNo;
                        _dbEntry.AppendQty = Part.AppendQty;
                        //_dbEntry.FromUG = Part.FromUG;
                        //_dbEntry.InPurchase = Part.InPurchase;
                        //_dbEntry.CreateDate = Part.CreateDate;
                    }
                    else
                    {
                        _dbEntry.ProjectID = Part.ProjectID;
                        _dbEntry.ShortName = Part.ShortName;
                        _dbEntry.Name = Part.Name;
                        _dbEntry.PartNumber = Part.PartNumber;
                        _dbEntry.Specification = Part.Specification;
                        _dbEntry.MaterialID = Part.MaterialID;
                        _dbEntry.MaterialName = Part.MaterialName;
                        _dbEntry.Hardness = Part.Hardness;
                        _dbEntry.BrandID = Part.BrandID;
                        _dbEntry.BrandName = Part.BrandName;
                        _dbEntry.SupplierName = Part.SupplierName;
                        _dbEntry.SupplierID = Part.SupplierID;
                        _dbEntry.DetailDrawing = Part.DetailDrawing;
                        _dbEntry.BriefDrawing = Part.BriefDrawing;
                        _dbEntry.PurchaseDrawing = Part.PurchaseDrawing;
                        _dbEntry.ExtraMaching = Part.ExtraMaching;
                        _dbEntry.Memo = Part.Memo;
                        //_dbEntry.CreateDate = Part.CreateDate;
                        _dbEntry.ReleaseDate = Part.ReleaseDate;
                        _dbEntry.Quantity = Part.Quantity;
                        _dbEntry.Version = Part.Version;
                        _dbEntry.Enabled = Part.Enabled;
                        _dbEntry.ModelPath = Part.ModelPath;
                        _dbEntry.DrawingPath = Part.DrawingPath;
                        _dbEntry.CatalogSpec = Part.CatalogSpec;
                        //_dbEntry.Status = Part.Status;
                        _dbEntry.JobNo = Part.JobNo;
                        _dbEntry.AppendQty = Part.AppendQty;
                        //_dbEntry.FromUG = Part.FromUG;
                        //_dbEntry.InPurchase = Part.InPurchase;
                        //_dbEntry.CreateDate = Part.CreateDate;
                    }
                    #endregion
                }
            }
            _context.SaveChanges();

            if (_newpart)
            {
                return Part.PartID;
            }
            else
            {
                return _dbEntry.PartID;
            }
        }

        public Part QueryByName(string Name)
        {
            Part _part = _context.Parts.Where(p => p.Name.ToLower().Contains(Name.ToLower())).FirstOrDefault();
            return _part;
        }

        public IQueryable<Part> Query(string Keyword)
        {
            throw new NotImplementedException();
        }

        public int Delete(int PartID)
        {
            Part _dbEntry = _context.Parts.Find(PartID);
            _dbEntry.Enabled = false;
            _context.SaveChanges();
            return _dbEntry.PartID;
        }


        public Part QueryByID(int PartID)
        {
            Part _part = _context.Parts.Find(PartID);
            return _part;
        }


        public void DeleteExisting(string MoldNumber)
        {
            IEnumerable<Part> _parts = _context.Parts.Where(p => p.PartNumber.Contains(MoldNumber)).Where(p => p.FromUG == true);
            foreach (Part _part in _parts)
            {
                _part.Enabled = false;
            }
            _context.SaveChanges();
        }


        public IEnumerable<Part> QueryBySpecification(string Keyword)
        {
            return _context.Parts.Where(p => p.Specification.Contains(Keyword)).Take(10);
        }
        #region added by felix 版本管控
        /// <summary>
        /// 根据名称（包含版本号）获取同一个零件的多个版本的part
        /// </summary>
        /// <param name="Name"></param>
        /// <returns></returns>
        public IQueryable<Part> GetPartsByName(string Name)
        {
            IQueryable<Part> _part = _context.Parts
                .Where(p => p.Name.ToLower().Contains(Name.ToLower().Substring(0, Name.Length - 2)))
                ;
            return _part;
        }

        /// <summary>
        /// 根据名称（包含版本号）获取Part对象
        /// </summary>
        /// <param name="Name"></param>
        /// <returns></returns>
        public IQueryable<Part> GetPartsByName(string Name, int projectid = 0)
        {
            IQueryable<Part> _part = _context.Parts
              .Where(p => p.Name.ToLower().Contains(Name.ToLower())) //.Substring(0, Name.Length - 2))
              //.Where(p => p.ProjectID == projectid)
              ;
            return _part;
        }
        /// <summary>
        /// 获取 
        /// </summary>
        /// <param name="Name"></param>
        /// <returns></returns>
        public IQueryable<Project> GetProjectsByNameIsPublish(string Name)
        {
            IQueryable<Part> _part = _context.Parts
                .Where(p => p.Name.ToLower().Contains(Name));

            IQueryable<Project> _proj = _context.Projects.Where(prj => prj.IsPublish == true)
                .Where(prj => prj.MoldNumber == Name.Substring(0, Name.IndexOf("_")))
                .Where(prj => _part.Where(p => p.ProjectID == prj.ProjectID).Count() > 0);
            return _proj;
        }

        /// <summary>
        /// 根据MoldNumber 获取最新版本的零件清单
        /// </summary>
        /// <param name="MoldNumber"></param>
        /// <returns></returns>
        public IEnumerable<Part> QueryByMoldNumber(string MoldNumber)
        {
            var result = _context.Parts.
                Where(p => p.PartNumber.Contains(MoldNumber))
                .Where(p => p.Enabled == true)
               .Select(o => new PartGroupVer
               {
                   ProjectID = o.ProjectID,
                   Name = o.Name.Substring(0, o.Name.Length - 2),
                   Version = o.Version
               });
            var rs = result.GroupBy(o => new { o.ProjectID, o.Name }).Select(g => new
            {
                ProjectID = g.Key.ProjectID,
                Name = g.Key.Name,
                Version = g.Max(x => x.Version)
            });
            var l = rs.Select(g => new { Name = g.Name + g.Version });

            IQueryable<Part> _parts = _context.Parts
            .Where(p => l.Where(x => p.Name == x.Name).Count() > 0)
            .Where(p => p.Enabled == true);
            //IEnumerable<Part> _parts = _context.Parts
            //    .Where(p => p.PartNumber.Contains(MoldNumber))
            //    .Where(p => p.Enabled == true);
            return _parts;
        }

        /// <summary>
        /// 根据projectid获取最新版本的零件清单
        /// </summary>
        /// <param name="ProjectID"></param>
        /// <returns></returns>
        public IQueryable<Part> QueryByProject(int ProjectID)
        {
            var result = _context.Parts.Where(p => p.ProjectID == ProjectID)
                .Select(o => new PartGroupVer
                {
                    ProjectID = o.ProjectID,
                    Name = o.Name.Substring(0, o.Name.Length - 2),
                    Version = o.Version
                });
            var rs = result.GroupBy(o => new { o.ProjectID, o.Name }).Select(g => new
            {
                ProjectID = g.Key.ProjectID,
                Name = g.Key.Name,
                Version = g.Max(x => x.Version)
            });
            var l = rs.Select(g => new { Name = g.Name + g.Version, ProjectID = ProjectID });

            IQueryable<Part> _parts = _context.Parts
            .Where(p => l.Where(x => p.Name == x.Name).Count() > 0)
            .Where(p => l.Where(x => p.ProjectID == x.ProjectID).Count() > 0)
            .Where(p => p.Enabled == true);
            //IQueryable<Part> _parts = _context.Parts
            //    .Where(p => p.ProjectID == ProjectID)
            //    .Where(p => p.Enabled == true);
            return _parts;
        }

        public int SaveNew(Part Part)
        {
            Part _dbEntry = null;
            bool _newpart = false;
            if (Part.PartID == 0)
            {
                //新增
                _newpart = true;
                Part.InPurchase = false;//web新增、UG升级
                //如果name 存在则修改，不存在则新增
                _dbEntry = QueryByNameVer(Part.Name, Part.PartListID);
                if (_dbEntry != null)
                {
                    //存在，修改 
                    #region   修改
                    _dbEntry.ProjectID = Part.ProjectID;
                    _dbEntry.ShortName = Part.ShortName;
                    _dbEntry.Name = Part.Name;
                    _dbEntry.PartNumber = Part.PartNumber;
                    _dbEntry.Specification = Part.Specification;
                    _dbEntry.MaterialID = Part.MaterialID;
                    _dbEntry.MaterialName = Part.MaterialName;
                    _dbEntry.Hardness = Part.Hardness;
                    _dbEntry.BrandID = Part.BrandID;
                    _dbEntry.BrandName = Part.BrandName;
                    _dbEntry.SupplierName = Part.SupplierName;
                    _dbEntry.SupplierID = Part.SupplierID;
                    _dbEntry.DetailDrawing = Part.DetailDrawing;
                    _dbEntry.BriefDrawing = Part.BriefDrawing;
                    _dbEntry.PurchaseDrawing = Part.PurchaseDrawing;
                    _dbEntry.ExtraMaching = Part.ExtraMaching;
                    _dbEntry.Memo = Part.Memo;
                    _dbEntry.CreateDate = Part.CreateDate;
                    _dbEntry.ReleaseDate = Part.ReleaseDate;
                    _dbEntry.Quantity = Part.Quantity;
                    _dbEntry.Version = Part.Version;
                    _dbEntry.Enabled = Part.Enabled;
                    _dbEntry.ModelPath = Part.ModelPath;
                    _dbEntry.DrawingPath = Part.DrawingPath;
                    _dbEntry.CatalogSpec = Part.CatalogSpec;
                    _dbEntry.Status = Part.Status;
                    _dbEntry.JobNo = Part.JobNo;
                    _dbEntry.AppendQty = Part.AppendQty;
                    _dbEntry.FromUG = Part.FromUG;
                    _dbEntry.PartListID = Part.PartListID;
                    _dbEntry.Latest = Part.Latest;
                    _dbEntry.Quantity = Part.Quantity;
                    _dbEntry.TotalQty = Part.TotalQty;
                    #endregion
                }
                else
                {
                    //不存在，新增
                    Part.Version = Part.Version != "" ? Part.Version : "00";
                    //不论来自UG或者来自导入或者Web手工端  只要是新创建零件Status都是1 开放状态
                    Part.CreateDate = DateTime.Now;
                    Part.Status = 1;
                    _context.Parts.Add(Part);
                }
            }
            else
            {
                //web端
                _dbEntry = _context.Parts.Find(Part.PartID);
                if (_dbEntry != null && !_dbEntry.Locked)
                {
                    PartList _partlist = _context.PartLists.Where(p => p.PartListID == _dbEntry.PartListID).FirstOrDefault() ?? new PartList();
                    
                    if (_dbEntry.Status < 1)
                    {
                        //不可修改关键字
                        #region   修改
                        _dbEntry.ProjectID = 0;
                        _dbEntry.ShortName = Part.ShortName;
                        _dbEntry.Name = Part.Name ?? _partlist.MoldNumber + "_" + Part.ShortName + "_" + Part.JobNo + "_V" + Part.Version;
                        _dbEntry.PartNumber = Part.PartNumber ?? _partlist.MoldNumber + "-" + Part.JobNo;
                        _dbEntry.Hardness = Part.Hardness;
                        _dbEntry.DetailDrawing = Part.DetailDrawing;
                        _dbEntry.BriefDrawing = Part.BriefDrawing;
                        _dbEntry.ExtraMaching = Part.ExtraMaching;
                        _dbEntry.Memo = Part.Memo;
                        _dbEntry.Version = Part.Version;
                        _dbEntry.Enabled = Part.Enabled;
                        _dbEntry.ModelPath = Part.ModelPath;
                        _dbEntry.DrawingPath = Part.DrawingPath;
                        _dbEntry.CatalogSpec = Part.CatalogSpec;
                        _dbEntry.JobNo = Part.JobNo;
                        _dbEntry.FromUG = Part.FromUG;
                        _dbEntry.InPurchase = Part.InPurchase;
                        #endregion
                    }
                    else
                    {
                        #region   修改
                        _dbEntry.ProjectID = 0;
                        _dbEntry.ShortName = Part.ShortName;
                        _dbEntry.Name = Part.Name ?? _partlist.MoldNumber + "_" + Part.ShortName + "_" + Part.JobNo + "_V" + Part.Version;
                        _dbEntry.PartNumber = Part.PartNumber ?? _partlist.MoldNumber + "-" + Part.JobNo;
                        _dbEntry.Specification = Part.Specification;
                        _dbEntry.MaterialID = Part.MaterialID;
                        _dbEntry.MaterialName = Part.MaterialName;
                        _dbEntry.Hardness = Part.Hardness;
                        _dbEntry.BrandID = Part.BrandID;
                        _dbEntry.BrandName = Part.BrandName;
                        _dbEntry.SupplierName = Part.SupplierName;
                        _dbEntry.SupplierID = Part.SupplierID;
                        _dbEntry.DetailDrawing = Part.DetailDrawing;
                        _dbEntry.BriefDrawing = Part.BriefDrawing;
                        _dbEntry.PurchaseDrawing = Part.PurchaseDrawing;
                        _dbEntry.ExtraMaching = Part.ExtraMaching;
                        _dbEntry.Memo = Part.Memo;
                        _dbEntry.Version = Part.Version;
                        _dbEntry.Enabled = Part.Enabled;
                        _dbEntry.ModelPath = Part.ModelPath;
                        _dbEntry.DrawingPath = Part.DrawingPath;
                        _dbEntry.CatalogSpec = Part.CatalogSpec;
                        _dbEntry.JobNo = Part.JobNo;
                        _dbEntry.AppendQty = Part.AppendQty;
                        _dbEntry.Quantity = Part.Quantity;
                        _dbEntry.TotalQty = Part.AppendQty + Part.Quantity;
                        _dbEntry.FromUG = Part.FromUG;
                        _dbEntry.InPurchase = Part.InPurchase;
                        #endregion
                    }
                }
                else return -1;
            }
            _context.SaveChanges();
            if (_newpart)
            {
                return Part.PartID;
            }
            else
            {
                return _dbEntry.PartID;
            }
        }


        //upgrade
        public int SaveUpgrade(Part Part)
        {

            Part.InPurchase = false;//web升级
            Part.ERPPartID = "";
            _context.Parts.Add(Part);

            _context.SaveChanges();
            return Part.PartID;
        }
        public Part QueryByNameVer(string Name)
        {
            Part _part = _context.Parts
                .Where(p => p.Name.ToLower() == Name.ToLower())
                .FirstOrDefault();
            return _part;
        }
        public Part QueryByNameVer(string Name, int PartlistID = 0)//int ProjectID=0
        {
            Part _part = _context.Parts
                .Where(p => p.Name.ToLower() == Name.ToLower())
                .Where(p => p.PartListID == PartlistID)
                //.Where(p => p.ProjectID == ProjectID)
                .FirstOrDefault();
            return _part;
        }
        /// <summary>
        /// 获取某project的所有版本清单
        /// </summary>
        /// <param name="MoldNumber"></param>
        /// <returns></returns>
        public List<MoldVersionInfo> GetMoldVerList(string MoldNumber)
        {
            List<PartList> _partlistList = _context.PartLists.Where(p => p.MoldNumber == MoldNumber).ToList().OrderByDescending(p => p.Version).ToList();
            List<MoldVersionInfo> list = new List<MoldVersionInfo>();
            if (_partlistList.Count > 0)
            {
                foreach (var partlist in _partlistList)
                {
                    MoldVersionInfo mvi = new MoldVersionInfo();
                    if (partlist.Released)
                    {
                        mvi.IsEdit = false;//发布的，不能编辑
                    }
                    else
                        mvi.IsEdit = true;//未发布，可编辑
                    mvi.ProjectID = partlist.ProjectID;
                    mvi.PartListID = partlist.PartListID;
                    mvi.Version = partlist.Version;
                    mvi.MoldNumber = partlist.MoldNumber;
                    mvi.CreateDate = partlist.CreateDate == null ? new DateTime(1900, 1, 1) : partlist.CreateDate;
                    mvi.ReleaseDate = partlist.ReleaseDate == null ? new DateTime(1900, 1, 1) : partlist.ReleaseDate;
                    list.Add(mvi);
                }
            }
            else
            {
                MoldVersionInfo mvi = new MoldVersionInfo();
                mvi.IsEdit = true;//未发布，可编辑
                mvi.ProjectID = 0;
                mvi.PartListID = 0;
                mvi.Version = 0;
                mvi.MoldNumber = MoldNumber;
                list.Add(mvi);
            }
            return list;
        }
        #endregion
        /// <summary>
        /// 获取最高版本Partlist中的Part
        /// </summary>
        /// <returns></returns>
        public IQueryable<Part> GetLatestVerParts()
        {
            var _parts = from p1 in _context.Parts
                          join p2 in _context.PartLists on p1.PartListID equals p2.PartListID
                          where p1.Enabled == true && p2.Latest == true
                          select p1;
            return _parts;
        }
    }
    public class PartGroupVer
    {
        /// <summary>
        /// 模具ID
        /// </summary>
        public int ProjectID { get; set; }
        /// <summary>
        /// 零件名称（含版本）
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// 版本号
        /// </summary>
        public string Version { get; set; }
    }
}
