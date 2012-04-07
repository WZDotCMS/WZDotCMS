

CREATE TABLE [jcms_module_article] (
	[Id] AutoIncrement primary key ,
	[ChannelId] int NOT NULL DEFAULT "0",
	[ClassId] int NOT NULL DEFAULT "0",
	[Title] varchar(150) NOT NULL ,
	[TColor] varchar(8) NOT NULL DEFAULT "",
	[AddDate] datetime NOT NULL DEFAULT NOW(),
	[Summary] text NOT NULL DEFAULT "",
	[Editor] varchar(20) NOT NULL DEFAULT "",
	[Author] varchar(30) NOT NULL DEFAULT "",
	[Tags] varchar(60) NOT NULL DEFAULT "",
	[ViewNum] int NOT NULL DEFAULT "0",
	[IsPass] int NOT NULL DEFAULT "0",
	[IsImg] int NOT NULL DEFAULT "0",
	[Img] varchar(150) NOT NULL DEFAULT "",
	[IsTop] int NOT NULL DEFAULT "0",
	[IsFocus] int NOT NULL DEFAULT "0",
	[UserId] int NOT NULL DEFAULT "0",
	[ReadGroup] int NOT NULL DEFAULT "0",
	[SourceFrom] varchar(30) NOT NULL DEFAULT "",
	[Content] text NOT NULL DEFAULT "",
	[FirstPage] varchar(150) NOT NULL DEFAULT "",
	[AliasPage] varchar(150) NOT NULL DEFAULT ""
)
GO

CREATE TABLE [jcms_module_document] (
	[Id] AutoIncrement primary key ,
	[ChannelId] int NOT NULL DEFAULT "0",
	[ClassId] int NOT NULL DEFAULT "0",
	[Title] varchar(150) NOT NULL ,
	[TColor] varchar(8) NOT NULL DEFAULT "",
	[AddDate] datetime NOT NULL DEFAULT NOW(),
	[Summary] text NOT NULL DEFAULT "",
	[Editor] varchar(20) NOT NULL DEFAULT "",
	[Author] varchar(30) NOT NULL DEFAULT "",
	[Tags] varchar(60) NOT NULL DEFAULT "",
	[ViewNum] int NOT NULL DEFAULT "0",
	[IsPass] int NOT NULL DEFAULT "0",
	[IsImg] int NOT NULL DEFAULT "0",
	[Img] varchar(150) NOT NULL DEFAULT "",
	[IsTop] int NOT NULL DEFAULT "0",
	[IsFocus] int NOT NULL DEFAULT "0",
	[UserId] int NOT NULL DEFAULT "0",
	[ReadGroup] int NOT NULL DEFAULT "0",
	[SourceFrom] varchar(30) NOT NULL DEFAULT "",
	[PageNumber] int NOT NULL DEFAULT "1",
	[Points] int NOT NULL DEFAULT "0",
	[DocumentUrl] text NOT NULL DEFAULT "",
	[PageSize] int NOT NULL DEFAULT "0",
	[FirstPage] varchar(150) NOT NULL DEFAULT "",
	[AliasPage] varchar(150) NOT NULL DEFAULT ""
)
GO

CREATE TABLE [jcms_module_document_downlogs] (
	[Id] AutoIncrement primary key ,
	[ChannelId] int NOT NULL DEFAULT "0",
	[UserId] int NOT NULL DEFAULT "0",
	[DocumentId] int NOT NULL DEFAULT "0",
	[Points] int NOT NULL DEFAULT "0",
	[DownTime] datetime NOT NULL DEFAULT NOW(),
	[DownIP] varchar(16) NULL ,
	[DownDegree] int NOT NULL DEFAULT "0"
)
GO

CREATE TABLE [jcms_module_paper] (
	[Id] AutoIncrement primary key ,
	[ChannelId] int NOT NULL DEFAULT "0",
	[ClassId] int NOT NULL DEFAULT "0",
	[Title] varchar(150) NOT NULL ,
	[TColor] varchar(8) NOT NULL DEFAULT "",
	[AddDate] datetime NOT NULL DEFAULT NOW(),
	[Summary] text NOT NULL DEFAULT "",
	[Editor] varchar(20) NOT NULL DEFAULT "",
	[Author] varchar(30) NOT NULL DEFAULT "",
	[Tags] varchar(60) NOT NULL DEFAULT "",
	[ViewNum] int NOT NULL DEFAULT "0",
	[IsPass] int NOT NULL DEFAULT "0",
	[IsImg] int NOT NULL DEFAULT "0",
	[Img] varchar(150) NOT NULL DEFAULT "",
	[IsTop] int NOT NULL DEFAULT "0",
	[IsFocus] int NOT NULL DEFAULT "0",
	[UserId] int NOT NULL DEFAULT "0",
	[ReadGroup] int NOT NULL DEFAULT "0",
	[SourceFrom] varchar(30) NOT NULL DEFAULT "",
	[PageNumber] int NOT NULL DEFAULT "1",
	[Points] int NOT NULL DEFAULT "0",
	[PaperUrl] varchar(150) NOT NULL DEFAULT "",
	[SwfFile] varchar(150) NOT NULL DEFAULT "",
	[PageSize] int NOT NULL DEFAULT "0",
	[FirstPage] varchar(150) NOT NULL DEFAULT "",
	[AliasPage] varchar(150) NOT NULL DEFAULT ""
)
GO

CREATE TABLE [jcms_module_paper_downlogs] (
	[Id] AutoIncrement primary key ,
	[ChannelId] int NOT NULL DEFAULT "0",
	[UserId] int NOT NULL DEFAULT "0",
	[PaperId] int NOT NULL DEFAULT "0",
	[Points] int NOT NULL DEFAULT "0",
	[DownTime] datetime NOT NULL DEFAULT NOW(),
	[DownIP] varchar(16) NULL ,
	[DownDegree] int NOT NULL DEFAULT "0"
)
GO

CREATE TABLE [jcms_module_photo] (
	[Id] AutoIncrement primary key ,
	[ChannelId] int NOT NULL DEFAULT "0",
	[ClassId] int NOT NULL DEFAULT "0",
	[Title] varchar(150) NOT NULL ,
	[TColor] varchar(8) NOT NULL DEFAULT "",
	[AddDate] datetime NOT NULL DEFAULT NOW(),
	[Summary] text NOT NULL DEFAULT "",
	[Editor] varchar(20) NOT NULL DEFAULT "",
	[Author] varchar(30) NOT NULL DEFAULT "",
	[Tags] varchar(60) NOT NULL DEFAULT "",
	[ViewNum] int NOT NULL DEFAULT "0",
	[IsPass] int NOT NULL DEFAULT "0",
	[IsImg] int NOT NULL DEFAULT "0",
	[Img] varchar(150) NOT NULL DEFAULT "",
	[IsTop] int NOT NULL DEFAULT "0",
	[IsFocus] int NOT NULL DEFAULT "0",
	[UserId] int NOT NULL DEFAULT "0",
	[ReadGroup] int NOT NULL DEFAULT "0",
	[SourceFrom] varchar(30) NOT NULL DEFAULT "",
	[ThumbsUrl] text NOT NULL DEFAULT "",
	[PhotoUrl] text NOT NULL DEFAULT "",
	[PageSize] int NOT NULL DEFAULT "0",
	[FirstPage] varchar(150) NOT NULL DEFAULT "",
	[AliasPage] varchar(150) NOT NULL DEFAULT ""
)
GO

