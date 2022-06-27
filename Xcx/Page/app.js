
const url = require('utils/url.js')
const util = require('utils/util.js')

App({

  /**
   * 全局变量
   */
  globalData: {
    wechatshareid:0,
    user_id: null,
    listType: -99,
    apiUrl: wx.getStorageSync("apiUrl") ? wx.getStorageSync("apiUrl") : url.apiUrl,
    adminUrl: wx.getStorageSync("adminUrl") ? wx.getStorageSync("adminUrl") : url.adminUrl,
    wxOnLineUrl: wx.getStorageSync("wxOnLineUrl") ? wx.getStorageSync("wxOnLineUrl") : url.wxOnLineUrl,
    // wsUrl: url.wsUrl,
  },

  api_root: '', // api地址

  /**
   * 生命周期函数--监听小程序初始化
   */
  onLaunch: function () {
  },

  /**
   * 当小程序启动，或从后台进入前台显示，会触发 onShow
   */
  onShow: function (options) {
    // 获取小程序基础信息
    this.getWxappBase(function (wxapp) {
      // 设置navbar标题、颜色
      wx.setNavigationBarColor({
        frontColor: wxapp.navbar.top_text_color.text,
        backgroundColor: wxapp.navbar.top_background_color
      })
    });
    const updateManager = wx.getUpdateManager()
    updateManager.onCheckForUpdate(function (res) {
      // 请求完新版本信息的回调
      console.log(res.hasUpdate)
    })
    updateManager.onUpdateReady(function () {
      wx.showModal({
        title: '更新提示',
        content: '新版本已经准备好，是否重启应用？',
        success(res) {
          if (res.confirm) {
            // 新的版本已经下载好，调用 applyUpdate 应用新版本并重启
            updateManager.applyUpdate()
          }
        }
      })
    })
    updateManager.onUpdateFailed(function () {
      // 新版本下载失败
    })
  },

  /**
   * 获取小程序基础信息
   */
  getWxappBase: function (callback) {
    let App = this;
    // app._get('wxapp/base', {}, function (result) {
    //     // 记录小程序基础信息
    //     wx.setStorageSync('wxapp', result.data.wxapp);
    //     callback && callback(result.data.wxapp);
    // }, false, false);
  },

  /**
   * 执行用户登录
   */
  doLogin: function () {
    // 保存当前页面
    let pages = getCurrentPages();
    if (pages.length) {
      let currentPage = pages[pages.length - 1];
      "pages/login/login" != currentPage.route &&
        wx.setStorageSync("currentPage", currentPage);
    }
    // 跳转授权页面
    wx.navigateTo({
      url: "/pages/login/login"
    });
  },

  /**
   * 当前用户id
   */
  getUserId: function () {
    return wx.getStorageSync('user_id');
  },

  /**
   * 显示成功提示框
   */
  showSuccess: function (msg, callback) {
    wx.showToast({
      title: msg,
      icon: 'success',
      content: msg,
      success: function () {
        callback && (setTimeout(function () {
          callback();
        }, 1500));
      }
    });
  },

  /**
   * 显示失败提示框
   */
  showError: function (msg, callback) {
    wx.showModal({
      title: '友情提示',
      content: msg,
      showCancel: false,
      success: function (res) {
        callback && (setTimeout(function() {
          callback();
        }, 2500));
        // callback && callback();
      }
    });
  },

  /**
   * 验证是否存在user_info
   */
  validateUserInfo: function () {
    let user_info = wx.getStorageSync('user_info');
    return !!wx.getStorageSync('user_info');
  },

  /**
   * 对象转URL
   */
  urlEncode: function urlencode(data) {
    var _result = [];
    for (var key in data) {
      var value = data[key];
      if (value.constructor == Array) {
        value.forEach(function (_value) {
          _result.push(key + "=" + _value);
        });
      } else {
        _result.push(key + '=' + value);
      }
    }
    return _result.join('&');
  },

  /**
   * 设置当前页面标题
   */
  setTitle: function () {
    let App = this,
      wxapp;
    if (wxapp = wx.getStorageSync('wxapp')) {
      wx.setNavigationBarTitle({
        title: wxapp.navbar.wxapp_title
      });
    } else {
      app.getWxappBase(function () {
        app.setTitle();
      });
    }
  },

});

 /*
 app.json页的
      {
        "pagePath": "pages/find/index",
        "text": "发现",
        "iconPath": "images/find.png",
        "selectedIconPath": "images/find-active.png"
      }, 
      */