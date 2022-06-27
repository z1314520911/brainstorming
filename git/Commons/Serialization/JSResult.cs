using System.Collections.Generic;
using System.Text;

namespace Commons
{
    public class JSResult
    {
        /// <summary>
        /// 返回前端 js 请求结果
        /// </summary>
        /// <param name="code">返回码</param>
        /// <param name="message">操作结果</param>
        /// <param name="data">操作数据</param>
        /// <returns></returns>
        public static string ToJSON(int code, string message, string data = "")
        {
            StringBuilder sbResult = new StringBuilder();

            sbResult.Append("{");

            sbResult.Append("\"Code\": \"" + code.ToString() + "\"");
            sbResult.Append(",\"Message\": \"" + message + "\"");

            if (!string.IsNullOrEmpty(data))
            {
                sbResult.Append(",\"Data\": " + data);
            }

            sbResult.Append("}");

            return sbResult.ToString();
        }
        /// <summary>
        /// 返回前端 js 请求结果
        /// </summary>
        /// <param name="code">返回码</param>
        /// <param name="message">操作结果</param>
        /// <param name="data">操作数据</param>
        /// <returns></returns>
        public static IDictionary<string, object> DictToJson(int code, string message, List<object> data = null)
        {
            IDictionary<string, object> dictResult = new Dictionary<string, object>();

            dictResult.Add("Code", code);
            dictResult.Add("Message", message);

            if (data != null)
            {
                dictResult.Add("Data", data);
            }

            return dictResult;
        }
    }
}
