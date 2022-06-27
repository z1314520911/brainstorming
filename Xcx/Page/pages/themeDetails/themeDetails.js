let app = getApp();
let gd = app.globalData;
const util = require('../../utils/util.js');

Page({
  data: {
    id: 0,
    t: 1,
    info: {},
    imgUrl: gd.adminUrl,
    NickName: '',
    IsOwn: false,
    IsJoin: false,
    IsStart: false,
    IsFinish: false,
    ItemList: [],
  },
  onLoad: function (op) {
    var that = this;
    that.setData({
      id: op.id
    })
    that.getInfo();
  },
  getInfo: function () {
    var that = this;
    util.Post("/Idea/GetTheme", {
      id: that.data.id
    }, function (res) {
      if (res) {
        that.setData({
          info: res.data,
          NickName: res.NickName,
          IsOwn: res.IsOwn,
          IsJoin: res.IsJoin,
          IsStart:res.data.IsStart,
          IsFinish:res.IsFinish,
          ItemList: res.ItemList,
        })
        console.log(res.data.IsFinish)
      }
    })
  },

  IsStart: function (e) {
    {
      var that = this;
      let data = {
        id: that.data.id
      };
      util.Post("/Idea/StartTheme", data, function (res) {
        if (res.Code > 0) {
          that.setData({
            IsStart:true,
          })
        } else {
          util.alert("开始失败");
        }
      })
    }
  },

  IsFinish: function (e) {
    {
      var that = this;
      let data = {
        id: that.data.id
      };
      util.Post("/Idea/FinishTheme", data, function (res) {
        if (res.Code > 0) {
          that.setData({
            IsFinish:true,
          })
        } else {
          util.alert("结束失败");
        }
      })
    }
  },

  IsLast: function (e) {
    {
      var that = this;
      let data = {
        id: that.data.id
      };
      util.Post("/Idea/LastTheme", data, function (res) {
        if (res.Code > 0) {
          that.getInfo();
        } else {
          util.alert("完成失败");
        }
      })
    }
  },

  dianzan: function (e) {
    {
      var that = this;
      let id = e.currentTarget.dataset.id;
      let data = {
        id: id
      };
      util.Post("/Idea/LikeIdeaTheme", data, function (res) {
        if (res.Code > 0) {
          that.getInfo();
        } else {
          util.alert("点赞失败");
        }
      })
    }
  },

  /* 页面相关事件处理函数--监听用户下拉动作 */
  onPullDownRefresh: function () {
    var that = this;
    that.getInfo();
    //当逻辑执行完后关闭刷新    
    wx.stopPullDownRefresh();
  },

  /* 用户点击右上角分享 */
  // onShareAppMessage: function () {
  //   var that = this;
  //   var indexThemeId; //首页themeData的_id
  //   indexThemeId = that.data.id;
  //   return {
  //     title: '转发',
  //     path: 'pages/themeDetails/themeDetails?id=' + indexThemeId + '&join=' + 1,
  //     success: function (res) {}
  //   }
  // },
})

//   /* 跳转生成图片页面 */
//   toImg:function(){
//     wx.navigateTo({
//       url: '../img/img',
//     })
//   }

// })