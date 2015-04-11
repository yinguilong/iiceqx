using MongoDB.Bson;
/************************************************************************************
 * Copyright (c) 2015Microsoft All Rights Reserved.
 * CLR版本： 4.0.30319.18444
 *命名空间：iiceqx.Model.Users
 *文件名：  ArticleTheme
 *版本号：  V1.0.0.0
 *创建人：  yinguilong
 *创建时间：3/28/2015 3:27:50 PM
 *描述：
 *
 *=====================================================================
 *修改标记
 *修改时间：3/28/2015 3:27:50 PM
 *修改人：  yinguilong
 *版本号： V1.0.0.0
 *描述：
 *
/************************************************************************************/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace iiceqx.Model
{
    [Serializable]
    public class ArticleTheme
    {
        
        public ObjectId _id;//BsonType.ObjectId 这个对应了 MongoDB.Bson.ObjectId 
        /// <summary>
        /// 文章主题ID
        /// </summary>
        public int ArticleThemeId { get; set; }
        /// <summary>
        /// 文章主题名称
        /// </summary>
        public string ArticleThemeName { get; set; }
        /// <summary>
        /// 文章主题状态
        /// -2  被禁止
        /// -1  太监了
        /// 0   新添加
        /// 1   更新中
        /// 2   完结
        /// </summary>
        public int ArticleThemeStatus { get; set; }
        /// <summary>
        /// 当前主题文章数
        /// </summary>
        public int ArticleCount { get; set; }
        /// <summary>
        /// 文章总数（预估）
        /// </summary>
        public int ArticleTotalCount { get; set; }
        /// <summary>
        /// 作者ID
        /// </summary>
        public int AuthorId { get; set; }
        /// <summary>
        /// 作者名称
        /// </summary>
        public string AuthorName { get; set; }
    }
}
