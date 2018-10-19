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
            //_server = new WebServer(ServerName, Port, "Michael", "5t6y&U*I");
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


        #region UG对接webserver服务接口
        /// <summary>
        /// 获取某模具的版本清单（版本控制）
        /// </summary>
        /// <param name="MoldNumber">模具编号</param>
        /// <returns></returns>
        public List<MoldVersionInfo> GetMoldVersionList(string MoldNumber)
        {
            try
            {
                string _url = "/Part/GetMoldVerList?MoldNumber=" + MoldNumber;
                string _data = _server.ReceiveStream(_url);
                List<MoldVersionInfo> _MoldVersionInfo = JsonConvert.DeserializeObject<List<MoldVersionInfo>>(_data);
                return _MoldVersionInfo;
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        /// <summary>
        /// 获取某零件的版本列表（版本控制）
        /// </summary>
        /// <param name="Name">零件名称（含版本号）</param>
        /// <returns></returns>
        public List<PartVersionInfo> GetPartVersionList(string Name)
        {
            try
            {
                string _url = "/Part/GetJsonPartVers?Name=" + Name;
                string _data = _server.ReceiveStream(_url);
                List<PartVersionInfo> _MoldVersionInfo = JsonConvert.DeserializeObject<List<PartVersionInfo>>(_data);
                return _MoldVersionInfo;
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        /// <summary>
        /// 获取某模具编号的最新版本的零件清单（版本控制）
        /// </summary>
        /// <param name="MoldNo">模具编号</param>
        /// <param name="FromUG">是否来自UG</param>
        /// <param name="Ver">模具版本号(-1 不指定版本号，默认取最新的版本)</param>
        /// <returns></returns>
        public List<Part> GetPartsByMoldVer(string MoldNo, bool FromUG, int Ver)
        {
            try
            {
                string _url = "/Part/JsonUGPartVer?MoldNumber=" + MoldNo + "&FromUG=" + FromUG + "&Ver=" + Ver;
                string _data = _server.ReceiveStream(_url);
                List<Part> _parts = JsonConvert.DeserializeObject<List<Part>>(_data);
                return _parts;
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        /// <summary>
        /// 提交一个零件明细（版本控制）
        /// 检查名称在库里是否存在，
        /// 1.若存在，则检查内容是否一致，不一致则不能提交，必须升级为新版本
        /// 2.不存在，则为新增，并检查版本号不能小于等于原有的版本号
        /// </summary>
        /// <param name="Part">零件明细</param> 
        /// <returns></returns>
        public ResponseInfo SavePartVer(Part Part)
        {
            try
            {
                string _url = "/Part/PartFromUGVer";
                Part.FromUG = true;
                string _return = _server.SendObject(_url, "NewPart", Part);
                ResponseInfo ri = JsonConvert.DeserializeObject<ResponseInfo>(_return);
                return ri;
            }
            catch (Exception ex)
            {
                return new ResponseInfo() { Status = -1, Message = ex.Message };
            }
        }
        /// <summary>
        /// 提交一批零件明细（版本控制）
        /// 检查每个零件的名称在库里是否存在，
        /// 1.若存在，则检查内容是否一致，若不一致则不能提交，必须升级为新版本；若一致，不做操作
        /// 2.不存在，则为新增，并检查版本号不能小于等于原有的版本号
        /// 3.检查库里有该名称，但提交的清单没有，则删除
        /// </summary>
        /// <param name="Parts">零件明细清单</param> 
        /// <returns></returns>
        public ResponseInfo SaveParts(List<Part> Parts)
        {
            try
            {
                string _url = "/Part/PartListFromUGVer";
                string _return = _server.SendObject(_url, "NewParts", Parts);
                return JsonConvert.DeserializeObject<ResponseInfo>(_return);
            }
            catch (Exception ex)
            {
                return new ResponseInfo() { Status = -1, Message = ex.Message };
            }
        }
        public ResponseInfo CheckIsModifyKey(Part NewPart)
        {
            try
            {
                string _url = "/Part/CheckIsModify";
                string _return = _server.SendObject(_url, "NewPart", NewPart);
                return JsonConvert.DeserializeObject<ResponseInfo>(_return);
            }
            catch (Exception ex)
            {
                return new ResponseInfo() { Status = -1, Message = ex.Message };
            }
        }
        #endregion
    }
}
