using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model
{
    [Serializable]
    public class AdminLogin
    {
        /// <summary>
        /// ID
        /// </summary>
        public int Id { get; set; }
        /// <summary>
        /// 昵称
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// 所属角色
        /// </summary>
        public string Role { get; set; }

        /// <summary>
        /// 所属角色Id
        /// </summary>
        public int RoleId { get; set; }

        /// <summary>
        /// 所属机构
        /// </summary>
        public int OrgId { get; set; }
        /// <summary>
        /// 下属机构
        /// </summary>
        public string SubOrg { get; set; }
        /// <summary>
        /// 所属权限组
        /// </summary>
        public string Per { get; set; }

        /// <summary>
        /// 上级代理
        /// </summary>
        public int UserId { get; set; }

        /// <summary>
        /// 上级代理列表
        /// </summary>
        public string UserIdList { get; set; }
    }
}
