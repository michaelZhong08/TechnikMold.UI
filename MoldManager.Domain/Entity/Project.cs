/*
 * Create By:lechun1
 * 
 * Description:data represent a project basic information
 * 
 */



using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TechnikSys.MoldManager.Domain.Entity
{
    public class Project
    {
        public int ProjectID { get; set; }
        public string ProjectNumber { get; set; }
        public string Name { get; set; }
        public int Version { get; set; }

        //Specifies the project type
        //0:main project
        //1: new mold project
        //2: mold fix projext

        public int Type { get; set; }
        public string MoldNumber { get; set; }
        public int CustomerID { get; set; }
        public string CustomerName { get; set; }
        public int ProjectStatus { get; set; }
        public string Memo { get; set; }
        public int ParentID { get; set; }
        public bool Enabled { get; set; }
        public string Attachment { get; set; }
        public int OldID { get; set; }
        public DateTime CreateTime { get; set; }
        public DateTime FinishTime { get; set; }
        public string FixMoldType { get; set; }
        public bool MainPhaseChange { get; set; }
        
        public bool IsPublish { get; set; }

        public string Creator { get; set; }
        public Project()
        {
            ProjectID=0;
            ProjectNumber="";
            Name="";
            Version=0;
            Type=0;
            MoldNumber="";
            CustomerID=0;
            CustomerName="";
            ProjectStatus=0;
            Memo="";
            ParentID=0;
            Enabled=true;
            Attachment="";
            OldID = -1;
            CreateTime=DateTime.Now;
            FinishTime=new DateTime(1900,1,1);
            FixMoldType="";
            MainPhaseChange=false;
            Creator = "";
        }
    }
}
