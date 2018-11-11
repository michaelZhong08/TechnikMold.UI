using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TechnikMold.UI.Models.GridViewModel;
using TechnikSys.MoldManager.Domain.Abstract;
using TechnikSys.MoldManager.Domain.Entity;

namespace TechnikMold.UI.Controllers
{
    public class AttachmentController : BaseController
    {
        public AttachmentController(IAttachFileInfoRepository AttachFileInfoRepository
            ,IPurchaseItemRepository PurchaseItemRepository)
        {
            _attachFileInfoRepository = AttachFileInfoRepository;
            _purchaseItemRepository = PurchaseItemRepository;
        }
        public JsonResult Service_GetFilesByObj(string ObjID, string ObjType)
        {
            IQueryable<AttachFileInfo> _attachFiles = _attachFileInfoRepository.GetAttachByObj(ObjID, ObjType);
            AttachGridViewModel _viewModel = new AttachGridViewModel(_attachFiles);
            return Json(_viewModel, JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        /// 文件上传
        /// </summary>
        /// <param name="ObjID">业务主键</param>
        /// <param name="ObjType">业务对象</param>
        /// <param name="Files">文件流</param>
        /// <returns></returns>
        [HttpPost]
        public JsonResult Service_FileUpload(string ObjID,string ObjType, IEnumerable<HttpPostedFileBase> Files)
        {
            try
            {
                #region 创建文件夹
                string filePath = Server.MapPath(string.Format("~/{0}/{1}/{2}", "Upload", ObjType, ObjID));
                if (!Directory.Exists(filePath))
                {
                    Directory.CreateDirectory(filePath);
                }
                #endregion
                foreach (HttpPostedFileBase _file in Files)
                {
                    #region 保存文件
                    string fileName = _file.FileName.Substring(0, _file.FileName.LastIndexOf('.'));
                    //if (fileName.IndexOf('+') > 0)
                    //{
                    //    return Json(new { Code = -99, Message = "Error:文件名包含特殊字符(如:'+')" }, JsonRequestBehavior.AllowGet);
                    //}
                    string ImportFilePath = Path.Combine(filePath, _file.FileName);
                    _file.SaveAs(ImportFilePath);
                    #endregion
                    #region 文件转成流并格式化
                    AttachFileInfo af = new AttachFileInfo();
                    //string timeStamp = "";
                    #region 获取文件流并转化为btye[]
                    Stream fs = _file.InputStream;
                    Byte[] btye1 = new byte[fs.Length];
                    fs.Read(btye1, 0, Convert.ToInt32(fs.Length));
                    fs.Close();
                    #endregion
                    //#region 获取时间戳
                    //TimeSpan ts = DateTime.UtcNow - new DateTime(1970, 1, 1, 0, 0, 0, 0);
                    //timeStamp = Convert.ToInt64(ts.TotalSeconds).ToString() + btye1.Length.ToString();
                    //#endregion
                    //af.EmbedID = timeStamp;
                    //if (fileName.IndexOf('+') > 0)
                    //{
                    //    fileName = fileName.Replace("+", "%2B");
                    //}
                    af.FileName = fileName;
                    af.FileStream = new byte[1];
                    af.FileType = _file.FileName.Split('.')[1];
                    af.Creator = GetCurrentUser();
                    af.ObjID = ObjID;
                    af.ObjType = ObjType;
                    af.FileSize = Math.Round(Convert.ToDouble(btye1.Length * 1.0 / (1024 * 1024)),2);
                    af.FilePath = @"\Upload\"+ ObjType +@"\"+ ObjID+ @"\";
                    _attachFileInfoRepository.Save(af);
                    #endregion
                }
                return Json(new { Code = 1, Message = "OK" }, JsonRequestBehavior.AllowGet);
            }
            catch(Exception ex)
            {
                LogRecord("附件上传", ex.InnerException == null ? ex.Message : ex.InnerException.InnerException == null ? ex.Message : ex.InnerException.InnerException.ToString());
                return Json(new { Code = -1, Message = "附件上传失败！" },JsonRequestBehavior.AllowGet);
            }
        }
        public JsonResult Service_DelAttach(string ObjID, string ObjType,string FileName,string FileType)
        {
            try
            {
                AttachFileInfo _model = _attachFileInfoRepository.GetAttachModel(ObjID, ObjType, FileName, FileType);
                if (_model.Creator != GetCurrentUser())
                {
                    return Json(new { Code = -1 }, JsonRequestBehavior.AllowGet);
                }
                else
                    _attachFileInfoRepository.Delete(_model);
                return Json(new { Code = 1 }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                LogRecord("附件删除", ex.InnerException==null? ex.Message: ex.InnerException.InnerException == null ? ex.Message: ex.InnerException.InnerException.ToString());
                return Json(new { Code = -1, Message = "附件删除失败！" }, JsonRequestBehavior.AllowGet);
            }
        }

        public ActionResult Service_FileDownLoad(string ObjID, string ObjType, string FileName, string FileType)
        {
            try
            {
                AttachFileInfo _model = _attachFileInfoRepository.GetAttachModel(ObjID, ObjType, FileName, FileType);
                string _url = Server.MapPath("~")+ _model.FilePath + _model.FileName + "." + _model.FileType;
                //return File(_url, "text/plain",_model.FileName); //返回file
                return File(new FileStream(_url, FileMode.Open), "text/plain", _model.FileName + "." + _model.FileType); //返回FileStream
            }
            catch (Exception ex)
            {
                LogRecord("附件下载", ex.InnerException == null ? ex.Message : ex.InnerException.InnerException == null ? ex.Message : ex.InnerException.InnerException.ToString());
                return Json(new { Code = -1, Message = "附件下载失败！" }, JsonRequestBehavior.AllowGet);
            }
        }

        public JsonResult Service_GetFilesByPurchaseItem(string _itemIDs)
        {
            var _itemIDList = _itemIDs.Split(',');
            if (_itemIDList.Count() > 0)
            {
                IQueryable<PurchaseItem> _items = _purchaseItemRepository.PurchaseItems.Where(p => _itemIDList.Contains(p.PurchaseItemID.ToString()));
                PurchaseAttachGridViewModel _viewModel = new PurchaseAttachGridViewModel(_items, _attachFileInfoRepository);
                return Json(_viewModel, JsonRequestBehavior.AllowGet);
            }
            return null;
        }
        /// <summary>
        /// 采购项附件保存
        /// </summary>
        /// <param name="_itemIDs"></param>
        /// <param name="Files">文件名 零件号(模具号-图纸号)-xxx(任意)</param>
        /// <returns></returns>
        [HttpPost]
        public JsonResult Service_PurchaseItemFileUpload(string _itemIDs, IEnumerable<HttpPostedFileBase> Files,int purFileModal= 1)
        {
            var _itemIDList = _itemIDs.Split(',');
            List<PurchaseItem> _items = _purchaseItemRepository.PurchaseItems.Where(p => _itemIDList.Contains(p.PurchaseItemID.ToString())).ToList();
            string ObjType = "PurchaseItems";
            string ObjID;
            #region 单文件
            if (Files.Count() == 1)
            {
                var _file = Files.FirstOrDefault();
                Stream fs = _file.InputStream;
                ObjID = GetTimeStamp(DateTime.Now) + Math.Round(Convert.ToDouble(fs.Length), 8).ToString();
                try
                {
                    #region 附件传递模式
                    if (purFileModal==1)//单附件模式
                    {
                        var res = Service_FileUpload(ObjID, ObjType, Files);
                        foreach (var p in _items)
                        {
                            p.AttachObjID = ObjID;
                            _purchaseItemRepository.Save(p);
                        }
                    }
                    else
                    {
                        foreach (var p in _items)
                        {
                            if (_file.FileName.Contains(p.PartNumber))
                            {
                                //List<HttpPostedFileBase> Files1 = new List<HttpPostedFileBase>();
                                //Files1.Add(_file);
                                var res = Service_FileUpload(ObjID, ObjType, Files);
                                p.AttachObjID = ObjID;
                                _purchaseItemRepository.Save(p);
                            }
                        }
                    }
                    #endregion
                    return Json(new { Code = 1 });
                }
                catch(Exception ex)
                {
                    LogRecord("附件上传", ex.InnerException == null ? ex.Message : ex.InnerException.InnerException == null ? ex.Message : ex.InnerException.InnerException.ToString());
                    return Json(new { Code = -1, Message = "附件上传失败！" }, JsonRequestBehavior.AllowGet);
                }
            }
            #endregion
            #region 多文件
            else if (Files.Count() > 1)
            {
                try
                {
                    foreach (var _file in Files)
                    {
                        Stream fs = _file.InputStream;
                        ObjID = GetTimeStamp(DateTime.Now) + Math.Round(Convert.ToDouble(fs.Length), 8).ToString();
                        foreach (var p in _items)
                        {
                            if (_file.FileName.Contains(p.PartNumber))
                            {
                                List<HttpPostedFileBase> Files1 = new List<HttpPostedFileBase>();
                                Files1.Add(_file);
                                var res = Service_FileUpload(ObjID, ObjType, Files1);
                                p.AttachObjID = ObjID;
                                _purchaseItemRepository.Save(p);
                            }
                        }
                    }
                    return Json(new { Code = 1 });
                }
                catch (Exception ex)
                {
                    LogRecord("附件上传", ex.InnerException == null ? ex.Message : ex.InnerException.InnerException == null ? ex.Message : ex.InnerException.InnerException.ToString());
                    return Json(new { Code = -1, Message = "附件上传失败！" }, JsonRequestBehavior.AllowGet);
                }
            }
            #endregion
            return Json(new { Code = -99 });
        }
    }
}