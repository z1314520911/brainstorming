using System;
using System.Collections.Generic;
using System.Text;
using System.Xml;
using System.Xml.XPath;

namespace Commons
{
    public static class ExtensionUtility
    {
        public static bool Boolean(this object source)
        {
            bool result;
            try
            {
                if (source == null)
                {
                    result = false;
                }
                else
                {
                    result = Convert.ToBoolean(source);
                }
            }
            catch
            {
                result = false;
            }
            return result;
        }

        public static int Int(this object source, int defaultValue = 0)
        {
            int result;
            try
            {
                if (source == null)
                {
                    result = defaultValue;
                }
                else
                {
                    result = Convert.ToInt32(source);
                }
            }
            catch
            {
                result = defaultValue;
            }
            return result;
        }

        public static uint UInt(this object source, uint defaultValue = 0u)
        {
            uint result;
            try
            {
                if (source == null)
                {
                    result = defaultValue;
                }
                else
                {
                    result = Convert.ToUInt32(source);
                }
            }
            catch
            {
                result = defaultValue;
            }
            return result;
        }

        public static string String(this object source, string defaultValue = "")
        {
            string result;
            try
            {
                if (source == null)
                {
                    result = defaultValue;
                }
                else
                {
                    result = Convert.ToString(source).Trim();
                }
            }
            catch
            {
                result = defaultValue;
            }
            return result;
        }

        public static DateTime? DateTime(this object source, EDateRange range = EDateRange.NULL)
        {
            DateTime? result;
            try
            {
                if (source == null)
                {
                    if (range.Equals(EDateRange.Min))
                    {
                        result = new DateTime?(new DateTime(1900, 1, 1, 0, 0, 0));
                    }
                    else if (range.Equals(EDateRange.Max))
                    {
                        result = new DateTime?(new DateTime(2999, 12, 31, 23, 59, 59));
                    }
                    else
                    {
                        result = null;
                    }
                }
                else
                {
                    result = new DateTime?(Convert.ToDateTime(source));
                }
            }
            catch
            {
                if (range.Equals(EDateRange.Min))
                {
                    result = new DateTime?(new DateTime(1900, 1, 1, 0, 0, 0));
                }
                else if (range.Equals(EDateRange.Max))
                {
                    result = new DateTime?(new DateTime(1900, 1, 1, 0, 0, 0));
                }
                else
                {
                    result = null;
                }
            }
            return result;
        }

        public enum EDateRange
        {
            NULL,
            Min,
            Max = 9
        }

        public static string TrimApiChar(this string source)
        {
            string result;
            try
            {
                if (string.IsNullOrWhiteSpace(source))
                {
                    result = source;
                }
                else
                {
                    result = source.Replace("\\", "").Trim(new char[]
					{
						'\\',
						'"'
					}).Trim();
                }
            }
            catch
            {
                result = source;
            }
            return result;
        }
    }
}
