using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace Model {
    
    
    /// <summary>
    /// 用户领取优惠券表
    /// </summary>
    [Serializable()]
    public class T_UserCoupon {
        
        private int _id;
        
        private int _userid;
        
        private int _couponid;
        
        private string _storeidlist;
        
        private int _typeid;
        
        private string _name;
        
        private int _usemoney;
        
        private decimal _money;
        
        private bool _status;
        
        private System.DateTime _startdate;
        
        private System.DateTime _enddate;
        
        private System.DateTime _createtime;
        
        private int _storeid;
        
        private string _verid;
        
        private System.DateTime _vertime;
        
        /// <summary>
        /// ID
        /// </summary>
        public int Id {
            get {
                return this._id;
            }
            set {
                this._id = value;
            }
        }
        
        /// <summary>
        /// 用户编号
        /// </summary>
        public int UserId {
            get {
                return this._userid;
            }
            set {
                this._userid = value;
            }
        }
        
        /// <summary>
        /// 优惠券编号
        /// </summary>
        public int CouponId {
            get {
                return this._couponid;
            }
            set {
                this._couponid = value;
            }
        }
        
        /// <summary>
        /// 所属车行
        /// </summary>
        public string StoreIdList {
            get {
                return this._storeidlist;
            }
            set {
                this._storeidlist = value;
            }
        }
        
        /// <summary>
        /// 商品类型
        /// </summary>
        public int TypeId {
            get {
                return this._typeid;
            }
            set {
                this._typeid = value;
            }
        }
        
        /// <summary>
        /// 名称
        /// </summary>
        public string Name {
            get {
                return this._name;
            }
            set {
                this._name = value;
            }
        }
        
        /// <summary>
        /// 满减金额，如：满50才可使用本优惠券
        /// </summary>
        public int UseMoney {
            get {
                return this._usemoney;
            }
            set {
                this._usemoney = value;
            }
        }
        
        /// <summary>
        /// 价值 ，小于0的小数为打折额度
        /// </summary>
        public decimal Money {
            get {
                return this._money;
            }
            set {
                this._money = value;
            }
        }
        
        /// <summary>
        /// 状态
        /// </summary>
        public bool Status {
            get {
                return this._status;
            }
            set {
                this._status = value;
            }
        }
        
        /// <summary>
        /// 优惠开始日期
        /// </summary>
        public System.DateTime StartDate {
            get {
                return this._startdate;
            }
            set {
                this._startdate = value;
            }
        }
        
        /// <summary>
        /// 优惠结束日期
        /// </summary>
        public System.DateTime EndDate {
            get {
                return this._enddate;
            }
            set {
                this._enddate = value;
            }
        }
        
        /// <summary>
        /// 领取时间
        /// </summary>
        public System.DateTime CreateTime {
            get {
                return this._createtime;
            }
            set {
                this._createtime = value;
            }
        }
        
        /// <summary>
        /// 车行编号
        /// </summary>
        public int StoreId {
            get {
                return this._storeid;
            }
            set {
                this._storeid = value;
            }
        }
        
        /// <summary>
        /// 验证用户编号
        /// </summary>
        public string VerId {
            get {
                return this._verid;
            }
            set {
                this._verid = value;
            }
        }
        
        /// <summary>
        /// 验证时间
        /// </summary>
        public System.DateTime VerTime {
            get {
                return this._vertime;
            }
            set {
                this._vertime = value;
            }
        }
    }
}
