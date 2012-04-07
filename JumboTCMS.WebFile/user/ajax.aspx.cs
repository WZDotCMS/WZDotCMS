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
using System.Web;
using System.Data;
using JumboTCMS.Utils;
using JumboTCMS.Common;
namespace JumboTCMS.WebFile.User
{
    public partial class _ajax : JumboTCMS.UI.UserCenter
    {
        private string _operType = string.Empty;
        private string _response = string.Empty;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!CheckFormUrl())
            {
                //Response.End();
            }

            this._operType = q("oper");
            switch (this._operType)
            {
                #region 头像设置
                case "uploadfile":
                    UploadFile();
                    break;
                case "uploadcutfile":
                    UploadCutFile();
                    break;
                case "uploadmedia":
                    UploadMedia();
                    break;
                #endregion
                case "ajaxChangePassword":
                    ajaxChangePassword();
                    break;
                case "ajaxChangeProfile":
                    ajaxChangeProfile();
                    break;
                case "ajaxChangeForumInfo":
                    ajaxChangeForumInfo();
                    break;
                case "ajaxGetMessageList":
                    ajaxGetMessageList();
                    break;
                case "ajaxSendMessage":
                    ajaxSendMessage();
                    break;
                case "ajaxReplyMessage":
                    ajaxReplyMessage();
                    break;
                case "ajaxCheckSendMessage":
                    ajaxCheckSendMessage();
                    break;
                case "ajaxDelMessage":
                    ajaxDelMessage();
                    break;
                case "ajaxGetNoticeList":
                    ajaxGetNoticeList();
                    break;
                case "ajaxGetFavoriteList":
                    ajaxGetFavoriteList();
                    break;
                case "ajaxDelFavorite":
                    ajaxDelFavorite();
                    break;
                case "ajaxAddFriend":
                    ajaxAddFriend();
                    break;
                case "ajaxAddFriend2":
                    ajaxAddFriend2();
                    break;
                case "ajaxCheckAddFriend":
                    ajaxCheckAddFriend();
                    break;
                case "ajaxDelFriend":
                    ajaxDelFriend();
                    break;
                case "ajaxGetFriendList":
                    ajaxGetFriendList();
                    break;
                case "ajaxGetConsumeList":
                    ajaxGetConsumeList();
                    break;
                case "ajaxGetOrderList":
                    ajaxGetOrderList();
                    break;
                case "ajaxGetGoodsList":
                    ajaxGetGoodsList();
                    break;

                case "ajaxDeleteOrder":
                    ajaxDeleteOrder();
                    break;
                case "ajaxPayOrder":
                    ajaxPayOrder();
                    break;

                case "ajaxFinishOrder":
                    ajaxFinishOrder();
                    break;
                case "ajaxGetCartList":
                    ajaxGetCartList();
                    break;
                case "ajaxSetCart2Order":
                    ajaxSetCart2Order();
                    break;
                case "ajaxDelCart":
                    ajaxDelCart();
                    break;
                case "ajaxSetBuyCount":
                    ajaxSetBuyCount();
                    break;
                case "ajaxCard2Points":
                    ajaxCard2Points();
                    break;
                case "ajaxRemoveOAuth":
                    ajaxRemoveOAuth();
                    break;
                default:
                    DefaultResponse();
                    break;
            }
            Response.Write(this._response);
        }
        private void DefaultResponse()
        {
            User_Load("", "json");
        }
        #region 头像设置
        /// <summary>
        /// 上传原始图片
        /// </summary>
        private void UploadFile()
        {
            if (!(new JumboTCMS.DAL.Normal_UserDAL()).ChkUserSign(q("userid"), q("usersign")))
            {
                Response.Write("验证信息有误");
                Response.End();
            }
            string _sUserUploadType = "*.jpg;*.bmp;*.gif;*.png;";
            int _sUserUploadSize = 2048;
            if (this.Page.Request.Files.Count > 0)
            {
                HttpPostedFile oFile = this.Page.Request.Files[0];//得到要上传文件

                if (oFile != null && oFile.ContentLength > 0)
                {
                    try
                    {
                        string fileExtension = System.IO.Path.GetExtension(oFile.FileName).ToLower(); //上传文件的扩展名
                        if (_sUserUploadType.ToLower().Contains("*" + fileExtension + ";"))//检测是否为允许的上传文件类型
                        {
                            if (_sUserUploadSize * 1024 >= oFile.ContentLength)//检测文件大小是否超过限制
                            {
                                string FullPath = "~/_data/tempfiles/user_" + q("userid") + "_avatar.jpg";
                                oFile.SaveAs(Server.MapPath(FullPath));
                                if (JumboTCMS.Utils.FileValidation.IsSecureUpfilePhoto(Server.MapPath(FullPath)))
                                    Response.Write("ok");
                                else
                                {
                                    SaveVisitLog(2, 0);
                                    Response.Write("不安全的图片格式，换一张吧。");
                                }
                            }
                            else//文件大小超过限制
                                Response.Write("图片大小" + Convert.ToInt32(oFile.ContentLength / 1024) + "KB,超出限制。");

                        }
                        else //文件类型不允许上传
                            Response.Write("上传的不是图片。");

                    }
                    catch
                    {
                        Response.Write("程序异常，上传未成功。");
                    }
                }
                else
                    Response.Write("请选择上传文件");
            }
            else
                Response.Write("上传有误");
        }
        /// <summary>
        /// 上传切割图
        /// </summary>
        private void UploadCutFile()
        {
            if (!(new JumboTCMS.DAL.Normal_UserDAL()).ChkUserSign(q("userid"), q("usersign")))
            {
                Response.Write("验证信息有误");
                Response.End();
            }
            System.Drawing.Image img = System.Drawing.Image.FromStream(Request.InputStream);
            string thumbnailPath1 = Server.MapPath("~/_data/avatar/" + q("userid") + "_l.jpg");
            string thumbnailPath2 = Server.MapPath("~/_data/avatar/" + q("userid") + "_m.jpg");
            string thumbnailPath3 = Server.MapPath("~/_data/avatar/" + q("userid") + "_s.jpg");
            img.Save(thumbnailPath1);
            JumboTCMS.Utils.ImageHelp.MakeMyThumbs(thumbnailPath1, thumbnailPath2, 48, 48, 0, 0, 120, 120);
            JumboTCMS.Utils.ImageHelp.MakeMyThumbs(thumbnailPath2, thumbnailPath3, 16, 16, 0, 0, 48, 48);
            img.Dispose();
            Response.Write("ok");
        }
        /// <summary>
        /// 预览图片
        /// </summary>
        private void UploadMedia()
        {
            if (!(new JumboTCMS.DAL.Normal_UserDAL()).ChkUserSign(q("userid"), q("usersign")))
            {
                Response.Write("验证信息有误");
                Response.End();
            }
            Response.Expires = 0;
            Response.Buffer = true;
            Response.ExpiresAbsolute = DateTime.Now.AddSeconds(-1);
            Response.AddHeader("pragma", "no-cache");
            Response.CacheControl = "no-cache";
            string _url = "~/_data/tempfiles/user_" + q("userid") + "_avatar.jpg";
            try
            {
                System.Drawing.Image img = System.Drawing.Image.FromFile(Server.MapPath(_url));
                System.IO.MemoryStream ms = new System.IO.MemoryStream();
                img.Save(ms, JumboTCMS.Utils.ImageHelp.ImgFormat(_url));
                Response.ClearContent();
                Response.BinaryWrite(ms.ToArray());
                Response.ContentType = "image/jpeg";//指定输出格式为图形
                img.Dispose();
                Response.End();
            }
            catch (System.Exception e)
            {
                throw e;
            }
        }
        #endregion
        /// <summary>
        /// 修改密码
        /// </summary>
        private void ajaxChangePassword()
        {
            User_Load("", "json");
            string _oldPass = f("txtOldPass");
            string _NewPass = f("txtNewPass1");
            if (_NewPass.Length > 14 || _NewPass.Length < 6)
            {
                this._response = "JumboTCMS.Alert('请输入6-14位的新密码', '0');";
            }
            else
            {
                if (new JumboTCMS.DAL.Normal_UserDAL().ChangeUserPassword(UserId, _oldPass, _NewPass))
                    this._response = "JumboTCMS.Message('修改成功', '1', \"window.location='" + site.Dir + "user/index.aspx';\");";
                else
                    this._response = "JumboTCMS.Alert('原始密码错误', '0');";
            }
        }
        /// <summary>
        /// 修改基本信息
        /// </summary>
        private void ajaxChangeProfile()
        {
            User_Load("", "json");
            doh.Reset();
            doh.ConditionExpress = "id=@id and state=1";
            doh.AddConditionParameter("@id", UserId);
            doh.AddFieldItem("Sex", f("rblSex"));
            doh.AddFieldItem("Birthday", f("txtBirthday"));
            doh.AddFieldItem("NickName", f("txtNickName"));
            doh.AddFieldItem("TrueName", f("txtTrueName"));
            doh.AddFieldItem("Signature", f("txtSignature"));
            doh.AddFieldItem("IDType", f("ddlIDType"));
            doh.AddFieldItem("IDCard", f("txtIDCard"));
            doh.AddFieldItem("ProvinceCity", f("selProvince") + "-" + f("selCity"));
            doh.AddFieldItem("WorkUnit", f("txtWorkUnit"));
            doh.AddFieldItem("Address", f("txtAddress"));
            doh.AddFieldItem("ZipCode", f("txtZipCode"));
            doh.AddFieldItem("MobileTel", f("txtMobileTel"));
            doh.AddFieldItem("Telephone", f("txtTelephone"));
            doh.AddFieldItem("QQ", f("txtQQ"));
            doh.AddFieldItem("MSN", f("txtMSN"));
            doh.AddFieldItem("HomePage", f("txtHomePage"));
            doh.Update("jcms_normal_user");
            this._response = "JumboTCMS.Message('修改成功', '1', \"window.location='" + site.Dir + "user/index.aspx';\");";
        }
        /// <summary>
        /// 修改论坛登录信息
        /// </summary>
        private void ajaxChangeForumInfo()
        {
            User_Load("", "json");
            string _ForumName = f("txtForumName");
            string _ForumPass = f("txtForumPass1");
            JumboTCMS.API.Discuz.Toolkit.DiscuzSession ds = JumboTCMS.API.Discuz.DiscuzSessionHelper.GetSession();
            ds.Logout(site.CookieDomain);//先登出
            if (ds.Login(ds.GetUserID(_ForumName), _ForumPass, false, 1, site.CookieDomain))
            {
                doh.Reset();
                doh.ConditionExpress = "id=@id and state=1";
                doh.AddConditionParameter("@id", UserId);
                doh.AddFieldItem("ForumName", _ForumName);
                doh.AddFieldItem("ForumPass", JumboTCMS.Utils.MD5.Lower32(_ForumPass));
                doh.Update("jcms_normal_user");
                this._response = "JumboTCMS.Message('修改成功', '1');";
            }
            else
                this._response = "JumboTCMS.Message('账户或密码错误', '0');";
        }
        #region 站内信
        private void ajaxGetMessageList()
        {
            User_Load("", "json");
            int page = Int_ThisPage();
            int PSize = Str2Int(q("pagesize"), 10);
            int countNum = 0;
            string sqlStr = "";
            string joinStr = "A.[SendUserId]=B.Id";
            string whereStr1 = "A.ReceiveUserId=" + UserId;//外围条件(带A.)
            string whereStr2 = "ReceiveUserId=" + UserId;//分页条件(不带A.)
            string mode = q("mode");
            if (mode != "")
            {
                switch (mode)
                {
                    case "new":
                        whereStr1 += " AND A.State=0";
                        whereStr2 += " AND State=0";
                        break;
                    case "old":
                        whereStr1 += " AND A.State=1";
                        whereStr2 += " AND State=1";
                        break;
                }
            }
            if (Str2Str(q("id")) != "0")
            {
                whereStr1 += " AND A.id=" + Str2Str(q("id"));
                whereStr2 += " AND id=" + Str2Str(q("id"));
            }
            doh.Reset();
            doh.ConditionExpress = whereStr2;
            countNum = doh.Count("jcms_normal_user_message");
            sqlStr = JumboTCMS.Utils.SqlHelp.GetSql("A.id as id,A.Title as Title,A.Content as Content,A.SendUserId as SendUserId,B.UserName as SendUserName,A.AddDate as AddDate,A.State as State", "jcms_normal_user_message", "jcms_normal_user", "Id", PSize, page, "desc", joinStr, whereStr1, whereStr2);
            doh.Reset();
            doh.SqlCmd = sqlStr;
            DataTable dt = doh.GetDataTable();
            this._response = "{result :\"1\"," +
                "returnval :\"操作成功\"," +
                "pagerbar :\"" + JumboTCMS.Utils.HtmlPager.GetPageBar(3, "js", 2, countNum, PSize, page, "javascript:ajaxList(<#page#>);") + "\"," +
                JumboTCMS.Utils.dtHelp.DT2JSON(dt, (PSize * (page - 1))) +
                "}";
            dt.Clear();
            dt.Dispose();
        }

        /// <summary>
        /// 发送站内消息
        /// </summary>
        private void ajaxSendMessage()
        {
            User_Load("", "json");
            if (Str2Str(f("txtUserId")) != "0" && f("txtUserName").Length > 0 && f("txtContent").Length > 0)
            {
                string _Content = JumboTCMS.Utils.Strings.HtmlEncode(f("txtContent"));
                string _Title = GetCutString(_Content, 10);
                new JumboTCMS.DAL.Normal_UserMessageDAL().SendMessage(_Title, _Content, UserId, f("txtUserId"), f("txtUserName"));
                this._response = "location='message_list.aspx';";
            }
            else
                this._response = "JumboTCMS.Alert('提交有误', '0');";

        }
        /// <summary>
        /// 回复站内消息
        /// </summary>
        private void ajaxReplyMessage()
        {
            User_Load("", "json");
            if (Str2Str(f("txtUserId")) != "0" && f("txtUserName").Length > 0 && f("txtContent").Length > 0)
            {
                string _Content = JumboTCMS.Utils.Strings.HtmlEncode(f("txtContent"));
                string _Title = GetCutString(JumboTCMS.Utils.Strings.HtmlEncode(f("txtTitle")), 10);
                new JumboTCMS.DAL.Normal_UserMessageDAL().SendMessage(_Title, _Content, UserId, f("txtUserId"), f("txtUserName"));
                Response.Write("location='message_list.aspx';");
            }
            else
                Response.Write("JumboTCMS.Alert('提交有误', '0');");
        }
        /// <summary>
        /// 确认是否能发送
        /// </summary>
        private void ajaxCheckSendMessage()
        {
            User_Load("", "json");
            if (q("txtUserName") == UserName)
                this._response = JsonResult(0, "0");
            else
            {
                doh.Reset();
                doh.ConditionExpress = "username=@username";
                doh.AddConditionParameter("@username", q("txtUserName"));
                string _uId = Str2Str(doh.GetField("jcms_normal_user", "id").ToString());
                if (_uId == "0")
                    this._response = JsonResult(0, "0");
                else
                    this._response = JsonResult(1, _uId);
            }
        }
        private void ajaxDelMessage()
        {
            User_Load("", "json");
            string mId = f("id");
            doh.Reset();
            doh.ConditionExpress = "id=@id and ReceiveUserId=" + UserId;
            doh.AddConditionParameter("@id", mId);
            if (doh.Delete("jcms_normal_user_message") != 0)
                this._response = JsonResult(1, "删除成功");
            else
                this._response = JsonResult(0, "删除失败");
        }
        #endregion
        #region 站内通知
        private void ajaxGetNoticeList()
        {
            User_Load("", "json");
            int page = Int_ThisPage();
            int PSize = Str2Int(q("pagesize"), 10);
            int countNum = 0;
            string sqlStr = "";
            string whereStr = "UserId=" + UserId;
            string type = q("type");
            if (type != "")
            {
                whereStr += " AND [NoticeType]='" + type + "'";
            }
            doh.Reset();
            doh.ConditionExpress = whereStr;
            countNum = doh.Count("jcms_normal_user_notice");
            doh.Reset();
            doh.ConditionExpress = "state=0 and id in(" + JumboTCMS.Utils.SqlHelp.GetSql("Id", "jcms_normal_user_notice", "Id", PSize, page, "desc", whereStr) + ")";
            doh.AddFieldItem("State", 1);
            doh.AddFieldItem("ReadTime", System.DateTime.Now.ToString());
            doh.Update("jcms_normal_user_notice");
            sqlStr = JumboTCMS.Utils.SqlHelp.GetSql("Id,Title,Content,AddDate,State", "jcms_normal_user_notice", "Id", PSize, page, "desc", whereStr);
            doh.Reset();
            doh.SqlCmd = sqlStr;
            DataTable dt = doh.GetDataTable();
            this._response = "{result :\"1\"," +
                "returnval :\"操作成功\"," +
                "pagerbar :\"" + JumboTCMS.Utils.HtmlPager.GetPageBar(3, "js", 2, countNum, PSize, page, "javascript:ajaxList(<#page#>);") + "\"," +
                JumboTCMS.Utils.dtHelp.DT2JSON(dt, (PSize * (page - 1))) +
                "}";
            dt.Clear();
            dt.Dispose();
        }
        #endregion
        #region 收藏夹
        private void ajaxGetFavoriteList()
        {
            User_Load("", "json");
            int page = Int_ThisPage();
            int PSize = Str2Int(q("pagesize"), 10);
            int countNum = 0;
            string sqlStr = "";
            string joinStr = "A.[ChannelId]=B.Id";
            string whereStr1 = "A.UserId=" + UserId;//外围条件(带A.)
            string whereStr2 = "UserId=" + UserId;//分页条件(不带A.)
            string sdate = q("sdate");
            if (sdate != "")
            {
                if (DBType == "0")
                {
                    switch (sdate)
                    {
                        case "1w":
                            whereStr1 += " AND datediff('ww',A.AddDate,'" + DateTime.Now.ToShortDateString() + "')=0";
                            whereStr2 += " AND datediff('ww',AddDate,'" + DateTime.Now.ToShortDateString() + "')=0";
                            break;
                        case "1m":
                            whereStr1 += " AND datediff('m',A.AddDate,'" + DateTime.Now.ToShortDateString() + "')=0";
                            whereStr2 += " AND datediff('m',AddDate,'" + DateTime.Now.ToShortDateString() + "')=0";
                            break;
                        case "1y":
                            whereStr1 += " AND A.AddDate>=#" + (DateTime.Now.Year + "-1-1") + "#";
                            whereStr2 += " AND AddDate>=#" + (DateTime.Now.Year + "-1-1") + "#";
                            break;
                    }
                }
                else
                {
                    switch (sdate)
                    {
                        case "1w":
                            whereStr1 += " AND datediff(ww,A.AddDate,'" + DateTime.Now.ToShortDateString() + "')=0";
                            whereStr2 += " AND datediff(ww,AddDate,'" + DateTime.Now.ToShortDateString() + "')=0";
                            break;
                        case "1m":
                            whereStr1 += " AND datediff(m,A.AddDate,'" + DateTime.Now.ToShortDateString() + "')=0";
                            whereStr2 += " AND datediff(m,AddDate,'" + DateTime.Now.ToShortDateString() + "')=0";
                            break;
                        case "1y":
                            whereStr1 += " AND A.AddDate>='" + (DateTime.Now.Year + "-1-1") + "'";
                            whereStr2 += " AND AddDate>='" + (DateTime.Now.Year + "-1-1") + "'";
                            break;
                    }
                }
            }
            doh.Reset();
            doh.ConditionExpress = whereStr2;
            countNum = doh.Count("jcms_normal_user_favorite");
            sqlStr = JumboTCMS.Utils.SqlHelp.GetSql("A.id as id,A.channelid as channelid,A.contentid as contentid,A.moduletype as moduletype,A.Title as Title,A.AddDate as AddDate", "jcms_normal_user_favorite", "jcms_normal_channel", "Id", PSize, page, "desc", joinStr, whereStr1, whereStr2);
            doh.Reset();
            doh.SqlCmd = sqlStr;
            DataTable dt = doh.GetDataTable();
            this._response = "{result :\"1\"," +
                "returnval :\"操作成功\"," +
                "pagerbar :\"" + JumboTCMS.Utils.HtmlPager.GetPageBar(3, "js", 2, countNum, PSize, page, "javascript:ajaxList(<#page#>);") + "\"," +
                JumboTCMS.Utils.dtHelp.DT2JSON(dt, (PSize * (page - 1))) +
                "}";
            dt.Clear();
            dt.Dispose();
        }
        private void ajaxDelFavorite()
        {
            User_Load("", "json");
            string fId = f("id");
            doh.Reset();
            doh.ConditionExpress = "id=@id and UserId=" + UserId;
            doh.AddConditionParameter("@id", fId);
            if (doh.Delete("jcms_normal_user_favorite") == 1)
                this._response = JsonResult(1, "成功删除");
            else
                this._response = JsonResult(0, "删除有误");
        }
        #endregion
        #region 站内好友
        /// <summary>
        /// 根据页面ID号添加
        /// </summary>
        private void ajaxAddFriend()
        {
            User_Load("", "json");
            string uId = Str2Str(f("id"));
            if (new JumboTCMS.DAL.Normal_UserFriendsDAL().AddFriend(UserId, UserName, uId))
                this._response = JsonResult(1, "好友添加成功");
            else
                this._response = JsonResult(0, "对方已是你好友");
        }
        /// <summary>
        /// 通过页面form添加
        /// </summary>
        private void ajaxAddFriend2()
        {
            User_Load("", "json");
            string uId = Str2Str(f("txtUserId"));
            if (uId != "0")
            {
                if (new JumboTCMS.DAL.Normal_UserFriendsDAL().AddFriend(UserId, UserName, uId))
                    this._response = "location='friend_list.aspx';";
                else
                    this._response = "JumboTCMS.Alert('对方已是你好友', '0');";
            }
            else
                this._response = "JumboTCMS.Alert('提交有误', '0');";
        }
        private void ajaxCheckAddFriend()
        {
            User_Load("", "json");
            if (q("txtUserName") == UserName)
                this._response = JsonResult(0, "0");
            else
            {
                doh.Reset();
                doh.ConditionExpress = "username=@username";
                doh.AddConditionParameter("@username", q("txtUserName"));
                string _uId = Str2Str(doh.GetField("jcms_normal_user", "id").ToString());
                if (_uId == "0")
                    this._response = JsonResult(0, "0");
                else
                    this._response = JsonResult(1, _uId);
            }
        }
        private void ajaxGetFriendList()
        {
            User_Load("", "json");
            int page = Int_ThisPage();
            int PSize = Str2Int(q("pagesize"), 10);
            string joinStr = "A.[FriendId]=B.Id";
            string whereStr1 = "1=1";//外围条件(带A.)
            string whereStr2 = "1=1";//分页条件(不带A.)
            whereStr1 += " AND A.UserId=" + UserId;
            whereStr2 += " AND UserId=" + UserId;
            string jsonStr = "";
            new JumboTCMS.DAL.Normal_UserFriendsDAL().GetListJSON(page, PSize, joinStr, whereStr1, whereStr2, ref jsonStr);
            this._response = jsonStr;
        }
        private void ajaxDelFriend()
        {
            User_Load("", "json");
            string fId = f("friendid");
            if (new JumboTCMS.DAL.Normal_UserFriendsDAL().DeleteByFriendID(UserId, UserName, fId))
                this._response = JsonResult(1, "删除成功");
            else
                this._response = JsonResult(0, "删除失败");
        }
        #endregion
        #region 消费记录
        /// <summary>
        /// 消费记录
        /// </summary>
        private void ajaxGetConsumeList()
        {
            User_Load("", "json");
            int page = Int_ThisPage();
            int PSize = Str2Int(q("pagesize"), 10);
            int countNum = 0;
            string sqlStr = "";
            string joinStr = "A.[UserId]=B.Id";
            string whereStr1 = "A.OperType=2";//外围条件(带A.)
            string whereStr2 = "OperType=2";//分页条件(不带A.)
            string sdate = q("sdate");
            if (sdate != "")
            {
                if (DBType == "0")
                {
                    switch (sdate)
                    {
                        case "1w":
                            whereStr1 += " AND datediff('ww',A.OperTime,'" + DateTime.Now.ToShortDateString() + "')=0";
                            whereStr2 += " AND datediff('ww',OperTime,'" + DateTime.Now.ToShortDateString() + "')=0";
                            break;
                        case "1m":
                            whereStr1 += " AND datediff('m',A.OperTime,'" + DateTime.Now.ToShortDateString() + "')=0";
                            whereStr2 += " AND datediff('m',OperTime,'" + DateTime.Now.ToShortDateString() + "')=0";
                            break;
                        case "1y":
                            whereStr1 += " AND A.OperTime>=#" + (DateTime.Now.Year + "-1-1") + "#";
                            whereStr2 += " AND OperTime>=#" + (DateTime.Now.Year + "-1-1") + "#";
                            break;
                    }
                }
                else
                {
                    switch (sdate)
                    {
                        case "1w":
                            whereStr1 += " AND datediff(ww,A.OperTime,'" + DateTime.Now.ToShortDateString() + "')=0";
                            whereStr2 += " AND datediff(ww,OperTime,'" + DateTime.Now.ToShortDateString() + "')=0";
                            break;
                        case "1m":
                            whereStr1 += " AND datediff(m,A.OperTime,'" + DateTime.Now.ToShortDateString() + "')=0";
                            whereStr2 += " AND datediff(m,OperTime,'" + DateTime.Now.ToShortDateString() + "')=0";
                            break;
                        case "1y":
                            whereStr1 += " AND A.OperTime>='" + (DateTime.Now.Year + "-1-1") + "'";
                            whereStr2 += " AND OperTime>='" + (DateTime.Now.Year + "-1-1") + "'";
                            break;
                    }
                }
            }
            whereStr1 += " AND A.UserId=" + UserId;
            whereStr2 += " AND UserId=" + UserId;
            doh.Reset();
            doh.ConditionExpress = whereStr2;
            countNum = doh.Count("jcms_normal_user_logs");
            sqlStr = JumboTCMS.Utils.SqlHelp.GetSql("A.id as id,A.OperInfo as OperInfo,A.OperTime as OperTime", "jcms_normal_user_logs", "jcms_normal_user", "Id", PSize, page, "desc", joinStr, whereStr1, whereStr2);
            doh.Reset();
            doh.SqlCmd = sqlStr;
            DataTable dt = doh.GetDataTable();
            this._response = "{result :\"1\"," +
                "returnval :\"操作成功\"," +
                "pagerbar :\"" + JumboTCMS.Utils.HtmlPager.GetPageBar(3, "js", 2, countNum, PSize, page, "javascript:ajaxList(<#page#>);") + "\"," +
                JumboTCMS.Utils.dtHelp.DT2JSON(dt, (PSize * (page - 1))) +
                "}";
            dt.Clear();
            dt.Dispose();
        }
        #endregion
        #region 订单及购物车管理
        /// <summary>
        /// 订单记录
        /// </summary>
        private void ajaxGetOrderList()
        {
            User_Load("", "json");
            int page = Int_ThisPage();
            int PSize = Str2Int(q("pagesize"), 10);
            int countNum = 0;
            string sqlStr = "";
            string joinStr = "A.[UserId]=B.Id";
            string whereStr1 = "1=1";//外围条件(带A.)
            string whereStr2 = "1=1";//分页条件(不带A.)
            string sdate = q("sdate");
            if (sdate != "")
            {
                if (DBType == "0")
                {
                    switch (sdate)
                    {
                        case "1w":
                            whereStr1 += " AND datediff('ww',A.OrderTime,'" + DateTime.Now.ToShortDateString() + "')=0";
                            whereStr2 += " AND datediff('ww',OrderTime,'" + DateTime.Now.ToShortDateString() + "')=0";
                            break;
                        case "1m":
                            whereStr1 += " AND datediff('m',A.OrderTime,'" + DateTime.Now.ToShortDateString() + "')=0";
                            whereStr2 += " AND datediff('m',OrderTime,'" + DateTime.Now.ToShortDateString() + "')=0";
                            break;
                        case "1y":
                            whereStr1 += " AND A.OrderTime>=#" + (DateTime.Now.Year + "-1-1") + "#";
                            whereStr2 += " AND OrderTime>=#" + (DateTime.Now.Year + "-1-1") + "#";
                            break;
                    }
                }
                else
                {
                    switch (sdate)
                    {
                        case "1w":
                            whereStr1 += " AND datediff(ww,A.OrderTime,'" + DateTime.Now.ToShortDateString() + "')=0";
                            whereStr2 += " AND datediff(ww,OrderTime,'" + DateTime.Now.ToShortDateString() + "')=0";
                            break;
                        case "1m":
                            whereStr1 += " AND datediff(m,A.OrderTime,'" + DateTime.Now.ToShortDateString() + "')=0";
                            whereStr2 += " AND datediff(m,OrderTime,'" + DateTime.Now.ToShortDateString() + "')=0";
                            break;
                        case "1y":
                            whereStr1 += " AND A.OrderTime>='" + (DateTime.Now.Year + "-1-1") + "'";
                            whereStr2 += " AND OrderTime>='" + (DateTime.Now.Year + "-1-1") + "'";
                            break;
                    }
                }
            }
            whereStr1 += " AND A.UserId=" + UserId;
            whereStr2 += " AND UserId=" + UserId;
            doh.Reset();
            doh.ConditionExpress = whereStr2;
            countNum = doh.Count("jcms_normal_user_order");
            sqlStr = JumboTCMS.Utils.SqlHelp.GetSql("A.*", "jcms_normal_user_order", "jcms_normal_user", "Id", PSize, page, "desc", joinStr, whereStr1, whereStr2);
            doh.Reset();
            doh.SqlCmd = sqlStr;
            DataTable dt = doh.GetDataTable();
            this._response = "{result :\"1\"," +
                "returnval :\"操作成功\"," +
                "pagerbar :\"" + JumboTCMS.Utils.HtmlPager.GetPageBar(3, "js", 2, countNum, PSize, page, "javascript:ajaxList(<#page#>);") + "\"," +
                JumboTCMS.Utils.dtHelp.DT2JSON(dt, (PSize * (page - 1))) +
                "}";
            dt.Clear();
            dt.Dispose();
        }
        /// <summary>
        /// 通过订单号获得商品
        /// </summary>
        private void ajaxGetGoodsList()
        {
            User_Load("", "json");
            int page = Int_ThisPage();
            int PSize = Str2Int(q("pagesize"), 10);
            string _ordernum = q("ordernum");
            string mode = q("mode");
            int countNum = 0;
            string sqlStr = "";
            string whereStr = "1=1";
            if (_ordernum.Length > 0)
            {
                page = 1;
                PSize = 100;
                whereStr += " AND OrderNum='" + _ordernum + "'";
            }
            if (mode != "")
            {
                switch (mode)
                {
                    case "new":
                        whereStr += " AND State=0";
                        break;
                    case "old":
                        whereStr += " AND State=1";
                        break;
                }
            }
            whereStr += " AND UserId=" + UserId;
            doh.Reset();
            doh.ConditionExpress = whereStr;
            countNum = doh.Count("jcms_normal_user_goods");
            sqlStr = JumboTCMS.Utils.SqlHelp.GetSql("*", "jcms_normal_user_goods", "Id", PSize, page, "desc", whereStr);
            doh.Reset();
            doh.SqlCmd = sqlStr;
            DataTable dt = doh.GetDataTable();
            this._response = "{result :\"1\"," +
                "returnval :\"操作成功\"," +
                "pagerbar :\"" + JumboTCMS.Utils.HtmlPager.GetPageBar(3, "js", 2, countNum, PSize, page, "javascript:ajaxList(<#page#>);") + "\"," +
                JumboTCMS.Utils.dtHelp.DT2JSON(dt, (PSize * (page - 1))) +
                "}";
            dt.Clear();
            dt.Dispose();
        }
        /// <summary>
        /// 购物车列表
        /// </summary>
        private void ajaxGetCartList()
        {
            User_Load("", "json");
            int page = Int_ThisPage();
            int PSize = Str2Int(q("pagesize"), 10);
            int countNum = 0;
            string sqlStr = "";
            string joinStr = "A.[ProductId]=B.Id";
            string whereStr1 = "A.State=0 AND A.UserId=" + UserId;
            string whereStr2 = "State=0 AND UserId=" + UserId;
            doh.Reset();
            doh.ConditionExpress = whereStr2;
            countNum = doh.Count("jcms_normal_user_cart");
            sqlStr = JumboTCMS.Utils.SqlHelp.GetSql("A.*,b.points as unitprice,(b.points*a.buycount) as totalprice,b.title as productname,b.img as productimg", "jcms_normal_user_cart", "jcms_module_product", "Id", PSize, page, "desc", joinStr, whereStr1, whereStr2);
            doh.Reset();
            doh.SqlCmd = sqlStr;
            DataTable dt = doh.GetDataTable();
            this._response = "{result :\"1\"," +
                "returnval :\"操作成功\"," +
                "pagerbar :\"" + JumboTCMS.Utils.HtmlPager.GetPageBar(3, "js", 2, countNum, PSize, page, "javascript:ajaxList(<#page#>);") + "\"," +
                JumboTCMS.Utils.dtHelp.DT2JSON(dt, (PSize * (page - 1))) +
                "}";
            dt.Clear();
            dt.Dispose();
        }
        /// <summary>
        /// 作废订单
        /// </summary>
        private void ajaxDeleteOrder()
        {
            User_Load("", "json");
            string orderNum = f("ordernum");
            doh.Reset();
            if (DBType == "0")
            {
                doh.ConditionExpress = "ordernum=@ordernum and state=0 and UserId=" + UserId + " AND OrderTime<=#" + DateTime.Now.AddDays(-1).ToString() + "#";
            }
            else
            {
                doh.ConditionExpress = "ordernum=@ordernum and state=0 and UserId=" + UserId + " AND OrderTime<='" + DateTime.Now.AddDays(-1).ToString() + "'";

            }
            doh.AddConditionParameter("@ordernum", orderNum);
            if (doh.Delete("jcms_normal_user_order") == 1)
            {
                doh.Reset();
                doh.ConditionExpress = "ordernum=@ordernum and state=0 and UserId=" + UserId;
                doh.AddConditionParameter("@ordernum", orderNum);
                doh.Delete("jcms_normal_user_goods");
                this._response = JsonResult(1, "删除成功");
            }
            else
                this._response = JsonResult(0, "24小时内下的订单不能被作废");
        }
        /// <summary>
        /// 直接支付
        /// </summary>
        private void ajaxPayOrder()
        {
            User_Load("", "json");
            string orderNum = f("ordernum");
            float ordermoney = new JumboTCMS.DAL.Normal_UserOrderDAL().GetOrderMoney(UserId, orderNum);
            if (UserPoints - ordermoney < 0)//博币不够
            {
                this._response = JsonResult(0, "您的博币不足，请先充值");
                return;
            }
            doh.Reset();
            doh.ConditionExpress = "Id=" + UserId;
            doh.AddFieldItem("Points", UserPoints - Convert.ToInt32(ordermoney));
            doh.Update("jcms_normal_user");
            doh.Reset();
            doh.ConditionExpress = "ordernum=@ordernum and state=0 and UserId=" + UserId;
            doh.AddConditionParameter("@ordernum", orderNum);
            doh.AddFieldItem("State", 1);
            if (doh.Update("jcms_normal_user_order") == 1)
            {
                doh.Reset();
                doh.ConditionExpress = "ordernum=@ordernum and state=0 and UserId=" + UserId;
                doh.AddConditionParameter("@ordernum", orderNum);
                doh.AddFieldItem("State", 1);
                doh.Update("jcms_normal_user_goods");
                this._response = JsonResult(1, "支付成功，请等待发货");
            }
            else
                this._response = JsonResult(0, "支付有误");
        }
        /// <summary>
        /// 确认收货
        /// </summary>
        private void ajaxFinishOrder()
        {
            User_Load("", "json");
            string orderNum = f("ordernum");
            doh.Reset();
            doh.ConditionExpress = "ordernum=@ordernum and state=2 and UserId=" + UserId;
            doh.AddConditionParameter("@ordernum", orderNum);
            doh.AddFieldItem("State", 3);
            if (doh.Update("jcms_normal_user_order") == 1)
            {
                doh.Reset();
                doh.ConditionExpress = "ordernum=@ordernum and state=2 and UserId=" + UserId;
                doh.AddConditionParameter("@ordernum", orderNum);
                doh.AddFieldItem("State", 3);
                doh.Update("jcms_normal_user_goods");
                this._response = JsonResult(1, "设置成功");
            }
            else
                this._response = JsonResult(0, "设置有误");
        }
        /// <summary>
        /// 购物车商品转成订单
        /// </summary>
        private void ajaxSetCart2Order()
        {
            User_Load("", "json");
            if (new JumboTCMS.DAL.Normal_UserOrderDAL().GetOrderTotal(UserId, 0) >= site.ProductMaxOrderCount)
            {
                this._response = "JumboTCMS.Alert('您有太多的订单未付款，请稍后再下新单', '0', \"window.location='maimai_orderlist.aspx';\");";
                return;
            }
            string trueName = f("txtTrueName");
            string address = f("txtAddress");
            string zipCode = f("txtZipCode");
            string mobileTel = f("txtMobileTel");
            if (trueName.Length == 0 || address.Length == 0 || zipCode.Length == 0 || mobileTel.Length == 0)
            {
                this._response = "JumboTCMS.Alert('收货信息不完整', '0');";
                return;
            }
            if (new JumboTCMS.DAL.Normal_UserOrderDAL().NewOrder(UserId, trueName, address, zipCode, mobileTel))
                this._response = "JumboTCMS.Message('订单提交成功，请尽快付款', '1', \"window.location='maimai_orderlist.aspx';\");";
            else
                this._response = "JumboTCMS.Alert('未知的错误', '0');";
        }
        /// <summary>
        /// 从购物车里删除商品
        /// </summary>
        private void ajaxDelCart()
        {
            User_Load("", "json");
            string cId = f("id");
            doh.Reset();
            doh.ConditionExpress = "id=@id and state=0 and UserId=" + UserId;
            doh.AddConditionParameter("@id", cId);
            if (doh.Delete("jcms_normal_user_cart") == 1)
                this._response = JsonResult(1, "删除成功");
            else
                this._response = JsonResult(0, "删除失败");
        }
        /// <summary>
        /// 设置商品数量
        /// </summary>
        private void ajaxSetBuyCount()
        {
            User_Load("", "json");
            string productId = f("productid");
            int buyCount = Str2Int(f("buycount"), 1);
            if (buyCount > 0 && buyCount <= site.ProductMaxBuyCount)
            {
                doh.Reset();
                doh.ConditionExpress = "productid=@productid and state=0 and UserId=" + UserId;
                doh.AddConditionParameter("@productid", productId);
                doh.AddFieldItem("BuyCount", buyCount);
                if (new JumboTCMS.DAL.Normal_UserCartDAL().UpdateGoods(UserId, productId, buyCount, 0))
                    this._response = JsonResult(1, "设置成功");
                else
                    this._response = JsonResult(0, "设置失败");
            }
            else
            {
                this._response = JsonResult(0, "每样商品只能购买1～" + site.ProductMaxBuyCount + "件");
            }
        }
        #endregion
        #region 点卡管理
        /// <summary>
        /// 激活点卡
        /// </summary>
        private void ajaxCard2Points()
        {
            User_Load("", "json");
            if (!CheckFormUrl())
                Response.End();
            string _cardNumber = f("txtCardNumber");
            string _cardPassword = f("txtCardPassword");
            doh.Reset();
            doh.ConditionExpress = "cardnumber=@cardnumber and cardpassword=@cardpassword AND State=2";
            doh.AddConditionParameter("@cardnumber", _cardNumber);
            doh.AddConditionParameter("@cardpassword", _cardPassword);
            doh.AddFieldItem("UserId", UserId);
            doh.AddFieldItem("ActiveTime", DateTime.Now.ToString());
            doh.AddFieldItem("ActiveIP", Const.GetUserIp);
            doh.AddFieldItem("State", 3);
            if (doh.Update("jcms_normal_pointscard") == 1)
            {
                doh.Reset();
                doh.ConditionExpress = "cardnumber=@cardnumber and cardpassword=@cardpassword AND State=3";
                doh.AddConditionParameter("@cardnumber", _cardNumber);
                doh.AddConditionParameter("@cardpassword", _cardPassword);
                int _AddPoints = Str2Int(doh.GetField("jcms_normal_pointscard", "Points").ToString());
                doh.Reset();
                doh.ConditionExpress = "id=@id AND State=1";
                doh.AddConditionParameter("@id", UserId);
                doh.Add("jcms_normal_user", "Points", _AddPoints);
                string _mailBody = "ID：[" + UserId + "]，用户[" + UserName + "]于" + DateTime.Now.ToString() + "激活充值卡" + _cardNumber;
                SendServiceNotice(site.Name + "用户激活充值卡", _mailBody, "1");
                this._response = "JumboTCMS.Alert('充值卡激活成功,请查看博币是否已增加', '1', \"window.location='index.aspx';\");";

            }
            else
                this._response = "JumboTCMS.Alert('充值卡帐号和密码不正确', '0');";
        }
        #endregion
        /// <summary>
        /// 接触第三方接口绑定
        /// </summary>
        private void ajaxRemoveOAuth()
        {
            User_Load("", "json");
            string OAuth_Code = f("oauthcode");
            doh.Reset();
            doh.ConditionExpress = "id=" + UserId;
            doh.AddFieldItem("Token_" + OAuth_Code, "");
            if (doh.Update("jcms_normal_user") == 1)
                this._response = JsonResult(1, "操作成功");
            else
                this._response = JsonResult(0, "操作失败");
        }
    }
}
