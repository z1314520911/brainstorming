using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Json;
using System.Text;
using Newtonsoft.Json;
using System.Web;

namespace Commons
{
    /// <summary>
    /// JSON 字符串操作工具类。
    /// </summary>
    public class JsonUtility
    {
        private static Dictionary<string, object> dict = new Dictionary<string, object>();

        /// <summary>
        /// 序列化 JSON 对象
        /// </summary>
        /// <param name="obj">JSON 对象</param>
        /// <returns>JSON 序列化字符串</returns>
        public static string WriteObject<T>(T obj) where T : class
        {
            try
            {
                var serializer = new DataContractJsonSerializer(typeof(T));
                var mstream = new MemoryStream();

                serializer.WriteObject(mstream, obj);

                byte[] Bytes = new byte[mstream.Length];

                mstream.Position = 0;
                mstream.Read(Bytes, 0, (int)mstream.Length);

                return Encoding.UTF8.GetString(Bytes);
            }
            catch
            {
                return string.Empty;
            }
        }

        /// <summary>
        /// 序列化 JSON 对象列表
        /// </summary>
        /// <param name="objs">JSON 对象列表</param>
        /// <returns>JSON 序列化字符串</returns>
        public static string WriteObject<T>(IEnumerable<T> objs) where T : class
        {
            try
            {
                StringBuilder sbResult = new StringBuilder();
                sbResult.AppendFormat("[", new object[0]);

                foreach (T obj in objs)
                {
                    sbResult.AppendFormat("{0},", WriteObject(obj));
                }

                // 判断是否需要删除最后的逗号
                if (sbResult.Length > 2)
                {
                    sbResult.Remove(sbResult.Length - 1, 1);
                }

                sbResult.AppendFormat("]", new object[0]);

                return Convert.ToString(sbResult);
            }
            catch
            {
                return string.Empty;
            }
        }

        /// <summary>
        /// 反序列化 JSON 对象
        /// </summary>
        /// <param name="data">JSON 对象序列化字符串</param>
        /// <returns>JSON 对象</returns>
        public static T ReadObject<T>(string data, bool flag = true) where T : class
        {
            try
            {
                if (flag)
                {
                    var mstream = new MemoryStream(Encoding.UTF8.GetBytes(data));
                    var serializer = new DataContractJsonSerializer(typeof(T));

                    return (T)serializer.ReadObject(mstream);
                }
                else
                {
                    return JsonConvert.DeserializeObject<T>(data);
                }
            }
            catch
            {
                return default(T);
            }
        }

        public static string Serialize(object obj)
        {
            try
            {
                return JsonConvert.SerializeObject(obj);
            }
            catch
            {
                return string.Empty;
            }
        }
        public static string Serialize2(object obj)
        {
            try
            {
                string strJson = JsonConvert.SerializeObject(obj, Formatting.Indented,
                                new JsonSerializerSettings
                                {
                                    PreserveReferencesHandling = PreserveReferencesHandling.Objects
                                });
                return strJson;
            }
            catch
            {
                return string.Empty;
            }
        }

        public static T Deserialize<T>(string json) where T : class
        {
            try
            {
                return JsonConvert.DeserializeObject<T>(json);
            }
            catch
            {
                return null;
            }
        }
    }
}
