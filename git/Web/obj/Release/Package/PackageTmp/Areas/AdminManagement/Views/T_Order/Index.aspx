<%@ Page Title="" Language="C#" Inherits="System.Web.Mvc.ViewPage" %>

<!DOCTYPE html>
<html>
<head>
    <meta charset="utf-8">
    <title>数据统计</title>
    <meta name="renderer" content="webkit">
    <meta http-equiv="X-UA-Compatible" content="IE=edge,chrome=1">
    <meta name="viewport" content="width=device-width, initial-scale=1.0, minimum-scale=1.0, maximum-scale=1.0, user-scalable=0">
    <link rel="stylesheet" href="/content/layui/css/layui.css" media="all">
    <link rel="stylesheet" href="/content/layui/build/css/app.css" media="all">
    <link rel="stylesheet" href="/content/layui/build/css/admin.css" media="all">

    <script type="text/javascript" src="/Content/admin/js/jquery.js"></script>
    <script type="text/javascript" src="/content/layui/layui.js"></script>
    <script type="text/javascript" src="/content/layui/lib/echarts.min.js"></script>
    <style>
        .layui-table{margin:0 0;}
    </style>
</head>
<body>
    <%
        System.Data.DataTable dt = ViewBag.Data as System.Data.DataTable;
    %>
    <div class="layui-fluid">
        <div class="layui-row layui-col-space15">            

            <%--本月销售报表b--%>
            <div class="layui-col-sm12">
                <div class="layui-card">
                    <div class="layui-card-header">
                        本月销售报表
                    </div>
                    <div class="layui-card-body">
                        <div class="layui-row">
                            <div class="layui-col-sm12">
                                <div class="layui-carousel" style="height: 260px;background-color: #fff;">


                                    <div id="chart" style="width: 100%; height: 280px;"></div>
                                    <%--<div><i class="layui-icon layui-icon-loading1 layadmin-loading"></i></div>--%>
                                </div>
                            </div>

                        </div>
                    </div>
                </div>
            </div>
            <%--本月销售报表e--%>
            
            <div class="layui-col-sm6 layui-row layui-col-space15">
                <div class="layui-col-sm6 layui-col-md6">
                    <div class="layui-card">
                        <div class="layui-card-header">
                            用户数<span class="layui-badge layui-bg-red layuiadmin-badge">人</span>
                        </div>
                        <div class="layui-card-body layuiadmin-card-list">

                            <p class="layuiadmin-big-font"><span id="s"><%= dt.Rows[0]["mans"] %></span></p>
                            <p>
                                总收入<span class="layuiadmin-span-color">*** <i class="layui-inline layui-icon layui-icon-dollar"></i></span>
                            </p>
                        </div>
                    </div>
                </div>
                <div class="layui-col-sm6 layui-col-md6" onclick='openIframe2(0,0,"分销商");'>
                    <div class="layui-card">
                        <div class="layui-card-header">
                            分销商数<span class="layui-badge layui-bg-red layuiadmin-badge">人</span>
                        </div>
                        <div class="layui-card-body layuiadmin-card-list">

                            <p class="layuiadmin-big-font"><span id="s"><%= dt.Rows[0]["fxs"] %></span></p>
                            <p>
                                总收入<span class="layuiadmin-span-color">*** <i class="layui-inline layui-icon layui-icon-dollar"></i></span>
                            </p>
                        </div>
                    </div>
                </div>

                <div class="layui-col-sm6 layui-col-md6">
                    <div class="layui-card">
                        <div class="layui-card-header">
                            月销量<span class="layui-badge layui-bg-blue layuiadmin-badge">月</span>
                        </div>
                        <div class="layui-card-body layuiadmin-card-list">
                            <p class="layuiadmin-big-font"><span id="s1"><%= dt.Rows[2]["sales"] %></span></p>
                            <p>
                                订单数<span class="layuiadmin-span-color"><span id="s2"><%= dt.Rows[2]["orders"] %></span> <i class="layui-inline layui-icon layui-icon-flag"></i></span>
                            </p>
                        </div>
                    </div>
                </div>
                <div class="layui-col-sm6 layui-col-md6">
                    <div class="layui-card">
                        <div class="layui-card-header">
                            七日销量<span class="layui-badge layui-bg-blue layuiadmin-badge">天</span>
                        </div>
                        <div class="layui-card-body layuiadmin-card-list">

                            <p class="layuiadmin-big-font"><span id="s7"><%= dt.Rows[3]["sales"] %></span></p>
                            <p>
                                订单数<span class="layuiadmin-span-color"><span id="s8"><%= dt.Rows[3]["orders"] %></span> <i class="layui-inline layui-icon layui-icon-user"></i></span>
                            </p>
                        </div>
                    </div>
                </div>
                
                <div class="layui-col-sm6 layui-col-md6">
                    <div class="layui-card">
                        <div class="layui-card-header">
                            昨日销量<span class="layui-badge layui-bg-orange layuiadmin-badge">天</span>
                        </div>
                        <div class="layui-card-body layuiadmin-card-list">
                            <p class="layuiadmin-big-font"><span id="s5"><%= dt.Rows[1]["sales"] %></span></p>
                            <p>
                                订单数<span class="layuiadmin-span-color"><span id="s6"><%= dt.Rows[1]["orders"] %></span> <i class="layui-inline layui-icon layui-icon-face-smile-b"></i></span>
                            </p>
                        </div>
                    </div>
                </div>
                <div class="layui-col-sm6 layui-col-md6">
                    <div class="layui-card">
                        <div class="layui-card-header">
                            今日销量<span class="layui-badge layui-bg-orange layuiadmin-badge">天</span>
                        </div>
                        <div class="layui-card-body layuiadmin-card-list">

                            <p class="layuiadmin-big-font"><span id="s7"><%= dt.Rows[0]["sales"] %></span></p>
                            <p>
                                订单数<span class="layuiadmin-span-color"><span id="s8"><%= dt.Rows[0]["orders"] %></span> <i class="layui-inline layui-icon layui-icon-user"></i></span>
                            </p>
                        </div>
                    </div>
                </div>
            </div>
            
            <%--商品销售排行b--%>
            <div class="layui-col-sm6">
                <div class="layui-card">
                    <div class="layui-card-header">
                        <span id="spanDR">商品销售排行</span>
                        <div class="layui-btn-group layuiadmin-btn-group">
                          <a href="javascript:;" class="layui-btn layui-btn-primary layui-btn-xs" onclick="getDayReport(1);">今日</a>
                          <a href="javascript:;" class="layui-btn layui-btn-primary layui-btn-xs" onclick="getDayReport(2);">昨日</a>
                        </div>
                    </div>
                    <div style="padding: 10px 15px;">
                        <%-- 数据表格 --%>
                        <table class="layui-hide" id="layListId" lay-filter="layList"></table>
                        
						<%--<script type="text/html" id="barDemo">
							<button type="button" class="layui-btn layui-btn-sm layui-btn-warm" lay-event="maintainRecord">保养记录</button>
							<button type="button" class="layui-btn layui-btn-sm layui-btn-warm" lay-event="repairRecord">维修记录</button>
						</script>--%>

                        <%--<div class="layui-row">
                            <div class="layui-col-sm12">
                                <div class="layui-carousel layadmin-carousel layadmin-dataview">

                                </div>
                            </div>

                        </div>--%>
                    </div>
                </div>
            </div>
            <%--商品销售排行e--%>

        </div>
    </div>
    
    
    <script src="/content/admin/js/JScript.js"></script>
    <script type="text/javascript">        var area2 = ['92%', '92%'];
        var url2 = ['/Admin/T_Order/ListFxs.aspx?id='];
    </script>

    <script type="text/javascript">
        getMonthChartJson(0);
        getDayReport(1);
        $(function () {
            //注意：这里是数据表格的加载数据，必须写
            layui.use(['table', 'layer', 'form'], function () {
                var table = layui.table;
                layer = layui.layer;
                form = layui.form;
                //CURD...
            }); 
        });

        function getMonthChartJson(m) {
            var myChart = echarts.init(document.getElementById('chart'));
            //图表显示提示信息
            myChart.showLoading({
                text: "图表数据正在努力加载..."
            });      
            //定义图表options
            var options = {
                title: {
                    text: "月销售数据",
                },
                //右侧工具栏
                toolbox: {
                    show: true,
                    feature: {
                        //magicType: { show: true, type: ['line', 'bar'] },
                        //mark: { show: true },
                        //dataView: { show: true, readOnly: false },
                        //restore: { show: true },
                        //saveAsImage: { show: true }
                    
                        myTool3: {
                            show: true,
                            title: '下载报表',
                            icon: 'image:///images/1.jpg',
                            onclick: function () {
                                alert('下载报表')
                            }
                        }
                    }
                },
                tooltip: {
                    trigger: 'axis'
                },
                legend: {
                    data: []
                },
                calculable: true,
                xAxis: [
                    {
                        type: 'category',
                        name: '月份',
                        data: []
                    }
                ],
                yAxis: [
                    {
                        type: 'value',
                        name: '销售额',
                        axisLabel: {
                            formatter: '{value} Y'
                        },
                        splitArea: { show: true }
                    },
                    {
                        type: 'value',
                        name: '件数',
                        show: false,
                        axisLabel: {
                            formatter: '{value} M3'
                        },
                        splitArea: { show: true }
                    }
                ],
                series: []
            };
            //通过Ajax获取数据
            $.ajax({
                type: "get",
                async: false,
                contentType: 'application/json; charset=utf-8',
                data: { action: "Report", m: m },
                url: "/Api.ashx",
                dataType: "json", //返回数据形式为json
                success: function (obj) {
                    if (obj) {
                        //将返回的category和series对象赋值给options对象内的category和series
                        //因为xAxis是一个数组 这里需要是xAxis[i]的形式
                        //options.yAxis[0].data = 1;
                        options.xAxis[0].data = obj.category;
                        options.series = obj.series;
                        options.legend.data = obj.legend;
                        myChart.hideLoading();
                        myChart.setOption(options);


                    }
                },
                error: function (XMLHttpRequest, textStatus, errorThrown) {
                    alert(XMLHttpRequest.responseText);
                    //alert(XMLHttpRequest.status);
                    //alert(XMLHttpRequest.readyState);
                    //alert(textStatus);
                }
            })
        }

        function getDayReport(day) {
            if (day == 1) {
                $("#spanDR").html("商品销售排行-今日");
            } else {
                $("#spanDR").html("商品销售排行-昨日");
            }
            layui.use(['table'], function () {
                var table = layui.table;
                table.render({
			        elem: '#layListId',
			        id: 'layTableId',
			        url: '/Api.ashx?action=ReportDay&day='+day,
			        //title: '日商品销售排行',
			        cellMinWidth: 100,
		            cols: [
                        [
                            //{ type: 'checkbox', fixed: 'left', width: '30' },
                            //{ type: 'numbers', fixed: 'left', width: '36' },
                            { field: 'GoodsName', title: '商品名称', width: '577' },
                            { field: 'Number', title: '成交数量', width: '87' },
                            { field: 'Price', title: '成交金额', width: '87' }
                            //, { title: '操作', minWidth: '180', align: 'center', toolbar: '#barDemo' }
                        ]
		            ],
			        page: true
                });
                
		        //监听工具条
		        table.on('tool(layList)', function(obj) {
			        var data = obj.data; //获得当前行数据
			        switch(obj.event) {
				        case 'maintainRecord':
					        maintainRecord();
					        break;
				        case 'repairRecord':
					        repairRecord();
					        break;
				        default:
					        break;
			        }

		        });
		        var $ = layui.$,
			        active = {
				        reload: function() {
					        var deviceNumber = $("#deviceNumber").val();

					        //执行重载
					        table.reload('layTableId', {
						        page: { curr: 1 },
                                where: {
                                    deviceNumber: deviceNumber
                                }
					        });
				        },
				        getCheckData: function() { //获取选中数据
					        var checkStatus = table.checkStatus('layTableId'),
						        data = checkStatus.data;
					        layer.alert(JSON.stringify(data));
				        },
				        getCheckLength: function() { //获取选中数目
					        var checkStatus = table.checkStatus('layTableId'),
						        data = checkStatus.data;
					        layer.msg('选中了：' + data.length + ' 个');
				        },
				        isAll: function() { //验证是否全选
					        var checkStatus = table.checkStatus('layTableId');
					        layer.msg(checkStatus.isAll ? '全选' : '未全选')
				        }

			        };
		        $('.layui-btn').on('click', function() {
			        var type = $(this).data('type');
			        active[type] && active[type].call(this);
		        });

		        function maintainRecord() {
			        layer.msg("maintainRecord");
		        };

		        function repairRecord() {
			        layer.msg("repairRecord");

		        };
            }); 
        }
    </script>

</body>
</html>
