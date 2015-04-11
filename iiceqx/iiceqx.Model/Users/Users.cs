using MongoDB.Bson;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace iiceqx.Model
{
    [Serializable]
    public class Users
    {
        public ObjectId _id;//BsonType.ObjectId 这个对应了 MongoDB.Bson.ObjectId 
        public int UserId { get; set; }
        public string Name { get; set; }
        public string Sex { set; get; }
        public string UserLevel { set; get; }
        public string LoginAccount { set; get; }
        public string PassWord { get; set; }
    }
}
