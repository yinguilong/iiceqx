using iiceqx.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using iiceqx.Model;
using MongoDB.Driver;
using MongoDB.Driver.Builders;
using MongoDB.Driver.Linq;
using MongoDB.Bson;
namespace iiceqx.Provider
{
    public class NewsProvider
    {
       
        /// <summary>
        /// 根据新闻类型查询出一定数量的新闻显示在首页
        /// </summary>
        /// <param name="userFavoriteList"></param>
        /// <param name="top"></param>
        /// <returns></returns>
        public List<News> GetIndexNewsForUser(List<UserFavorite> userFavoriteList, int top = 6)
        {

            if (userFavoriteList != null && userFavoriteList.Any())
            {
                var list = new List<MongoDB.Bson.BsonInt32>();
                userFavoriteList.ForEach(x =>
                {
                    list.Add(new BsonInt32(x.FavoriteType));
                });
                var queryNews = Query.In("NewsType", list);
                return MongoDBHelper.GetAll<News>("News", queryNews);
            }
            return null;
        }
        public News GetNewsByNewsId(int newsId)
        {
            var query = Query.EQ("NewsId", newsId);
            return MongoDBHelper.GetOne<News>("News", query);

        }
        public List<News> GetIndexNewsForVisitor(int top = 6)
        {
            return GetNewsByPage(1, top);
        }
        public List<News> GetNewsByPage(int page, int pageSize)
        {
            var sortQuery = new SortByDocument();
            sortQuery.Add("CreateTime", -1);
            return MongoDBHelper.GetAll<News>("News", null, new PagerInfo() { Page = page, PageSize = pageSize }, sortQuery);
        }
        /// <summary>
        /// 根据用户Id查询出用户关注的新闻类型
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        public List<UserFavorite> GetUserFavoriteList(int userId)
        {
            var query = Query.EQ("UserId", userId);
            return MongoDBHelper.GetAll<UserFavorite>("UserFavorite", query);
        }
    }
}
