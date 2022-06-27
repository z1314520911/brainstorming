<%@ Page Title="" Language="C#" Inherits="System.Web.Mvc.ViewPage<PagerModel>" %>

<%@ Import Namespace="System.Data" %>
<%@ Import Namespace="Newtonsoft.Json.Linq" %>
<%@ Import Namespace="Commons" %>
<%@ Import Namespace="Model" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head>
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title>SKU</title>
    <link type="text/css" rel="stylesheet" href="/content/sku/css/sku_style.css" />
    <style type="text/css">
        .cusUL {
            width: 99%;
            float: left;
        }

        .noBorder {
            border: none;
        }
    </style>

    <script type="text/javascript" src="/content/sku/js/jquery.min.js"></script>
    <script type="text/javascript" src="/content/sku/js/createSkuTable.js"></script>
    <script type="text/javascript" src="/content/sku/js/customSku.js"></script>
    <script type="text/javascript" src="/content/sku/js/getSetSkuVals.js"></script>
    <script type="text/javascript">

        function HtmlTableToJson(tableid) {
            // table need set thead and tbody
            // talbe head need set name attr
            // value control need set josnval attr or will get null string
            var jsondata = [];
            // table head
            var heads = [];
            $("#" + tableid + ' th').each(function (index, item) {
                heads.push($(item).attr('name'));
            });
            // tbody
            $("#" + tableid + " tr[class*='sku_table_tr']").each(function (index, item) {
                var rowdata = {};
                $(item).find('td').each(function (index, item) {
                    if ($(item).find('[josnval]').size() > 0) {
                        console.log("have json val");
                        rowdata[heads[index]] = $(item).find('[josnval]').val();
                    } else {
                        console.log("no jsonval");
                        rowdata[heads[index]] = "";
                    }
                });
                rowdata.id = $(item).attr("propvalids").split(",")[0];
                jsondata.push(rowdata);
            });
            return jsondata;
        }

        function tj() {
            var json = HtmlTableToJson("cusTable");
            //alert(JSON.stringify(json));

            var url = "/Admin/T_Goods/SaveAttr.aspx";
            $.ajax({
                url: url,
                dataType: "json",
                type: "post",
                data: { id: "<%= ViewBag.GoodsId %>", list: JSON.stringify(json) },
                success: function (res) {
                    layer.msg(res.Message);
                },
                complete: function () {
                    layer.close(loadIndex);
                }
            })
        }
        
    </script>
</head>
<body class="">
    
<%
    DataTable dt = ViewBag.Dt as DataTable;
    Dictionary<string, string> dict = new Dictionary<string, string>();
    //List<T_GoodsPrice> list = 
    foreach (DataRow row in dt.Rows)
    {
        JObject jObject = JObject.Parse(row["Attribute"].ToString());
        foreach (var obj in jObject)
        {
            if (dict.ContainsKey(obj.Key))
            {
                if(dict[obj.Key].IndexOf(obj.Value.ToString()) == -1)
                {
                    dict[obj.Key] = dict[obj.Key] + "," + obj.Value.ToString();
                }
            }
            else
                dict.Add(obj.Key, obj.Value.ToString());
        }
    }
    JObject jo = JObject.Parse(JsonUtility.Serialize(dict));

    string json = JsonUtility.Serialize(dt);
    //Response.Write(JsonUtility.Serialize(dict));
%>
    <ul class="SKU_TYPE" style="display: none;">
        <li is_required='0' propid='0' sku-type-name="666666">666666：</li>
    </ul>
    <ul style="display: none;">
        <li>
            <label>
                <input type="checkbox" class="sku_value" propvalid='1' value="666666" />666666</label></li>
    </ul>
    <div class="clear"></div>
<%
    int rowIndex = 0;
    int id = 1;
    string ckid = string.Empty;
    foreach (var item in dict)
    {
        id = dt.Rows[rowIndex++]["id"].Int();
%>
    <div>
        <ul class="SKU_TYPE">
            <li is_required='0' propid='<%= id %>' sku-type-name="<%= item.Key %>">
                <input type="text" class="cusSkuTypeInput" value='<%= item.Key %>' readonly="readonly" />
                <a href="javascript:void(0);" class="delCusSkuType">移除</a>
            </li>
        </ul>
        <ul class="cusUL">
        <%
            string[] arrs = item.Value.Split(',');
            int cIndex = 1;
            foreach (string value in arrs)
            {
                ckid = "ck_" + id + "_" + cIndex;
        %>
            <li>
                <label>
                    <%--<input type="checkbox" class="sku_value" propvalid='1' value='<%= value %>' />--%>
                    <input type="checkbox" id="<%= ckid %>" class="sku_value" propvalid='<%= cIndex %>' propid='<%= id %>' value='<%= value %>' checked="checked" />
                    <input type="text" class="cusSkuValInput" value="<%= value %>" />
                </label>
                <a href="javascript:void(0);" class="delCusSkuVal">删除</a>
            </li>
        <%
                cIndex++;
            }
        %>
            <button class="cloneSkuVal">添加属性</button>
        </ul>
        <div class="clear"></div>
    </div>
<%
        id++;
    }
%>
    <script type="text/javascript">
        var json = <%= json %>; //alert(json[0].Attribute);
        $(function(){
            setSkuVals();
            
            //获取设置的SKU属性值
            $("tr[class*='sku_table_tr']").each(function (index, item) {
                $(this).find("input[type='text'][class*='setting_sku_price']").val(json[index].Price);//SKU价格
                $(this).find("input[type='text'][class*='setting_sku_stock']").val(json[index].Inventory);//SKU库存
            });
        });
    </script>
    <button class="cloneSku">添加规格</button>

    <!--sku模板,用于克隆,生成自定义sku-->
    <div id="skuCloneModel" style="display: none;">
        <div class="clear"></div>
        <ul class="SKU_TYPE">
            <li is_required='0' propid='' sku-type-name="">
                <input type="text" class="cusSkuTypeInput" placeholder="规格名称 (比如: 重量)" />
                <a href="javascript:void(0);" class="delCusSkuType">移除</a>
            </li>
        </ul>
        <ul class="cusUL">
            <li>
                <input type="checkbox" class="model_sku_val" propvalid='' />
                <input type="text" class="cusSkuValInput" />
            </li>
            <button class="cloneSkuVal">添加属性</button>
        </ul>
        <div class="clear"></div>
    </div>
    <!--单个sku值克隆模板-->
    <li style="display: none;" id="onlySkuValCloneModel">
        <input type="checkbox" class="model_sku_val" propvalid='' value="" />
        <input type="text" class="cusSkuValInput" />
        <a href="javascript:void(0);" class="delCusSkuVal">删除</a>
    </li>
    <div class="clear"></div>
    <div id="skuTable"></div>

    <ul>
        <li>
            <input type="button" id="getSetSkuVal" class="l-button" value="提交" onclick="tj();" /></li>
    </ul>



</body>
</html>
