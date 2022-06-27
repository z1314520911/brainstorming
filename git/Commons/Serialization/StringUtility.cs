using System;
using System.IO;
using System.Security.Cryptography;
using System.Text;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.Security;

namespace Commons
{
    /// <summary>
    /// 字符串转换、编码操作工具类。
    /// </summary>
    public class StringUtility
    {
        /// <summary>
        /// 把字符串编码为 Base64
        /// </summary>
        /// <param name="Source">要编码在字符串</param>
        /// <param name="e">编码格式，默认：UTF-8。</param>
        /// <param name="urlencode">是否对结果字符串进行Url编码</param>
        /// <returns>编码结果</returns>
        public static string ToBase64(string Source, Encoding e = null, bool urlencode = false)
        {
            try
            {
                byte[] byteSource = e.GetBytes(Source);

                // 对字符串进行 URL 编码，防止出现 + 或 / 在传递过程中丢失。
                string strBaseCode = Convert.ToBase64String(byteSource).Replace('+', '(').Replace('/', ')');

                if (urlencode)
                {
                    if (e == null)
                    {
                        strBaseCode = HttpUtility.UrlEncode(strBaseCode, Encoding.UTF8);
                    }
                    else
                    {
                        strBaseCode = HttpUtility.UrlEncode(strBaseCode, e);
                    }
                }

                return strBaseCode;
            }
            catch
            {
                return string.Empty;
            }
        }

        /// <summary>
        /// 对 Base64 字符串解码
        /// </summary>
        /// <param name="Code">要解码的字符串</param>
        /// <param name="e">解码格式，默认：UTF-8。</param>
        /// <param name="urlencode">源字符串是否经过Url编码</param>
        /// <returns>解码结果</returns>
        public static string FromBase64(string Code, Encoding e = null, bool urlencode = false)
        {
            try
            {
                string strResultCode = Code;

                if (urlencode)
                {
                    if (e == null)
                    {
                        strResultCode = HttpUtility.UrlDecode(Code, Encoding.UTF8);
                    }
                    else
                    {
                        strResultCode = HttpUtility.UrlDecode(Code, e);
                    }
                }

                byte[] byteResult = Convert.FromBase64String(strResultCode.Replace(')', '/').Replace('(', '+'));

                strResultCode = e.GetString(byteResult);

                return strResultCode;
            }
            catch
            {
                return string.Empty;
            }
        }

        /// <summary>
        /// 把字符串编码为 Base64
        /// </summary>
        /// <param name="source">要编码在字符串</param>
        /// <param name="e">编码格式，默认：UTF-8。</param>
        /// <param name="urlencode">是否对结果字符串进行Url编码</param>
        /// <returns>编码结果</returns>
        public static string FromByteToBase64(byte[] source, Encoding e = null, bool urlencode = false)
        {
            try
            {
                // 对字符串进行 URL 编码，防止出现 + 或 / 在传递过程中丢失。
                string strBaseCode = Convert.ToBase64String(source, Base64FormattingOptions.None).Replace('+', '(').Replace('/', ')');

                if (urlencode)
                {
                    if (e == null)
                    {
                        strBaseCode = HttpUtility.UrlEncode(strBaseCode, Encoding.UTF8);
                    }
                    else
                    {
                        strBaseCode = HttpUtility.UrlEncode(strBaseCode, e);
                    }
                }

                return strBaseCode;
            }
            catch
            {
                return string.Empty;
            }
        }

        /// <summary>
        /// 对 Base64 字符串解码
        /// </summary>
        /// <param name="Code">要解码的字符串</param>
        /// <param name="e">解码格式，默认：UTF-8。</param>
        /// <param name="urlencode">源字符串是否经过Url编码</param>
        /// <returns>解码结果</returns>
        public static byte[] FromBase64ToByte(string Code, Encoding e = null, bool urlencode = false)
        {
            try
            {
                string strResultCode = Code;

                if (urlencode)
                {
                    if (e == null)
                    {
                        strResultCode = HttpUtility.UrlDecode(Code, Encoding.UTF8);
                    }
                    else
                    {
                        strResultCode = HttpUtility.UrlDecode(Code, e);
                    }
                }

                byte[] byteResult = Convert.FromBase64String(strResultCode.Replace(')', '/').Replace('(', '+'));

                return byteResult;
            }
            catch
            {
                return null;
            }
        }

        /// <summary>
        /// 对字符串进行编码
        /// </summary>
        /// <param name="source">要编码的源字符串</param>
        /// <returns></returns>
        public static string Encode(string source)
        {
            try
            {
                return Uri.EscapeDataString(source);
            }
            catch
            {
                return source;
            }
        }

