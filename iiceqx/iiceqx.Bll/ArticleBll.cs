using iiceqx.IBll;
using iiceqx.Model;
using iiceqx.Provider.Provider;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace iiceqx.Bll
{
    public class ArticleBll : IArticleBll
    {
        private static ArticleProvider articleProvider = new ArticleProvider();
        public Article GetArticleById(int id)
        {
            return articleProvider.GetArticleById(id);
        }
        public List<Article> GetArticleListIndex(string themeName, int? themeId, int pageInex, int pageSize)
        {
            return articleProvider.GetArticleListIndex(themeName, themeId, pageInex, pageSize);
        }
        public List<Article> GetArticleList(string themeName, int? themeId)
        {
            return articleProvider.GetArticleList(themeName, themeId);
        }
        public List<string> GetArticleTheme(int pageIndex, int pageSize)
        {
            return articleProvider.GetArticleTheme(pageIndex, pageSize);
        }
    }
}
