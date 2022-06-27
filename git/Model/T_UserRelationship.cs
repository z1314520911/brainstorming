using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace Model {
    
    
    /// <summary>
    /// ��Ա��ϵ
    /// </summary>
    [Serializable()]
    public class T_UserRelationship {
        
        private int _id;
        
        private string _openid;
        
        private string _s1;
        
        private string _s2;
        
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
        /// ΢��OpenId
        /// </summary>
        public string OpenId {
            get {
                return this._openid;
            }
            set {
                this._openid = value;
            }
        }
        
        /// <summary>
        /// ��һ��OpenId
        /// </summary>
        public string S1 {
            get {
                return this._s1;
            }
            set {
                this._s1 = value;
            }
        }
        
        /// <summary>
        /// �϶���OpenId
        /// </summary>
        public string S2 {
            get {
                return this._s2;
            }
            set {
                this._s2 = value;
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
