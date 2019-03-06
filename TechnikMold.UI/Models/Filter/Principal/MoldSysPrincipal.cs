using Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Web;
using System.Web.Script.Serialization;
using System.Web.Security;

namespace TechnikMold.UI.Models.Filter.Principal
{
    public class MoldSysPrincipal<TUserData>: IPrincipal where TUserData : class, new()
    {
        private IIdentity _identity;
        private TUserData _userData;
        public MoldSysPrincipal(FormsAuthenticationTicket ticket, TUserData userData)
        {
            if (ticket == null)
                throw new ArgumentNullException("ticket");
            if (userData == null)
                throw new ArgumentNullException("userData");

            //_identity = new FormsIdentity(ticket);
            _identity = new MoldSysIdentity(userData as UserPrincipal);
            _userData = userData;
        }
        public TUserData UserData
        {
            get { return _userData; }
        }

        public IIdentity Identity
        {
            get { return _identity; }
        }
        public bool IsInRole(string role)
        {
            // 把判断用户组的操作留给UserData去实现。

            IPrincipal principal = _userData as IPrincipal;
            if (principal == null)
                throw new NotImplementedException();
            else
                return principal.IsInRole(role);
        }
        /// <summary>
        /// 执行用户登录操作
        /// </summary>
        /// <param name="UserID">工号</param>
        /// <param name="userData">与登录名相关的用户信息</param>
        /// <param name="expiration">登录Cookie的过期时间，单位：分钟。</param>
        public static void SignIn(string UserID, TUserData userData, int expiration)
        {
            if (string.IsNullOrEmpty(UserID))
                throw new ArgumentNullException("UserID");
            if (userData == null)
                throw new ArgumentNullException("userData");

            // 1. 把需要保存的用户数据转成一个json字符串。
            string data = null;
            if (userData != null)
                data = (new JavaScriptSerializer()).Serialize(userData);


            // 2. 创建一个FormsAuthenticationTicket，它包含登录名以及额外的用户数据。
            FormsAuthenticationTicket ticket = new FormsAuthenticationTicket(
                2, UserID, DateTime.Now, DateTime.Now.AddDays(1), true, data);

            // 3. 加密Ticket，变成一个加密的字符串。
            string cookieValue = FormsAuthentication.Encrypt(ticket);


            // 4. 根据加密结果创建登录Cookie
            HttpCookie cookie = new HttpCookie("ASP.NET", cookieValue);
            cookie.HttpOnly = true;
            cookie.Secure = FormsAuthentication.RequireSSL;
            cookie.Domain = FormsAuthentication.CookieDomain;
            cookie.Path = FormsAuthentication.FormsCookiePath;
            if (expiration > 0)
            {
                //cookie.Expires = DateTime.Now.AddMinutes(expiration);
            }
            HttpContext context = HttpContext.Current;
            if (context == null)
                throw new InvalidOperationException();

            // 5. 写登录Cookie
            context.Response.Cookies.Remove(cookie.Name);
            context.Response.Cookies.Add(cookie);
            //设置当前用户数据Cookie
            try
            {
                if (context.Request.Cookies["AppUser"].Value != null)
                {
                    CookieOpera.delCookie("AppUser");
                }
            }
            catch { }
            //JsonHelper jh = new JsonHelper();
            string jsonstr = JsonHelper.GetJson(userData);
            CookieOpera.WriteCookie("AppUser", jsonstr);
        }
        /// <summary>
        /// 根据HttpContext对象设置用户标识对象
        /// </summary>
        /// <param name="context"></param>
        public static void TrySetUserInfo(HttpContext context)
        {
            if (context == null)
                throw new ArgumentNullException("context");

            // 1. 读登录Cookie
            HttpCookie cookie = context.Request.Cookies["ASP.NET"];
            if (cookie == null || string.IsNullOrEmpty(cookie.Value))
                return;

            try
            {
                TUserData userData = null;
                // 2. 解密Cookie值，获取FormsAuthenticationTicket对象
                FormsAuthenticationTicket ticket = FormsAuthentication.Decrypt(cookie.Value);

                if (ticket != null && string.IsNullOrEmpty(ticket.UserData) == false)
                    // 3. 还原用户数据
                    userData = (new JavaScriptSerializer()).Deserialize<TUserData>(ticket.UserData);

                if (ticket != null && userData != null)
                    // 4. 构造我们的MyFormsPrincipal实例，重新给context.User赋值。
                    context.User = new MoldSysPrincipal<TUserData>(ticket, userData);
            }
            catch { /* 有异常也不要抛出，防止攻击者试探。 */ }
        }
    }
}