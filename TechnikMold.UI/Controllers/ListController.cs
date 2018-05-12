using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TechnikSys.MoldManager.Domain.Abstract;
using TechnikSys.MoldManager.Domain.Entity;

namespace MoldManager.WebUI.Controllers
{
    public class ListController : Controller
    {

        private IListTypeRepository _listTypeRepository;
        private IListValueRepository _listValueRepository;

        public ListController(IListTypeRepository ListTypeRepository,
            IListValueRepository ListValueRepository)
        {
            _listTypeRepository = ListTypeRepository;
            _listValueRepository = ListValueRepository;
        }

        public ActionResult Index(int TypeID = 0)
        {
            IEnumerable<ListType> _listTypes = _listTypeRepository.ListTypes.Where(t => t.Enabled == true);
            ViewBag.ListTypeID = TypeID;
            return View(_listTypes);
        }

        #region 列表类型

        /// <summary>
        /// 列表类型修改保存
        /// </summary>
        /// <param name="ListType"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult EditListType(ListType ListType)
        {
            int _typeID = _listTypeRepository.Save(ListType);
            ViewBag.TypeID = _typeID;
            return RedirectToAction("Index", "List", new { TypeID = _typeID });
        }

        
        #endregion

        #region 列表内容
        /// <summary>
        /// 列表内容修改保存
        /// </summary>
        /// <param name="ListValue"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult EditListContent(ListValue ListValue)
        {
            _listValueRepository.Save(ListValue);
            int _typeID = ListValue.ListTypeID;
            ViewBag.TypeID = _typeID;
            return RedirectToAction("Index", "List", new { TypeID = _typeID });
        }

        public ActionResult DeleteListContent(int ListValueID)
        {
            int _typeID=_listValueRepository.Delete(ListValueID);
            return RedirectToAction("Index", "List", new { TypeID = _typeID });
        }

        public JsonResult ListValues(int TypeID, bool Enabled=true)
        {
            IEnumerable<ListValue> _listValues = _listValueRepository.ListValues.Where(v => v.ListTypeID == TypeID);
            if (Enabled)
            {
                _listValues = _listValues.Where(v => v.Enabled == true);
            }
            return Json(_listValues, JsonRequestBehavior.AllowGet);
        }

        public JsonResult ListValuesByName(string TypeName, bool Enabled=true)
        {
            int _typeID = _listTypeRepository.ListTypes.Where(t => t.Name == TypeName).Select(t => t.ListTypeID).FirstOrDefault();;
            return ListValues(_typeID);
        }
        #endregion

    }
}