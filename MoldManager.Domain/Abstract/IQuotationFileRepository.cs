using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TechnikSys.MoldManager.Domain.Entity;

namespace TechnikSys.MoldManager.Domain.Abstract
{
    public interface IQuotationFileRepository
    {
        IQueryable<QuotationFile> QuotationFiles { get; }
        int Save(QuotationFile File);

        void Delete(int QuotationFileID);

        List<QuotationFile> QueryByQuotationRequest(int QuotationRequestID, int SupplierID = 0);

        QuotationFile QueryByID(int QuotationFileID);
    }
}
