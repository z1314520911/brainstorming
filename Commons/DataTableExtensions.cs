using System;
using System.Data;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Collections;
using System.Reflection;
using System.Collections.Generic;
using System.Xml;
using System.Dynamic;

/// <summary>
///DataTableExtensions 的摘要说明
/// </summary>
public static class DataTableExtensions
{
    /// <summary>  
    /// 转化一个DataTable  
    /// </summary>  
    /// <typeparam name="T"></typeparam>  
    /// <param name="list"></param>  
    /// <returns></returns>  
    public static DataTable ToDataTable<T>(this IEnumerable<T> list)
    {
        //创建属性的集合  
        List<PropertyInfo> pList = new List<PropertyInfo>();
        //获得反射的入口  
        Type type = typeof(T);
        DataTable dt = new DataTable();
        //把所有的public属性加入到集合 并添加DataTable的列  
        Array.ForEach<PropertyInfo>(type.GetProperties(), p => { pList.Add(p); dt.Columns.Add(p.Name, p.PropertyType); });
        foreach (var item in list)
        {
            //创建一个DataRow实例  
            DataRow row = dt.NewRow();
            //给row 赋值  
            pList.ForEach(p => row[p.Name] = p.GetValue(item, null));
            //加入到DataTable  
            dt.Rows.Add(row);
        }

        return dt;
    }
    /// <summary>  
    /// DataTable 转换为List 集合  
    /// </summary>  
    /// <typeparam name="TResult">类型</typeparam>  
    /// <param name="dt">DataTable</param>  
    /// <returns></returns>  
    public static List<T> ToList<T>(this DataTable dt) where T : class, new()
    {
        //创建一个属性的列表  
        List<PropertyInfo> prlist = new List<PropertyInfo>();
        //获取TResult的类型实例  反射的入口  
        Type t = typeof(T);
        //获得TResult 的所有的Public 属性 并找出TResult属性和DataTable的列名称相同的属性(PropertyInfo) 并加入到属性列表   
        Array.ForEach<PropertyInfo>(t.GetProperties(), p => { if (dt.Columns.IndexOf(p.Name) != -1) prlist.Add(p); });
        //创建返回的集合  
        List<T> oblist = new List<T>();
        foreach (DataRow row in dt.Rows)
        {
            //创建TResult的实例  
            T ob = new T();
            //找到对应的数据  并赋值  
            prlist.ForEach(p => { if (row[p.Name] != DBNull.Value) p.SetValue(ob, row[p.Name], null); });
            //放入到返回的集合中.  
            oblist.Add(ob);
        }
        return oblist;
    }

    /// <summary>  
    /// 将集合类转换成DataTable  
    /// </summary>  
    /// <param name="list">集合</param>  
    /// <returns></returns>  
    public static DataTable ToDataTableTow(IList list)
    {
        DataTable result = new DataTable();
        if (list.Count > 0)
        {
            PropertyInfo[] propertys = list[0].GetType().GetProperties();
            foreach (PropertyInfo pi in propertys)
            {
                result.Columns.Add(pi.Name, pi.PropertyType);
            }
            for (int i = 0; i < list.Count; i++)
            {
                ArrayList tempList = new ArrayList();
                foreach (PropertyInfo pi in propertys)
                {
                    object obj = pi.GetValue(list[i], null);
                    tempList.Add(obj);
                }
                object[] array = tempList.ToArray();
                result.LoadDataRow(array, true);
            }
        }
        return result;
    }

    /**/
    /// <summary>  
    /// 将泛型集合类转换成DataTable  
    /// </summary>  
    /// <typeparam name="T">集合项类型</typeparam>  
    /// <param name="list">集合</param>  
    /// <returns>数据集(表)</returns>  
    public static DataTable ToDataTable<T>(IList<T> list)
    {
        return ToDataTable<T>(list, null);
    }

