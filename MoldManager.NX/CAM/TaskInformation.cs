using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TechnikSys.MoldManager.Domain.Entity;
using TechnikSys.MoldManager.NX.Common;

namespace TechnikSys.MoldManager.NX.CAM
{
    public class TaskInformation
    {
        private WebServer _server;

        public TaskInformation(WebServer Server)
        {
            _server = Server;
        }

        public TaskInformation(string ServerName, string Port)
        {
            _server = new WebServer(ServerName, Port, "Le Chunming", "1qaz@WSX");
        }

        public bool TaskReleased(string TaskName, int Version)
        {
            string _url = "/Task/TaskReleased?TaskName=" + TaskName + "&Version=" + Version;
            bool _result = Convert.ToBoolean(_server.ReceiveStream(_url));
            return _result;
        }
    }
}
