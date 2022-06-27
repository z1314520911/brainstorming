let app = getApp();
let gd = app.globalData;
const util = require('../../utils/util.js');

Page({
  data: {
    loadingHidden: false,
    id: 0,
  },
  onLoad: function (op) {
    var that = this;
    if (op.id) {
      that.setData({
        id: op.id
      });
      util.Post("/Idea/GetIdeaTheme", {id: op.id}, function (res) {
        that.setData({
          model: res,
        });
      });
    }
  },

  formSubmit: function (e) {
    let that = this;

    if (util.checkEmpty(e.detail.value.Idea0, '请输入观点1!')) {
      return false;
    }
    else if (util.checkEmpty(e.detail.value.Idea1, '请输入观点2!')) {
      return false;
    }
    else if (util.checkEmpty(e.detail.value.Idea2, '请输入观点3!')) {
      return false;
    } else {
      that.setData({
        loadingHidden: false,
      })

      var pages = getCurrentPages();
      var prevPage = pages[pages.length - 2];   //上一个页面

      var formData = e.detail.value;

      formData = util.createJson(formData, 'id', that.data.id);

      util.Post("/Idea/SaveIdeaTheme", formData, function (res) {
        that.setData({
          loadingHidden: true,
        })
        if (res.Code > 0) {
          prevPage.getInfo();
    
          wx.navigateBack({
            delta: 1
          })
        } else {
          util.alert(res.Message);
        }
      });
    }
  },

  JoinOn: function (e) {
    {
      var that = this;
      let data = {
        room: that.data.room
      };
      util.Post("/Idea/JoinTheme", data, function (res) {
        if (res.Code > 0) {
          let url = "../themeDetails/themeDetails?id=" + res.Code;

          util.toUrl(url);
        } else {
            util.alert("加入失败");
        }
      })
    }
  },


  bindinput: function (e) {
    this.setData({
      room: e.detail.value
    })
  },

})