        /// <summary>
        /// 对字符串进行解码
        /// </summary>
        /// <param name="target">要解码的目标字符串</param>
        /// <returns></returns>
        public static string Decode(string target)
        {
            try
            {
                return Uri.UnescapeDataString(target);
            }
            catch
            {
                return target;
            }
        }

        /// <summary>
        /// 对字符串进行 URL 编码
        /// </summary>
        /// <param name="source">要编码的源字符串</param>
        /// <param name="e">编码格式，默认：UTF-8。</param>
        /// <returns></returns>
        public static string UrlEncode(string source, Encoding e = null)
        {
            try
            {
                if (e == null)
                {
                    return HttpUtility.UrlEncode(source, Encoding.UTF8);
                }
                else
                {
                    return HttpUtility.UrlEncode(source, e);
                }
            }
            catch
            {
                return source;
            }
        }

        /// <summary>
        /// 对字符串进行 URL 解码
        /// </summary>
        /// <param name="target">要解码的目标字符串</param>
        /// <param name="e">解码格式，默认：UTF-8。</param>
        /// <returns></returns>
        public static string UrlDecode(string target, Encoding e = null)
        {
            try
            {
                if (e == null)
                {
                    return HttpUtility.UrlDecode(target, Encoding.UTF8);
                }
                else
                {
                    return HttpUtility.UrlDecode(target, e);
                }
            }
            catch
            {
                return target;
            }
        }

        /// <summary>
        /// 转换字符串中的 HTML 标签
        /// </summary>
        /// <param name="source">需要进行转换的源字符串</param>
        /// <returns>转换后的字符串</returns>
        public static string HTMLTagConverter(string source)
        {
            try
            {
                return source.Replace("&", "&amp;").Replace("\"", "&quot;").Replace(" ", "&nbsp;").Replace("<", "&lt;").Replace(">", "&gt;");
            }
            catch
            {
                return source;
            }
        }

        /// <summary>
        /// 移除字符串中的 HTML 标签
        /// </summary>
        /// <param name="source">源字符串</param>
        /// <returns>移除标签后的字符串</returns>
        public static string HTMLTagClear(string source)
        {
            try
            {
                return Regex.Replace(source, @"<[\s\S]*?>", "", RegexOptions.IgnoreCase);
            }
            catch
            {
                return source;
            }
        }



        #region RSA 加密/解密
        ///// <summary>
        ///// 加密算法
        ///// </summary>
        ///// <param name="encryptString"></param>
        ///// <returns></returns>
        //public static string RSAEncrypt1(string encryptString)
        //{
        //        CspParameters csp = new CspParameters();
        //        csp.KeyContainerName = "guoqiang";
        //        RSACryptoServiceProvider RSAProvider = new RSACryptoServiceProvider(csp);
        //        byte[] encryptBytes = RSAProvider.Encrypt(ASCIIEncoding.ASCII.GetBytes(encryptString), true);
        //        string str = "";
        //        foreach (byte b in encryptBytes)
        //        {
        //            str = str + string.Format("{0:x2}", b);
        //        }
        //        return str;
        //}
        ///// <summary>
        ///// 解密算法
        ///// </summary>
        ///// <param name="decryptString"></param>
        ///// <returns></returns>
        //public static string RSADecrypt1(string decryptString)
        //{
        //    try
        //    {
        //        CspParameters csp = new CspParameters();
        //        csp.KeyContainerName = "guoqiang";
        //        RSACryptoServiceProvider RSAProvider = new RSACryptoServiceProvider(csp);
        //        int length = (decryptString.Length / 2);
        //        byte[] decryptBytes = new byte[length];
        //        for (int index = 0; index < length; index++)
        //        {
        //            string substring = decryptString.Substring(index * 2, 2);
        //            decryptBytes[index] = Convert.ToByte(substring, 16);
        //        }
        //        decryptBytes = RSAProvider.Decrypt(decryptBytes, true);
        //        return ASCIIEncoding.ASCII.GetString(decryptBytes);
        //    }
        //    catch
        //    {
        //        return "";
        //    }
        //}
        #endregion

