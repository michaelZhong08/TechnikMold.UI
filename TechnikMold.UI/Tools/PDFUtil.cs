using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.IO;
using System.Text;
using System.Net;

using iTextSharp.text;
using iTextSharp.text.pdf;
using iTextSharp.tool.xml;

namespace MoldManager.WebUI.Tools
{
    public static  class PDFUtil
    {
        ////Create PDF Format PR
        //public static MemoryStream CreatePR(string Server, int PurchaseRequestID)
        //{
        //    string _path = Server + "/Purchase/PRForm?PurchaseRequestID=" + PurchaseRequestID;
        //    WebClient _wc = new WebClient();
        //    _wc.Headers.Add("Accept: */*");
        //    _wc.Headers.Add("User-Agent: Mozilla/4.0 (compatible; MSIE 7.0; Windows NT 5.1; Trident/4.0; .NET4.0E; .NET4.0C; InfoPath.2; .NET CLR 2.0.50727; .NET CLR 3.0.04506.648; .NET CLR 3.5.21022; .NET CLR 3.0.4506.2152; .NET CLR 3.5.30729; SE 2.X MetaSr 1.0)");
        //    _wc.Headers.Add("Accept-Language: zh-cn");
        //    _wc.Headers.Add("Content-Type: multipart/form-data");
        //    _wc.Headers.Add("Accept-Encoding: gzip, deflate");
        //    _wc.Headers.Add("Cache-Control: no-cache");
        //    _wc.Encoding = Encoding.UTF8;
        //    string _content = _wc.DownloadString(_path);
        //    //string _content = "<html><body>aaa</body></html>";
            

        //    MemoryStream ms = new MemoryStream(Html2PDF(_content));

        //    return ms;
        //}

        public static MemoryStream QRAttachmen(string Server , int PurchaseRequestID){
            byte[] _prDoc = GetQR(Server, PurchaseRequestID);
            return new MemoryStream(_prDoc);
        }

        public static byte[] GetQR(string server, int QuotationRequestID)
        {
            HttpWebRequest _request = null;
            HttpWebResponse _response = null;
            //NetworkCredential _cred = new NetworkCredential("Le Chunming", "1qaz@WSX", "LeChunming-PC");
            string content = "";
            try
            {
                _request = (HttpWebRequest)WebRequest.Create(server + "/Purchase/QRForm?QuotationRequestID=" + QuotationRequestID);
                _request.PreAuthenticate = true;
                NetworkCredential _cred = CredentialCache.DefaultNetworkCredentials;
                _request.Credentials = _cred;
                _request.Timeout = 10000;
                _request.AllowAutoRedirect = false;
                _response = (HttpWebResponse)_request.GetResponse();
                if (_response.ToString() != "")
                {
                    Stream streamReceive = _response.GetResponseStream();
                    Encoding encoding = Encoding.GetEncoding("UTF-8");//乱码处理
                    StreamReader streamReader = new StreamReader(streamReceive, encoding);
                    content = streamReader.ReadToEnd();
                }
            }
            catch
            {
                content= "";
            }
            return Html2PDF(content);
        }


        public static byte[] GetPO(string server, int PurchaseOrderID)
        {
            HttpWebRequest _request = null;
            HttpWebResponse _response = null;
            //NetworkCredential _cred = new NetworkCredential("Le Chunming", "1qaz@WSX", "LeChunming-PC");
            string content = "";
            //try
            //{
                _request = (HttpWebRequest)WebRequest.Create(server + "/Purchase/PRForm?PurchaseOrderID=" + PurchaseOrderID);
                _request.PreAuthenticate = true;
                NetworkCredential _cred = CredentialCache.DefaultNetworkCredentials;
                _request.Credentials = _cred;
                _request.Timeout = 10000;
                _request.AllowAutoRedirect = false;
                _response = (HttpWebResponse)_request.GetResponse();
                if (_response.ToString() != "")
                {
                    Stream streamReceive = _response.GetResponseStream();
                    Encoding encoding = Encoding.GetEncoding("UTF-8");//乱码处理
                    StreamReader streamReader = new StreamReader(streamReceive, encoding);
                    content = streamReader.ReadToEnd();
                }
            //}
            //catch
            //{
            //    content = "";
            //}
            return Html2PDF(content);
        }

        //public static string WebResult(string Server, int PurchaseRequestID)
        //{
        //    string _path = "/Purchase/PRForm?PurchaseRequestID=" + PurchaseRequestID;
        //    WebClient _wc = new WebClient();
        //    _wc.Encoding=Encoding.UTF8;
        //    string _content = _wc.DownloadString(_path);
        //    return _content;
        //}

        private static  byte[] Html2PDF(string Content)
        {
            if (string.IsNullOrEmpty(Content))
            {
                return null;
            }
            MemoryStream oStream = new MemoryStream();
            byte[] data = Encoding.UTF8.GetBytes(Content);
            MemoryStream iStream = new MemoryStream(data);
            Document doc = new Document(PageSize.A4);
            
            PdfWriter writer = PdfWriter.GetInstance(doc, oStream);
            PdfDestination pdfDest = new PdfDestination(PdfDestination.XYZ, 0, doc.PageSize.Height, 1f);
            doc.Open();
            try
            {
                //doc.Add(_image);
                XMLWorkerHelper.GetInstance().ParseXHtml(writer, doc, iStream, null, Encoding.UTF8, new UnicodeFontFactory());
                
                PdfAction action = PdfAction.GotoLocalPage(1, pdfDest, writer);
                writer.SetOpenAction(action);
                
                doc.Close();
                
            }
            catch(Exception ex)
            {
                throw ex;
            }
            finally
            {
                iStream.Close();
                oStream.Close();
            }
           
            
            return oStream.ToArray();
        }
    }
}