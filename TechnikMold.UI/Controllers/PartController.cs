using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TechnikSys.MoldManager.Domain.Abstract;
using TechnikSys.MoldManager.Domain.Entity;
using TechnikSys.MoldManager.Domain.Output;
using MoldManager.WebUI.Models.EditModel;
using MoldManager.WebUI.Models.GridRowModel;
using MoldManager.WebUI.Models.GridViewModel;
using MoldManager.WebUI.Models.ViewModel;
using Newtonsoft.Json;
using System.Reflection;
using System.IO;
using NPOI.XSSF.UserModel;
using NPOI.SS.UserModel;
using TechnikMold.UI.Models;

namespace MoldManager.WebUI.Controllers
{
    public class PartController : Controller
    {
        IPartRepository _partRepository;
        IPartTypeRepository _partTypeRepository;
        IBrandRepository _brandRepository;
        IMaterialRepository _materialRepository;
        IHardnessRepository _hardnessRepository;
        IPartCodeRepository _partCodeRepository;
        IProjectRepository _projectRepository;
        IPartImportRecordRepository _recordRepository;
        IPartStockRepository _partStockRepository;
        IWarehouseStockRepository _warehouseStockRepository;
        ISupplierRepository _supplierRepository;
        IPartListRepository _partListRepository;

        public PartController(IPartRepository PartRepository,
            IPartTypeRepository PartTypeRepository,
            IBrandRepository BrandRepository,
            IMaterialRepository MaterialRepository,
            IHardnessRepository HardnessRepository,
            IPartCodeRepository PartCodeRepository,
            IProjectRepository ProjectRepository,
            IPartImportRecordRepository PartImportRecordRepository,
            IPartStockRepository PartStockRepository,
            IWarehouseStockRepository WarehouseStockRepository,
            ISupplierRepository SupplierRepository,
            IPartListRepository PartListRepository)
        {
            _partRepository = PartRepository;
            _partTypeRepository = PartTypeRepository;
            _brandRepository = BrandRepository;
            _materialRepository = MaterialRepository;
            _hardnessRepository = HardnessRepository;
            _partCodeRepository = PartCodeRepository;
            _projectRepository = ProjectRepository;
            _recordRepository = PartImportRecordRepository;
            _partStockRepository = PartStockRepository;
            _warehouseStockRepository = WarehouseStockRepository;
            _supplierRepository = SupplierRepository;
            _partListRepository = PartListRepository;
        }


        #region PageView
        // GET: Part
        public ActionResult Index(string MoldNumber = "")
        {
            ViewBag.MoldNumber = MoldNumber;
            return View();
        }

        /// <summary>
        /// Save part information(edit/create)
        /// </summary>
        /// <param name="Part"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult Edit(Part Part, string MoldNumber, string BomVersion)
        {
            try
            {
                int partlistID = Convert.ToInt32(BomVersion);
                PartList partlistNow = _partListRepository.PartLists.Where(p => p.PartListID == partlistID).FirstOrDefault() ?? new PartList();
                if (Part.PartID == 0)
                {
                    #region 做正则算法匹配内容
                    Part.Name = MoldNumber + "_" + Part.ShortName + "_" + Part.JobNo + "_" + "V00";
                    Part.PartNumber = MoldNumber + "-" + Part.JobNo;
                    #endregion
                    Part.CreateDate = DateTime.Now;
                    Part.Version = "00";
                    Part.Status = 1;
                    Part.Locked = false;
                    Part.Latest = true;

                    string version = "";
                    if (partlistID != 0)
                    {
                        version = "00" + partlistNow.Version.ToString();
                    }
                    version = version.Substring(version.Count() - 2, 2);
                    Part.Memo = Part.Memo + "  Bom版本 V" + version + " 创建";
                }
                Part.PartListID = partlistNow.PartListID;
                if (Part.MaterialID == 0)
                {
                    Material _material = _materialRepository.GetMaterialByName(Part.MaterialName);
                    if (_material != null)
                    {
                        Part.MaterialID = _material.MaterialID;
                        Part.MaterialName = _material.Name;
                    }
                    else
                    {
                        Part.MaterialID = 0;
                        Part.MaterialName = "";
                    }

                }
                if (Part.Hardness == null)
                {
                    Part.Hardness = "";
                }

                if (Part.SupplierName == "-")
                {
                    Part.SupplierName = "";
                }
                if (Part.Memo == null)
                {
                    Part.Memo = "";
                }
                Project proj = _projectRepository.GetLatestActiveProject(MoldNumber);
                if (proj != null)
                {
                    Part.ProjectID = proj.ProjectID;
                }
                else
                {
                    Part.ProjectID = 0;
                }

                Part.FromUG = false;
                _partRepository.SaveNew(Part);
            }
            catch (Exception ex)
            { }
            return RedirectToAction("Index", "Part", new { MoldNumber = MoldNumber });
        }

        [HttpPost]
        public int PartFromUG(String NewPart)
        {
            Part _part = JsonConvert.DeserializeObject<Part>(NewPart);
            if (_part.PartID == 0)
            {
                _part.CreateDate = DateTime.Now;
                //_part.Version = "00";
            }

            if (_part.MaterialID == 0)
            {
                Material _material = _materialRepository.GetMaterialByName(_part.MaterialName);
                if (_material != null)
                {
                    _part.MaterialID = _material.MaterialID;
                    _part.MaterialName = _material.Name;
                }
            }
            _part.Hardness = _part.Hardness == null ? "" : _part.Hardness;
            _part.BrandName = _part.BrandName == null ? "" : _part.BrandName;
            _part.CatalogSpec = _part.CatalogSpec == null ? "" : _part.CatalogSpec;
            _part.DrawingPath = _part.DrawingPath == null ? "" : _part.DrawingPath;
            _part.ModelPath = _part.ModelPath == null ? "" : _part.ModelPath;
            _part.JobNo = _part.JobNo == null ? "" : _part.JobNo;
            _part.SupplierName = _part.SupplierName == null ? "" : _part.SupplierName;
            string _moldNumber = _part.Name.Substring(0, _part.Name.IndexOf('_'));
            Project _project = _projectRepository.QueryActiveByMoldNumber(_moldNumber);

            _part.ProjectID = _project == null ? 0 : _project.ProjectID;
            try
            {
                int _partID = _partRepository.Save(_part);
                return _partID;
            }
            catch
            {
                return 0;
            }

        }

        [HttpPost]
        public ActionResult PartListFromUG(string NewParts)
        {
            IEnumerable<Part> _parts = JsonConvert.DeserializeObject<IEnumerable<Part>>(NewParts);
            List<int> _partIDs = new List<int>();
            foreach (Part _part in _parts)
            {

            }
            return Json(_parts, JsonRequestBehavior.AllowGet);
        }



        public ActionResult Delete(Part Part)
        {
            _partRepository.Delete(Part.PartID);
            return RedirectToAction("Index", "Part", new { ProjectID = Part.ProjectID });
        }
        /// <summary>
        /// delete parts  michael
        /// </summary>
        /// <param name="PartIDs"></param>
        /// <returns></returns>
        public ActionResult DeleteParts(string PartIDs)
        {
            try
            {
                bool IsUG = false;
                bool IsPurchase = false;
                int p=0;
                string[] _partIDs = PartIDs.Split(',');
                for (int i = 0; i < _partIDs.Length; i++)
                {
                    int PartID = Convert.ToInt32(_partIDs[i]);
                    
                    if (PartID > 0)
                    {
                        IsUG = _partRepository.QueryByID(PartID).FromUG == true ? true : false;
                        IsPurchase = _partRepository.QueryByID(PartID).InPurchase== true ? true : false;
                        p = PartID;
                    }
                    if (IsUG || IsPurchase)
                        break;               
                }
                
                if (!IsUG && !IsPurchase)
                {
                    for (int i = 0; i < _partIDs.Length; i++)
                    {
                        int PartID = Convert.ToInt32(_partIDs[i]);
                        if (PartID > 0)
                        {
                            _partRepository.Delete(PartID);
                        }                           
                    }
                    return Json(new { Code = 1, Message = "零件删除成功！" }, JsonRequestBehavior.AllowGet);
                }
                else
                {
                    Part part = _partRepository.QueryByID(p)??new Part();
                    return Json(new { Code = -1, Message = "零件删除失败——零件["+part.JobNo+"] 来自UG或者已被采购，不允许删除！" }, JsonRequestBehavior.AllowGet);
                }                
            }
            catch(Exception ex)
            {
                return Json(new { Code = -2, Message = ex.Message.ToString() }, JsonRequestBehavior.AllowGet);
            }

            //return RedirectToAction("Index", "Part", new { ProjectID = Part.ProjectID });
        }
        #endregion

