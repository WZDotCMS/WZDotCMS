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
namespace JumboTCMS.DAL
{
    public class ModuleCommand
    {
        public static IModule IMD;
        static ModuleCommand()
        {

        }
        /// <summary>
        /// 得到内容页地址
        /// </summary>
        /// <param name="_page"></param>
        /// <param name="_ishtml"></param>
        /// <param name="_channelid"></param>
        /// <param name="_contentid"></param>
        /// <returns></returns>
        public static string GetContentLink(string _module, int _page, bool _ishtml, string _channelid, string _contentid, bool _truefile)
        {
            IMD = (IModule)Activator.CreateInstance(Type.GetType(String.Format("JumboTCMS.DAL.Module_{0}DAL", _module), true, true));
            return IMD.GetContentLink(_page, _ishtml, _channelid, _contentid, _truefile);
        }
        /// <summary>
        /// 生成内容页
        /// </summary>
        /// <param name="_ChannelId"></param>
        /// <param name="_ContentId"></param>
        /// <param name="_CurrentPage"></param>
        public static void CreateContent(string _module, string _ChannelId, string _ContentId, int _CurrentPage)
        {
            IMD = (IModule)Activator.CreateInstance(Type.GetType(String.Format("JumboTCMS.DAL.Module_{0}DAL", _module), true, true));
            IMD.CreateContent(_ChannelId, _ContentId, _CurrentPage);
        }
        /// <summary>
        /// 得到内容页
        /// </summary>
        /// <param name="_ChannelId"></param>
        /// <param name="_ContentId"></param>
        /// <param name="_CurrentPage"></param>
        public static string GetContent(string _module, string _ChannelId, string _ContentId, int _CurrentPage)
        {
            IMD = (IModule)Activator.CreateInstance(Type.GetType(String.Format("JumboTCMS.DAL.Module_{0}DAL", _module), true, true));
            return IMD.GetContent(_ChannelId, _ContentId, _CurrentPage);
        }
        /// <summary>
        /// 删除内容页
        /// </summary>
        /// <param name="_ChannelId"></param>
        /// <param name="_ContentId"></param>
        public static void DeleteContent(string _module, string _ChannelId, string _ContentId)
        {
            IMD = (IModule)Activator.CreateInstance(Type.GetType(String.Format("JumboTCMS.DAL.Module_{0}DAL", _module), true, true));
            IMD.DeleteContent(_ChannelId, _ContentId);
        }
    }
}
