/*
 * 程序中文名称: 将博内容管理系统通用版
 * 
 * 程序英文名称: JumboTCMS
 * 
 * 程序版本: 5.2.X
 * 
 * 程序编写: 随风缘 (定制开发请联系：jumbot114#126.com,不接受免费的技术答疑,请见谅)
 * 
 * 官方网站: http://www.jumbotcms.net/
 * 
 * 商业服务: http://www.jumbotcms.net/about/service.html
 * 
 */

using System;
using System.Data;
using System.Data.SqlClient;
using System.Web;
using JumboTCMS.Utils;
using JumboTCMS.DBUtility;

namespace JumboTCMS.DAL
{
    /// <summary>
    /// 频道表信息
    /// </summary>
    public class Normal_ChannelDAL : Common
    {
        public Normal_ChannelDAL()
        {
            base.SetupSystemDate();
        }
        /// <summary>
        /// 是否存在记录
        /// </summary>
        /// <param name="_wherestr">条件</param>
        /// <returns></returns>
        public bool Exists(string _wherestr)
        {
            using (DbOperHandler _doh = new Common().Doh())
            {
                int _ext = 0;
                _doh.Reset();
                _doh.ConditionExpress = _wherestr;
                if (_doh.Exist("jcms_normal_channel"))
                    _ext = 1;
                return (_ext == 1);
            }

        }
        /// <summary>
        /// 判断重复性(标题是否存在)
        /// </summary>
        /// <param name="_title">需要检索的标题</param>
        /// <param name="_id">除外的ID</param>
        /// <param name="_wherestr">其他条件</param>
        /// <returns></returns>
        public bool ExistTitle(string _title, string _id, string _wherestr)
        {
            int _ext = 0;
            using (DbOperHandler _doh = new Common().Doh())
            {
                _doh.Reset();
                _doh.ConditionExpress = "title=@title and id<>" + _id;
                if (_wherestr != "") _doh.ConditionExpress += " and " + _wherestr;
                _doh.AddConditionParameter("@title", _title);
                if (_doh.Exist("jcms_normal_channel"))
                    _ext = 1;
            }
            return (_ext == 1);
        }
        /// <summary>
        /// 删除一条数据
        /// </summary>
        public bool DeleteByID(string _id)
        {
            using (DbOperHandler _doh = new Common().Doh())
            {
                _doh.Reset();
                _doh.ConditionExpress = "id=@id";
                _doh.AddConditionParameter("@id", _id);
                int _del = _doh.Delete("jcms_normal_channel");
                return (_del == 1);
            }

        }
        /// <summary>
        /// 绑定记录至频道实体
        /// </summary>
        /// <param name="_id"></param>
        public JumboTCMS.Entity.Normal_Channel GetEntity(DataRow dr)
        {
            JumboTCMS.Entity.Normal_Channel channel = new JumboTCMS.Entity.Normal_Channel();
            channel.Id = dr["Id"].ToString();
            channel.Title = dr["Title"].ToString();
            channel.Info = dr["Info"].ToString();
            channel.ClassDepth = Validator.StrToInt(dr["ClassDepth"].ToString(), 0);
            channel.Dir = dr["Dir"].ToString();
            channel.SubDomain = dr["SubDomain"].ToString();
            channel.pId = Validator.StrToInt(dr["pId"].ToString(), 0);
            channel.ItemName = dr["ItemName"].ToString();
            channel.ItemUnit = dr["ItemUnit"].ToString();
            channel.TemplateId = Validator.StrToInt(dr["TemplateId"].ToString(), 0);
            channel.Type = dr["Type"].ToString().ToLower();
            channel.Enabled = Validator.StrToInt(dr["Enabled"].ToString(), 0) == 1;
            channel.DefaultThumbs = Validator.StrToInt(dr["DefaultThumbs"].ToString(), 0);
            channel.IsPost = Validator.StrToInt(dr["IsPost"].ToString(), 0) == 1;
            channel.IsHtml = Validator.StrToInt(dr["IsHtml"].ToString(), 0) == 1;
            channel.IsTop = Validator.StrToInt(dr["IsTop"].ToString(), 0) == 1;
            channel.UploadPath = dr["UploadPath"].ToString().Replace("<#SiteDir#>", site.Dir).Replace("<#ChannelDir#>", channel.Dir).Replace("//", "/");
            channel.UploadType = dr["UploadType"].ToString();
            channel.UploadSize = Validator.StrToInt(dr["UploadSize"].ToString(), 1024);
            channel.LanguageCode = dr["LanguageCode"].ToString();
            return channel;
        }
        /// <summary>
        /// 获得单页内容的单条记录实体
        /// </summary>
        /// <param name="_id"></param>
        public JumboTCMS.Entity.Normal_Channel GetEntity(string _id)
        {

            using (DbOperHandler _doh = new Common().Doh())
            {
                JumboTCMS.Entity.Normal_Channel channel = new JumboTCMS.Entity.Normal_Channel();
                _doh.Reset();
                _doh.SqlCmd = "SELECT * FROM [jcms_normal_channel] WHERE [Id]=" + _id;
                DataTable dt = _doh.GetDataTable();
                if (dt.Rows.Count > 0)
                {
                    DataRow dr = dt.Rows[0];
                    channel = GetEntity(dr);
                }
                dt.Clear();
                dt.Dispose();
                return channel;
            }

        }
        public string GetChannelName(string _id)
        {
            using (DbOperHandler _doh = new Common().Doh())
            {
                _doh.Reset();
                _doh.SqlCmd = "SELECT [Title] FROM [jcms_normal_channel] WHERE [Id]=" + _id;
                DataTable dt = _doh.GetDataTable();
                if (dt.Rows.Count > 0)
                {
                    return dt.Rows[0]["Title"].ToString().ToLower();
                }
                return string.Empty;
            }

        }
        public string GetChannelType(string _id)
        {
            using (DbOperHandler _doh = new Common().Doh())
            {
                _doh.Reset();
                _doh.SqlCmd = "SELECT [Type] FROM [jcms_normal_channel] WHERE [Id]=" + _id;
                DataTable dt = _doh.GetDataTable();
                if (dt.Rows.Count > 0)
                {
                    return dt.Rows[0]["Type"].ToString().ToLower();
                }
                return string.Empty;
            }
        }
        public string GetChannelLink(bool _ishtml, string _channelid, bool _truefile)
        {
            JumboTCMS.Entity.Normal_Channel _Channel = new JumboTCMS.DAL.Normal_ChannelDAL().GetEntity(_channelid);
            string TempUrl = JumboTCMS.Common.PageFormat.Channel(site.Dir, site.UrlReWriter);
            if ((_Channel.SubDomain.Length > 0) && (!_truefile))
                TempUrl = TempUrl.Replace("<#SiteDir#><#ChannelDir#>", _Channel.SubDomain);
            TempUrl = TempUrl.Replace("<#SiteDir#>", site.Dir);
            TempUrl = TempUrl.Replace("<#SiteStaticExt#>", site.StaticExt);
            TempUrl = TempUrl.Replace("<#ChannelId#>", _channelid);
            TempUrl = TempUrl.Replace("<#ChannelDir#>", _Channel.Dir);
            return TempUrl;
        }
        /// <summary>
        /// 解析频道标签
        /// </summary>
        /// <param name="pagestr">原内容</param>
        /// <param name="_channelid">ChannelId不能为0</param>
        public void ExecuteTags(ref string PageStr, string _channelid)
        {
            JumboTCMS.Entity.Normal_Channel _Channel = GetEntity(_channelid);
            ExecuteTags(ref PageStr, _Channel);
        }
        public void ExecuteTags(ref string PageStr, JumboTCMS.Entity.Normal_Channel _Channel)
        {
            PageStr = PageStr.Replace("{$ChannelId}", _Channel.Id.ToString());
            PageStr = PageStr.Replace("{$ChannelName}", _Channel.Title);
            PageStr = PageStr.Replace("{$ChannelInfo}", _Channel.Info);
            PageStr = PageStr.Replace("{$ChannelType}", _Channel.Type);
            PageStr = PageStr.Replace("{$ChannelDir}", _Channel.Dir);
            PageStr = PageStr.Replace("{$ChannelItemName}", _Channel.ItemName);
            PageStr = PageStr.Replace("{$ChannelItemUnit}", _Channel.ItemUnit);
            PageStr = PageStr.Replace("{$ChannelLink}", Go2Channel(site.IsHtml, _Channel.Id.ToString(), false));
        }
        /// <summary>
        /// 获得频道默认缩略图尺寸
        /// </summary>
        /// <param name="_channelid"></param>
        /// <param name="iWidth"></param>
        /// <param name="iHeight"></param>
        /// <returns></returns>
        public bool GetThumbsSize(string _channelid, ref int iWidth, ref int iHeight)
        {
            using (DbOperHandler _doh = new Common().Doh())
            {
                iWidth = 0;
                iHeight = 0;
                _doh.Reset();
                _doh.SqlCmd = "select iWidth,iHeight from [jcms_normal_thumbs] where id =(select DefaultThumbs from [jcms_normal_channel] where id=" + _channelid + ")";
                DataTable dtThumbs = _doh.GetDataTable();
                if (dtThumbs.Rows.Count == 1)
                {
                    iWidth = Str2Int(dtThumbs.Rows[0]["iWidth"].ToString());
                    iHeight = Str2Int(dtThumbs.Rows[0]["iHeight"].ToString());
                }
                dtThumbs.Clear();
                dtThumbs.Dispose();
                return true;
            }
        }
    }
}
