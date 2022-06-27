using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace Model {
    
    
    /// <summary>
    /// 网站管理员
    /// </summary>
    [Serializable()]
    public class AdminUser {
        
        private int _id;
        
        private string _username;
        
        private string _password;
        
        private string _per;
        
        private string _name;
        
        private bool _status;
        
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
        /// 用户名
        /// </summary>
        public string UserName {
            get {
                return this._username;
            }
            set {
                this._username = value;
            }
        }
        
        /// <summary>
        /// 密码
        /// </summary>
        public string PassWord {
            get {
                return this._password;
            }
            set {
                this._password = value;
            }
        }
        
        /// <summary>
        /// 权限
        /// </summary>
        public string Per {
            get {
                return this._per;
            }
            set {
                this._per = value;
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
        /// 审核
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
