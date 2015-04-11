using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using iiceqx.IBll;
using iiceqx.Tool.IocHelper;
using iiceqx.Model;
namespace iiceqx.Service
{
    class Program
    {
        static void Main(string[] args)
        {
            var Run = true;
            while (Run)
            {
                try
                {
                    var service = IocHelper.Resolve<IService>();
                    //service.InsetArticleFromCnblogs("http://www.cnblogs.com/nihaoCPP/p/operator_overload.html", DictArticleType.CPlus);
                    //service.UpdateNewsContent();
                    service.InsertNews("http://www.infoq.com/cn/news/2015/04/Mono-4-Preview", DictNewsType.Mono);
                    //service.InsertArticleFromWinMono("http://bbs.winmono.com/thread-52-1-1.html", DictArticleType.Android, "Mono For Android", (int)DictReaderLevel.初级读者);
                }
                catch (Exception ex)
                {

                    Console.WriteLine("出错啦！ex:" + ex.Message);
                }
                Run = false;
            }
        }
    }
}
