using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace Model {
    
    
    /// <summary>
    /// 账户信息
    /// </summary>
    [Serializable()]
    public class T_UserAccount {
        
        private int _id;
        
        private int _userid;
        
        private decimal _money;
        
        private decimal _commissiontotal;
        
        private decimal _commissionwithdraw;
        
        private decimal _commission;
        
        private System.DateTime _createtime;
        
        private System.DateTime _updatetime;
        
        private bool _isdeleted;
        
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
        /// 余额
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
        /// 累计佣金
        /// </summary>
        public decimal CommissionTotal {
            get {
                return this._commissiontotal;
            }
            set {
                this._commissiontotal = value;
            }
        }
        
        /// <summary>
        /// 已提现佣金
        /// </summary>
        public decimal CommissionWithdraw {
            get {
                return this._commissionwithdraw;
            }
            set {
                this._commissionwithdraw = value;
            }
        }
        
        /// <summary>
        /// 可提现佣金
        /// </summary>
        public decimal Commission {
            get {
                return this._commission;
            }
            set {
                this._commission = value;
            }
        }
        
        /// <summary>
        /// 添加时间
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
        /// 最后更新时间
        /// </summary>
        public System.DateTime UpdateTime {
            get {
                return this._updatetime;
            }
            set {
                this._updatetime = value;
            }
        }
        
        /// <summary>
        /// 是否删除
        /// </summary>
        public bool IsDeleted {
            get {
                return this._isdeleted;
            }
            set {
                this._isdeleted = value;
            }
        }
    }
}
