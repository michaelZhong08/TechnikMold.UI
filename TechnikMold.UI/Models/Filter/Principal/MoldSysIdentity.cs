using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Security.Principal;

namespace TechnikMold.UI.Models.Filter.Principal
{
    public class MoldSysIdentity:IIdentity
    {
        UserPrincipal curuser = new UserPrincipal();
        ///
        /// 构造函数，根据用户名
        ///
        ///
        public MoldSysIdentity(UserPrincipal user)
        {
            //根据UserName查询数据库获得以下数据
            this.curuser = user;
        }
        public string AuthenticationType
        {
            get { return "Form"; }
        }
        ///
        /// 是否验证
        ///
        public bool IsAuthenticated
        {
            get { return true; }
        }
        ///
        /// 返回用户
        ///
        public string Name
        {
            get { return curuser.FullName; }
        }
    }
}