using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;

namespace Common
{
    public class CookieOpera
    {
        /// <summary>  
        /// 写cookie值  
        /// 登陆：WriteCookie(键,值)   
        /// 退出：WriteCookie(键,"")  
        /// </summary>  
        /// <param name="strName">名称</param>  
        /// <param name="strValue">值</param>  
        public static void WriteCookie(string strName, string strValue)
        {
            HttpCookie cookie = HttpContext.Current.Request.Cookies[strName];

            if (cookie == null)
            {
                cookie = new HttpCookie(strName);
            }
            cookie.Value = HttpUtility.UrlEncode(strValue, Encoding.GetEncoding("UTF-8"));

            cookie.Expires = DateTime.Now.AddDays(14);
            HttpContext.Current.Response.AppendCookie(cookie);
        }
        public static string GetCookie(string strName)
        {
            try
            {

                if (HttpContext.Current.Request.Cookies != null && HttpContext.Current.Request.Cookies[strName] != null)
                {
                    string cookieStr = HttpContext.Current.Request.Cookies[strName].Value.ToString();
                    return HttpUtility.UrlDecode(cookieStr, Encoding.GetEncoding("UTF-8"));
                }
            }
            catch
            {
                return "";
            }

            return "";
        }
        /// <summary>
        /// 删除Cookies
        /// </summary>
        /// <param name="strName">主键</param>
        /// <returns></returns>
        public static bool delCookie(string strName)
        {
            try
            {
                HttpCookie Cookie = new HttpCookie(strName);
                //Cookie.Domain = ".xxx.com";//当要跨域名访问的时候,给cookie指定域名即可,格式为.xxx.com
                Cookie.Expires = DateTime.Now.AddDays(-1);
                System.Web.HttpContext.Current.Response.Cookies.Add(Cookie);
                return true;
            }
            catch
            {
                return false;
            }
        }
    }
}
