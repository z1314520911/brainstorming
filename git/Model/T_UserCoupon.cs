using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace Model {
    
    
    /// <summary>
    /// �û���ȡ�Ż�ȯ��
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
        /// �Ż�ȯ���
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
        /// ��������
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
        /// ��Ʒ����
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
        /// ����
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
        /// �������磺��50�ſ�ʹ�ñ��Ż�ȯ
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
        /// ��ֵ ��С��0��С��Ϊ���۶��
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
        /// ״̬
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
        /// �Żݿ�ʼ����
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
        /// �Żݽ�������
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
        /// ��ȡʱ��
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
        /// ���б��
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
        /// ��֤�û����
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
        /// ��֤ʱ��
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
