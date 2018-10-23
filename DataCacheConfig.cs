using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace Com.Ddlev.Weixin
{
    internal static class DataCacheConfig
    {
        /// <summary>
        /// 获取缓存的操作方式（用于配置缓存）
        /// </summary>
        /// <returns></returns>
        public static Com.Ddlev.DataCache.IDataCacheHelper GetHelper()
        {
            
            Com.Ddlev.DataCache.DataCacheType ct = Com.Ddlev.DataCache.DataCacheType.Sysnet;
            int _db = -1;
            ct = string.IsNullOrWhiteSpace(System.Configuration.ConfigurationManager.AppSettings["wxcachetype"])? Com.Ddlev.DataCache.DataCacheType.Sysnet : (Com.Ddlev.DataCache.DataCacheType)Convert.ToInt32(System.Configuration.ConfigurationManager.AppSettings["wxcachetype"]);
            if (ct == DataCache.DataCacheType.Redis)
            {
                _db = string.IsNullOrWhiteSpace(System.Configuration.ConfigurationManager.AppSettings["_wxdbindex"]) ? -1 : Convert.ToInt32(System.Configuration.ConfigurationManager.AppSettings["_wxdbindex"]);
            }
            return Com.Ddlev.DataCache.DataCacheHelper.GetCacheHelp(ct, _db, System.Configuration.ConfigurationManager.AppSettings["_wxradisconfig"]);
        }

        
    }
}
