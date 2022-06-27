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
        /// ����
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
        /// ������
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
        /// �����
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
        /// �Ƿ�ʼ
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
        /// ״̬
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
