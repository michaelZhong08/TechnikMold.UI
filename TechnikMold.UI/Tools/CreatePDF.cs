using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using iTextSharp.text;
using iTextSharp.text.pdf;
using iTextSharp.tool.xml;
using System.IO;
using System.Text;
using System.Net;

namespace MoldManager.WebUI.Tools
{
    public class CreatePDF
    {
        private string _content;
        public  CreatePDF(string Content){
            WebClient _wc = new WebClient();

            _content = _wc.DownloadString("http://localhost:62363/Purchase/PRForm?PurchaseRequestID=2");
        }

        public byte[] ConvertHtmlToPDF()
        {
            if (string.IsNullOrEmpty(_content))
            {
                return null;
            }
            MemoryStream oStream = new MemoryStream();
            byte[] data = Encoding.UTF8.GetBytes(_content);
            MemoryStream iStream = new MemoryStream(data);
            Document doc = new Document();
            PdfWriter writer = PdfWriter.GetInstance(doc, oStream);
            PdfDestination pdfDest = new PdfDestination(PdfDestination.XYZ, 0, doc.PageSize.Height, 1f);
            doc.Open();
            XMLWorkerHelper.GetInstance().ParseXHtml(writer, doc, iStream, null, Encoding.UTF8, new UnicodeFontFactory());
            PdfAction action = PdfAction.GotoLocalPage(1, pdfDest, writer);
            writer.SetOpenAction(action);
            doc.Close();
            iStream.Close();
            oStream.Close();
            return oStream.ToArray();
        }

        public MemoryStream PDFStream()
        {
            return new MemoryStream(ConvertHtmlToPDF());
        }
    }
}