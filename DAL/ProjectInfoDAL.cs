using Common;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL
{
    public class ProjectInfoDAL
    {
        static string constr;
        DataAccess db;
        public ProjectInfoDAL(string conStr)
        {
            constr = conStr;
            db = new DataAccess(constr, CommandType.StoredProcedure);
        }
        public DataTable Get_TBTMPL_PROJ(string Keyword = "", int Type = 1, int DepID = 18, bool isDepFinish = false)
        {
            string procName = "Proc_GETTBTMPLCONTENT_PROJECTS";
            string tbName = "PROJTBTMPLDATA";
            SqlParameter[] prams =
            {
                db.MakeInParam("@Keyword",SqlDbType.NVarChar,100,Keyword),
                db.MakeInParam("@Type",SqlDbType.Int,8,Type),
                db.MakeInParam("@DepID",SqlDbType.Int,8,DepID),
                db.MakeInParam("@DepFinished",SqlDbType.Bit,8,isDepFinish),
            };
            DataTable dt = db.RunProcReturn(procName, prams, tbName).Tables[0];
            return dt;
        }
    }
}
