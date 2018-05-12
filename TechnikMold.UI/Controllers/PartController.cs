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
        public ActionResult Edit(Part Part, string MoldNumber)
        {
            if (Part.PartID == 0) { 
                Part.CreateDate = DateTime.Now;
            }

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
            Part.ProjectID = _projectRepository.GetLatestActiveProject(MoldNumber).ProjectID;

            //Part.BrandID = 0;
            //Part.BrandName = "";
            Part.FromUG = false;
            _partRepository.Save(Part);
            return RedirectToAction("Index", "Part", new { MoldNumber=MoldNumber});
        }

        [HttpPost]
        public int PartFromUG(String  NewPart)
        {
            Part _part = JsonConvert.DeserializeObject<Part>(NewPart);
            if (_part.PartID == 0)
            {
                _part.CreateDate = DateTime.Now;
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

            _part.ProjectID =_project==null?0: _project.ProjectID;
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
            return Json(_parts, JsonRequestBehavior.AllowGet) ;
        }

        

        public ActionResult Delete(Part Part)
        {
            _partRepository.Delete(Part.PartID);
            return RedirectToAction("Index", "Part", new { ProjectID = Part.ProjectID });
        }

        public int DeleteParts(string PartIDs)
        {
            try
            {
                string[] _partIDs = PartIDs.Split(',');

                for (int i = 0; i < _partIDs.Length; i++)
                {
                    _partRepository.Delete(Convert.ToInt32(_partIDs[i]));
                }
                return 1;
            }
            catch
            {
                return 0;
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
        public JsonResult JsonParts(string MoldNumber="")
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

        public JsonResult JsonBrands(string Keyword="")
        {
            IEnumerable<Brand> _brands = _brandRepository.Brands.Where(b=>b.Enabled==true).Where(b=>b.Name.Contains(Keyword));
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

        public JsonResult JsonMaterials(string MaterialKeyword="")
        {
            IEnumerable<Material> _materials ;
            if (MaterialKeyword != "") {
                _materials = _materialRepository.Materials.Where(m => m.Enabled == true)
                    .Where(m => m.Name.ToLower().Contains(MaterialKeyword.ToLower()))
                    .OrderBy(m=>m.Name);
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
            
            try{
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
                _partCodes = _partCodeRepository.PartCodes.Where(p=>p.Name.ToLower().Contains(Keyword.ToLower()));
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
        /// Generate json part data for UG plugin
        /// </summary>
        /// <param name="ProjectID"></param>
        /// <returns></returns>
        public JsonResult JsonUGPart(string MoldNumber, bool FromUG)
        {
            IEnumerable<Part> _parts;
            Project _project = _projectRepository.QueryByMoldNumber(MoldNumber, 1);
            if (_project != null)
            {
                int _projectID = _projectRepository.QueryByMoldNumber(MoldNumber, 1).ProjectID;
                _parts = _partRepository.Parts.Where(p => p.ProjectID == _projectID).Where(p=>p.Enabled==true);
            }
            else
            {
                _parts = _partRepository.Parts.Where(p => p.Name.Contains(MoldNumber));
            }
            _parts = _parts.Where(p=>p.FromUG==FromUG);
            
            return Json(_parts, JsonRequestBehavior.AllowGet);
        }


        public int CheckPartNumberExist(string PartNumber)
        {
            IEnumerable<Part> _parts = _partRepository.Parts
                .Where(p => p.PartNumber == PartNumber)
                .Where(p=>p.Version=="0")
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
            Part _part = _partRepository.Parts.Where(p=>p.Name ==Name).Where(p=>p.FromUG==true).FirstOrDefault();
            return Json(_part, JsonRequestBehavior.AllowGet);
        }

        public ActionResult GetPartNames(string Name)
        {
            IEnumerable<string> _names = _partRepository.Parts
                .Where(p => p.Name.Contains(Name))
                .Where(p => p.FromUG == true).OrderByDescending(p=>p.Name)
                .Select(p => p.Name);
            return Json(_names, JsonRequestBehavior.AllowGet);
        }


        public bool DeleteExisting(string MoldNumber)
        {
            try{
                _partRepository.DeleteExisting(MoldNumber);
                return true;
            }catch (Exception e){
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
                IEnumerable<Hardness> _hardness = _hardnesses.Where(h=>h.MaterialID==_m.MaterialID);
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

        public ActionResult GetMoldNumberList(string KeyWord="")
        {
            List<int> _projectIDs = _partRepository.Parts.Where(p => p.Enabled == true).Select(p => p.ProjectID).Distinct().ToList<int>();
            
            
            IEnumerable<string> _moldNumbers = _projectRepository.Projects
                .Where(p => (_projectIDs.Contains(p.ProjectID)))
                .Where(p=>p.ProjectStatus==0)
                .Select(p => p.MoldNumber).Distinct();

            if (KeyWord != "")
            {
                _moldNumbers = _moldNumbers.Where(m => m.Contains(KeyWord));
            }
            return Json(_moldNumbers, JsonRequestBehavior.AllowGet);
        }

        public ActionResult Test(){
            return View();
        }


        public ActionResult QueryBySpec(string Keyword)
        {
            IEnumerable<Part> _parts = _partRepository.QueryBySpecification(Keyword);
            return Json(_parts, JsonRequestBehavior.AllowGet);
        }


        //public int SavePartList(PartList PartList)
        //{
        //    PartList _latest = _partListRepository.QueryByMoldNumber(PartList.MoldNumber, true).FirstOrDefault();
        //    IEnumerable<Part> _oldParts = _partRepository.queryby
        //}
    }
}