using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace Model {
    
    
    /// <summary>
    /// ΢���Զ��ظ�
    /// </summary>
    [Serializable()]
    public class T_WxReply {
        
        private int _id;
        
        private string _name;
        
        private string _keywords;
        
        private string _content;
        
        private string _url;
        
        private string _pic;
        
        private int _parentid;
        
        private int _parenttype;
        
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
        /// �ؼ���
        /// </summary>
        public string Keywords {
            get {
                return this._keywords;
            }
            set {
                this._keywords = value;
            }
        }
        
        /// <summary>
        /// �ظ�����
        /// </summary>
        public string Content {
            get {
                return this._content;
            }
            set {
                this._content = value;
            }
        }
        
        /// <summary>
        /// ����
        /// </summary>
        public string Url {
            get {
                return this._url;
            }
            set {
                this._url = value;
            }
        }
        
        /// <summary>
        /// ͼƬ
        /// </summary>
        public string Pic {
            get {
                return this._pic;
            }
            set {
                this._pic = value;
            }
        }
        
        /// <summary>
        /// ���
        /// </summary>
        public int ParentId {
            get {
                return this._parentid;
            }
            set {
                this._parentid = value;
            }
        }
        
        /// <summary>
        /// �ظ�����
        /// </summary>
        public int ParentType {
            get {
                return this._parenttype;
            }
            set {
                this._parenttype = value;
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
