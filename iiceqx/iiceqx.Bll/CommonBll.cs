using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using iiceqx.IBll;
using iiceqx.Provider;
namespace iiceqx.Bll
{
    public class CommonBll : ICommonBll
    {
        private CommonProvider commonProvider = new CommonProvider();
        public bool LoginCheck(string loginAccount, string pwd)
        {
            var user = commonProvider.GetUsers(loginAccount);
            if (user != null && user.PassWord.Equals(pwd))
            {
                return true;
            }
            return false;
        }
    }
}
