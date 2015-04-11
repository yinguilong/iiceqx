using MongoDB.Bson;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace iiceqx.Model
{
    [Serializable]
    public class News
    {
        public ObjectId _id;//BsonType.ObjectId 这个对应了 MongoDB.Bson.ObjectId 
        public int NewsId { get; set; }
        public int NewsType { get; set; }
        public string Title { get; set; }
        public string SouceUrl { get; set; }
        public string Operator { get; set; }
        public string Content { get; set; }
        /// <summary>
        /// 简介
        /// </summary>
        public string Summary { get; set; }
        public DateTime CreateTime { get; set; }
    }
}
