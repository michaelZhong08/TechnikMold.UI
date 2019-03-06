using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Security.Principal;
using System.Web.Script.Serialization;
using TechnikMold.UI.Models.ViewModel;

namespace TechnikMold.UI.Models.Filter.Principal
{
    public class UserPrincipal : CkUserData, IPrincipal
    {
        #region IPrincipal Members

        [ScriptIgnore]
        public IIdentity Identity
        {
            get { throw new NotImplementedException(); }
        }

        public bool IsInRole(string role)
        {
            HttpContext context = HttpContext.Current;
            //读取cookie 获得当前用户权限列表
            //string cookieStr = CookieOpera.GetCookie("User");
            //User_Formal user = JsonHelper.ParseFromJson<User_Formal>(cookieStr);
            //List<Base_Role> roleList = user.UserRoleList;
            //foreach (Base_Role roleModel in roleList)
            //{
            //    if (role.Equals(roleModel.BriefName))
            //    {
            //        return true;
            //    }
            //}
            //return false;
            return true;
        }
        #endregion
    }
}