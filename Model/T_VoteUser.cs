using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace Model {
    
    
    /// <summary>
    /// 投票参与
    /// </summary>
    [Serializable()]
    public class T_VoteUser {
        
        private int _id;
        
        private int _voteid;
        
        private int _userid;
        
        private int _typeid;
        
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
        /// 主题Id
        /// </summary>
        public int VoteId {
            get {
                return this._voteid;
            }
            set {
                this._voteid = value;
            }
        }
        
        /// <summary>
        /// 用户Id
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
        /// 身份Id
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
