using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using TechnikSys.MoldManager.Domain.Entity;

namespace TechnikMold.UI.Models.GridRowModel
{
    public class AttachGridRowModel
    {
        public string[] cell;
        public AttachGridRowModel(AttachFileInfo model)
        {
            cell = new string[8];
            cell[0] = model.ObjID;
            cell[1] = model.ObjType;
            cell[2] = model.FilePath;
            cell[3] = model.FileName;
            cell[4] = model.FileType;
            cell[5] = model.FileSize.ToString();
            cell[6] = model.CreateTime.ToString("yyyy-MM-dd");
            cell[7] = model.Creator;
        }
    }
}