CREATE TABLE [jcms_module_product] (
	[Id] AutoIncrement primary key ,
	[ChannelId] int NOT NULL DEFAULT "0",
	[ClassId] int NOT NULL DEFAULT "0",
	[Title] varchar(150) NOT NULL ,
	[TColor] varchar(8) NOT NULL DEFAULT "",
	[AddDate] datetime NOT NULL DEFAULT NOW(),
	[Summary] text NOT NULL DEFAULT "",
	[Editor] varchar(20) NOT NULL DEFAULT "",
	[Author] varchar(30) NOT NULL DEFAULT "",
	[Tags] varchar(60) NOT NULL DEFAULT "",
	[ViewNum] int NOT NULL DEFAULT "0",
	[IsPass] int NOT NULL DEFAULT "0",
	[IsImg] int NOT NULL DEFAULT "0",
	[Img] varchar(150) NOT NULL DEFAULT "",
	[IsTop] int NOT NULL DEFAULT "0",
	[IsFocus] int NOT NULL DEFAULT "0",
	[UserId] int NOT NULL DEFAULT "0",
	[ReadGroup] int NOT NULL DEFAULT "0",
	[SourceFrom] varchar(30) NOT NULL DEFAULT "",
	[Content] text NOT NULL,
	[Price0] int NOT NULL DEFAULT "0",
	[Points] int NOT NULL DEFAULT "0",
	[FirstPage] varchar(150) NOT NULL DEFAULT "",
	[AliasPage] varchar(150) NOT NULL DEFAULT ""
)
GO

CREATE TABLE [jcms_module_soft] (
	[Id] AutoIncrement primary key ,
	[ChannelId] int NOT NULL DEFAULT "0",
	[ClassId] int NOT NULL DEFAULT "0",
	[Title] varchar(150) NOT NULL ,
	[TColor] varchar(8) NOT NULL DEFAULT "",
	[AddDate] datetime NOT NULL DEFAULT NOW(),
	[Summary] text NOT NULL DEFAULT "",
	[Editor] varchar(20) NOT NULL DEFAULT "",
	[Author] varchar(30) NOT NULL DEFAULT "",
	[Tags] varchar(60) NOT NULL DEFAULT "",
	[ViewNum] int NOT NULL DEFAULT "0",
	[IsPass] int NOT NULL DEFAULT "0",
	[IsImg] int NOT NULL DEFAULT "0",
	[Img] varchar(150) NOT NULL DEFAULT "",
	[IsTop] int NOT NULL DEFAULT "0",
	[IsFocus] int NOT NULL DEFAULT "0",
	[UserId] int NOT NULL DEFAULT "0",
	[ReadGroup] int NOT NULL DEFAULT "0",
	[SourceFrom] varchar(30) NOT NULL DEFAULT "",
	[Version] varchar(100) NOT NULL DEFAULT "",
	[OperatingSystem] varchar(255) NULL ,
	[UnZipPass] varchar(100) NOT NULL DEFAULT "",
	[DemoUrl] varchar(255) NULL ,
	[RegUrl] varchar(255) NULL ,
	[SSize] varchar(20) NULL ,
	[Points] int NOT NULL DEFAULT "0",
	[DownUrl] text NOT NULL DEFAULT "",
	[DownNum] int NOT NULL DEFAULT "0",
	[Content] text NOT NULL DEFAULT "",
	[FirstPage] varchar(150) NOT NULL DEFAULT "",
	[AliasPage] varchar(150) NOT NULL DEFAULT ""
)
GO

CREATE TABLE [jcms_module_soft_downlogs] (
	[Id] AutoIncrement primary key ,
	[ChannelId] int NOT NULL DEFAULT "0",
	[UserId] int NOT NULL DEFAULT "0",
	[SoftId] int NOT NULL DEFAULT "0",
	[Points] int NOT NULL DEFAULT "0",
	[DownTime] datetime NOT NULL DEFAULT NOW(),
	[DownIP] varchar(16) NULL ,
	[DownDegree] int NOT NULL DEFAULT "0"
)
GO


CREATE TABLE [jcms_module_video] (
	[Id] AutoIncrement primary key ,
	[ChannelId] int NOT NULL DEFAULT "0",
	[ClassId] int NOT NULL DEFAULT "0",
	[Title] varchar(150) NOT NULL ,
	[TColor] varchar(8) NOT NULL DEFAULT "",
	[AddDate] datetime NOT NULL DEFAULT NOW(),
	[Summary] text NOT NULL DEFAULT "",
	[Editor] varchar(20) NOT NULL DEFAULT "",
	[Author] varchar(30) NOT NULL DEFAULT "",
	[Tags] varchar(60) NOT NULL DEFAULT "",
	[ViewNum] int NOT NULL DEFAULT "0",
	[IsPass] int NOT NULL DEFAULT "0",
	[IsImg] int NOT NULL DEFAULT "0",
	[Img] varchar(150) NOT NULL DEFAULT "",
	[IsTop] int NOT NULL DEFAULT "0",
	[IsFocus] int NOT NULL DEFAULT "0",
	[UserId] int NOT NULL DEFAULT "0",
	[ReadGroup] int NOT NULL DEFAULT "0",
	[SourceFrom] varchar(30) NOT NULL DEFAULT "",
	[VideoUrl] text NOT NULL DEFAULT "",
	[PageSize] int NOT NULL DEFAULT "0",
	[FirstPage] varchar(150) NOT NULL DEFAULT "",
	[AliasPage] varchar(150) NOT NULL DEFAULT ""
)
GO

CREATE TABLE [jcms_normal_adminlogs] (
	[Id] AutoIncrement primary key ,
	[AdminId] int NOT NULL DEFAULT "0",
	[OperInfo] varchar(200) NOT NULL DEFAULT "",
	[OperTime] datetime NOT NULL DEFAULT NOW(),
	[OperIP] varchar(15) NULL
)
GO

CREATE TABLE [jcms_normal_channel] (
	[Id] AutoIncrement primary key ,
	[Title] varchar(20) NOT NULL ,
	[SubDomain] varchar(100) NOT NULL DEFAULT "",
	[Info] varchar(200) NOT NULL DEFAULT "",
	[ClassDepth] int NOT NULL DEFAULT "0",
	[Dir] varchar(20) NOT NULL ,
	[pId] int NOT NULL DEFAULT "0",
	[ItemName] varchar(4) NOT NULL DEFAULT "",
	[ItemUnit] varchar(2) NULL ,
	[TemplateId] int NOT NULL DEFAULT "0",
	[Type] varchar(10) NOT NULL DEFAULT "",
	[Enabled] int NOT NULL DEFAULT "0",
	[DefaultThumbs] int NOT NULL DEFAULT "0",
	[IsPost] int NOT NULL DEFAULT "0",
	[IsNav] int NOT NULL DEFAULT "0",
	[IsHtml] int NOT NULL DEFAULT "0",
	[IsTop] int NOT NULL DEFAULT "0",
	[UploadPath] varchar(100) NOT NULL DEFAULT "",
	[UploadType] varchar(50) NOT NULL DEFAULT "",
	[UploadSize] int NOT NULL DEFAULT "0",
	[LanguageCode] varchar(20) NOT NULL DEFAULT "cn"
)
GO

