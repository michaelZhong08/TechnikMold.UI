using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TechnikMold.UI.Models.ViewModel
{
    public class QRMailTable
    {
        public string PartName { get; set; }
        public string PartNum { get; set; }
        public string Spec { get; set; }
        public string Qty { get; set; }
        public string Material { get; set; }
        public string Brand { get; set; }
        public string DrawRequired { get; set; }
        public string RequiredDate { get; set; }
        public string Memo { get; set; }
    }
}