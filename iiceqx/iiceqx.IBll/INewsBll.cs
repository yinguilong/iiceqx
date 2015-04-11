using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using iiceqx.Model;

namespace iiceqx.IBll
{
    public interface INewsBll
    {
        /// <summary>
        /// 为特殊用户加载首页新闻
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="top"></param>
        /// <returns></returns>
        List<News> GetIndexNewsForUser(int userId, int top=6);
        /// <summary>
        /// 加载首页的新闻，为普通游客
        /// </summary>
        /// <param name="top"></param>
        /// <returns></returns>
        List<News> GetInexNewsForVisitor(int top = 6);
        /// <summary>
        /// 获取新闻
        /// </summary>
        /// <param name="newsId"></param>
        /// <returns></returns>
        News GetNewsByNewsId(int newsId);
        /// <summary>
        /// 分页获取新闻
        /// </summary>
        /// <param name="page"></param>
        /// <param name="pageSize"></param>
        /// <returns></returns>
        List<News> GetNewsIndexByPage(int page, int pageSize);
    }
}
