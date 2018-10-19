using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TechnikSys.MoldManager.Domain.Entity
{
    public class CAMDrawing
    {
        public	int	CAMDrawingID	{ get; set; }
        public	string	DrawingName	{ get; set; }
        public	string	MoldName	{ get; set; }
        public	bool	Lock	{ get; set; }
        public	DateTime	CreateDate	{ get; set; }
        public	string	CreateBy	{ get; set; }
        public	DateTime	ReleaseDate	{ get; set; }
        public	string	ReleaseBy	{ get; set; }
        public	bool	active	{ get; set; }
    }
}
