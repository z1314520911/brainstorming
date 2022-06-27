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
        /// 删除数据
        /// </summary>
        /// <param name="tableName">表</param>
        /// <param name="Id">序号ID</param>
        /// <returns></returns>
        public int Delete(string tableName, int Id)
        {
            string sql = "Delete from " + Config.DatabasePrefix(tableName) + " where Id=@Id";
            SqlParameter[] param = new SqlParameter[]
            {
                new SqlParameter("@Id",SqlDbType.BigInt,0,ParameterDirection.Input,false,0,0,"Id",DataRowVersion.Current,Id)

            };
            int n = SqlHelper.ExecuteNonQuery(SqlHelper.connstr, CommandType.Text, sql, param);
            return n;
        }
        /// <summary>
        /// 删除数据
        /// </summary>
        /// <param name="tableName">表</param>
        /// <param name="Id">序号ID</param>
        /// <param name="SelectWhere">查询条件</param>
        /// <param name="myParam">参数</param>
        /// <returns></returns>
        public int Delete(string tableName, int Id, string SelectWhere, params SqlParameter[] myParam)
        {
            string sql = "Delete from " + Config.DatabasePrefix(tableName) + " where Id=@Id" + (string.IsNullOrEmpty(SelectWhere) ? "" : " and " + SelectWhere);
            SqlParameter[] param = new SqlParameter[]
            {
                new SqlParameter("@Id",SqlDbType.BigInt,0,ParameterDirection.Input,false,0,0,"Id",DataRowVersion.Current,Id)

            };
            param = MergerSqlParameter(myParam, param);
            int n = SqlHelper.ExecuteNonQuery(SqlHelper.connstr, CommandType.Text, sql, param);
            return n;
        }
        /// <summary>
        /// 批量删除数据
        /// </summary>
        /// <param name="tableName">表</param>
        /// <param name="Id">序号ID</param>
        /// <returns></returns>
        public int DeleteAll(string tableName, string Id)
        {
            string sql1 = "";
            SqlParameter[] param = SplitId(Id, ref sql1);
            string sql = "Delete from " + Config.DatabasePrefix(tableName) + " where Id in (" + sql1 + ")";
            int n = SqlHelper.ExecuteNonQuery(SqlHelper.connstr, CommandType.Text, sql, param);
            return n;
        }
        /// <summary>
        /// 简单查询语句
        /// </summary>
        /// <param name="PageSiz">条数</param>
        /// <param name="Field">字段</param>
        /// <param name="tableName">表</param>
        /// <param name="SelectWhere">查询条件</param>
        /// <param name="OrderBy">排序</param>
        /// <returns></returns>
        public DataTable getPage(int PageSiz, string Field, string tableName, string SelectWhere, string OrderBy, params SqlParameter[] param)
        {
            SqlConnection conn = new SqlConnection(SqlHelper.connstr);
            try
            {
                string sql = "select " + (PageSiz == 0 ? "" : " top " + PageSiz) + (Field == "" ? " * " : " " + Field) + "  from " + Config.DatabasePrefix(tableName) + " " + (SelectWhere == "" ? "" : " where " + SelectWhere) + "  " + (OrderBy == "" ? "" : " order by " + OrderBy) + " ";
                SqlDataReader reader = SqlHelper.ExecuteReader(SqlHelper.connstr, CommandType.Text, sql, param);
                return Convertdrtodt(reader);


                //string sql = "select " + (PageSiz == 0 ? "" : " top " + PageSiz) + (Field == "" ? " * " : " " + Field) + "  from [" + tableName + "] " + (SelectWhere == "" ? "" : " where " + SelectWhere) + "  " + (OrderBy == "" ? "" : " order by " + OrderBy) + " ";
                //SqlDataAdapter da = new SqlDataAdapter(sql, conn);
                //da.Fill(ds, "tempTable");
                //SqlDataAdapter da = new SqlDataAdapter();
                //da.SelectCommand = cmd;
                //da.Fill(dt);
            }
            catch(Exception e)
            {
                string aa = e.Message;
                conn.Close();
                //throw (e);
            }
            finally
            {
                conn.Close();
            }
            return new DataTable();
        }
        /// <summary>
        /// 简单查询语句(联表)
        /// </summary>
        /// <param name="PageSiz">条数</param>
        /// <param name="Field">字段</param>
        /// <param name="tableName">表</param>
        /// <param name="JoinOn">联查</param>
        /// <param name="SelectWhere">查询条件</param>
        /// <param name="OrderBy">排序</param>
        /// <returns></returns>
        public DataTable getPage(int PageSiz, string Field, string tableName, string JoinOn, string SelectWhere, string OrderBy, params SqlParameter[] param)
        {
            SqlConnection conn = new SqlConnection(SqlHelper.connstr);
            try
            {
                string sql = "select " + (PageSiz == 0 ? "" : " top " + PageSiz) + (Field == "" ? " * " : " " + Field) + "  from " + Config.DatabasePrefix(tableName) + " " + JoinOn + " " + (SelectWhere == "" ? "" : " where " + SelectWhere) + "  " + (OrderBy == "" ? "" : " order by " + OrderBy) + " ";
                SqlDataReader reader = SqlHelper.ExecuteReader(SqlHelper.connstr, CommandType.Text, sql, param);
                return Convertdrtodt(reader);
            }
            catch
            {
                conn.Close();
                //throw (e);
            }
            finally
            {
                conn.Close();
            }
            return new DataTable();
        }
        /// <summary>
        /// 分页查询
        /// </summary>
        /// <param name="intPageNo">页数</param>
        /// <param name="PageSiz">条数</param>
        /// <param name="Field">字段</param>
        /// <param name="tableName">表</param>
        /// <param name="SelectWhere">查询条件</param>
        /// <param name="OrderBy">排序</param>
        /// <returns></returns>
        public DataTable GetAll(int intPageNo, int PageSiz, string Field, string tableName, string SelectWhere, string OrderBy, params SqlParameter[] param)
        {
            DataTable dt = new DataTable();
            SqlConnection conn = new SqlConnection(SqlHelper.connstr);
            try
            {
                string sql;
                string selectwhere1, selectwhere2;
                if (string.IsNullOrEmpty(SelectWhere))
                {
                    selectwhere1 = "";
                    selectwhere2 = "";
                }
                else
                {
                    selectwhere1 = " as z where " + SelectWhere;
                    selectwhere2 = SelectWhere + " and ";
                }
                //OrderBy = (OrderBy == "" ? "" : " order by " + OrderBy);
                OrderBy = (OrderBy == "" ? "order by id desc" : " order by " + OrderBy);
                Field = (Field == "" ? " * " : " " + Field);

                sql = "select * from (select " + Field + " ,row_number() over(" + OrderBy + ") rownumber from " + Config.DatabasePrefix(tableName) + " " + selectwhere1 + ") a where rownumber between " + (1 + (intPageNo - 1) * PageSiz) + " and " + (intPageNo * PageSiz);

                //SqlDataAdapter da = new SqlDataAdapter(sql, conn);
                ////da.Fill(dt, "tempTable");
                //da.Fill(dt);

                SqlDataReader reader = SqlHelper.ExecuteReader(SqlHelper.connstr, CommandType.Text, sql, param);
                return Convertdrtodt(reader);
            }
            catch
            {
                conn.Close();
                //throw (e);
            }
            finally
            {
                conn.Close();
            }
            return dt;
        }
        /// <summary>
        /// 分页查询(联表)
        /// </summary>
        /// <param name="intPageNo">页数</param>
        /// <param name="PageSiz">条数</param>
        /// <param name="Field">字段</param>
        /// <param name="tableName">表</param>
        /// <param name="JoinOn">联查</param>
        /// <param name="SelectWhere">查询条件</param>
        /// <param name="OrderBy">排序</param>
        /// <returns></returns>
        public DataTable GetAll(int intPageNo, int PageSiz, string Field, string tableName, string JoinOn, string SelectWhere, string OrderBy, params SqlParameter[] param)
        {
            DataTable dt = new DataTable();
            SqlConnection conn = new SqlConnection(SqlHelper.connstr);
            try
            {
                string sql;
                string selectwhere1, selectwhere2;
                if (string.IsNullOrEmpty(SelectWhere))
                {
                    selectwhere1 = "";
                    selectwhere2 = "";
                }
                else
                {
                    selectwhere1 = " where " + SelectWhere;
                    selectwhere2 = SelectWhere + " and ";
                }
                //OrderBy = (OrderBy == "" ? "" : " order by " + OrderBy);
                OrderBy = (OrderBy == "" ? "order by id desc" : " order by " + OrderBy);
                Field = (Field == "" ? " * " : " " + Field);
                if (intPageNo == 1)
                {
                    sql = "select top " + PageSiz + " " + Field + " from " + Config.DatabasePrefix(tableName) + " " + JoinOn + " " + selectwhere1 + " " + OrderBy;
                }
                else
                {
                    //sql = "select top 50 * from [SMSRecord] where id>(select max(id) from (select top 9900 id from [SMSRecord] order by id)a) order by id";
                    sql = "select * from (select " + Field + " ,row_number() over(" + OrderBy + ") rownumber from " + Config.DatabasePrefix(tableName) + " " + JoinOn + " " + selectwhere1 + ") a where rownumber between " + (1 + (intPageNo - 1) * PageSiz) + " and " + (intPageNo * PageSiz);
                    //sql = "select top " + PageSiz + " " + Field + " from [" + Table + "] where " + selectwhere2 + " id not in(select top " + ((intPageNo - 1) * PageSiz) + " id from [" + Table + "] " + selectwhere1 + " " + OrderBy + ") " + OrderBy;
                }
                //SqlDataAdapter da = new SqlDataAdapter(sql, conn);
                ////da.Fill(dt, "tempTable");
                //da.Fill(dt);

                SqlDataReader reader = SqlHelper.ExecuteReader(SqlHelper.connstr, CommandType.Text, sql, param);
                return Convertdrtodt(reader);
            }
            catch
            {
                conn.Close();
                //throw (e);
            }
            finally
            {
                conn.Close();
            }
            return dt;
        }
        /// <summary>
        /// 分页查询(联表)
        /// </summary>
        /// <param name="intPageNo">页数</param>
        /// <param name="PageSiz">条数</param>
        /// <param name="Field">字段</param>
        /// <param name="tableName">表</param>
        /// <param name="JoinOn">联查</param>
        /// <param name="SelectWhere">查询条件</param>
        /// <param name="OrderBy">排序</param>
        /// <param name="TotalRow">数据总量</param>
        /// <returns></returns>
        public DataTable GetAll(int intPageNo, int PageSiz, string Field, string tableName, string JoinOn, string SelectWhere, string OrderBy, ref int TotalRow, params SqlParameter[] param)
        {
            TotalRow = AllPage(tableName, JoinOn, SelectWhere, param);
            return GetAll(intPageNo, PageSiz, Field, tableName, JoinOn, SelectWhere, OrderBy, param);
        }
        /// <summary>
        /// 分页查询
        /// </summary>
        /// <param name="intPageNo">页数</param>
        /// <param name="PageSiz">条数</param>
        /// <param name="Field">字段</param>
        /// <param name="tableName">表</param>
        /// <param name="SelectWhere">查询条件</param>
        /// <param name="OrderBy">排序</param>
        /// <param name="TotalRow">数据总量</param>
        /// <returns></returns>
        public DataTable GetAll(int intPageNo, int PageSiz, string Field, string tableName, string SelectWhere, string OrderBy, ref int TotalRow, params SqlParameter[] param)
        {
            TotalRow = AllPage(tableName, SelectWhere, param);
            return GetAll(intPageNo, PageSiz, Field, tableName, SelectWhere, OrderBy, param);
        }
        /// <summary>
        /// 获取数据总量
        /// </summary>
        /// <param name="tableName">表</param>
        /// <param name="SelectWhere">查询条件</param>
        /// <returns></returns>
        public int AllPage(string tableName, string SelectWhere, params SqlParameter[] param)
        {
            string sql = "select count(1) from " + Config.DatabasePrefix(tableName) + " as z " + (string.IsNullOrEmpty(SelectWhere) ? "" : " where " + SelectWhere);
            int count = int.Parse(SqlHelper.ExecuteScalar(SqlHelper.connstr, CommandType.Text, sql, param).ToString());
            return count;
        }
        /// <summary>
        /// 获取数据总量(联表)
        /// </summary>
        /// <param name="tableName">表</param>
        /// <param name="JoinOn">联查</param>
        /// <param name="SelectWhere">查询条件</param>
        /// <returns></returns>
        public int AllPage(string tableName, string JoinOn, string SelectWhere, params SqlParameter[] param)
        {
            string sql = "select count(1) from " + Config.DatabasePrefix(tableName) + " " + JoinOn + " " + (string.IsNullOrEmpty(SelectWhere) ? "" : " where " + SelectWhere);
            int count = int.Parse(SqlHelper.ExecuteScalar(SqlHelper.connstr, CommandType.Text, sql, param).ToString());
            return count;
        }
        /// <summary>
        /// 获取数据库最大的ID
        /// </summary>
        /// <param name="tableName">表名</param>
        /// <returns></returns>
        public int QueryBigId(string tableName)
        {
            int id = 1;
            string sql = "select max(Id) as Id from " + Config.DatabasePrefix(tableName);
            SqlDataReader reader = SqlHelper.ExecuteReader(SqlHelper.connstr, CommandType.Text, sql, null);
            while (reader.Read())
            {
                id = Common.IsInt(Convert.ToString(reader["Id"])) + 1;
            }
            reader.Close();
            return id;
        }

        /// <summary>
        /// 直接执行SQL语句
        /// </summary>
        /// <param name="sql">SQL语句</param>
        /// <returns></returns>
        public int ExecuteSql(string sql, params SqlParameter[] param)
        {
            int n = SqlHelper.ExecuteNonQuery(SqlHelper.connstr, CommandType.Text, sql, param);
            return n;
        }

        /// <summary>
        /// 直接执行SQL语句
        /// </summary>
        /// <param name="sql">SQL语句</param>
        /// <returns></returns>
        public DataTable ExecuteSqlAll(string sql, params SqlParameter[] param)
        {
            DataTable dt = new DataTable();
            SqlConnection conn = new SqlConnection(SqlHelper.connstr);
            try
            {
                CommandType cType = CommandType.Text;
                if (sql.IndexOf("exec [") != -1)
                {
                    cType = CommandType.StoredProcedure;
                }
                SqlDataReader reader = SqlHelper.ExecuteReader(SqlHelper.connstr, cType, sql, param);
                
                return Convertdrtodt(reader);
            }
            catch(Exception ex)
            {
                conn.Close();
                throw (ex);
            }
            finally
            {
                conn.Close();
            }
            return dt;
        }

        /// <summary>
        /// 通过ID获取数据
        /// </summary>
        /// <typeparam name="T">Model类</typeparam>
        /// <param name="Id">序号Id</param>
        /// <returns></returns>
        public T QueryByID<T>(int Id)
        {
            T list = System.Activator.CreateInstance<T>();
            Type type = list.GetType();
            string sql = "select top 1 * from " + Config.DatabasePrefix(type.Name) + " where Id=" + Id;
            SqlDataReader reader = SqlHelper.ExecuteReader(SqlHelper.connstr, CommandType.Text, sql, null);
            while (reader.Read())
            {
                Info<T>(ref list, reader);
            }
            reader.Close();
            return list;
        }

        /// <summary>
        /// 通过OpenId获取数据
        /// </summary>
        /// <typeparam name="T">Model类</typeparam>
        /// <param name="UserId">用户OpenId</param>
        /// <returns></returns>
        public T QueryByOpenId<T>(string OpenId)
        {
            T list = System.Activator.CreateInstance<T>();
            Type type = list.GetType();
            string sql = "select top 1 * from " + Config.DatabasePrefix(type.Name) + " where OpenId='" + OpenId + "'";
            SqlDataReader reader = SqlHelper.ExecuteReader(SqlHelper.connstr, CommandType.Text, sql, null);
            while (reader.Read())
            {
                Info<T>(ref list, reader);
            }
            reader.Close();
            return list;
        }

        /// <summary>
        /// 通过UserId获取数据
        /// </summary>
        /// <typeparam name="T">Model类</typeparam>
        /// <param name="UserId">用户Id</param>
        /// <returns></returns>
        public T QueryByUserId<T>(int UserId)
        {
            T list = System.Activator.CreateInstance<T>();
            Type type = list.GetType();
            string sql = "select top 1 * from " + Config.DatabasePrefix(type.Name) + " where UserId=" + UserId;
            SqlDataReader reader = SqlHelper.ExecuteReader(SqlHelper.connstr, CommandType.Text, sql, null);
            while (reader.Read())
            {
                Info<T>(ref list, reader);
            }
            reader.Close();
            return list;
        }

        /// <summary>
        /// 通过UserId获取数据
        /// </summary>
        /// <typeparam name="T">Model类</typeparam>
        /// <param name="UserId">用户Id</param>
        /// <param name="Id">序号Id</param>
        /// <returns></returns>
        public T QueryByUserId<T>(int UserId, int Id)
        {
            T list = System.Activator.CreateInstance<T>();
            Type type = list.GetType();
            string sql = "select top 1 * from " + Config.DatabasePrefix(type.Name) + " where UserId=" + UserId + " and Id=" + Id;
            SqlDataReader reader = SqlHelper.ExecuteReader(SqlHelper.connstr, CommandType.Text, sql, null);
            while (reader.Read())
            {
                Info<T>(ref list, reader);
            }
            reader.Close();
            return list;
        }

        /// <summary>
        /// 通过 OrderNo 获取数据
        /// </summary>
        /// <typeparam name="T">Model类</typeparam>
        /// <param name="OrderNo">订单 OrderNo</param>
        /// <returns></returns>
        public T QueryByOrderNo<T>(string OrderNo)
        {
            T list = System.Activator.CreateInstance<T>();
            Type type = list.GetType();
            string sql = "select top 1 * from " + Config.DatabasePrefix(type.Name) + " where OrderNo='" + OrderNo + "'";
            SqlDataReader reader = SqlHelper.ExecuteReader(SqlHelper.connstr, CommandType.Text, sql, null);
            while (reader.Read())
            {
                Info<T>(ref list, reader);
            }
            reader.Close();
            return list;
        }

        /// <summary>
        /// 通过查询条件获取数据
        /// </summary>
        /// <typeparam name="T">Model类</typeparam>
        /// <param name="SelectWhere">查询条件</param>
        /// <returns></returns>
        public T QueryBySelectWhere<T>(string SelectWhere, params SqlParameter[] param)
        {
            T list = System.Activator.CreateInstance<T>();
            Type type = list.GetType();
            string sql = "select top 1 * from " + Config.DatabasePrefix(type.Name) + " where " + SelectWhere;
            SqlDataReader reader = SqlHelper.ExecuteReader(SqlHelper.connstr, CommandType.Text, sql, param);
            while (reader.Read())
            {
                Info<T>(ref list, reader);
            }
            reader.Close();
            return list;
        }

        /// <summary>
        /// 通过查询条件获取数据(可排序)
        /// </summary>
        /// <typeparam name="T">Model类</typeparam>
        /// <param name="SelectWhere">查询条件</param>
        /// <param name="OrderBy">排序</param>
        /// <returns></returns>
        public T QueryBySelectWhereAndOrderBy<T>(string SelectWhere, string OrderBy, params SqlParameter[] param)
        {
            T list = System.Activator.CreateInstance<T>();
            Type type = list.GetType();
            string sql = "select top 1 * from " + Config.DatabasePrefix(type.Name) + " where " + SelectWhere + (string.IsNullOrEmpty(OrderBy) ? "" : " order by " + OrderBy);
            SqlDataReader reader = SqlHelper.ExecuteReader(SqlHelper.connstr, CommandType.Text, sql, param);
            while (reader.Read())
            {
                Info<T>(ref list, reader);
            }
            reader.Close();
            return list;
        }

        /// <summary>
        /// 检测数据是否存在
        /// </summary>
        /// <param name="tableName">表</param>
        /// <param name="SelectWhere">查询条件</param>
        /// <returns></returns>
        public bool CheckInFo(string tableName, string SelectWhere, params SqlParameter[] param)
        {
            string sql = "select top 1 * from " + Config.DatabasePrefix(tableName) + " where " + SelectWhere;
            SqlDataReader reader = SqlHelper.ExecuteReader(SqlHelper.connstr, CommandType.Text, sql, param);
            while (reader.Read())
            {
                reader.Close();
                return true;
            }
            reader.Close();
            return false;
        }

        /// <summary>
        /// 获取数据库最大的字段
        /// </summary>
        /// <param name="tableName">表</param>
        /// <param name="Field">字段</param>
        /// <param name="SelectWhere">查询条件</param>
        /// <returns></returns>
        public object QueryMaxBySelectWhere(string tableName, string Field, string SelectWhere, params SqlParameter[] param)
        {
            object max = 0;
            string sql = "select max(" + Field + ") as max from " + Config.DatabasePrefix(tableName) + (string.IsNullOrEmpty(SelectWhere) ? "" : " where " + SelectWhere);
            SqlDataReader reader = SqlHelper.ExecuteReader(SqlHelper.connstr, CommandType.Text, sql, param);
            while (reader.Read())
            {
                max = reader["max"];
            }
            reader.Close();
            return max;
        }

        #region 函数
        /// <summary>
        /// SqlDataReader转换成DataTable
        /// </summary>
        /// <param name="dr"></param>
        /// <returns></returns>
        public DataTable Convertdrtodt(SqlDataReader dr)
        {
            DataTable dataTable = new DataTable();
            //dataTable.Load(dr);
            //dr.Close();
            //return (dataTable);

            for (int i = 0; i < dr.FieldCount; i++)
            {
                DataColumn mydc = new DataColumn();//关键的一步
                mydc.DataType = dr.GetFieldType(i);
                mydc.ColumnName = dr.GetName(i);
                mydc.MaxLength = -1;
                dataTable.Columns.Add(mydc);//关键的第二步
            }
            while (dr.Read())
            {
                DataRow mydr = dataTable.NewRow();//关键的第三步
                for (int i = 0; i < dr.FieldCount; i++)
                {
                    mydr[i] = dr[i].ToString();
                }
                dataTable.Rows.Add(mydr);//关键的第四步
                mydr = null;
            }
            dr.Close();
            return (dataTable);//别忘了要返回datatable，否则出错
        }
        /// <summary>
        /// 映射赋值
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="list"></param>
        /// <param name="reader"></param>
        public static void Info<T>(ref T list, SqlDataReader reader)
        {
            Type type = list.GetType();
            PropertyInfo[] ps = type.GetProperties();
            for (int i = 0; i < ps.Length; i++)
            {
                //ps[i].PropertyType + "<br/>");
                GetValueBySqlDataReader<T>(ref ps[i], ref list, reader);
            }
        }
        public static void GetValueBySqlDataReader<T>(ref PropertyInfo p, ref T list, SqlDataReader reader)
        {
            switch (p.PropertyType.ToString())
            {
                case "System.String":
                    p.SetValue(list, Common.IsString(reader[p.Name]), null);
                    break;
                case "System.Int32":
                    p.SetValue(list, Common.IsInt(reader[p.Name]), null);
                    break;
                case "System.Boolean":
                    p.SetValue(list, Common.IsBool(reader[p.Name]), null);
                    break;
                case "System.DateTime":
                    p.SetValue(list, Common.IsDateTime(reader[p.Name]), null);
                    break;
                case "System.Decimal":
                    p.SetValue(list, Common.IsDecimal(reader[p.Name]), null);
                    break;
                case "System.Double":
                    p.SetValue(list, Common.IsDouble(reader[p.Name]), null);
                    break;
                default:
                    p.SetValue(list, Common.IsString(reader[p.Name]), null);
                    break;
            }
        }

        #endregion
    }
}
