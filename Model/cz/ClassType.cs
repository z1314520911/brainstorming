using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace Model {
    
    
    /// <summary>
    /// 栏目分类管理
    /// </summary>
    [Serializable()]
    public class ClassType {
        
        private int _id;
        
        private string _name;
        
        private string _tag;
        
        private string _parentname;
        
        private string _url;
        
        private string _master;
        
        private string _belongs;
        
        private int _sort;
        
        private string _languages;
        
        private int _addtime;
        
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
        /// 分类名称
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
        /// 栏目名称
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
        /// 连接位置
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
        /// 母板页
        /// </summary>
        public string Master {
            get {
                return this._master;
            }
            set {
                this._master = value;
            }
        }
        
        /// <summary>
        /// 类型
        /// </summary>
        public string Belongs {
            get {
                return this._belongs;
            }
            set {
                this._belongs = value;
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
        public int AddTime {
            get {
                return this._addtime;
            }
            set {
                this._addtime = value;
            }
        }
    }
}
