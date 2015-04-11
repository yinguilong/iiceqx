using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using iiceqx.IBll;
using iiceqx.Model;
using iiceqx.Provider;
using HtmlAgilityPack;
using System.Net;
namespace iiceqx.Bll
{
    public class Service : IService
    {
        private static ServiceProvider serviceProvider = new ServiceProvider();
        #region Article相关
        public bool InsertArticleFromWinMono(string sourceUrl, DictArticleType articleType,string mastTitle,int readerLevel)
        {
            var lastArticle = serviceProvider.GetLastArticle();
            var article = new Article();
            article.ArticleId = lastArticle == null ? 1 : (lastArticle.ArticleId + 1);
            article.ArticleMasterTitle = mastTitle;
            string title = string.Empty;
            string summary = string.Empty;
            string authorName = string.Empty;
            article.CreateDate = DateTime.Now;
            article.ReaderLevel = readerLevel;
            article.ArticleSouce = "www.winmono.com";
            article.Content = GetArticleContentWinMono(sourceUrl, ref title, ref summary, ref authorName);
            article.ArticleType = (int)articleType;
            article.AuthorName = authorName;
            article.ArticleTitle = title;
            article.Summary = summary;
            serviceProvider.Insert<Article>(article);
            var list = serviceProvider.GetAll<Article>();
            return true;
        }

