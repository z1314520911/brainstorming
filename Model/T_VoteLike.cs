using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace Model {
    
    
    /// <summary>
    /// ͶƱ����
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
        /// ����Id
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
        /// �۵�Id
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
        /// �û�Id
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
