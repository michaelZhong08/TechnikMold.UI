using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;

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
                FileStream fs = new FileStream(Path, FileMode.Open, FileAccess.Write);
                StreamWriter sr = new StreamWriter(fs);
                sr.WriteLine(Content);//开始写入值
                sr.Close();
                fs.Close();
            }
        }
    }
}