using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace Model {
    
    
    /// <summary>
    /// 核销记录
    /// </summary>
    [Serializable()]
    public class T_WriteOffLog {
        
        private int _id;
        
        private int _couponid;
        
        private int _usercouponid;
        
        private int _status;
        
        private System.DateTime _addtime;
        
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
        /// 优惠卷ID
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
        /// 用户领取ID
        /// </summary>
        public int UserCouponId {
            get {
                return this._usercouponid;
            }
            set {
                this._usercouponid = value;
            }
        }
        
        /// <summary>
        /// 状态
        /// </summary>
        public int Status {
            get {
                return this._status;
            }
            set {
                this._status = value;
            }
        }
        
        /// <summary>
        /// 添加时间
        /// </summary>
        public System.DateTime AddTime {
            get {
                return this._addtime;
            }
            set {
                this._addtime = value;
            }
        }
    }
}
