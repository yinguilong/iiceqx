using iiceqx.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace iiceqx.IBll
{
    public interface IArticleBll
    {
        /// <summary>
        /// 获取文章
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Article GetArticleById(int id);
        /// <summary>
        /// 首页文章获取
        /// </summary>
        /// <param name="pageInex"></param>
        /// <param name="pageSize"></param>
        /// <returns></returns>
        List<Article> GetArticleListIndex(string themeName, int? themeId, int pageInex, int pageSize);
        /// <summary>
        /// 根据主题来获取所有文章
        /// </summary>
        /// <param name="themeName"></param>
        /// <param name="themeId"></param>
        /// <returns></returns>
        List<Article> GetArticleList(string themeName, int? themeId);
        /// <summary>
        /// 获取文章主题类型
        /// </summary>
        /// <param name="groupbyStr"></param>
        /// <returns></returns>
        List<string> GetArticleTheme(int pageIndex, int pageSize);
    }
}
