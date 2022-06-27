using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace Model {
    
    
    /// <summary>
    /// 栏目内容管理
    /// </summary>
    [Serializable()]
    public class About {
        
        private int _id;
        
        private string _name;
        
        private string _tag;
        
        private string _title;
        
        private string _keywords;
        
        private string _descrip;
        
        private string _parentname;
        
        private string _parentclass;
        
        private string _content;
        
        private int _click;
        
        private int _sort;
        
        private string _languages;
        
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
        /// 栏目名称
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
        /// 标签
        /// </summary>
        public string Tag {
            get {
                return this._tag;
            }
            set {
                this._tag = value;
            }
        }
        
        /// <summary>
        /// 标题
        /// </summary>
        public string Title {
            get {
                return this._title;
            }
            set {
                this._title = value;
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
        /// 网页描述
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
        /// 类别
        /// </summary>
        public string ParentName {
            get {
                return this._parentname;
            }
            set {
                this._parentname = value;
            }
        }
        
        /// <summary>
        /// 大类
        /// </summary>
        public string ParentClass {
            get {
                return this._parentclass;
            }
            set {
                this._parentclass = value;
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
        /// 语言
        /// </summary>
        public string Languages {
            get {
                return this._languages;
            }
            set {
                this._languages = value;
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
