﻿using System;
using System.Collections.Generic;
using System.Text;
using System.Xml.Serialization;
using Newtonsoft.Json;

namespace JumboTCMS.API.Discuz.Toolkit
{
    /// <summary>
    /// 用户信息
    /// </summary>
    public class User : Friend
    {
        public static readonly string[] FIELDS = { "uid", "user_name", "nick_name", "password", "space_id", "secques", "gender", 
                                                     "admin_id", "group_id", "group_expiry", "reg_ip", "join_date", "last_ip", 
                                                     "last_visit", "last_activity", "last_post", "last_post_id", "last_post_title", 
                                                     "post_count", "digest_post_count", "online_time", "page_view_count", "credits", 
                                                     "ext_credits_1", "ext_credits_2", "ext_credits_3", "ext_credits_4", 
                                                     "ext_credits_5", "ext_credits_6", "ext_credits_7", "ext_credits_8", "email", 
                                                     "birthday", "tpp", "ppp", "template_id", "pm_sound", "show_email", "invisible", 
                                                     "has_new_pm", "new_pm_count", "access_masks", "online_state", "web_site", "icq", 
                                                     "qq", "yahoo", "msn", "skype", "location", "custom_status", "avatar", 
                                                     "avatar_width", "avatar_height", "medals", "about_me", "sign_html", "real_name", 
                                                     "id_card", "mobile", "telephone" };
        //[XmlElement("user_id", IsNullable=false)]
        //public int Uid;	//用户uid

        [XmlElement("user_name", IsNullable = true)]
        [JsonPropertyAttribute("user_name")]
        public string UserName;	//用户名

        [XmlElement("nick_name", IsNullable = true)]
        [JsonPropertyAttribute("nick_name")]
        public string NickName;	//昵称

        [XmlElement("password", IsNullable = true)]
        [JsonPropertyAttribute("password")]
        public string Password;	//用户密码

        [XmlElement("space_id", IsNullable = true)]
        [JsonPropertyAttribute("space_id")]
        public int? SpaceId; //个人空间ID,0为用户还未申请空间;负数是用户已经申请,等待管理员开通,绝对值为开通以后的真实Spaceid;正数是用户已经开通的Spaceid

        [XmlElement("secques", IsNullable = true)]
        [JsonPropertyAttribute("secques")]
        public string Secques;	//用户安全提问码

        [XmlElement("gender", IsNullable = true)]
        [JsonPropertyAttribute("gender")]
        public int? Gender;	//性别

        [XmlElement("admin_id", IsNullable = true)]
        [JsonPropertyAttribute("admin_id")]
        public int? Adminid;	//管理组ID

        [XmlElement("group_id", IsNullable = true)]
        [JsonPropertyAttribute("group_id")]
        public int? GroupId;	//用户组ID

        [XmlElement("group_expiry", IsNullable = true)]
        [JsonPropertyAttribute("group_expiry")]
        public int? GroupExpiry;	//组过期时间

        [XmlElement("ext_groupids", IsNullable = true)]
        [JsonPropertyAttribute("ext_groupids")]
        public string ExtGroupids;	//扩展用户组

        [XmlElement("reg_ip", IsNullable = true)]
        [JsonPropertyAttribute("reg_ip")]
        public string RegIp;	//注册IP

        [XmlElement("join_date", IsNullable = true)]
        [JsonPropertyAttribute("join_date")]
        public string JoinDate;	//注册时间

        [XmlElement("last_ip", IsNullable = true)]
        [JsonPropertyAttribute("last_ip")]
        public string LastIp;	//上次登录IP

        [XmlElement("last_visit", IsNullable = true)]
        [JsonPropertyAttribute("last_visit")]
        public string LastVisit;	//上次访问时间

        [XmlElement("last_activity", IsNullable = true)]
        [JsonPropertyAttribute("last_activity")]
        public string LastActivity;	//最后活动时间

        [XmlElement("last_post", IsNullable = true)]
        [JsonPropertyAttribute("last_post")]
        public string LastPost;	//最后发贴时间

        [XmlElement("last_post_id", IsNullable = true)]
        [JsonPropertyAttribute("last_post_id")]
        public int? LastPostid;	//最后发贴id

        [XmlElement("last_post_title", IsNullable = true)]
        [JsonPropertyAttribute("last_post_title")]
        public string LastPostTitle;	//最后发贴标题

        [XmlElement("post_count", IsNullable = true)]
        [JsonPropertyAttribute("post_count")]
        public int? Posts;	//发贴数

        [XmlElement("digest_post_count", IsNullable = true)]
        [JsonPropertyAttribute("digest_post_count")]
        public int? DigestPosts;	//精华贴数

        [XmlElement("online_time", IsNullable = true)]
        [JsonPropertyAttribute("online_time")]
        public int? OnlineTime;	//在线时间

        [XmlElement("page_view_count", IsNullable = true)]
        [JsonPropertyAttribute("page_view_count")]
        public int? PageViews;	//页面浏览量