        #region Json Data

        /// <summary>
        /// Generate JSON part list for grid view
        /// </summary>
        /// <param name="ProjectID">Primary key of project</param>
        /// <returns></returns>
        public JsonResult JsonParts(string MoldNumber = "")
        {
            //IEnumerable<Part> _parts = _partRepository.QueryByProject(ProjectID);
            PartGridViewModel _parts;
            if (MoldNumber != "")
            {
                _parts = new PartGridViewModel(_partRepository.QueryByMoldNumber(MoldNumber).Where(p => p.Enabled == true), _warehouseStockRepository);
                return Json(_parts, JsonRequestBehavior.AllowGet);
            }
            else
            {
                return null;
            }

        }

        /// <summary>
        /// Generate JSON part data for edit/view
        /// </summary>
        /// <param name="PartID">Primary key of part</param>
        /// <returns></returns>
        public JsonResult JsonPart(int PartID)
        {
            Part _part = _partRepository.QueryByID(PartID);
            return Json(_part, JsonRequestBehavior.AllowGet);
        }

        public JsonResult JsonBrands(string Keyword = "")
        {
            IEnumerable<Brand> _brands = _brandRepository.Brands.Where(b => b.Enabled == true).Where(b => b.Name.Contains(Keyword));
            return Json(_brands, JsonRequestBehavior.AllowGet);
        }

        public int GetBrand(string BrandName)
        {
            int _count = _brandRepository.QueryByName(BrandName).Count();
            return _count;
        }

        public JsonResult JsonSuppliers()
        {
            IEnumerable<Supplier> _suppliers = _supplierRepository.Suppliers.Where(s => s.Enabled == true);
            return Json(_suppliers, JsonRequestBehavior.AllowGet);
        }

        public JsonResult JsonPartTypes()
        {
            IEnumerable<PartType> _partTypes = _partTypeRepository.PartTypes;
            return Json(_partTypes, JsonRequestBehavior.AllowGet);
        }

        public JsonResult JsonMaterials(string MaterialKeyword = "")
        {
            IEnumerable<Material> _materials;
            if (MaterialKeyword != "")
            {
                _materials = _materialRepository.Materials.Where(m => m.Enabled == true)
                    .Where(m => m.Name.ToLower().Contains(MaterialKeyword.ToLower()))
                    .OrderBy(m => m.Name);
            }
            else
            {
                _materials = _materialRepository.Materials.Where(m => m.Enabled == true).OrderBy(m => m.Name);
            }
            return Json(_materials, JsonRequestBehavior.AllowGet);
        }

        public JsonResult JsonMaterial(int MaterialID)
        {
            Material _material = _materialRepository.GetMaterial(MaterialID);
            return Json(_material, JsonRequestBehavior.AllowGet);
        }

        public JsonResult JsonHardness(int MaterialID)
        {
            IEnumerable<Hardness> _hardness = _hardnessRepository.QueryByMaterial(MaterialID);
            return Json(_hardness, JsonRequestBehavior.AllowGet);
        }

        public JsonResult JsonHardnessByName(string MaterialName)
        {
            int MaterialID;

            try
            {
                MaterialID = _materialRepository.GetMaterialByName(MaterialName).MaterialID;
            }
            catch
            {
                MaterialID = 0;
            }

            return JsonHardness(MaterialID);
        }

        public JsonResult JsonPartCodes(string Keyword = "")
        {
            IEnumerable<PartCode> _partCodes;
            if (Keyword != "")
            {
                _partCodes = _partCodeRepository.PartCodes.Where(p => p.Name.ToLower().Contains(Keyword.ToLower()));
            }
            else
            {
                _partCodes = _partCodeRepository.PartCodes;
            }
            return Json(_partCodes, JsonRequestBehavior.AllowGet);
        }

        public JsonResult JsonPartCode(int PartCodeID)
        {
            PartCode _partCode = _partCodeRepository.PartCodes.Where(p => p.PartCodeID == PartCodeID).FirstOrDefault();
            return Json(_partCode, JsonRequestBehavior.AllowGet);
        }

        #endregion


        #region UG Plugin

        /// <summary>
        /// Get ItemNo information by RawNo
        /// </summary>
        /// <param name="RawNo"></param>
        /// <returns></returns>
        public string StockItemNo(string RawNo)
        {
            PartStock _partStock = _partStockRepository.QueryByRawNo(RawNo);
            return _partStock.ItemNO;
        }

        /// <summary>
        /// Generate json part data for UG plugin =>  updated by michal
        /// </summary>
        /// <param name="ProjectID"></param>
        /// <returns></returns>
        public JsonResult JsonUGPart(string MoldNumber, bool FromUG)
        {
            IEnumerable<Part> _parts;
            PartList LastestPL = _partListRepository.PartLists.Where(p => p.MoldNumber == MoldNumber).OrderByDescending(p => p.PartListID).FirstOrDefault();
            Project _project = _projectRepository.QueryByMoldNumber(MoldNumber, 1);
            if (LastestPL != null)
            {
                _parts = _partRepository.Parts.Where(p => p.PartListID == LastestPL.PartListID).Where(p => p.Enabled == true);
            }
            else if (_project != null)
            {
                int _projectID = _projectRepository.QueryByMoldNumber(MoldNumber, 1).ProjectID;
                _parts = _partRepository.Parts.Where(p => p.ProjectID == _projectID).Where(p => p.Enabled == true);

            }
            else
            {
                _parts = _partRepository.Parts.Where(p => p.Name.Contains(MoldNumber));
            }
            _parts = _parts.Where(p => p.FromUG == FromUG);
            return Json(_parts, JsonRequestBehavior.AllowGet);
        }


        public int CheckPartNumberExist(string PartNumber)
        {
            IEnumerable<Part> _parts = _partRepository.Parts
                .Where(p => p.PartNumber == PartNumber)
                .Where(p => p.Version == "0")
                .Where(p => p.Enabled == true);

            return _parts.Count();
        }
        #endregion

        #region UG Interface
        /// <summary>
        /// Receive serialized part data and save to system
        /// </summary>
        /// <param name="Data">Serialized Part Object</param>
        /// <returns>PartID in string</returns>
        [HttpPost]
        public string ImportPart(string Data)
        {
            int _partID;
            Part _part = JsonConvert.DeserializeObject<Part>(Data);
            if (_part.PartID == 0)
            {
                _part.CreateDate = DateTime.Now;
            }
            if (_part.ProjectID == 0)
            {
                int _projectID = GetProjectID(_part.PartNumber);
                if (_projectID != 0)
                {
                    _part.ProjectID = _projectID;
                }
                else
                {
                    _part.ProjectID = 0;
                }
            }

            _partID = _partRepository.Save(_part);
            _recordRepository.Save(Data, _partID);
            return _part.PartID.ToString();
        }

        private int GetProjectID(string PartNumber)
        {

            string[] _data = PartNumber.Split('_');
            Project _project = _projectRepository.QueryByMoldNumber(_data[0]);
            if (_project != null)
            {
                return _project.ProjectID;
            }
            else
            {
                return 0;
            }

        }

        public ActionResult GetUGPart(string Name)
        {
            Part _part = _partRepository.Parts.Where(p => p.Name == Name).Where(p => p.FromUG == true).FirstOrDefault();
            return Json(_part, JsonRequestBehavior.AllowGet);
        }

        public ActionResult GetPartNames(string Name)
        {
            IEnumerable<string> _names = _partRepository.Parts
                .Where(p => p.Name.Contains(Name))
                .Where(p => p.FromUG == true).OrderByDescending(p => p.Name)
                .Select(p => p.Name);
            return Json(_names, JsonRequestBehavior.AllowGet);
        }


        public bool DeleteExisting(string MoldNumber)
        {
            try
            {
                _partRepository.DeleteExisting(MoldNumber);
                return true;
            }
            catch (Exception e)
            {
                string _msg = e.ToString();
                return false;
            }
        }

