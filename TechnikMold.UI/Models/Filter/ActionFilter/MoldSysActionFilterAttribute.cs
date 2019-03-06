using DAL;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Mvc;
using TechnikMold.Domain.Entity;

namespace TechnikMold.UI.Models.Filter.ActionFilter
{
    public class MoldSysActionFilterAttribute: ActionFilterAttribute
    {
        static string constr = ConfigurationManager.ConnectionStrings["EFDbContext"].ToString();
        UserInfoDal _userRepository = new UserInfoDal(constr);
        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            string UserName = filterContext.HttpContext.User.Identity.Name;
            #region Cookie设置
            SetUserCookie(UserName);

            #endregion
        }
        public override void OnActionExecuted(ActionExecutedContext filterContext)
        {

        }
        public void SetUserCookie(string UserName)
        {
            HttpContext context = HttpContext.Current;
            string fullName = Toolkits.GetLogonName(UserName);
            string[] _info = UserName.Split('\\');
            if (_info[1] == "DefaultAppPool" || string.IsNullOrEmpty(UserName))
            {
                return;
            }
            if (context.Request.Cookies["User"] == null) 
            {
                DataTable dt;
                try
                {
                    dt = _userRepository.Get_UserIngoDataA(fullName);
                }
                catch(Exception ex)
                {
                    dt = null;
                }

                if (dt != null)
                {
                    HttpCookie _cookie = new HttpCookie("User");
                    if (dt.Rows.Count > 0)
                    {
                        _cookie.Values.Add("UserID", dt.Rows[0]["UserID"].ToString());
                        _cookie.Values.Add("UserCode", HttpUtility.UrlEncode(dt.Rows[0]["UserCode"].ToString(), Encoding.GetEncoding("UTF-8")));
                        _cookie.Values.Add("FullName", HttpUtility.UrlEncode(dt.Rows[0]["FullName"].ToString(), Encoding.GetEncoding("UTF-8")));

                        _cookie.Values.Add("Department", dt.Rows[0]["DepartmentID"].ToString());
                        _cookie.Values.Add("DepartmentName", HttpUtility.UrlEncode(dt.Rows[0]["DepartmenName"].ToString(), Encoding.GetEncoding("UTF-8")));

                        _cookie.Values.Add("Position", dt.Rows[0]["PositionID"].ToString());
                        _cookie.Values.Add("PositionName", HttpUtility.UrlEncode(dt.Rows[0]["PositionName"].ToString(), Encoding.GetEncoding("UTF-8")));

                        context.Response.Cookies.Add(_cookie);
                    }
                    else
                    {
                        throw new HttpException(401,string.Format("在系统中没有找到 {0} 的信息，请与系统管理员联系", _info[1] + "---" + UserName));
                        //context.Response.Redirect("/User/NoRegister?UserName=" + _info[1] + "---" + UserName);
                    }
                }
                else
                {
                    throw new HttpException(401,string.Format("在系统中没有找到 {0} 的信息，请与系统管理员联系", _info[1] + "---" + UserName));
                    //context.Response.Redirect("/User/NoRegister?UserName=" + _info[1] + "---" + UserName);
                }
            }
        }
    }
}