        [XmlElement("credits", IsNullable = true)]
        [JsonPropertyAttribute("credits")]
        public int? Credits;	//积分数

        [XmlElement("ext_credits_1", IsNullable = true)]
        [JsonPropertyAttribute("ext_credits_1")]
        public float? ExtCredits1;	//扩展积分1

        [XmlElement("ext_credits_2", IsNullable = true)]
        [JsonPropertyAttribute("ext_credits_2")]
        public float? ExtCredits2;	//扩展积分2

        [XmlElement("ext_credits_3", IsNullable = true)]
        [JsonPropertyAttribute("ext_credits_3")]
        public float? ExtCredits3;	//扩展积分3

        [XmlElement("ext_credits_4", IsNullable = true)]
        [JsonPropertyAttribute("ext_credits_4")]
        public float? ExtCredits4;	//扩展积分4

        [XmlElement("ext_credits_5", IsNullable = true)]
        [JsonPropertyAttribute("ext_credits_5")]
        public float? ExtCredits5;	//扩展积分5

        [XmlElement("ext_credits_6", IsNullable = true)]
        [JsonPropertyAttribute("ext_credits_6")]
        public float? ExtCredits6;	//扩展积分6

        [XmlElement("ext_credits_7", IsNullable = true)]
        [JsonPropertyAttribute("ext_credits_7")]
        public float? ExtCredits7;	//扩展积分7

        [XmlElement("ext_credits_8", IsNullable = true)]
        [JsonPropertyAttribute("ext_credits_8")]
        public float? ExtCredits8;	//扩展积分8

        [XmlIgnore]
        [JsonIgnore]
        public int? AvatarShowId;	//头像ID

        [XmlElement("email", IsNullable = true)]
        [JsonPropertyAttribute("email")]
        public string Email;	//邮件地址

        [XmlElement("birthday", IsNullable = true)]
        [JsonPropertyAttribute("birthday")]
        public string Birthday;	//生日

        [XmlIgnore]
        [JsonIgnore]
        public int? SigStatus;	//签名

        [XmlElement("tpp", IsNullable = true)]
        [JsonPropertyAttribute("tpp")]
        public int? Tpp;	//每页主题数

        [XmlElement("ppp", IsNullable = true)]
        [JsonPropertyAttribute("ppp")]
        public int? Ppp;	//每页贴数

        [XmlElement("template_id", IsNullable = true)]
        [JsonPropertyAttribute("template_id")]
        public int? Templateid;	//风格ID

        [XmlElement("pm_sound", IsNullable = true)]
        [JsonPropertyAttribute("pm_sound")]
        public int? PmSound;	//短消息铃声

        [XmlElement("show_email", IsNullable = true)]
        [JsonPropertyAttribute("show_email")]
        public int? ShowEmail;	//是否显示邮箱

        //[XmlElement("tv")]
        //public ReceivePMSettingType m_newsletter;	//是否接收论坛通知

        [XmlElement("invisible", IsNullable = true)]
        [JsonPropertyAttribute("invisible")]
        public int? Invisible;	//是否隐身
        //private string m_timeoffset;	//时差

        [XmlElement("has_new_pm", IsNullable = true)]
        [JsonPropertyAttribute("has_new_pm")]
        public int? NewPm;	//是否有新消息

        [XmlElement("new_pm_count", IsNullable = true)]
        [JsonPropertyAttribute("new_pm_count")]
        public int? NewPmCount;	//新短消息数量

        [XmlElement("access_masks", IsNullable = true)]
        [JsonPropertyAttribute("access_masks")]
        public int? AccessMasks;	//是否使用特殊权限

        [XmlElement("online_state", IsNullable = true)]
        [JsonPropertyAttribute("online_state")]
        public int? OnlineState;	//在线状态, 1为在线, 0为不在线





        [XmlElement("web_site", IsNullable = true)]
        [JsonPropertyAttribute("web_site")]
        public string WebSite;	//网站

        [XmlElement("icq", IsNullable = true)]
        [JsonPropertyAttribute("icq")]
        public string Icq;	//icq号码

        [XmlElement("qq", IsNullable = true)]
        [JsonPropertyAttribute("qq")]
        public string Qq;	//qq号码

        [XmlElement("yahoo", IsNullable = true)]
        [JsonPropertyAttribute("yahoo")]
        public string Yahoo;	//yahoo messenger帐号

        [XmlElement("msn", IsNullable = true)]
        [JsonPropertyAttribute("msn")]
        public string Msn;	//msn messenger帐号

        [XmlElement("skype", IsNullable = true)]
        [JsonPropertyAttribute("skype")]
        public string Skype;	//skype帐号

        [XmlElement("location", IsNullable = true)]
        [JsonPropertyAttribute("location")]
        public string Location;	//来自

        [XmlElement("custom_status", IsNullable = true)]
        [JsonPropertyAttribute("custom_status")]
        public string CustomStatus;	//自定义头衔

