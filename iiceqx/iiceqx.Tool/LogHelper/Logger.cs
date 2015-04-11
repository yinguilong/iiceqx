using log4net;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using log4net;

namespace iiceqx.Tool
{
    public class Logger
    {

        static Logger()
        {
            //            AppDomain.CurrentDomain.BaseDirectory           
            string strLog4netConfigPath = AppDomain.CurrentDomain.BaseDirectory + @"\log4net.xml";
            log4net.Config.XmlConfigurator.ConfigureAndWatch(new System.IO.FileInfo(strLog4netConfigPath));
            log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
            log4net.Appender.IAppender[] appenders = log.Logger.Repository.GetAppenders();
            log4net.Appender.AdoNetAppender adoNetAppender = null;
            for (int i = 0, j = appenders.Length; i < j; i++)
            {
                log4net.Appender.IAppender appender = appenders[i];
                if ("Log2DB" == appender.Name)
                {
                    adoNetAppender = (appender as log4net.Appender.AdoNetAppender);
                    adoNetAppender.ConnectionString = CryptHelper.Decrypto(adoNetAppender.ConnectionString);
                    //log4.Logger.Repository.ResetConfiguration(); //重新加载所有配置，不能使用
                    //重新加载激活（或称为 重新加载）配置
                    adoNetAppender.ActivateOptions();
                }
            }
        }
        public static ILog log;

        public static ILog GetLogger(Type t)
        {
            log = log4net.LogManager.GetLogger(t);
            return log;
        }

        public static ILog GetLogger(string name)
        {
            log = log4net.LogManager.GetLogger(name);
            return log;
        }
        /// <summary>
        /// 输出日志到Log4Net
        /// </summary>
        /// <param name="t"></param>
        /// <param name="ex"></param>
        public static void WriteLog(Type t, Exception ex)
        {
            ILog log = log4net.LogManager.GetLogger(t);
            log.Error("Error", ex);
        }

        public static void WriteLog(Type t, string msg)
        {
            ILog log = log4net.LogManager.GetLogger(t);
            log.Error(msg);
        }

        /// <summary>
        /// 指定文件夹名称并且按时间分组写入日志
        /// </summary>
        /// <param name="t">日志类型</param>
        /// <param name="folder">文件夹名称</param>
        /// <param name="title">日志标题</param>
        /// <param name="content">日志内容</param>
        public static void WriteFileLogForFolder(Type t, string folder, string title, string content)
        {
            var d = DateTime.Now;
            string filePath = string.Format("{0}/{1}/{2}/{3}.log", folder, d.ToString("yyyy"), d.ToString("yyyyMM"),
                                            d.ToString("yyyyMMdd"));
            WriteFileLog(filePath, title, content);
        }
        public static void WriteFileLog(string title, string content)
        {
            WriteFileLog("project.txt", title, content);
        }
        /// <summary>
        /// 指定文件夹名称并且按时间分组写入日志
        /// </summary>
        /// <param name="folder">文件夹名称</param>
        /// <param name="title">日志标题</param>
        /// <param name="content">日志内容</param>
        public static void WriteFileLogForFolder(string folder, string title, string content)
        {
            var d = DateTime.Now;
            string filePath = string.Format(@"{0}\{1}\{2}.txt", folder, d.ToString("yyyyMM"),
                                            d.ToString("yyyyMMdd"));
            WriteFileLog(filePath, title, content);
        }
        /// <summary>
        /// 为特定用途，写文本文件做日志
        ///     \Log\
        /// </summary>
        /// <param name="fileName">xxx.txt</param>
        /// <param name="strErrorMsg">错误信息</param>
        /// <param name="strErrorPrompt">错误提示</param>
        public static void WriteFileLog(String fileName, String strTitle, String strContent)
        {
            String strFile, strDir;
            DateTime now = DateTime.Now;

            //strDir = AppDomain.CurrentDomain.BaseDirectory + @"\Log\";
            strDir = AppDomain.CurrentDomain.BaseDirectory + @"/Log/";
            if (!Directory.Exists(strDir))
            {
                Directory.CreateDirectory(strDir);
            }
            //创建多级目录
            if (!string.IsNullOrEmpty(fileName))
            {
                string[] parts = fileName.Split('/');
                if (parts.Length > 1)
                {
                    for (int i = 0; i < parts.Length - 1; i++)
                    {
                        string part = parts[i];
                        strDir += part + @"\";
                        if (!Directory.Exists(strDir))
                            Directory.CreateDirectory(strDir);
                    }
                    strFile = strDir + parts[parts.Length - 1];
                }
                else
                    strFile = strDir + fileName;
            }
            else
            {
                strFile = strDir + "log.txt";
            }

            FileStream objFileStream = null;
            //文件
            if (File.Exists(strFile))
            {
                objFileStream = new FileStream(strFile, FileMode.Append, FileAccess.Write, FileShare.ReadWrite);
                //sw = File.AppendText(strFile);
            }
            else
            {
                objFileStream = new FileStream(strFile, FileMode.Create, FileAccess.Write, FileShare.ReadWrite);
                //sw = File.CreateText(strFile);
            }

            StreamWriter objStreamWriter = new StreamWriter(objFileStream);
            objStreamWriter.WriteLine("<----------------{0}------------------->", now.ToLongDateString() + " " + now.ToLongTimeString());
            objStreamWriter.WriteLine("Title:");
            objStreamWriter.WriteLine("\t" + strTitle);
            objStreamWriter.WriteLine("Content");
            objStreamWriter.WriteLine("\t" + strContent);
            objStreamWriter.WriteLine("");

            objStreamWriter.Close();
            objStreamWriter.Dispose();
            objFileStream.Close();
            objFileStream.Dispose();

        }

        /// <summary>
        /// 计算代码段运行时间，记录信息到文件中
        /// </summary>
        #region 计算代码段运行时间，记录信息到文件中
        private long _startTimeStamp = 0L;
        private long _endTimeStamp = 0L;
        private string _fileName = string.Empty;
        private string _strTitle = string.Empty;

        /// <summary>
        /// 性能计算开始
        /// </summary>
        /// <param name="className"></param>
        /// <param name="strTitle"></param>
        public Logger(string fileName, string strTitle)
        {
            this._fileName = fileName;
            this._strTitle = strTitle;
            this._startTimeStamp = Stopwatch.GetTimestamp();
        }
        /// <summary>
        /// 性能计算结束
        /// </summary>
        public void PFEnd()
        {
            this._endTimeStamp = Stopwatch.GetTimestamp();
            double elapseSec = (double)(_endTimeStamp - _startTimeStamp) / Stopwatch.Frequency;
            WriteFileLog("performance/" + _fileName + ".txt", _strTitle, string.Format("using:{0}s", elapseSec.ToString("0.000000")));
        }
        #endregion
    }
}
