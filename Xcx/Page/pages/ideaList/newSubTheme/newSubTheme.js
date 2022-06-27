// // miniprogram/pages/ideaList/newSubTheme/newSubTheme.js
// const db = wx.cloud.database();

// Page({

//   /* 页面的初始数据 */
//   data: {
//     firstTheme: '', //一级主题名称
//     firstThemeId: '', //一级主题id
//     secondTheme: '', //二级主题名称
//     remark: '',
//     indexThemeId: '',
//     gradation: '',
//     themeIndex:''
//   },

//   /* 生命周期函数--监听页面加载 */
//   onLoad: function(option) {
//     console.log('12313', option);
//     this.setData({
//       firstTheme: option.firstTheme,
//       firstThemeId: option.firstThemeId,
//       secondTheme: option.secondTheme,
//       indexThemeId: option.indexThemeId,
//       themeIndex: option.themeIndex,
//       gradation: option.gradation + '/' + option.secondTheme
//     })
//     // console.log(typeof(this.data.themeIndex))
//   },

//   /* 获取用户输入值 */
//   getRemark: function(e) {
//     var that = this;
//     that.setData({
//       remark: e.detail.value
//     })
//   },

//   /* 页面跳转_完成新主题的创建后跳转到pages/themeDetails/themeDetails */
//   /* 本页url：pages/ideaList/newSubTheme/newSubTheme */
//   onFinished: function() {
//     var that = this;
//     var firstTheme = that.data.firstTheme;
//     var firstThemeId = that.data.firstThemeId;
//     var secondTheme = that.data.secondTheme;
//     var remark = that.data.remark;
//     var themeIndex = parseInt(that.data.themeIndex) + 1
//     if (themeIndex == 2) {
//       db.collection('secondLevelThemeDetails').add({
//         data: {
//           themeIndex: themeIndex,
//           ideas: {},
//           remark: remark,
//           themeId: firstThemeId,
//           theme: secondTheme,
//           themeBefore: firstTheme,
//           gradation: this.data.gradation
//         }
//       })
//     } else if (themeIndex == 3) {
// console.log('save')
//       db.collection('thirdLevelThemeDetails').add({
//         data: {
//           themeIndex: themeIndex,
//           ideas: {},
//           remark: remark,
//           themeId: firstThemeId,
//           theme: secondTheme,
//           themeBefore: firstTheme,
//           gradation: this.data.gradation
//         }
//       })

//     }


//     let pages = getCurrentPages(); //获取当前页面js里面的pages里的所有信息。
//     let prevPage = pages[pages.length - 4];
//     //prevPage 是获取上一个页面的js里面的pages的所有信息。 -2 是上一个页面，-3是上上个页面以此类推。
//     prevPage.setData({ // 将我们想要传递的参数在这里直接setData。上个页面就会执行这里的操作。
//       indexThemeId: this.data.indexThemeId,
//     })
//     //上一个页面内执行setData操作，将我们想要的信息保存住。当我们返回去的时候，页面已经处理完毕。

//     //最后就是返回上一个页面。
//     wx.navigateBack({
//       delta: 3
//     })
//   },

// })