INSERT INTO [jcms_normal_channel] ([Title],[ClassDepth],[Dir],[pId],[ItemName],[ItemUnit],[TemplateId],[Type],[Enabled],[DefaultThumbs],[IsPost],[IsNav],[IsHtml],[UploadPath],[UploadType],[UploadSize]) VALUES ('新闻中心',2,'html/news',1,'内容','篇',2,'article',1,1,1,0,1,'<#SiteDir#><#ChannelDir#>/uploadfiles/','*.jpg;*.gif;*.rar;*.zip;',10240)
GO
INSERT INTO [jcms_normal_channel] ([Title],[ClassDepth],[Dir],[pId],[ItemName],[ItemUnit],[TemplateId],[Type],[Enabled],[DefaultThumbs],[IsPost],[IsNav],[IsHtml],[UploadPath],[UploadType],[UploadSize]) VALUES ('图片中心',2,'html/photo',2,'图片','组',8,'photo',1,1,1,0,1,'<#SiteDir#><#ChannelDir#>/uploadfiles/','*.jpg;*.bmp;*.gif;*.png;',1024)
GO
INSERT INTO [jcms_normal_channel] ([Title],[ClassDepth],[Dir],[pId],[ItemName],[ItemUnit],[TemplateId],[Type],[Enabled],[DefaultThumbs],[IsPost],[IsNav],[IsHtml],[UploadPath],[UploadType],[UploadSize]) VALUES ('下载中心',2,'html/down',3,'文件','个',5,'soft',1,1,1,0,1,'<#SiteDir#><#ChannelDir#>/uploadfiles/','*.rar;*.zip;*.7z;',5120)
GO
INSERT INTO [jcms_normal_channel] ([Title],[ClassDepth],[Dir],[pId],[ItemName],[ItemUnit],[TemplateId],[Type],[Enabled],[DefaultThumbs],[IsPost],[IsNav],[IsHtml],[UploadPath],[UploadType],[UploadSize]) VALUES ('视频中心',2,'html/video',4,'视频','个',11,'video',1,1,1,0,1,'<#SiteDir#><#ChannelDir#>/uploadfiles/','*.flv;*.avi;*.swf;',20480)
GO
INSERT INTO [jcms_normal_channel] ([Title],[ClassDepth],[Dir],[pId],[ItemName],[ItemUnit],[TemplateId],[Type],[Enabled],[DefaultThumbs],[IsPost],[IsNav],[IsHtml],[UploadPath],[UploadType],[UploadSize]) VALUES ('产品中心',2,'html/product',5,'产品','个',14,'product',1,1,1,0,1,'<#SiteDir#><#ChannelDir#>/uploadfiles/','*.jpg;*.gif;*.png;',1024)
GO

CREATE TABLE [jcms_normal_class] (
	[Id] AutoIncrement primary key ,
	[ChannelId] int NOT NULL DEFAULT "0",
	[ParentId] int NOT NULL DEFAULT "0",
	[Title] varchar(40) NOT NULL ,
	[Info] varchar(100) NOT NULL DEFAULT "",
	[Img] varchar(150) NOT NULL DEFAULT "",
	[SortRank] int NOT NULL DEFAULT "0",
	[Folder] varchar(50) NOT NULL DEFAULT "",
	[FilePath] varchar(150) NOT NULL DEFAULT "",
	[Code] varchar(40) NOT NULL DEFAULT "",
	[IsPost] int NOT NULL DEFAULT "0",
	[IsTop] int NOT NULL DEFAULT "0",
	[TopicNum] int NOT NULL DEFAULT "0",
	[TemplateId] int NOT NULL DEFAULT "0",
	[ContentTemp] int NOT NULL DEFAULT "0",
	[PageSize] int NOT NULL DEFAULT "0",
	[IsOut] int NOT NULL DEFAULT "0",
	[ReadGroup] int NOT NULL DEFAULT "0",
	[FirstPage] varchar(150) NOT NULL DEFAULT "",
	[AliasPage] varchar(150) NOT NULL DEFAULT ""
)
GO

CREATE TABLE [jcms_normal_extends] (
	[Id] AutoIncrement primary key ,
	[Title] varchar(20) NOT NULL DEFAULT "",
	[Name] varchar(20) NOT NULL DEFAULT "",
	[Author] varchar(20) NOT NULL DEFAULT "",
	[Info] varchar(50) NOT NULL DEFAULT "",
	[Type] int NOT NULL DEFAULT "0",
	[pId] int NOT NULL DEFAULT "0",
	[BaseTable] varchar(200) NOT NULL DEFAULT "",
	[ManageUrl] varchar(50) NOT NULL DEFAULT "",
	[Enabled] int NOT NULL DEFAULT "0",
	[Locked] int NOT NULL DEFAULT "0"
)
GO

INSERT INTO [jcms_normal_extends] ([Title],[Name],[Author],[Info],[Type],[pId],[BaseTable],[ManageUrl],[Enabled],[Locked]) VALUES ('网站公告','Placard','jumbot','官方插件',0,4,'jcms_extends_placard','admin.aspx',1,0)
GO
INSERT INTO [jcms_normal_extends] ([Title],[Name],[Author],[Info],[Type],[pId],[BaseTable],[ManageUrl],[Enabled],[Locked]) VALUES ('投票调查','Vote','jumbot','官方插件',0,6,'jcms_extends_vote','admin.aspx',1,0)
GO
INSERT INTO [jcms_normal_extends] ([Title],[Name],[Author],[Info],[Type],[pId],[BaseTable],[ManageUrl],[Enabled],[Locked]) VALUES ('QQ在线客服','QQOnline','jumbot','官方插件',0,7,'jcms_extends_qqonline','admin.aspx',1,0)
GO

CREATE TABLE [jcms_normal_forbidip] (
	[Id] AutoIncrement primary key ,
	[StartIP] float NULL ,
	[StartIP2] varchar(16) NULL ,
	[EndIP] float NULL ,
	[EndIP2] varchar(16) NULL ,
	[ExpireDate] datetime NOT NULL DEFAULT NOW(),
	[Enabled] int NOT NULL DEFAULT "0"
)
GO