        public ActionResult JsonMaterialHardness()
        {
            IEnumerable<Material> _materials = _materialRepository.Materials.Where(m => m.Enabled == true);
            IEnumerable<Hardness> _hardnesses = _hardnessRepository.Hardnesses.Where(h => h.Enabled == true);
            List<MaterialHardness> _data = new List<MaterialHardness>();
            foreach (Material _m in _materials)
            {
                IEnumerable<Hardness> _hardness = _hardnesses.Where(h => h.MaterialID == _m.MaterialID);
                if (_hardness.Count() > 0)
                {
                    foreach (Hardness _h in _hardness)
                    {
                        _data.Add(new MaterialHardness(_m, _h));
                    }
                }
                else
                {
                    _data.Add(new MaterialHardness(_m, null));
                }
            }
            _data = _data.OrderBy(m => m.MaterialName).ToList();
            return Json(_data, JsonRequestBehavior.AllowGet);
        }
        #endregion

        #region added by michael GetBomList

        public ActionResult GetMoldNumberList(string KeyWord = "")
        {
            //List<int> _projectIDs = _partRepository.Parts.Where(p => p.Enabled == true).Select(p => p.ProjectID).Distinct().ToList<int>();
            IEnumerable<string> _moldNumbers = _partListRepository.PartLists.Where(p => p.Enabled == true).Select(p => p.MoldNumber).Distinct();
            //IEnumerable<string> _moldNumbers = _projectRepository.Projects
            //    .Where(p => (_projectIDs.Contains(p.ProjectID)))
            //    .Where(p => p.ProjectStatus == 0)
            //    .Select(p => p.MoldNumber).Distinct();

            if (KeyWord != "")
            {
                _moldNumbers = _moldNumbers.Where(m => m.Contains(KeyWord));
            }
            return Json(_moldNumbers, JsonRequestBehavior.AllowGet);
        }
        #endregion
        public ActionResult Test()
        {
            return View();
        }


        public ActionResult QueryBySpec(string Keyword)
        {
            IEnumerable<Part> _parts = _partRepository.QueryBySpecification(Keyword);
            return Json(_parts, JsonRequestBehavior.AllowGet);
        }


        #region added by felix 20170729 包括对接UG
        /// <summary>
        /// UG-根据项目id获取零件列表（版本控制）
        /// </summary>
        /// <param name="ProjectID"></param>
        /// <returns></returns>
        public JsonResult GetJsonParts(int ProjectID = 0)
        {
            try
            {

                if (ProjectID != 0)
                {
                    List<Part> _parts = _partRepository.QueryByProject(ProjectID).ToList();
                    return Json(_parts, JsonRequestBehavior.AllowGet);
                }
                else
                {
                    return null;
                }
            }
            catch
            {
                return null;
            }
        }
        /// <summary>
        /// web-根据项目id获取零件列表（版本控制）
        /// </summary>
        /// <param name="ProjectID"></param>
        /// <returns></returns>
        public JsonResult GetJsonPartsByPID(int ProjectID = 0)
        {
            //IEnumerable<Part> _parts = _partRepository.QueryByProject(ProjectID);
            PartGridViewModel _parts;
            if (ProjectID != 0)
            {
                _parts = new PartGridViewModel(
                    _partRepository.QueryByProject(ProjectID)
                    .Where(p => p.Enabled == true),
                    _warehouseStockRepository);
                return Json(_parts, JsonRequestBehavior.AllowGet);
            }
            else
            {
                return null;
            }

        }

        /// <summary>
        /// 获取某零件的版本列表（版本控制） michael
        /// </summary>
        /// <param name="Name">零件名称（含版本号）</param>
        /// <returns></returns>
        public JsonResult GetJsonPartVers(string Name = "")
        {
            try
            {
                List<PartVersionInfo> pviList = new List<PartVersionInfo>();
                IList<Part> parts = _partRepository.GetPartsByName(Name).ToList();
                if (parts != null)
                {
                    List<string> _partNamelist = parts.Select(p => p.Name).Distinct().ToList();
                    foreach (var name in _partNamelist)
                    {
                        Part part = _partRepository.Parts.Where(p => p.Name == name).OrderByDescending(p => p.PartID).FirstOrDefault();
                        PartVersionInfo pvi = new PartVersionInfo();
                        if (part.Locked)
                        {
                            pvi.IsEdit = false;//加锁，不可编辑
                        }
                        else
                            pvi.IsEdit = true;//未加锁，可编辑
                        pvi.ProjectID = part.ProjectID;
                        pvi.Version = part.Version;
                        pvi.PartID = part.PartID;
                        pvi.Name = part.Name;
                        pviList.Add(pvi);
                    }
                    //foreach (Part p in parts)
                    //{
                    //    PartVersionInfo pvi = new PartVersionInfo();
                    //    if (p.Locked)
                    //    {
                    //        pvi.IsEdit = false;//加锁，不可编辑
                    //    }
                    //    else
                    //        pvi.IsEdit = true;//未加锁，可编辑
                    //    pvi.ProjectID = p.ProjectID;
                    //    pvi.Version = p.Version;
                    //    pvi.PartID = p.PartID;
                    //    pvi.Name = p.Name;
                    //    pviList.Add(pvi);
                    //}
                }
                return Json(pviList, JsonRequestBehavior.AllowGet);

            }
            catch
            {
                return null;
            }
        }

