//const API_URL = getApp().globalData.Url

module.exports = {

  uuid: function (data){
    return 'xxxxxxxx-xxxx-4xxx-yxxx-xxxxxxxxxxxx'.replace(/[xy]/g, function (c) {
      var r = Math.random() * 16 | 0, v = c == 'x' ? r : (r & 0x3 | 0x8)
      return v.toString(16)
    })
  },
  pageSetData: function (data) {
    var page = getCurrentPages()
    [page.length - 4].setData({
      communityId: t.data.communityId,
      communityName: t.data.communityName
    })
    wx.navigateBack({
      delta: 3
    })
  },
  getMyDate: function(n){
    let myDate = new Date();
    let date;
    switch(n){
      case 1:       date = myDate.getYear(); break;       //获取当前年份(2位)
      case 2:       date = myDate.getFullYear(); break;    //获取完整的年份(4位,1970-????)
      case 3:       date = myDate.getMonth()+1; break;       //获取当前月份(0-11,0代表1月)
      case 4:       date = myDate.getDate(); break;        //获取当前日(1-31)
      case 5:       date = myDate.getDay(); break;         //获取当前星期X(0-6,0代表星期天)
      case 6:       date = myDate.getTime(); break;        //获取当前时间(从1970.1.1开始的毫秒数)
      case 7:       date = myDate.getHours(); break;       //获取当前小时数(0-23)
      case 8:       date = myDate.getMinutes(); break;     //获取当前分钟数(0-59)
      case 9:       date = myDate.getSeconds(); break;     //获取当前秒数(0-59)
      case 10:      date = myDate.getMilliseconds(); break;    //获取当前毫秒数(0-999)
      case 11:      date = myDate.toLocaleDateString(); break;     //获取当前日期
      case 12:      date = myDate.toLocaleTimeString(); break;    //获取当前时间
      case 12:      date = myDate.toLocaleString( );        //获取日期与时间
    }
    return date;
  },
  dayToWeek: function (data) {
    var date = new Date(Date.parse(data));
    const month = date.getDay();
    var weekDay;
    switch (month) {
      case 0: weekDay = '周日'; break;
      case 1: weekDay = '周一'; break;
      case 2: weekDay = '周二'; break;
      case 3: weekDay = '周三'; break;
      case 4: weekDay = '周四'; break;
      case 5: weekDay = '周五'; break;
      case 6: weekDay = '周六'; break
    }
    return weekDay;
  },
  addDay: function (data, t) {
    var timestamp = Date.parse(new Date());
    var newTimestamp = timestamp + data * 24 * 60 * 60 * 1000;
    var date = new Date(newTimestamp);

    const year = date.getFullYear();
    const month = date.getMonth() + 1;
    const day = date.getDate();

    if (t == 'md') {
      return [month, day].map(formatNumber).join('-');
    }
    if (t == 'w') {
      return this.dayToWeek([year, month, day].map(formatNumber).join('-'));
    }

    return [year, month, day].map(formatNumber).join('-');
  },
  formatDate: function (t, e) {
    var a = {
      "M+": (t = isNaN(t) ? t : new Date(t)).getMonth() + 1,
      "d+": t.getDate(),
      "H+": t.getHours(),
      "m+": t.getMinutes(),
      "s+": t.getSeconds(),
      "q+": Math.floor((t.getMonth() + 3) / 3),
      "f+": t.getMilliseconds()
    };
    /(y+)/.test(e) && (e = e.replace(RegExp.$1, (t.getFullYear() + "").substr(4 - RegExp.$1.length)));
    for (var o in a) new RegExp("(" + o + ")").test(e) && (e = e.replace(RegExp.$1, 1 === RegExp.$1.length ? a[o] : ("00" + a[o]).substr(("" + a[o]).length)));
    return e;
  },
  formatNumber: function (t) {
    return (t = t.toString())[1] ? t : "0" + t;
  },
  getDaysLater: function (t) {
    var e = new Date();
    return new Date(e.valueOf() + 864e5 * t);
  },
  getUTCDateInSeconds: function (t) {
    var e = t.split("-");
    return new Date(e[0], e[1] - 1, e[2], 0, 0, 0).getTime() / 1e3;
  },
  getTimeInSeconds: function (t) {
    var e = t.split(":");
    return 60 * e[0] * 60 + 60 * e[1];
  },
  getTimeString: function (t) {
    var e = Math.floor(t / 60 / 60);
    e < 10 && (e = "0" + e);
    var a = Math.floor((t - 60 * e * 60) / 60);
    return a < 10 && (a = "0" + a), e + ":" + a;
  },
  getDiffTime: function (a, o) {
    var n = this.getUTCDateInSeconds(a) + this.getTimeInSeconds(o) - new Date().getTime() / 1e3,
      s = "0天";
    if (n > 0) {
      var i = Math.floor(n / 86400),
        r = Math.floor(n / 3600);
      s = i > 0 ? i + "天" : r > 0 ? Math.floor(n / 3600) + "小时" : Math.floor(n / 60) + "分钟";
    }
    return s;
  },
  isTextEmpty: function (t) {
    return !t || "undefined" == t || 0 == t.trim().length;
  },
  isBlank: function (str) {
    if (Object.prototype.toString.call(str) === '[object Undefined]') {
      return true
    } else if (
      Object.prototype.toString.call(str) === '[object String]' ||
      Object.prototype.toString.call(str) === '[object Array]') {
      return str.length == 0 ? true : false
    } else if (Object.prototype.toString.call(str) === '[object Object]') {
      return JSON.stringify(str) == '{}' ? true : false
    } else {
      return true
    }
  },
  isElemInArray: function (t, e) {
    for (var a = 0; a < t.length; a++)
      if (t[a] == e) return !0;
    return !1;
  },
  GetByParams(url, page = 1, pageSize = 20, search = '') {
    const params = {
      start: (page - 1) * pageSize,
      pageSize: pageSize
    }
    return getApi(url, search ? Object.assign(params, {
      q: search
    }) : params)
      .then(res => res.data)
  },
  GetById(url, id) {
    return getApi(url, id)
      .then(res => res.data)
  },

  Get: function (url, data, fn, method = "get", header = "") {
    let app = getApp()
    let urlTmp = app.globalData.apiUrl + url
    if (url.indexOf("http://") >= 0 || url.indexOf("https://") >= 0)
      urlTmp = url
    wx.showLoading({ title: '加载中' });
    let that = this;
    var Token = this.getUser().Token;
    if (!that.isTextEmpty(Token)) {
      data = this.createJson(data, 'token', Token);
    }
    wx.request({
      url: urlTmp,
      method: method ? method : 'get',
      data: data,
      header: header ? header : {
        "Content-Type": "application/json"
      },
      success: function (res) {
        wx.hideLoading();
        // if (res && res.data && res.data == false) {
        if (res && res.data == false) {
          that.showLoginModal();
        } else {
          fn(res.data);
        }
      }
    });
  },
  Post: function (url, data, fn, method = "post", header = "") {
    
    let app = getApp()
    let urlTmp = app.globalData.apiUrl + url
    if (url.indexOf("http://") >= 0 || url.indexOf("https://") >= 0)
      urlTmp = url
    wx.showLoading({ title: '加载中' });
    let that = this;
    var Token = this.getUser().Token;
    if (!that.isTextEmpty(Token)) {
      data = that.createJson(data, 'token', Token);
    }
    wx.request({
      url: urlTmp,
      method: method ? method : 'post',
      data: data,
      header: header ? header : {
        "content-type": "application/x-www-form-urlencoded"
      },
      success: function (res) {
        wx.hideLoading();
        if (res && res.data == false) {
          that.showLoginModal();
        } else {
          fn(res.data);
        }
      }
    });
  },
  uploadImg: function (url, tempFilePaths, fn, id = 0, name = "", data = "") {
    let that = this;
    let app = getApp();
    
    wx.uploadFile({
      url: app.globalData.apiUrl + url,
      filePath: tempFilePaths[id],
      name: name ? name : 'uploadfile_fang',
      formData: data ? data : {
        "user": "fang",
        token: that.getUser().Token,
      },
      header: {
        "Content-Type": "multipart/form-data"
      },
      success: function (res) {
        fn(res.data, id);
        id++;
        if (tempFilePaths.length > id) {
          module.exports.uploadImg(url, tempFilePaths, fn, id, name = "", data = "");
        }
      }
    });
  },
  checkEmpty: function (value, info) {
    if (value.length == 0) {
      if (info == null || info.length == 0) {
        info = "数据不能为空";
      }
      wx.showToast({
        title: info,
        icon: 'none',
        duration: 2000
      })
      return true;
    } else {
      return false;
    }
  },
  createJson: function (str1, prop, val) {
    if (typeof val === "undefined") {
      // 删除属性
      delete str1[prop];
    } else {
      // 添加 或 修改
      str1[prop] = val;
    }
    return str1;
  },
  // success	显示成功图标，此时 title 文本最多显示 7 个汉字长度	
  // loading	显示加载图标，此时 title 文本最多显示 7 个汉字长度	
  // none	不显示图标，此时 title 文本最多可显示两行，1.9.0及以上版本支持	
  alert: function (info, icon) {
    if (info == null || info.length == 0) info = "参数错误"
    wx.showToast({
      title: info,
      icon: icon ? (icon == "s" ? "success" : icon) : "none",
      duration: 1600
    })
  },
  showLoading: function (t) {
    wx.showLoading ? wx.showLoading({
      title: t,
      mask: !0
    }) : wx.showToast({
      title: t,
      icon: "loading",
      mask: !0,
      duration: 2e4
    });
  },
  hideLoading: function () {
    wx.hideLoading ? wx.hideLoading() : wx.hideToast();
  },
  // success, success_no_circle, info, warn, waiting, cancel, download, search, clear
  showToast: function (t, i) {
    setTimeout(function () {
      wx.showToast({
        title: t,
        icon: i ? i : "success",
        duration: 1e3
      });
    }, 300);
  },
  showFailedToast: function (t, e) {
    var o = this.isTextEmpty(e) ? t : t + " " + e;
    wx.showModal({
      title: "提示",
      content: o,
      showCancel: !1,
      confirmText: "我知道了",
      confirmColor: "#ff5353"
    });
  },
  showModelTips: function (t, e) {
    wx.showModal({
      title: e || "提示",
      content: t,
      showCancel: !1,
      confirmText: "我知道了",
      confirmColor: "#ff5353"
    });
  },
  setStorage: function (t, e) {
    try {
      wx.setStorageSync(t, e);
    } catch (a) {
      wx.setStorageSync(t, e);
    }
  },
  getStorage: function h(t) {
    try {
      return wx.getStorageSync(t);
    } catch (e) {
      return wx.getStorageSync(t);
    }
  },
  removeStorage: function (t) {
    try {
      return wx.removeStorageSync(t);
    } catch (e) {
      return wx.removeStorageSync(t);
    }
  },
  buttonClicked: function (t) {
    t.setData({
      buttonClicked: true
    }), setTimeout(function () {
      t.setData({
        buttonClicked: false
      });
    }, 500);
  },

  bb: function (ss) {
    console.log(111);
  },
  checkPhone: function (value, info) {
    // let rule = /^1[3456789]\d{9}$/;
    let rule = /^[1][3,4,5,6,7,8,9][0-9]{9}$/;

    if (!rule.test(value)) {
      wx.showToast({
        title: info,
        icon: 'none',
        duration: 2000
      })
      return true;
    } else {
      return false;
    }
  },
  notice: function (that) {
    var app = getApp();
    if (app.globalData.notice && app.globalData.notice != '') {
      that.setData({
        notice: app.globalData.notice,
      });
    } else {
      this.AJAX("news.aspx", function (res) {
        if (res == '') {
          res = '1';
        }
        that.setData({
          notice: res,
        });
        app.globalData.notice = res;
      });
    }
  },
  scroll: function (that, url, data) {
    // 加载更多 loading
    that.setData({
      pageHidden: true
    })
    if (that.data.dataListPage == -1) {
      that.setData({
        pageHidden: false
      });
    } else {
      that.setData({
        hidden: false
      });
      this.Post(url, data, function (res) {
        var arr = res.dataList;
        if (arr&&(arr.length > 0 || that.data.dataListPage == 1)) {
          var list = that.data.dataList;
          that.setData({
            dataList: that.data.dataListPage == 1 ? arr : list.concat(arr),
            dataListPage: that.data.dataListPage + 1,
            hidden: true,
          });
        }
        if (!res.next || arr.length < 1) {
          that.setData({
            dataListPage: -1,
            pageHidden: false,
            hidden: true,
          });
        }
      });
    }
  },
  toUrl: function (url) {
    setTimeout(function () {
      wx.redirectTo({
        url: url,
        fail: function () {
          wx.switchTab({
            url: url
          })
        }
      })
    }, 500)    
  },
  showLoginModal: function () {
    let that = this;
    let url = '/pages/login/login?url=' + that.getUrlArgs();
    console.log("showLoginModal-url:" + url)
    //跳转
    wx.redirectTo({
      url: url,
      fail: function () {
        wx.switchTab({
          url: url
        });
      }
    });
  },
  // 判断是否登录
  isLogin: function (callback) {
    let that = this;
    // 需要 wx.checkSession({
    wx.showLoading({ title: '加载中' })
    wx.checkSession({
      success: function () {
        wx.hideLoading()
        // debugger
        //session_key 未过期，并且在本生命周期一直有效
        var user = wx.getStorageSync('user') || [];
        if (user.Token) {
          // 如果有全局存储的登录态，暂时认为他是登录状态
          return callback && callback()
          // that.Post("/WxXcx/CheckGuExist", {}, function (res) {
          //   if (res) {
          //     return callback && callback()
          //   }else{
          //     that.showLoginModal();
          //   }
          // });
        } else {
          // 如果没有登录态，弹窗提示一键登录
          that.showLoginModal();
        }
      },
      fail: function () {
        //需要执行登录流程
        that.showLoginModal();
      }
    })
  },
  logout: function () {
    try {
      wx.clearStorageSync('user');
      wx.clearStorageSync('wxUser');
      wx.clearStorageSync('open');
      this.alert("退出成功！");

      let url = '/pages/index/index';
      wx.redirectTo({
        url: url,
        fail: function () {
          wx.switchTab({
            url: url
          });
        }
      })
    } catch (e) { }
  },
  //post userId 和 unitId
  //gd.ChangeCartNumberById
  //更新用户绑定关系
  toPostUserIdAndUnitId: function (url, myscene) {
    let that = this;
    console.log(myscene);
    let sceneArr = myscene.split(",");
    let length = sceneArr.length;
    let unitId = -1;
    let userId = -1
    if (length >= 2) {
      unitId = sceneArr[0];
      userId = sceneArr[1];
    }
    if (unitId == -1 && userId == -1) {
      return false
    }
    let user = that.getUser();
    if (user.UserId == 0 || user.UserId == null || user.UnitId == 0 || user.UnitId == null) { //如果当前缓存数据不存在则获取更新
      let data = {
        id: user.Id,
        userId: userId,
        unitId: unitId
      };
      that.Post(url, data, function (res) {
        //var data = JSON.parse(res);//jsonstring change to json object
        var data = res;
        if (data.Code > 0) {
          if (data.Detail != '') {
            var userData = JSON.parse(data.Detail);
            wx.setStorageSync('user', userData); //更新缓存
          }
        } else {
          //修改失败的操作
          util.alert(data.Message);
        }
      });
    }
  },
  formatTime: function (date, hasTime) {
    const year = date.getFullYear()
    const month = date.getMonth() + 1
    const day = date.getDate()
    const hour = date.getHours()
    const minute = date.getMinutes()
    const second = date.getSeconds();

    // debugger
    let result = [year, month, day].map(formatNumber).join('/')
    if (hasTime) {
      result = result + ' ' + [hour, minute, second].map(formatNumber).join(':')
    }
    return result
  },
  formatMyTime: function (date, hasSecond) {
    const hour = date.getHours()
    const minute = date.getMinutes()
    const second = date.getSeconds()
    let result = hour + ':' + minute + (hasSecond ? ":" + second : "")
    return result;
  },
  findIndexById: function (array, uuid) {
    if (uuid) {
      for (let i in array) {
        if (array[i].uuid == uuid) {
          return i;
        }
      }
    }
    return -1
  },
  getDate: function (date) {
    return date.getFullYear() + '-' + formatNumber(date.getMonth() + 1) + '-' + formatNumber(date.getDate())
  },
  getTime: function (date) {
    return formatNumber(date.getHours()) + ':' + formatNumber(date.getMinutes())
  },
  setDate: function (str, date) {
    if (typeof (date) != typeof (Date)) {
      date = new Date(date)
    }
    console.log(typeof (str), str)
    console.log(typeof (date), date)
    let [year, month, day] = str.split('-')
    date.setFullYear(parseInt(year))
    date.setMonth(parseInt(month) - 1)//因为月份是从0开始的
    date.setDate(parseInt(day))
  },
  setTime: function (str, date) {
    if (typeof (date) != typeof (Date)) {
      date = new Date(date)
    }
    let [hour, minute] = str.split(':')
    date.setHours(parseInt(hour))
    date.setMinutes(parseInt(minute))
  },
  stringToByte: function (str) {
    var bytes = new Array();
    var len, c;
    len = str.length;
    for (var i = 0; i < len; i++) {
      c = str.charCodeAt(i);
      if (c >= 0x010000 && c <= 0x10FFFF) {
        bytes.push(((c >> 18) & 0x07) | 0xF0);
        bytes.push(((c >> 12) & 0x3F) | 0x80);
        bytes.push(((c >> 6) & 0x3F) | 0x80);
        bytes.push((c & 0x3F) | 0x80);
      } else if (c >= 0x000800 && c <= 0x00FFFF) {
        bytes.push(((c >> 12) & 0x0F) | 0xE0);
        bytes.push(((c >> 6) & 0x3F) | 0x80);
        bytes.push((c & 0x3F) | 0x80);
      } else if (c >= 0x000080 && c <= 0x0007FF) {
        bytes.push(((c >> 6) & 0x1F) | 0xC0);
        bytes.push((c & 0x3F) | 0x80);
      } else {
        bytes.push(c & 0xFF);
      }
    }
    return bytes;
  },
  byteToString: function (arr) {
    if (typeof arr === 'string') {
      return arr;
    }
    var str = '',
      _arr = arr;
    for (var i = 0; i < _arr.length; i++) {
      var one = _arr[i].toString(2),
        v = one.match(/^1+?(?=0)/);
      if (v && one.length == 8) {
        var bytesLength = v[0].length;
        var store = _arr[i].toString(2).slice(7 - bytesLength);
        for (var st = 1; st < bytesLength; st++) {
          store += _arr[st + i].toString(2).slice(2);
        }
        str += String.fromCharCode(parseInt(store, 2));
        i += bytesLength - 1;
      } else {
        str += String.fromCharCode(_arr[i]);
      }
    }
    return str;
  },
  // 获取用户信息
  getUser: function () {
    return wx.getStorageSync('user')
  },
  getUrl: getCurrentPageUrl,
  getUrlArgs: getCurrentPageUrlWithArgs,
  group: function(array, subGroupLength) {
    let index = 0;
    let newArray = [];
    while(index < array.length) {
        newArray.push(array.slice(index, index += subGroupLength));
    }
    return newArray;
  },
  /* 复制文本内容 */
  copyText: function (text) {
    wx.setClipboardData({
      data: text,
      success: function (res) {
        wx.getClipboardData({
          success: function (res) {
            wx.showToast({
              title: '复制成功'
            })
          }
        })
      }
    })
  },
}

/*获取当前页url*/
function getCurrentPageUrl() {
  var pages = getCurrentPages()
  var currentPage = pages[pages.length - 1]
  var url = currentPage.route
  return url
}

/*获取当前页带参数的url*/
function getCurrentPageUrlWithArgs() {
  var pages = getCurrentPages() //获取加载的页面
  var currentPage = pages[pages.length - 1] //获取当前页面的对象
  var url = currentPage.route //当前页面url
  var options = currentPage.options //如果要获取url中所带的参数可以查看options

  //拼接url的参数
  var urlWithArgs = url + '&'
  // var urlWithArgs = url + '?'
  for (var key in options) {
    var value = options[key]
    urlWithArgs += key + '=' + value + '&'
  }
  urlWithArgs = urlWithArgs.substring(0, urlWithArgs.length - 1)

  return urlWithArgs
}

function getApi(url, params) {
  return new Promise((res, rej) => {
    wx.request({
      url: url,
      data: Object.assign({}, params),
      header: {
        'Content-Type': 'application/json'
      },
      success: res,
      fail: rej
    })
  })
}

const formatNumber = n => {
  n = n.toString()
  return n[1] ? n : '0' + n
}