CREATE TABLE [jcms_normal_modules] (
	[Id] AutoIncrement primary key ,
	[Title] varchar(100) NOT NULL ,
	[Type] varchar(10) NOT NULL DEFAULT "",
	[ItemName] varchar(4) NOT NULL DEFAULT "",
	[ItemUnit] varchar(2) NULL ,
	[PId] int NOT NULL DEFAULT "0",
	[Enabled] int NOT NULL DEFAULT "0",
	[Locked] int NOT NULL DEFAULT "0",
	[SearchFieldValues] varchar(200) NOT NULL DEFAULT "",
	[SearchFieldTexts] varchar(200) NULL
)
GO
INSERT INTO [jcms_normal_modules] (Title,Type,pId,Enabled,Locked,SearchFieldValues,SearchFieldTexts,ItemName,ItemUnit) values('文章','article',1,1,1,'title,summary','标题,简介','内容','篇')
GO
INSERT INTO [jcms_normal_modules] (Title,Type,pId,Enabled,Locked,SearchFieldValues,SearchFieldTexts,ItemName,ItemUnit) values('图片','photo',2,1,1,'title,summary','标题,简介','图片','组')
GO
INSERT INTO [jcms_normal_modules] (Title,Type,pId,Enabled,Locked,SearchFieldValues,SearchFieldTexts,ItemName,ItemUnit) values('下载','soft',3,1,1,'title,summary','标题,简介','文件','个')
GO
INSERT INTO [jcms_normal_modules] (Title,Type,pId,Enabled,Locked,SearchFieldValues,SearchFieldTexts,ItemName,ItemUnit) values('视频','video',4,1,1,'title,summary','标题,简介','视频','个')
GO
INSERT INTO [jcms_normal_modules] (Title,Type,pId,Enabled,Locked,SearchFieldValues,SearchFieldTexts,ItemName,ItemUnit) values('文档','document',6,1,1,'title,summary','标题,简介','文档','篇')
GO
INSERT INTO [jcms_normal_modules] (Title,Type,pId,Enabled,Locked,SearchFieldValues,SearchFieldTexts,ItemName,ItemUnit) values('文库','paper',7,1,1,'title,summary','标题,简介','文件','篇')
GO
INSERT INTO [jcms_normal_modules] (Title,Type,pId,Enabled,Locked,SearchFieldValues,SearchFieldTexts,ItemName,ItemUnit) values('产品','product',5,1,1,'title,summary','标题,简介','商品','个')
GO

CREATE TABLE [jcms_normal_page] (
	[Id] AutoIncrement primary key ,
	[Title] varchar(20) NOT NULL DEFAULT "",
	[Source] varchar(50) NULL,
	[OutUrl] varchar(100) NULL
)
GO
INSERT INTO [jcms_normal_page] (Title,Source,OutUrl) VALUES ('网站地图','system_sitemap.htm','/sitemap.shtml')
GO
INSERT INTO [jcms_normal_page] (Title,Source,OutUrl) VALUES ('RSS订阅','system_rss.htm','/rss.shtml')
GO
INSERT INTO [jcms_normal_page] (Title,Source,OutUrl) VALUES ('帮助中心','system_help.htm','/help.shtml')
GO



CREATE TABLE [jcms_normal_pointscard] (
	[Id] AutoIncrement primary key ,
	[CardNumber] varchar(16) NULL ,
	[CardPassword] varchar(32) NULL ,
	[UserId] int NOT NULL DEFAULT "0",
	[Points] int NOT NULL DEFAULT "0",
	[LimitedDate] datetime NOT NULL DEFAULT NOW(),
	[ActiveTime] datetime NOT NULL DEFAULT NOW(),
	[ActiveIP] varchar(15) NULL ,
	[State] int NOT NULL DEFAULT "0"
)
GO

CREATE TABLE [jcms_normal_pointscard_sort] (
	[Id] AutoIncrement primary key ,
	[Title] varchar(50) NOT NULL DEFAULT "",
	[Points] int NOT NULL
)
GO
INSERT INTO [jcms_normal_pointscard_sort] (Title,Points) values('50元=>50点',50)
GO
INSERT INTO [jcms_normal_pointscard_sort] (Title,Points) values('100元=>100点',100)
GO
INSERT INTO [jcms_normal_pointscard_sort] (Title,Points) values('200元=>200点',200)
GO
INSERT INTO [jcms_normal_pointscard_sort] (Title,Points) values('500元=>500点',500)
GO
INSERT INTO [jcms_normal_pointscard_sort] (Title,Points) values('1000元=>1000点',1000)
GO

CREATE TABLE [jcms_normal_special] (
	[Id] AutoIncrement primary key ,
	[Title] varchar(100) NOT NULL DEFAULT "",
	[Info] varchar(200) NOT NULL DEFAULT "",
	[Source] varchar(50) NULL
)
GO

CREATE TABLE [jcms_normal_specialcontent] (
	[Id] AutoIncrement primary key ,
	[Title] varchar(100) NOT NULL DEFAULT "",
	[sId] int NOT NULL DEFAULT "0",
	[ChannelId] int NOT NULL DEFAULT "0",
	[ContentId] int NOT NULL
)
GO

CREATE TABLE [jcms_normal_tag] (
	[Id] AutoIncrement primary key ,
	[ChannelId] int NOT NULL DEFAULT "0",
	[Title] varchar(15) NOT NULL ,
	[ClickTimes] int NOT NULL DEFAULT "0"
)
GO

