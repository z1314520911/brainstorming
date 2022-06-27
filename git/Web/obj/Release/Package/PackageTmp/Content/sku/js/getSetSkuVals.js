//获取已经设置的SKU
$("#getSetSkuVal").on("click", function () {
    debugger
	$("tr[class*='sku_table_tr']").each(function(){
		var propids = $(this).attr("propids");//SKU类型主键
		var propvalids = $(this).attr("propvalids");//SKU值主键
		
		
	});
});
