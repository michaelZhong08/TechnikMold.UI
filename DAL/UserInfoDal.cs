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
    public class UserInfoDal
    {
        static string constr;
        DataAccess db;
        public UserInfoDal(string conStr)
        {
            constr = conStr;
            db = new DataAccess(constr, CommandType.StoredProcedure);
        }
        #region User Valid
        public DataTable Get_UserIngoDataA(string FullName)
        {
            string procName = "Proc_User_GetUserInfo";
            string tbName = "UserInfos";
            SqlParameter[] prams =
            {
                db.MakeInParam("@FullName",SqlDbType.NVarChar,100,FullName),
            };
            DataTable dt = db.RunProcReturn(procName, prams, tbName).Tables[0];
            return dt;
        }
        #endregion
    }
}
