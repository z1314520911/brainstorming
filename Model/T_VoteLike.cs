using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace Model {
    
    
    /// <summary>
    /// 投票点赞
    /// </summary>
    [Serializable()]
    public class T_VoteLike {
        
        private int _id;
        
        private int _voteid;
        
        private int _viewid;
        
        private int _userid;
        
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
        /// 观点Id
        /// </summary>
        public int ViewId {
            get {
                return this._viewid;
            }
            set {
                this._viewid = value;
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