CREATE TABLE [jcms_normal_template] (
	[Id] AutoIncrement primary key ,
	[Title] varchar(100) NOT NULL DEFAULT "",
	[PId] int NOT NULL DEFAULT "0",
	[Type] varchar(50) NOT NULL DEFAULT "",
	[SType] varchar(50) NOT NULL DEFAULT "",
	[IsDefault] int NOT NULL DEFAULT "0",
	[Source] varchar(50) NOT NULL DEFAULT ""
)
GO
INSERT INTO [jcms_normal_template] (Title,pId,Type,sType,IsDefault,Source) values('默认模板站点首页',1,'system','index',1,'system_index.htm')
GO
INSERT INTO [jcms_normal_template] (Title,pId,Type,sType,IsDefault,Source) values('默认模板文章频道页',1,'article','channel',1,'article_channel.htm')
GO
INSERT INTO [jcms_normal_template] (Title,pId,Type,sType,IsDefault,Source) values('默认模板文章列表页',1,'article','class',1,'article_class.htm')
GO
INSERT INTO [jcms_normal_template] (Title,pId,Type,sType,IsDefault,Source) values('默认模板文章内容页',1,'article','content',1,'article_content.htm')
GO
INSERT INTO [jcms_normal_template] (Title,pId,Type,sType,IsDefault,Source) values('默认模板下载频道页',1,'soft','channel',1,'soft_channel.htm')
GO
INSERT INTO [jcms_normal_template] (Title,pId,Type,sType,IsDefault,Source) values('默认模板下载列表页',1,'soft','class',1,'soft_class.htm')
GO
INSERT INTO [jcms_normal_template] (Title,pId,Type,sType,IsDefault,Source) values('默认模板下载内容页',1,'soft','content',1,'soft_content.htm')
GO
INSERT INTO [jcms_normal_template] (Title,pId,Type,sType,IsDefault,Source) values('默认模板图片频道页',1,'photo','channel',1,'photo_channel.htm')
GO
INSERT INTO [jcms_normal_template] (Title,pId,Type,sType,IsDefault,Source) values('默认模板图片列表页',1,'photo','class',1,'photo_class.htm')
GO
INSERT INTO [jcms_normal_template] (Title,pId,Type,sType,IsDefault,Source) values('默认模板图片内容页',1,'photo','content',1,'photo_content.htm')
GO
INSERT INTO [jcms_normal_template] (Title,pId,Type,sType,IsDefault,Source) values('默认模板视频频道页',1,'video','channel',1,'video_channel.htm')
GO
INSERT INTO [jcms_normal_template] (Title,pId,Type,sType,IsDefault,Source) values('默认模板视频列表页',1,'video','class',1,'video_class.htm')
GO
INSERT INTO [jcms_normal_template] (Title,pId,Type,sType,IsDefault,Source) values('默认模板视频内容页',1,'video','content',1,'video_content.htm')
GO
INSERT INTO [jcms_normal_template] (Title,pId,Type,sType,IsDefault,Source) values('默认模板产品频道页',1,'product','channel',1,'product_channel.htm')
GO
INSERT INTO [jcms_normal_template] (Title,pId,Type,sType,IsDefault,Source) values('默认模板产品列表页',1,'product','class',1,'product_class.htm')
GO
INSERT INTO [jcms_normal_template] (Title,pId,Type,sType,IsDefault,Source) values('默认模板产品内容页',1,'product','content',1,'product_content.htm')
GO
INSERT INTO [jcms_normal_template] (Title,pId,Type,sType,IsDefault,Source) values('默认模板文档频道页',1,'document','channel',1,'document_channel.htm')
GO
INSERT INTO [jcms_normal_template] (Title,pId,Type,sType,IsDefault,Source) values('默认模板文档列表页',1,'document','class',1,'document_class.htm')
GO
INSERT INTO [jcms_normal_template] (Title,pId,Type,sType,IsDefault,Source) values('默认模板文档内容页',1,'document','content',1,'document_content.htm')
GO
INSERT INTO [jcms_normal_template] (Title,pId,Type,sType,IsDefault,Source) values('默认模板文库频道页',1,'paper','channel',1,'paper_channel.htm')
GO
INSERT INTO [jcms_normal_template] (Title,pId,Type,sType,IsDefault,Source) values('默认模板文库列表页',1,'paper','class',1,'paper_class.htm')
GO
INSERT INTO [jcms_normal_template] (Title,pId,Type,sType,IsDefault,Source) values('默认模板文库内容页',1,'paper','content',1,'paper_content.htm')
GO


CREATE TABLE [jcms_normal_templateproject] (
	[Id] AutoIncrement primary key ,
	[Title] varchar(100) NOT NULL DEFAULT "",
	[Info] varchar(200) NOT NULL DEFAULT "",
	[Dir] varchar(50) NOT NULL DEFAULT "",
	[IsDefault] int NOT NULL DEFAULT "0"
)
GO
INSERT INTO [jcms_normal_templateproject] (Title,Info,Dir,isDefault) values('默认模板','','default',1)
GO

CREATE TABLE [jcms_normal_thumbs] (
	[Id] AutoIncrement primary key ,
	[ChannelId] int NOT NULL DEFAULT "0",
	[Title] varchar(30) NOT NULL DEFAULT "",
	[iWidth] int NOT NULL DEFAULT "0",
	[iHeight] int NOT NULL DEFAULT "0"
)
GO
INSERT INTO [jcms_normal_thumbs] (ChannelId,Title,iWidth,iHeight) Values (0,'360X270(4:3)',360,270)
GO
INSERT INTO [jcms_normal_thumbs] (ChannelId,Title,iWidth,iHeight) Values (0,'200X150(3:4)',200,150)
GO
INSERT INTO [jcms_normal_thumbs] (ChannelId,Title,iWidth,iHeight) Values (0,'270X360(3:4)',270,360)
GO
INSERT INTO [jcms_normal_thumbs] (ChannelId,Title,iWidth,iHeight) Values (0,'150X200(3:4)',150,200)
GO
INSERT INTO [jcms_normal_thumbs] (ChannelId,Title,iWidth,iHeight) Values (0,'240X240(1:1)',240,240)
GO
INSERT INTO [jcms_normal_thumbs] (ChannelId,Title,iWidth,iHeight) Values (0,'150X150(1:1)',150,150)
GO

CREATE TABLE [jcms_normal_user] (
	[Id] AutoIncrement primary key ,
	[GUID] varchar(40) NOT NULL DEFAULT "",
	[UserName] varchar(40) NOT NULL DEFAULT "",
	[NickName] varchar(40) NOT NULL DEFAULT "",
	[UserPass] varchar(40) NOT NULL DEFAULT "",
	[TrueName] varchar(20) NOT NULL DEFAULT "",
	[Question] varchar(30) NOT NULL DEFAULT "",
	[Answer] varchar(20) NOT NULL DEFAULT "",
	[Sex] int NOT NULL DEFAULT "0",
	[Email] varchar(80) NOT NULL DEFAULT "",
	[Group] int NOT NULL DEFAULT "0",
	[State] int NOT NULL DEFAULT "0",
	[Cookies] varchar(10) NOT NULL DEFAULT "",
	[RegTime] datetime NOT NULL DEFAULT NOW(),
	[RegIp] varchar(15) NULL ,
	[LastTime] datetime NOT NULL DEFAULT NOW(),
	[LastIp] varchar(15) NULL ,
	[HomePage] varchar(100) NOT NULL DEFAULT "",
	[QQ] varchar(50) NOT NULL DEFAULT "",
	[ICQ] varchar(50) NOT NULL DEFAULT "",
	[MSN] varchar(50) NOT NULL DEFAULT "",
	[BirthDay] varchar(50) NOT NULL DEFAULT "",
	[Signature] varchar(30) NOT NULL DEFAULT "",
	[ProvinceCity] varchar(40) NOT NULL DEFAULT "江苏-苏州",
	[Login] int NOT NULL DEFAULT "0",
	[Points] int NOT NULL DEFAULT "0",
	[IDType] int NOT NULL DEFAULT "0",
	[IDCard] varchar(30) NOT NULL DEFAULT "",
	[WorkUnit] varchar(100) NOT NULL DEFAULT "",
	[Address] varchar(100) NOT NULL DEFAULT "",
	[ZipCode] varchar(10) NOT NULL DEFAULT "",
	[Telephone] varchar(20) NOT NULL DEFAULT "",
	[MobileTel] varchar(11) NULL ,
	[IsVIP] int NOT NULL DEFAULT "0",
	[VIPTime] datetime NOT NULL DEFAULT NOW(),
	[Integral] int NOT NULL DEFAULT "0",
	[UserSign] varchar(64) NULL ,
	[AdminId] int NOT NULL DEFAULT "0",
	[AdminName] varchar(20) NOT NULL DEFAULT "",
	[AdminPass] varchar(32) NULL DEFAULT "",
	[Setting] text NOT NULL DEFAULT "",
	[LastTime2] datetime NOT NULL DEFAULT NOW(),
	[LastIp2] varchar(15) NULL ,
	[Cookiess] varchar(10) NOT NULL DEFAULT "",
	[AdminSign] varchar(64) NULL ,
	[AdminState] int NOT NULL DEFAULT "0",
	[ForumName] varchar(20) NOT NULL DEFAULT "",
	[ForumPass] varchar(32) NULL DEFAULT "",
	[ServiceId] int NOT NULL DEFAULT "0",
	[ServiceName] varchar(20) NOT NULL DEFAULT "",
	[LastTime3] datetime NOT NULL DEFAULT NOW(),
	[LastIp3] varchar(15) NULL,
	[Token_Sina] varchar(32) NULL DEFAULT "",
	[Token_Tencent] varchar(32) NULL DEFAULT "",
	[Token_Renren] varchar(32) NULL DEFAULT "",
	[Token_Baidu] varchar(32) NULL DEFAULT "",
	[Token_Kaixin] varchar(32) NULL DEFAULT ""
)
GO

