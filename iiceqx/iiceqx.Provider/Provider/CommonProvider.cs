using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using iiceqx.Model;
using MongoDB.Driver;
using MongoDB.Driver.Builders;
using MongoDB.Driver.Linq;

namespace iiceqx.Provider
{
    public class CommonProvider
    {
        public Users GetUsers(string loginAccount)
        {
            var query = Query.EQ("LoginAccount", loginAccount);
            return MongoDBHelper.GetOne<Users>("Users", query);
        }
    }
}
