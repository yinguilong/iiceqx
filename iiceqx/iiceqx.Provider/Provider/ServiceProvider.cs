using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using iiceqx.Model;
using MongoDB.Driver;
using MongoDB.Driver.Builders;

namespace iiceqx.Provider
{
    public class ServiceProvider
    {
        public void Insert<T>(T t) where T : class
        {
            MongoDBHelper.InsertOne<T>(typeof(T).Name, t);
        }
        public void InsertArticle(Article article)
        {
            MongoDBHelper.InsertOne<Article>("Article", article);
        }
        public Article GetLastArticle()
        {
            IMongoSortBy sortQuery = new SortByDocument("ArticleId", -1);
            return MongoDBHelper.GetAll<Article>("Article", 1, sortQuery).SingleOrDefault();
        }
        public bool InsertNews(News news)
        {
            var result = MongoDBHelper.InsertOne<News>("News", news);
            return true;
        }
        public List<T> GetAll<T>() where T : class
        {
            return MongoDBHelper.GetAll<T>(typeof(T).Name);
        }
        public News GetLastNews()
        {
            IMongoSortBy sortQuery = new SortByDocument("NewsId", -1);
            return MongoDBHelper.GetAll<News>("News", 1, sortQuery).SingleOrDefault();
        }
        public void UpdateNews(News news)
        {
            MongoDBHelper.UpdateOne<News>("News", news);
        }
        public void Update<T>(T t) where T : class
        {
            MongoDBHelper.UpdateOne<T>(typeof(T).Name, t);
        }
    }
}
