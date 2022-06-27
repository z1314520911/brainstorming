using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace Model {
    
    
    /// <summary>
    /// 视频管理
    /// </summary>
    [Serializable()]
    public class T_Video {
        
        private int _id;
        
        private string _name;
        
        private string _pic;
        
        private string _files;
        
        private string _descrip;
        
        private string _content;
        
        private int _parentid;
        
        private int _click;
        
        private int _sort;
        
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
        /// 视频地址
        /// </summary>
        public string Files {
            get {
                return this._files;
            }
            set {
                this._files = value;
            }
        }
        
        /// <summary>
        /// 视频描述
        /// </summary>
        public string Descrip {
            get {
                return this._descrip;
            }
            set {
                this._descrip = value;
            }
        }
        
        /// <summary>
        /// 内容
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
        /// 点击率
        /// </summary>
        public int Click {
            get {
                return this._click;
            }
            set {
                this._click = value;
            }
        }
        
        /// <summary>
        /// 排序
        /// </summary>
        public int Sort {
            get {
                return this._sort;
            }
            set {
                this._sort = value;
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
