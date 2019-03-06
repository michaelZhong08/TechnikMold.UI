using Common;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using TechnikSys.MoldManager.Domain.Entity;
using TechnikSys.MoldManager.Domain.ViewModel;

namespace DAL
{
    public class WorkHourDal
    {
        static string constr;
        DataAccess db;
        public WorkHourDal(string conStr)
        {
            constr = conStr;
            db =new DataAccess(constr, CommandType.StoredProcedure);
        }
        
        #region 人工工时
        public int Save_EmpWHRecords(TaskHoursEmp model)
        {
            string procName = "Proc_WHEmp_SaveWHRecord";
            DateTime _sTime = model.StartTime;
            DateTime _eTime = model.EndTime == new DateTime(1, 1, 1) ? new DateTime(1900, 1, 1) : model.EndTime;
            DateTime _approvalTime = model.ApprovalTime == new DateTime(1, 1, 1) ? new DateTime(1900, 1, 1) : model.ApprovalTime;
            DateTime _docDate = DateTime.Parse(_sTime.ToString("yyyy-MM-dd"));
            //User user = (_userRepository.GetUserByCode(model.EmpCode) ?? new TechnikSys.MoldManager.Domain.Entity.User());
            SqlParameter[] prams =
            {
                       db.MakeInParam("@Id",SqlDbType.Int,100,model.Id),
                       db.MakeInParam("@EmpCode",SqlDbType.NVarChar,100,model.EmpCode),
                       db.MakeInParam("@EmpName",SqlDbType.NVarChar,100,model.EmpName??""),
                       db.MakeInParam("@DepID",SqlDbType.Int,100,model.DepID),
                       db.MakeInParam("@DocDate",SqlDbType.DateTime,100,_docDate),
                       db.MakeInParam("@StartTime",SqlDbType.DateTime,100,model.StartTime),
                       db.MakeInParam("@EndTime",SqlDbType.DateTime,100,_eTime),
                       db.MakeInParam("@CreateTime",SqlDbType.DateTime,100,DateTime.Now),
                       db.MakeInParam("@Enable",SqlDbType.Bit,100,model.Enable),
                       db.MakeInParam("@MoldNumber",SqlDbType.NVarChar,100,model.MoldNumber),
                       db.MakeInParam("@WorkType",SqlDbType.NVarChar,100,model.WorkType),
                       db.MakeInParam("@BC",SqlDbType.NVarChar,100,model.BC??""),
                       db.MakeInParam("@MachineCode",SqlDbType.NVarChar,100,model.MachineCode??""),
                       db.MakeInParam("@Status",SqlDbType.Int,100,model.Status),
                       db.MakeInParam("@Time",SqlDbType.Float,100,model.Time),
                       db.MakeInParam("@ApprovalUser",SqlDbType.Int,100,model.ApprovalUser),
                       db.MakeInParam("@ApprovalTime",SqlDbType.DateTime,100,_approvalTime),
                    };
            int r = db.RunProc(procName, prams);
            return r;
        }
        public string Chk_EmpWHRecords(TaskHoursEmp model)
        {
            string res = string.Empty;
            if (model.StartTime > DateTime.Now || model.EndTime > DateTime.Now)
            {
                res = "开始or结束时间 不能超过当前时间！";
            }
            else
            {
                string procName = "Proc_WHEmp_ChkEmpWHRecord";
                string tName = "WHRecordA";
                SqlParameter[] prams =
                {
                db.MakeInParam("@Id",SqlDbType.Int,100,model.Id),
                db.MakeInParam("@STime",SqlDbType.DateTime,100,model.StartTime),
                db.MakeInParam("@ETime",SqlDbType.DateTime,100,model.EndTime),
                db.MakeInParam("@EmpCode",SqlDbType.NVarChar,100,model.EmpCode),
            };
                DataTable dt = db.RunProcReturn(procName, prams, tName).Tables[0];
                if (dt.Rows.Count > 0)
                {
                    int ChkFlag = Convert.ToInt32(dt.Rows[0]["ChkFlag"]);
                    if (ChkFlag == 1)
                    {
                        res = "当前用户存在未分配结束的任务工时——\r\n";
                        for (var i = 0; i < dt.Rows.Count; i++)
                        {
                            var taskTypeName = dt.Rows[i]["TaskTypeName"].ToString();
                            var taskName = dt.Rows[i]["TaskName"].ToString();
                            res = res+ taskTypeName + "：" + taskName + ";\r\n";
                        }
                    }
                    else
                    {
                        res = "当前用户以下时间段工时与本次记录冲突——\r\n";
                        foreach (DataRow row in dt.Rows)
                        {
                            TaskHourEmpViewModel dbEntry = DtToList.FillModel<TaskHourEmpViewModel>(row);
                            res = res + dbEntry.StartTime + "~" + dbEntry.EndTime + ";\r\n";
                        }
                    }
                }
            }
            return res;
        }
        public TaskHourEmpViewModel Get_EmpRecordById(int id)
        {
            string procName = "Proc_WHEmp_GetRecordById";
            string tName = "WHRecordA";
            SqlParameter[] prams =
            {
                        db.MakeInParam("@Id",SqlDbType.Int,100,id),
                    };
            DataTable dt = db.RunProcReturn(procName, prams, tName).Tables[0];
            if (dt.Rows.Count > 0)
            {
                TaskHourEmpViewModel dbEntry = DtToList.FillModel<TaskHourEmpViewModel>(dt.Rows[0]);
                return dbEntry;
            }
            return new TaskHourEmpViewModel();
        }
        public DataTable Get_EmpWHRecordsByDay(string EmpCode)
        {
            string procName = "Proc_WHEmp_GetWHRecordByDay";
            string tbName = "WHReportDataA2";
            SqlParameter[] prams =
            {
                db.MakeInParam("@EmpCode",SqlDbType.NVarChar,100,EmpCode),
            };
            DataTable dt = db.RunProcReturn(procName, prams, tbName).Tables[0];
            return dt;
        }
        public DataTable Get_EmpWHReportDataA(int Status, string WorkType, DateTime? startTimeFr, DateTime? startTimeTo, DateTime? finishTimeFr, DateTime? finishTimeTo, int Depid)
        {
            string procName = "Proc_WHEmp_GetRecordDataA";
            string tbName = "EmpWHReportDataA1";
            SqlParameter[] prams =
            {
                db.MakeInParam("@Status",SqlDbType.Int,100,Status),
                db.MakeInParam("@DepID",SqlDbType.Int,10,Depid),
                db.MakeInParam("@WorkType",SqlDbType.NVarChar,100,WorkType),
                db.MakeInParam("@startTimeFr",SqlDbType.DateTime,100,startTimeFr??new DateTime(1900,1,1)),
                db.MakeInParam("@startTimeTo",SqlDbType.DateTime,100,startTimeTo??new DateTime(2200,1,1)),
                db.MakeInParam("@finishTimeFr",SqlDbType.DateTime,100,finishTimeFr??new DateTime(1900,1,1)),
                db.MakeInParam("@finishTimeTo",SqlDbType.DateTime,100,finishTimeTo??new DateTime(2200,1,1)),
            };
            DataTable dt = db.RunProcReturn(procName, prams, tbName).Tables[0];
            return dt;
        }
        public void Upt_EmpRecordState(int id, int status)
        {
            string procName = "Proc_WHEmp_UptRecordState";

            SqlParameter[] prams =
            {
                 db.MakeInParam("@Id",SqlDbType.Int,100,id),
                 db.MakeInParam("@Status",SqlDbType.Int,100,status),
            };
            db.RunProc(procName, prams);
        }
        #endregion

        #region 机器工时
        public DataTable Get_MacWHReportDataA(string mCode, int taskType, DateTime? startTimeFr, DateTime? startTimeTo, DateTime? finishTimeFr, DateTime? finishTimeTo)
        {
            string procName = "Proc_WH_GetWHReportDataA";
            string tbName = "WHReportDataA1";
            SqlParameter[] prams =
            {
                db.MakeInParam("@MCode",SqlDbType.NVarChar,100,mCode),
                db.MakeInParam("@TaskType",SqlDbType.Int,10,taskType),
                db.MakeInParam("@startTimeFr",SqlDbType.DateTime,100,startTimeFr??new DateTime(1900,1,1)),
                db.MakeInParam("@startTimeTo",SqlDbType.DateTime,100,startTimeTo??new DateTime(2200,1,1)),
                db.MakeInParam("@finishTimeFr",SqlDbType.DateTime,100,finishTimeFr??new DateTime(1900,1,1)),
                db.MakeInParam("@finishTimeTo",SqlDbType.DateTime,100,finishTimeTo??new DateTime(2200,1,1)),
            };
            DataTable dt = db.RunProcReturn(procName, prams, tbName).Tables[0];
            return dt;
        }
        #endregion
    }
}
