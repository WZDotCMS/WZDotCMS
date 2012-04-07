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
using System.Collections.Generic;
using System.Text;

using JumboTCMS.TEngine;
using JumboTCMS.TEngine.Parser.AST;

namespace JumboTCMS.DAL
{
    /// <summary>
    /// 获得标题
    /// </summary>
    public class TemplateTag_GetFormatTitle : ITagHandler
    {
        public void TagBeginProcess(TemplateManager manager, Tag tag, ref bool processInnerElements, ref bool captureInnerContent)
        {
            processInnerElements = true;
            captureInnerContent = true;
        }

        public void TagEndProcess(TemplateManager manager, Tag tag, string innerContent)
        {
            Expression exp;
            string _title, _formattitle;
            exp = tag.AttributeValue("title");
            if (exp == null)
                throw new Exception("没有title标签");
            _title = manager.EvalExpression(exp).ToString();
            _formattitle = JumboTCMS.Utils.Strings.HtmlEncode(_title);
            manager.WriteValue(_formattitle);
        }
    }
    /// <summary>
    /// 获得频道名称
    /// </summary>
    public class TemplateTag_GetChannelName : ITagHandler
    {
        public void TagBeginProcess(TemplateManager manager, Tag tag, ref bool processInnerElements, ref bool captureInnerContent)
        {
            processInnerElements = true;
            captureInnerContent = true;
        }

        public void TagEndProcess(TemplateManager manager, Tag tag, string innerContent)
        {
            Expression exp;
            string _channelid, _channelname;
            exp = tag.AttributeValue("channelid");
            if (exp == null)
                throw new Exception("没有channelid标签");
            _channelid = manager.EvalExpression(exp).ToString();
            _channelname = (new JumboTCMS.DAL.Normal_ChannelDAL().GetChannelName(_channelid));
            manager.WriteValue(_channelname);
        }
    }
    /// <summary>
    /// 获得频道地址
    /// </summary>
    public class TemplateTag_GetChannelLink : ITagHandler
    {
        public void TagBeginProcess(TemplateManager manager, Tag tag, ref bool processInnerElements, ref bool captureInnerContent)
        {
            processInnerElements = true;
            captureInnerContent = true;
        }

        public void TagEndProcess(TemplateManager manager, Tag tag, string innerContent)
        {
            Expression exp;
            string _channelid, _channelishtml, _channellink;
            exp = tag.AttributeValue("channelid");
            if (exp == null)
                throw new Exception("没有channelid标签");
            _channelid = manager.EvalExpression(exp).ToString();
            exp = tag.AttributeValue("channelishtml");
            if (exp == null)
                _channelishtml = "0";
            _channelishtml = manager.EvalExpression(exp).ToString();
            _channellink = (new Normal_ChannelDAL()).GetChannelLink(_channelishtml == "1", _channelid, false);
            manager.WriteValue(_channellink);
        }
    }
    /// <summary>
    /// 获得栏目名称
    /// </summary>
    public class TemplateTag_GetClassName : ITagHandler
    {
        public void TagBeginProcess(TemplateManager manager, Tag tag, ref bool processInnerElements, ref bool captureInnerContent)
        {
            processInnerElements = true;
            captureInnerContent = true;
        }

        public void TagEndProcess(TemplateManager manager, Tag tag, string innerContent)
        {
            Expression exp;
            string _classid, _classname;
            exp = tag.AttributeValue("classid");
            if (exp == null)
                throw new Exception("没有classid标签");
            _classid = manager.EvalExpression(exp).ToString();
            _classname = (new JumboTCMS.DAL.Normal_ClassDAL().GetClassName(_classid));
            manager.WriteValue(_classname);
        }
    }
    /// <summary>
    /// 获得栏目地址
    /// </summary>
    public class TemplateTag_GetClassLink : ITagHandler
    {
        public void TagBeginProcess(TemplateManager manager, Tag tag, ref bool processInnerElements, ref bool captureInnerContent)
        {
            processInnerElements = true;
            captureInnerContent = true;
        }

