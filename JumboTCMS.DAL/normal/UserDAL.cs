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
using System.Web;
using JumboTCMS.Utils;
using JumboTCMS.DBUtility;

namespace JumboTCMS.DAL
{
    /// <summary>
    /// 标签表信息
    /// </summary>
    public class Normal_UserDAL : Common
    {
        public Normal_UserDAL()
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
            int _ext = 0;
            using (DbOperHandler _doh = new Common().Doh())
            {
                _doh.Reset();
                _doh.ConditionExpress = _wherestr;
                if (_doh.Exist("jcms_normal_user"))
                    _ext = 1;
            }
            return (_ext == 1);
        }
        public DataTable GetUserList(int _thispage, int _pagesize, string _wherestr)
        {
            using (DbOperHandler _doh = new Common().Doh())
            {
                _doh.Reset();
                _doh.ConditionExpress = _wherestr;
                string sqlStr = "";
                int _countnum = _doh.Count("jcms_normal_user");
                sqlStr = JumboTCMS.Utils.SqlHelp.GetSql("[ID],[UserName],[GUID]", "jcms_normal_user", "Id", _pagesize, _thispage, "desc", _wherestr);
                _doh.Reset();
                _doh.SqlCmd = sqlStr;
                DataTable dt = _doh.GetDataTable();
                return dt;
            }
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
                int _del = _doh.Delete("jcms_normal_user");
                return (_del == 1);
            }

        }

        /// <summary>
        /// 获得单页内容的单条记录实体
        /// </summary>
        /// <param name="_id"></param>
        public JumboTCMS.Entity.Normal_User GetEntity(string _id)
        {
            JumboTCMS.Entity.Normal_User user = new JumboTCMS.Entity.Normal_User();
            using (DbOperHandler _doh = new Common().Doh())
            {
                _doh.Reset();
                _doh.SqlCmd = "SELECT * FROM [jcms_normal_user] WHERE [Id]=" + _id;
                DataTable dt = _doh.GetDataTable();
                if (dt.Rows.Count > 0)
                {
                    user.Id = dt.Rows[0]["Id"].ToString();
                    user.AdminId = Validator.StrToInt(dt.Rows[0]["AdminId"].ToString(), 0);
                    user.UserName = dt.Rows[0]["UserName"].ToString();
                    user.NickName = dt.Rows[0]["NickName"].ToString();
                    user.Signature = dt.Rows[0]["Signature"].ToString();
                    user.AdminName = dt.Rows[0]["AdminName"].ToString();
                    user.ForumName = dt.Rows[0]["ForumName"].ToString();
                    user.UserPass = dt.Rows[0]["UserPass"].ToString();
                    user.Question = dt.Rows[0]["Question"].ToString();
                    user.Answer = dt.Rows[0]["Answer"].ToString();
                    user.Sex = Validator.StrToInt(dt.Rows[0]["Sex"].ToString(), 0);
                    user.Email = dt.Rows[0]["Email"].ToString();
                    user.Group = Validator.StrToInt(dt.Rows[0]["Group"].ToString(), 0);
                    user.State = Validator.StrToInt(dt.Rows[0]["State"].ToString(), 0);
                    user.Cookies = dt.Rows[0]["Cookies"].ToString();
                    user.RegTime = Validator.StrToDate(dt.Rows[0]["RegTime"].ToString(), DateTime.Now);
                    user.IsVIP = Validator.StrToInt(dt.Rows[0]["IsVIP"].ToString(), 0);
                    user.VIPDate = Validator.StrToDate(dt.Rows[0]["VIPTime"].ToString(), DateTime.Now.AddDays(-1)).ToShortDateString();
                    if (JumboTCMS.Utils.Validator.ValidDate(user.VIPDate))//说明已经过期,再判断一次
                        user.IsVIP = 0;
                    user.RegIp = dt.Rows[0]["RegIp"].ToString();
                    user.LastTime = Validator.StrToDate(dt.Rows[0]["LastTime"].ToString(), DateTime.Now);
                    user.LastIP = dt.Rows[0]["LastIP"].ToString();
                    user.HomePage = dt.Rows[0]["HomePage"].ToString();
                    user.TrueName = dt.Rows[0]["TrueName"].ToString();
                    user.IDType = Validator.StrToInt(dt.Rows[0]["IDType"].ToString(), 0);
                    user.IDCard = dt.Rows[0]["IDCard"].ToString();
                    user.QQ = dt.Rows[0]["QQ"].ToString();
                    user.ICQ = dt.Rows[0]["ICQ"].ToString();
                    user.MSN = dt.Rows[0]["MSN"].ToString();
                    user.BirthDay = dt.Rows[0]["BirthDay"].ToString();
                    user.ProvinceCity = dt.Rows[0]["ProvinceCity"].ToString();
                    user.WorkUnit = dt.Rows[0]["WorkUnit"].ToString();
                    user.Address = dt.Rows[0]["Address"].ToString();
                    user.ZipCode = dt.Rows[0]["ZipCode"].ToString();
                    user.Login = Validator.StrToInt(dt.Rows[0]["Login"].ToString(), 0);
                    user.Points = Validator.StrToInt(dt.Rows[0]["Points"].ToString(), 0);
                    user.Integral = Validator.StrToInt(dt.Rows[0]["Integral"].ToString(), 0);
                    user.MobileTel = dt.Rows[0]["MobileTel"].ToString();
                    user.Telephone = dt.Rows[0]["Telephone"].ToString();
                }
                return user;
            }

        }
        /// <summary>
        /// 插入GUID
        /// </summary>
        /// <param name="_id"></param>
        public string InsertGUID(string _id)
        {
            using (DbOperHandler _doh = new Common().Doh())
            {
                string _guid = "";
                _doh.Reset();
                _doh.SqlCmd = "SELECT [GUID] FROM [jcms_normal_user] WHERE [Id]=" + _id;
                DataTable dt = _doh.GetDataTable();
                if (dt.Rows.Count > 0)
                {
                    _guid = dt.Rows[0][0].ToString();
                }
                if (_guid.Length != 36)
                {
                    _guid = Guid.NewGuid().ToString();
                    _doh.ConditionExpress = "id=" + _id;
                    _doh.AddFieldItem("guid", _guid);
                    _doh.Update("jcms_normal_user");
                }
                return _guid;
            }

        }
        /// <summary>
        /// 获得用户名
        /// </summary>
        /// <param name="_id"></param>
        /// <returns></returns>
        public string GetUserName(string _id)
        {
            using (DbOperHandler _doh = new Common().Doh())
            {
                _doh.Reset();
                _doh.SqlCmd = "SELECT [UserName] FROM [jcms_normal_user] WHERE [Id]=" + _id;
                DataTable dt = _doh.GetDataTable();
                if (dt.Rows.Count > 0)
                {
                    return dt.Rows[0]["UserName"].ToString();
                }
                return string.Empty;
            }

        }
        /// <summary>
        /// 实时判断会员是不是VIP
        /// </summary>
        /// <param name="_id"></param>
        /// <param name="doh"></param>
        /// <returns></returns>
        public bool IsVIPUser(string _id)
        {
            using (DbOperHandler _doh = new Common().Doh())
            {
                int _isvip = 0;
                string _vipdate = "2000-1-1";
                _doh.Reset();
                _doh.SqlCmd = "SELECT IsVIP,VIPTime FROM [jcms_normal_user] WHERE [Id]=" + _id;
                DataTable dt = _doh.GetDataTable();
                if (dt.Rows.Count > 0)
                {
                    DataRow dr = dt.Rows[0];
                    _isvip = Validator.StrToInt(dr["IsVIP"].ToString(), 0);
                    _vipdate = Validator.StrToDate(dr["VIPTime"].ToString(), DateTime.Now.AddDays(-1)).ToShortDateString();
                    if (JumboTCMS.Utils.Validator.ValidDate(_vipdate))//说明已经过期
                        _isvip = 0;
                }
                return (_isvip == 1);
            }

        }
        /// <summary>
        /// 修改密码
        /// </summary>
        /// <param name="_id">用户ID</param>
        /// <param name="_pass">修改后的密码</param>
        public void ChangePsd(string _id, string _pass)
        {
            using (DbOperHandler _doh = new Common().Doh())
            {
                _doh.Reset();
                _doh.ConditionExpress = "id=" + _id;
                _doh.AddFieldItem("UserPass", _pass);
                _doh.Update("jcms_normal_user");
            }

        }
        /// <summary>
        /// 加博币
        /// </summary>
        /// <param name="_id">用户ID</param>
        /// <param name="_points">博币</param>
        public void AddPoints(string _id, int _points)
        {
            using (DbOperHandler _doh = new Common().Doh())
            {
                _doh.Reset();
                _doh.ConditionExpress = "id=" + _id;
                _doh.Add("jcms_normal_user", "Points", _points);
            }

        }
        /// <summary>
        /// 扣博币
        /// </summary>
        /// <param name="_id">用户ID</param>
        /// <param name="_points">博币</param>
        public void DeductPoints(string _id, int _points)
        {
            using (DbOperHandler _doh = new Common().Doh())
            {
                _doh.Reset();
                _doh.ConditionExpress = "id=" + _id;
                _doh.Deduct("jcms_normal_user", "Points", _points);
            }

        }
        /// <summary>
        /// 扣除积分
        /// </summary>
        /// <param name="_id">用户ID</param>
        /// <param name="_integral">扣的积分</param>
        public void DeductIntegral(string _id, int _integral)
        {
            using (DbOperHandler _doh = new Common().Doh())
            {
                _doh.Reset();
                _doh.ConditionExpress = "id=" + _id;
                _doh.Deduct("jcms_normal_user", "Integral", _integral);
            }

        }
        /// <summary>
        /// 续费VIP
        /// </summary>
        /// <param name="_id">用户ID</param>
        /// <param name="_vipyears">续的年数</param>
        public void AddVIPYears(string _id, int _vipyears)
        {
            using (DbOperHandler _doh = new Common().Doh())
            {
                DateTime LimitDate = DateTime.Now;
                _doh.Reset();
                _doh.ConditionExpress = "id=" + _id;
                object[] values = _doh.GetFields("jcms_normal_user", "IsVIP,VIPTime");
                bool _isvip = (JumboTCMS.Utils.Validator.StrToInt(values[0].ToString(), 0) == 1);
                if (!_isvip)//如果还不是VIP
                    LimitDate = DateTime.Now.AddYears(_vipyears);
                else
                {
                    if (JumboTCMS.Utils.Validator.ValidDate(values[1].ToString()))//如果已经过期
                        LimitDate = DateTime.Now.AddYears(_vipyears);
                    else
                        LimitDate = DateTime.Parse(values[1].ToString()).AddYears(_vipyears);
                }
                _doh.Reset();
                _doh.ConditionExpress = "id=" + _id;
                _doh.AddFieldItem("IsVIP", 1);
                _doh.AddFieldItem("VIPTime", LimitDate);
                _doh.Update("jcms_normal_user");
            }

        }
        /// <summary>
        /// 会员注册
        /// </summary>
        /// <param name="_username">用户名</param>
        /// <param name="_userpass">密码</param>
        /// <param name="isMD5Passwd">是否已加密</param>
        /// <param name="_sex">性别</param>
        /// <param name="_email">邮箱</param>
        /// <param name="_birthday">生日</param>
        /// <param name="_usersign">验证字符串</param>
        /// <returns></returns>
        public int Register(string _username, string _nickname, string _userpass, bool isMD5Passwd, int _sex, string _email, string _birthday, string _usersign)
        {
            return Register(_username, _nickname, _userpass, isMD5Passwd, _sex, _email, _birthday, _usersign, "", "", "", "");
        }
        public int Register(string _username, string _nickname, string _userpass, bool isMD5Passwd, int _sex, string _email, string _birthday, string _usersign, string _adminname, string _adminpass, string _oauth_code, string _oauth_token)
        {
            if (_oauth_code == "") _oauth_code = "tencent";
            if (Exists(string.Format("username='{0}'", _username)))
                return 0;
            using (DbOperHandler _doh = new Common().Doh())
            {
                string _md5pass = isMD5Passwd ? _userpass : JumboTCMS.Utils.MD5.Lower32(_userpass);
                string _md5pass2 = isMD5Passwd ? _adminpass : JumboTCMS.Utils.MD5.Lower32(_adminpass);
                int dPoints = Str2Int(JumboTCMS.Utils.XmlCOM.ReadConfig("~/_data/config/site", "DefaultPoints"), 0);
                int uState = site.CheckReg ? 0 : 1;
                object[,] addFields = new object[2, 19] {
                        {
                            "UserName", "NickName", "UserPass", "Sex", "Email", "Birthday", 
                            "Group", "Points", "Login", "State", "AdminId", "Setting", "UserSign", 
                            "AdminState", "IsVIP", "Integral","RegTime", "RegIp","Token_"+_oauth_code}, 
                        {
                            _username, _nickname, _md5pass, _sex,_email, _birthday, 
                            1, dPoints, 0, uState,0, ",,", _usersign, 
                            0,0, 0, DateTime.Now.ToString(),IPHelp.ClientIP,_oauth_token} 
                        };
                _doh.Reset();
                _doh.AddFieldItems(addFields);
                int _uID = _doh.Insert("jcms_normal_user");
                #region 复制头像
                JumboTCMS.Utils.DirFile.CopyFile("~/_data/avatar/0_l.jpg", "~/_data/avatar/" + _uID + "_l.jpg", true);
                JumboTCMS.Utils.DirFile.CopyFile("~/_data/avatar/0_m.jpg", "~/_data/avatar/" + _uID + "_m.jpg", true);
                JumboTCMS.Utils.DirFile.CopyFile("~/_data/avatar/0_s.jpg", "~/_data/avatar/" + _uID + "_s.jpg", true);
                #endregion
                #region 同步升级为管理员
                if (_adminname.Length > 0 && _adminpass.Length > 0)
                {
                    _doh.Reset();
                    _doh.ConditionExpress = "id=" + _uID;
                    _doh.AddFieldItem("State", 1);
                    _doh.AddFieldItem("AdminState", 1);
                    _doh.AddFieldItem("AdminId", _uID);
                    _doh.AddFieldItem("AdminName", _adminname);
                    _doh.AddFieldItem("AdminPass", _md5pass2);
                    _doh.AddFieldItem("Group", site.AdminGroupId);
                    _doh.Update("jcms_normal_user");
                    _doh.Reset();
                    _doh.ConditionExpress = "id=" + site.AdminGroupId;
                    _doh.Add("jcms_normal_usergroup", "UserTotal");
                }
                else
                {
                    _doh.Reset();
                    _doh.ConditionExpress = "id=1";
                    _doh.Add("jcms_normal_usergroup", "UserTotal");
                }
                #endregion
                #region 论坛同步注册
                if (site.ForumAPIKey != "")
                {
                    string _ForumAutoRegister = JumboTCMS.Utils.XmlCOM.ReadConfig("~/_data/config/site", "ForumAutoRegister");
                    if (_ForumAutoRegister == "true")//表示自动注册论坛用户
                    {
                        JumboTCMS.API.Discuz.Toolkit.DiscuzSession ds = JumboTCMS.API.Discuz.DiscuzSessionHelper.GetSession();
                        int _userid = ds.Register(_username, _userpass, _email, isMD5Passwd);
                        if (_userid > 0)
                        {
                            //注册成功
                            _doh.Reset();
                            _doh.ConditionExpress = "id=" + _uID;
                            _doh.AddFieldItem("ForumName", _username);
                            _doh.AddFieldItem("ForumPass", _md5pass);
                            _doh.Update("jcms_normal_user");
                            return _uID;
                        }
                        return 0;
                    }
                    return _uID;
                }
                #endregion
                return _uID;
            }
        }
        /// <summary>
        /// 修改指定用户的密码
        /// </summary>
        /// <param name="_userid"></param>
        /// <param name="_originalPassword">原始密码</param>
        /// <param name="_newPassword">新密码</param>
        /// <returns></returns>
        public bool ChangeUserPassword(string _userid, string _originalPassword, string _newPassword)
        {
            using (DbOperHandler _doh = new Common().Doh())
            {
                _doh.Reset();
                _doh.ConditionExpress = "id=@id and state=1";
                _doh.AddConditionParameter("@id", _userid);
                object pass = _doh.GetField("jcms_normal_user", "UserPass");
                if (pass != null)//用户存在
                {
                    if (pass.ToString().ToLower() == JumboTCMS.Utils.MD5.Lower32(_originalPassword) || pass.ToString().ToLower() == JumboTCMS.Utils.MD5.Lower16(_originalPassword)) //验证旧密码
                    {
                        _doh.Reset();
                        _doh.ConditionExpress = "id=@id and state=1";
                        _doh.AddConditionParameter("@id", _userid);
                        _doh.AddFieldItem("UserPass", JumboTCMS.Utils.MD5.Lower32(_newPassword));
                        _doh.Update("jcms_normal_user");
                        return true;
                    }
                    else
                        return false;
                }
                else
                    return false;
            }

        }
        /// <summary>
        /// 会员登录
        /// </summary>
        /// <param name="_username">登录名</param>
        /// <param name="_userpass">密码(明文)</param>
        /// <param name="iExpires">保存信息的天数</param>
        /// <returns></returns>
        public string ChkUserLogin(string _username, string _userpass, int iExpires)
        {
            return ChkUserLogin(_username, _userpass, iExpires, false);
        }
        /// <summary>
        /// 会员登录
        /// </summary>
        /// <param name="_username"></param>
        /// <param name="_userpass"></param>
        /// <param name="iExpires"></param>
        /// <param name="isMD5Passwd"></param>
        /// <returns></returns>
        public string ChkUserLogin(string _username, string _userpass, int iExpires, bool isMD5Passwd)
        {
            if (!isMD5Passwd)
                _userpass = JumboTCMS.Utils.MD5.Lower32(_userpass);
            using (DbOperHandler _doh = new Common().Doh())
            {
                _username = _username.Replace("\'", "");
                _doh.Reset();
                _doh.ConditionExpress = "username=@username";
                _doh.AddConditionParameter("@username", _username);
                string _userid = _doh.GetField("jcms_normal_user", "id").ToString();
                if (_userid != "")
                {
                    JumboTCMS.Entity.Normal_User _User = new JumboTCMS.DAL.Normal_UserDAL().GetEntity(_userid);
                    if (_User.UserPass != _userpass)
                    {
                        return "密码错误";
                    }
                    if (_User.State != 1)
                    {
                        return "帐号被锁定";
                    }
                    _doh.Reset();
                    _doh.SqlCmd = "SELECT [id],[GroupName],[IsLogin],[Setting] FROM [jcms_normal_usergroup] WHERE [Id]=" + _User.Group;
                    DataTable dtUserGroup = _doh.GetDataTable();
                    if (dtUserGroup.Rows.Count == 0)
                    {
                        return "用户组有误";
                    }
                    if (dtUserGroup.Rows[0]["IsLogin"].ToString() != "1")
                    {
                        return "帐号禁止登录";
                    }
                    string _userGroupid = dtUserGroup.Rows[0]["Id"].ToString();
                    string _userGroupname = dtUserGroup.Rows[0]["GroupName"].ToString();
                    string _userSetting = dtUserGroup.Rows[0]["Setting"].ToString();
                    dtUserGroup.Clear();
                    dtUserGroup.Dispose();
                    string _userCookies = "c" + (new Random().Next(10000000, 99999999)).ToString();
                    //设置Cookies
                    System.Collections.Specialized.NameValueCollection myCol = new System.Collections.Specialized.NameValueCollection();
                    myCol.Add("id", _userid);
                    myCol.Add("name", _User.UserName);
                    myCol.Add("nickname", _User.NickName);
                    myCol.Add("password", _User.UserPass);
                    myCol.Add("email", _User.Email);
                    myCol.Add("groupid", _userGroupid);
                    myCol.Add("groupname", _userGroupname);
                    myCol.Add("setting", _userSetting);
                    myCol.Add("cookies", _userCookies);
                    JumboTCMS.Utils.Cookie.SetObj(site.CookiePrev + "user", 60 * 60 * 24 * iExpires, myCol, site.CookieDomain, site.CookiePath);

                    //更新User登陆信息
                    _doh.Reset();
                    _doh.ConditionExpress = "id=@id and state=1";
                    _doh.AddConditionParameter("@id", _userid);
                    _doh.AddFieldItem("Cookies", _userCookies);
                    _doh.AddFieldItem("LastTime", DateTime.Now.ToString());
                    _doh.AddFieldItem("LastIP", IPHelp.ClientIP);
                    _doh.AddFieldItem("UserSign", "");
                    _doh.Update("jcms_normal_user");
                    if (site.ForumAPIKey != "")
                    {
                        bool _AutoLogining = false;
                        string _ForumAutoRegister = JumboTCMS.Utils.XmlCOM.ReadConfig("~/_data/config/site", "ForumAutoRegister");
                        if (_ForumAutoRegister == "true")//表示自动注册论坛用户
                            _AutoLogining = true;
                        if (_AutoLogining == true)
                        {
                            //登陆社区
                            _doh.Reset();
                            _doh.ConditionExpress = "id=@id and state=1";
                            _doh.AddConditionParameter("@id", _userid);
                            object[] _forumInfo = _doh.GetFields("jcms_normal_user", "ForumName,ForumPass");
                            if (_forumInfo[0].ToString().Length > 0 && _forumInfo[1].ToString().Length > 0)
                            {
                                JumboTCMS.API.Discuz.Toolkit.DiscuzSession ds = JumboTCMS.API.Discuz.DiscuzSessionHelper.GetSession();
                                ds.Login(ds.GetUserID(_forumInfo[0].ToString()), _forumInfo[1].ToString(), true, iExpires, site.CookieDomain);
                            }
                        }
                    }
                    return "ok";
                }
                else
                {
                    return "帐号不存在";
                }

            }

        }
        /// <summary>
        /// 会员注销
        /// </summary>
        /// <param name="_userkey"></param>
        /// <returns></returns>
        public bool ChkUserLogout(string _userkey)
        {
            //ChkAdminLogout();
            if (JumboTCMS.Utils.Cookie.GetValue(site.CookiePrev + "user") != null)
            {
                if (JumboTCMS.Utils.Cookie.GetValue(site.CookiePrev + "user", "password").Substring(4, 8) == _userkey)
                {
                    JumboTCMS.Utils.Cookie.Del(site.CookiePrev + "user", site.CookieDomain, site.CookiePath);
                    if (site.ForumAPIKey != "")
                    {
                        //退出社区
                        JumboTCMS.API.Discuz.Toolkit.DiscuzSession ds = JumboTCMS.API.Discuz.DiscuzSessionHelper.GetSession();
                        ds.Logout(site.CookieDomain);
                    }
                    return true;
                }
                else
                    return false;
            }
            return false;
        }
        /// <summary>
        /// 更新客服列表
        /// </summary>
        public void RefreshServiceList()
        {
            string _serviceids = "";
            string _servicenames = "";
            string _servicemails = "";
            using (DbOperHandler _doh = new Common().Doh())
            {
                _doh.Reset();
                _doh.SqlCmd = "SELECT [ServiceId],[ServiceName],[Email] FROM [jcms_normal_user] WHERE [ServiceId]>0";
                DataTable dt = _doh.GetDataTable();
                for (int j = 0; j < dt.Rows.Count; j++)
                {
                    if (j == 0)
                    {
                        _serviceids = dt.Rows[j]["ServiceId"].ToString();
                        _servicenames = dt.Rows[j]["ServiceName"].ToString();
                        _servicemails = dt.Rows[j]["Email"].ToString();
                    }
                    else
                    {
                        _serviceids += "," + dt.Rows[j]["ServiceId"].ToString();
                        _servicenames += "," + dt.Rows[j]["ServiceName"].ToString();
                        _servicemails += "," + dt.Rows[j]["Email"].ToString();
                    }
                }
                string strXmlFile = HttpContext.Current.Server.MapPath("~/_data/config/message.config");
                JumboTCMS.DBUtility.XmlControl XmlTool = new JumboTCMS.DBUtility.XmlControl(strXmlFile);
                XmlTool.Update("Messages/Service/UserId", _serviceids);
                XmlTool.Update("Messages/Service/UserName", _servicenames);
                XmlTool.Update("Messages/Service/UserMail", _servicemails);
                XmlTool.Save();
                XmlTool.Dispose();
            }
        }
        /// <summary>
        /// 插入GUID
        /// </summary>
        /// <param name="_id"></param>
        public string InsertGUID(string _id, DbOperHandler doh)
        {
            using (DbOperHandler _doh = new Common().Doh())
            {
                string _guid = "";
                _doh.Reset();
                _doh.SqlCmd = "SELECT [GUID] FROM [jcms_normal_user] WHERE [Id]=" + _id;
                DataTable dt = _doh.GetDataTable();
                if (dt.Rows.Count > 0)
                {
                    _guid = dt.Rows[0][0].ToString();
                }
                if (_guid.Length != 36)
                {
                    _guid = Guid.NewGuid().ToString();
                    _doh.ConditionExpress = "id=" + _id;
                    _doh.AddFieldItem("guid", _guid);
                    _doh.Update("jcms_normal_user");
                }
                return _guid;
            }
        }
        /// <summary>
        /// 获得所有客服
        /// </summary>
        /// <param name="doh"></param>
        /// <returns></returns>
        public DataTable GetServiceList()
        {
            using (DbOperHandler _doh = new Common().Doh())
            {
                _doh.Reset();
                _doh.SqlCmd = "SELECT [ServiceId],[GUID],[ServiceName],[Email] FROM [jcms_normal_user] WHERE [ServiceId]>0";
                DataTable dt = _doh.GetDataTable();
                return dt;
            }

        }
        /// <summary>
        /// 实时判断会员是不是客服
        /// </summary>
        /// <param name="_id"></param>
        /// <param name="doh"></param>
        /// <param name="_servicename"></param>
        /// <returns></returns>
        public bool Service(string _id, DbOperHandler doh, ref string _servicename)
        {
            using (DbOperHandler _doh = new Common().Doh())
            {
                _doh.Reset();
                _doh.SqlCmd = "SELECT [ServiceName] FROM [jcms_normal_user] WHERE [ServiceId]>0 AND [Id]=" + _id;
                DataTable dt = _doh.GetDataTable();
                //_doh.Dispose();
                if (dt.Rows.Count > 0)
                {
                    _servicename = dt.Rows[0]["ServiceName"].ToString();
                    return true;
                }
                else
                    return false;
            }
        }
        /// <summary>
        /// 判断usersign是否正确
        /// </summary>
        /// <param name="_userid"></param>
        /// <param name="_usersign">长度一定是32位</param>
        /// <returns></returns>
        public bool ChkUserSign(string _userid, string _usersign)
        {
            if (_usersign.Length != 32 || _userid == "")
            {
                return false;
            }
            using (DbOperHandler _doh = new Common().Doh())
            {
                _doh.Reset();
                _doh.ConditionExpress = "id=@userid and usersign=@usersign and state=1";
                _doh.AddConditionParameter("@userid", Str2Int(_userid));
                _doh.AddConditionParameter("@usersign", _usersign);
                if (_doh.Exist("jcms_normal_user"))
                    return true;
                else
                    return false;
            }
        }
    }
}
