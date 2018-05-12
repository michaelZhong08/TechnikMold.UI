using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TechnikSys.MoldManager.Domain.Abstract;
using TechnikSys.MoldManager.Domain.Entity;

namespace TechnikSys.MoldManager.Domain.Concrete
{
    public class QuotationFileRepository : IQuotationFileRepository
    {
        private EFDbContext _context = new EFDbContext();

        public IQueryable<QuotationFile> QuotationFiles
        {
            get { return _context.QuotationFiles.Where(f => f.Enabled == true); }
        }
 

        public int Save(QuotationFile File)
        {
            if (File.QuotationFileID == 0)
            {
                _context.QuotationFiles.Add(File);
            }
            else
            {
                QuotationFile _dbEntry = _context.QuotationFiles.Find(File.QuotationFileID);
                if (_dbEntry != null)
                {
                    _dbEntry.QuotationRequestID = File.QuotationRequestID;
                    _dbEntry.SupplierID = File.SupplierID;
                    _dbEntry.CreateDate = DateTime.Now;
                    _dbEntry.Enabled = File.Enabled;
                    _dbEntry.FileName = File.FileName;
                }
            }
            _context.SaveChanges();
            return File.QuotationFileID;
        }

        public void Delete(int QuotationFileID)
        {
            QuotationFile _file = _context.QuotationFiles.Find(QuotationFileID);
            _file.Enabled = false;
            _context.SaveChanges();
        }

        public List<QuotationFile> QueryByQuotationRequest(int QuotationRequestID, int SupplierID = 0)
        {
            List<QuotationFile> _files = _context.QuotationFiles
                .Where(f=>f.Enabled==true).Where(f => f.QuotationRequestID == QuotationRequestID).ToList();
            if (SupplierID > 0)
            {
                _files = _files.Where(f => f.SupplierID == SupplierID).ToList();
            }
            return _files;
        }



        public QuotationFile QueryByID(int QuotationFileID)
        {
            return _context.QuotationFiles.Find(QuotationFileID);
        }
    }
}