        public bool InsetArticleFromCnblogs(string sourceUrl, DictArticleType articleType)
        {
            var lastArticle = serviceProvider.GetLastArticle();
            var article = new Article();
            article.ArticleId = lastArticle == null ? 1 : (lastArticle.ArticleId + 1);
            article.ArticleMasterTitle = "C++初级编程";
            string title=string.Empty;
            string summary=string.Empty;
            string authorName = string.Empty;
            article.CreateDate=DateTime.Now;
            article.ReaderLevel = (int)DictReaderLevel.初级读者;
            article.ArticleSouce = "www.cnblogs.com";
            article.Content = GetArticleContentCnblogs(sourceUrl, ref title, ref summary, ref authorName);
            article.ArticleType = (int)articleType;
            article.AuthorName = authorName;
            article.ArticleTitle = title;
            article.Summary = summary;
            serviceProvider.Insert<Article>(article);
            var list = serviceProvider.GetAll<Article>();
            return true;
        }
        public string GetArticleContentWinMono(string sourceUrl, ref string title, ref string summary, ref string authorName)
        {
            string ret = string.Empty;
            title = string.Empty;
            summary = string.Empty;
            authorName = string.Empty;
            authorName = "未知";
            var htmlWeb = new HtmlWeb { OverrideEncoding = Encoding.GetEncoding("gbk") };
            var htmlDocument = htmlWeb.Load(sourceUrl);
            foreach (var script in htmlDocument.DocumentNode.Descendants("script").ToArray())
                script.Remove();
            foreach (var style in htmlDocument.DocumentNode.Descendants("style").ToArray())
                style.Remove();
            var textNodes = htmlDocument.DocumentNode.SelectNodes("//td[@class='t_f']")[0];
            if (textNodes != null)
            {
                ret = textNodes.InnerHtml;
            }
            else
            {

            }
            var cNodes = htmlDocument.DocumentNode.SelectSingleNode("//h1/a[@id='thread_subject']");


            if (cNodes != null)
            {
                title = cNodes.InnerText;
            }
            var sumNode = textNodes.SelectNodes("p")[0];
            if (sumNode != null)
            {
                summary = sumNode.InnerHtml;
            }
            return ret;
        }
        public string GetArticleContentCnblogs(string sourceUrl,ref string title,ref string summary,ref string authorName)
        {
            string ret = string.Empty;
            title = string.Empty;
            summary = string.Empty;
            authorName = string.Empty;
            string[] arrUrl = sourceUrl.Split('/');
            authorName = arrUrl[3];
            var htmlWeb = new HtmlWeb { OverrideEncoding = Encoding.GetEncoding("UTF-8") };
            var htmlDocument = htmlWeb.Load(sourceUrl);
            foreach (var script in htmlDocument.DocumentNode.Descendants("script").ToArray())
                script.Remove();
            foreach (var style in htmlDocument.DocumentNode.Descendants("style").ToArray())
                style.Remove();
            var textNodes = htmlDocument.DocumentNode.SelectSingleNode("//div[@id='cnblogs_post_body']");
            if (textNodes != null)
            {
                ret = textNodes.InnerHtml;
            }
            else
            {
               
            }
            var cNodes = htmlDocument.DocumentNode.SelectSingleNode("//a[@id='cb_post_title_url']");
         

            if (cNodes != null)
            {
                title = cNodes.InnerText;
            }
            var sumNode = textNodes.ChildNodes[0];
            if (sumNode!=null)
            {
                summary = sumNode.InnerHtml;
            }
            return ret;
        }
        #endregion
        #region 新闻相关
        public bool InsertNews(string sourceUrl, DictNewsType newsType)
        {

            var lastNews = serviceProvider.GetLastNews();
            var news = new News();
            news.NewsId = lastNews.NewsId + 1;
            news.NewsType = (int)newsType;
            string summary = string.Empty;
            string title = string.Empty;
            news.Content = GetContent(sourceUrl, out summary, out title);
            news.Title = title;
            news.Summary = summary;
            news.CreateTime = DateTime.Now;
            news.SouceUrl = "www.infoq.com";
            news.Operator = "system";
            serviceProvider.InsertNews(news);

            var list = serviceProvider.GetAll<News>();
            return true;
        }
        public void UpdateNewsContent()
        {
            var list = serviceProvider.GetAll<News>();
            if (list!=null&&list.Any())
            {
                for (int i = 0; i < list.Count; i++)
                {
                    var news = list[i];
                    news.Content = news.Content.Replace("<p>", "<p class=\"lead text-justify\">");
                    serviceProvider.UpdateNews(news);
                }
            }
        }
        public string GetContent(string url, out string summary, out string title)
        {
            string ret = string.Empty;

            //string url = "http://www.infoq.com/cn/news/2015/02/mongodb-3-will-release";
            var htmlWeb = new HtmlWeb { OverrideEncoding = Encoding.GetEncoding("UTF-8") };
            HtmlNode.ElementsFlags.Remove("p");
            var htmlDocument = htmlWeb.Load(url, "GET");
            //WebClient wc = new WebClient();
            //wc.Encoding = Encoding.UTF8;
            //HtmlDocument htmlDocument = new HtmlDocument();
            //string html = wc.DownloadString(url);
            //htmlDocument.LoadHtml(html);
            foreach (var script in htmlDocument.DocumentNode.Descendants("script").ToArray())
                script.Remove();
            foreach (var style in htmlDocument.DocumentNode.Descendants("style").ToArray())
                style.Remove();
            var textNodes = htmlDocument.DocumentNode.SelectNodes("//div[@class='text_info']/p");
            string str = string.Empty;
            HtmlNodeCollection newTextNodes;
            if (textNodes.Count == 1)
            {
                str = FormatPtag(textNodes[0].OuterHtml);
                HtmlNode.ElementsFlags.Add("p", HtmlElementFlag.Closed | HtmlElementFlag.Empty);
                HtmlDocument newHtmlDocument = new HtmlDocument();
                newHtmlDocument.LoadHtml(str);
                newTextNodes = newHtmlDocument.DocumentNode.SelectNodes("/p");
            }
            else
            {
                newTextNodes = htmlDocument.DocumentNode.SelectNodes("//div[@class='text_info']/p|//div[@class='text_info']/blockquote");
            }
            var cNodes = htmlDocument.DocumentNode.SelectSingleNode("//div[@class='text_info']/hr");
            var titleNodes = htmlDocument.DocumentNode.SelectSingleNode("//div[@id='content']/h1");
            title = string.Empty;
            summary = string.Empty;
            if (titleNodes != null)
            {
                title = titleNodes.InnerText;
            }
            if (newTextNodes != null)
            {
                StringBuilder sb = new StringBuilder();
                for (int i = 0; i < newTextNodes.Count; i++)
                {
                    var item = newTextNodes[i];
                    if (string.IsNullOrEmpty(summary))
                    {
                        summary = item.InnerHtml;
                    }
                    var strHtml = item.OuterHtml;
                    if (strHtml.Contains("感谢") && strHtml.Contains("审校"))
                    {
                        continue;
                    }
                    if (strHtml.Contains("给InfoQ中文站投稿或者参与内容翻译工作，请邮件至"))
                    {
                        continue;
                    }
                    sb.Append(item.OuterHtml);
                }

                ret = sb.ToString();
            }
            return ret;

        }
        public string FormatPtag(string sourceHtml)
        {
            if (!sourceHtml.Contains("<p"))
            {
                return sourceHtml;
            }
            sourceHtml = sourceHtml.Replace("<p>", "|").Replace("</p>", "|");
            string[] textNodesArr = sourceHtml.Split('|');
            StringBuilder sb = new StringBuilder();
            foreach (var item in textNodesArr)
            {
                if (!string.IsNullOrEmpty(item) && item != Environment.NewLine && !item.Contains("class=\"random_links\"") && !item.Contains("<p") && !item.Contains("<hr>") && !item.Contains("editorial_links") && !item.Contains("class=\"links\"") && !item.Contains("class=\"clear\"") && !item.Contains("face=\"Verdana\""))
                    sb.AppendFormat("<p class=\"lead text-justify\">{0}</p>", item);
            }
            return sb.ToString();
        }
        #endregion
    }
}
