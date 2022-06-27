using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace Model {
    
    
    /// <summary>
    /// ��վ����Ա
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
        /// �û���
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
        /// ����
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
        /// Ȩ��
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
        /// ���
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