        #region AES 加密/解密
        /// <summary>
        /// AES 加密(高级加密标准，是下一代的加密算法标准，速度快，安全级别高，目前 AES 标准的一个实现是 Rijndael 算法)
        /// </summary>
        /// <param name="EncryptString">待加密密文</param>
        /// <param name="EncryptKey">私密</param>
        /// <returns></returns>
        public static string RSAEncrypt(string EncryptString, string EncryptKey="")
        {
            if (string.IsNullOrEmpty(EncryptString)) { return ""; }
            if (string.IsNullOrEmpty(EncryptKey)) { EncryptKey = "guoqiang"; }
            string m_strEncrypt = "";
            byte[] m_btIV = Convert.FromBase64String("Rkb4jvUy/ye7Cd7k89QQgQ==");
            Rijndael m_AESProvider = Rijndael.Create();
            try
            {
                byte[] m_btEncryptString = Encoding.Default.GetBytes(EncryptString);
                MemoryStream m_stream = new MemoryStream();
                CryptoStream m_csstream = new CryptoStream(m_stream, m_AESProvider.CreateEncryptor(Encoding.Default.GetBytes(EncryptKey), m_btIV), CryptoStreamMode.Write);
                m_csstream.Write(m_btEncryptString, 0, m_btEncryptString.Length); m_csstream.FlushFinalBlock();
                m_strEncrypt = Convert.ToBase64String(m_stream.ToArray());
                m_stream.Close(); m_stream.Dispose();
                m_csstream.Close(); m_csstream.Dispose();
                return m_strEncrypt.Replace("+", "(add)").Replace("&", "(and)");
            }
            catch (Exception ex) { throw ex; }
            //catch (IOException ex) { throw ex; }
            //catch (CryptographicException ex) { throw ex; }
            //catch (ArgumentException ex) { throw ex; }
            //catch (Exception ex) { throw ex; }
            //finally { m_AESProvider.Clear(); }
        }

        /// <summary>
        /// AES 解密(高级加密标准，是下一代的加密算法标准，速度快，安全级别高，目前 AES 标准的一个实现是 Rijndael 算法)
        /// </summary>
        /// <param name="DecryptString">待解密密文</param>
        /// <param name="DecryptKey">私密</param>
        /// <returns></returns>
        public static string RSADecrypt(string DecryptString, string DecryptKey = "")
        {
            if (string.IsNullOrEmpty(DecryptString)) { return ""; }
            if (string.IsNullOrEmpty(DecryptKey)) { DecryptKey = "guoqiang"; }
            string m_strDecrypt = "";
            byte[] m_btIV = Convert.FromBase64String("Rkb4jvUy/ye7Cd7k89QQgQ==");
            Rijndael m_AESProvider = Rijndael.Create();
            try
            {
                DecryptString = DecryptString.Replace("(add)", "+").Replace("(and)", "&");
                byte[] m_btDecryptString = Convert.FromBase64String(DecryptString);
                MemoryStream m_stream = new MemoryStream();
                CryptoStream m_csstream = new CryptoStream(m_stream, m_AESProvider.CreateDecryptor(Encoding.Default.GetBytes(DecryptKey), m_btIV), CryptoStreamMode.Write);
                m_csstream.Write(m_btDecryptString, 0, m_btDecryptString.Length); m_csstream.FlushFinalBlock();
                m_strDecrypt = Encoding.Default.GetString(m_stream.ToArray());
                m_stream.Close(); m_stream.Dispose();
                m_csstream.Close(); m_csstream.Dispose();
                return m_strDecrypt;
            }
            catch
            {
                return "";
            }
            //catch (IOException ex) { throw ex; }
            //catch (CryptographicException ex) { throw ex; }
            //catch (ArgumentException ex) { throw ex; }
            //catch (Exception ex) { throw ex; }
            //finally { m_AESProvider.Clear(); }
        }
        #endregion

        /// <summary>
        /// 对字符串进行加密
        /// </summary>
        /// <param name="Passowrd">待加密的字符串</param>
        /// <returns>string</returns>
        public static string Encrypt(string Passowrd)
        {
            string strResult = "";

            FormsAuthenticationTicket ticket = new FormsAuthenticationTicket(Passowrd, true, 200);
            strResult = FormsAuthentication.Encrypt(ticket).ToString();

            return strResult;
        }



        /// <summary>
        /// 对字符串进行解密
        /// </summary>
        /// <param name="Passowrd">已加密的字符串</param>
        /// <returns></returns>
        public static string Decrypt(string Passowrd)
        {
            string strResult = "";

            strResult = FormsAuthentication.Decrypt(Passowrd).Name.ToString();

            return strResult;
        }


        /// <summary>
        /// 对字符串进行加密（不可逆）
        /// </summary>
        /// <param name="Password">要加密的字符串</param>
        /// <param name="Format">加密方式,0 is SHA1,1 is MD5</param>
        /// <returns></returns>
        public static string NoneEncrypt(string Password, int Format)
        {
            string strResult = "";
            switch (Format)
            {
                case 0:
                    strResult = FormsAuthentication.HashPasswordForStoringInConfigFile(Password, "SHA1");
                    break;
                case 1:
                    strResult = FormsAuthentication.HashPasswordForStoringInConfigFile(Password, "MD5");
                    break;
                default:
                    strResult = Password;
                    break;
            }

            return strResult;
        }
    }
}