    /**/
    /// <summary>  
    /// 将泛型集合类转换成DataTable  
    /// </summary>  
    /// <typeparam name="T">集合项类型</typeparam>  
    /// <param name="list">集合</param>  
    /// <param name="propertyName">需要返回的列的列名</param>  
    /// <returns>数据集(表)</returns>  
    public static DataTable ToDataTable<T>(IList<T> list, params string[] propertyName)
    {
        List<string> propertyNameList = new List<string>();
        if (propertyName != null)
        {
            propertyNameList.AddRange(propertyName);
        }
        DataTable result = new DataTable();
        if (list.Count > 0)
        {
            PropertyInfo[] propertys = list[0].GetType().GetProperties();
            foreach (PropertyInfo pi in propertys)
            {
                if (propertyNameList.Count == 0)
                {
                    result.Columns.Add(pi.Name, pi.PropertyType);
                }
                else
                {
                    if (propertyNameList.Contains(pi.Name))
                    {
                        result.Columns.Add(pi.Name, pi.PropertyType);
                    }
                }
            }
            for (int i = 0; i < list.Count; i++)
            {
                ArrayList tempList = new ArrayList();
                foreach (PropertyInfo pi in propertys)
                {
                    if (propertyNameList.Count == 0)
                    {
                        object obj = pi.GetValue(list[i], null);
                        tempList.Add(obj);
                    }
                    else
                    {
                        if (propertyNameList.Contains(pi.Name))
                        {
                            object obj = pi.GetValue(list[i], null);
                            tempList.Add(obj);
                        }
                    }
                }
                object[] array = tempList.ToArray();
                result.LoadDataRow(array, true);
            }
        }
        return result;
    }
    /// <summary>
    /// 返回List<T>的DataTable集合"/>
    /// </summary>
    /// <typeparam name="T">T集合</typeparam>
    /// <param name="list">List对象</param>
    /// <param name="XmlSelectSingleNode">XML里匹配的对象</param>
    /// <returns></returns>
    public static DataTable ToDataTableBind<T>(IList<T> list, string XmlSelectSingleNode)
    {
        DataTable dt = new DataTable();
        if (list.Count > 0)
        {
            string filePath = System.Web.HttpContext.Current.Server.MapPath("~/App_Data/DataCollection.config");
            XmlDocument xmlDoc = new XmlDocument();
            xmlDoc.Load(filePath);
            string[] PropertyName = xmlDoc.SelectSingleNode("Config/" + XmlSelectSingleNode + "/PropertyName").InnerText.Split(',');
            string[] BankName = xmlDoc.SelectSingleNode("Config/" + XmlSelectSingleNode + "/BankName").InnerText.Split(',');
            dt = ToDataTable<T>(list, PropertyName);
            for (int i = 0; i < BankName.Length; i++)
            {
                dt.Columns["" + PropertyName[i] + ""].ColumnName = BankName[i];
            }
        }
        return dt;
    }
    /// <summary>
    /// DataRow转Model
    /// </summary>
    /// <typeparam name="T">T集合</typeparam>
    /// <param name="dr"></param>
    /// <returns></returns>
    public static T ToModel<T>(DataRow dr) where T : class, new()
    {
        List<PropertyInfo> prlist = new List<PropertyInfo>();
        Type t = typeof(T);
        Array.ForEach<PropertyInfo>(t.GetProperties(), p => { if (dr.Table.Columns.IndexOf(p.Name) != -1) prlist.Add(p); });
        T ob = new T();
        prlist.ForEach(p => { if (dr[p.Name] != DBNull.Value) p.SetValue(ob, dr[p.Name], null); });
        return ob;
    }
    /// <summary>
    /// datarow[] 转换成 datatable
    /// </summary>
    /// <param name="dt"></param>
    /// <param name="strWhere">筛选的条件</param>
    /// <returns></returns>
    public static DataTable SreeenDataTable(DataTable dt, string strWhere, string sort = "")
    {
        if (dt.Rows.Count <= 0) return dt;
        DataTable dtNew = dt.Clone();
        DataRow[] rows = dt.Select(strWhere, sort);
        foreach (DataRow row in rows)
        {
            dtNew.ImportRow(row);
        }
        return dtNew;
    }
    /// <summary>
    /// 将DataTable 转换成 List<dynamic>
    /// reverse 反转：控制返回结果中是只存在 FilterField 指定的字段,还是排除.
    /// [flase 返回FilterField 指定的字段]|[true 返回结果剔除 FilterField 指定的字段]
    /// FilterField  字段过滤，FilterField 为空 忽略 reverse 参数；返回DataTable中的全部数
    /// </summary>
    /// <param name="table">DataTable</param>
    /// <param name="reverse">
    /// 反转：控制返回结果中是只存在 FilterField 指定的字段,还是排除.
    /// [flase 返回FilterField 指定的字段]|[true 返回结果剔除 FilterField 指定的字段]
    ///</param>
    /// <param name="FilterField">字段过滤，FilterField 为空 忽略 reverse 参数；返回DataTable中的全部数据</param>
    /// <returns>List<dynamic></returns>
    public static List<dynamic> ToDynamicList(this DataTable table, bool reverse = true, params string[] FilterField)
    {
        var modelList = new List<dynamic>();
        foreach (DataRow row in table.Rows)
        {
            dynamic model = new ExpandoObject();
            var dict = (IDictionary<string, object>)model;
            foreach (DataColumn column in table.Columns)
            {
                if (FilterField.Length != 0)
                {
                    if (reverse == true)
                    {
                        if (!FilterField.Contains(column.ColumnName))
                        {
                            dict[column.ColumnName] = row[column];
                        }
                    }
                    else
                    {
                        if (FilterField.Contains(column.ColumnName))
                        {
                            dict[column.ColumnName] = row[column];
                        }
                    }
                }
                else
                {
                    dict[column.ColumnName] = row[column];
                }
            }
            modelList.Add(model);
        }
        return modelList;
    }


    /// <summary>
    /// 将DataTable 转换成 dynamic<dynamic>
    /// reverse 反转：控制返回结果中是只存在 FilterField 指定的字段,还是排除.
    /// [flase 返回FilterField 指定的字段]|[true 返回结果剔除 FilterField 指定的字段]
    /// FilterField  字段过滤，FilterField 为空 忽略 reverse 参数；返回DataTable中的全部数
    /// </summary>
    /// <param name="table">DataTable</param>
    /// <param name="reverse">
    /// 反转：控制返回结果中是只存在 FilterField 指定的字段,还是排除.
    /// [flase 返回FilterField 指定的字段]|[true 返回结果剔除 FilterField 指定的字段]
    ///</param>
    /// <param name="FilterField">字段过滤，FilterField 为空 忽略 reverse 参数；返回DataTable中的全部数据</param>
    /// <returns>List<dynamic></returns>
    public static dynamic ToDynamic(this DataTable table, bool reverse = true, params string[] FilterField)
    {
        List<dynamic> modelList = ToDynamicList(table, reverse, FilterField);
        if (modelList.Count > 0)
        {
            return modelList[0];
        }
        return null;
    }

    public static dynamic ToDynamic(this DataRow row, bool reverse = true, params string[] FilterField)
    {
        //List<dynamic> modelList = ToDynamicList(row.Table, reverse, FilterField);
        //if (modelList.Count > 0)
        //{
        //    return modelList[0];
        //}
        //return null;

        dynamic model = new ExpandoObject();
        var dict = (IDictionary<string, object>)model;
        foreach (DataColumn column in row.Table.Columns)
        {
            dict[column.ColumnName] = row[column];
        }
        return model;
    }
}
