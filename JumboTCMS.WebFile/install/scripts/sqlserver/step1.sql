if exists (select * from sysobjects where id = OBJECT_ID('[jcms_module_article]') and OBJECTPROPERTY(id, 'IsUserTable') = 1) 
DROP TABLE [jcms_module_article]
GO
CREATE TABLE [dbo].[jcms_module_article] (
	[Id] [int] IDENTITY (1, 1) NOT NULL ,
	[ChannelId] [int] NOT NULL DEFAULT(0),
	[ClassId] [int] NOT NULL DEFAULT(0),
	[Title] [nvarchar] (150) NOT NULL DEFAULT (''),
	[TColor] [nvarchar] (8) NOT NULL DEFAULT (''),
	[AddDate] [datetime] NOT NULL DEFAULT (getdate()),
	[Summary] [ntext] NOT NULL DEFAULT ('') ,
	[Editor] [nvarchar] (20) NOT NULL DEFAULT (''),
	[Author] [nvarchar] (30) NOT NULL DEFAULT (''),
	[Tags] [nvarchar] (60) NOT NULL DEFAULT (''),
	[ViewNum] [int] NOT NULL DEFAULT(0),
	[IsPass] [int] NOT NULL DEFAULT(0),
	[IsImg] [int] NOT NULL DEFAULT(0),
	[Img] [nvarchar] (150) NOT NULL DEFAULT (''),
	[IsTop] [int] NOT NULL DEFAULT(0),
	[IsFocus] [int] NOT NULL DEFAULT(0),
	[UserId] [int] NOT NULL DEFAULT(0),
	[ReadGroup] [int] NOT NULL DEFAULT(0),
	[SourceFrom] [nvarchar] (30) NOT NULL DEFAULT (''),
	[Content] [ntext] NOT NULL DEFAULT (''),
	[FirstPage] [nvarchar] (150) NOT NULL DEFAULT (''),
	[AliasPage] [nvarchar] (150) NOT NULL DEFAULT (''),
	CONSTRAINT [PK_module_article] PRIMARY KEY CLUSTERED 
	(
		[Id]
	) ON [PRIMARY] 
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO

if exists (select * from sysobjects where id = OBJECT_ID('[jcms_module_document]') and OBJECTPROPERTY(id, 'IsUserTable') = 1) 
DROP TABLE [jcms_module_document]
GO
CREATE TABLE [dbo].[jcms_module_document] (
	[Id] [int] IDENTITY (1, 1) NOT NULL ,
	[ChannelId] [int] NOT NULL DEFAULT(0),
	[ClassId] [int] NOT NULL DEFAULT(0),
	[Title] [nvarchar] (150) NOT NULL DEFAULT (''),
	[TColor] [nvarchar] (8) NOT NULL DEFAULT (''),
	[AddDate] [datetime] NOT NULL DEFAULT (getdate()),
	[Summary] [ntext] NOT NULL DEFAULT ('') ,
	[Editor] [nvarchar] (20) NOT NULL DEFAULT (''),
	[Author] [nvarchar] (30) NOT NULL DEFAULT (''),
	[Tags] [nvarchar] (60) NOT NULL DEFAULT (''),
	[ViewNum] [int] NOT NULL DEFAULT(0),
	[IsPass] [int] NOT NULL DEFAULT(0),
	[IsImg] [int] NOT NULL DEFAULT(0),
	[Img] [nvarchar] (150) NOT NULL DEFAULT (''),
	[IsTop] [int] NOT NULL DEFAULT(0),
	[IsFocus] [int] NOT NULL DEFAULT(0),
	[UserId] [int] NOT NULL DEFAULT(0),
	[ReadGroup] [int] NOT NULL DEFAULT(0),
	[SourceFrom] [nvarchar] (30) NOT NULL DEFAULT (''),
	[PageNumber] [int] NOT NULL DEFAULT(0),
	[Points] [int] NOT NULL DEFAULT(0),
	[DocumentUrl] [ntext] NOT NULL DEFAULT ('') ,
	[PageSize] [int] NOT NULL DEFAULT(0),
	[FirstPage] [nvarchar] (150) NOT NULL DEFAULT (''),
	[AliasPage] [nvarchar] (150) NOT NULL DEFAULT (''),
	CONSTRAINT [PK_module_document] PRIMARY KEY CLUSTERED 
	(
		[Id]
	) ON [PRIMARY] 
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO

if exists (select * from sysobjects where id = OBJECT_ID('[jcms_module_document_downlogs]') and OBJECTPROPERTY(id, 'IsUserTable') = 1) 
DROP TABLE [jcms_module_document_downlogs]
GO
CREATE TABLE [dbo].[jcms_module_document_downlogs] (
	[Id] [bigint] IDENTITY (1, 1) NOT NULL ,
	[ChannelId] [int] NOT NULL DEFAULT(0),
	[UserId] [int] NOT NULL DEFAULT(0),
	[DocumentId] [int] NOT NULL DEFAULT(0),
	[Points] [int] NOT NULL DEFAULT(0),
	[DownTime] [datetime] NOT NULL DEFAULT (getdate()),
	[DownIP] [nvarchar] (16) NULL ,
	[DownDegree] [int] NOT NULL DEFAULT(0),
	CONSTRAINT [PK_module_document_downlogs] PRIMARY KEY CLUSTERED 
	(
		[Id]
	) ON [PRIMARY] 
) ON [PRIMARY]
GO

if exists (select * from sysobjects where id = OBJECT_ID('[jcms_module_paper]') and OBJECTPROPERTY(id, 'IsUserTable') = 1) 
DROP TABLE [jcms_module_paper]
GO
CREATE TABLE [dbo].[jcms_module_paper] (
	[Id] [int] IDENTITY (1, 1) NOT NULL ,
	[ChannelId] [int] NOT NULL DEFAULT(0),
	[ClassId] [int] NOT NULL DEFAULT(0),
	[Title] [nvarchar] (150) NOT NULL DEFAULT (''),
	[TColor] [nvarchar] (8) NOT NULL DEFAULT (''),
	[AddDate] [datetime] NOT NULL DEFAULT (getdate()),
	[Summary] [ntext] NOT NULL DEFAULT ('') ,
	[Editor] [nvarchar] (20) NOT NULL DEFAULT (''),
	[Author] [nvarchar] (30) NOT NULL DEFAULT (''),
	[Tags] [nvarchar] (60) NOT NULL DEFAULT (''),
	[ViewNum] [int] NOT NULL DEFAULT(0),
	[IsPass] [int] NOT NULL DEFAULT(0),
	[IsImg] [int] NOT NULL DEFAULT(0),
	[Img] [nvarchar] (150) NOT NULL DEFAULT (''),
	[IsTop] [int] NOT NULL DEFAULT(0),
	[IsFocus] [int] NOT NULL DEFAULT(0),
	[UserId] [int] NOT NULL DEFAULT(0),
	[ReadGroup] [int] NOT NULL DEFAULT(0),
	[SourceFrom] [nvarchar] (30) NOT NULL DEFAULT (''),
	[PageNumber] [int] NOT NULL DEFAULT(0),
	[Points] [int] NOT NULL DEFAULT(0),
	[PaperUrl] [nvarchar] (150) NOT NULL DEFAULT (''),
	[SwfFile] [nvarchar] (150) NOT NULL DEFAULT (''),
	[PageSize] [int] NOT NULL DEFAULT(0),
	[FirstPage] [nvarchar] (150) NOT NULL DEFAULT (''),
	[AliasPage] [nvarchar] (150) NOT NULL DEFAULT (''),
	CONSTRAINT [PK_module_paper] PRIMARY KEY CLUSTERED 
	(
		[Id]
	) ON [PRIMARY] 
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO

if exists (select * from sysobjects where id = OBJECT_ID('[jcms_module_paper_downlogs]') and OBJECTPROPERTY(id, 'IsUserTable') = 1) 
DROP TABLE [jcms_module_paper_downlogs]
GO
CREATE TABLE [dbo].[jcms_module_paper_downlogs] (
	[Id] [bigint] IDENTITY (1, 1) NOT NULL ,
	[ChannelId] [int] NOT NULL DEFAULT(0),
	[UserId] [int] NOT NULL DEFAULT(0),
	[PaperId] [int] NOT NULL DEFAULT(0),
	[Points] [int] NOT NULL DEFAULT(0),
	[DownTime] [datetime] NOT NULL DEFAULT (getdate()),
	[DownIP] [nvarchar] (16) NULL ,
	[DownDegree] [int] NOT NULL DEFAULT(0),
	CONSTRAINT [PK_module_paper_downlogs] PRIMARY KEY CLUSTERED 
	(
		[Id]
	) ON [PRIMARY] 
) ON [PRIMARY]
GO


if exists (select * from sysobjects where id = OBJECT_ID('[jcms_module_photo]') and OBJECTPROPERTY(id, 'IsUserTable') = 1) 
DROP TABLE [jcms_module_photo]
GO
CREATE TABLE [dbo].[jcms_module_photo] (
	[Id] [int] IDENTITY (1, 1) NOT NULL ,
	[ChannelId] [int] NOT NULL DEFAULT(0),
	[ClassId] [int] NOT NULL DEFAULT(0),
	[Title] [nvarchar] (150) NOT NULL DEFAULT (''),
	[TColor] [nvarchar] (8) NOT NULL DEFAULT (''),
	[AddDate] [datetime] NOT NULL DEFAULT (getdate()),
	[Summary] [ntext] NOT NULL DEFAULT ('') ,
	[Editor] [nvarchar] (20) NOT NULL DEFAULT (''),
	[Author] [nvarchar] (30) NOT NULL DEFAULT (''),
	[Tags] [nvarchar] (60) NOT NULL DEFAULT (''),
	[ViewNum] [int] NOT NULL DEFAULT(0),
	[IsPass] [int] NOT NULL DEFAULT(0),
	[IsImg] [int] NOT NULL DEFAULT(0),
	[Img] [nvarchar] (150) NOT NULL DEFAULT (''),
	[IsTop] [int] NOT NULL DEFAULT(0),
	[IsFocus] [int] NOT NULL DEFAULT(0),
	[UserId] [int] NOT NULL DEFAULT(0),
	[ReadGroup] [int] NOT NULL DEFAULT(0),
	[SourceFrom] [nvarchar] (30) NOT NULL DEFAULT (''),
	[ThumbsUrl] [ntext] NOT NULL DEFAULT ('') ,
	[PhotoUrl] [ntext] NOT NULL DEFAULT ('') ,
	[PageSize] [int] NOT NULL DEFAULT(0),
	[FirstPage] [nvarchar] (150) NOT NULL DEFAULT (''),
	[AliasPage] [nvarchar] (150) NOT NULL DEFAULT (''),
	CONSTRAINT [PK_module_photo] PRIMARY KEY CLUSTERED 
	(
		[Id]
	) ON [PRIMARY] 
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO

if exists (select * from sysobjects where id = OBJECT_ID('[jcms_module_product]') and OBJECTPROPERTY(id, 'IsUserTable') = 1) 
DROP TABLE [jcms_module_product]
GO
CREATE TABLE [dbo].[jcms_module_product] (
	[Id] [int] IDENTITY (1, 1) NOT NULL ,
	[ChannelId] [int] NOT NULL DEFAULT(0),
	[ClassId] [int] NOT NULL DEFAULT(0),
	[Title] [nvarchar] (150) NOT NULL DEFAULT (''),
	[TColor] [nvarchar] (8) NOT NULL DEFAULT (''),
	[AddDate] [datetime] NOT NULL DEFAULT (getdate()),
	[Summary] [ntext] NOT NULL DEFAULT ('') ,
	[Editor] [nvarchar] (20) NOT NULL DEFAULT (''),
	[Author] [nvarchar] (30) NOT NULL DEFAULT (''),
	[Tags] [nvarchar] (60) NOT NULL DEFAULT (''),
	[ViewNum] [int] NOT NULL DEFAULT(0),
	[IsPass] [int] NOT NULL DEFAULT(0),
	[IsImg] [int] NOT NULL DEFAULT(0),
	[Img] [nvarchar] (150) NOT NULL DEFAULT (''),
	[IsTop] [int] NOT NULL DEFAULT(0),
	[IsFocus] [int] NOT NULL DEFAULT(0),
	[UserId] [int] NOT NULL DEFAULT(0),
	[ReadGroup] [int] NOT NULL DEFAULT(0),
	[SourceFrom] [nvarchar] (30) NOT NULL DEFAULT (''),
	[Content] [ntext] NOT NULL ,
	[FirstPage] [nvarchar] (150) NOT NULL DEFAULT (''),
	[AliasPage] [nvarchar] (150) NOT NULL DEFAULT (''),
	[Price0] [float] NOT NULL DEFAULT(0),
	[Points] [int] NOT NULL DEFAULT(0),
	CONSTRAINT [PK_module_product] PRIMARY KEY CLUSTERED 
	(
		[Id]
	) ON [PRIMARY] 
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO


if exists (select * from sysobjects where id = OBJECT_ID('[jcms_module_soft]') and OBJECTPROPERTY(id, 'IsUserTable') = 1) 
DROP TABLE [jcms_module_soft]
GO
CREATE TABLE [dbo].[jcms_module_soft] (
	[Id] [int] IDENTITY (1, 1) NOT NULL ,
	[ChannelId] [int] NOT NULL DEFAULT(0),
	[ClassId] [int] NOT NULL DEFAULT(0),
	[Title] [nvarchar] (150) NOT NULL DEFAULT (''),
	[TColor] [nvarchar] (8) NOT NULL DEFAULT (''),
	[AddDate] [datetime] NOT NULL DEFAULT (getdate()),
	[Summary] [ntext] NOT NULL DEFAULT ('') ,
	[Editor] [nvarchar] (20) NOT NULL DEFAULT (''),
	[Author] [nvarchar] (30) NOT NULL DEFAULT (''),
	[Tags] [nvarchar] (60) NOT NULL DEFAULT (''),
	[ViewNum] [int] NOT NULL DEFAULT(0),
	[IsPass] [int] NOT NULL DEFAULT(0),
	[IsImg] [int] NOT NULL DEFAULT(0),
	[Img] [nvarchar] (150) NOT NULL DEFAULT (''),
	[IsTop] [int] NOT NULL DEFAULT(0),
	[IsFocus] [int] NOT NULL DEFAULT(0),
	[UserId] [int] NOT NULL DEFAULT(0),
	[ReadGroup] [int] NOT NULL DEFAULT(0),
	[SourceFrom] [nvarchar] (30) NOT NULL DEFAULT (''),
	[Version] [nvarchar] (100) NOT NULL DEFAULT (''),
	[OperatingSystem] [nvarchar] (255) NULL ,
	[UnZipPass] [nvarchar] (100) NOT NULL DEFAULT (''),
	[DemoUrl] [nvarchar] (255) NULL ,
	[RegUrl] [nvarchar] (255) NULL ,
	[SSize] [varchar] (20) NULL ,
	[Points] [int] NOT NULL DEFAULT(0),
	[DownUrl] [ntext] NOT NULL DEFAULT ('') ,
	[DownNum] [int] NOT NULL DEFAULT(0),
	[Content] [ntext] NOT NULL DEFAULT (''),
	[FirstPage] [nvarchar] (150) NOT NULL DEFAULT (''),
	[AliasPage] [nvarchar] (150) NOT NULL DEFAULT (''),
	CONSTRAINT [PK_module_soft] PRIMARY KEY CLUSTERED 
	(
		[Id]
	) ON [PRIMARY] 
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO


if exists (select * from sysobjects where id = OBJECT_ID('[jcms_module_soft_downlogs]') and OBJECTPROPERTY(id, 'IsUserTable') = 1) 
DROP TABLE [jcms_module_soft_downlogs]
GO
CREATE TABLE [dbo].[jcms_module_soft_downlogs] (
	[Id] [bigint] IDENTITY (1, 1) NOT NULL ,
	[ChannelId] [int] NOT NULL DEFAULT(0),
	[UserId] [int] NOT NULL DEFAULT(0),
	[SoftId] [int] NOT NULL DEFAULT(0),
	[Points] [int] NOT NULL DEFAULT(0),
	[DownTime] [datetime] NOT NULL DEFAULT (getdate()),
	[DownIP] [nvarchar] (16) NULL ,
	[DownDegree] [int] NOT NULL DEFAULT(0),
	CONSTRAINT [PK_module_soft_downlogs] PRIMARY KEY CLUSTERED 
	(
		[Id]
	) ON [PRIMARY] 
) ON [PRIMARY]
GO


if exists (select * from sysobjects where id = OBJECT_ID('[jcms_module_video]') and OBJECTPROPERTY(id, 'IsUserTable') = 1) 
DROP TABLE [jcms_module_video]
GO
CREATE TABLE [dbo].[jcms_module_video] (
	[Id] [int] IDENTITY (1, 1) NOT NULL ,
	[ChannelId] [int] NOT NULL DEFAULT(0),
	[ClassId] [int] NOT NULL DEFAULT(0),
	[Title] [nvarchar] (150) NOT NULL DEFAULT (''),
	[TColor] [nvarchar] (8) NOT NULL DEFAULT (''),
	[AddDate] [datetime] NOT NULL DEFAULT (getdate()),
	[Summary] [ntext] NOT NULL DEFAULT ('') ,
	[Editor] [nvarchar] (20) NOT NULL DEFAULT (''),
	[Author] [nvarchar] (30) NOT NULL DEFAULT (''),
	[Tags] [nvarchar] (60) NOT NULL DEFAULT (''),
	[ViewNum] [int] NOT NULL DEFAULT(0),
	[IsPass] [int] NOT NULL DEFAULT(0),
	[IsImg] [int] NOT NULL DEFAULT(0),
	[Img] [nvarchar] (150) NOT NULL DEFAULT (''),
	[IsTop] [int] NOT NULL DEFAULT(0),
	[IsFocus] [int] NOT NULL DEFAULT(0),
	[UserId] [int] NOT NULL DEFAULT(0),
	[ReadGroup] [int] NOT NULL DEFAULT(0),
	[SourceFrom] [nvarchar] (30) NOT NULL DEFAULT (''),
	[VideoUrl] [ntext] NOT NULL DEFAULT ('') ,
	[PageSize] [int] NOT NULL DEFAULT(0),
	[FirstPage] [nvarchar] (150) NOT NULL DEFAULT (''),
	[AliasPage] [nvarchar] (150) NOT NULL DEFAULT (''),
	CONSTRAINT [PK_module_video] PRIMARY KEY CLUSTERED 
	(
		[Id]
	) ON [PRIMARY] 
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO


if exists (select * from sysobjects where id = OBJECT_ID('[jcms_normal_adminlogs]') and OBJECTPROPERTY(id, 'IsUserTable') = 1) 
DROP TABLE [jcms_normal_adminlogs]
GO
CREATE TABLE [dbo].[jcms_normal_adminlogs] (
	[Id] [int] IDENTITY (1, 1) NOT NULL ,
	[AdminId] [int] NOT NULL ,
	[OperInfo] [nvarchar] (200) NOT NULL DEFAULT (''),
	[OperTime] [datetime] NOT NULL DEFAULT (getdate()),
	[OperIP] [nvarchar] (15) NULL ,
	CONSTRAINT [PK_normal_adminlogs] PRIMARY KEY CLUSTERED 
	(
		[Id]
	) ON [PRIMARY] 
) ON [PRIMARY]
GO


if exists (select * from sysobjects where id = OBJECT_ID('[jcms_normal_channel]') and OBJECTPROPERTY(id, 'IsUserTable') = 1) 
DROP TABLE [jcms_normal_channel]
GO
CREATE TABLE [dbo].[jcms_normal_channel] (
	[Id] [int] IDENTITY (1, 1) NOT NULL ,
	[Title] [nvarchar] (20) NOT NULL ,
	[SubDomain] [nvarchar] (100) NOT NULL DEFAULT (''),
	[Info] [nvarchar] (200) NOT NULL DEFAULT (''),
	[ClassDepth] [int] NOT NULL DEFAULT(0),
	[Dir] [nvarchar] (20) NOT NULL ,
	[pId] [int] NOT NULL DEFAULT(0),
	[ItemName] [nvarchar] (4) NOT NULL DEFAULT (''),
	[ItemUnit] [nvarchar] (2) NULL ,
	[TemplateId] [int] NOT NULL DEFAULT(0),
	[Type] [nvarchar] (10) NOT NULL DEFAULT (''),
	[Enabled] [int] NOT NULL DEFAULT(0),
	[DefaultThumbs] [int] NOT NULL DEFAULT(0),
	[IsPost] [int] NOT NULL DEFAULT(0),
	[IsNav] [int] NOT NULL DEFAULT(0),
	[IsHtml] [int] NOT NULL DEFAULT(0),
	[IsTop] [int] NOT NULL DEFAULT(0),
	[UploadPath] [nvarchar](100) NOT NULL DEFAULT (''),
	[UploadType] [nvarchar](50) NOT NULL DEFAULT (''),
	[UploadSize] [bigint] NOT NULL DEFAULT(0),
	[LanguageCode] [nvarchar](20) NOT NULL DEFAULT ('cn'),
	CONSTRAINT [PK_normal_channel] PRIMARY KEY CLUSTERED 
	(
		[Id]
	) ON [PRIMARY] 
) ON [PRIMARY]
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


if exists (select * from sysobjects where id = OBJECT_ID('[jcms_normal_class]') and OBJECTPROPERTY(id, 'IsUserTable') = 1) 
DROP TABLE [jcms_normal_class]
GO
CREATE TABLE [dbo].[jcms_normal_class] (
	[Id] [int] IDENTITY (1, 1) NOT NULL ,
	[ChannelId] [int] NOT NULL ,
	[ParentId] [int] NOT NULL ,
	[Title] [nvarchar] (40) NOT NULL ,
	[Info] [nvarchar] (100) NOT NULL DEFAULT (''),
	[Img] [nvarchar] (150) NOT NULL DEFAULT (''),
	[SortRank] [int] NOT NULL DEFAULT(0),
	[Folder] [nvarchar] (50) NOT NULL DEFAULT (''),
	[FilePath] [nvarchar] (150) NOT NULL DEFAULT (''),
	[Code] [nvarchar] (40) NOT NULL DEFAULT (''),
	[IsPost] [int] NOT NULL DEFAULT(0),
	[IsTop] [int] NOT NULL DEFAULT(0),
	[TopicNum] [int] NOT NULL DEFAULT(0),
	[TemplateId] [int] NOT NULL DEFAULT(0),
	[ContentTemp] [int] NOT NULL DEFAULT(0),
	[PageSize] [int] NOT NULL DEFAULT(0),
	[IsOut] [int] NOT NULL DEFAULT(0),
	[FirstPage] [nvarchar] (150) NOT NULL DEFAULT (''),
	[AliasPage] [nvarchar] (150) NOT NULL DEFAULT (''),
	[ReadGroup] [int] NOT NULL DEFAULT(0),
	CONSTRAINT [PK_normal_class] PRIMARY KEY CLUSTERED 
	(
		[Id]
	) ON [PRIMARY] 
) ON [PRIMARY]
GO


if exists (select * from sysobjects where id = OBJECT_ID('[jcms_normal_extends]') and OBJECTPROPERTY(id, 'IsUserTable') = 1) 
DROP TABLE [jcms_normal_extends]
GO
CREATE TABLE [dbo].[jcms_normal_extends] (
	[Id] [int] IDENTITY (1, 1) NOT NULL ,
	[Title] [nvarchar] (20) NOT NULL DEFAULT (''),
	[Name] [nvarchar] (20) NOT NULL DEFAULT (''),
	[Author] [nvarchar] (20) NOT NULL DEFAULT (''),
	[Info] [nvarchar] (50) NOT NULL DEFAULT (''),
	[Type] [int] NOT NULL DEFAULT(0),
	[pId] [int] NOT NULL ,
	[BaseTable] [nvarchar] (200) NOT NULL DEFAULT (''),
	[ManageUrl] [nvarchar] (50) NOT NULL DEFAULT (''),
	[Enabled] [int] NOT NULL DEFAULT(0),
	[Locked] [int] NOT NULL DEFAULT(0),
	CONSTRAINT [PK_normal_extends] PRIMARY KEY CLUSTERED 
	(
		[Id]
	) ON [PRIMARY] 
) ON [PRIMARY]
GO


INSERT INTO [jcms_normal_extends] ([Title],[Name],[Author],[Info],[Type],[pId],[BaseTable],[ManageUrl],[Enabled],[Locked]) VALUES ('网站公告','Placard','jumbot','官方插件',0,4,'jcms_extends_placard','admin.aspx',1,0)
GO
INSERT INTO [jcms_normal_extends] ([Title],[Name],[Author],[Info],[Type],[pId],[BaseTable],[ManageUrl],[Enabled],[Locked]) VALUES ('投票调查','Vote','jumbot','官方插件',0,6,'jcms_extends_vote','admin.aspx',1,0)
GO
INSERT INTO [jcms_normal_extends] ([Title],[Name],[Author],[Info],[Type],[pId],[BaseTable],[ManageUrl],[Enabled],[Locked]) VALUES ('QQ在线客服','QQOnline','jumbot','官方插件',0,7,'jcms_extends_qqonline','admin.aspx',1,0)
GO

if exists (select * from sysobjects where id = OBJECT_ID('[jcms_normal_forbidip]') and OBJECTPROPERTY(id, 'IsUserTable') = 1) 
DROP TABLE [jcms_normal_forbidip]
GO
CREATE TABLE [dbo].[jcms_normal_forbidip] (
	[Id] [int] IDENTITY (1, 1) NOT NULL ,
	[StartIP] [bigint] NULL ,
	[StartIP2] [nvarchar] (16) NULL ,
	[EndIP] [bigint] NULL ,
	[EndIP2] [nvarchar] (16) NULL ,
	[ExpireDate] [datetime] NOT NULL DEFAULT (getdate()),
	[Enabled] [int] NOT NULL DEFAULT(0),
	CONSTRAINT [PK_normal_forbidip] PRIMARY KEY CLUSTERED 
	(
		[Id]
	) ON [PRIMARY]
) ON [PRIMARY]
GO


if exists (select * from sysobjects where id = OBJECT_ID('[jcms_normal_modules]') and OBJECTPROPERTY(id, 'IsUserTable') = 1) 
DROP TABLE [jcms_normal_modules]
GO
CREATE TABLE [dbo].[jcms_normal_modules] (
	[Id] [int] IDENTITY (1, 1) NOT NULL ,
	[Title] [nvarchar] (100) NOT NULL ,
	[Type] [nvarchar] (10) NOT NULL DEFAULT (''),
	[ItemName] [nvarchar] (4) NOT NULL DEFAULT (''),
	[ItemUnit] [nvarchar] (2) NULL ,
	[PId] [int] NOT NULL DEFAULT(0),
	[Enabled] [int] NOT NULL DEFAULT(0),
	[Locked] [int] NOT NULL DEFAULT(0),
	[SearchFieldValues] [nvarchar] (200) NOT NULL DEFAULT (''),
	[SearchFieldTexts] [nvarchar] (200) NULL ,
	CONSTRAINT [PK_normal_modules] PRIMARY KEY CLUSTERED 
	(
		[Id]
	) ON [PRIMARY] 
) ON [PRIMARY]
GO
INSERT INTO [jcms_normal_modules] (Title,Type,pId,Enabled,Locked,SearchFieldValues,SearchFieldTexts,ItemName,ItemUnit) values('文章','article',1,1,1,'title,summary','标题,简介','内容','篇');
INSERT INTO [jcms_normal_modules] (Title,Type,pId,Enabled,Locked,SearchFieldValues,SearchFieldTexts,ItemName,ItemUnit) values('图片','photo',2,1,1,'title,summary','标题,简介','图片','组');
INSERT INTO [jcms_normal_modules] (Title,Type,pId,Enabled,Locked,SearchFieldValues,SearchFieldTexts,ItemName,ItemUnit) values('下载','soft',3,1,1,'title,summary','标题,简介','文件','个');
INSERT INTO [jcms_normal_modules] (Title,Type,pId,Enabled,Locked,SearchFieldValues,SearchFieldTexts,ItemName,ItemUnit) values('视频','video',4,1,1,'title,summary','标题,简介','视频','个');
INSERT INTO [jcms_normal_modules] (Title,Type,pId,Enabled,Locked,SearchFieldValues,SearchFieldTexts,ItemName,ItemUnit) values('文档','document',6,1,1,'title,summary','标题,简介','文档','篇');
INSERT INTO [jcms_normal_modules] (Title,Type,pId,Enabled,Locked,SearchFieldValues,SearchFieldTexts,ItemName,ItemUnit) values('文库','paper',7,1,1,'title,summary','标题,简介','文件','篇');
INSERT INTO [jcms_normal_modules] (Title,Type,pId,Enabled,Locked,SearchFieldValues,SearchFieldTexts,ItemName,ItemUnit) values('产品','product',5,1,1,'title,summary','标题,简介','商品','个');


if exists (select * from sysobjects where id = OBJECT_ID('[jcms_normal_page]') and OBJECTPROPERTY(id, 'IsUserTable') = 1) 
DROP TABLE [jcms_normal_page]
GO
CREATE TABLE [dbo].[jcms_normal_page] (
	[Id] [int] IDENTITY (1, 1) NOT NULL ,
	[Title] [nvarchar] (20) NOT NULL DEFAULT (''),
	[Source] [nvarchar] (50) NULL ,
	[OutUrl] [nvarchar] (100) NULL ,
	CONSTRAINT [PK_normal_page] PRIMARY KEY CLUSTERED 
	(
		[Id]
	) ON [PRIMARY] 
) ON [PRIMARY]
GO
INSERT INTO [jcms_normal_page] (Title,Source,OutUrl) VALUES ('网站地图','system_sitemap.htm','/sitemap.shtml')
INSERT INTO [jcms_normal_page] (Title,Source,OutUrl) VALUES ('RSS订阅','system_rss.htm','/rss.shtml')
INSERT INTO [jcms_normal_page] (Title,Source,OutUrl) VALUES ('帮助中心','system_help.htm','/help.shtml')


if exists (select * from sysobjects where id = OBJECT_ID('[jcms_normal_pointscard]') and OBJECTPROPERTY(id, 'IsUserTable') = 1) 
DROP TABLE [jcms_normal_pointscard]
GO
CREATE TABLE [dbo].[jcms_normal_pointscard] (
	[Id] [int] IDENTITY (1, 1) NOT NULL ,
	[CardNumber] [nvarchar] (16) NULL ,
	[CardPassword] [nvarchar] (32) NULL ,
	[UserId] [int] NOT NULL DEFAULT(0),
	[Points] [int] NOT NULL,
	[LimitedDate] [datetime] NOT NULL DEFAULT (getdate()),
	[ActiveTime] [datetime] NOT NULL DEFAULT (getdate()),
	[ActiveIP] [nvarchar] (15) NULL ,
	[State] [int] NOT NULL DEFAULT(0),
	CONSTRAINT [PK_normal_pointscard] PRIMARY KEY CLUSTERED 
	(
		[Id]
	) ON [PRIMARY] 
) ON [PRIMARY]
GO


if exists (select * from sysobjects where id = OBJECT_ID('[jcms_normal_pointscard_sort]') and OBJECTPROPERTY(id, 'IsUserTable') = 1) 
DROP TABLE [jcms_normal_pointscard_sort]
GO
CREATE TABLE [dbo].[jcms_normal_pointscard_sort] (
	[Id] [int] IDENTITY (1, 1) NOT NULL ,
	[Title] [nvarchar] (50) NOT NULL DEFAULT (''),
	[Points] [int] NOT NULL ,
	CONSTRAINT [PK_normal_pointscard_sort] PRIMARY KEY CLUSTERED 
	(
		[Id]
	) ON [PRIMARY] 
) ON [PRIMARY]
GO
INSERT INTO [jcms_normal_pointscard_sort] (Title,Points) values('50元=>50点',50);
INSERT INTO [jcms_normal_pointscard_sort] (Title,Points) values('100元=>100点',100);
INSERT INTO [jcms_normal_pointscard_sort] (Title,Points) values('200元=>200点',200);
INSERT INTO [jcms_normal_pointscard_sort] (Title,Points) values('500元=>500点',500);
INSERT INTO [jcms_normal_pointscard_sort] (Title,Points) values('1000元=>1000点',1000);



if exists (select * from sysobjects where id = OBJECT_ID('[jcms_normal_special]') and OBJECTPROPERTY(id, 'IsUserTable') = 1) 
DROP TABLE [jcms_normal_special]
GO
CREATE TABLE [dbo].[jcms_normal_special] (
	[Id] [int] IDENTITY (1, 1) NOT NULL ,
	[Title] [nvarchar] (100) NOT NULL DEFAULT (''),
	[Info] [nvarchar] (200) NOT NULL DEFAULT (''),
	[Source] [nvarchar] (50) NULL ,
	CONSTRAINT [PK_normal_special] PRIMARY KEY CLUSTERED 
	(
		[Id]
	) ON [PRIMARY] 
) ON [PRIMARY]
GO


if exists (select * from sysobjects where id = OBJECT_ID('[jcms_normal_specialcontent]') and OBJECTPROPERTY(id, 'IsUserTable') = 1) 
DROP TABLE [jcms_normal_specialcontent]
GO
CREATE TABLE [dbo].[jcms_normal_specialcontent] (
	[Id] [int] IDENTITY (1, 1) NOT NULL ,
	[Title] [nvarchar] (100) NOT NULL DEFAULT (''),
	[sId] [int] NOT NULL ,
	[ChannelId] [int] NOT NULL ,
	[ContentId] [int] NOT NULL ,
	CONSTRAINT [PK_normal_specialcontent] PRIMARY KEY CLUSTERED 
	(
		[Id]
	) ON [PRIMARY] 
) ON [PRIMARY]
GO


if exists (select * from sysobjects where id = OBJECT_ID('[jcms_normal_tag]') and OBJECTPROPERTY(id, 'IsUserTable') = 1) 
DROP TABLE [jcms_normal_tag]
GO
CREATE TABLE [jcms_normal_tag] (
	[Id] [int] IDENTITY (1, 1) NOT NULL ,
	[ChannelId] [int] NOT NULL DEFAULT(0) ,
	[Title] [nvarchar] (15) NOT NULL ,
	[ClickTimes] [int] NOT NULL DEFAULT(0) ,
	CONSTRAINT [PK_normal_tag] PRIMARY KEY CLUSTERED 
	(
		[Id]
	) ON [PRIMARY] 
) ON [PRIMARY]
GO


if exists (select * from sysobjects where id = OBJECT_ID('[jcms_normal_template]') and OBJECTPROPERTY(id, 'IsUserTable') = 1) 
DROP TABLE [jcms_normal_template]
GO
CREATE TABLE [dbo].[jcms_normal_template] (
	[Id] [int] IDENTITY (1, 1) NOT NULL ,
	[Title] [nvarchar] (100) NOT NULL DEFAULT (''),
	[PId] [int] NOT NULL DEFAULT(0),
	[Type] [nvarchar] (50) NOT NULL DEFAULT (''),
	[SType] [nvarchar] (50) NOT NULL DEFAULT (''),
	[IsDefault] [int] NOT NULL DEFAULT(0),
	[Source] [nvarchar] (50) NOT NULL DEFAULT (''),
	CONSTRAINT [PK_normal_template] PRIMARY KEY CLUSTERED 
	(
		[Id]
	) ON [PRIMARY]
) ON [PRIMARY]
GO
INSERT INTO [jcms_normal_template] (Title,pId,Type,sType,IsDefault,Source) values('默认模板站点首页',1,'system','index',1,'system_index.htm');
INSERT INTO [jcms_normal_template] (Title,pId,Type,sType,IsDefault,Source) values('默认模板文章频道页',1,'article','channel',1,'article_channel.htm');
INSERT INTO [jcms_normal_template] (Title,pId,Type,sType,IsDefault,Source) values('默认模板文章列表页',1,'article','class',1,'article_class.htm');
INSERT INTO [jcms_normal_template] (Title,pId,Type,sType,IsDefault,Source) values('默认模板文章内容页',1,'article','content',1,'article_content.htm');
INSERT INTO [jcms_normal_template] (Title,pId,Type,sType,IsDefault,Source) values('默认模板下载频道页',1,'soft','channel',1,'soft_channel.htm');
INSERT INTO [jcms_normal_template] (Title,pId,Type,sType,IsDefault,Source) values('默认模板下载列表页',1,'soft','class',1,'soft_class.htm');
INSERT INTO [jcms_normal_template] (Title,pId,Type,sType,IsDefault,Source) values('默认模板下载内容页',1,'soft','content',1,'soft_content.htm');
INSERT INTO [jcms_normal_template] (Title,pId,Type,sType,IsDefault,Source) values('默认模板图片频道页',1,'photo','channel',1,'photo_channel.htm');
INSERT INTO [jcms_normal_template] (Title,pId,Type,sType,IsDefault,Source) values('默认模板图片列表页',1,'photo','class',1,'photo_class.htm');
INSERT INTO [jcms_normal_template] (Title,pId,Type,sType,IsDefault,Source) values('默认模板图片内容页',1,'photo','content',1,'photo_content.htm');
INSERT INTO [jcms_normal_template] (Title,pId,Type,sType,IsDefault,Source) values('默认模板视频频道页',1,'video','channel',1,'video_channel.htm');
INSERT INTO [jcms_normal_template] (Title,pId,Type,sType,IsDefault,Source) values('默认模板视频列表页',1,'video','class',1,'video_class.htm');
INSERT INTO [jcms_normal_template] (Title,pId,Type,sType,IsDefault,Source) values('默认模板视频内容页',1,'video','content',1,'video_content.htm');
INSERT INTO [jcms_normal_template] (Title,pId,Type,sType,IsDefault,Source) values('默认模板产品频道页',1,'product','channel',1,'product_channel.htm');
INSERT INTO [jcms_normal_template] (Title,pId,Type,sType,IsDefault,Source) values('默认模板产品列表页',1,'product','class',1,'product_class.htm');
INSERT INTO [jcms_normal_template] (Title,pId,Type,sType,IsDefault,Source) values('默认模板产品内容页',1,'product','content',1,'product_content.htm');
INSERT INTO [jcms_normal_template] (Title,pId,Type,sType,IsDefault,Source) values('默认模板文档频道页',1,'document','channel',1,'document_channel.htm');
INSERT INTO [jcms_normal_template] (Title,pId,Type,sType,IsDefault,Source) values('默认模板文档列表页',1,'document','class',1,'document_class.htm');
INSERT INTO [jcms_normal_template] (Title,pId,Type,sType,IsDefault,Source) values('默认模板文档内容页',1,'document','content',1,'document_content.htm');
INSERT INTO [jcms_normal_template] (Title,pId,Type,sType,IsDefault,Source) values('默认模板文库频道页',1,'paper','channel',1,'paper_channel.htm');
INSERT INTO [jcms_normal_template] (Title,pId,Type,sType,IsDefault,Source) values('默认模板文库列表页',1,'paper','class',1,'paper_class.htm');
INSERT INTO [jcms_normal_template] (Title,pId,Type,sType,IsDefault,Source) values('默认模板文库内容页',1,'paper','content',1,'paper_content.htm');



if exists (select * from sysobjects where id = OBJECT_ID('[jcms_normal_templateproject]') and OBJECTPROPERTY(id, 'IsUserTable') = 1) 
DROP TABLE [jcms_normal_templateproject]
GO
CREATE TABLE [dbo].[jcms_normal_templateproject] (
	[Id] [int] IDENTITY (1, 1) NOT NULL ,
	[Title] [nvarchar] (100) NOT NULL DEFAULT (''),
	[Info] [nvarchar] (200) NOT NULL DEFAULT (''),
	[Dir] [nvarchar] (50) NOT NULL DEFAULT (''),
	[IsDefault] [int] NOT NULL DEFAULT(0),
	CONSTRAINT [PK_normal_templateproject] PRIMARY KEY CLUSTERED 
	(
		[Id]
	) ON [PRIMARY]
) ON [PRIMARY]
GO
INSERT INTO [jcms_normal_templateproject] (Title,Info,Dir,isDefault) values('默认模板','','default',1);


if exists (select * from sysobjects where id = OBJECT_ID('[jcms_normal_thumbs]') and OBJECTPROPERTY(id, 'IsUserTable') = 1) 
DROP TABLE [jcms_normal_thumbs]
GO
CREATE TABLE [dbo].[jcms_normal_thumbs] (
	[Id] [int] IDENTITY (1, 1) NOT NULL ,
	[ChannelId] [int] NOT NULL DEFAULT(0),
	[Title] [nvarchar] (30) NOT NULL DEFAULT (''),
	[iWidth] [int] NOT NULL DEFAULT(0),
	[iHeight] [int] NOT NULL DEFAULT(0),
	CONSTRAINT [PK_normal_thumbs] PRIMARY KEY CLUSTERED 
	(
		[Id]
	) ON [PRIMARY]
) ON [PRIMARY]
GO
INSERT INTO [jcms_normal_thumbs] (ChannelId,Title,iWidth,iHeight) Values (0,'360X270(4:3)',360,270);
INSERT INTO [jcms_normal_thumbs] (ChannelId,Title,iWidth,iHeight) Values (0,'200X150(3:4)',200,150);
INSERT INTO [jcms_normal_thumbs] (ChannelId,Title,iWidth,iHeight) Values (0,'270X360(3:4)',270,360);
INSERT INTO [jcms_normal_thumbs] (ChannelId,Title,iWidth,iHeight) Values (0,'150X200(3:4)',150,200);
INSERT INTO [jcms_normal_thumbs] (ChannelId,Title,iWidth,iHeight) Values (0,'240X240(1:1)',240,240);
INSERT INTO [jcms_normal_thumbs] (ChannelId,Title,iWidth,iHeight) Values (0,'150X150(1:1)',150,150);


if exists (select * from sysobjects where id = OBJECT_ID('[jcms_normal_user]') and OBJECTPROPERTY(id, 'IsUserTable') = 1) 
DROP TABLE [jcms_normal_user]
GO
CREATE TABLE [dbo].[jcms_normal_user] (
	[Id] [int] IDENTITY (1, 1) NOT NULL ,
	[GUID] [nvarchar] (40) NOT NULL DEFAULT (''),
	[UserName] [nvarchar] (40) NOT NULL DEFAULT (''),
	[NickName] [nvarchar] (20) NOT NULL DEFAULT (''),
	[UserPass] [nvarchar] (40) NOT NULL DEFAULT (''),
	[TrueName] [nvarchar] (20) NOT NULL DEFAULT (''),
	[Question] [nvarchar] (30) NOT NULL DEFAULT (''),
	[Answer] [nvarchar] (20) NOT NULL DEFAULT (''),
	[Sex] [int] NOT NULL DEFAULT(0),
	[Email] [nvarchar] (80) NOT NULL DEFAULT (''),
	[Group] [int] NOT NULL DEFAULT(0),
	[State] [int] NOT NULL DEFAULT(0),
	[Cookies] [nvarchar] (10) NOT NULL DEFAULT (''),
	[RegTime] [datetime] NOT NULL DEFAULT (getdate()),
	[RegIp] [nvarchar] (15) NULL ,
	[LastTime] [datetime] NOT NULL DEFAULT (getdate()),
	[LastIp] [nvarchar] (15) NULL ,
	[HomePage] [nvarchar] (100) NOT NULL DEFAULT (''),
	[QQ] [nvarchar] (50) NOT NULL DEFAULT (''),
	[ICQ] [nvarchar] (50) NOT NULL DEFAULT (''),
	[MSN] [nvarchar] (50) NOT NULL DEFAULT (''),
	[BirthDay] [nvarchar] (50) NOT NULL DEFAULT (''),
	[Signature] [nvarchar] (30) NOT NULL DEFAULT (''),
	[ProvinceCity] [nvarchar] (40) NOT NULL DEFAULT ('江苏-苏州'),
	[Login] [int] NOT NULL DEFAULT(0),
	[Points] [int] NOT NULL DEFAULT(0),
	[IDType] [int] NOT NULL DEFAULT(0),
	[IDCard] [nvarchar] (30) NOT NULL DEFAULT (''),
	[WorkUnit] [nvarchar] (100) NOT NULL DEFAULT (''),
	[Address] [nvarchar] (100) NOT NULL DEFAULT (''),
	[ZipCode] [nvarchar] (10) NOT NULL DEFAULT (''),
	[Telephone] [nvarchar] (20) NOT NULL DEFAULT (''),
	[MobileTel] [nvarchar] (11) NULL ,
	[IsVIP] [int] NOT NULL DEFAULT(0),
	[VIPTime] [datetime] NOT NULL DEFAULT (getdate()),
	[Integral] [int] NOT NULL DEFAULT(0),
	[UserSign] [nvarchar] (64) NULL ,
	[AdminId] [int] NOT NULL DEFAULT(0),
	[AdminName] [nvarchar] (20) NOT NULL DEFAULT (''),
	[AdminPass] [nvarchar] (32) NULL ,
	[Setting] [text] NOT NULL DEFAULT (''),
	[LastTime2] [datetime] NOT NULL DEFAULT (getdate()),
	[LastIp2] [nvarchar] (15) NULL ,
	[Cookiess] [nvarchar] (10) NOT NULL DEFAULT (''),
	[AdminSign] [nvarchar] (64) NULL ,
	[AdminState] [int] NOT NULL DEFAULT(0),
	[ForumName] [nvarchar] (20) NOT NULL DEFAULT (''),
	[ForumPass] [nvarchar] (32) NULL DEFAULT (''),
	[ServiceId] [int] NOT NULL DEFAULT(0),
	[ServiceName] [nvarchar] (20) NOT NULL DEFAULT (''),
	[LastTime3] [datetime] NOT NULL DEFAULT (getdate()),
	[LastIp3] [nvarchar] (15) NULL ,
	[Token_Sina] [nvarchar] (32) NOT NULL DEFAULT (''),
	[Token_Tencent] [nvarchar] (32) NOT NULL DEFAULT (''),
	[Token_Renren] [nvarchar] (32) NOT NULL DEFAULT (''),
	[Token_Baidu] [nvarchar] (32) NOT NULL DEFAULT (''),
	[Token_Kaixin] [nvarchar] (32) NOT NULL DEFAULT (''),
	CONSTRAINT [PK_normal_user] PRIMARY KEY CLUSTERED 
	(
		[Id]
	) ON [PRIMARY] 
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO


if exists (select * from sysobjects where id = OBJECT_ID('[jcms_normal_user_logs]') and OBJECTPROPERTY(id, 'IsUserTable') = 1) 
DROP TABLE [jcms_normal_user_logs]
GO
CREATE TABLE [dbo].[jcms_normal_user_logs] (
	[Id] [int] IDENTITY (1, 1) NOT NULL ,
	[UserId] [int] NOT NULL ,
	[OperInfo] [nvarchar] (200) NOT NULL DEFAULT (''),
	[OperType] [int] NOT NULL DEFAULT(0),
	[OperTime] [datetime] NOT NULL DEFAULT (getdate()),
	[OperIP] [nvarchar] (15) NULL ,
	CONSTRAINT [PK_normal_user_logs] PRIMARY KEY CLUSTERED 
	(
		[Id]
	) ON [PRIMARY] 
) ON [PRIMARY]
GO



if exists (select * from sysobjects where id = OBJECT_ID('[jcms_normal_user_friends]') and OBJECTPROPERTY(id, 'IsUserTable') = 1) 
DROP TABLE [jcms_normal_user_friends]
GO
CREATE TABLE [dbo].[jcms_normal_user_friends] (
	[Id] [int] IDENTITY (1, 1) NOT NULL ,
	[UserId] [int] NOT NULL ,
	[AddDate] [datetime] NOT NULL DEFAULT (getdate()),
	[FriendId] [int] NOT NULL ,
	CONSTRAINT [PK_normal_user_friends] PRIMARY KEY CLUSTERED 
	(
		[Id]
	) ON [PRIMARY] 
) ON [PRIMARY]
GO


if exists (select * from sysobjects where id = OBJECT_ID('[jcms_normal_user_message]') and OBJECTPROPERTY(id, 'IsUserTable') = 1) 
DROP TABLE [jcms_normal_user_message]
GO
CREATE TABLE [dbo].[jcms_normal_user_message] (
	[Id] [int] IDENTITY (1, 1) NOT NULL ,
	[Title] [nvarchar] (50) NOT NULL DEFAULT (''),
	[Content] [ntext] NOT NULL DEFAULT ('') ,
	[SendIP] [nvarchar] (15) NULL ,
	[SendUserId] [int] NOT NULL ,
	[ReceiveUserId] [int] NOT NULL ,
	[ReceiveUserName] [nvarchar] (20) NOT NULL DEFAULT (''),
	[AddDate] [datetime] NOT NULL DEFAULT (getdate()),
	[State] [int] NOT NULL DEFAULT(0),
	[ReadTime] [datetime] NOT NULL DEFAULT (getdate()),
	CONSTRAINT [PK_normal_user_message] PRIMARY KEY CLUSTERED 
	(
		[Id]
	) ON [PRIMARY] 
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO


if exists (select * from sysobjects where id = OBJECT_ID('[jcms_normal_user_favorite]') and OBJECTPROPERTY(id, 'IsUserTable') = 1) 
DROP TABLE [jcms_normal_user_favorite]
GO
CREATE TABLE [dbo].[jcms_normal_user_favorite] (
	[Id] [int] IDENTITY (1, 1) NOT NULL ,
	[Title] [nvarchar] (150) NOT NULL DEFAULT (''),
	[ModuleType] [nvarchar] (20) NOT NULL DEFAULT (''),
	[ChannelId] [int] NOT NULL ,
	[ContentId] [int] NOT NULL ,
	[AddDate] [datetime] NOT NULL DEFAULT (getdate()),
	[UserId] [int] NOT NULL ,
	CONSTRAINT [PK_normal_user_favorite] PRIMARY KEY CLUSTERED 
	(
		[Id]
	) ON [PRIMARY] 
) ON [PRIMARY]
GO


if exists (select * from sysobjects where id = OBJECT_ID('[jcms_normal_user_notice]') and OBJECTPROPERTY(id, 'IsUserTable') = 1) 
DROP TABLE [jcms_normal_user_notice]
GO
CREATE TABLE [dbo].[jcms_normal_user_notice] (
	[Id] [int] IDENTITY (1, 1) NOT NULL ,
	[Title] [nvarchar] (50) NOT NULL DEFAULT (''),
	[Content] [nvarchar] (250) NOT NULL DEFAULT (''),
	[NoticeType] [nvarchar] (16) NULL ,
	[UserId] [int] NOT NULL ,
	[AddDate] [datetime] NOT NULL DEFAULT (getdate()),
	[State] [int] NOT NULL DEFAULT(0),
	[ReadTime] [datetime] NOT NULL DEFAULT (getdate()),
	CONSTRAINT [PK_normal_user_notice] PRIMARY KEY CLUSTERED 
	(
		[Id]
	) ON [PRIMARY] 
) ON [PRIMARY]
GO


if exists (select * from sysobjects where id = OBJECT_ID('[jcms_normal_usergroup]') and OBJECTPROPERTY(id, 'IsUserTable') = 1) 
DROP TABLE [jcms_normal_usergroup]
GO
CREATE TABLE [dbo].[jcms_normal_usergroup] (
	[Id] [int] IDENTITY (1, 1) NOT NULL ,
	[GroupName] [nvarchar] (50) NOT NULL DEFAULT (''),
	[Setting] [nvarchar] (250) NOT NULL DEFAULT (''),
	[IsLogin] [int] NOT NULL DEFAULT(0),
	[UserTotal] [int] NOT NULL DEFAULT(0),
	CONSTRAINT [PK_normal_usergroup] PRIMARY KEY CLUSTERED 
	(
		[Id]
	) ON [PRIMARY]
) ON [PRIMARY]
GO
INSERT INTO [jcms_normal_usergroup] (GroupName,Setting,IsLogin,UserTotal) Values('临时用户','1,1,1,0|23,1,10,10,1,0,1,1,5,1,1,5,1,1,5,',1,0);
INSERT INTO [jcms_normal_usergroup] (GroupName,Setting,IsLogin,UserTotal) Values('初级用户','1,1,1,0|23,1,10,10,1,0,1,1,10,1,1,10,1,1,10,',1,0);
INSERT INTO [jcms_normal_usergroup] (GroupName,Setting,IsLogin,UserTotal) Values('中级用户','1,1,1,0|23,1,10,10,1,0,1,1,50,1,1,50,1,1,50,',1,0);
INSERT INTO [jcms_normal_usergroup] (GroupName,Setting,IsLogin,UserTotal) Values('高级用户','1,1,1,0|23,1,10,10,1,0,1,1,100,1,1,100,1,1,100,',1,0);
INSERT INTO [jcms_normal_usergroup] (GroupName,Setting,IsLogin,UserTotal) Values('管理用户','1,1,1,0|23,1,10,10,1,0,1,1,100,1,1,100,1,1,100,',1,0);


---------2010-02-04
if exists (select * from sysobjects where id = OBJECT_ID('[jcms_normal_email]') and OBJECTPROPERTY(id, 'IsUserTable') = 1) 
DROP TABLE [jcms_normal_email]
GO
CREATE TABLE [dbo].[jcms_normal_email] (
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[NickName] [nvarchar] (20) NULL DEFAULT (''),
	[EmailAddress] [nvarchar] (80) NOT NULL DEFAULT (''),
	[State] [int] NOT NULL DEFAULT(0),
	[GroupId] [int] NOT NULL DEFAULT(0),
	[SignCode] [nvarchar] (64) NOT NULL DEFAULT (''),
        [SuccessTimes] [int] NOT NULL DEFAULT(0),
        [FailureTimes] [int] NOT NULL DEFAULT(0),
	CONSTRAINT [PK_normal_email] PRIMARY KEY CLUSTERED 
	(
		[Id]
	) ON [PRIMARY] 
) ON [PRIMARY]
GO


if exists (select * from sysobjects where id = OBJECT_ID('[jcms_normal_emailserver]') and OBJECTPROPERTY(id, 'IsUserTable') = 1) 
DROP TABLE [jcms_normal_emailserver]
GO
CREATE TABLE [dbo].[jcms_normal_emailserver] (
	[Id] [int] IDENTITY (1, 1) NOT NULL ,
	[FromAddress] [varchar] (100) NOT NULL ,
	[FromName] [nvarchar] (30) NOT NULL DEFAULT (''),
	[FromPwd] [varchar] (32) NOT NULL DEFAULT (''),
	[SmtpHost] [varchar] (60) NOT NULL DEFAULT (''),
	[Enabled] [int] NOT NULL DEFAULT(1) ,
	CONSTRAINT [PK_normal_emailserver] PRIMARY KEY CLUSTERED
	(
		[Id]
	) ON [PRIMARY]
) ON [PRIMARY]
GO

if exists (select * from sysobjects where id = OBJECT_ID('[jcms_normal_emailgroup]') and OBJECTPROPERTY(id, 'IsUserTable') = 1) 
DROP TABLE [jcms_normal_emailgroup]
GO
CREATE TABLE [dbo].[jcms_normal_emailgroup] (
	[Id] [int] IDENTITY (1, 1) NOT NULL ,
	[GroupName] [nvarchar] (50) NOT NULL DEFAULT (''),
	[EmailTotal] [int] NOT NULL DEFAULT(0),
	CONSTRAINT [PK_normal_emailgroup] PRIMARY KEY CLUSTERED 
	(
		[Id]
	) ON [PRIMARY]
) ON [PRIMARY]
GO
INSERT INTO [jcms_normal_emailgroup] (GroupName,EmailTotal) Values('临时用户',0);
INSERT INTO [jcms_normal_emailgroup] (GroupName,EmailTotal) Values('初级用户',0);
INSERT INTO [jcms_normal_emailgroup] (GroupName,EmailTotal) Values('中级用户',0);
INSERT INTO [jcms_normal_emailgroup] (GroupName,EmailTotal) Values('高级用户',0);


if exists (select * from sysobjects where id = OBJECT_ID('[jcms_normal_emaillogs]') and OBJECTPROPERTY(id, 'IsUserTable') = 1) 
DROP TABLE [jcms_normal_emaillogs]
GO
CREATE TABLE [dbo].[jcms_normal_emaillogs] (
	[Id] [int] IDENTITY (1, 1) NOT NULL ,
	[AdminId] [int] NOT NULL ,
	[SendTitle] [nvarchar] (80) NOT NULL DEFAULT (''),
	[SendUsers] [ntext] NOT NULL DEFAULT (''),
	[SendTime] [datetime] NOT NULL DEFAULT (getdate()),
	[SendIP] [nvarchar] (15) NULL ,
	CONSTRAINT [PK_normal_emaillogs] PRIMARY KEY CLUSTERED 
	(
		[Id]
	) ON [PRIMARY] 
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO


--------2011-04-06新增
if exists (select * from sysobjects where id = OBJECT_ID('[jcms_normal_adminpower]') and OBJECTPROPERTY(id, 'IsUserTable') = 1) 
DROP TABLE [jcms_normal_adminpower]
GO
CREATE TABLE [dbo].[jcms_normal_adminpower] (
	[Id] [int] IDENTITY (1, 1) NOT NULL ,
	[Title] [nvarchar] (20) NOT NULL DEFAULT (''),
	[Code] [nvarchar] (30) NOT NULL DEFAULT (''),
	[PId] [int] NOT NULL DEFAULT(0),
	[Enabled] [int] NOT NULL DEFAULT(1),
	CONSTRAINT [PK_normal_adminpower] PRIMARY KEY CLUSTERED 
	(
		[Id]
	) ON [PRIMARY] 
) ON [PRIMARY]
GO

INSERT INTO [jcms_normal_adminpower] ([Title],[PId],[Code]) VALUES ('模块管理',2,'templateinclude-mng')
INSERT INTO [jcms_normal_adminpower] ([Title],[PId],[Code]) VALUES ('专题管理',3,'special-mng')
INSERT INTO [jcms_normal_adminpower] ([Title],[PId],[Code]) VALUES ('广告管理',4,'adv-mng')
INSERT INTO [jcms_normal_adminpower] ([Title],[PId],[Code]) VALUES ('外站调用管理',5,'js-mng')


if exists (select * from sysobjects where id = OBJECT_ID('[jcms_normal_templateinclude]') and OBJECTPROPERTY(id, 'IsUserTable') = 1) 
DROP TABLE [jcms_normal_templateinclude]
GO
CREATE TABLE [dbo].[jcms_normal_templateinclude] (
	[Id] [int]  IDENTITY (1, 1)  NOT NULL,
	[Title] [nvarchar]  (100) NOT NULL DEFAULT (''),
	[Info] [nvarchar]  (200) NOT NULL DEFAULT (''),
	[PId] [int]  NOT NULL DEFAULT(0),
	[Sort] [int]  NOT NULL DEFAULT(0),
	[NeedBuild] [int]  NOT NULL DEFAULT(0),
	[Source] [nvarchar]  (100) NOT NULL DEFAULT (''),
	CONSTRAINT [PK_normal_templateinclude] PRIMARY KEY CLUSTERED 
	(
		[Id]
	) ON [PRIMARY] 
) ON [PRIMARY]
GO

INSERT INTO [jcms_normal_templateinclude] (Title,PId,Sort,NeedBuild,Source) VALUES ('公用头部文件',1,1,1,'header.htm')
INSERT INTO [jcms_normal_templateinclude] (Title,PId,Sort,NeedBuild,Source) VALUES ('公用尾部文件',1,2,1,'footer.htm')
INSERT INTO [jcms_normal_templateinclude] (Title,PId,Sort,NeedBuild,Source) VALUES ('百度分享',1,3,1,'share.htm')


if exists (select * from sysobjects where id = OBJECT_ID('[jcms_normal_javascript]') and OBJECTPROPERTY(id, 'IsUserTable') = 1) 
DROP TABLE [jcms_normal_javascript]
GO
CREATE TABLE [dbo].[jcms_normal_javascript] (
	[Id] [int] IDENTITY (1, 1) NOT NULL ,
	[Title] [nvarchar] (50) NOT NULL ,
	[Code] [nvarchar] (64) NOT NULL ,
	[TemplateContent] [ntext] NOT NULL DEFAULT (''),
	CONSTRAINT [PK_normal_javascript] PRIMARY KEY CLUSTERED 
	(
		[Id]
	) ON [PRIMARY] 
) ON [PRIMARY]
GO


if exists (select * from sysobjects where id = OBJECT_ID('[jcms_normal_advclass]') and OBJECTPROPERTY(id, 'IsUserTable') = 1) 
DROP TABLE [jcms_normal_advclass]
GO
CREATE TABLE [dbo].[jcms_normal_advclass] (
	[Id] [int] IDENTITY (1, 1) NOT NULL ,
	[Title] [nvarchar] (10) NOT NULL DEFAULT (''),
	[Code] [nvarchar] (10) NOT NULL DEFAULT (''),
	CONSTRAINT [PK_normal_advclass] PRIMARY KEY CLUSTERED 
	(
		[Id]
	) ON [PRIMARY] 
) ON [PRIMARY]
GO

INSERT INTO [jcms_normal_advclass] (Title,Code) values('图片','img')
GO
INSERT INTO [jcms_normal_advclass] (Title,Code) values('动画','flash')
GO
INSERT INTO [jcms_normal_advclass] (Title,Code) values('植入网页','iframe')
GO
INSERT INTO [jcms_normal_advclass] (Title,Code) values('嵌入代码','html')
GO



if exists (select * from sysobjects where id = OBJECT_ID('[jcms_normal_adv]') and OBJECTPROPERTY(id, 'IsUserTable') = 1) 
DROP TABLE [jcms_normal_adv]
GO
CREATE TABLE [dbo].[jcms_normal_adv] (
	[Id] [int] IDENTITY (1, 1) NOT NULL ,
	[Title] [nvarchar] (50) NOT NULL DEFAULT (''),
	[AddDate] [datetime] NOT NULL DEFAULT (getdate()),
	[Content] [ntext] NOT NULL DEFAULT ('') ,
	[State] [int] NOT NULL DEFAULT(0),
	[AdvType] [nvarchar] (10) NOT NULL DEFAULT (''),
	[Url] [nvarchar] (250) NOT NULL DEFAULT (''),
	[Picurl] [nvarchar] (250) NOT NULL DEFAULT (''),
	[Width] [int] NOT NULL DEFAULT(0),
	[Height] [int] NOT NULL DEFAULT(0),
	CONSTRAINT [PK_normal_adv] PRIMARY KEY CLUSTERED 
	(
		[Id]
	) ON [PRIMARY] 
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
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

if exists (select * from sysobjects where id = OBJECT_ID('[jcms_extends_placard]') and OBJECTPROPERTY(id, 'IsUserTable') = 1) 
DROP TABLE [jcms_extends_placard]
GO
CREATE TABLE [dbo].[jcms_extends_placard] (
	[Id] [int] IDENTITY (1, 1) NOT NULL ,
	[Title] [nvarchar] (150) NOT NULL DEFAULT ('') ,
	[Content] [text] NOT NULL DEFAULT ('') ,
	[AddTime] [datetime] NULL ,
	[State] [int] NOT NULL DEFAULT(0) ,
	CONSTRAINT [PK_extends_placard] PRIMARY KEY CLUSTERED 
	(
		[Id]
	) ON [PRIMARY] 
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO

if exists (select * from sysobjects where id = OBJECT_ID('[jcms_extends_vote]') and OBJECTPROPERTY(id, 'IsUserTable') = 1) 
DROP TABLE [jcms_extends_vote]
GO
CREATE TABLE [dbo].[jcms_extends_vote] (
	[Id] [int] IDENTITY (1, 1) NOT NULL ,
	[Title] [nvarchar] (100) NOT NULL DEFAULT ('') ,
	[VoteText] [text] NOT NULL DEFAULT ('') ,
	[VoteNum] [nvarchar] (50) NOT NULL DEFAULT ('') ,
	[VoteTotal] [int] NOT NULL DEFAULT(0) ,
	[Type] [int] NOT NULL DEFAULT(0) ,
	[Lock] [int] NOT NULL DEFAULT(0) ,
	CONSTRAINT [PK_extends_vote] PRIMARY KEY CLUSTERED 
	(
		[Id]
	) ON [PRIMARY] 
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO


if exists (select * from sysobjects where id = OBJECT_ID('[jcms_extends_qqonline]') and OBJECTPROPERTY(id, 'IsUserTable') = 1) 
DROP TABLE [jcms_extends_qqonline]
GO
CREATE TABLE [dbo].[jcms_extends_qqonline] (
	[Id] [int] IDENTITY (1, 1) NOT NULL ,
	[QQID] [nvarchar] (14) NOT NULL DEFAULT ('') ,
	[Title] [nvarchar] (10) NOT NULL DEFAULT ('') ,
	[TColor] [nvarchar] (8) NOT NULL DEFAULT ('') ,
	[Face] [nvarchar] (4) NOT NULL DEFAULT ('') ,
	[OrderNum] [int] NOT NULL DEFAULT(0) ,
	[State] [int] NOT NULL DEFAULT(0)
	CONSTRAINT [PK_extends_qqonline] PRIMARY KEY CLUSTERED 
	(
		[Id]
	) ON [PRIMARY] 
) ON [PRIMARY]
GO

INSERT INTO [jcms_extends_qqonline] ([QQID],[Title],[TColor],[Face],[OrderNum],[State]) VALUES ('4259024','项目合作','#FF3300','1',3,1)
GO
INSERT INTO [jcms_extends_qqonline] ([QQID],[Title],[TColor],[Face],[OrderNum],[State]) VALUES ('112561265','授权服务','#111111','1',3,1)
GO


if exists (select * from sysobjects where id = OBJECT_ID('[jcms_normal_recharge]') and OBJECTPROPERTY(id, 'IsUserTable') = 1) 
DROP TABLE [jcms_normal_recharge]
GO
CREATE TABLE [dbo].[jcms_normal_recharge] (
	[Id] [int] IDENTITY (1, 1) NOT NULL ,
	[OrderNum] [nvarchar] (30) NOT NULL DEFAULT (''),
	[PaymentWay] [nvarchar] (10) NOT NULL DEFAULT (''),
	[Points] [int] NOT NULL DEFAULT(0),
	[OrderTime] [datetime] NOT NULL DEFAULT (getdate()),
	[OrderIP] [nvarchar] (16) NULL ,
	[State] [int] NOT NULL DEFAULT(0),
	[UserId] [int] NOT NULL DEFAULT(0),
	CONSTRAINT [PK_normal_recharge] PRIMARY KEY CLUSTERED 
	(
		[Id]
	) ON [PRIMARY] 
) ON [PRIMARY]
GO


if exists (select * from sysobjects where id = OBJECT_ID('[jcms_normal_user_order]') and OBJECTPROPERTY(id, 'IsUserTable') = 1) 
DROP TABLE [jcms_normal_user_order]
GO
CREATE TABLE [dbo].[jcms_normal_user_order] (
	[Id] [int] IDENTITY (1, 1) NOT NULL ,
	[OrderNum] [nvarchar] (30) NOT NULL DEFAULT (''),
	[TrueName] [nvarchar] (20) NOT NULL DEFAULT (''),
	[Address] [nvarchar] (100) NOT NULL DEFAULT (''),
	[ZipCode] [nvarchar] (10) NOT NULL DEFAULT (''),
	[MobileTel] [nvarchar] (11) NULL ,
	[PaymentWay] [nvarchar] (10) NOT NULL DEFAULT (''),
	[Money] [float] NOT NULL DEFAULT(0),
	[OrderTime] [datetime] NOT NULL DEFAULT (getdate()),
	[OrderIP] [nvarchar] (16) NULL ,
	[State] [int] NOT NULL DEFAULT(0),
	[UserId] [int] NOT NULL DEFAULT(0),
	CONSTRAINT [PK_normal_user_order] PRIMARY KEY CLUSTERED 
	(
		[Id]
	) ON [PRIMARY] 
) ON [PRIMARY]
GO


if exists (select * from sysobjects where id = OBJECT_ID('[jcms_normal_user_cart]') and OBJECTPROPERTY(id, 'IsUserTable') = 1) 
DROP TABLE [jcms_normal_user_cart]
GO
CREATE TABLE [dbo].[jcms_normal_user_cart] (
	[Id] [int] IDENTITY (1, 1) NOT NULL ,
	[ProductId] [int] NOT NULL DEFAULT(0),
	[ProductLink] [nvarchar] (200) NOT NULL DEFAULT (''),
	[BuyCount] [int] NOT NULL DEFAULT(1),
	[CartTime] [datetime] NOT NULL DEFAULT (getdate()),
	[State] [int] NOT NULL DEFAULT(0),
	[UserId] [int] NOT NULL DEFAULT(0),
	CONSTRAINT [PK_normal_user_cart] PRIMARY KEY CLUSTERED 
	(
		[Id]
	) ON [PRIMARY] 
) ON [PRIMARY]
GO

if exists (select * from sysobjects where id = OBJECT_ID('[jcms_normal_user_goods]') and OBJECTPROPERTY(id, 'IsUserTable') = 1) 
DROP TABLE [jcms_normal_user_goods]
GO
CREATE TABLE [dbo].[jcms_normal_user_goods] (
	[Id] [int] IDENTITY (1, 1) NOT NULL ,
	[OrderNum] [nvarchar] (30) NOT NULL DEFAULT (''),
	[ProductId] [int] NOT NULL DEFAULT(0),
	[ProductName] [nvarchar] (150) NOT NULL DEFAULT (''),
	[ProductImg] [nvarchar] (150) NOT NULL DEFAULT (''),
	[ProductLink] [nvarchar] (200) NOT NULL DEFAULT (''),
	[UnitPrice] [float] NOT NULL DEFAULT(0),
	[BuyCount] [int] NOT NULL DEFAULT(1),
	[TotalPrice] [float] NOT NULL DEFAULT(0),
	[GoodsTime] [datetime] NOT NULL DEFAULT (getdate()),
	[State] [int] NOT NULL DEFAULT(0),
	[UserId] [int] NOT NULL DEFAULT(0),
	CONSTRAINT [PK_normal_user_goods] PRIMARY KEY CLUSTERED 
	(
		[Id]
	) ON [PRIMARY] 
) ON [PRIMARY]
GO


if exists (select * from sysobjects where id = OBJECT_ID('[jcms_normal_question]') and OBJECTPROPERTY(id, 'IsUserTable') = 1) 
DROP TABLE [jcms_normal_question]
GO
CREATE TABLE [dbo].[jcms_normal_question] (
	[Id] [int] IDENTITY (1, 1) NOT NULL ,
	[ParentId] [int] NOT NULL DEFAULT(0) ,
	[AddDate] [datetime] NULL ,
	[Title] [nvarchar] (50) NOT NULL DEFAULT ('') ,
	[Content] [nvarchar] (250) NOT NULL DEFAULT ('') ,
	[IP] [varchar] (15) NOT NULL DEFAULT ('') ,
	[UserName] [nvarchar] (50) NOT NULL DEFAULT ('') ,
	[UserId] [int] NOT NULL DEFAULT(0) ,
	[ClassId] [int] NOT NULL DEFAULT(0) ,
	[IsPass] [int] NOT NULL DEFAULT(0) ,
	CONSTRAINT [PK_normal_question] PRIMARY KEY CLUSTERED 
	(
		[Id]
	) ON [PRIMARY] 
) ON [PRIMARY]
GO


if exists (select * from sysobjects where id = OBJECT_ID('[jcms_normal_question_class]') and OBJECTPROPERTY(id, 'IsUserTable') = 1) 
DROP TABLE [jcms_normal_question_class]
GO
CREATE TABLE [dbo].[jcms_normal_question_class] (
	[Id] [int] IDENTITY (1, 1) NOT NULL ,
	[Title] [nvarchar] (50) NOT NULL DEFAULT ('') ,
	[PId] [int] NOT NULL DEFAULT(0) ,
	CONSTRAINT [PK_normal_question_class] PRIMARY KEY CLUSTERED 
	(
		[Id]
	) ON [PRIMARY] 
) ON [PRIMARY]
GO

INSERT INTO [jcms_normal_question_class] ([Title],[PId]) VALUES ('技术咨询',1)
GO
INSERT INTO [jcms_normal_question_class] ([Title],[PId]) VALUES ('产品咨询',2)
GO
INSERT INTO [jcms_normal_question_class] ([Title],[PId]) VALUES ('意见建议',3)
GO
INSERT INTO [jcms_normal_question_class] ([Title],[PId]) VALUES ('其他问题',4)
GO


if exists (select * from sysobjects where id = OBJECT_ID('[jcms_normal_link]') and OBJECTPROPERTY(id, 'IsUserTable') = 1) 
DROP TABLE [jcms_normal_link]
GO
CREATE TABLE [dbo].[jcms_normal_link] (
	[Id] [int] IDENTITY (1, 1) NOT NULL ,
	[Title] [nvarchar] (50) NOT NULL DEFAULT ('') ,
	[Url] [nvarchar] (150) NOT NULL DEFAULT ('') ,
	[ImgPath] [nvarchar] (150) NOT NULL DEFAULT ('') ,
	[Info] [nvarchar] (250) NOT NULL DEFAULT ('') ,
	[OrderNum] [int] NOT NULL DEFAULT(0) ,
	[State] [int] NOT NULL DEFAULT(0) ,
	[Style] [int] NOT NULL DEFAULT(0) ,
	CONSTRAINT [PK_normal_link] PRIMARY KEY CLUSTERED 
	(
		[Id]
	) ON [PRIMARY] 
) ON [PRIMARY]
GO

if exists (select * from sysobjects where id = OBJECT_ID('[jcms_normal_digg]') and OBJECTPROPERTY(id, 'IsUserTable') = 1) 
DROP TABLE [jcms_normal_digg]
GO
CREATE TABLE [dbo].[jcms_normal_digg] (
	[Id] [int] IDENTITY (1, 1) NOT NULL ,
	[ContentId] [int] NOT NULL DEFAULT(0) ,
	[ChannelType] [varchar] (10) NOT NULL DEFAULT ('') ,
	[DiggNum] [int] NOT NULL DEFAULT(0) ,
	CONSTRAINT [PK_normal_digg] PRIMARY KEY CLUSTERED 
	(
		[Id]
	) ON [PRIMARY] 
) ON [PRIMARY]
GO


if exists (select * from sysobjects where id = OBJECT_ID('[jcms_normal_review]') and OBJECTPROPERTY(id, 'IsUserTable') = 1) 
DROP TABLE [jcms_normal_review]
GO
CREATE TABLE [dbo].[jcms_normal_review] (
	[Id] [int] IDENTITY (1, 1) NOT NULL ,
	[ChannelId] [int] NOT NULL DEFAULT(0) ,
	[ParentId] [int] NOT NULL DEFAULT(0)  ,
	[ContentId] [int] NOT NULL DEFAULT(0) ,
	[AddDate] [datetime] NULL ,
	[Content] [nvarchar] (250) NOT NULL DEFAULT ('') ,
	[IP] [nvarchar] (15) NOT NULL DEFAULT ('') ,
	[UserName] [nvarchar] (50) NOT NULL DEFAULT ('') ,
	[IsPass] [int] NOT NULL DEFAULT(0) ,
	CONSTRAINT [PK_normal_review] PRIMARY KEY CLUSTERED 
	(
		[Id]
	) ON [PRIMARY] 
) ON [PRIMARY]
GO


if exists (select * from sysobjects where id = OBJECT_ID('[jcms_normal_language]') and OBJECTPROPERTY(id, 'IsUserTable') = 1) 
DROP TABLE [jcms_normal_language]
GO
CREATE TABLE [dbo].[jcms_normal_language] (
	[Id] [int] IDENTITY (1, 1) NOT NULL ,
	[Title] [nvarchar] (20) NOT NULL DEFAULT ('') ,
	[Code] [nvarchar] (20) NOT NULL DEFAULT ('') ,
	CONSTRAINT [PK_normal_language] PRIMARY KEY CLUSTERED 
	(
		[Id]
	) ON [PRIMARY] 
) ON [PRIMARY]
GO
INSERT INTO [jcms_normal_language] ([Title],[Code]) VALUES ('中文','cn')
GO
INSERT INTO [jcms_normal_language] ([Title],[Code]) VALUES ('英文','en')
GO


if exists (select * from sysobjects where id = OBJECT_ID('[jcms_normal_user_oauth]') and OBJECTPROPERTY(id, 'IsUserTable') = 1) 
DROP TABLE [jcms_normal_user_oauth]
GO
CREATE TABLE [dbo].[jcms_normal_user_oauth] (
	[Id] [int] IDENTITY (1, 1) NOT NULL ,
	[Title] [nvarchar] (20) NOT NULL DEFAULT ('') ,
	[Code] [nvarchar] (20) NOT NULL DEFAULT ('') ,
	[PId] [int] NOT NULL DEFAULT(0) ,
	[Enabled] [int] NOT NULL DEFAULT(1) ,
	CONSTRAINT [PK_normal_user_oauth] PRIMARY KEY CLUSTERED 
	(
		[Id]
	) ON [PRIMARY] 
) ON [PRIMARY]
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
