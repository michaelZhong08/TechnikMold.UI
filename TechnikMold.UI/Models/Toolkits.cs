using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Web;
using System.Web.Mvc;
using TechnikMold.UI.Models.Attribute;

namespace TechnikMold.UI.Models
{
    public static class Toolkits
    {
        /// <summary>
        /// 写日志
        /// </summary>
        /// <param name="Path"></param>
        /// <param name="Content"></param>
        public static void WriteLog(string Path, string Content)
        {
            try
            {
                if (!System.IO.File.Exists(Path))
                {
                    FileStream fs1 = new FileStream(Path, FileMode.Create, FileAccess.Write);//创建写入文件 
                    StreamWriter sw = new StreamWriter(fs1);
                    sw.WriteLine(Content);//开始写入值
                    sw.Close();
                    fs1.Close();
                }
                else
                {
                    FileStream fs = new FileStream(Path, FileMode.Append, FileAccess.Write);
                    StreamWriter sr = new StreamWriter(fs);
                    sr.WriteLine(Content);//开始写入值
                    sr.Close();
                    fs.Close();
                }
            }
            catch { }
        }
        /// <summary>
        /// 返回英文 像素长度
        /// </summary>
        /// <param name="c"></param>
        /// <returns>2 大写 1.5 小写 1 数字</returns>
        public static double CheckStringASC(char c)
        {
            if (c >= 'a' && c <= 'z')
            {
                return 1.5;
            }
            else if (c >= 'A' && c <= 'Z')
            {
                return 2;
            }
            return 1.5;
        }
        /// <summary>
        /// 返回中文 像素长度
        /// </summary>
        /// <param name="c"></param>
        /// <returns>3 中文 0 其它</returns>
        public static double CheckStringChinese(char c)
        {
            //bool res = false;
            if ((int)c > 127)
                return 2.5;
            return 0;
        }
        public static bool CheckZero(DateTime _date)
        {
            DateTime _datezero = new DateTime(1, 1, 1);
            if (_date < DateTime.Parse("2000/01/01"))
            {
                return true;
            }
            if (_date == _datezero)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        public static string GetLogonName(string UserName)
        {
            string _userName;
            if (UserName.IndexOf('\\') > 0)
            {
                string[] _info = UserName.Split('\\');
                _userName = _info[1];
            }
            else
            {
                _userName = UserName;
            }
            return _userName;
        }
        /// <summary>
        /// 按顺序返回Excel列
        /// </summary>
        /// <param name="obj">实体类</param>
        /// <returns></returns>
        public static PropertyInfo[] GetPropsArrayByOrder(PropertyInfo[] properties)
        {
            //var t = obj.GetType();
            List<PropertyInfo> propsList = new List<PropertyInfo>();
            PropertyInfo[] props=null;
            //var properties = t.GetProperties();
            int k = 0;
            bool exchange;
            foreach (var property in properties)
            {
                if (!property.IsDefined(typeof(ExcelFieldAttribute), false)) continue;
                propsList.Add(property);
            }
            if (propsList.Count() > 0)
            {
                props = new PropertyInfo[propsList.Count];
                foreach (var p in propsList)
                {
                    props[k] = p;
                    k++;
                }
                //冒泡算法
                for (int i = 0; i < props.Count(); i++)
                {
                    exchange = false;
                    for (int j = props.Count() - 2; j >= i; j--)
                    {
                        var attributej1 = props[j + 1].GetCustomAttribute(typeof(ExcelFieldAttribute));//CustomAttributes.ToList()[0];
                        var attributej = props[j].GetCustomAttribute(typeof(ExcelFieldAttribute));
                        int orderNum1 = (int)attributej1.GetType().GetProperty("ExcelFieldOrder").GetValue(attributej1);
                        int orderNum = (int)attributej.GetType().GetProperty("ExcelFieldOrder").GetValue(attributej);
                        //目标序列按照 从小到大排序
                        if (orderNum1 < orderNum)
                        {
                            var temp = props[j + 1];
                            props[j + 1] = props[j];
                            props[j] = temp;
                            exchange = true;
                        }
                    }
                    if (!exchange)//本趟排序未发生交换，提前终止算法 
                    {
                        break;
                    }
                }
            }
            return props;
        }
    }   
}