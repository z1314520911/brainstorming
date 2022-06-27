
//var url ='/Admin/T_Order/ExpressUpload.aspx';
function UpExcel(url) {
debugger
    let file = $("#myxls")[0].files[0];
    var formData = new FormData();
    formData.append("myxls", $("#myxls")[0].files[0]);
    console.log(file);
    layer.load(2, {
        shade: [0.9, '#6a6a6a']
    });
    axios.post(url, formData)
        .then(function (res) {
            layer.closeAll('loading');
            console.log(res);
            if (res.data.Message != "") {
                alert(res.data.Message);
            }
            if (res.data.Code > 0) {
                window.location.reload();
            }
        })
        .catch(function (error) {
            console.log(error);
        });
}
