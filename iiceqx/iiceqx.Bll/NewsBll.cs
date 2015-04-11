using iiceqx.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using iiceqx.Provider;
using iiceqx.IBll;
namespace iiceqx.Bll
{
    public class NewsBll : INewsBll
    {
        private static NewsProvider newsProvider = new NewsProvider();
        public List<News> GetIndexNewsForUser(int userId, int top = 6)
        {
            //现获取用户的关注的新闻类型
            var userFavoritList = newsProvider.GetUserFavoriteList(userId);
            var list = newsProvider.GetIndexNewsForUser(userFavoritList, top);
            return list;
        }
        public List<News> GetInexNewsForVisitor(int top = 6)
        {
            return newsProvider.GetIndexNewsForVisitor(top);
        }
        public List<News> GetNewsIndexByPage(int page, int pageSize)
        {
            return newsProvider.GetNewsByPage(page, pageSize);
        }
        public News GetNewsByNewsId(int newsId)
        {
            return newsProvider.GetNewsByNewsId(newsId);
        }
    }
}
