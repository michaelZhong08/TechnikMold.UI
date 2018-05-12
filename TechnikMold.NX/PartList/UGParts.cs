using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TechnikSys.MoldManager.Domain.Entity;
using TechnikSys.MoldManager.Domain.Output;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using TechnikSys.MoldManager.NX.Common;


namespace TechnikSys.MoldManager.NX.PartList
{
    public class UGParts
    {

        private WebServer _server;

        public UGParts(WebServer Server)
        {
            _server = Server;
        }

        public UGParts(string ServerName, string Port)
        {
            _server = new WebServer(ServerName, Port, "Le Chunming", "1qaz@WSX");
        }
        //public  static string GetURL(string Server, string ProjectID)
        //{
        //    string _url = Server + "/Part/JsonUGPart?ProjectID=" + ProjectID;
        //    return _url;
        //}

        //public  static string SaveURL(string Server)
        //{
        //    string _url = Server + "/Part/ImportPart?Data=";
        //    return _url;
        //}

        //public  static List<Part> GetParts(string Data)
        //{
        //    JArray _jsonArray = (JArray)JsonConvert.DeserializeObject(Data);
        //    List<Part> _parts = new List<Part>();
        //    for (int i = 0; i < _jsonArray.Count; i++)
        //    {
        //        _parts.Add(JsonConvert.DeserializeObject<Part>(_jsonArray[i].ToString()));
        //    }
        //    return _parts;
        //}

        /// <summary>
        /// SEL_LatestBomPartList/SEL_LatestReleaseBomPartList
        /// </summary>
        /// <param name="MoldNo"></param>
        /// <returns></returns>
        public List<Part> GetMoldParts(string MoldNo, bool FromUG)
        {
            string _url = "/Part/JsonUGPart?MoldNumber=" + MoldNo+"&FromUG="+FromUG;
            string _data = _server.ReceiveStream(_url);
            List<Part> _parts = JsonConvert.DeserializeObject<List<Part>>(_data);
            return _parts;
        }

        public Part GetPart(string Name)
        {
            Name = Name.Replace("+", "%2B");
            string _url = "/Part/GetUGPart?Name=" + Name;
            string _data = _server.ReceiveStream(_url);
            Part _part = JsonConvert.DeserializeObject<Part>(_data);
            return _part;
        }

        public List<String> GetPartNames(string MainPartName)
        {
            MainPartName = MainPartName.Replace("+", "%2B");
            string _url = "/Part/GetPartNames?Name=" + MainPartName;
            string _data = _server.ReceiveStream(_url);
            List<String> _partNames = JsonConvert.DeserializeObject<IEnumerable<String>>(_data).ToList<string>();
            return _partNames;

        }

        /// <summary>
        /// select ItemNO from HB_PartStock where RawNO='{0}'
        /// </summary>
        /// <param name="RawNo"></param>
        /// <returns></returns>
        public string GetItemNo(string RawNo)
        {
            string _url = "/Part/StockItemNo?RawNo=" + RawNo;
            string _result = _server.ReceiveStream(_url);
            return _result;
        }

        /// <summary>
        /// PR_AddOrUpdateTempPartListR03
        /// </summary>
        /// <param name="Part"></param>
        /// <param name="Supplier"></param>
        /// <param name="CreateUser"></param>
        /// <param name="CreateComputer"></param>
        /// <returns></returns>
        public string SavePart(Part Part, string Brand, string CreateUser, string CreateComputer)
        {
           
                string _url = "/Part/PartFromUG";

                Part.BrandName = Brand;
                Part.FromUG = true;

                string _return = _server.SendObject(_url, "NewPart", Part);
                return _return;

                //return PartID;
            
        }

        public List<Part> SaveParts(List<Part> Parts, string CreateUser, string CreateComputer)
        {
            try
            {
                string _url = "/Part/PartListFromUG";
                string _return =  _server.SendObject(_url, "NewParts", Parts);
                return JsonConvert.DeserializeObject<List<Part>>(_return );
            }
            catch 
            {
                return null;
            }
        }

        public bool DeleteMoldPart(string MoldNumber)
        {
            string _url = "/Part/DeleteExisting?MoldNumber=" + MoldNumber;
            try
            {
                return Convert.ToBoolean( _server.ReceiveStream(_url));
            }
            catch
            {
                return false;
            }
        }
        

        public int GetUserID(string UserName)
        {
            string _url = "/User/GetUserByName?UserName=" + UserName;
            try
            {
                string data = _server.ReceiveStream(_url);
                User _user = JsonConvert.DeserializeObject<User>(data);
                return _user.UserID;
            }
            catch
            {
                return 0;
            }            
        }

        public string GetMoldName(string MoldNumber)
        {
            string _url = "/Project/GetMoldName?MoldNumber=" + MoldNumber;

            try
            {
                string _moldName = _server.ReceiveStream(_url);
                return _moldName;
            }
            catch(Exception e)
            {
                return e.Message;
            }
        }

        public List<MaterialHardness> GetMaterialHardness()
        {
            string _url = "/Part/JsonMaterialHardness";
            string _data = _server.ReceiveStream(_url);
            List<MaterialHardness> _materials = JsonConvert.DeserializeObject<List<MaterialHardness>>(_data);
            return _materials;
        }

        public List<Brand> GetPartBrands()
        {
            string _url = "/Part/JsonBrands";
            string _data = _server.ReceiveStream(_url);
            List<Brand> _brands = JsonConvert.DeserializeObject<List<Brand>>(_data);
            return _brands;
        }

        public List<WarehouseStock> GetStockParts(string Specification, string Material)
        {
            string _url = "/Warehouse/QueryStockParts?Specification=" + Specification + "&Material=" + Material;
            string _data = _server.ReceiveStream(_url);
            List<WarehouseStock> _stockParts = JsonConvert.DeserializeObject<List<WarehouseStock>>(_data);
            return _stockParts;
        }

        
    }
}
