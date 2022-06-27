using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace Model {
    
    
    /// <summary>
    /// 投票主题
    /// </summary>
    [Serializable()]
    public class T_Vote {
        
        private int _id;
        
        private string _name;
        
        private int _userid;
        
        private string _room;
        
        private bool _isstart;
        
        private int _status;
        
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
        /// 创建者
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
        /// 房间号
        /// </summary>
        public string Room {
            get {
                return this._room;
            }
            set {
                this._room = value;
            }
        }
        
        /// <summary>
        /// 是否开始
        /// </summary>
        public bool IsStart {
            get {
                return this._isstart;
            }
            set {
                this._isstart = value;
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
