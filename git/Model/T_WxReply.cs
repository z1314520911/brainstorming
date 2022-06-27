using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace Model {
    
    
    /// <summary>
    /// 微信自动回复
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
        /// 标题
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
        /// 关键字
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
        /// 回复内容
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
        /// 链接
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
        /// 图片
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
        /// 类别
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
        /// 回复类型
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
