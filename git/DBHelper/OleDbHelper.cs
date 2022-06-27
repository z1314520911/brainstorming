using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.OleDb;
using System.Configuration;

namespace DBHelper
{
    public abstract class OleDbHelper
    {
        //数据库连接字符串
        public static readonly string connstr = ConfigurationManager.AppSettings["ConnStr"] + System.AppDomain.CurrentDomain.BaseDirectory.ToString() + ConfigurationManager.AppSettings["DbPath"];

        /// <summary>
        /// 用提供的参数，在连接字符串所指定的数据库中执行OleDb语句（非查询）（增、删、改）
        /// </summary>
        /// <remarks>
        /// 使用示例:  
        ///  int result = ExecuteNonQuery(connString, CommandType.StoredProcedure, "PublishOrders", new OleDbParameter("@prodid", 24));
        /// </remarks>
        /// <param name="connectionString">数据库连接字符串</param>
        /// <param name="commandType">命令类型（存储过程、文本等）</param>
        /// <param name="commandText">存储过程名或T-OleDb语句</param>
        /// <param name="commandParameters">用于执行命令的参数数组</param>
        /// <returns>受命令所影响的行数</returns>
        public static int ExecuteNonQuery(string connectionString, CommandType commandType, string commandText, params OleDbParameter[] commandParameters)
        {

            OleDbCommand cmd = new OleDbCommand();

            using (OleDbConnection conn = new OleDbConnection(connectionString))
            {
                PrepareCommand(cmd, conn, null, commandType, commandText, commandParameters);
                int val = cmd.ExecuteNonQuery();
                cmd.Parameters.Clear();
                return val;
            }
        }

        /// <summary>
        /// 用提供的参数和存在的数据库连接对象，执行OleDb语句（非查询）
        /// </summary>
        /// <remarks>
        /// 使用示例:  
        ///  int result = ExecuteNonQuery(connection, CommandType.StoredProcedure, "PublishOrders", new OleDbParameter("@prodid", 24));
        /// </remarks>
        /// <param name="connection">存在的数据库连接对象</param>
        /// <param name="commandType">命令类型（存储过程、文本等）</param>
        /// <param name="commandText">存储过程名或T-OleDb语句</param>
        /// <param name="commandParameters">用于执行命令的参数数组</param>
        /// <returns>受命令所影响的行数</returns>
        public static int ExecuteNonQuery(OleDbConnection connection, CommandType commandType, string commandText, params OleDbParameter[] commandParameters)
        {

            OleDbCommand cmd = new OleDbCommand();

            PrepareCommand(cmd, connection, null, commandType, commandText, commandParameters);
            int val = cmd.ExecuteNonQuery();
            cmd.Parameters.Clear();
            return val;
        }

        /// <summary>
        /// 用提供的参数和存在的事务对象，执行OleDb语句（非查询）
        /// </summary>
        /// <remarks>
        /// 使用示例:  
        ///  int result = ExecuteNonQuery(trans, CommandType.StoredProcedure, "PublishOrders", new OleDbParameter("@prodid", 24));
        /// </remarks>
        /// <param name="trans">存在的事务对象</param>
        /// <param name="commandType">命令类型（存储过程、文本等）</param>
        /// <param name="commandText">存储过程名或T-OleDb语句</param>
        /// <param name="commandParameters">用于执行命令的参数数组</param>
        /// <returns>受命令所影响的行数</returns>
        public static int ExecuteNonQuery(OleDbTransaction trans, CommandType commandType, string commandText, params OleDbParameter[] commandParameters)
        {
            OleDbCommand cmd = new OleDbCommand();
            PrepareCommand(cmd, trans.Connection, trans, commandType, commandText, commandParameters);
            int val = cmd.ExecuteNonQuery();
            cmd.Parameters.Clear();
            return val;
        }

