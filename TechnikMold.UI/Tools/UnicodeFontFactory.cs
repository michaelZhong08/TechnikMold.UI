using iTextSharp.text;
using iTextSharp.text.pdf;
//using iTextSharp.tool.xml;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Web;


namespace MoldManager.WebUI.Tools
{
    public class UnicodeFontFactory : FontFactoryImp 
    {
        private static readonly string arialFontPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Fonts),
      "arialuni.ttf");//arial unicode MS是完整的unicode字型。
        private static readonly string STFangSoPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Fonts),
          "STFANGSO.TTF");//仿宋体STFangSo
        private static readonly string SimFangPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Fonts),
          "simfang.ttf");//仿宋体STFangSo


        public override Font GetFont(string fontname, string encoding, bool embedded, float size, int style, BaseColor color,
          bool cached)
        {
            BaseFont baseFont;
            //可用Arial或标楷体，自己选一个
            try
            {
                baseFont = BaseFont.CreateFont(STFangSoPath, BaseFont.IDENTITY_H, BaseFont.EMBEDDED);
            }
            catch
            {
                try
                {
                    baseFont = BaseFont.CreateFont(SimFangPath, BaseFont.IDENTITY_H, BaseFont.EMBEDDED);
                }
                catch
                {
                    baseFont = BaseFont.CreateFont(arialFontPath, BaseFont.IDENTITY_H, BaseFont.EMBEDDED);
                }
            }
            
            return new Font(baseFont, size, style, color);
        }
   
    }
}