        /// <summary>
        /// 获取某模具编号的最新版本的零件清单（版本控制）
        /// </summary>
        /// <param name="MoldNumber">模具编号</param>
        /// <param name="FromUG">是否来自UG</param>
        /// <param name="Ver">模具版本号(-1 不指定版本号，默认取最新的版本)</param>
        /// <returns></returns>
        public JsonResult JsonUGPartVer(string MoldNumber, bool FromUG, int Ver = -1)
        {
            try
            {
                List<Part> _parts = new List<Part>();
                if (Ver == -1)
                {
                    PartList partlist = _partListRepository.PartLists.Where(p => p.MoldNumber == MoldNumber).Where(p => p.Latest == true).Where(p => p.Enabled == true).FirstOrDefault();
                    if (partlist != null)
                    {
                        _parts = _partRepository.Parts.Where(p => p.PartListID == partlist.PartListID).Where(p => p.FromUG == FromUG).Where(p => p.Enabled == true).ToList();
                    }
                    else
                    {
                        _parts = _partRepository.QueryByMoldNumber(MoldNumber).Where(p => p.FromUG == FromUG).Where(p => p.Enabled == true).ToList();
                    }
                }
                //else if (Ver == 0)
                //{
                //    _parts = _partRepository.QueryByMoldNumber(MoldNumber).Where(p => p.FromUG == FromUG).ToList();
                //}
                else
                {
                    PartList partlist = _partListRepository.PartLists.Where(p => p.MoldNumber == MoldNumber).Where(p => p.Version == Ver).FirstOrDefault();
                    _parts = _partRepository.Parts.Where(p => p.PartListID == partlist.PartListID).Where(p => p.FromUG == FromUG).Where(p => p.Enabled == true).ToList();
                }
                return Json(_parts, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return null;
            }

        }

        /// <summary>
        /// 保存一个零件（提供给UG使用）
        /// </summary>
        /// <param name="NewPart"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult PartFromUGVer(String NewPart)
        {
            ResponseInfo ri = new ResponseInfo();
            try
            {
                Part _part = JsonConvert.DeserializeObject<Part>(NewPart);
                _part.FromUG = true;//该方法只能UG调用

                ri = CheckIsValid(_part);

                if (ri.Status != -1)
                    ri = SavePartVer(_part);

            }
            catch (Exception ex)
            {
                ri.Status = -1;
                ri.Message = ex.Message;

            }
            return Json(ri, JsonRequestBehavior.AllowGet);
        }
        private ResponseInfo SavePartVer(Part _part, int PartListID = 0)
        {
            try
            {
                ResponseInfo ri = new ResponseInfo();
                if (_part == null) throw new Exception("推送数据异常");
                if (PartListID > 0)
                {
                    #region   更新最新版本part
                    //get latest part
                    Part _part_db = _partRepository.GetPartsByName(_part.Name, _part.ProjectID)
                    .OrderByDescending(p => p.Version).FirstOrDefault();
                    if (_part_db != null)
                    {

                        _part_db.Version = _part.Version == null ? (_part.Name.Substring(_part.Name.Length - 2, 2)) : _part.Version;
                        if (_part.MaterialID == 0)
                        {
                            Material _material = _materialRepository.GetMaterialByName(_part.MaterialName);
                            if (_material != null)
                            {
                                _part_db.MaterialID = _material.MaterialID;
                                _part_db.MaterialName = _material.Name;
                            }
                        }
                        _part_db.Hardness = _part.Hardness == null ? "" : _part.Hardness;
                        _part_db.BrandName = _part.BrandName == null ? "" : _part.BrandName;
                        _part_db.CatalogSpec = _part.CatalogSpec == null ? "" : _part.CatalogSpec;
                        _part_db.DrawingPath = _part.DrawingPath == null ? "" : _part.DrawingPath;
                        _part_db.ModelPath = _part.ModelPath == null ? "" : _part.ModelPath;
                        _part_db.JobNo = _part.JobNo == null ? "" : _part.JobNo;
                        _part_db.SupplierName = _part.SupplierName == null ? "" : _part.SupplierName;
                        _part_db.MaterialID = _part.MaterialID;
                        _part_db.MaterialName = _part.MaterialName == null ? "" : _part.MaterialName;
                        _part_db.Specification = _part.Specification == null ? "" : _part.Specification;
                        _part_db.Name = _part.Name == null ? "" : _part.Name;
                        _part_db.ShortName = _part.ShortName == null ? "" : _part.ShortName;
                        _part_db.PartNumber = _part.PartNumber == null ? "" : _part.PartNumber;
                        _part_db.Memo = _part.Memo == null ? "" : _part.Memo;
                        _part_db.Quantity = _part.Quantity;
                        _part_db.TotalQty = _part.TotalQty;
                        _part_db.AppendQty = _part.AppendQty;
                        _part_db.PartListID = PartListID;
                        _part_db.FromUG = _part.FromUG;
                        _part_db.DetailDrawing = _part.DetailDrawing;
                        _part_db.BriefDrawing = _part.BriefDrawing;
                        _part_db.PurchaseDrawing = _part.PurchaseDrawing;
                        _part_db.ExtraMaching = _part.ExtraMaching;
                        //string _moldNumber = _part.Name.Substring(0, _part.Name.IndexOf('_'));
                        //Project _project = _projectRepository.GetProjectByMoldNumberVer(_moldNumber);
                        _part_db.ProjectID = 0;//_project == null ? 0 : _project.ProjectID;
                        //UG端过来的零件只有新建时 FromUG属性设置为true
                        //_part_db.FromUG = true;
                        _part_db.Enabled = true;
                        int _partID = _partRepository.SaveNew(_part_db);
                    }
                    else
                    {
                        _part.ProjectID = 0;
                        //_part.FromUG = true;
                        //当前版本新建零件
                        _partRepository.SaveNew(_part);
                    }
                    #endregion
                    ri.Status = 1;
                    ri.Message = "保存成功";
                    return ri;
                }
                else
                {
                    _part.CreateDate = DateTime.Now;
                    #region   新建part
                    _part.Version = _part.Version == null ? (_part.Name.Substring(_part.Name.Length - 2, 2)) : _part.Version;
                    if (_part.MaterialID == 0)
                    {
                        Material _material = _materialRepository.GetMaterialByName(_part.MaterialName);
                        if (_material != null)
                        {
                            _part.MaterialID = _material.MaterialID;
                            _part.MaterialName = _material.Name;
                        }
                    }
                    _part.Hardness = _part.Hardness == null ? "" : _part.Hardness;
                    _part.BrandName = _part.BrandName == null ? "" : _part.BrandName;
                    _part.CatalogSpec = _part.CatalogSpec == null ? "" : _part.CatalogSpec;
                    _part.DrawingPath = _part.DrawingPath == null ? "" : _part.DrawingPath;
                    _part.ModelPath = _part.ModelPath == null ? "" : _part.ModelPath;
                    _part.JobNo = _part.JobNo == null ? "" : _part.JobNo;
                    _part.SupplierName = _part.SupplierName == null ? "" : _part.SupplierName;
                    string _moldNumber = _part.Name.Substring(0, _part.Name.IndexOf('_'));
                    //Project _project = _projectRepository.GetProjectByMoldNumberVer(_moldNumber);
                    //_part.ProjectID = _project == null ? 0 : _project.ProjectID;
                    //_part.FromUG = true;
                    _part.Enabled = true;
                    _part.PartID = 0;
                    int _partID = _partRepository.SaveNew(_part);
                    #endregion
                    ri.Status = 1;
                    ri.Message = "保存成功";
                    return ri;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #region Chk Parts
        private ResponseInfo CheckIsValid(Part _part)
        {
            ResponseInfo ri = new ResponseInfo();
            try
            {

                if (_part == null) throw new Exception("推送数据异常");

                //if (_part.PartID == 0)
                //{
                //    _part.CreateDate = DateTime.Now;
                //}
                //#region   
                //_part.Version = _part.Version == null ? (_part.Name.Substring(_part.Name.Length - 2, 2)) : _part.Version;
                //if (_part.MaterialID == 0)
                //{
                //    Material _material = _materialRepository.GetMaterialByName(_part.MaterialName);
                //    if (_material != null)
                //    {
                //        _part.MaterialID = _material.MaterialID;
                //        _part.MaterialName = _material.Name;
                //    }
                //}
                //_part.Hardness = _part.Hardness == null ? "" : _part.Hardness;
                //_part.BrandName = _part.BrandName == null ? "" : _part.BrandName;
                //_part.CatalogSpec = _part.CatalogSpec == null ? "" : _part.CatalogSpec;
                //_part.DrawingPath = _part.DrawingPath == null ? "" : _part.DrawingPath;
                //_part.ModelPath = _part.ModelPath == null ? "" : _part.ModelPath;
                //_part.JobNo = _part.JobNo == null ? "" : _part.JobNo;
                //_part.SupplierName = _part.SupplierName == null ? "" : _part.SupplierName;
                //string _moldNumber = _part.Name.Substring(0, _part.Name.IndexOf('_'));
                ////Project _project = _projectRepository.GetProjectByMoldNumberVer(_moldNumber);

                //_part.ProjectID = 0;//_project == null ? 0 : _project.ProjectID;
                //_part.FromUG = true;
                //#endregion

                //获取该模具的最新版本的零件
                Part _part_db = _partRepository.GetPartsByName(_part.Name)
                    .OrderByDescending(p => p.PartID).FirstOrDefault();
                if (_part_db == null)
                {
                    //新增
                    ri.Status = 1;
                    ri.Message = "OK";
                    return ri;
                }
                string ver_db = _part_db.Version;
                string ver_ug = _part.Version;
                //判断是否可以修改零件的关键字段，如果不可以修改，则需要判断提交的part的关键字段是否与库里一致
                //true:可以修改 false：不可以修改
                bool isModifyKey = CheckIsModifyKey(_part_db);
                if (!isModifyKey)
                {
                    bool isSame = CheckPartDBToUG(_part_db, _part);//判断关键字段是否一致

                    if (ver_db.Equals(ver_ug) && !isSame)
                    {
                        //版本号一样，说明不是升级，但是内容不一样，即数据异常，请调整版本号
                        throw new Exception("该版本的零件【" + _part.Name + "】已存在，并且内容不一样，需修改版本号重新提交.");
                    }
                }
                //else
                //{
                //    ri = SavePartVer(_part);
                //}

                if (ver_ug.CompareTo(ver_db) < 0)
                {
                    throw new Exception("该零件【" + _part.Name + "】的版本号小于当前最新版本号，请修改版本号重新提交.");
                }
                //内容一样，不需修改
                //if (ver_db.Equals(ver_ug) && isSame)
                //{
                //    ri.Status = 1;
                //    ri.Message = "OK";

                //}
                //if (ver_ug.CompareTo(ver_db) > 0)
                //{
                //    //升级
                //    ri.Status = 1;
                //    ri.Message = "OK";
                //}
                ri.Status = 1;
                ri.Message = "OK";
                return ri;
            }
            catch (Exception ex)
            {
                ri.Status = -1;
                ri.Message = ex.Message;
                return ri;
            }

        }

        public bool CheckIsModifyKey(Part p)
        {
            try
            {
                //判断是否存在发布的bom版本
                //var prjs = _partRepository.GetProjectsByNameIsPublish(p.Name);
                //var part = _partRepository.QueryByName(p.Name) ?? new Part();
                //if (part.PartListID > 0)
                //{
                //    return false;//不能改，只能升级
                //}
                //else
                //{
                //    //判断是否被采购
                //    if (p.InPurchase)
                //    {
                //        return false;//不能改，只能升级
                //    }
                //    else
                //    {
                //        return true;//可以修改
                //    }
                //}
                //零件被锁 or 升版带过来
                if (p.Locked || p.Status <= 0)
                {
                    return false;//不能改，只能升级
                }
                else
                {
                    return true;//可以修改
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        public ActionResult CheckIsModify(string NewPart)
        {
            ResponseInfo ri = new ResponseInfo();
            Part p = JsonConvert.DeserializeObject<Part>(NewPart);
            try
            {
                bool mark = CheckIsModifyKey(p);
                if (mark)
                {
                    ri.Status = 1;
                    ri.Message = "可以修改关键属性";
                }
                else
                {
                    ri.Status = 0;
                    ri.Message = "不能修改关键属性";
                }
            }
            catch (Exception ex)
            {
                ri.Status = -1;
                ri.Message = ex.Message;
            }
            return Json(ri, JsonRequestBehavior.AllowGet);

        }
        #endregion
        #region Log
        public void WriteLog(string Path, string Content)
        {
            if (!System.IO.File.Exists(Path))
            {
                FileStream fs1 = new FileStream(Path, FileMode.Create, FileAccess.Write);//创建写入文件 
                StreamWriter sw = new StreamWriter(fs1);
                sw.WriteLine(Content);//开始写入值
                sw.Close();
                fs1.Close();
            }
            else
            {
                FileStream fs = new FileStream(Path, FileMode.Open, FileAccess.Write);
                StreamWriter sr = new StreamWriter(fs);
                sr.WriteLine(Content);//开始写入值
                sr.Close();
                fs.Close();
            }
        }
        #endregion
        /// <summary>
        /// 保存一批零件
        /// </summary>
        /// <param name="NewParts"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult PartListFromUGVer(string NewParts)
        {
            ResponseInfo ri = new ResponseInfo();
            try
            {
                string WebLogPath = Server.MapPath("~/Log/");
                if (!Directory.Exists(WebLogPath))
                {
                    DirectoryInfo dir = new DirectoryInfo(WebLogPath);
                    dir.Create();
                }
                List<Part> _parts = JsonConvert.DeserializeObject<IEnumerable<Part>>(NewParts).ToList();
                string MoldNum = "";
                string[] strlist = _parts[0].Name.Split(new char[] { '_' });
                MoldNum = strlist[0] ?? "";
                //写日志
                string logPath = WebLogPath + MoldNum + "_" + DateTime.Now.ToString("yyMMddhhmmss") + ".txt";
                Toolkits.WriteLog(logPath, NewParts);
                //
                PartList newpl = _partListRepository.QueryByMoldNumber(MoldNum, true).FirstOrDefault() ?? new PartList();
                //bool IsInitialBom = true;
                if (newpl.PartListID != 0)
                {
                    foreach (Part _part in _parts)
                    {
                        ri = CheckIsValid(_part);
                        if (ri.Status == -1)
                        {
                            break;
                        }
                    }
                }
                if (ri.Status == -1)
                {
                    return Json(ri, JsonRequestBehavior.AllowGet);
                }
                else
                {
                    #region 新建初始bom
                    if (newpl.PartListID == 0)
                    {
                        Project _project = _projectRepository.GetProjectByMoldNumberVer(MoldNum) ?? new Project();
                        //
                        PartList partlist = new PartList();
                        partlist.MoldNumber = MoldNum;
                        partlist.Released = false;
                        partlist.Version = 0;
                        partlist.Enabled = true;
                        partlist.Latest = true;
                        partlist.ProjectID = _project.ProjectID;
                        partlist.CreateDate = DateTime.Now;
                        //r => partlistID
                        int r = _partListRepository.Save(partlist);
                        if (r > 0)
                        {
                            foreach (var _part in _parts)
                            {
                                _part.PartListID = r;
                                _part.Latest = true;
                                SavePartVer(_part);
                            }
                        }
                        else
                        {
                            ri.Status = -1;
                            ri.Message = "创建Bom失败！";
                        }
                    }
                    #endregion
                    else
                    {
                        foreach (var _part in _parts)
                        {
                            _part.PartListID = newpl.PartListID;
                            _part.Latest = true;
                            SavePartVer(_part, newpl.PartListID);
                        }
                    }
                    PartList latestpl = _partListRepository.QueryByMoldNumber(MoldNum, true).FirstOrDefault() ?? new PartList();
                    List<Part> partsDB = _partRepository.Parts.Where(p => p.PartListID == latestpl.PartListID).Where(p => p.Enabled == true).ToList();//_partRepository.QueryByProject(projectId).ToList();

                    foreach (Part p in partsDB)
                    {
                        bool mark = false;
                        //p.ProjectID = projectId;
                        foreach (Part pUG in _parts)
                        {
                            if (p.Name.Substring(0, p.Name.Length - 2) == pUG.Name.Substring(0, pUG.Name.Length - 2))
                            {
                                mark = true;
                                break;
                            }

                        }
                        if (!mark)
                        {
                            if (p.FromUG)
                            {
                                //提交清单中是UG 的，如果没有，即删除
                                p.Enabled = false;
                                _partRepository.SaveNew(p);
                            }
                        }
                    }

                }
            }
            catch (Exception ex)
            {
                ri.Status = -1;
                ri.Message = ex.Message + "??";

            }
            return Json(ri, JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        /// 检查数据库中的零件和UG推送的零件明细是否一致
        /// </summary>
        /// <param name="partDB"></param>
        /// <param name="partUG"></param>
        /// <returns></returns>
        private bool CheckPartDBToUG(Part partDB, Part partUG)
        {
            //if (partDB.AppendQty != partUG.AppendQty)
            //    return false;

            //if (partDB.BrandID != partUG.BrandID)
            //    return false;
            //if (partDB.BrandName != partUG.BrandName)
            //    return false;
            //if (partDB.BriefDrawing != partUG.BriefDrawing)
            //    return false;
            //if (partDB.CatalogSpec != partUG.CatalogSpec)
            //    return false;
            //if (partDB.DetailDrawing != partUG.DetailDrawing)
            //    return false;
            //if (partDB.DrawingPath != partUG.DrawingPath)
            //    return false;
            //if (partDB.ExtraMaching != partUG.ExtraMaching)
            //    return false;
            //if (partDB.Hardness != partUG.Hardness)
            //    return false;
            //if (partDB.JobNo != partUG.JobNo)
            //    return false;

            //材料名称
            if (partDB.MaterialName.ToLower() != partUG.MaterialName.ToLower())
                return false;
            //附图订购
            if (partDB.PurchaseDrawing != partUG.PurchaseDrawing)
                return false;
            //数量
            if (partDB.Quantity != partUG.Quantity)
                return false;
            //180601 不管备件 michael  
            ////总数
            //if (partDB.TotalQty != partUG.TotalQty)
            //    return false;
            //规格
            if (partDB.Specification.ToLower() != partUG.Specification.ToLower())
                return false;
            //品牌名称
            if (partDB.BrandName.ToLower() != partUG.BrandName.ToLower())
                return false;

            //if (partDB.Memo != partUG.Memo)
            //    return false;
            //if (partDB.ModelPath != partUG.ModelPath)
            //    return false;
            //if (partDB.Name != partUG.Name)
            //    return false;
            //if (partDB.PartListID != partUG.PartListID)
            //    return false;
            //if (partDB.PartNumber != partUG.PartNumber)
            //    return false;
            //if (partDB.ProjectID != partUG.ProjectID)
            //    return false;
            //if (partDB.Version != partUG.Version)
            //    return false;
            //if (partDB.FromUG != partUG.FromUG)
            //    return false;
            return true;
        }


        /// <summary>
        /// upgrade partlist 
        /// </summary>
        /// <param name="projectID"></param>
        /// <returns></returns>
        public int UpgradePartlist(string moldNumber)
        {
            try
            {
                //创建新的partlist 版本  先发布 再升级
                PartList partlist = new PartList();
                PartList Lastpl = _partListRepository.QueryByMoldNumber(moldNumber).OrderByDescending(p => p.Version).FirstOrDefault();
                int NewpartlistID = 0;
                if (Lastpl != null)
                {
                    partlist.PartListID = 0;
                    partlist.MoldNumber = Lastpl.MoldNumber;
                    partlist.Version = Lastpl.Version + 1;
                    partlist.Enabled = true;
                    partlist.PrevVersion = Lastpl.Version;
                    partlist.Latest = true;
                    partlist.ProjectID = Lastpl.ProjectID;
                    partlist.CreateDate = DateTime.Now;
                    Lastpl.Latest = false;
                    NewpartlistID = _partListRepository.Save(partlist);
                    _partListRepository.Save(Lastpl);
                }
                else
                {
                    return 0;
                }
                //copy上一版的Bom 的part 清单

                List<Part> pList = _partRepository.Parts.Where(p => p.PartListID == Lastpl.PartListID).ToList();
                foreach (Part part in pList)
                {
                    //
                    if (part.Enabled == true)
                    {
                        PropertyInfo[] pis = part.GetType().GetProperties();
                        Part entity = new Part();
                        foreach (PropertyInfo pi in pis)
                        {
                            pi.SetValue(entity, pi.GetValue(part));
                        }
                        entity.PartID = 0;
                        entity.ProjectID = 0;//Lastpl.ProjectID;
                        entity.PartListID = NewpartlistID;
                        entity.Status = 0;//重置为0
                        //升级bom 零件全部解冻
                        entity.Locked = false;
                        _partRepository.SaveUpgrade(entity);
                    }
                    part.Latest = false;
                    part.Locked = true;
                    part.ReleaseDate = DateTime.Now;
                    _partRepository.SaveNew(part);
                }

                return 1;
            }
            catch (Exception ex)
            {
                return 0;
            }

            //return RedirectToAction("Index", "Part", new { ProjectID = Part.ProjectID });
        }
        /// <summary>
        /// judge either Upgrade michael
        /// </summary>
        /// <param name="ProjectID"></param>
        /// <returns></returns>
        public int GetIsUpgrade(int PartListID)
        {
            try
            {
                //当前PartList
                PartList partlist = _partListRepository.PartLists.Where(p => p.PartListID == PartListID).FirstOrDefault();
                if (partlist != null && partlist.Released == true && partlist.Latest == true)
                {//可以升级（最新的版本 ，已发布的）
                    return 1;
                }
                else if (partlist != null && partlist.Latest != true)
                {
                    return 0;//不是最新版本
                }
                else if (partlist != null && partlist.Latest == true && partlist.Released == false)
                {
                    return 2;//该最新版本未发布
                }
                else
                    //00版
                    return 3;
            }
            catch
            {
                return -1;
            }
        }
        /// <summary>
        /// 获取某模具的版本清单（版本控制）  michael
        /// </summary>
        /// <param name="MoldNumber">模具编号</param>
        /// <returns></returns>
        public JsonResult GetMoldVerList(string MoldNumber)
        {
            try
            {
                List<MoldVersionInfo> _versionList =
                    _partRepository.GetMoldVerList(MoldNumber);
                return Json(_versionList, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return null;
            }
        }
        /// <summary>
        /// web 升级
        /// </summary>
        /// <param name="ids"></param>
        /// <returns></returns>
        public JsonResult UpgradePart(string ids)
        {
            List<object> list = new List<object>();

            try
            {

                string[] strIds = ids.Split(',');
                foreach (string id in strIds)
                {
                    //创建新的part 版本 
                    Part part = _partRepository.QueryByID(int.Parse(id));
                    if (part.FromUG)
                    {
                        //ug web不能升级
                        list.Add(new { PartName = part.Name });
                    }
                    else
                    {
                        //升级
                        #region 升级

                        Part entity = new Part();
                        PropertyInfo[] pis = part.GetType().GetProperties();
                        foreach (PropertyInfo pi in pis)
                        {
                            pi.SetValue(entity, pi.GetValue(part));
                        }
                        part.Enabled = false;
                        part.Locked = true;

                        entity.PartID = 0;
                        string ver = (int.Parse("1" + part.Version) + 1).ToString().Substring(1, 2);
                        entity.Version = ver;
                        entity.Name = entity.Name.Substring(0, entity.Name.Length - 2) + ver;
                        entity.CreateDate = DateTime.Now;
                        entity.Status = part.Status + 1;
                        entity.Locked = false;
                        _partRepository.SaveNew(part);
                        _partRepository.SaveNew(entity);

                        #endregion
                    }
                }

                return Json(list, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(list, JsonRequestBehavior.AllowGet);
            }

            //return RedirectToAction("Index", "Part", new { ProjectID = Part.ProjectID });
        }

        public int GetIsDelete(int partId = 0)
        {
            try
            {
                Part part = _partRepository.QueryByID(partId);
                if (part != null)
                {
                    Project prj = _projectRepository.GetByID(part.ProjectID);
                    if (prj != null && prj.IsPublish == false && part.InPurchase == false && part.FromUG == false)
                    {
                        //可以删除
                        return 1;
                    }
                    else if (prj != null && prj.IsPublish == false && part.FromUG == true)
                    {
                        return 2;//NX 料号不能在网页端删除

                    }
                    else if (prj != null && prj.IsPublish == true)
                    {
                        return 3;//项目已发布，不能删除关联的零件

                    }
                    else
                    {
                        return 0;
                    }
                }
                else
                    return 0;
            }
            catch (Exception ex)
            {
                return -1;
            }
        }

        #endregion
        #region By Michael 
        /// <summary>
        /// publish PartList
        /// </summary>
        /// <param name="MoldNum"></param>
        /// <returns></returns>
        public int PublishPartList(string MoldNum)
        {
            List<PartList> pl = _partListRepository.QueryByMoldNumber(MoldNum, true).ToList();
            //获取新模项目号
            Project project = _projectRepository.GetProjectByMoldNumberVer(MoldNum, -1) ?? new Project();
            try
            {
                #region 发布Bom 并对Bom中零件加锁
                if (pl.Count > 0)
                {
                    //获取最后一个版本
                    PartList Last_pl = pl.OrderByDescending(p => p.PartListID).FirstOrDefault();
                    Last_pl.Released = true;
                    Last_pl.ReleaseDate = DateTime.Now;
                    List<Part> parts = _partRepository.Parts.Where(p => p.PartListID == Last_pl.PartListID).Where(p => p.Enabled == true).ToList();
                    if (parts.Count > 0)
                    {
                        foreach (var part in parts)
                        {
                            part.Locked = true;
                            _partRepository.Save(part);
                        }
                        _partListRepository.Save(Last_pl);
                    }

                }
                else
                {
                    //创建bom 当初逻辑是在00版发布时创建最初bom
                    PartList partlist = new PartList();
                    partlist.MoldNumber = MoldNum;
                    partlist.Released = true;
                    partlist.Version = 0;
                    partlist.Enabled = true;
                    partlist.Latest = true;
                    partlist.ProjectID = project.ProjectID;
                    partlist.CreateDate = DateTime.Now;
                    partlist.ReleaseDate = DateTime.Now;
                    int r = _partListRepository.Save(partlist);
                    if (r > 0)
                    {
                        PartList newpl = _partListRepository.QueryByMoldNumber(MoldNum, true).FirstOrDefault();
                        List<Part> parts = _partRepository.QueryByMoldNumber(MoldNum).Where(p => p.Enabled == true).ToList();
                        foreach (var part in parts)
                        {
                            part.PartListID = newpl.PartListID;
                            part.Locked = true;
                            _partRepository.Save(part);
                        }
                    }
                }
                #endregion
                return 1;
            }
            catch { }
            return 0;
        }
        /// <summary>
        /// web-根据Bom id获取零件列表（版本控制）
        /// </summary>
        /// <param name=""></param>
        /// <returns></returns>
        public JsonResult GetJsonPartsByBomID(int PartListID = 0, string MoldNumber = "")
        {
            //IEnumerable<Part> _parts = _partRepository.QueryByProject(ProjectID);
            PartGridViewModel _parts;
            if (PartListID != 0)
            {
                _parts = new PartGridViewModel(
                    _partRepository.Parts.Where(p => p.PartListID == PartListID)
                    .Where(p => p.Enabled == true),
                    _warehouseStockRepository);
                return Json(_parts, JsonRequestBehavior.AllowGet);
            }
            else if (!string.IsNullOrEmpty(MoldNumber))
            {
                _parts = new PartGridViewModel(
                   _partRepository.Parts.Where(p => p.PartNumber.Contains(MoldNumber))
                   .Where(p => p.Enabled == true),
                   _warehouseStockRepository);
                return Json(_parts, JsonRequestBehavior.AllowGet);
            }
            else
            {
                _parts = new PartGridViewModel(
                   _partRepository.Parts.Where(p => p.PartNumber == MoldNumber)
                   .Where(p => p.Enabled == true),
                   _warehouseStockRepository);
                return Json(_parts, JsonRequestBehavior.AllowGet);
            }

        }
        public JsonResult GetPartListJson(int PartListID = 0)
        {
            try
            {
                if (PartListID != 0)
                {
                    PartList partlist = _partListRepository.Query(PartListID);
                    return Json(partlist, JsonRequestBehavior.AllowGet);
                }
                else
                {
                    PartList partlist = new PartList { PartListID = 0, MoldNumber = "", Version = 0, Released = false, Enabled = true, Latest = true, PrevVersion = 0, ProjectID = 0 };
                    return Json(partlist, JsonRequestBehavior.AllowGet);
                }
            }
            catch
            {
                return null;
            }
        }
        #endregion
        #region PartList Import  By Michael
        [HttpPost]
        public ActionResult PLImport(HttpPostedFileBase file)
        {
            try
            {
                #region 将文件保存至服务器
                var fileName = file.FileName.Split('.')[0] + "_" + DateTime.Now.ToString("yyMMddhhmmss") + "." + file.FileName.Split('.')[1];
                var filePath = Server.MapPath(string.Format("~/{0}/{1}", "File", "PLImport"));
                if (!Directory.Exists(filePath))
                {
                    Directory.CreateDirectory(filePath);
                }
                var ImportFilePath = Path.Combine(filePath, fileName);
                file.SaveAs(ImportFilePath);
                #endregion
                #region 将数据上传至服务器
                ResponseInfo response = FileImport(ImportFilePath);
                if (response.Status > 0)
                {
                    string _partsStr = JsonConvert.SerializeObject(response.Data);
                    try
                    {
                        //检查零件版本、关键字变更
                        foreach(var _part in response.Data)
                        {
                            ResponseInfo ri = CheckIsValid(_part);
                            if (ri.Status == -1)
                            {
                                return Json(new { code = -3, Message = ri.Message });
                            }
                        }
                        PartListFromUGVer(_partsStr);
                    }
                    catch (Exception ex)
                    {
                        return Json(new { code = -2, Message = "将数据上传至服务器失败！原因：" + ex.ToString() });
                    }
                }
                return Json(new { code = 1, Message = response.Message });
                #endregion
            }
            catch (Exception ex)
            {
                return Json(new { code = -1, Message = "将文件保存至服务器失败！原因：" + ex.ToString() });
            }

        }
        public ResponseInfo FileImport(string ImportFilePath)
        {
            XSSFWorkbook hssfworkbook;
            //ResponseInfo response;
            List<Part> _parts = new List<Part>();
            try
            {
                using (FileStream fs = System.IO.File.Open(ImportFilePath, FileMode.Open,
                FileAccess.Read, FileShare.ReadWrite))
                {
                    //把xls文件读入workbook变量里，之后就可以关闭了
                    hssfworkbook = new XSSFWorkbook(fs);
                    fs.Close();
                }
            }
            catch (Exception ex)
            {
                return new ResponseInfo { Status = -99, Message = "读取Excel文件出错！原因：" + ex.ToString(), Data = new List<Part>() };
                throw ex;
            }
            #region 读取Excel文件数据 检查正确 存到数据库
            try
            {
                #region 普通Sheet
                XSSFSheet sheet0 = (XSSFSheet)hssfworkbook.GetSheetAt(0);
                #region 普通Sheet Title
                IRow row_3 = sheet0.GetRow(2);
                IRow row_4 = sheet0.GetRow(3);
                //设计
                ICell cell_Desiger = row_3.GetCell(5);
                string Desiger = GetCellValue(cell_Desiger);
                //项目编号
                ICell cell_PJNum = row_4.GetCell(5);
                string PJNum = GetCellValue(cell_PJNum);
                //模具编号
                ICell cell_MoldNum = row_4.GetCell(8);
                string MoldNum = GetCellValue(cell_MoldNum);
                if (string.IsNullOrEmpty(MoldNum))
                    return new ResponseInfo { Status = -1, Message = "模具编号不能为空，请重新导入！", Data = new List<Part>() };
                PartList LatestPL = _partListRepository.QueryByMoldNumber(MoldNum, true).FirstOrDefault() ?? new PartList();
                if (LatestPL.PartListID > 0 && GetIsUpgrade(LatestPL.PartListID) != 2)//返回2 说明Partlist是最新版并且未发布
                    return new ResponseInfo { Status = -1, Message = string.Format("模具:{0} 的Partlist最新版本{1}已发布，不允许变更！", MoldNum, LatestPL.Version.ToString()), Data = new List<Part>() };
                //模具名称
                ICell cell_MoldName = row_3.GetCell(8);
                string MoldName = GetCellValue(cell_MoldName);
                //版本
                ICell cell_Version = row_3.GetCell(14);
                string Version = GetCellValue(cell_Version);
                //日期
                ICell cell_Date = row_4.GetCell(14);
                try
                {
                    DateTime date = Convert.ToDateTime(GetCellValue(cell_Date));
                }
                catch
                {
                    DateTime date = new DateTime(1900, 1, 1);
                }

                #endregion
                #region 普通Sheet Content
                int Sheet0_Index = 0;
                int Sheet0_ContentRowIndex = 6;//正文从第7行开始
                int Sheet0_ContentRowNow = Sheet0_ContentRowIndex;//正文当前行
                string ShortName = GetCellValue(sheet0.GetRow(Sheet0_ContentRowIndex).GetCell(1));//零件名位于第2列
                //当零件名不为null时 遍历
                while (!string.IsNullOrEmpty(ShortName))
                {
                    IRow row_Content = sheet0.GetRow(Sheet0_ContentRowNow);
                    Part _entity = new Part();
                    //LineNum
                    Sheet0_Index = Sheet0_Index + 1;
                    //零件短名 非Null
                    ICell cell_ShortName = row_Content.GetCell(1);
                    _entity.ShortName = GetCellValue(cell_ShortName);
                    if (string.IsNullOrEmpty(_entity.ShortName))
                        return new ResponseInfo { Status = -2, Message = string.Format("Sheet{0}: 行{1}短零件名不能为空,请重新导入！", "0", Sheet0_Index.ToString()), Data = new List<Part>() };
                    //物料编号
                    ICell cell_PartNum = row_Content.GetCell(2);
                    _entity.PartNumber = GetCellValue(cell_PartNum);
                    //版本
                    ICell cell_PartVersion = row_Content.GetCell(3);
                    string PartVersion = "0" + GetCellValue(cell_PartVersion) == null ? "00" : GetCellValue(cell_PartVersion);
                    _entity.Version = PartVersion.Substring(PartVersion.Count() - 2, 2);
                    //ERP料号
                    ICell cell_ERPPartNum = row_Content.GetCell(4);
                    _entity.ERPPartID = GetCellValue(cell_ERPPartNum);
                    //尺寸或规格  非Null
                    ICell cell_Spec = row_Content.GetCell(5);
                    _entity.Specification = GetCellValue(cell_Spec);
                    if (string.IsNullOrEmpty(_entity.Specification))
                        return new ResponseInfo { Status = -2, Message = string.Format("Sheet{0}: 行{1}短零件规格不能为空,请重新导入！", "0", Sheet0_Index.ToString()), Data = new List<Part>() };
                    //材料
                    ICell cell_MaterialName = row_Content.GetCell(6);
                    _entity.MaterialName = GetCellValue(cell_MaterialName);
                    Material _material = _materialRepository.QueryByName(_entity.MaterialName).FirstOrDefault() ?? new Material();
                    _entity.MaterialID = _material.MaterialID;
                    //数量  非Null
                    ICell cell_Qty = row_Content.GetCell(7);
                    _entity.Quantity = GetCellValue(cell_Qty) == null ? 0 : Convert.ToInt32(GetCellValue(cell_Qty));
                    if (_entity.Quantity == 0)
                        return new ResponseInfo { Status = -2, Message = string.Format("Sheet{0}: 行{1}数量不能为0,请重新导入！", "0", Sheet0_Index.ToString()), Data = new List<Part>() };
                    //硬度
                    ICell cell_HardnessName = row_Content.GetCell(8);
                    _entity.Hardness = GetCellValue(cell_HardnessName);
                    //品牌
                    ICell cell_BrandName = row_Content.GetCell(9);
                    _entity.BrandName = GetCellValue(cell_BrandName);
                    //零件号  非Null
                    ICell cell_JobNo = row_Content.GetCell(10);
                    _entity.JobNo = GetCellValue(cell_JobNo);
                    if (string.IsNullOrEmpty(_entity.JobNo))
                        return new ResponseInfo { Status = -2, Message = string.Format("Sheet{0}: 行{1}零件号不能为空,请重新导入！", "0", Sheet0_Index.ToString()), Data = new List<Part>() };
                    //详图
                    ICell cell_xt = row_Content.GetCell(11);
                    _entity.DetailDrawing = GetCellValue(cell_xt) == "Y" ? true : false; ;
                    //简图
                    ICell cell_jt = row_Content.GetCell(12);
                    _entity.BriefDrawing = GetCellValue(cell_jt) == "Y" ? true : false; ;
                    //附图订购
                    ICell cell_ftdg = row_Content.GetCell(13);
                    _entity.PurchaseDrawing = GetCellValue(cell_ftdg) == "Y" ? true : false; ;
                    //追加工
                    ICell cell_zjg = row_Content.GetCell(14);
                    _entity.ExtraMaching = GetCellValue(cell_zjg) == "Y" ? true : false;
                    //备注
                    ICell cell_Memo = row_Content.GetCell(15);
                    _entity.Memo = GetCellValue(cell_Memo);

                    _entity.PartID = 0;
                    _entity.Name = MoldNum + "_" + _entity.ShortName + "_" + _entity.JobNo + "_V" + _entity.Version;
                    _entity.CreateDate = DateTime.Now;
                    _entity.Status = 1;
                    _entity.Locked = false;
                    _entity.Latest = true;
                    _entity.Enabled = true;
                    _entity.FromUG = false;
                    _parts.Add(_entity);
                    //行+1
                    Sheet0_ContentRowNow = Sheet0_ContentRowNow + 1;
                    ShortName = GetCellValue(sheet0.GetRow(Sheet0_ContentRowNow).GetCell(1));
                }

                #endregion

                #endregion

                #region 模架Sheet
                XSSFSheet sheet1 = (XSSFSheet)hssfworkbook.GetSheetAt(1);
                #region 模架Sheet Content
                int Sheet1_Index = 0;
                int Sheet1_ContentRowIndex = 6;//正文从第7行开始
                int Sheet1_ContentRowNow = Sheet1_ContentRowIndex;//正文当前行
                string Sheet1_ShortName = GetCellValue(sheet1.GetRow(Sheet1_ContentRowIndex).GetCell(1));//零件名位于第2列
                //当零件名不为null时 遍历
                while (!string.IsNullOrEmpty(Sheet1_ShortName))
                {
                    IRow row_Content = sheet1.GetRow(Sheet1_ContentRowNow);
                    Part _entity = new Part();
                    //LineNum
                    Sheet1_Index = Sheet1_Index + 1;
                    //零件短名 非Null
                    ICell cell_ShortName = row_Content.GetCell(1);
                    _entity.ShortName = GetCellValue(cell_ShortName);
                    if (string.IsNullOrEmpty(_entity.ShortName))
                        return new ResponseInfo { Status = -2, Message = string.Format("Sheet{0}: 行{1}短零件名不能为空,请重新导入！", "1", Sheet1_Index.ToString()), Data = new List<Part>() };
                    //物料编号
                    ICell cell_PartNum = row_Content.GetCell(2);
                    _entity.PartNumber = GetCellValue(cell_PartNum);
                    //版本
                    ICell cell_PartVersion = row_Content.GetCell(3);
                    string PartVersion = "0" + GetCellValue(cell_PartVersion) == null ? "00" : GetCellValue(cell_PartVersion);
                    _entity.Version = PartVersion.Substring(PartVersion.Count() - 2, 2);
                    //ERP料号
                    ICell cell_ERPPartNum = row_Content.GetCell(4);
                    _entity.ERPPartID = GetCellValue(cell_ERPPartNum);
                    //尺寸或规格  非Null
                    ICell cell_Spec = row_Content.GetCell(5);
                    _entity.Specification = GetCellValue(cell_Spec);
                    if (string.IsNullOrEmpty(_entity.Specification))
                        return new ResponseInfo { Status = -2, Message = string.Format("Sheet{0}: 行{1}短零件规格不能为空,请重新导入！", "1", Sheet1_Index.ToString()), Data = new List<Part>() };
                    //材料
                    ICell cell_MaterialName = row_Content.GetCell(6);
                    _entity.MaterialName = GetCellValue(cell_MaterialName);
                    Material _material = _materialRepository.QueryByName(_entity.MaterialName).FirstOrDefault() ?? new Material();
                    _entity.MaterialID = _material.MaterialID;
                    //数量  非Null
                    ICell cell_Qty = row_Content.GetCell(7);
                    _entity.Quantity = GetCellValue(cell_Qty) == null ? 0 : Convert.ToInt32(GetCellValue(cell_Qty));
                    if (_entity.Quantity == 0)
                        return new ResponseInfo { Status = -2, Message = string.Format("Sheet{0}: 行{1}数量不能为0,请重新导入！", "1", Sheet1_Index.ToString()), Data = new List<Part>() };
                    //硬度
                    ICell cell_HardnessName = row_Content.GetCell(8);
                    _entity.Hardness = GetCellValue(cell_HardnessName);
                    //品牌
                    ICell cell_BrandName = row_Content.GetCell(9);
                    _entity.BrandName = GetCellValue(cell_BrandName);
                    //零件号  非Null
                    ICell cell_JobNo = row_Content.GetCell(10);
                    _entity.JobNo = GetCellValue(cell_JobNo);
                    if (string.IsNullOrEmpty(_entity.JobNo))
                        return new ResponseInfo { Status = -2, Message = string.Format("Sheet{0}: 行{1}零件号不能为空,请重新导入！", "1", Sheet1_Index.ToString()), Data = new List<Part>() };
                    //详图
                    ICell cell_xt = row_Content.GetCell(11);
                    _entity.DetailDrawing = GetCellValue(cell_xt) == "Y" ? true : false; ;
                    //简图
                    ICell cell_jt = row_Content.GetCell(12);
                    _entity.BriefDrawing = GetCellValue(cell_jt) == "Y" ? true : false; ;
                    //附图订购
                    ICell cell_ftdg = row_Content.GetCell(13);
                    _entity.PurchaseDrawing = GetCellValue(cell_ftdg) == "Y" ? true : false; ;
                    //追加工
                    ICell cell_zjg = row_Content.GetCell(14);
                    _entity.ExtraMaching = GetCellValue(cell_zjg) == "Y" ? true : false;
                    //备注
                    ICell cell_Memo = row_Content.GetCell(15);
                    _entity.Memo = GetCellValue(cell_Memo);

                    _entity.PartID = 0;
                    _entity.Name = MoldNum + "_" + _entity.ShortName + "_" + _entity.JobNo + "_V" + _entity.Version;
                    _entity.CreateDate = DateTime.Now;
                    _entity.Status = 1;
                    _entity.Locked = false;
                    _entity.Latest = true;
                    _entity.Enabled = true;
                    _entity.FromUG = false;
                    _parts.Add(_entity);
                    //行+1
                    Sheet1_ContentRowNow = Sheet1_ContentRowNow + 1;
                    Sheet1_ShortName = GetCellValue(sheet1.GetRow(Sheet1_ContentRowNow).GetCell(1));
                }

                #endregion

                #endregion
                if((Sheet0_Index + Sheet1_Index) > 0)
                {
                    return new ResponseInfo { Status = 1, Message = string.Format("全部{0}行 导入成功！", (Sheet0_Index + Sheet1_Index).ToString()), Data = _parts };
                }
                return new ResponseInfo { Status = -100, Message = string.Format("未能获取Excel数据，请检查是否存在零件名！"), Data = _parts };
            }
            catch (Exception ex)
            {
                return new ResponseInfo { Status = -99, Message = "读取Excel文件内容出错！原因：" + ex.ToString(), Data = new List<Part>() };
            }
            #endregion
        }
        /// <summary>
        /// 根据Excel列类型获取列的值
        /// </summary>
        /// <param name="cell">Excel列</param>
        /// <returns></returns>
        private static string GetCellValue(ICell cell)
        {
            if (cell == null)
                return string.Empty;
            switch (cell.CellType)
            {
                case CellType.Blank:
                    return string.Empty;
                case CellType.Boolean:
                    return cell.BooleanCellValue.ToString();
                case CellType.Error:
                    return cell.ErrorCellValue.ToString();
                case CellType.Numeric:
                case CellType.Unknown:
                default:
                    return cell.ToString();//This is a trick to get the correct value of the cell. NumericCellValue will return a numeric value no matter the cell value is a date or a number
                case CellType.String:
                    return cell.StringCellValue;
                case CellType.Formula:
                    try
                    {
                        XSSFFormulaEvaluator e = new XSSFFormulaEvaluator(cell.Sheet.Workbook);
                        e.EvaluateInCell(cell);
                        return cell.ToString();
                    }
                    catch
                    {
                        return cell.NumericCellValue.ToString();
                    }
            }
        }
        #endregion
        /// <summary>
        /// Git Test
        /// </summary>
        public void GitTest()
        {
            
        }
    }
}