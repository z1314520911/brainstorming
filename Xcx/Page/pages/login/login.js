let app = getApp();
let gd = app.globalData;
const util = require('../../utils/util.js');
const url = require('../../utils/url.js');

Page({
  data: {
    loadingHidden: true,
    //判断小程序的API，回调，参数，组件等是否在当前版本可用。
    canIUse: wx.canIUse('button.open-type.getUserInfo'),
    url: 'pages/index/index',
    urlReg: '/pages/login/register',
    openId: '',
    wechatshareid: '',
    info: '',
    canIUseGetUserProfile: false,
  },
  onLoad: function (options) {

    if (wx.getUserProfile) {
      this.setData({
        canIUseGetUserProfile: true
      })
    }
    
    wx.hideLoading()
    var that = this;
    
    //let wechatshareid = decodeURIComponent(options.scene);
    let wechatshareid = app.globalData.wechatshareid;
    console.log("login: id = " + wechatshareid);
    that.setData({
      wechatshareid: wechatshareid,
    })
    // that.login(that);
    //app.getOpenIdFun();
    if (options.url) {
      let url = "/" + options.url + "?";
      for (let obj in options) {
        if (obj != "url") {
          url += obj + "=" + options[obj] + "&"
        }
      }
      url = url.substring(0, url.length - 1)
      that.setData({
        url: url,
      })

      if (wx.getStorageSync('apiUrl')) {
        that.setData({
          info: '测试环境',
        })
      }
    }
  },
  bindGetUserInfo: function (e) {
    if (e.detail.userInfo) {
      //用户按了允许授权按钮
      var that = this;
      that.setData({
        loadingHidden: false
      });
      //console.log(e.detail.userInfo); 
      var ui = e.detail.userInfo;
      wx.setStorageSync('wxUser', ui);//wxUser 
       
      that.autoLoginReg(that, ui);

    } else {
      //用户按了拒绝按钮
      wx.showModal({
        title: '警告',
        content: '您点击了拒绝授权，将无法进入小程序，请授权之后再进入!!!',
        showCancel: false,
        confirmText: '返回授权',
        success: function (res) {
          if (res.confirm) {
            //console.log('用户点击了“返回授权”')
          }
        }
      })
    }
  },
  bindGetUserProfile: function (e) {
      wx.getUserProfile({
        desc: '用于完善会员资料', // 声明获取用户个人信息后的用途，后续会展示在弹窗中，请谨慎填写
        success: (res) => {
            //用户按了允许授权按钮
            var that = this;
            that.setData({
              loadingHidden: false
            });
          //console.log(e.detail.userInfo); 
          var ui = res.userInfo;
          wx.setStorageSync('wxUser', ui);//wxUser 
          
          that.autoLoginReg(that, ui);
          this.setData({
            userInfo: res.userInfo,
            hasUserInfo: true
          })
        },
        fail: (res) => {
          //用户按了拒绝按钮
          wx.showModal({
            title: '警告',
            content: '您点击了拒绝授权，将无法进入小程序，请授权之后再进入!!!',
            showCancel: false,
            confirmText: '返回授权',
            success: function (res) {
              if (res.confirm) {
                //console.log('用户点击了“返回授权”')
              }
            }
          })
        }
      })
  },
  //获取用户信息接口
  queryUsreInfo: function () {
    var that = this;
    var data = { openId: that.openId };
    util.Get("/User/GetByOpenId", data, function (res) {
      //console.log(res);
      var data = JSON.parse(res);
      gd.user = data;
      //跳转
      wx.redirectTo({
        url: that.data.url
      });
    });

  },
  autoLoginReg: function (that, ui) {
    wx.showLoading({ title: '加载中' })
    let jscode = '';
    //var that_ = this;
    //console.log('id2=' + that_.data.wechatshareid);

    wx.login({
      success: function (res) {
        if (res.code) {
          jscode=res.code;
          //2、调用获取用户信息接口
          wx.getUserInfo({
            success: function (res) {
              encryptedData: res.encryptedData;
              iv:res.iv;
              //3.解密用户信息 获取unionId
              //console.log("encryptedData:" + res.encryptedData);
              //console.log("iv:" + res.iv);
              //console.log("jscode:" + jscode);

              var data = { opr: "getcode", jscode: jscode, name: ui.nickName, avatarUrl: ui.avatarUrl, province: ui.province, city: ui.city, encryptedData: res.encryptedData, iv: res.iv, wechatshareid: that.data.wechatshareid }
              util.Get(gd.wxOnLineUrl + "/WxXcx/GetOpenId", data, function (res) {
                wx.hideLoading()
                that.setData({
                  loadingHidden: true
                });
                gd.user_id = res.Code;
                app.globalData.user_id = res.Code;

                console.log("gd.user_id=" + gd.user_id);

                let version = res.Message && res.Message.split('/')[4]
                console.log(res.Message + "★ version=" + version)
                // wx.showToast({
                //   title: version,
                //   icon: 'success',
                //   duration: 3000
                // })
                //正式版
                let apiUrl = wx.getStorageSync("apiUrl") ? wx.getStorageSync("apiUrl") : url.apiUrl
                let adminUrl = wx.getStorageSync("adminUrl") ? wx.getStorageSync("adminUrl") : url.adminUrl
                let wxOnLineUrl = wx.getStorageSync("wxOnLineUrl") ? wx.getStorageSync("wxOnLineUrl") : url.wxOnLineUrl
                let isTest = false
                //开发者工具
                if (version == "devtools") {
                  apiUrl = url.apiUrl_t
                  wxOnLineUrl = url.wxOnLineUrl_t
                  adminUrl = url.adminUrl_t
                  isTest = true
                }
                //开发版，体验版，审核版
                if (version == 0) {
                  apiUrl = url.apiUrl_t_online
                  wxOnLineUrl = url.apiUrl_t_online
                  adminUrl = url.adminUrl_t_online
                  isTest = true
                }
                
                if (isTest) {
                  // that.setData({ info: "测试环境" })
                  gd.apiUrl = apiUrl
                  gd.adminUrl = adminUrl
                  gd.wxOnLineUrl = wxOnLineUrl
                  wx.setStorageSync("apiUrl", apiUrl)
                  wx.setStorageSync("adminUrl", adminUrl)
                  wx.setStorageSync("wxOnLineUrl", wxOnLineUrl)
                }
                console.log(version + "★" + apiUrl + "★" + adminUrl + "★" + wxOnLineUrl)

                if (res.Code > 0) {
                  var data = JSON.parse(res.Detail);
                  gd.user = data;
                  console.log(data)
                  wx.setStorageSync('user', data)
                  let url = that.data.url;
                  
                  app.showSuccess("登录成功")
                  util.toUrl(url)
                } else {
                  app.showError(res.Message);
                }
              })
            },
            fail: function () {
              console.log('获取用户信息失败')
            }
          });

        } else {
          console.log('获取用户登录失败！' + res.errMsg);
        }
        that.setData({
          loadingHidden: true
        })
      }
    });
  },
  loginClose: function (e) {
    let url = "/pages/index/index"

    wx.showModal({
      title: '友情提示',
      content: "取消登录",
      success: function (res) {
        if (res.confirm) util.toUrl(url)
      }
    })
  }

})