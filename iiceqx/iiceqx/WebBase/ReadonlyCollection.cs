using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;

namespace iiceqx.web.WebBase
{
    public class ReadonlyCollection
    {
        public static readonly string CURRENTUSER = "currentuserlogin_iiceqx";
        public static readonly string CURRENTUSER_ID = "currentuserid_iiceqx_";
        public static readonly string CURRENTUSERPASSWORD = "currentuserpwd_iiceqx";
        public static readonly string CLIENTSESSIONNAME = "clientsessionname_iiceqx";
        public static readonly string CLIENTSESSIONNAMECOOKIEVALUE = "cookie_key_iiceqx";
        public static readonly string ROOTDOMAIN = ConfigurationManager.AppSettings["RootDomain"]; 
        public static readonly string SERVICE_KEY_PREFFIX = "iiceqx_service_";
    }
}