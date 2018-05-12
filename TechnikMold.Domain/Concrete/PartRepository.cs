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



namespace TechnikSys.MoldManager.Domain.Concrete
{
    public class PartRepository:IPartRepository
    {
        private EFDbContext _context = new EFDbContext();
        public IQueryable<Part> Parts
        {
            get {
                return _context.Parts;
            }
        }

        public int Save(Part Part)
        {
            Part _dbEntry;
            bool _newpart = false ;
            if (Part.PartID==0){
                _dbEntry = QueryByName(Part.PartNumber);
                if (_dbEntry == null)
                {
                    _newpart = true;
                    _context.Parts.Add(Part);
                }
                else
                {
                    if ((_dbEntry.FromUG==Part.FromUG)||(_dbEntry.FromUG==true))
                    {
                        _dbEntry.ProjectID = Part.ProjectID;
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
                    }
                    else
                    {
                        return -1;
                    }
                }
                
            }else{
                _dbEntry = _context.Parts.Find(Part.PartID);
                if (_dbEntry!=null){
                    _dbEntry.ProjectID = Part.ProjectID;
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
                    _dbEntry.InPurchase = Part.InPurchase;
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
            Part _part = _context.Parts.Where(p => p.PartNumber.ToLower().Contains(Name.ToLower())).FirstOrDefault();
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


        public IQueryable<Part> QueryByProject(int ProjectID)
        {
            IQueryable<Part> _parts = _context.Parts.Where(p => p.ProjectID == ProjectID);
            return _parts;
        }


        public Part QueryByID(int PartID)
        {
            Part _part = _context.Parts.Find(PartID);
            return _part;
        }


        public void DeleteExisting(string MoldNumber)
        {
            IEnumerable<Part> _parts = _context.Parts.Where(p => p.PartNumber.Contains(MoldNumber)).Where(p=>p.FromUG==true);
            foreach (Part _part in _parts)
            {
                _part.Enabled = false;
            }
            _context.SaveChanges();
        }

        public IEnumerable<Part> QueryByMoldNumber(string MoldNumber)
        {
            IEnumerable<Part> _parts = _context.Parts.Where(p => p.PartNumber.Contains(MoldNumber)).Where(p => p.Enabled == true);
            return _parts;
        }


        public IEnumerable<Part> QueryBySpecification(string Keyword)
        {
            return _context.Parts.Where(p => p.Specification.Contains(Keyword)).Take(10);
        }
    }
}
