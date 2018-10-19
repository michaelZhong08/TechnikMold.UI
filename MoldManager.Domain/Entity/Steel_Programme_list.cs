using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace TechnikSys.MoldManager.Domain.Entity
{
    public class Steel_Programme_list
    {
        [Key]
        public int ID  { get; set; }
        public int GroupID { get; set; }
	    public string ProgrammeName { get; set; }
	    public string FileName{ get; set; }
	    public string ToolName{ get; set; }
	    public double time{ get; set; }
	    public double Depth{ get; set; }
	    public bool bHaveFile{ get; set; }
	    public int sequence { get; set; }
	    public bool active  { get; set; }
    }
}
