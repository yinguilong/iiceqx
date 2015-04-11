using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace iiceqx.IBll
{
    public interface ICommonBll
    {
        /// <summary>
        /// 用户登录
        /// </summary>
        /// <param name="loginAccount"></param>
        /// <param name="pwd"></param>
        /// <returns></returns>
        bool LoginCheck(string loginAccount, string pwd);
    }
}
