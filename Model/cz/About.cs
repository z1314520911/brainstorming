using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace Model {
    
    
    /// <summary>
    /// ��Ŀ���ݹ���
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
        /// ��Ŀ����
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
        /// ��ǩ
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
        /// ����
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
        /// ��ҳ����
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
        /// ���
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
        /// ����
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
        /// ����
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
        /// �����
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
        /// ����
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
        /// ����
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
