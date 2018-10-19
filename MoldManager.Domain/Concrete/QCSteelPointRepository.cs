using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TechnikSys.MoldManager.Domain.Abstract;
using TechnikSys.MoldManager.Domain.Entity;

namespace TechnikSys.MoldManager.Domain.Concrete
{
    public class QCSteelPointRepository:IQCSteelPointRepository
    {
        private EFDbContext _context = new EFDbContext();

        public IQueryable<QCSteelPoint> QCSteelPoints
        {
            get { return _context.QCSteelPoints; }
        }

        public IEnumerable<QCSteelPoint> QueryByName(string Name, int Version)
        {
            IEnumerable<QCSteelPoint> _qcSteelPoints = _context.QCSteelPoints.Where(q => q.PartName == Name)
                .Where(q => q.Rev < Version).
                Where(q => q.Enabled == true);
            return _qcSteelPoints;
        }

        public QCSteelPoint QueryStatus(string Name, int Version)
        {
            QCSteelPoint _qcSteelPoint = _context.QCSteelPoints.Where(q => q.PartName == Name)
                .Where(q => q.Rev == Version).Where(q => q.Enabled == true).FirstOrDefault();
            return _qcSteelPoint;
        }

        public IEnumerable<QCSteelPoint> QueryByFullPartName(string FullPartName)
        {
            IEnumerable<QCSteelPoint> _points = _context.QCSteelPoints.Where(q => q.FullPartName == FullPartName);
            return _points;
        }

        public QCSteelPoint QueryByNameVersion(string PartName, int Version)
        {
            QCSteelPoint _qcSteelPoint = _context.QCSteelPoints.Where(q => q.PartName == PartName)
                .Where(q => q.Rev == Version).Where(q => q.Enabled == true).FirstOrDefault();
            return _qcSteelPoint;
        }



        public int Save(QCSteelPoint QCSteelPoint)
        {
            QCSteelPoint _dbEntry = null;
            bool newItem = false;
            if (QCSteelPoint.QCSteelPointID == 0)
            {
                _dbEntry = _context.QCSteelPoints.Where(q => q.PartName == QCSteelPoint.PartName)
                    .Where(q => q.Rev == QCSteelPoint.Rev).Where(q => q.Enabled == true).FirstOrDefault();
                if (_dbEntry == null)
                {
                    newItem = true;
                    QCSteelPoint.CreateDate = DateTime.Now;
                    QCSteelPoint.DeleteDate = new DateTime(1900, 1, 1);
                    QCSteelPoint.DeleteBy = "";
                    QCSteelPoint.Enabled = true;
                    _context.QCSteelPoints.Add(QCSteelPoint);
                }
                else
                {
                    newItem = false;
                    _dbEntry.OldID=QCSteelPoint.OldID;
                    _dbEntry.PartName=QCSteelPoint.PartName;
                    _dbEntry.Rev=QCSteelPoint.Rev;
                    _dbEntry.Csys=QCSteelPoint.Csys;
                    _dbEntry.MoldName=QCSteelPoint.MoldName;
                    _dbEntry.FullPartName=QCSteelPoint.FullPartName;
                    _dbEntry.XYZName=QCSteelPoint.XYZName;
                    _dbEntry.Clearance=QCSteelPoint.Clearance;
                    _dbEntry.Status=QCSteelPoint.Status;
                    _dbEntry.PartName3D=QCSteelPoint.PartName3D;
                    _dbEntry.CreateDate=DateTime.Now;
                    _dbEntry.CreateComputer=QCSteelPoint.CreateComputer;
                    _dbEntry.CreateBy=QCSteelPoint.CreateBy;
                    _dbEntry.DeleteBy="";
                    _dbEntry.DeleteDate=new DateTime(1900,1,1);
                    _dbEntry.Enabled=true;
                }
            }
            else
            {
                _dbEntry = _context.QCSteelPoints.Find(QCSteelPoint.QCSteelPointID);
                if (_dbEntry != null)
                {
                    newItem = false;
                    _dbEntry.OldID=QCSteelPoint.OldID;
                    _dbEntry.PartName=QCSteelPoint.PartName;
                    _dbEntry.Rev=QCSteelPoint.Rev;
                    _dbEntry.Csys=QCSteelPoint.Csys;
                    _dbEntry.MoldName=QCSteelPoint.MoldName;
                    _dbEntry.FullPartName=QCSteelPoint.FullPartName;
                    _dbEntry.XYZName=QCSteelPoint.XYZName;
                    _dbEntry.Clearance=QCSteelPoint.Clearance;
                    _dbEntry.Status=QCSteelPoint.Status;
                    _dbEntry.PartName3D=QCSteelPoint.PartName3D;
                    _dbEntry.CreateDate=DateTime.Now;
                    _dbEntry.CreateComputer=QCSteelPoint.CreateComputer;
                    _dbEntry.CreateBy=QCSteelPoint.CreateBy;
                    _dbEntry.DeleteBy="";
                    _dbEntry.DeleteDate=new DateTime(1900,1,1);
                    _dbEntry.Enabled=true;

                }
            }
            _context.SaveChanges();
            if (newItem)
            {
                return QCSteelPoint.QCSteelPointID;
            }
            else
            {
                return _dbEntry.QCSteelPointID;
            }
        }
    }
}
