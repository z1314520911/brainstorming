let app = getApp();
let gd = app.globalData;
const util = require('../../utils/util.js');
Page({
  data: {
    dataList: [],
    dataListPage: 1,
    pageHidden: true,
  },
  onLoad: function (op) {
    this.bindData(this);
  },
  bindData: function (that) {
    console.log(app.globalData.apiUrl);
    let data = {
      page: that.data.dataListPage,
    };
    util.scroll(that,'/Idea/RecordList', data);
  },

  scrolltolower: function (e) {
    this.bindData(this);
  },
})