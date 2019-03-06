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
using TechnikMold.UI.Models;
using System.Configuration;

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
            string content = "";
            HttpWebRequest _request = null;
            HttpWebResponse _response = null;
            string acc = ConfigurationManager.AppSettings["ServerAcc"].ToString();
            string pwd = ConfigurationManager.AppSettings["ServerPwd"].ToString();
            NetworkCredential _cred = new NetworkCredential(acc, pwd);
            //NetworkCredential _cred = CredentialCache.DefaultNetworkCredentials;
            try
            {
                _request = (HttpWebRequest)WebRequest.Create(server + "/Purchase/QRForm?QuotationRequestID=" + QuotationRequestID);
                //_request.PreAuthenticate = true;
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
            catch(Exception ex)
            {
                string WebLogPath = HttpContext.Current.Server.MapPath("~/Log/");
                if (!Directory.Exists(WebLogPath))
                {
                    DirectoryInfo dir = new DirectoryInfo(WebLogPath);
                    dir.Create();
                }
                string logPath = WebLogPath + "QR(PDF)生成_" + QuotationRequestID.ToString() + "_" + DateTime.Now.ToString("yyMMddHHmm") + ".txt";
                Toolkits.WriteLog(logPath, "用户名:" + _cred.UserName + ";密码:" + _cred.Password + "\r\n" + ex.Message.ToString());
                content = "";
            }
            return Html2PDF(content,"QR");
        }

        public static byte[] GetPO(string server, int PurchaseOrderID)
        {
            HttpWebRequest _request = null;
            HttpWebResponse _response = null;
            string acc = ConfigurationManager.AppSettings["ServerAcc"].ToString();
            string pwd = ConfigurationManager.AppSettings["ServerPwd"].ToString();
            NetworkCredential _cred = new NetworkCredential(acc, pwd);
            //NetworkCredential _cred = CredentialCache.DefaultNetworkCredentials;
            //Uri uri = new Uri(server);
            //ICredentials credentials = CredentialCache.DefaultNetworkCredentials;
            //NetworkCredential _cred = credentials.GetCredential(uri, "Basic");

            string content = "";
            try
            {
                _request = (HttpWebRequest)WebRequest.Create(server + "/Purchase/PRForm?PurchaseOrderID=" + PurchaseOrderID);
                _request.PreAuthenticate = true;
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
            catch(Exception ex)
            {
                string WebLogPath = HttpContext.Current.Server.MapPath("~/Log/");
                if (!Directory.Exists(WebLogPath))
                {
                    DirectoryInfo dir = new DirectoryInfo(WebLogPath);
                    dir.Create();
                }
                string logPath = WebLogPath + "PO(PDF)生成_" + PurchaseOrderID.ToString() + "_" + DateTime.Now.ToString("yyMMddHHmm") + ".txt";
                Toolkits.WriteLog(logPath, "用户名:"+ _cred.UserName+";密码:"+ _cred.Password+"\r\n"+ex.Message.ToString());
                content = "";
            }
            return Html2PDF(content,"PO");
        }

        //public static string WebResult(string Server, int PurchaseRequestID)
        //{
        //    string _path = "/Purchase/PRForm?PurchaseRequestID=" + PurchaseRequestID;
        //    WebClient _wc = new WebClient();
        //    _wc.Encoding=Encoding.UTF8;
        //    string _content = _wc.DownloadString(_path);
        //    return _content;
        //}

        private static  byte[] Html2PDF(string Content,string BussinessType)
        {
            //return null;
            if (string.IsNullOrEmpty(Content))
            {
                return null;
            }
            MemoryStream oStream = new MemoryStream();
            byte[] data = Encoding.UTF8.GetBytes(Content);
            MemoryStream iStream = new MemoryStream(data);
            Document doc = new Document(PageSize.A4,25,25,100,50);
            
            PdfWriter writer = PdfWriter.GetInstance(doc, oStream);
            PdfDestination pdfDest = new PdfDestination(PdfDestination.XYZ, 0, doc.PageSize.Height, 1f);
            doc.Open();
            #region 页眉页脚
            switch (BussinessType)
            {
                case "PO":
                    writer.PageEvent = new HeaderAndFooterEvent();
                    HeaderAndFooterEvent.PAGE_NUMBER = true;
                    HeaderAndFooterEvent.tpl = writer.DirectContent.CreateTemplate(0, 0);
                    break;
                case "QR":
                    break;
            }
            #endregion
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
    /// <summary>
    /// 重写PDF生成相关事件
    /// </summary>
    public class HeaderAndFooterEvent : PdfPageEventHelper, IPdfPageEvent
    {
        public static PdfTemplate tpl = null; //模版
        public static bool PAGE_NUMBER = false; //为True时就生成 页眉和页脚

        //重写 关闭一个页面时
        public override void OnEndPage(PdfWriter writer, Document document)
        {
            if (PAGE_NUMBER)
            {
                Image gif = Image.GetInstance(HttpContext.Current.Server.MapPath("~/Images") + "/logo.png");
                //document.Add(gif);

                //BaseFont baseFont = BaseFont.CreateFont("STSong-Light", BaseFont.IDENTITY_H, BaseFont.NOT_EMBEDDED);
                BaseFont baseFont = BaseFont.CreateFont("C:/Windows/Fonts/SIMYOU.TTF", BaseFont.IDENTITY_H, BaseFont.NOT_EMBEDDED);

                Phrase header1 = new Phrase("Purchase Order", new Font(baseFont, 20, 1, BaseColor.BLUE));
                Phrase header2 = new Phrase("采 购 订 单", new Font(baseFont, 20, 1, BaseColor.BLUE));
                Phrase header3 = new Phrase("生成日期:" + DateTime.Now.ToString("yyyy/MM/dd"), new Font(baseFont, 13));
                Phrase header4 = new Phrase("PO单号:TO1812178", new Font(baseFont, 13));
                //Phrase header5 = new Phrase(1,Chunk.IMAGE);
                //header5.Add(gif);
                //document.
                //Phrase footer1 = new Phrase("NYMOI Analyzer 4.0 ");
                Phrase footer2 = new Phrase("Page " + writer.PageNumber);
                #region 添加图片
                PdfContentByte cb = writer.DirectContent;
                gif.SetAbsolutePosition(25, document.Top + 40);
                gif.ScaleToFit(90, 35);
                cb.AddImage(gif);
                #endregion
                //模版 显示总共页数
                cb.AddTemplate(tpl, 0, 0); //调节模版显示的位置

                //页眉显示的位置
                //ColumnText.ShowTextAligned(cb, Element.ALIGN_LEFT, header5, Convert.ToInt32(document.Left) + 10, document.Top + 40, 0);
                ColumnText.ShowTextAligned(cb, Element.ALIGN_CENTER, header1, Convert.ToInt32(document.Right / 2) + 10, document.Top + 60, 0);
                ColumnText.ShowTextAligned(cb, Element.ALIGN_CENTER, header2, Convert.ToInt32(document.Right / 2) + 10, document.Top + 30, 0);
                ColumnText.ShowTextAligned(cb, Element.ALIGN_RIGHT, header4, Convert.ToInt32(document.Right) , document.Top + 25, 0);
                ColumnText.ShowTextAligned(cb, Element.ALIGN_RIGHT, header3, Convert.ToInt32(document.Right) , document.Top + 10, 0);

                //页脚显示的位置
                //ColumnText.ShowTextAligned(cb, Element.ALIGN_CENTER, footer1, 30 + document.LeftMargin, document.Bottom, 0);
                ColumnText.ShowTextAligned(cb, Element.ALIGN_CENTER, footer2, document.Right - 60 + document.LeftMargin, 20, 0);

                
            }
        }

        //重写 打开一个新页面时
        public override void OnStartPage(PdfWriter writer, Document document)
        {
            if (PAGE_NUMBER)
            {
                writer.PageCount = writer.PageNumber - 1;
            }
        }
        //关闭PDF文档时发生该事件
        public override void OnCloseDocument(PdfWriter writer, Document document)
        {
            //BaseFont bf = BaseFont.CreateFont(Path.GetDirectoryName(Application.ExecutablePath) + "\\Fonts\\msyh.ttf", BaseFont.IDENTITY_H, BaseFont.NOT_EMBEDDED);
            tpl.BeginText();
            //tpl.SetFontAndSize(, 16); //生成的模版的字体、颜色
            //tpl.ShowText((writer.PageNumber - 2).ToString()); //模版显示的内容
            tpl.EndText();
            tpl.ClosePath();
        }
    }
}