using MongoDB.Bson;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace iiceqx.Model
{
    [Serializable]
    public class Article
    {
        public ObjectId _id;//BsonType.ObjectId 这个对应了 MongoDB.Bson.ObjectId 
        /// <summary>
        /// 文章Id
        /// </summary>
        public int ArticleId { get; set; }
        /// <summary>
        /// 作者ID
        /// </summary>
        public int AuthorId { get; set; }
        /// <summary>
        /// 作者名称
        /// </summary>
        public string AuthorName { get; set; }
        /// <summary>
        /// 文章内容
        /// </summary>
        public string Content { get; set; }
        /// <summary>
        /// 创建时间
        /// </summary>
        public DateTime CreateDate { get; set; }
        /// <summary>
        /// 文章类型
        /// </summary>
        public int ArticleType { get; set; }
        /// <summary>
        /// 文章标题
        /// </summary>
        public string ArticleTitle { get; set; }
        /// <summary>
        /// 读者水平
        /// </summary>
        public int ReaderLevel { get; set; }
        /// <summary>
        /// 文章主标题
        /// </summary>
        public string ArticleMasterTitle { get; set; }
        /// <summary>
        /// 文章来源
        /// </summary>
        public string ArticleSouce { get; set; }
        /// <summary>
        /// 文章简介
        /// </summary>
        public string Summary { get; set; }
        /// <summary>
        /// 文章在主题里面的排序
        /// </summary>
        public int ArticleIndex { get; set; }
        /// <summary>
        /// 文章主题ID
        /// </summary>
        public int ArticleThemeId { get; set; }
    }
}
