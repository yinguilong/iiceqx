using iiceqx.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace iiceqx.IBll
{
    public interface IService
    {
        /// <summary>
        /// 添加新闻
        /// </summary>
        /// <returns></returns>
        bool InsertNews(string sourceUrl, DictNewsType newsType);
        void UpdateNewsContent();
        /// <summary>
        /// 添加博客园文章
        /// </summary>
        /// <param name="sourceUrl"></param>
        /// <param name="articleType"></param>
        /// <returns></returns>
        bool InsetArticleFromCnblogs(string sourceUrl, DictArticleType articleType);
        /// <summary>
        /// 添加winmono文章
        /// </summary>
        /// <param name="sourceUrl"></param>
        /// <param name="articleType"></param>
        /// <param name="mastTitle"></param>
        /// <param name="readerLevel"></param>
        /// <returns></returns>
        bool InsertArticleFromWinMono(string sourceUrl, DictArticleType articleType, string mastTitle, int readerLevel);
    }
}
