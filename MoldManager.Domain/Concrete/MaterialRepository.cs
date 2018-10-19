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
    public class MaterialRepository:IMaterialRepository
    {
        private EFDbContext _context = new EFDbContext();
        public IQueryable<Material> Materials
        {
            get { 
                return _context.Materials; 
            }
        }

        public int Save(Material Material)
        {
            if (Material.MaterialID == 0)
            {
                _context.Materials.Add(Material);
            }
            else
            {
                Material _dbEntry = _context.Materials.Find(Material.MaterialID);
                if (_dbEntry != null)
                {
                    _dbEntry.Name = Material.Name;
                    _dbEntry.Enabled = Material.Enabled;
                }
            }
            _context.SaveChanges();
            return Material.MaterialID;
        }

        public IQueryable<Material> QueryByName(string Name)
        {
            IQueryable<Material> _materials = _context.Materials.Where(m => m.Name.Contains(Name));
            return _materials;
        }

        public int Delete(int MaterialID)
        {
            Material _material = GetMaterial(MaterialID);
            _material.Enabled = !_material.Enabled;
            _context.SaveChanges();
            return _material.MaterialID;

        }

        public Material GetMaterial(int MaterialID)
        {
            Material _material = _context.Materials.Find(MaterialID);
            return _material;
        }


        public Material GetMaterialByName(string Name)
        {
            return _context.Materials.Where(m => m.Name.ToLower() == Name.ToLower()).FirstOrDefault();
        }
    }
}