CREATE TABLE [jcms_normal_user_logs] (
	[Id] AutoIncrement primary key ,
	[UserId] int NOT NULL DEFAULT "0",
	[OperInfo] varchar(200) NOT NULL DEFAULT "",
	[OperType] int NOT NULL DEFAULT "0",
	[OperTime] datetime NOT NULL DEFAULT NOW(),
	[OperIP] varchar(15) NULL
)
GO

CREATE TABLE [jcms_normal_user_friends] (
	[Id] AutoIncrement primary key ,
	[UserId] int NOT NULL DEFAULT "0",
	[AddDate] datetime NOT NULL DEFAULT NOW(),
	[FriendId] int NOT NULL
)
GO

CREATE TABLE [jcms_normal_user_message] (
	[Id] AutoIncrement primary key ,
	[Title] varchar(50) NOT NULL DEFAULT "",
	[Content] text NOT NULL DEFAULT "",
	[SendIP] varchar(15) NULL ,
	[SendUserId] int NOT NULL DEFAULT "0",
	[ReceiveUserId] int NOT NULL DEFAULT "0",
	[ReceiveUserName] varchar(20) NOT NULL DEFAULT "",
	[AddDate] datetime NOT NULL DEFAULT NOW(),
	[ReadTime] datetime NOT NULL DEFAULT NOW(),
	[State] int NOT NULL DEFAULT "0"
)
GO

CREATE TABLE [jcms_normal_user_favorite] (
	[Id] AutoIncrement primary key ,
	[Title] varchar(150) NOT NULL ,
	[ModuleType] varchar(20) NOT NULL DEFAULT "",
	[ChannelId] int NOT NULL DEFAULT "0",
	[ContentId] int NOT NULL DEFAULT "0",
	[AddDate] datetime NOT NULL DEFAULT NOW(),
	[UserId] int NOT NULL
)
GO

CREATE TABLE [jcms_normal_user_notice] (
	[Id] AutoIncrement primary key ,
	[Title] varchar(50) NOT NULL DEFAULT "",
	[Content] varchar(250) NOT NULL DEFAULT "",
	[NoticeType] varchar(16) NULL ,
	[UserId] int NOT NULL DEFAULT "0",
	[AddDate] datetime NOT NULL DEFAULT NOW(),
	[ReadTime] datetime NOT NULL DEFAULT NOW(),
	[State] int NOT NULL DEFAULT "0"
)
GO

CREATE TABLE [jcms_normal_usergroup] (
	[Id] AutoIncrement primary key ,
	[GroupName] varchar(50) NOT NULL DEFAULT "",
	[Setting] text NOT NULL DEFAULT "",
	[IsLogin] int NOT NULL DEFAULT "0",
	[UserTotal] int NOT NULL DEFAULT "0"
)
GO
INSERT INTO [jcms_normal_usergroup] (GroupName,setting,IsLogin,UserTotal) Values('临时用户','1,1,1,0|23,1,10,10,1,0,1,1,5,1,1,5,1,1,5,',1,0)
GO
INSERT INTO [jcms_normal_usergroup] (GroupName,setting,IsLogin,UserTotal) Values('初级用户','1,1,1,0|23,1,10,10,1,0,1,1,10,1,1,10,1,1,10,',1,0)
GO
INSERT INTO [jcms_normal_usergroup] (GroupName,setting,IsLogin,UserTotal) Values('中级用户','1,1,1,0|23,1,10,10,1,0,1,1,50,1,1,50,1,1,50,',1,0)
GO
INSERT INTO [jcms_normal_usergroup] (GroupName,setting,IsLogin,UserTotal) Values('高级用户','1,1,1,0|23,1,10,10,1,0,1,1,100,1,1,100,1,1,100,',1,0)
GO
INSERT INTO [jcms_normal_usergroup] (GroupName,setting,IsLogin,UserTotal) Values('管理用户','1,1,1,0|23,1,10,10,1,0,1,1,100,1,1,100,1,1,100,',1,0)
GO


CREATE TABLE [jcms_normal_email] (
	[Id] AutoIncrement primary key ,
	[NickName] varchar(20) NULL DEFAULT "",
	[EmailAddress] varchar(80) NOT NULL DEFAULT "",
	[State] int NOT NULL DEFAULT "0",
	[GroupId] int NOT NULL DEFAULT "0",
	[SignCode] varchar(64) NOT NULL DEFAULT "",
        [SuccessTimes] int NOT NULL DEFAULT "0",
        [FailureTimes] int NOT NULL DEFAULT "0"
)
GO

CREATE TABLE [jcms_normal_emailserver] (
	[Id] AutoIncrement primary key ,
	[FromAddress] varchar(100) NOT NULL ,
	[FromName] varchar(30) NOT NULL DEFAULT "",
	[FromPwd] varchar(32) NOT NULL DEFAULT "",
	[SmtpHost] varchar(60) NOT NULL DEFAULT "",
	[Enabled] int NOT NULL DEFAULT "1"
)
GO

CREATE TABLE [jcms_normal_emailgroup] (
	[Id] AutoIncrement primary key ,
	[GroupName] varchar(50) NOT NULL DEFAULT "",
	[EmailTotal] int NOT NULL DEFAULT "0"
)
GO

INSERT INTO [jcms_normal_emailgroup] (GroupName,EmailTotal) Values('临时用户',0)
GO
INSERT INTO [jcms_normal_emailgroup] (GroupName,EmailTotal) Values('初级用户',0)
GO
INSERT INTO [jcms_normal_emailgroup] (GroupName,EmailTotal) Values('中级用户',0)
GO
INSERT INTO [jcms_normal_emailgroup] (GroupName,EmailTotal) Values('高级用户',0)
GO

CREATE TABLE [jcms_normal_emaillogs] (
	[Id] AutoIncrement primary key ,
	[AdminId] int NOT NULL DEFAULT "0",
	[SendTitle] varchar(80) NOT NULL DEFAULT "",
	[SendUsers] text NOT NULL DEFAULT "",
	[SendTime] datetime NOT NULL DEFAULT NOW(),
	[SendIP] varchar(15) NULL
)
GO


CREATE TABLE [jcms_normal_adminpower] (
	[Id] AutoIncrement primary key ,
	[Title] varchar(20) NOT NULL DEFAULT "",
	[Code] varchar(30) NOT NULL DEFAULT "",
	[PId] int NOT NULL DEFAULT "0",
	[Enabled] int NOT NULL DEFAULT "1"
)
GO

