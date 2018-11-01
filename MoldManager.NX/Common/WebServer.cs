/*
 * Create By:lechun1
 * 
 * Description:
 * 
 */



using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Net;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Net.Security;
using System.Web;
using System.Net.NetworkInformation;

namespace TechnikSys.MoldManager.NX.Common
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
            //ServicePointManager.ServerCertificateValidationCallback = new System.Net.Security.RemoteCertificateValidationCallback(CheckValidationResult);
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
            #region 异常写入
            string filePath = Directory.GetCurrentDirectory() + "\\" + "WebEx.txt";
            if (File.Exists(filePath))
                File.Delete(filePath);
            FileStream fs = new FileStream(filePath, FileMode.Create);
            string str = JsonConvert.SerializeObject(_result);
            byte[] data = System.Text.Encoding.Default.GetBytes(str);
            //开始写入
            fs.Write(data, 0, data.Length);
            //清空缓冲区、关闭流
            fs.Flush();
            fs.Close();
            #endregion

            return _result;
        }

        public string SendObject(string Url, String Name, Object Data)
        {
            //HttpUtility.UrlEncode 对post数据编码 解决特殊符号(+ %) 无法正常传递到服务器端问题
            return SendStream(Url, Name + "=" + HttpUtility.UrlEncode(JsonConvert.SerializeObject(Data)));
        }
        public bool CheckValidationResult(object sender, X509Certificate certificate, X509Chain chain, SslPolicyErrors errors)
        { // 总是接受
            return true;
        }
        public string ChkNetConntect()
        {
            bool online = false;
            bool ChkAcc = false;
            string res = string.Empty;
            #region Ping IP地址
            try
            {

                Ping ping = new Ping();
                PingReply pingReply = ping.Send(_serverName);
                if (pingReply.Status == IPStatus.Success)
                {
                    online = true;
                }
                else
                    res = "IP地址无法ping通，请检查网络连接！";

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
                    res = ex.Message;
                }
                //StreamReader readStream = new StreamReader(response.GetResponseStream(), Encoding.UTF8);
                //_result = readStream.ReadToEnd();
                //_result = JsonConvert.DeserializeObject<string>(_result);
                //if (_result == str)
                //{
                //    ChkAcc = true;
                //}

                //if (online && ChkAcc)
                //    return true;
                //return false;
                #endregion
            }
            return res;
        }
    }
}
