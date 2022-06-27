let app = getApp();
let gd = app.globalData
let url = require('../../utils/url.js');
let util = require('../../utils/util.js');

Page({

  /* 页面的初始数据*/
  data: {
    // canIUse: wx.canIUse('button.open-type.getUserInfo'),
    // avatarUrl: "",
    // userInfo:{}
    themeData: {},
    openid: '',
    items: [{
        name: '1',
        value: '主持人'
      },
      {
        name: '2',
        value: '参与者',
        checked: false
      },
    ],
    index: 0,
  },

  /* 生命周期函数--监听页面加载 */
  onLoad: function (options) {
    if (app.globalData.openid) {
      this.setData({
        openid: app.globalData.openid
      })
    }
    this.onQuery();
    // console.log('333');
    // console.log(app.onQuery("themeData", "_openid", this.data.openid));
    // this.setData({
    //   themeData: app.onQuery("themeData", "_openid", this.data.openid)
    // })
  },
  radioChange(e) {
    console.log('radio发生change事件，携带value值为：', e.detail.value);
    this.setData({
      index: e.detail.value
    })
  },

  /* 获取主题及备注等信息 */
  // onQuery: function() {
  //   var that = this;
  //   const db = wx.cloud.database()
  //   wx.showNavigationBarLoading();  //显示导航条加载动画
  //   db.collection('themeData').where({
  //     _openid: this.data.openid
  //   }).get({
  //     success: res => {
  //       wx.hideNavigationBarLoading();
  //       console.log('[数据库“themeData”] [查询记录] 成功: ', res.data);
  //       var themeData = [];
  //       for (var idx in res.data) {
  //         var subject = res.data[idx];
  //         var theme = subject.theme;  //主题名
  //         var remark = subject.remark;
  //         if (theme.length > 6) {
  //           theme = theme.substring(0, 6) + '...';  //名字太长就要截取
  //         }
  //         if (remark.length > 42) {
  //           remark = remark.substring(0, 42) + '…'; //备注太长就要截取
  //         }

  //         var temp = {
  //           theme: theme,
  //           remark: remark,
  //           themeId: subject._id,
  //           openId: subject._openid
  //         }
  //         themeData.push(temp); //把数据push到数组里
  //       }
  //       themeData.reverse(); //数组逆序
  //       that.setData({
  //         themeData: themeData
  //       })
  //       console.log('裁剪过后存的数据：', that.data);
  //     },
  //     fail: err => {
  //       // wx.showToast({
  //       //   icon: 'none',
  //       //   title: '查询记录失败'
  //       // })
  //       console.error('首页查询失败：', err)
  //     }
  //   })
  // },

  onQuery: function () {
    // var that = this;
    // var themeData = [];
    // wx.showNavigationBarLoading(); //显示导航条加载动画
    // db.collection('themeAndPerson').where({
    //   _openid: that.data.openid
    // }).get({
    //   success: res => {
    //     var themeIds = res.data[0].theme;
    //     // console.log(themeIds)
    //     for (var idx in themeIds) {
    //       var themeDataId = themeIds[idx];
    //       // console.log(themeDataId)
    //       db.collection('themeData').doc(themeIds[idx]).get({
    //         success: res => {
    //           var subject = res.data;
    //           var theme = subject.theme;
    //           var remark = subject.remark;
    //           // console.log(subject)
    //           if (theme.length > 6) {
    //             theme = theme.substring(0, 6) + '...'; //名字太长就要截取
    //             // console.log(theme)
    //           }
    //           if (remark.length > 42) {
    //             remark = remark.substring(0, 42) + '…'; //备注太长就要截取
    //           }
    //           var temp = {
    //             theme: theme,
    //             remark: remark,
    //             themeId: subject._id,
    //             openId: subject._openid
    //           }
    //           themeData.push(temp); 
    //           themeData.reverse(); //数组逆序
    //           that.setData({
    //             themeData: themeData
    //           })
    //           wx.hideNavigationBarLoading()
    //         },
    //         fali: err => {

    //         }
    //       })
    //     }
    //   },
    //   fail: err => {

    //   }
    // })
  },

  /*  */
  onNewTheme: function () {
    var that = this;
    if (that.data.index == 0) {
      util.alert("请选择角色");

    } else {
      wx.navigateTo({
        url: '../newTheme/newTheme?type='+that.data.index,
        success: function (res) {},
        fail: function (res) {},
        complete: function (res) {},
      })
    }
  },

  /*  */
  onThemeDetails: function (event) {
    var themeId = event.currentTarget.dataset.themeId;
    // console.log(themeId);
    wx.navigateTo({
      url: '../themeDetails/themeDetails?indexThemeId=' + themeId + '&join=' + 0
      // url: 'post-detail/post-detail?id='+postId
    })
  },


  //获取openId
  // onGetOpenid: function () {
  //   // 调用云函数
  //   wx.cloud.callFunction({
  //     name: 'login',
  //     data: {},
  //     success: res => {
  //       console.log('[云函数] [login] user openid: ', res.result.openid)
  //       app.globalData.openid = res.result.openid;
  //       // this.onQuery();
  //     },
  //     fail: err => {
  //       console.error('[云函数] [login] 调用失败', err)
  //     }
  //   })
  // },

  /* 生命周期函数--监听页面初次渲染完成 */
  onReady: function () {

  },

  /* 生命周期函数--监听页面显示 */
  onShow: function () {

  },

  /* 生命周期函数--监听页面隐藏 */
  onHide: function () {

  },

  /* 生命周期函数--监听页面卸载 */
  onUnload: function () {

  },

  /* 页面相关事件处理函数--监听用户下拉动作 */
  onPullDownRefresh: function () {
    var that = this;
    that.onQuery();
    //当逻辑执行完后关闭刷新    
    wx.stopPullDownRefresh();
  },

})