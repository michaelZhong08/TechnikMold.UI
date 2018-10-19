using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TechnikSys.MoldManager.Domain.Abstract;
using TechnikSys.MoldManager.Domain.Entity;

namespace TechnikSys.MoldManager.Domain.Concrete
{
    public class QCPointPorgramRepository:IQCPointProgramRepository
    {
        private EFDbContext _context = new EFDbContext();
        public IQueryable<QCPointProgram> QCPointPrograms
        {
            get { return _context.QCPointPrograms; }
        }

        public QCPointProgram QueryByPart3D(string ELE3DName, int Version)
        {
            QCPointProgram _program = _context.QCPointPrograms
                .Where(p => p.Part3D == ELE3DName)
                .Where(p => p.Rev == Version)
                .FirstOrDefault();
            return null;
        }


        public int Save(QCPointProgram QCPointProgram)
        {
            bool _newItem = false;
            QCPointProgram _dbEntry=null;
            if (QCPointProgram.QCPointProgramID == 0)
            {
                _dbEntry = _context.QCPointPrograms.Where(q => q.ElectrodeName == QCPointProgram.ElectrodeName)
                    .Where(q => q.Rev == QCPointProgram.Rev).Where(q => q.Enabled == QCPointProgram.Enabled).FirstOrDefault();

                if (_dbEntry == null)
                {
                    _newItem = true;
                    QCPointProgram.CreateDate = DateTime.Now;
                    _context.QCPointPrograms.Add(QCPointProgram);
                }
                else
                {
                    _dbEntry.ElectrodeName = QCPointProgram.ElectrodeName;
                    _dbEntry.Rev = QCPointProgram.Rev;
                    _dbEntry.LatestFlat = QCPointProgram.LatestFlat;
                    _dbEntry.Clearance = QCPointProgram.Clearance;
                    _dbEntry.X = QCPointProgram.X;
                    _dbEntry.Y = QCPointProgram.Y;
                    _dbEntry.PartPath = QCPointProgram.PartPath;
                    _dbEntry.XYZFlieName = QCPointProgram.XYZFlieName;
                    _dbEntry.PartName3D = QCPointProgram.PartName3D;
                    _dbEntry.Part3D = QCPointProgram.Part3D;
                    _dbEntry.Part3DRev = QCPointProgram.Part3DRev;
                    _dbEntry.CreateBy = QCPointProgram.CreateBy;
                    _dbEntry.CreateDate = DateTime.Now;
                    _dbEntry.CreateComputer = QCPointProgram.CreateComputer;
                    _dbEntry.Enabled = QCPointProgram.Enabled;
                    _dbEntry.OldID = QCPointProgram.OldID;
                }
            }
            else
            {
                _dbEntry = _context.QCPointPrograms.Find(QCPointProgram.QCPointProgramID);
                _dbEntry.ElectrodeName = QCPointProgram.ElectrodeName;
                _dbEntry.Rev = QCPointProgram.Rev;
                _dbEntry.LatestFlat = QCPointProgram.LatestFlat;
                _dbEntry.Clearance = QCPointProgram.Clearance;
                _dbEntry.X = QCPointProgram.X;
                _dbEntry.Y = QCPointProgram.Y;
                _dbEntry.PartPath = QCPointProgram.PartPath;
                _dbEntry.XYZFlieName = QCPointProgram.XYZFlieName;
                _dbEntry.PartName3D = QCPointProgram.PartName3D;
                _dbEntry.Part3D = QCPointProgram.Part3D;
                _dbEntry.Part3DRev = QCPointProgram.Part3DRev;
                _dbEntry.CreateBy = QCPointProgram.CreateBy;
                _dbEntry.CreateDate = DateTime.Now;
                _dbEntry.CreateComputer = QCPointProgram.CreateComputer;
                _dbEntry.Enabled = QCPointProgram.Enabled;
                _dbEntry.OldID = QCPointProgram.OldID;
            }
            _context.SaveChanges();
            if (_newItem)
            {
                return QCPointProgram.QCPointProgramID;
            }
            else
            {
                return _dbEntry.QCPointProgramID;
            }
        }
    }
}
