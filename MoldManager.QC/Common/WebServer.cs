using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Net;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Net.NetworkInformation;
using System.Web;

namespace MoldManager.QC.Common
{
    public class WebServer
    {
        private string _serverName { get; set; }
        private string _port { get; set; }
        private NetworkCredential Credential { get; set; }

        public WebServer(string ServerName, string Port, string UserName, string Password)
        {
            _serverName = ServerName;
            _port = Port;
            Credential = new NetworkCredential(UserName, Password);
        }

        public string ServerURL
        {
            get
            {
                return "http://" + _serverName + ":" + _port;
            }
        }


        public string ReceiveStream(string Url)
        {
            string _cUrl = ServerURL + Url;
            HttpWebRequest _request = (HttpWebRequest)WebRequest.Create(_cUrl);

            //NetworkCredential _credential = new NetworkCredential("Administrator", "catia_4");
            _request.Credentials = Credential;
            HttpWebResponse _response = (HttpWebResponse)_request.GetResponse();

            Stream _stream = _response.GetResponseStream();
            UTF8Encoding _encoding = new UTF8Encoding();
            StreamReader _result = new StreamReader(_stream, _encoding);
            return _result.ReadToEnd();
        }

        private string SendStream(string Url, string Data)
        {
            string _cURL = ServerURL + Url;
            Uri _uri = new Uri(_cURL);
            HttpWebRequest _request = (HttpWebRequest)WebRequest.Create(_uri);
            //NetworkCredential _credential = new NetworkCredential("Administrator", "catia_4");
            _request.Credentials = Credential;
            _request.Method = "Post";
            _request.ContentType = "application/x-www-form-urlencoded";
            _request.Timeout = 50000;
            //_request.ContentLength = Data.Length;
            using (Stream writeStream = _request.GetRequestStream())
            {
                UTF8Encoding encoding = new UTF8Encoding();
                byte[] bytes = encoding.GetBytes(Data);
                writeStream.Write(bytes, 0, bytes.Length);
                writeStream.Close();
            }

            //string _result = string.Empty;

            //using (HttpWebResponse response = (HttpWebResponse)_request.GetResponse())
            //{
            //    using (Stream responseStream = response.GetResponseStream())
            //    {
            //        using (StreamReader readStream = new StreamReader(responseStream, Encoding.UTF8))
            //        {
            //            _result = readStream.ReadToEnd();
            //        }
            //    }
            //}
            string _result = string.Empty;

            HttpWebResponse response;
            try
            {
                response = (HttpWebResponse)_request.GetResponse();
            }
            catch (WebException ex)
            {
                response = (HttpWebResponse)ex.Response;
            }
            StreamReader readStream = new StreamReader(response.GetResponseStream(), Encoding.UTF8);
            _result = readStream.ReadToEnd();

            return _result;
        }

        public string SendObject(string Url, String Name, Object Data)
        {
            return SendStream(Url, Name + "=" + JsonConvert.SerializeObject(Data));
        }
        public string ChkNetConntect()
        {
            bool online = false;
            bool ChkAcc = false;
            string res = string.Empty;
            #region Ping IP地址
            try
            {
                //循环遍历 Ping程序
                for (int i = 1; i < 5; i++)
                {
                    if (NetPing())
                    {
                        online = true;
                        break;
                    }
                }
                if (!online)
                {
                    res = "地址:" + _serverName + " 不通！";
                }
            }
            catch (Exception ex)
            {
                res = ex.Message;
            }
            #endregion 
            if (string.IsNullOrEmpty(res))
            {
                #region 验证用户名密码
                HttpWebResponse response;
                try
                {
                    string arg = "testData";
                    string str = "url test data";
                    string Data = arg + "=" + HttpUtility.UrlEncode(JsonConvert.SerializeObject(str));
                    string _cURL = ServerURL + "/Administrator/AcceptClientData";
                    Uri _uri = new Uri(_cURL);
                    HttpWebRequest _request = (HttpWebRequest)WebRequest.Create(_uri);
                    _request.Credentials = Credential;
                    _request.Method = "Post";
                    _request.ContentType = "application/x-www-form-urlencoded";
                    _request.Timeout = 10000;
                    using (Stream writeStream = _request.GetRequestStream())
                    {
                        UTF8Encoding encoding = new UTF8Encoding();
                        byte[] bytes = encoding.GetBytes(Data);
                        writeStream.Write(bytes, 0, bytes.Length);
                        writeStream.Close();
                    }
                    string _result = string.Empty;
                    response = (HttpWebResponse)_request.GetResponse();
                }
                catch (WebException ex)
                {
                    response = (HttpWebResponse)ex.Response;
                    res = "401Error:用户名或密码不正确！";//ex.Message;
                }
                #endregion
            }
            return res;
        }
        public bool NetPing()
        {
            bool online = false;
            #region Ping
            Ping ping = new Ping();
            int timeout = 1000;
            string data = "sendData:123";
            byte[] buffer = Encoding.ASCII.GetBytes(data);
            PingReply pingReply = ping.Send(_serverName, timeout, buffer);
            #endregion
            if (pingReply.Status == IPStatus.Success)
            {
                online = true;
            }
            return online;
        }
    }
}
