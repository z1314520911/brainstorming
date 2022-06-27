let app = getApp();
let gd = app.globalData;
const util = require('../../utils/util.js');

Page({
  data: {
    loadingHidden: false,
    type: 1,
  },
  onLoad: function (op) {
    var that = this;
    if (op.type) {
      that.setData({
        type: op.type
      });
    }
  },

  formSubmit: function (e) {
    let that = this;

    if (util.checkEmpty(e.detail.value.Name, '请输入问题!')) {
      return false;
    }
    if (util.checkEmpty(e.detail.value.Room, '请输入房间号!')) {
      return false;
    }

    if (e.detail.value.Room.length != 4) {
      wx.showToast({
        title: '请输入四位房间号!',
        icon: 'none',
        duration: 2000
      })
    } else {
      that.setData({
        loadingHidden: false,
      })

      var formData = e.detail.value;

      formData = util.createJson(formData, 'type', that.data.type);

      util.Post("/Idea/SaveTheme", formData, function (res) {
        that.setData({
          loadingHidden: true,
        })
        if (res.Code > 0) {
          util.toUrl("../themeDetails/themeDetails?id=" + res.Code);
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