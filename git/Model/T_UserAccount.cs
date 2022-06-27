using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace Model {
    
    
    /// <summary>
    /// �˻���Ϣ
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
        /// �û����
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
        /// ���
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
        /// �ۼ�Ӷ��
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
        /// ������Ӷ��
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
        /// ������Ӷ��
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
        /// ���ʱ��
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
        /// ������ʱ��
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
        /// �Ƿ�ɾ��
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
