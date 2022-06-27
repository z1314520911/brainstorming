using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using System.Web.Security;
using Model;
using BLL;
using Commons;
using System.Text.RegularExpressions;

namespace Controllers
{
    //[HandleError]
    public class MyCheckBox
    {
        public static List<int> GetCheckBox(FormCollection collection, string CheckName)
        {
            List<int> list = new List<int>(); //定义一个List用来存储从checkbox中得到的值
            string Pattern = @"\d{1,}";    //这是一个正则表达式，意思是至少包含一个数字
            if (collection.GetValue(CheckName) == null)//这是判断name为checkboxPerson的checkbox的值是否为空，若为空返回NULL;
            {
                return list;
            }

//下面一段是若不为空，遍历name为checkboxPerson的checkbox的值，并放在list中（这里得到的是ID）
            else
            {
                foreach (var item in Regex.Matches(collection.GetValue(CheckName).AttemptedValue, Pattern))   //collection.GetValue("checkboxPerson").
                    //   意思是得到的name为checkboxPerson的checkbox中你选中的checkbox的值。
                    list.Add(Int32.Parse(item.ToString()));
            }
            return list;
        }
        public static string GetStringCheckBox(FormCollection collection, string CheckName)
        {
            string list = "";
            string Pattern = @"\d{1,}";    //这是一个正则表达式，意思是至少包含一个数字
            if (collection.GetValue(CheckName) == null)//这是判断name为checkboxPerson的checkbox的值是否为空，若为空返回NULL;
            {
                return list;
            }

//下面一段是若不为空，遍历name为checkboxPerson的checkbox的值，并放在list中（这里得到的是ID）
            else
            {
                foreach (var item in Regex.Matches(collection.GetValue(CheckName).AttemptedValue, Pattern))   //collection.GetValue("checkboxPerson").
                    //   意思是得到的name为checkboxPerson的checkbox中你选中的checkbox的值。
                    list = list + item.ToString() + ",";
            }
            if (!string.IsNullOrEmpty(list))
            {
                list = list.TrimEnd(',');
            }

            return list;
        }


    }
}
