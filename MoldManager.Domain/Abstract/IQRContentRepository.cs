using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TechnikSys.MoldManager.Domain.Entity;

namespace TechnikSys.MoldManager.Domain.Abstract
{
    public interface IQRContentRepository
    {
        IQueryable<QRContent> QRContents { get; }

        int Save(QRContent QRContent);

        void Delete(int QRContentID);

        void DeleteByQRID(int QRID);

        QRContent GetByID(int QRContentID);

        IEnumerable<QRContent> QueryByQRID(int QuotationRequestID);

        IEnumerable<QRContent> Query(string Keyword);

    }
}
