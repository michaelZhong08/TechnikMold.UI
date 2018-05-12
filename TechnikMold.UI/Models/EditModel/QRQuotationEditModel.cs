using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using TechnikSys.MoldManager.Domain.Entity;

namespace MoldManager.WebUI.Models.EditModel
{
    public class QRQuotationEditModel
    {
        private QRContent _qrContent;
        private QRQuotation _prQuotation;
        public QRQuotationEditModel(QRContent QRContent, QRQuotation QRQuotation=null)
        {
            _qrContent = QRContent;
            if (QRQuotation != null)
            {
                _prQuotation = QRQuotation;
            }
            else
            {
                _prQuotation = new QRQuotation();
            }
            
        }

        public string PartName
        {
            get{ return _qrContent.PartName; }
        }

        public string PartNumber
        {
            get { return _qrContent.PartNumber; }
        }

        public string Specification
        {
            get { return _qrContent.PartSpecification; }
        }

        public int Quantity
        {
            get { return _qrContent.Quantity; }
        }

        public int QRContentID
        {
            get { return _qrContent.QRContentID; }
        }

        public double UnitPrice
        {
            get { return _prQuotation.UnitPrice; }
        }

        public int QuotationRequestID
        {
            get { return _qrContent.QuotationRequestID; }
        }

        public int QRQuotationID
        {
            get
            {
                if (_prQuotation == null)
                {
                    return 0;
                }
                else
                {
                    return _prQuotation.QRQuotationID;
                }
            }
        }
    }
}