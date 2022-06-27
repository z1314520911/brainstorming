using System.Reflection;
using System.Data;
using System.Data.SqlClient;
using System;

using Commons;
using Model;
using DAL;

namespace BLL
{
    public partial class ModelBLL
    {
        //独立insert，update版本
        

        /// <summary>
        /// 单条更新信息
        /// </summary>
        /// <param name="tableName">表名</param>
        /// <param name="id">更新ID</param>
        /// <param name="UpdateInfo">要更新的数据</param>
        /// <returns>返回整数验证数据执行是否成功</returns>
        public static int UpdateInfo(string tableName, int Id, string UpdateInfo, params SqlParameter[] param)
        {
            return cs.UpdateInfo(tableName, Id, UpdateInfo, param);
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
        public static int UpdateInfo(string tableName, string UpdateName, int Id, string UpdateInfo, string UserName, params SqlParameter[] param)
        {
            return cs.UpdateInfo(tableName, UpdateName, Id, UpdateInfo, UserName, param);
        }

        /// <summary>
        /// 批量更新信息
        /// </summary>
        /// <param name="tableName">表名</param>
        /// <param name="id">更新ID</param>
        /// <param name="UpdateInfo">要更新的数据</param>
        /// <returns></returns>
        public static int UpdateInfo(string tableName, string Id, string UpdateInfo, params SqlParameter[] param)
        {
            return cs.UpdateInfo(tableName, Id, UpdateInfo, param);
        }
        /// <summary>
        /// 通过条件更新数据
        /// </summary>
        /// <param name="tableName">表名</param>
        /// <param name="UpdateInfo">要更新的数据</param>
        /// <param name="SelectWhere">更新条件</param>
        /// <returns></returns>
        public static int UpdateInfoBySelectWhere(string tableName, string UpdateInfo, string SelectWhere, params SqlParameter[] param)
        {
            return cs.UpdateInfoBySelectWhere(tableName, UpdateInfo, SelectWhere, param);
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
        public static int UpdateInfo(string tableName, string UpdateName, string Id, string UpdateInfo, string UserName, params SqlParameter[] param)
        {
            return cs.UpdateInfo(tableName, UpdateName, Id, UpdateInfo, UserName, param);
        }
        private static ModelService_Sql csSD = new ModelService_Sql();

        /// <summary>
        /// 删除数据
        /// </summary>
        /// <param name="Table">表</param>
        /// <param name="Id">序号ID</param>
        /// <returns></returns>
        public static int Delete(string Table, int Id)
        {
            return csSD.Delete(Table, Id);
        }

        /// <summary>
        /// 删除数据
        /// </summary>
        /// <param name="Table">表</param>
        /// <param name="Id">序号ID</param>
        /// <param name="SelectWhere">查询条件</param>
        /// <param name="myParam">参数</param>
        /// <returns></returns>
        public static int Delete(string Table, int Id, string SelectWhere, params SqlParameter[] myParam)
        {
            return csSD.Delete(Table, Id, SelectWhere, myParam);
        }

        /// <summary>
        /// 批量删除数据
        /// </summary>
        /// <param name="Table">表</param>
        /// <param name="Id">序号ID</param>
        /// <returns></returns>
        public static int DeleteAll(string Table, string Id)
        {
            return csSD.DeleteAll(Table, Id);
        }

        /// <summary>
        /// 简单查询语句
        /// </summary>
        /// <param name="PageSiz">条数</param>
        /// <param name="Field">字段</param>
        /// <param name="Table">表</param>
        /// <param name="SelectWhere">查询条件</param>
        /// <param name="OrderBy">排序</param>
        /// <returns></returns>
        public static DataTable getPage(int PageSiz, string Field, string Table, string SelectWhere, string OrderBy, params SqlParameter[] param)
        {
            return csSD.getPage(PageSiz, Field, Table, SelectWhere, OrderBy, param);
        }

        /// <summary>
        /// 简单查询语句(联表)
        /// </summary>
        /// <param name="PageSiz">条数</param>
        /// <param name="Field">字段</param>
        /// <param name="Table">表</param>
        /// <param name="JoinOn">联查</param>
        /// <param name="SelectWhere">查询条件</param>
        /// <param name="OrderBy">排序</param>
        /// <returns></returns>
        public static DataTable getPage(int PageSiz, string Field, string Table, string JoinOn, string SelectWhere, string OrderBy, params SqlParameter[] param)
        {
            return csSD.getPage(PageSiz, Field, Table, JoinOn, SelectWhere, OrderBy, param);
        }


        /// <summary>
        /// 分页查询
        /// </summary>
        /// <param name="intPageNo">页数</param>
        /// <param name="PageSiz">条数</param>
        /// <param name="Field">字段</param>
        /// <param name="Table">表</param>
        /// <param name="SelectWhere">查询条件</param>
        /// <param name="OrderBy">排序</param>
        /// <returns></returns>
        public static DataTable GetAll(int intPageNo, int PageSiz, string Field, string Table, string SelectWhere, string OrderBy, params SqlParameter[] param)
        {
            return csSD.GetAll(intPageNo, PageSiz, Field, Table, SelectWhere, OrderBy, param);
        }

        /// <summary>
        /// 联表分页查询(联表)
        /// </summary>
        /// <param name="intPageNo">页数</param>
        /// <param name="PageSiz">条数</param>
        /// <param name="Field">字段</param>
        /// <param name="Table">表</param>
        /// <param name="JoinOn">联查</param>
        /// <param name="SelectWhere">查询条件</param>
        /// <param name="OrderBy">排序</param>
        /// <returns></returns>
        public static DataTable GetAll(int intPageNo, int PageSiz, string Field, string Table, string JoinOn, string SelectWhere, string OrderBy, params SqlParameter[] param)
        {
            return csSD.GetAll(intPageNo, PageSiz, Field, Table, JoinOn, SelectWhere, OrderBy, param);
        }

        /// <summary>
        /// 分页查询
        /// </summary>
        /// <param name="intPageNo">页数</param>
        /// <param name="PageSiz">条数</param>
        /// <param name="Field">字段</param>
        /// <param name="Table">表</param>
        /// <param name="SelectWhere">查询条件</param>
        /// <param name="OrderBy">排序</param>
        /// <param name="TotalRow">数据总量</param>
        /// <returns></returns>
        public static DataTable GetAll(int intPageNo, int PageSiz, string Field, string Table, string SelectWhere, string OrderBy, ref int TotalRow, params SqlParameter[] param)
        {
            return csSD.GetAll(intPageNo, PageSiz, Field, Table, SelectWhere, OrderBy, ref TotalRow, param);
        }

        /// <summary>
        /// 联表分页查询(联表)
        /// </summary>
        /// <param name="intPageNo">页数</param>
        /// <param name="PageSiz">条数</param>
        /// <param name="Field">字段</param>
        /// <param name="Table">表</param>
        /// <param name="JoinOn">联查</param>
        /// <param name="SelectWhere">查询条件</param>
        /// <param name="OrderBy">排序</param>
        /// <param name="TotalRow">数据总量</param>
        /// <returns></returns>
        public static DataTable GetAll(int intPageNo, int PageSiz, string Field, string Table, string JoinOn, string SelectWhere, string OrderBy, ref int TotalRow, params SqlParameter[] param)
        {
            return csSD.GetAll(intPageNo, PageSiz, Field, Table, JoinOn, SelectWhere, OrderBy, ref TotalRow, param);
        }

        /// <summary>
        /// 获取数据总量
        /// </summary>
        /// <param name="Table">表</param>
        /// <param name="SelectWhere">查询条件</param>
        /// <returns></returns>
        public static int AllPage(string Table, string SelectWhere, params SqlParameter[] param)
        {
            return csSD.AllPage(Table, SelectWhere, param);
        }

        /// <summary>
        /// 获取数据库最大的ID
        /// </summary>
        /// <param name="tableName">表名</param>
        /// <returns></returns>
        public static int QueryBigId(string tableName)
        {
            return csSD.QueryBigId(tableName);
        }

        /// <summary>
        /// 直接执行SQL语句
        /// </summary>
        /// <param name="sql">SQL语句</param>
        /// <returns></returns>
        public static int ExecuteSql(string sql, params SqlParameter[] param)
        {
            return csSD.ExecuteSql(sql, param);
        }

        /// <summary>
        /// 直接执行SQL语句
        /// </summary>
        /// <param name="sql">SQL语句</param>
        /// <returns></returns>
        public static DataTable ExecuteSqlAll(string sql, params SqlParameter[] param)
        {
            return csSD.ExecuteSqlAll(sql, param);
        }

        /// <summary>
        /// 通过ID获取数据
        /// </summary>
        /// <typeparam name="T">Model类</typeparam>
        /// <param name="Id">序号Id</param>
        /// <returns></returns>
        public static T QueryByID<T>(object Id)
        {
            return csSD.QueryByID<T>(Common.IsInt(Id));
        }

        /// <summary>
        /// 通过OpenId获取数据
        /// </summary>
        /// <typeparam name="T">Model类</typeparam>
        /// <param name="UserId">用户OpenId</param>
        /// <returns></returns>
        public static T QueryByOpenId<T>(object OpenId)
        {
            return csSD.QueryByOpenId<T>(Common.IsString(OpenId));
        }

        /// <summary>
        /// 通过UserId获取数据
        /// </summary>
        /// <typeparam name="T">Model类</typeparam>
        /// <param name="UserId">用户Id</param>
        /// <returns></returns>
        public static T QueryByUserId<T>(object UserId)
        {
            return csSD.QueryByUserId<T>(Common.IsInt(UserId));
        }

        /// <summary>
        /// 通过UserId获取数据
        /// </summary>
        /// <typeparam name="T">Model类</typeparam>
        /// <param name="UserId">用户Id</param>
        /// <param name="Id">序号Id</param>
        /// <returns></returns>
        public static T QueryByUserId<T>(object UserId, object Id)
        {
            return csSD.QueryByUserId<T>(Common.IsInt(UserId), Common.IsInt(Id));
        }

        /// <summary>
        /// 通过 OrderNo 获取数据
        /// </summary>
        /// <typeparam name="T">Model类</typeparam>
        /// <param name="OrderNo">订单 OrderNo</param>
        /// <returns></returns>
        public static T QueryByOrderNo<T>(object OrderNo)
        {
            return csSD.QueryByOrderNo<T>(Common.IsString(OrderNo));
        }

        /// <summary>
        /// 通过查询条件获取数据
        /// </summary>
        /// <typeparam name="T">Model类</typeparam>
        /// <param name="SelectWhere">查询条件</param>
        /// <returns></returns>
        public static T QueryBySelectWhere<T>(string SelectWhere, params SqlParameter[] param)
        {
            return csSD.QueryBySelectWhere<T>(SelectWhere, param);
        }

        /// <summary>
        /// 通过查询条件获取数据(可排序)
        /// </summary>
        /// <typeparam name="T">Model类</typeparam>
        /// <param name="SelectWhere">查询条件</param>
        /// <param name="OrderBy">排序</param>
        /// <returns></returns>
        public static T QueryBySelectWhereAndOrderBy<T>(string SelectWhere, string OrderBy, params SqlParameter[] param)
        {
            return csSD.QueryBySelectWhereAndOrderBy<T>(SelectWhere, OrderBy, param);
        }

        /// <summary>
        /// 检测数据是否存在
        /// </summary>
        /// <param name="Table">表</param>
        /// <param name="SelectWhere">查询条件</param>
        /// <returns></returns>
        public static bool CheckInFo(string Table, string SelectWhere, params SqlParameter[] param)
        {
            return csSD.CheckInFo(Table, SelectWhere, param);
        }

        /// <summary>
        /// 检测数据是否存在
        /// </summary>
        /// <param name="Table">表</param>
        /// <param name="Field">字段</param>
        /// <param name="SelectWhere">查询条件</param>
        /// <returns></returns>
        public static object QueryMaxBySelectWhere(string Table, string Field, string SelectWhere, params SqlParameter[] param)
        {
            return csSD.QueryMaxBySelectWhere(Table, Field, SelectWhere, param);
        }


        #region 函数
        public static void CheckValue<T>(ref PropertyInfo p, ref T list)
        {
            switch (p.PropertyType.ToString())
            {
                case "System.String":
                    p.SetValue(list, Common.IsString(p.GetValue(list, null)), null);
                    break;
                case "System.Int32":
                    p.SetValue(list, Common.IsInt(p.GetValue(list, null)), null);
                    break;
                case "System.Boolean":
                    p.SetValue(list, Common.IsBool(p.GetValue(list, null)), null);
                    break;
                case "System.DateTime":
                    p.SetValue(list, Common.IsDateTime(p.GetValue(list, null)), null);
                    break;
                case "System.Decimal":
                    p.SetValue(list, Common.IsDecimal(p.GetValue(list, null)), null);
                    break;
                case "System.Double":
                    p.SetValue(list, Common.IsDouble(p.GetValue(list, null)), null);
                    break;
                default:
                    p.SetValue(list, Common.IsString(p.GetValue(list, null)), null);
                    break;
            }
        }
        public static void CheckValue<T>(ref PropertyInfo p, ref T list, object value)
        {
            switch (p.PropertyType.ToString())
            {
                case "System.String":
                    p.SetValue(list, Common.IsString(value), null);
                    break;
                case "System.Int32":
                    p.SetValue(list, Common.IsInt(value), null);
                    break;
                case "System.Boolean":
                    p.SetValue(list, Common.IsBool(value), null);
                    break;
                case "System.DateTime":
                    p.SetValue(list, Common.IsDateTime(value), null);
                    break;
                case "System.Decimal":
                    p.SetValue(list, Common.IsDecimal(value), null);
                    break;
                case "System.Double":
                    p.SetValue(list, Common.IsDouble(value), null);
                    break;
                default:
                    p.SetValue(list, Common.IsString(value), null);
                    break;
            }
        }
        // sql server数据类型（如：varchar）
        // 转换为SqlDbType类型
        //public static string OleDbTypeString2SqlType(string sqlTypeString)
        //{
        //    string reval = "OleDbType.VarChar";

        //    switch (sqlTypeString.ToLower())
        //    {
        //        case "2":
        //            reval = "OleDbType.Boolean";
        //            break;
        //        case "3":
        //            reval = "OleDbType.TinyInt";
        //            break;
        //        case "4":
        //            reval = "OleDbType.UnsignedTinyInt";
        //            break;
        //        case "5":
        //            reval = "OleDbType.SmallInt";
        //            break;
        //        case "bit":
        //            reval = "OleDbType.Integer";
        //            break;
        //        case "char":
        //            reval = "OleDbType.BigInt";
        //            break;
        //        case "7":
        //            reval = "OleDbType.UnsignedSmallInt";
        //            break;
        //        case "11":
        //            reval = "OleDbType.UnsignedInt";
        //            break;
        //        case "18":
        //            reval = "OleDbType.UnsignedBigInt";
        //            break;
        //        case "130":
        //            reval = "OleDbType.Single";
        //            break;
        //        case "131":
        //            reval = "OleDbType.Currency";
        //            break;
        //        default:
        //            reval = "OleDbType.Decimal";
        //            break;
        //        case "xml":
        //            reval = "OleDbType.Double";
        //            break;
        //        case "xml":
        //            reval = "OleDbType.Numeric";
        //            break;
        //        case "xml":
        //            reval = "OleDbType.VarNumeric";
        //            break;
        //        case "xml":
        //            reval = "OleDbType.Guid";
        //            break;
        //        case "xml":
        //            reval = "OleDbType.Double";
        //            break;
        //        case "xml":
        //            reval = "OleDbType.Double";
        //            break;
        //        case "xml":
        //            reval = "OleDbType.Double";
        //            break;
        //        case "xml":
        //            reval = "OleDbType.Double";
        //            break;
        //    }
        //    return reval;
        //}
        #endregion
    }
}
