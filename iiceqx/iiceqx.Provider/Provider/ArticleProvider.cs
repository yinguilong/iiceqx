using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using iiceqx.Model;
using MongoDB.Driver.Builders;
using MongoDB.Driver;
using MongoDB.Driver.Linq;
using MongoDB.Bson;

namespace iiceqx.Provider.Provider
{
    public class ArticleProvider
    {
        public Article GetArticleById(int id)
        {
            var query = Query.EQ("ArticleId", id);
            return MongoDBHelper.GetOne<Article>("Article", query);
        }
        public List<Article> GetArticleListIndex(string themeName, int? themeId, int pageInex, int pageSize)
        {
            IMongoQuery query;
            var listQuery = new List<IMongoQuery>();
            if (!string.IsNullOrEmpty(themeName))
            {
                listQuery.Add(Query.Matches("ArticleMasterTitle", themeName));
            }
            if (themeId.HasValue && themeId.Value >= 0)
            {
                listQuery.Add(Query.EQ("ArticleThemeId", themeId.Value));
            }
            if (listQuery.Any())
            {
                query = Query.And(listQuery);
            }
            else
            {
                query = null;
            }
            IMongoSortBy sortQuery = new SortByDocument("ArticleId", -1);
            var list = MongoDBHelper.GetAll<Article>("Article", query, new PagerInfo() { Page = pageInex, PageSize = pageSize }, sortQuery);
            return list;
        }
        public List<Article> GetArticleList(string themeName, int? themeId)
        {
            IMongoQuery query;
            var listQuery = new List<IMongoQuery>();
            if (!string.IsNullOrEmpty(themeName))
            {
                listQuery.Add(Query.EQ("ArticleMasterTitle", themeName));
            }
            if (themeId.HasValue && themeId.Value >= 0)
            {
                listQuery.Add(Query.EQ("ArticleThemeId", themeId.Value));
            }
            if (listQuery.Any())
            {
                query = Query.And(listQuery);
            }
            else
            {
                query = null;
            }
            var list = MongoDBHelper.GetAll<Article>("Article", query).OrderBy(x=>x.ArticleId).ToList();
            return list;
        
        }
        public List<string> GetArticleTheme(int pageIndex, int pageSize)
        {
            MongoServer server = new MongoClient(MongoDBHelper.connectionString_Default).GetServer();
            MongoDatabase database = server.GetDatabase(MongoDBHelper.database_Default);
            using (server.RequestStart(database))//开始连接数据库。
            {
                MongoCollection<BsonDocument> myCollection = database.GetCollection<BsonDocument>("Article");
                var query = myCollection.AsQueryable<Article>();
                return query.Select(x => x.ArticleMasterTitle).Distinct().ToList().Skip(pageIndex - 1).Take(pageSize).ToList();
            }

        }
    }
}
