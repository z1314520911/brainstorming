using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net;

namespace Commons
{
    public class WebClientUtils
    {
        public static string Get(string Url, Encoding Encoder, ref string CookieStr, string Referer = "")
        {
            string result = "";

            WebClient myClient = new WebClient();
            myClient.Headers.Add("Accept: text/html,application/xhtml+xml,application/xml;q=0.9,image/webp,image/apng,*/*;q=0.8");
            myClient.Headers.Add("Accept-Encoding: gzip, deflate, br");
            myClient.Headers.Add("Accept-Language: zh-CN,zh;q=0.9");
            myClient.Headers.Add("Cache-Control: max-age=0");
            //myClient.Headers.Add("Connection: keep-alive");
            myClient.Headers.Add("Host: sp0.baidu.com");
            myClient.Headers.Add("Cookie: BIDUPSID=C5E82BB086083ACE081ADD4F692B858F; PSTM=1562221634; BDUSS=TJJYWhGcXZQU3ZreWtIbklyVVlzdkJIdDN1MmRPVndnejZWUHR5Y2pwaUpxa2RkSVFBQUFBJCQAAAAAAAAAAAEAAABhA2YJY3Jpc3RpYW5vanpob3UAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAIkdIF2JHSBdd; BAIDUID=882204C8A290AE968FC842889A64AF59:FG=1;H_WISE_SIDS=133539_124610_114550_133855_131676_134119_132550_131887_126063_133678_120160_133016_132910_133042_131247_134307_132439_130763_132378_131517_118880_118869_118848_118822_118792_107319_133159_133351_129650_132251_127027_128967_132542_133472_131905_128892_133847_132551_133839_134319_134214_129644_131423_134236_133996_133916_110085_134155_127969_131752_131298_130598_127416_133729_134151_134393_134354_100457; MCITY=-125%3A; BDORZ=B490B5EBF6F3CD402E515D22BCDA1598; H_PS_PSSID=1466_21087_29568_29699_29221_26350_22158; BDSFRCVID=r5DOJeC62rMmLZ6wvKHRk_t49vObP07TH6aoSkjQthTzM4Mmi2YLEG0PjM8g0KubVwkKogKKWgOTHNkF_2uxOjjg8UtVJeC6EG0Ptf8g0M5; H_BDCLCKID_SF=tJIO_K_MJCL3fP36q4_Vh-Lbqxby2C62aJ0L-pRvWJ5TMCo4X-6VKJKW2aJXJPndbTbqoqka2b6cShPC-tnHMfPeetLD2-6DbaRwKbQX3l02Vb59e-t2ynLV5Rr8aPRMW23U0h7mWprOsxA45J7cM4IseboJLfT-0bc4KKJxbnLWeIJEjjCaD55LDGAtJjnfb5kXWb7O-nTjKROvhjRC5MKgyxomtjjQQTch0IQ52hnpsn5_ypJ_bt_H-GbZLUkqKCOhsRQlLxL5EP55QPn0MnkXQttjQPThfIkja-KEBqRkER7TyURRbU47y-TW0q4Hb6b9BJcjfU5MSlcNLTjpQT8r5MDOK5OuJRLHVI02tCL-hIk9ebO2MKCs-x6W2JJObPo2WDvYLlvcOR5Jj6K-XJ0kLUTfaMvJKDTZaM385Rj_HCO_3MA--t4L0nDDXxrTJ64qQIQcLqc6sq0x0hnte-bQypoaL6oJ0KOMahv65h7xOhrcQlPK5JkgMx6MqpQJQeQ-5KQN3KJmfbL9bT3YjjTLea0ftT0etR3fL-082bb_jJjvq4bohjPXjtO9BtQmJJrH2R61yDQbflvS04Rr553-54naQT8fQg-q3R7R2bcGfbn22xFMXtutDRrr0x-jLIQuVn0MW-5DS43cyPnJyUPibPnnBU6W3H8HL4nv2JcJbM5m3x6qLTKkQN3T-PKO5bRu_CcJ-J8XhC8Cj53P; delPer=0; PSINO=6; __guid=195541998.276109823695003420.1573205140886.9094; ZD_ENTRY=baidu; monitor_count=29");
            myClient.Headers.Add("Upgrade-Insecure-Requests: 1");
            myClient.Headers.Add("User-Agent: Mozilla/5.0 (Linux; Android 6.0; Nexus 5 Build/MRA58N) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/63.0.3239.132 Mobile Safari/537.36");
            //myClient.Headers.Add("Content-Type: multipart/form-data");
            myClient.Encoding = Encoder;
            result = myClient.DownloadString(Url);
            //if (CookieStr != "")
            //{
            //    myClient.Headers.Add(CookieStr);
            //}
            //if (CookieStr == "")
            //{
            //    CookieStr = myClient.ResponseHeaders["Set-Cookie"].ToString();
            //    CookieStr = GetCookie(CookieStr);
            //}
            return result;
        }
        public static string Post(string Url, string Referer, Encoding Encoder, ref string CookieStr, string Data)
        {
            string result = "";

            WebClient myClient = new WebClient();
            myClient.Headers.Add("Accept: */*");
            myClient.Headers.Add("User-Agent: Mozilla/4.0 (compatible; MSIE 7.0; Windows NT 5.1; Trident/4.0; .NET4.0E; .NET4.0C; InfoPath.2; .NET CLR 2.0.50727; .NET CLR 3.0.04506.648; .NET CLR 3.5.21022; .NET CLR 3.0.4506.2152; .NET CLR 3.5.30729; SE 2.X MetaSr 1.0)");
            myClient.Headers.Add("Accept-Language: zh-cn");
            myClient.Headers.Add("Content-Type: multipart/form-data");
            myClient.Headers.Add("Accept-Encoding: gzip, deflate");
            myClient.Headers.Add("Cache-Control: no-cache");
            if (CookieStr != "")
            {
                myClient.Headers.Add(CookieStr);
            }
            myClient.Encoding = Encoder;
            result = myClient.UploadString(Url, Data);
            if (CookieStr == "")
            {
                CookieStr = myClient.ResponseHeaders["Set-Cookie"].ToString();
                CookieStr = GetCookie(CookieStr);
            }
            return result;
        }
        private static string GetCookie(string CookieStr)
        {
            string result = "";

            string[] myArray = CookieStr.Split(',');
            if (myArray.Count() > 0)
            {
                result = "Cookie: ";
                foreach (var str in myArray)
                {
                    string[] CookieArray = str.Split(';');
                    result += CookieArray[0].Trim();
                    result += "; ";
                }
                result = result.Substring(0, result.Length - 2);
            }
            return result;
        }
    }
}