        /// <summary>
        /// 用提供的参数，在连接字符串所指定的数据库中执行OleDb查询，并返回结果集（OleDbDataReader）
        /// </summary>
        /// <remarks>
        /// 使用示例:  
        ///  OleDbDataReader r = ExecuteReader(connectionString, CommandType.StoredProcedure, "PublishOrders", new OleDbParameter("@prodid", 24));
        /// </remarks>
        /// <param name="connectionString">数据库连接字符串</param>
        /// <param name="commandType">命令类型（存储过程、文本等）</param>
        /// <param name="commandText">存储过程名或T-OleDb语句</param>
        /// <param name="commandParameters">用于执行命令的参数数组</param>
        /// <returns>一个包含结果的OleDbDataReader对象</returns>
        public static OleDbDataReader ExecuteReader(string connectionString, CommandType cmdType, string cmdText, params OleDbParameter[] commandParameters)
        {
            OleDbCommand cmd = new OleDbCommand();
            OleDbConnection conn = new OleDbConnection(connectionString);

            // 之所以这里用 try/catch，是因为：
            // 如果方法抛出异常时，我们希望关闭连接并抛出异常
            // 因为此时不会返回 DataReader，故 commandBehaviour.CloseConnection 也不起作用
            try
            {
                PrepareCommand(cmd, conn, null, cmdType, cmdText, commandParameters);
                OleDbDataReader rdr = cmd.ExecuteReader(CommandBehavior.CloseConnection);
                cmd.Parameters.Clear();
                return rdr;
            }
            catch
            {
                conn.Close();
                throw;
            }
        }

        /// <summary>
        /// 用提供的参数，在连接字符串所指定的数据库中执行OleDb查询，并返回查询结果的第一行第一列的值
        /// </summary>
        /// <remarks>
        /// 使用示例:  
        ///  Object obj = ExecuteScalar(connectionString, CommandType.StoredProcedure, "PublishOrders", new OleDbParameter("@prodid", 24));
        /// </remarks>
        /// <param name="connectionString">数据库连接字符串</param>
        /// <param name="commandType">命令类型（存储过程、文本等）</param>
        /// <param name="commandText">存储过程名或T-OleDb语句</param>
        /// <param name="commandParameters">用于执行命令的参数数组</param>
        /// <returns>一个 object 对象，可用 Convert.To{Type} 转换为所需类型</returns>
        public static object ExecuteScalar(string connectionString, CommandType commandType, string commandText, params OleDbParameter[] commandParameters)
        {
            OleDbCommand cmd = new OleDbCommand();

            using (OleDbConnection connection = new OleDbConnection(connectionString))
            {
                PrepareCommand(cmd, connection, null, commandType, commandText, commandParameters);
                object val = cmd.ExecuteScalar();
                cmd.Parameters.Clear();
                return val;
            }
        }

        /// <summary>
        /// 用提供的参数和存在的数据库连接对象，执行OleDb查询，并返回查询结果的第一行第一列的值
        /// </summary>
        /// <remarks>
        /// 使用示例:  
        ///  Object obj = ExecuteScalar(connection, CommandType.StoredProcedure, "PublishOrders", new OleDbParameter("@prodid", 24));
        /// </remarks>
        /// <param name="connection">存在的数据库连接对象</param>
        /// <param name="commandType">命令类型（存储过程、文本等）</param>
        /// <param name="commandText">存储过程名或T-OleDb语句</param>
        /// <param name="commandParameters">用于执行命令的参数数组</param>
        /// <returns>一个 object 对象，可用 Convert.To{Type} 转换为所需类型</returns>
        public static object ExecuteScalar(OleDbConnection connection, CommandType commandType, string commandText, params OleDbParameter[] commandParameters)
        {

            OleDbCommand cmd = new OleDbCommand();

            PrepareCommand(cmd, connection, null, commandType, commandText, commandParameters);
            object val = cmd.ExecuteScalar();
            cmd.Parameters.Clear();
            return val;
        }

        /// <summary>
        /// 构建一个用于执行的命令对象
        /// </summary>
        /// <param name="cmd">OleDbCommand 对象</param>
        /// <param name="conn">OleDbConnection 对象</param>
        /// <param name="trans">OleDbTransaction 对象</param>
        /// <param name="cmdType">命令类型（存储过程、文本等）</param>
        /// <param name="cmdText">存储过程名或T-OleDb语句</param>
        /// <param name="cmdParms">用于执行命令的参数数组</param>
        private static void PrepareCommand(OleDbCommand cmd, OleDbConnection conn, OleDbTransaction trans, CommandType cmdType, string cmdText, OleDbParameter[] cmdParms)
        {
            if (conn.ConnectionString == "" || conn.ConnectionString == null)
            {
                conn.ConnectionString = connstr;
            }
            if (conn.State != ConnectionState.Open)
                conn.Open();

            cmd.Connection = conn;
            cmd.CommandText = cmdText;

            if (trans != null)
                cmd.Transaction = trans;

            cmd.CommandType = cmdType;

            if (cmdParms != null)
            {
                foreach (OleDbParameter parm in cmdParms)
                    cmd.Parameters.Add(parm);
            }
        }
    }
}
