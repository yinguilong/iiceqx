using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace iiceqx.Model
{
    [Serializable]
    public class CurrentOperator
    {
        /// <summary>
        /// 登陆级别
        /// </summary>
        public int SystemLevel { get; set; }
        /// <summary>
        /// 登录账号Id
        /// </summary>
        public int UserId { get; set; }
        /// <summary>
        /// 客户端Ip
        /// </summary>
        public string ClientIP { get; set; }
      
    }
}
