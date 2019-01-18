using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;

namespace Common
{
    public class DataAccess
    {
        private string constr;
        private CommandType type;
        SqlConnection conn;
        #region 构造
        //private DBHelper unDBHelper;
        public DataAccess(string Constr, CommandType Type)
        {
            this.constr = Constr;
            this.type = Type;
        }
        //public DBHelper GetDBHelper(string Constr, CommandType Type)
        //{
        //    if (unDBHelper != null)
        //    {
        //        unDBHelper = new DBHelper(Constr, Type);
        //    }
        //    return unDBHelper;
        //}
        #endregion
        #region  DataConn Class
        /// <summary>
        /// open data connect
        /// </summary>
        private void open()
        {
            if (conn == null)
            {
                conn = new SqlConnection(constr);
            }
            if (conn.State == System.Data.ConnectionState.Closed)
            {
                conn.Open();
            }
        }
        /// <summary>
        /// close data connection
        /// </summary>
        public void close()
        {
            if (conn != null)
            {
                conn.Close();
            }
        }
        /// <summary>
        /// dispose data source
        /// </summary>
        public void dispose()
        {
            if (conn != null)
            {
                conn.Dispose();
                conn = null;
            }
        }
        #endregion
        #region   传入参数并且转换为SqlParameter类型
        /// <summary>
        /// 转换参数
        /// </summary>
        /// <param name="ParamName"></param>
        /// <param name="DbType"></param>
        /// <param name="Size"></param>
        /// <param name="Value"></param>
        /// <returns></returns>
        public SqlParameter MakeInParam(string ParamName, SqlDbType DbType, int Size, object Value)
        {
            if (Value == null)
            {
                return MakeParam(ParamName, DbType, Size, ParameterDirection.Input, "");
            }
            return MakeParam(ParamName, DbType, Size, ParameterDirection.Input, Value);
        }
        /// <summary>
        /// 初始化参数
        /// </summary>
        /// <param name="ParamName"></param>
        /// <param name="DbType"></param>
        /// <param name="Size"></param>
        /// <param name="Direction"></param>
        /// <param name="Value"></param>
        /// <returns></returns>
        public SqlParameter MakeParam(string ParamName, SqlDbType DbType, Int32 Size, ParameterDirection Direction, object Value)
        {
            SqlParameter param;
            if (Size > 0)
                param = new SqlParameter(ParamName, DbType, Size);
            else
                param = new SqlParameter(ParamName, DbType);
            param.Direction = Direction;
            if (!(Direction == ParameterDirection.Output && Value == null))
                param.Value = Value;
            return param;
        }
        #endregion
        #region data input  operation
        /// <summary>
        /// 直接执行sql语句
        /// </summary>
        /// <param name="procName"></param>
        /// <returns></returns>
        public int RunProc(string procName)
        {
            this.open();
            SqlCommand cmd = new SqlCommand(procName, conn);
            cmd.ExecuteNonQuery();
            this.close();
            return 1;
        }
        /// <summary>
        /// 执行带参的命令
        /// </summary>
        /// <param name="procName"></param>
        /// <param name="prams"></param>
        /// <returns></returns>
        public int RunProc(string procName, SqlParameter[] prams)
        {
            SqlCommand cmd = CreateCommand(procName, prams);
            cmd.ExecuteNonQuery();
            this.close();
            return (int)cmd.Parameters["ReturnValue"].Value;//Get Return Right Value
        }
        #endregion
        #region create dataadapter to get data
        /// <summary>
        /// 带参桥接器
        /// </summary>
        /// <param name="procName"></param>
        /// <param name="prams"></param>
        /// <param name="tbName"></param>
        /// <returns></returns>
        public DataSet RunProcReturn(string procName, SqlParameter[] prams, string tbName)
        {
            SqlDataAdapter dap = CreateDataAdapter(procName, prams);
            DataSet ds = new DataSet();
            dap.Fill(ds, tbName);//在dataset中添加或刷新行以匹配使用
            this.close();
            return ds;
        }
        /// <summary>
        /// 不带参桥接器
        /// </summary>
        /// <param name="procName"></param>
        /// <param name="tbNaem"></param>
        /// <returns></returns>
        public DataSet RunProcReturn(string procName, string tbName)
        {
            SqlDataAdapter dap = CreateDataAdapter(procName, null);
            DataSet ds = new DataSet();
            dap.Fill(ds, tbName);
            this.close();
            return ds;
        }
        #endregion
        #region 创建桥接器与commamd对象，执行存储过程
        private SqlDataAdapter CreateDataAdapter(string procName, SqlParameter[] prams)
        {
            this.open();
            SqlDataAdapter dap = new SqlDataAdapter(procName, conn);
            dap.SelectCommand.CommandType = type;
            if (prams != null)
            {
                foreach (SqlParameter parameter in prams)
                    dap.SelectCommand.Parameters.Add(parameter);
            }
            dap.SelectCommand.Parameters.Add(new SqlParameter("ReturnValue", SqlDbType.Int, 4,
                ParameterDirection.ReturnValue, false, 0, 0, string.Empty, DataRowVersion.Default, null));
            return dap;
        }

        public SqlCommand CreateCommand(string procName, SqlParameter[] prams)
        {
            this.open();
            SqlCommand cmd = new SqlCommand(procName, conn);
            cmd.CommandType = type;
            if (prams != null)
            {
                foreach (SqlParameter parameter in prams)
                    cmd.Parameters.Add(parameter);
            }
            cmd.Parameters.Add(new SqlParameter("ReturnValue", SqlDbType.Int, 4,
                ParameterDirection.ReturnValue, false, 0, 0, string.Empty, DataRowVersion.Default, null));
            return cmd;
        }
        #endregion
    }
}
