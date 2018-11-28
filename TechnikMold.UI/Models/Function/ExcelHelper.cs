using Aspose.Cells;
using NPOI;
using NPOI.HSSF.UserModel;
using NPOI.OpenXml4Net.OPC;
using NPOI.POIFS.FileSystem;
using NPOI.SS.UserModel;
using NPOI.XSSF.UserModel;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Web;

namespace TechnikMold.UI.Models.Function
{
    public class ExcelHelper 
    {
        private IWorkbook workbook = null;

        /// <summary>
        /// Excel表格导出
        /// </summary>
        /// <typeparam name="T">实体类</typeparam>
        /// <param name="_excelPath">excel模板路径</param>
        /// <param name="_headList">excel列头</param>
        /// <param name="_excelContent">excel内容</param>
        /// <param name="sheetName">excel sheet名</param>
        /// <param name="isColumnWritten">是否导入列名</param>
        /// <returns></returns>
        public MemoryStream ExportExcelStream<T>(List<T> _excelContent, string _excelPath,string sheetName, List<string> _headList,bool isColumnWritten)
        {
            HttpContext _httpcontext = HttpContext.Current;
            string TempletFileName = _httpcontext.Server.MapPath(_excelPath);

            int i = 0;
            int j = 0;
            int count = 0;
            ISheet sheet = null;
            //IWorkbook workbook = null;
            int _count;
            Type type = typeof(T);
            PropertyInfo[] props = type.GetProperties(BindingFlags.Public | BindingFlags.Instance);
            if (_headList != null)
                _count = _headList.Count;
            else
                _count = props.Count();

            using (FileStream fs = System.IO.File.Open(TempletFileName, FileMode.OpenOrCreate, FileAccess.Read, FileShare.ReadWrite))
            {
                //把xls文件读入workbook变量里，之后就可以关闭了
                if (TempletFileName.IndexOf(".xlsx") > 0) // 2007版本
                    workbook = new XSSFWorkbook(fs);
                else if (TempletFileName.IndexOf(".xls") > 0) // 2003版本
                    workbook = new HSSFWorkbook(fs);
                //workbook = new XSSFWorkbook(fs);
                fs.Close();
            }
            try
            {
                //ExcelOperations eo = new ExcelOperations();                
                if (workbook != null)
                {
                    //sheet = workbook.CreateSheet(sheetName);
                    sheet = workbook.GetSheetAt(0);
                }
                else
                {
                    return null;
                }
                
                if (isColumnWritten == true) //写入列名
                {
                    IRow row = sheet.GetRow(0);
                    if (_headList != null)
                    {
                        for (j = 0; j < _count; ++j)
                        {
                            ICell _cell = row.CreateCell(j);
                            _cell.SetCellValue(_headList[j].ToString());
                            //row.GetCell(j).SetCellValue(_headList[j]);
                        }
                    }
                    else
                    {                        
                        for (j = 0; j < _count; ++j)
                        {
                            string name = props[j].Name;
                            ICell _cell = row.CreateCell(j);
                            _cell.SetCellValue(name.ToString());
                            //row.GetCell(j).SetCellValue(name);
                        }
                    }
                    count = 1;
                }
                else
                {
                    count = 0;
                }
                
                for (i = 0; i < _excelContent.Count; ++i)
                {
                    IRow _formatRow;
                    if (count % 2 == 1)
                    {
                        //_formatRow = sheet.GetRow(1);
                    }
                    else
                    {
                        //_formatRow = sheet.GetRow(2);
                    }
                    //eo.InsertRow(sheet, 2+count, 1, _formatRow);
                    IRow row = sheet.CreateRow(count);
                    T _t = _excelContent[i];                  
                    for (j = 0; j < _count; ++j)
                    {
                        string name = props[j].Name;
                        PropertyInfo p = typeof(T).GetProperty(name);
                        string value = p.GetValue(_t, null) == null ? "" : p.GetValue(_t, null).ToString();
                        row.CreateCell(j).SetCellValue(value);
                    }
                    ++count;
                }
                MemoryStream _ms =new MemoryStream();
                workbook.Write(_ms); //写入到excel
                _ms.Flush();
                _ms.Position = 0;
                return _ms;
            }
            catch (Exception ex)
            {
                Console.WriteLine("Exception: " + ex.Message);
                return null;
            }
        }
        /// <summary>
        /// Excel表格导出
        /// </summary>
        /// <typeparam name="T">实体类</typeparam>
        /// <param name="_excelPath">excel模板路径</param>
        /// <param name="_headList">excel列头</param>
        /// <param name="_excelContentDT">excel内容</param>
        /// <param name="sheetName">excel sheet名</param>
        /// <param name="isColumnWritten">是否导入列名</param>
        /// <returns></returns>
        public MemoryStream ExportExcelStream(DataTable _excelContentDT,string _excelPath, string sheetName, List<string> _headList, bool isColumnWritten)
        {
            HttpContext _httpcontext = HttpContext.Current;
            string TempletFileName = _httpcontext.Server.MapPath(_excelPath);

            int i = 0;
            int j = 0;
            int count = 0;
            ISheet sheet = null;
            int _count;
            if (_headList != null)
                _count = _headList.Count;
            else
                _count = _excelContentDT.Columns.Count;
            using (FileStream fs = System.IO.File.Open(TempletFileName, FileMode.OpenOrCreate, FileAccess.Read, FileShare.ReadWrite))
            {
                //把xls文件读入workbook变量里，之后就可以关闭了
                if (TempletFileName.IndexOf(".xlsx") > 0) // 2007版本
                    workbook = new XSSFWorkbook(fs);
                else if (TempletFileName.IndexOf(".xls") > 0) // 2003版本
                    workbook = new HSSFWorkbook(fs);

                fs.Close();
            }
            try
            {
                if (workbook != null)
                {
                    sheet = workbook.CreateSheet(sheetName);
                }
                else
                {
                    return null;
                }

                if (isColumnWritten == true) //写入列名
                {
                    IRow row = sheet.CreateRow(0);
                    for (j = 0; j < _excelContentDT.Columns.Count; ++j)
                    {
                        row.CreateCell(j).SetCellValue(_excelContentDT.Columns[j].ColumnName);
                    }
                    count = 1;
                }
                else
                {
                    count = 0;
                }

                for (i = 0; i < _excelContentDT.Rows.Count; ++i)
                {
                    IRow row = sheet.CreateRow(count);
                    for (j = 0; j < _count; ++j)
                    {
                        row.CreateCell(j).SetCellValue(_excelContentDT.Rows[i][j].ToString());
                    }
                    ++count;
                }
                MemoryStream _ms = new MemoryStream();
                workbook.Write(_ms); //写入到excel
                _ms.Flush();
                _ms.Position = 0;
                return _ms;
            }
            catch (Exception ex)
            {
                //Console.WriteLine("Exception: " + ex.Message);
                return null;
            }
        }
    }
    public class ExcelOperations
    {
        private int insertRowIndex;
        private int insertRowCount;
        private Dictionary<int, string> insertData;

