﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Com.Ddlev.Weixin
{
    public static class DataCacheConfig
    {
        public static Com.Ddlev.DataCache.IDataCacheHelper GetHelper()
        {
            Com.Ddlev.DataCache.DataCacheType ct = Com.Ddlev.DataCache.DataCacheType.Sysnet;
            int _db = -1;
            try
            {
                ct =(Com.Ddlev.DataCache.DataCacheType) Convert.ToInt32( System.Configuration.ConfigurationManager.AppSettings["cachetype"]);
            }
            catch
            {
                ct = Com.Ddlev.DataCache.DataCacheType.Sysnet;
            }
            if (ct == DataCache.DataCacheType.Redis)
            {
                try
                {
                    _db = Convert.ToInt32(System.Configuration.ConfigurationManager.AppSettings["_dbindex"]); //设置保存在redis的第几个数据库里
                }
                catch
                {
                    _db = -1; //默认是第一库
                }
            }
            return  Com.Ddlev.DataCache.DataCacheHelper.GetCacheHelp(ct, _db);
        }
    }
}