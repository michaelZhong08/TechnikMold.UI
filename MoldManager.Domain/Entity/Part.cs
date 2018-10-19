/*
 * Create By:lechun1
 * 
 * Description:data represents information of a single part
 * 
 */



using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace TechnikSys.MoldManager.Domain.Entity
{
    public class Part
    {

        //Primary Key
        public int PartID{ get; set; }
        //Identity of project
        public int ProjectID { get; set; }
        //ShortName of the Part
        public string ShortName { get; set; }
        //FullName of the Part
        public string Name { get; set; }
        //Job Number
        public string JobNo { get; set; }
        //Part Number(Mold Number+Job Number)
        public string PartNumber{ get; set; }
        //Size of part(Raw size read from UG)
        public string Specification { get; set; }
        //Database identity of material
        public int MaterialID { get; set; }
        //Name of Material
        public string MaterialName { get; set; }
        //Hardness value of material
        public string Hardness { get; set; }
        //Database identity of part brand(if the part is purchased)
        public int BrandID { get; set; }
        //Name of part brand
        public string BrandName { get; set; }
        //Database identity of supplier
        public int SupplierID { get; set; }
        //Name of Supplier
        public string SupplierName { get; set; }
        //Part has detail drawing or not
        public bool DetailDrawing { get; set; }
        //Part has brief drawing or not
        public bool BriefDrawing { get; set; }
        //Purchase with drawing or not
        public bool PurchaseDrawing { get; set; }
        //Part nees extra machining
        public bool ExtraMaching { get; set; }
        //Memo
        public string Memo { get; set; }
        //Create date
        public DateTime CreateDate { get; set; }
        //Release Date
        public DateTime ReleaseDate{ get; set; }
        //Quantity of current part in mold
        public int Quantity { get; set; }
        //Version number of current part(V00, V01...)
        public string Version { get; set; }
        //Validate tag of the part(deleted or not
        public bool Enabled { get; set; }
        //Path of the 3D file
        public string ModelPath { get; set; }        
        //Path of the 2D file
        public string DrawingPath { get; set; }
        //Size recorded in product catalog
        public string CatalogSpec { get; set; }
        //Status of the part(in work, released, obsoleted)
        public int Status { get; set; }

        //Append quantity
        public int AppendQty { get; set; }
        //Total Quantity
        public int TotalQty { get; set; }
        //Virtual Part Tag
        public bool Virtual { get; set; }

        //Whether Part is from UG
        public bool FromUG { get; set; }

        //Whether Part is purchased or not
        public bool InPurchase { get; set; }

        public int PartListID { get; set; }

        public bool Locked { get; set; }
        /// <summary>
        /// 新建零件 true
        /// </summary>
        public bool Latest { get; set; }

        public string ERPPartID { get; set; }
    }
}
