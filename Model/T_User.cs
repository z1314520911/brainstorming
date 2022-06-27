using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace Model {
    
    
    /// <summary>
    /// 账号管理
    /// </summary>
    [Serializable()]
    public class T_User {
        
        private int _id;
        
        private int _userid;
        
        private int _storeid;
        
        private string _name;
        
        private string _loginid;
        
        private string _password;
        
        private string _unionid;
        
        private string _openid;
        
        private string _wxopenid;
        
        private string _guid;
        
        private string _nickname;
        
        private string _phone;
        
        private string _email;
        
        private decimal _money;
        
        private bool _ispayuser;
        
        private string _qrcode;
        
        private int _levelid;
        
        private int _typeid;
        
        private string _avatar;
        
        private int _sex;
        
        private int _province;
        
        private int _city;
        
        private int _area;
        
        private string _address;
        
        private string _carcard;
        
        private string _intro;
        
        private System.DateTime _thetime;
        
        private string _theip;
        
        private System.DateTime _lasttime;
        
        private string _lastip;
        
        private int _status;
        
        private bool _subscribe;
        
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
        /// 代理ID
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
        /// 登录账号
        /// </summary>
        public string LoginId {
            get {
                return this._loginid;
            }
            set {
                this._loginid = value;
            }
        }
        
        /// <summary>
        /// 密码
        /// </summary>
        public string Password {
            get {
                return this._password;
            }
            set {
                this._password = value;
            }
        }
        
        /// <summary>
        /// UnionID
        /// </summary>
        public string UnionID {
            get {
                return this._unionid;
            }
            set {
                this._unionid = value;
            }
        }
        
        /// <summary>
        /// OpenId
        /// </summary>
        public string OpenId {
            get {
                return this._openid;
            }
            set {
                this._openid = value;
            }
        }
        
        /// <summary>
        /// 公众号OpenId
        /// </summary>
        public string WxOpenId {
            get {
                return this._wxopenid;
            }
            set {
                this._wxopenid = value;
            }
        }
        
        /// <summary>
        /// Guid
        /// </summary>
        public string Guid {
            get {
                return this._guid;
            }
            set {
                this._guid = value;
            }
        }
        
        /// <summary>
        /// 微信昵称
        /// </summary>
        public string NickName {
            get {
                return this._nickname;
            }
            set {
                this._nickname = value;
            }
        }
        
        /// <summary>
        /// 联系方式
        /// </summary>
        public string Phone {
            get {
                return this._phone;
            }
            set {
                this._phone = value;
            }
        }
        
        /// <summary>
        /// 邮箱
        /// </summary>
        public string EMail {
            get {
                return this._email;
            }
            set {
                this._email = value;
            }
        }
        
        /// <summary>
        /// 金额
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
        /// 付款用户(新用户)
        /// </summary>
        public bool IsPayUser {
            get {
                return this._ispayuser;
            }
            set {
                this._ispayuser = value;
            }
        }
        
        /// <summary>
        /// 二维码
        /// </summary>
        public string QRCode {
            get {
                return this._qrcode;
            }
            set {
                this._qrcode = value;
            }
        }
        
        /// <summary>
        /// 会员级别
        /// </summary>
        public int LevelId {
            get {
                return this._levelid;
            }
            set {
                this._levelid = value;
            }
        }
        
        /// <summary>
        /// 会员类别
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
        /// 微信头像
        /// </summary>
        public string Avatar {
            get {
                return this._avatar;
            }
            set {
                this._avatar = value;
            }
        }
        
        /// <summary>
        /// 性别
        /// </summary>
        public int Sex {
            get {
                return this._sex;
            }
            set {
                this._sex = value;
            }
        }
        
        /// <summary>
        /// 省ID
        /// </summary>
        public int Province {
            get {
                return this._province;
            }
            set {
                this._province = value;
            }
        }
        
        /// <summary>
        /// 市ID
        /// </summary>
        public int City {
            get {
                return this._city;
            }
            set {
                this._city = value;
            }
        }
        
        /// <summary>
        /// 县(区)ID
        /// </summary>
        public int Area {
            get {
                return this._area;
            }
            set {
                this._area = value;
            }
        }
        
        /// <summary>
        /// 地址
        /// </summary>
        public string Address {
            get {
                return this._address;
            }
            set {
                this._address = value;
            }
        }
        
        /// <summary>
        /// 车牌号码
        /// </summary>
        public string CarCard {
            get {
                return this._carcard;
            }
            set {
                this._carcard = value;
            }
        }
        
        /// <summary>
        /// 个人简介
        /// </summary>
        public string Intro {
            get {
                return this._intro;
            }
            set {
                this._intro = value;
            }
        }
        
        /// <summary>
        /// 本次登陆时间
        /// </summary>
        public System.DateTime TheTime {
            get {
                return this._thetime;
            }
            set {
                this._thetime = value;
            }
        }
        
        /// <summary>
        /// 本次登陆IP地址
        /// </summary>
        public string TheIp {
            get {
                return this._theip;
            }
            set {
                this._theip = value;
            }
        }
        
        /// <summary>
        /// 上次登陆时间
        /// </summary>
        public System.DateTime LastTime {
            get {
                return this._lasttime;
            }
            set {
                this._lasttime = value;
            }
        }
        
        /// <summary>
        /// 上次登陆IP地址
        /// </summary>
        public string LastIp {
            get {
                return this._lastip;
            }
            set {
                this._lastip = value;
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
        /// 是否客服
        /// </summary>
        public bool Subscribe {
            get {
                return this._subscribe;
            }
            set {
                this._subscribe = value;
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