        public IWorkbook NPOIOpenExcel(string filename)
        {
            Stream excelStream = OpenResource(filename);
            if (POIFSFileSystem.HasPOIFSHeader(excelStream))
                return new HSSFWorkbook(excelStream);
            if (POIXMLDocument.HasOOXMLHeader(excelStream))
            {
                return new XSSFWorkbook(OPCPackage.Open(excelStream));
            }
            if (filename.EndsWith(".xlsx"))
            {
                return new XSSFWorkbook(excelStream);
            }
            if (filename.EndsWith(".xls"))
            {
                new HSSFWorkbook(excelStream);
            }
            throw new Exception("Your InputStream was neither an OLE2 stream, nor an OOXML stream");
        }

        public Stream OpenResource(string filename)
        {
            FileStream fs = new FileStream(filename, FileMode.Open, FileAccess.Read);
            return fs;
        }
        //插入
        public void InsertRow(ISheet sheet, int insertRowIndex, int insertRowCount, IRow formatRow)
        {
            sheet.ShiftRows(insertRowIndex, 100, insertRowCount, true, false);
            for (int i = insertRowIndex; i < insertRowIndex + insertRowCount; i++)
            {
                IRow targetRow = null;
                ICell sourceCell = null;
                ICell targetCell = null;
                targetRow = sheet.CreateRow(i);
                for (int m = formatRow.FirstCellNum; m < formatRow.LastCellNum; m++)
                {
                    sourceCell = formatRow.GetCell(m);
                    if (sourceCell == null)
                    {
                        continue;
                    }
                    targetCell = targetRow.CreateCell(m);
                    targetCell.CellStyle = sourceCell.CellStyle;
                    targetCell.SetCellType(sourceCell.CellType);

                }
            }
        }
        public void WriteToFile(IWorkbook workbook, string filename)
        {
            if (File.Exists(filename))
            {
                File.Delete(filename);
            }
            using (FileStream fs = new FileStream(filename, FileMode.OpenOrCreate, FileAccess.Write))
            {
                workbook.Write(fs);
                fs.Close();
            }
        }
    }
}