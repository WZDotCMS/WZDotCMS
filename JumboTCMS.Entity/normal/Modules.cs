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
namespace JumboTCMS.Entity
{
    /// <summary>
    /// 模型-------表映射实体
    /// </summary>

    public class Normal_Modules
    {
        public Normal_Modules()
        { }

        private string _id;
        private string _title;
        private string _type;
        private int _pid;
        private int _enabled;
        private int _locked;
        private string _searchfieldvalues;
        private string _searchfieldtexts;
        /// <summary>
        /// 
        /// </summary>
        public string Id
        {
            set { _id = value; }
            get { return _id; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string Title
        {
            set { _title = value; }
            get { return _title; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string Type
        {
            set { _type = value; }
            get { return _type; }
        }
        /// <summary>
        /// 
        /// </summary>
        public int PId
        {
            set { _pid = value; }
            get { return _pid; }
        }
        /// <summary>
        /// 
        /// </summary>
        public int Enabled
        {
            set { _enabled = value; }
            get { return _enabled; }
        }
        /// <summary>
        /// 
        /// </summary>
        public int Locked
        {
            set { _locked = value; }
            get { return _locked; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string SearchFieldValues
        {
            set { _searchfieldvalues = value; }
            get { return _searchfieldvalues; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string SearchFieldTexts
        {
            set { _searchfieldtexts = value; }
            get { return _searchfieldtexts; }
        }


    }
}

