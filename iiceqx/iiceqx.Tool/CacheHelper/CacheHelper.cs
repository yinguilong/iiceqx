using Memcached.Client;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;

namespace iiceqx.Tool
{
    public class CacheHelper
    {
        private static readonly string CacheServer = ConfigurationManager.AppSettings["CacheServer"];
        private static readonly MemcachedClient Mc;
        static CacheHelper()
        {
            Mc = new MemcachedClient();
        }

        public static void Set<T>(string key, T obj) where T : class
        {

            try
            {
                Mc.EnableCompression = false;
                if (obj != null)
                {
                    Mc.Set(key, obj);
                    //Mc.Set(key, DataJsonParse.JsonSerializer<T>(obj));
                    //Mc.Set(key, Newtonsoft.Json.JavaScriptConvert.SerializeObject(obj));
                }
            }
            catch (Exception ex)
            {
                Logger.WriteFileLog("MemcachedClient", ex.ToString());
            }
        }

        public static void Set<T>(string key, T obj, DateTime expiry) where T : class
        {
            if (obj == null) return;
            try
            {
                var mc = new MemcachedClient();
                mc.EnableCompression = false;
                mc.Set(key, obj, expiry);
            }
            catch (Exception ex)
            {
                Logger.WriteFileLog("MemcachedClient", ex.ToString());
            }
        }

        /// <summary>
        /// 更新一个对象的内容及过期时间
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="key"></param>
        /// <param name="obj"></param>
        /// <param name="expiry"></param>
        public static void UpdateObj<T>(string key, T obj, DateTime expiry) where T : class
        {
            if (obj == null)
                return;
            try
            {
                var mc = new MemcachedClient();
                mc.EnableCompression = false;
                mc.Replace(key, obj, expiry);
            }
            catch (Exception ex)
            {
                Logger.WriteFileLog("MemcachedClient", ex.ToString());
            }
        }

        public static T Get<T>(string key)
        {
            try
            {
                var mc = new MemcachedClient();
                if (mc.KeyExists(key))
                {
                    return (T)mc.Get(key);
                    //return   Newtonsoft.Json.JavaScriptConvert.DeserializeObject<T>(Mc.Get(key).ToString());
                    //return DataJsonParse.ParseFromJson<T>(Mc.Get(key).ToString());
                }
            }
            catch (Exception ex)
            {
                Logger.WriteFileLog("MemcachedClient", ex.ToString());
            }
            return default(T);
        }

        public static bool FlushAll()
        {
            try
            {
                return Mc.FlushAll();
            }
            catch (Exception ex)
            {
                Logger.WriteFileLog("MemcachedClient", ex.ToString());
                return false;
            }
        }

        public static bool DeleteCacheByKey(string key)
        {
            try
            {
                var mc = new MemcachedClient();
                return mc.Delete(key);
            }
            catch (Exception ex)
            {
                Logger.WriteFileLog("MemcachedClient", ex.ToString());
                return false;
            }
        }

        public static void MemcachedPoolinitialize()
        {
            char[] separator = { ',' };
            string[] serverlist = CacheServer.Split(separator);

            // initialize the pool for memcache servers  
            try
            {
                SockIOPool pool = SockIOPool.GetInstance();
                if (pool != null)
                {
                    pool.SetServers(serverlist);

                    pool.InitConnections = 3;
                    pool.MinConnections = 3;
                    pool.MaxConnections = 50;

                    pool.SocketConnectTimeout = 1000;
                    pool.SocketTimeout = 3000;

                    pool.MaintenanceSleep = 30;
                    pool.Failover = true;

                    pool.Nagle = false;
                    pool.Initialize();
                }
            }
            catch (Exception err)
            {
                Logger.WriteLog(typeof(MemcachedClient), err);
            }
        }

        public static void MemcachedPoolDestory()
        {
            if (SockIOPool.GetInstance() != null)
                SockIOPool.GetInstance().Shutdown();
        }
    }
}
