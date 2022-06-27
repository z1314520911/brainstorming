using System;
using System.Collections.Generic;
using System.Text;
using System.Data.SqlClient;
using System.Data;
using System.Reflection;
using DBHelper;
using Commons;

namespace DAL
{
    public partial class ModelService_Sql
    {
        /// <summary>
        /// 单条更新信息
        /// </summary>
        /// <param name="tableName">表名</param>
        /// <param name="UpdateName">更新用户</param>
        /// <param name="id">更新ID</param>
        /// <param name="UpdateInfo">要更新的数据</param>
        /// <returns>返回整数验证数据执行是否成功</returns>
        public int UpdateInfo(string tableName, int Id, string UpdateInfo, params SqlParameter[] myParam)
        {
            string sql = "Update " + Config.DatabasePrefix(tableName) + " set " + UpdateInfo + " where Id=@Id";
            SqlParameter[] param = new SqlParameter[]
            {
                new SqlParameter("@Id",SqlDbType.BigInt,0,ParameterDirection.Input,false,0,0,"Id",DataRowVersion.Current,Id)
            };
            param = MergerSqlParameter(myParam, param);
            int n = SqlHelper.ExecuteNonQuery(SqlHelper.connstr, CommandType.Text, sql, param);
            return n;
        }
        /// <summary>
        /// 通过用户名更新单条信息
        /// </summary>
        /// <param name="tableName">表名</param>
        /// <param name="UpdateName">更新用户</param>
        /// <param name="id">更新ID</param>
        /// <param name="UpdateInfo">要更新的数据</param>
        /// <param name="UserName">用户名</param>
        /// <returns>返回整数验证数据执行是否成功</returns>
        public int UpdateInfo(string tableName, string UpdateName, int Id, string UpdateInfo, string UserName, params SqlParameter[] myParam)
        {
            string sql = "Update " + Config.DatabasePrefix(tableName) + " set " + UpdateInfo + ",UpdateName=@UpdateName,UpdateTime=@UpdateTime where Id=@Id and AddName=@AddName";
            SqlParameter[] param = new SqlParameter[]
            {
                new SqlParameter("@UpdateName",SqlDbType.VarChar,50,ParameterDirection.Input,false,0,0,"UpdateName",DataRowVersion.Current,UpdateName),
                new SqlParameter("@UpdateTime",SqlDbType.Date,0,ParameterDirection.Input,false,0,0,"UpdateTime",DataRowVersion.Current,DateTime.Now),
                new SqlParameter("@Id",SqlDbType.BigInt,0,ParameterDirection.Input,false,0,0,"Id",DataRowVersion.Current,Id),
                new SqlParameter("@AddName",SqlDbType.VarChar,50,ParameterDirection.Input,false,0,0,"AddName",DataRowVersion.Current,UserName)

            };
            param = MergerSqlParameter(myParam, param);
            int n = SqlHelper.ExecuteNonQuery(SqlHelper.connstr, CommandType.Text, sql, param);
            return n;
        }
        /// <summary>
        /// 批量更新信息
        /// </summary>
        /// <param name="tableName">表名</param>
        /// <param name="UpdateName">更新用户</param>
        /// <param name="id">更新ID</param>
        /// <param name="UpdateInfo">要更新的数据</param>
        /// <returns></returns>
        public int UpdateInfo(string tableName, string Id, string UpdateInfo, params SqlParameter[] myParam)
        {
            string sql1 = "";
            SqlParameter[] param = SplitId(Id, ref sql1);
            string sql = "Update " + Config.DatabasePrefix(tableName) + " set " + UpdateInfo + " where Id in (" + sql1 + ")";
            param = MergerSqlParameter(myParam, param);
            int n = SqlHelper.ExecuteNonQuery(SqlHelper.connstr, CommandType.Text, sql, null);
            return n;
        }
        /// <summary>
        /// 通过条件更新数据
        /// </summary>
        /// <param name="tableName">表名</param>
        /// <param name="UpdateInfo">要更新的数据</param>
        /// <param name="SelectWhere">更新条件</param>
        /// <returns></returns>
        public int UpdateInfoBySelectWhere(string tableName, string UpdateInfo, string SelectWhere, params SqlParameter[] param)
        {
            string sql = "Update " + Config.DatabasePrefix(tableName) + " set " + UpdateInfo + (SelectWhere == "" ? "" : " where " + SelectWhere);
            int n = SqlHelper.ExecuteNonQuery(SqlHelper.connstr, CommandType.Text, sql, param);
            return n;
        }
        /// <summary>
        /// 通过用户名批量更新信息
        /// </summary>
        /// <param name="tableName">表名</param>
        /// <param name="UpdateName">更新用户</param>
        /// <param name="id">更新ID</param>
        /// <param name="UpdateInfo">要更新的数据</param>
        /// <param name="UserName">用户名</param>
        /// <returns></returns>
        public int UpdateInfo(string tableName, string UpdateName, string Id, string UpdateInfo, string UserName, params SqlParameter[] myParam)
        {
            string sql = "Update " + Config.DatabasePrefix(tableName) + " set " + UpdateInfo + ",UpdateName=@UpdateName,UpdateTime=@UpdateTime where Id in (" + Id + ") and AddName=@AddName";
            SqlParameter[] param = new SqlParameter[]
            {
                new SqlParameter("@UpdateName",SqlDbType.VarChar,50,ParameterDirection.Input,false,0,0,"UpdateName",DataRowVersion.Current,UpdateName),
                new SqlParameter("@UpdateTime",SqlDbType.Date,0,ParameterDirection.Input,false,0,0,"UpdateTime",DataRowVersion.Current,DateTime.Now),
                new SqlParameter("@AddName",SqlDbType.VarChar,50,ParameterDirection.Input,false,0,0,"AddName",DataRowVersion.Current,UserName)
            };
            param = MergerSqlParameter(myParam, param);
            int n = SqlHelper.ExecuteNonQuery(SqlHelper.connstr, CommandType.Text, sql, param);
            return n;
        }

        #region 函数

        /// <summary>
        /// 拆分Id
        /// </summary>
        /// <param name="Id"></param>
        /// <param name="sql1"></param>
        /// <returns></returns>
        public SqlParameter[] SplitId(string Id, ref string sql1)
        {
            string[] strings = Id.Split(',');
            SqlParameter[] param = new SqlParameter[strings.Length];
            for (int i = 0; i < strings.Length; i++)
            {
                sql1 += "@Id" + i + ",";
                param[i] = new SqlParameter("@Id" + i, SqlDbType.Int, 0) { Value = Common.IsInt(strings[i]) };
            }
            sql1 = sql1.TrimEnd(',');
            return param;
        }
        /// <summary>
        /// 合并2个SqlParameter
        /// </summary>
        /// <param name="FirstSqlParameter">要合并的SqlParameter</param>
        /// <param name="SecondSqlParameter">原始SqlParameter</param>
        /// <returns></returns>
        public SqlParameter[] MergerSqlParameter(SqlParameter[] FirstSqlParameter, SqlParameter[] SecondSqlParameter)
        {

            if (FirstSqlParameter.Length > 0)
            {
                List<SqlParameter> list = new List<SqlParameter>();
                foreach (SqlParameter parm in FirstSqlParameter)
                {
                    list.Add(parm);
                }
                foreach (SqlParameter parm in SecondSqlParameter)
                {
                    list.Add(parm);
                }
                return list.ToArray();
            }
            return SecondSqlParameter;
        }

        #endregion
    }
}