        public void TagEndProcess(TemplateManager manager, Tag tag, string innerContent)
        {
            Expression exp;
            string _channelid, _channelishtml, _classid, _classlink;
            exp = tag.AttributeValue("channelid");
            if (exp == null)
                throw new Exception("没有channelid标签");
            _channelid = manager.EvalExpression(exp).ToString();

            exp = tag.AttributeValue("channelishtml");
            if (exp == null)
                _channelishtml = "0";
            _channelishtml = manager.EvalExpression(exp).ToString();

            exp = tag.AttributeValue("classid");
            if (exp == null)
                throw new Exception("没有classid标签");
            _classid = manager.EvalExpression(exp).ToString();
            _classlink = (new Normal_ClassDAL()).GetClassLink(1, _channelishtml == "1", _channelid, _classid, false);
            manager.WriteValue(_classlink);
        }
    }
    /// <summary>
    /// 获得内容缩略图
    /// </summary>
    public class TemplateTag_GetImgurl : ITagHandler
    {
        public void TagBeginProcess(TemplateManager manager, Tag tag, ref bool processInnerElements, ref bool captureInnerContent)
        {
            processInnerElements = true;
            captureInnerContent = true;
        }

        public void TagEndProcess(TemplateManager manager, Tag tag, string innerContent)
        {
            Expression exp;
            string _sitedir, _isimg, _img, _imgurl;
            exp = tag.AttributeValue("sitedir");
            if (exp == null)
                throw new Exception("没有sitedir标签");
            _sitedir = manager.EvalExpression(exp).ToString();
            exp = tag.AttributeValue("isimg");
            if (exp == null)
                _isimg = "0";
            else
                _isimg = manager.EvalExpression(exp).ToString();
            exp = tag.AttributeValue("img");
            if (exp == null)
                _img = "";
            else
                _img = manager.EvalExpression(exp).ToString();
            if (_isimg == "0" || _img.Length == 0)
                _imgurl = _sitedir + "style/common/nophoto.jpg";
            else
                _imgurl = _img;
            manager.WriteValue(_imgurl);
        }
    }
    /// <summary>
    /// 获得截断后的字符串
    /// </summary>
    public class TemplateTag_GetCutstring : ITagHandler
    {
        public void TagBeginProcess(TemplateManager manager, Tag tag, ref bool processInnerElements, ref bool captureInnerContent)
        {
            processInnerElements = true;
            captureInnerContent = true;
        }

        public void TagEndProcess(TemplateManager manager, Tag tag, string innerContent)
        {
            Expression exp;
            string _len, _cutstring;
            exp = tag.AttributeValue("len");
            if (exp == null)
                throw new Exception("没有len标签");
            _len = manager.EvalExpression(exp).ToString();
            _cutstring = JumboTCMS.Utils.Strings.CutString(JumboTCMS.Utils.Strings.NoHTML(innerContent), Convert.ToInt32(_len));
            manager.WriteValue(_cutstring);
        }
    }
    /// <summary>
    /// 获得点击率
    /// </summary>
    public class TemplateTag_GetViewnum : ITagHandler
    {
        public void TagBeginProcess(TemplateManager manager, Tag tag, ref bool processInnerElements, ref bool captureInnerContent)
        {
            processInnerElements = true;
            captureInnerContent = true;
        }

        public void TagEndProcess(TemplateManager manager, Tag tag, string innerContent)
        {
            Expression exp;
            string _sitedir, _channelid, _channeltype, _contentid, _viewnum;
            exp = tag.AttributeValue("sitedir");
            if (exp == null)
                throw new Exception("没有sitedir标签");
            _sitedir = manager.EvalExpression(exp).ToString();
            exp = tag.AttributeValue("channelid");
            if (exp == null)
                throw new Exception("没有channelid标签");
            _channelid = manager.EvalExpression(exp).ToString();
            exp = tag.AttributeValue("channeltype");
            if (exp == null)
                throw new Exception("没有channeltype标签");
            _channeltype = manager.EvalExpression(exp).ToString();
            exp = tag.AttributeValue("contentid");
            if (exp == null)
                throw new Exception("没有contentid标签");
            _contentid = manager.EvalExpression(exp).ToString();
            _viewnum = "<script src=\"" + _sitedir + "plus/viewcount.aspx?ccid=" + _channelid + "&cType=" + _channeltype + "&id=" + _contentid + "&addit=0\"></script>";
            manager.WriteValue(_viewnum);
        }
    }
}