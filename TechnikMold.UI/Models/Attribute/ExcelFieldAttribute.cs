using System;

namespace TechnikMold.UI.Models.Attribute
{
    [AttributeUsage(AttributeTargets.Property)]
    public class ExcelFieldAttribute : System.Attribute
    {
        private int _excelFieldOrder;
        public ExcelFieldAttribute(int excelFieldOrder)
        {
            _excelFieldOrder = excelFieldOrder;
        }
        public int ExcelFieldOrder { get { return _excelFieldOrder; } }
    }
}