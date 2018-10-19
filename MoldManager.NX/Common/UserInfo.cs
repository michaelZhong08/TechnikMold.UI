using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TechnikSys.MoldManager.Domain.Entity;
using Newtonsoft.Json;

namespace TechnikSys.MoldManager.NX.Common
{
    public class UserInfo
    {
        WebServer _server;
        public UserInfo(WebServer Server)
        {
            _server = Server;
        }


        public int GetUserID(string UserName)
        {
            string _url = "/User/GetUserByName?UserName=" + UserName;

            string data = _server.ReceiveStream(_url);

            User _user = JsonConvert.DeserializeObject<User>(data);

            try
            {
                return _user.UserID;
            }
            catch
            {
                return 0;
            }
            
        }
    }
}
