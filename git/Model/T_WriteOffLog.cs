using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace Model {
    
    
    /// <summary>
    /// ������¼
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
        /// �Żݾ�ID
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
        /// �û���ȡID
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
        /// ״̬
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
        /// ���ʱ��
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