        [XmlElement("avatar", IsNullable = true)]
        [JsonPropertyAttribute("avatar")]
        public string Avatar;	//头像宽度

        [XmlElement("avatar_width", IsNullable = true)]
        [JsonPropertyAttribute("avatar_width")]
        public int? AvatarWidth;	//头像宽度

        [XmlElement("avatar_height", IsNullable = true)]
        [JsonPropertyAttribute("avatar_height")]
        public int? AvatarHeight;	//头像高度

        [XmlElement("medals", IsNullable = true)]
        [JsonPropertyAttribute("medals")]
        public string Medals; //勋章列表

        [XmlElement("about_me", IsNullable = true)]
        [JsonPropertyAttribute("about_me")]
        public string Bio;	//自我介绍

        [XmlIgnore]
        [JsonIgnore]
        public string Signature;	//签名

        [XmlElement("sign_html", IsNullable = true)]
        [JsonPropertyAttribute("sign_html")]
        public string Sightml;	//签名Html(自动转换得到)

        [XmlIgnore]
        [JsonIgnore]
        public string AuthStr;	//验证码

        [XmlIgnore]
        [JsonIgnore]
        public string AuthTime;	//验证码生成日期

        [XmlIgnore]
        [JsonIgnore]
        public byte AuthFlag;	//验证码使用标志(0 未使用,1 用户邮箱验证及用户信息激活, 2 用户密码找回)

        [XmlElement("real_name", IsNullable = true)]
        [JsonPropertyAttribute("real_name")]
        public string RealName;  //用户实名

        [XmlElement("id_card", IsNullable = true)]
        [JsonPropertyAttribute("id_card")]
        public string IdCard;    //用户身份证件号

        [XmlElement("mobile", IsNullable = true)]
        [JsonPropertyAttribute("mobile")]
        public string Mobile;    //用户移动电话

        [XmlElement("telephone", IsNullable = true)]
        [JsonPropertyAttribute("telephone")]
        public string Phone;     //用户固定电话
    }

    public class UserForEditing //: Friend
    {

        [JsonPropertyAttribute("nick_name")]
        public string NickName;	//昵称


        [JsonPropertyAttribute("password")]
        public string Password;	//用户密码


        [JsonPropertyAttribute("space_id")]
        public string SpaceId; //个人空间ID,0为用户还未申请空间;负数是用户已经申请,等待管理员开通,绝对值为开通以后的真实Spaceid;正数是用户已经开通的Spaceid


        //[JsonPropertyAttribute("secues")]
        //public string Secques;	//用户安全提问码


        [JsonPropertyAttribute("gender")]
        public string Gender;	//性别


        [JsonPropertyAttribute("ext_credits_1")]
        public string ExtCredits1;	//扩展积分1


        [JsonPropertyAttribute("ext_credits_2")]
        public string ExtCredits2;	//扩展积分2


        [JsonPropertyAttribute("ext_credits_3")]
        public string ExtCredits3;	//扩展积分3


        [JsonPropertyAttribute("ext_credits_4")]
        public string ExtCredits4;	//扩展积分4


        [JsonPropertyAttribute("ext_credits_5")]
        public string ExtCredits5;	//扩展积分5


        [JsonPropertyAttribute("ext_credits_6")]
        public string ExtCredits6;	//扩展积分6


        [JsonPropertyAttribute("ext_credits_7")]
        public string ExtCredits7;	//扩展积分7


        [JsonPropertyAttribute("ext_credits_8")]
        public string ExtCredits8;	//扩展积分8


        [JsonPropertyAttribute("email")]
        public string Email;	//邮件地址


        [JsonPropertyAttribute("birthday")]
        public string Birthday;	//生日

        //[XmlIgnore]
        //[JsonIgnore]
        //public int SigStatus;	//签名


        //[JsonPropertyAttribute("invisible")]
        //public int Invisible;	//是否隐身
        //private string m_timeoffset;	//时差



        [JsonPropertyAttribute("web_site")]
        public string WebSite;	//网站


        [JsonPropertyAttribute("icq")]
        public string Icq;	//icq号码


        [JsonPropertyAttribute("qq")]
        public string Qq;	//qq号码


        [JsonPropertyAttribute("yahoo")]
        public string Yahoo;	//yahoo messenger帐号


        [JsonPropertyAttribute("msn")]
        public string Msn;	//msn messenger帐号


        [JsonPropertyAttribute("skype")]
        public string Skype;	//skype帐号


        [JsonPropertyAttribute("location")]
        public string Location;	//来自


        [JsonPropertyAttribute("about_me")]
        public string Bio;	//自我介绍


        [JsonPropertyAttribute("real_name")]
        public string RealName;  //用户实名


        [JsonPropertyAttribute("id_card")]
        public string IdCard;    //用户身份证件号


        [JsonPropertyAttribute("mobile")]
        public string Mobile;    //用户移动电话


        [JsonPropertyAttribute("telephone")]
        public string Phone;     //用户固定电话

    }

}
