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
            try
            {
                ct =(Com.Ddlev.DataCache.DataCacheType) Convert.ToInt32( System.Configuration.ConfigurationManager.AppSettings["wxcachetype"]);
            }
            catch
            {
                ct = Com.Ddlev.DataCache.DataCacheType.Sysnet;
            }
            if (ct == DataCache.DataCacheType.Redis)
            {
                try
                {
                    _db = Convert.ToInt32(System.Configuration.ConfigurationManager.AppSettings["_wxdbindex"]); //设置保存在redis的第几个数据库里
                }
                catch
                {
                    _db = -1; //默认是第一库
                }
            }
            return Com.Ddlev.DataCache.DataCacheHelper.GetCacheHelp(ct, _db);
        }
    }
}
