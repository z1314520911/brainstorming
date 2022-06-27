using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;

/// <summary>
/// RegCheck ������ʽ�жϸ�ʽ
/// </summary>
public class RegCheck
{
    public RegCheck()
    {
        //
        // TODO: �ڴ˴���ӹ��캯���߼�
        //
    }
    /// <summary>
    /// �Ƿ�Ϊ�������ַ���
    /// </summary>
    /// <param name="StrSource">�����ַ���(2008-05-08)</param>
    /// <returns></returns>
    public static bool IsDate(string StrSource)
    {
        return Regex.IsMatch(StrSource, @"^((((1[6-9]|[2-9]\d)\d{2})-(0?[13578]|1[02])-(0?[1-9]|[12]\d|3[01]))|(((1[6-9]|[2-9]\d)\d{2})-(0?[13456789]|1[012])-(0?[1-9]|[12]\d|30))|(((1[6-9]|[2-9]\d)\d{2})-0?2-(0?[1-9]|1\d|2[0-9]))|(((1[6-9]|[2-9]\d)(0[48]|[2468][048]|[13579][26])|((16|[2468][048]|[3579][26])00))-0?2-29-))$");
    }

    /// <summary>
    /// �Ƿ�Ϊʱ�����ַ���
    /// </summary>
    /// <param name="source">ʱ���ַ���(15:00:00)</param>
    /// <returns></returns>
    public static bool IsTime(string StrSource)
    {
        return Regex.IsMatch(StrSource, @"^((20|21|22|23|[0-1]?\d):[0-5]?\d:[0-5]?\d)$");
    }

    /// <summary>
    /// �Ƿ�Ϊ����+ʱ�����ַ���
    /// </summary>
    /// <param name="source">ʱ���ַ���(2008-05-08 15:00:00)</param>
    /// <returns></returns>
    public static bool IsDateTime(string StrSource)
    {
        return Regex.IsMatch(StrSource, @"^(((((1[6-9]|[2-9]\d)\d{2})-(0?[13578]|1[02])-(0?[1-9]|[12]\d|3[01]))|(((1[6-9]|[2-9]\d)\d{2})-(0?[13456789]|1[012])-(0?[1-9]|[12]\d|30))|(((1[6-9]|[2-9]\d)\d{2})-0?2-(0?[1-9]|1\d|2[0-8]))|(((1[6-9]|[2-9]\d)(0[48]|[2468][048]|[13579][26])|((16|[2468][048]|[3579][26])00))-0?2-29-)) (20|21|22|23|[0-1]?\d):[0-5]?\d:[0-5]?\d)$ ");
        //(?:(?:(?:(?:19|20)(?:(?:[02468][048])|(?:[13579][26]))-0?2-29))|(?:\d{4}-0?(?!(?:[4|6|9]-31)|(?:2-29)|(?:11-31)|(?:2-30)|(?:2-31))((?!0|(?:1[3-9]))(?:1?[0-9])-(?!0|(?:3[2-9]))(?:[1-3]?[0-9]))))\s+(?!(?:2[4-9]))(?:[1-2]?[0-9]):0?0:0?0
    }
    /// <summary>
    /// �Ƿ�Ϊ�����ʽ
    /// </summary>
    /// <param name="StrSource"></param>
    /// <returns></returns>
    public static bool IsEmail(string StrSource)
    {
        //   Return   true   if   strIn   is   in   valid   e-mail   format.   
        return Regex.IsMatch(StrSource, @"^([\w-\.]+)@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.)|(([\w-]+\.)+))([a-zA-Z]{2,4}|[0-9]{1,3})(\]?)$");
    }
    /// <summary>
    /// �Ƿ�Ϊ�ֻ�����
    /// </summary>
    /// <param name="StrSource"></param>
    /// <returns></returns>
    public static bool IsTel(string StrSource)
    {
        return Regex.IsMatch(StrSource, @"^1[3,4,5,6,7,8,9]\d{9}$");
        //return Regex.IsMatch(StrSource, @"^(((13[0-9]{1})|159|(15[0-9]{1})|(18[0-9]{1}))+\d{8})$");
    }
    /// <summary>
    /// �Ƿ�ΪQQ����
    /// </summary>
    /// <param name="StrSource"></param>
    /// <returns></returns>
    public static bool IsQq(string StrSource)
    {
        return Regex.IsMatch(StrSource, @"^\d{5,12}$");
    }
    /// <summary>
    /// �Ƿ�ΪIP��ַ
    /// </summary>
    /// <param name="StrSource"></param>
    /// <returns></returns>
    public static bool IsIpAddress(string StrSource)
    {
        return Regex.IsMatch(StrSource, @"(\d{1,2}|1\d\d|2[0-4]\d|25[0-5])\.(\d{1,2}|1\d\d|2[0-4]\d|25[0-5])\.(\d{1,2}|1\d\d|2[0-4]\d|25[0-5])\.(\d{1,2}|1\d\d|2[0-4]\d|25[0-5])");
    }
    /// <summary>
    /// �Զ���������ʽ
    /// </summary>
    /// <param name="str">Ҫ����֤���ַ���</param>
    /// <param name="regexStr">������ʽ</param>
    /// <returns></returns>
    public bool getRegexInput(string str, string regexStr)
    {
        return Regex.IsMatch(str, regexStr);
    }


    public static bool IsDecimal(string StrSource)
    {
        return Regex.IsMatch(StrSource, @"(^\d*\.?\d*[0-9]+\d*$)|(^[0-9]+\d*\.\d*$)");
    }

    /// <summary>
    /// ��֤��ַ��Ч��
    /// </summary>
    /// <param name="url"></param>
    /// <returns></returns>
    public static bool IsUrl(string url)
    {
        return System.Uri.CheckSchemeName(url);
    }

    /// <summary>
    /// ��֤������Ч�� 6-15λ����ĸ�����֣��»���
    /// </summary>
    /// <param name="url"></param>
    /// <returns></returns>
    public static bool IsPassWord(string StrSource)
    {
        return Regex.IsMatch(StrSource, @"(^[a-zA-Z0-9\-_\.\@]{6,15}$)");
    }

    /// <summary>
    /// ��֤�û�����Ч�� 3-20λ����ĸ�����֣��»���
    /// </summary>
    /// <param name="url"></param>
    /// <returns></returns>
    public static bool IsUserName(string StrSource)
    {
        return Regex.IsMatch(StrSource, @"(^[a-zA-Z0-9\-_\.\@]{3,20}$)");
    }

    /// <summary>
    /// �ֻ�����*��
    /// </summary>
    /// <param name="url"></param>
    /// <returns></returns>
    public static string ReplacePhone(object phone)
    {
        return System.Text.RegularExpressions.Regex.Replace(Commons.Common.IsString(phone), "(\\d{3})\\d{5}(\\d{3})", "$1****$2");
    }
}