INSERT INTO [jcms_normal_adminpower] (Title,PId,Code) VALUES ('模块管理',2,'templateinclude-mng')
GO
INSERT INTO [jcms_normal_adminpower] (Title,PId,Code) VALUES ('专题管理',3,'special-mng')
GO
INSERT INTO [jcms_normal_adminpower] (Title,PId,Code) VALUES ('广告管理',4,'adv-mng')
GO
INSERT INTO [jcms_normal_adminpower] (Title,PId,Code) VALUES ('外站调用管理',5,'js-mng')
GO



CREATE TABLE [jcms_normal_templateinclude] (
	[Id] AutoIncrement primary key ,
	[Title] varchar(100) NOT NULL DEFAULT "",
	[Info] varchar(200) NOT NULL DEFAULT "",
	[PId] int NOT NULL DEFAULT "0",
	[Sort] int NOT NULL DEFAULT "0",
	[NeedBuild] int NOT NULL DEFAULT "0",
	[Source] varchar(100) NOT NULL DEFAULT ""
)
GO

INSERT INTO [jcms_normal_templateinclude] (Title,PId,Sort,NeedBuild,Source) VALUES ('公用头部文件',1,1,1,'header.htm')
GO
INSERT INTO [jcms_normal_templateinclude] (Title,PId,Sort,NeedBuild,Source) VALUES ('公用尾部文件',1,2,1,'footer.htm')
GO
INSERT INTO [jcms_normal_templateinclude] (Title,PId,Sort,NeedBuild,Source) VALUES ('百度分享',1,3,1,'share.htm')
GO

CREATE TABLE [jcms_normal_javascript] (
	[Id] AutoIncrement primary key ,
	[Title] varchar(50) NOT NULL ,
	[Code] varchar(64) NOT NULL ,
	[TemplateContent] text NOT NULL DEFAULT ""
)
GO

CREATE TABLE [jcms_normal_advclass] (
	[Id] AutoIncrement primary key ,
	[Title] varchar(10) NOT NULL DEFAULT "",
	[Code] varchar(10) NOT NULL DEFAULT ""
)
GO


INSERT INTO [jcms_normal_advclass] (Title,Code) values('图片','img')
GO
INSERT INTO [jcms_normal_advclass] (Title,Code) values('动画','flash')
GO
INSERT INTO [jcms_normal_advclass] (Title,Code) values('植入网页','iframe')
GO
INSERT INTO [jcms_normal_advclass] (Title,Code) values('嵌入代码','html')
GO


CREATE TABLE [jcms_normal_adv] (
	[Id] AutoIncrement primary key ,
	[Title] varchar(50) NOT NULL DEFAULT "",
	[AddDate] datetime NOT NULL DEFAULT NOW(),
	[Content] text NOT NULL DEFAULT "",
	[State] int NOT NULL DEFAULT "0",
	[AdvType] varchar(10) NOT NULL DEFAULT "",
	[Url] varchar(220) NOT NULL DEFAULT "",
	[Picurl] varchar(220) NOT NULL DEFAULT "",
	[Width] int NOT NULL DEFAULT "0",
	[Height] int NOT NULL DEFAULT "0"
)
GO


