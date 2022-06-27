using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace Model {
    
    
    /// <summary>
    /// 投票观点
    /// </summary>
    [Serializable()]
    public class T_VoteView {
        
        private int _id;
        
        private int _voteid;
        
        private int _userid;
        
        private int _voteuserid;
        
        private string _idea;
        
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
        /// 参与Id
        /// </summary>
        public int VoteUserId {
            get {
                return this._voteuserid;
            }
            set {
                this._voteuserid = value;
            }
        }
        
        /// <summary>
        /// 主意
        /// </summary>
        public string Idea {
            get {
                return this._idea;
            }
            set {
                this._idea = value;
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
