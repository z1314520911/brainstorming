/*
 * MD5 加密数据。
 */

using System;
using System.Security.Cryptography;
using System.Text;

namespace Commons
{
    /// <summary>
    /// MD5 操作工具类。
    /// </summary>
    public class MD5Utility
    {
        /// <summary>
        /// MD5 加密
        /// </summary>
        /// <param name="source">需要加密的字符串</param>
        /// <returns>加密成功的字符串</returns>
        public static string Encrypt(string source)
        {
            StringBuilder sbResult = new StringBuilder();
            MD5 m = MD5.Create();

            byte[] byteSource = m.ComputeHash(Encoding.UTF8.GetBytes(source));

            foreach (byte b in byteSource)
            {
                sbResult.Append(b.ToString("x2"));
            }

            return sbResult.ToString();
        }

        /// <summary>
        /// MD5 校验未加密字符串
        /// </summary>
        /// <param name="source">校验证源字符串（未加密）</param>
        /// <param name="target">校验的目标字符串</param>
        /// <returns>验证结果</returns>
        public static bool MD5Verify(string source, string target)
        {
            // 对源字符串进行 MD5 加密。
            string hashOfInput = Encrypt(source);

            // 创建字符串比较对象。
            StringComparer comparer = StringComparer.OrdinalIgnoreCase;

            if (0 == comparer.Compare(hashOfInput, target))
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        /// <summary>
        /// 校验 MD5 字符串
        /// </summary>
        /// <param name="source">校验证源字符串（未加密）</param>
        /// <param name="target">校验的目标字符串（已加密）</param>
        /// <param name="salt">加密盐，默认为空字符串。</param>
        /// <returns>验证结果</returns>
        public static bool Verify(string source, string target, string salt = "")
        {
            // 对源字符串进行 MD5 加密。
            string hashOfInput = SALTMD5(source, salt);

            // 创建字符串比较对象。
            StringComparer comparer = StringComparer.OrdinalIgnoreCase;

            if (0 == comparer.Compare(hashOfInput, target))
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        /// <summary>
        /// MD5 校验已加密字符串
        /// </summary>
        /// <param name="souce">校验证源字符串（已加密）</param>
        /// <param name="target">校验的目标字符串（已加密）</param>
        /// <returns>验证结果</returns>
        public static bool Equals(string souce, string target)
        {
            StringComparer comparer = StringComparer.OrdinalIgnoreCase;

            if (0 == comparer.Compare(souce, target))
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        /// <summary>
        /// 对字符串加盐后进行 MD5 加密计算。
        /// </summary>
        /// <param name="source">要加密的源字符串。</param>
        /// <param name="salt">盐值：15 位或以上的字符串，默认为空字符串。建议使用 36 位或以上长度的 GUID 值。</param>
        /// <returns></returns>
        public static string SALTMD5(string source, string salt = "")
        {
            // 如果没有盐值或盐值长度小于 15 时，则直接对字符串进行 MD5 加密用于做盐。
            if (string.IsNullOrEmpty(salt.Trim()) || salt.Length < 15)
            {
                salt = Encrypt(source);
            }

            int intFirstLength = Convert.ToInt32(salt.Length / 3);      // 计算第一段字符的长度。
            int intSecondStart = Convert.ToInt32(salt.Length / 2);      // 计算第二段字符的起始位置。

            string strSALTString = string.Format("{0}#{1}@{2}", salt.Substring(0, intFirstLength), source, salt.Substring(intSecondStart));

            return Encrypt(strSALTString);
        }
    }
}
