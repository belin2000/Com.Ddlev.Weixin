using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Text;


namespace Com.Ddlev.Weixin.High
{
    /// <summary>
    /// 上传与下载
    /// </summary>
    public class UploadAndDownload
    {
        /// <summary>
        /// 上传文件到微信上
        /// </summary>
        /// <param name="c">配置</param>
        /// <param name="type">分别有图片（image）、语音（voice）、视频（video）和缩略图（thumb，主要用于视频与音乐格式的缩略图）</param>
        /// <param name="filename">文件的路径（真实路径）</param>
        /// <returns>返回media_id</returns>
        public static string Upload(Config c, string type, string filename)
        {
            try
            {
                HightToken token = new HightToken(c);
                string posturl = "http://file.api.weixin.qq.com/cgi-bin/media/upload?access_token=" + token.Token + "&type=" + type;
                string sb = BaseClass.BaseMethod.UploadFileByWebClient(posturl, filename);
                JObject json = JObject.Parse(sb);
                if (json["errcode"] != null)
                {
                    return "";
                }
                else
                {
                    return json["media_id"].ToString();
                }
            }
            catch
            {
                return "";
            }
        }
        
        /// <summary>
        /// 下载文件到本地
        /// </summary>
        /// <param name="c">配置</param>
        /// <param name="media_id">media_id</param>
        /// <param name="pathandfile">不含文件后缀（"/upload/abc"）,后缀自动获取</param>
        /// <returns>返回文件的路径</returns>
        public static string Download(Config c, string media_id,string pathandfile)
        {
            HightToken token = new HightToken(c);
            string posturl = "http://file.api.weixin.qq.com/cgi-bin/media/get?access_token=" + token.Token + "&media_id=" + media_id;
            string sb = BaseClass.BaseMethod.DownloadFileByWebClient(posturl, pathandfile);
            return sb;
        }
    }
}