INSERT INTO [jcms_normal_adv] (Title,AddDate,Content,State,AdvType,Url,Picurl,Width,Height) VALUES ('google468x60','2011-2-14 11:10:38','<script>
google_ad_client = "pub-6117841416763262";
google_ad_slot = "8858973439";
google_ad_width = 468;
google_ad_height = 60;
</script>
<script type="text/javascript" src="http://pagead2.googlesyndication.com/pagead/show_ads.js"></script>',1,'html','','',468,60)
GO

INSERT INTO [jcms_normal_adv] (Title,AddDate,Content,State,AdvType,Url,Picurl,Width,Height) VALUES ('google728x15','2011-2-14 11:10:38','<script>
google_ad_client = "pub-6117841416763262";
google_ad_slot = "0453230926";
google_ad_width = 728;
google_ad_height = 15;
</script>
<script type="text/javascript" src="http://pagead2.googlesyndication.com/pagead/show_ads.js"></script>',1,'html','','',728,15)
GO

INSERT INTO [jcms_normal_adv] (Title,AddDate,Content,State,AdvType,Url,Picurl,Width,Height) VALUES ('google468x15','2011-2-14 11:10:38','<script>
google_ad_client = "pub-6117841416763262";
google_ad_slot = "3976506009";
google_ad_width = 468;
google_ad_height = 15;
</script>
<script type="text/javascript" src="http://pagead2.googlesyndication.com/pagead/show_ads.js"></script>',1,'html','','',468,15)
GO

INSERT INTO [jcms_normal_adv] (Title,AddDate,Content,State,AdvType,Url,Picurl,Width,Height) VALUES ('知远防务234*60','2011-2-14 11:10:38','',1,'img','http://www.knowfar.org.cn','/_data/style/knowfar234X60.gif',234,60)
GO

INSERT INTO [jcms_normal_adv] (Title,AddDate,Content,State,AdvType,Url,Picurl,Width,Height) VALUES ('百度联盟250*250','2011-2-14 11:10:38','<script type="text/javascript">/*百度联盟250*250*/ var cpro_id = "u793380";</script><script src="http://cpro.baidu.com/cpro/ui/c.js" type="text/javascript"></script>',1,'html','','',250,250)
GO


CREATE TABLE [jcms_extends_placard] (
	[Id] AutoIncrement primary key ,
	[Title] varchar(150) NOT NULL DEFAULT "",
	[Content] text NOT NULL DEFAULT "" ,
	[AddTime] datetime NULL ,
	[State] int NOT NULL DEFAULT "0"
)
GO

CREATE TABLE [jcms_extends_vote] (
	[Id] AutoIncrement primary key ,
	[Title] varchar(100) NOT NULL DEFAULT "",
	[VoteText] text NOT NULL DEFAULT "",
	[VoteNum] varchar(50) NOT NULL DEFAULT "" ,
	[VoteTotal] int NOT NULL DEFAULT "0",
	[Type] int NOT NULL DEFAULT "0",
	[Lock] int NOT NULL DEFAULT "0"
)
GO

CREATE TABLE [jcms_extends_qqonline] (
	[Id] AutoIncrement primary key ,
	[QQID] varchar(14) NOT NULL DEFAULT "",
	[Title] varchar(10) NOT NULL DEFAULT "",
	[TColor] varchar(8) NOT NULL DEFAULT "" ,
	[Face] varchar(4) NOT NULL DEFAULT "" ,
	[OrderNum] int NOT NULL DEFAULT "0" ,
	[State] int NOT NULL DEFAULT "0"
)
GO

INSERT INTO [jcms_extends_qqonline] ([QQID],[Title],[TColor],[Face],[OrderNum],[State]) VALUES ('4259024','项目合作','#FF3300','1',3,1)
GO
INSERT INTO [jcms_extends_qqonline] ([QQID],[Title],[TColor],[Face],[OrderNum],[State]) VALUES ('112561265','授权服务','#111111','1',3,1)
GO



CREATE TABLE [jcms_normal_recharge] (
	[Id] AutoIncrement primary key ,
	[OrderNum] varchar(30) NOT NULL DEFAULT "",
	[PaymentWay] varchar(10) NOT NULL DEFAULT "",
	[Points] int NOT NULL DEFAULT "0",
	[OrderTime] datetime NOT NULL DEFAULT NOW(),
	[OrderIP] varchar(16) NULL ,
	[State] int NOT NULL DEFAULT "0",
	[UserId] int NOT NULL DEFAULT "0"
)
GO

CREATE TABLE [jcms_normal_user_order] (
	[Id] AutoIncrement primary key ,
	[OrderNum] varchar(30) NOT NULL DEFAULT "",
	[TrueName] varchar(20) NOT NULL DEFAULT "",
	[Address] varchar(100) NOT NULL DEFAULT "",
	[ZipCode] varchar(10) NOT NULL DEFAULT "",
	[MobileTel] varchar(11) NULL ,
	[PaymentWay] varchar(10) NOT NULL DEFAULT "",
	[Money] float NOT NULL DEFAULT "0",
	[OrderTime] datetime NOT NULL DEFAULT NOW(),
	[OrderIP] varchar(16) NULL ,
	[State] int NOT NULL DEFAULT "0",
	[UserId] int NOT NULL DEFAULT "0"
)
GO

CREATE TABLE [jcms_normal_user_cart] (
	[Id] AutoIncrement primary key ,
	[ProductId] int NOT NULL DEFAULT "0",
	[ProductLink] varchar(150) NOT NULL ,
	[BuyCount] int NOT NULL DEFAULT "1",
	[CartTime] datetime NOT NULL DEFAULT NOW(),
	[State] int NOT NULL DEFAULT "0",
	[UserId] int NOT NULL DEFAULT "0"
)
GO

CREATE TABLE [jcms_normal_user_goods] (
	[Id] AutoIncrement primary key ,
	[OrderNum] varchar(30) NOT NULL DEFAULT "",
	[ProductId] int NOT NULL DEFAULT "0",
	[ProductName] varchar(150) NOT NULL ,
	[ProductImg] varchar(150) NOT NULL ,
	[ProductLink] varchar(150) NOT NULL ,
	[UnitPrice] float NOT NULL DEFAULT "0",
	[BuyCount] int NOT NULL DEFAULT "1",
	[TotalPrice] float NOT NULL DEFAULT "0",
	[GoodsTime] datetime NOT NULL DEFAULT NOW(),
	[State] int NOT NULL DEFAULT "0",
	[UserId] int NOT NULL DEFAULT "0"
)
GO


CREATE TABLE [jcms_normal_question] (
	[Id] AutoIncrement primary key ,
	[ParentId] int NOT NULL DEFAULT "0" ,
	[AddDate] datetime NULL ,
	[Title] varchar(50) NOT NULL DEFAULT "" ,
	[Content] varchar(250) NOT NULL DEFAULT "" ,
	[IP] varchar(15) NOT NULL DEFAULT "" ,
	[UserName] varchar(50) NOT NULL DEFAULT "" ,
	[UserId] int NOT NULL DEFAULT "0" ,
	[ClassId] int NOT NULL DEFAULT "0" ,
	[IsPass] int NOT NULL DEFAULT "0"
)
GO

CREATE TABLE [jcms_normal_question_class] (
	[Id] AutoIncrement primary key ,
	[Title] varchar(50) NOT NULL DEFAULT "" ,
	[PId] int NOT NULL DEFAULT "0"
)
GO

INSERT INTO [jcms_normal_question_class] ([Title],[PId]) VALUES ('技术咨询',1)
GO
INSERT INTO [jcms_normal_question_class] ([Title],[PId]) VALUES ('产品咨询',2)
GO
INSERT INTO [jcms_normal_question_class] ([Title],[PId]) VALUES ('意见建议',3)
GO
INSERT INTO [jcms_normal_question_class] ([Title],[PId]) VALUES ('其他问题',4)
GO


CREATE TABLE [jcms_normal_link] (
	[Id] AutoIncrement primary key ,
	[Title] varchar(50) NOT NULL DEFAULT "",
	[Url] varchar(150) NOT NULL DEFAULT "" ,
	[ImgPath] varchar(150) NOT NULL DEFAULT "" ,
	[Info] varchar(250) NOT NULL DEFAULT "",
	[OrderNum] int NOT NULL DEFAULT "0" ,
	[State] int NOT NULL DEFAULT "0" ,
	[Style] int NOT NULL DEFAULT "0" 
)
GO

CREATE TABLE [jcms_normal_digg] (
	[Id] AutoIncrement primary key ,
	[ContentId] int NOT NULL DEFAULT "0" ,
	[ChannelType] varchar(10) NOT NULL DEFAULT "" ,
	[DiggNum] int NOT NULL DEFAULT "0" 
)
GO

CREATE TABLE [jcms_normal_review] (
	[Id] AutoIncrement primary key ,
	[ChannelId] int NOT NULL DEFAULT "0" ,
	[ParentId] int NOT NULL DEFAULT "0",
	[ContentId] int NOT NULL DEFAULT "0",
	[AddDate] datetime NULL ,
	[Content] varchar(250) NOT NULL DEFAULT "" ,
	[IP] varchar(15) NOT NULL DEFAULT "" ,
	[UserName] varchar(50) NOT NULL DEFAULT "",
	[IsPass] int NOT NULL DEFAULT "0"
)
GO


CREATE TABLE [jcms_normal_language] (
	[Id] AutoIncrement primary key ,
	[Title] varchar(20) NOT NULL DEFAULT "" ,
	[Code] varchar(20) NOT NULL DEFAULT ""
)
GO
INSERT INTO [jcms_normal_language] ([Title],[Code]) VALUES ('中文','cn')
GO
INSERT INTO [jcms_normal_language] ([Title],[Code]) VALUES ('英文','en')
GO




CREATE TABLE [jcms_normal_user_oauth] (
	[Id] AutoIncrement primary key ,
	[Title] varchar(20) NOT NULL DEFAULT "" ,
	[Code] varchar(20) NOT NULL DEFAULT "",
	[PId] int NOT NULL DEFAULT "0",
	[Enabled] int NOT NULL DEFAULT "1"
)
GO

INSERT INTO [jcms_normal_user_oauth] ([Title],[Code],PId,Enabled) VALUES ('新浪微博','sina',1,0)
GO
INSERT INTO [jcms_normal_user_oauth] ([Title],[Code],PId,Enabled) VALUES ('QQ账号','tencent',2,0)
GO
INSERT INTO [jcms_normal_user_oauth] ([Title],[Code],PId,Enabled) VALUES ('人人网账号','renren',3,0)
GO
INSERT INTO [jcms_normal_user_oauth] ([Title],[Code],PId,Enabled) VALUES ('百度账号','baidu',4,0)
GO
INSERT INTO [jcms_normal_user_oauth] ([Title],[Code],PId,Enabled) VALUES ('开心网账号','kaixin',5,0)
GO
