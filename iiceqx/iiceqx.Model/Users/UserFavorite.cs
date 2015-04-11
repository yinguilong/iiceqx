using MongoDB.Bson;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace iiceqx.Model
{
    [Serializable]
    public class UserFavorite
    {
        public ObjectId _id;//BsonType.ObjectId 这个对应了 MongoDB.Bson.ObjectId 
        public int UserId { get; set; }
        public int FavoriteType { get; set; }
    }
}
