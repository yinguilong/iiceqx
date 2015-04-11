using MongoDB.Bson;
/************************************************************************************
 * Copyright (c) 2015Microsoft All Rights Reserved.
 * CLR版本： 4.0.30319.18444
 *命名空间：iiceqx.Model.Users
 *文件名：  Author
 *版本号：  V1.0.0.0
 *创建人：  yinguilong
 *创建时间：3/28/2015 5:30:04 PM
 *描述：
 *
 *=====================================================================
 *修改标记
 *修改时间：3/28/2015 5:30:04 PM
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
    public class Author
    {
        public ObjectId _id;//BsonType.ObjectId 这个对应了 MongoDB.Bson.ObjectId 
        public int AuthorId { get; set; }
        public string AuthorName { get; set; }
        public int Cash { get; set; }
        public DateTime RegisterDate { get; set; }
        public int Staus { get; set; }
    }
}
