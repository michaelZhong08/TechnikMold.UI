using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Net.Mail;
using System.IO;
using System.Net;

namespace MoldManager.WebUI.Tools
{
    public static class SendMail
    {
        //private string[] _sendTo;
        //private string _mailTitle;
        //private string _mailBody;
        //private int _mode;
        //public SendMail(string SendTo, string Title, string Body, int Mode=0)
        //{
        //    _sendTo = SendTo.Split(';');
        //    _mailTitle = Title;
        //    _mailBody = Body;
        //    _mode = Mode;
        //}

        public static bool SendQR(string Receivers, 
            int PurchaseRequestID, 
            string ServerInfo, 
            NetworkCredential MailCredential) 
        {

            string[] _receiver=Receivers.Split(';');

            MailMessage _msg = new MailMessage();

            for (int i = 0; i < _receiver.Length; i++)
            {
                try {
                    if (_receiver[i] != "") { 
                        _msg.Bcc.Add(new MailAddress(_receiver[i]));
                    }
                }
                catch
                {

                }
            }

            //Mail Subject & body
            _msg.Subject = "星诺奇采购询价单";
            _msg.Body = "";


            //Mail setting
            _msg.IsBodyHtml = false;
            _msg.SubjectEncoding = System.Text.Encoding.UTF8;
            _msg.Priority = MailPriority.Normal;
            _msg.From = new MailAddress("ricky.le@techniksys.com", "rickyle", System.Text.Encoding.UTF8);

            //Server setting
            SmtpClient _client = new SmtpClient();
            //NetworkCredential _cred = MailCredential;
            NetworkCredential _cred = new NetworkCredential("ricky.le@techniksys.com", "1qaz!QAZ");
            _client.Credentials = _cred;
            _client.Host = "smtp.ym.163.com";
            _client.EnableSsl = false;
            _client.Port = 25;

            //Mail Attachment
            Stream _attach = null;
            try
            {
                _attach = PDFUtil.QRAttachmen(ServerInfo, PurchaseRequestID);
                _msg.Attachments.Add(new Attachment(_attach, "PurchaseRequest.pdf"));
                try
                {
                    _client.Send(_msg);
                    return true;
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.Message);
                    return false;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return false;
            }        
        }


       

        //public bool Send()
        //{
        //    MailMessage _msg = new MailMessage();
        //    MailAddressCollection _address= new MailAddressCollection();
        //    for (int i = 0; i < _sendTo.Length; i++)
        //    {
                
        //        _msg.Bcc.Add(_sendTo[i]);
        //    }
            
        //    _msg.Subject = _mailTitle;
        //    _msg.Body = _mailBody;
            

        //    //Mail Attachment
        //    CreatePDF _attachment = new CreatePDF("<table><tr><td>中文字</td><td>bbb</td></tr></table>");
        //    _msg.Attachments.Add(new Attachment((System.IO.Stream)_attachment.PDFStream(), "询价单.pdf"));

        //    //Mail Settings
        //    _msg.IsBodyHtml = false;
        //    _msg.SubjectEncoding = System.Text.Encoding.UTF8;
        //    _msg.Priority = MailPriority.Normal;
        //    _msg.From = new MailAddress("ricky.le@techniksys.com", "rickyle", System.Text.Encoding.UTF8);
        //    SmtpClient _client = new SmtpClient();

        //    _client.Credentials = new System.Net.NetworkCredential("ricky.le@techniksys.com", "1qaz!QAZ");

        //    _client.Host = "smtp.ym.163.com";
        //    _client.EnableSsl = true;
        //    _client.Port = 25;
        //    try
        //    {
        //        _client.Send(_msg);
        //        return true;
        //    }
        //    catch (Exception e)
        //    {
        //        Console.WriteLine(e.Message);
        //        return false;
        //    }
        //}
    }
}