﻿/*
 * Create By:lechun1
 * 
 * Description: Data represents internal department
 * 
 */



using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TechnikSys.MoldManager.Domain.Entity
{
    public class Department
    {
        public int DepartmentID { get; set; }
        public string Name { get; set; }
        public bool Enabled { get; set; }
    }
}
