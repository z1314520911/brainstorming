// const db = wx.cloud.database()
// const app = getApp();

// Page({

//   /**
//    * 页面的初始数据
//    */
//   data: {
//     theme: '',
//     id: '',
//     index: '',//灵感的index
//     themeId: '',
//     thisThemeId:'',
//     themeIndex: '',//主题的层级
//     ideaData: [],
//     idea: '',
//     isBtnDisabled: false,
//     gradation:'',//层级关系
//     indexThemeId:'',
//     avatarUrl: ''    //用户头像url
//   },

//   /**
//    * 生命周期函数--监听页面加载
//    */
//   onLoad: function(options) {
//     console.log(options)
//     this.setData({
//       theme: options.theme,
//       // idea:options.idea,
//       avatarUrl: getApp().globalData.avatarUrl,
//       id: options.id,
//       index: options.idx,
//       themeIndex: options.themeIndex,
//       gradation: options.gradation,
//       indexThemeId: options.indexThemeId
//     })
//     console.log(options)
//     if (this.data.themeIndex == 1) {
//       this.onQuery('themeDetailsData');
//     }
//     if (this.data.themeIndex == 2) {
//       this.onQuery('secondLevelThemeDetails');
//     }
//     if (this.data.themeIndex == 3) {
//       this.onQuery('thirdLevelThemeDetails');
//     }
//     // this.onQuery();
//   },

//   onQuery: function(tableName) {
//     var that = this;
//     var index = that.data.index;
//     // const db = wx.cloud.database()
//     wx.showNavigationBarLoading();
//     db.collection(tableName).where({
//       _openid: that.data.openid,
//       _id: that.data.id
//     }).get({
//       success: res => {
//         that.setData({
//           ideaData: res.data[0].ideas,
//           themeId: res.data[0].themeId,
//           idea: res.data[0].ideas[index].idea,
//           thisThemeId: res.data[0]._id,
//           // gradation: that.data.gradation + '/' + res.data[0].ideas[index].idea
//         })
//         console.log(that.data.ideaData);
//       },
//       fail: err => {
//         console.error('ideaDetails查询失败：', err)
//       }
//     })

//   },

//   onSupportBtn: function(e) {
//     this.changeSupportNum(true);
//   },
//   onOpposeBtn: function(e) {
//     this.changeSupportNum(false);
//   },

//   changeSupportNum: function(isPlus) {
//     var that = this;
//     var index = that.data.index;
//     var ideaData = that.data.ideaData;
//     if (isPlus) {
//       ideaData[index].supportNum++;
//     } else {
//       ideaData[index].supportNum--;
//     }
//     wx.showToast({
//       title: '表态成功',
//     })
//     ideaData.sort(that.compare("supportNum"));
//     that.setData({
//       ideaData: ideaData
//     })
//     console.log(that.data.ideaData)
//   },

//   /* 页面跳转_新建子主题 */
//   creatSubTheme: function(e) {
//     //跳转传递大主题+路径+主题
//     var that=this;
//     var firstTheme;
//     var secondTheme;
//     var firstThemeId;
//     firstTheme=that.data.theme;   //一级主题
//     secondTheme = that.data.idea; //二级主题
//     firstThemeId = that.data.thisThemeId;  //二级主题的id
//     wx.navigateTo({
//       url: '../newSubTheme/newSubTheme?firstTheme='+firstTheme+'&secondTheme=' + secondTheme + '&firstThemeId=' + firstThemeId + '&themeIndex=' + this.data.themeIndex + '&indexThemeId=' + this.data.indexThemeId+'&gradation='+this.data.gradation,
//       success: function(res) {},
//       fail: function(res) {},
//       complete: function(res) {},
//     })
//   },

//   compare: function(property) {
//     return function(a, b) {
//       var value1 = a[property];
//       var value2 = b[property];
//       return value2 - value1;
//     }
//   },

//   /**
//    * 生命周期函数--监听页面初次渲染完成
//    */
//   onReady: function() {

//   },

//   /**
//    * 生命周期函数--监听页面显示
//    */
//   onShow: function() {

//   },

//   /**
//    * 生命周期函数--监听页面隐藏
//    */
//   onHide: function() {

//   },

//   /**
//    * 生命周期函数--监听页面卸载
//    */
//   onUnload: function() {

//   },

//   /**
//    * 页面相关事件处理函数--监听用户下拉动作
//    */
//   onPullDownRefresh: function() {

//   },

//   /**
//    * 页面上拉触底事件的处理函数
//    */
//   onReachBottom: function() {

//   },

//   /**
//    * 用户点击右上角分享
//    */
//   onShareAppMessage: function() {

//   }
// })