USE [master]
GO

CREATE DATABASE [FamilyTree]
GO

USE [FamilyTree]
GO
/****** Object:  Table [dbo].[Event]    Script Date: 6/7/2023 9:38:14 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Event](
	[EventID] [int] NOT NULL identity(1,1),
	[CreatorID] [int] NULL,
	[EventName] [nvarchar](200) NOT NULL,
	[StartDate] [datetime] NULL,
	[EndDate] [datetime] NULL,
	[Information] [nvarchar](500) NULL,
	[Status] [varchar](10) NULL,
PRIMARY KEY CLUSTERED 
(
	[EventID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
CREATE TABLE [dbo].[Admin](
	[AdminID] [int] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](40) NOT NULL,
	[PhoneNumber] [char](10) NULL,
	[Email] [varchar](100) NOT NULL,
	[Password] [varchar](30) NOT NULL,
 CONSTRAINT [PK_Admin] PRIMARY KEY CLUSTERED 
(
	[AdminID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]

GO
CREATE TABLE [dbo].[EventReport](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[EventID] [int] NOT NULL,
	[Status] [int] NULL,
	[Date] [date] NULL, -- Added Date column
 CONSTRAINT [PK_EventReport] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]

CREATE TABLE [dbo].[AccountReport](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[UserID] [int] NOT NULL,
	[Status] [int] NULL,
	[Date] [date] NULL, -- Added Date column
 CONSTRAINT [PK_AccountReport] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]



CREATE TABLE [dbo].[Family](
	[FamilyID] [int] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](40) NOT NULL,
	[Address] [nvarchar](200) NOT NULL,
	[Information] [nvarchar](500) NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[FamilyID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO

CREATE TABLE [dbo].[RelationshipDetail](
	[RelationshipDetailID] [int] IDENTITY(1,1) NOT NULL,
	[RelationshipName] [varchar](180) NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[RelationshipDetailID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO

CREATE TABLE [dbo].[Relationship](
	[RelationshipID] [int] NOT NULL,
	[UserID_1] [int] NOT NULL,
	[RelationshipDetailID] [int] NOT NULL,
	[UserID_2] [int] NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[RelationshipID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO

CREATE TABLE [dbo].[User](
	[UserID] [int] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](40) NOT NULL,
	[Email] [varchar](100) NULL,
	[Password] [varchar](30) NULL,
	[Gender] [bit] NULL,
	[PhoneNumber] [char](10) NULL,
	[Birthday] [date] NULL,
	[FamilyID] [int] NULL,
	[Code] [char](10) NULL,
	[Status] [nvarchar](40 )NULL,
PRIMARY KEY CLUSTERED 
(
	[UserID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[UserJoin]    Script Date: 6/7/2023 9:38:14 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[UserJoin](
	[EventID] [int] NOT NULL,
	[UserID] [int] NOT NULL,
	[Status] [varchar](10) NOT NULL,
	[View] [int] NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[EventID] ASC,
	[UserID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO

GO
INSERT INTO Admin(Name, PhoneNumber, Email, Password)
VALUES('Admin Test','1803914555','admin@admin.com','1'),
      ('Admin','0123456789','mainadmin@admin.com','1');


INSERT INTO Family(Name, Address, Information)
VALUES('Happy family',N'123 Xa lộ Hà Nội, quận 9, thành phố Thủ Đức',N'Một gia đình hạnh phúc, muốn tìm một app có thể giúp các bạn nhỏ xem cây gia đình của gia đình mình, giúp các em yêu gia đình mình hơn'),
      ('Rich family',N'1 Bùi Viện, Thành phố Hồ Chí Minh',N'Không có gì ngoài tiền, cứ build app đi tiền nong k phải là vấn đề');

INSERT INTO RelationshipDetail(RelationshipName)
VALUES('Parents-children'),
      ('Brothers/Sisters-younger'),
	  ('Husband-wife'),
	  ('Grandparents-grandchildren');

INSERT INTO [User](name, Email, password, gender, phonenumber, Birthday, FamilyID, Code, status)
VALUES('User Test','user@user.com','1','1','0987654321',CAST(N'2002-10-16' AS DateTime),'1','ABCDXYZ','Using'),
	  (N'bố của User Test','userfather@user.com','1','1','0918273645',CAST(N'1980-3-9' AS DateTime),'1','ABCDXYZT','Using'),
	  (N'mẹ của User Test','usermother@user.com','1','0','0918273641',CAST(N'1981-10-02' AS DateTime),'1','ABCDXYZU','Using'),
	  (N'anh của User Test','userbrother@user.com','1','1','0987654322',CAST(N'2001-12-02' AS DateTime),'1','ABCDXYZQ','Using'),
	  (N'ông của User Test','usergrandfather@user.com','1','1','0987654320',CAST(N'1960-12-30' AS DateTime),'1','ABCDXYZH','Using'),
	  (N'Rich Guy','rich@user.com','1','1','0987654123',CAST(N'2000-01-13' AS DateTime),'2','ANHBAGA','Using');

INSERT INTO Relationship(RelationshipID,UserID_1,RelationshipDetailID,UserID_2)
VALUES(1,2,1,1),
	  (2,3,1,1),
	  (3,4,2,1),
	  (4,5,4,1),
	  (5,2,3,3);

INSERT INTO Event(CreatorID,EventName,StartDate, EndDate, Information, Status)
VALUES(1,N'Ngày câu cá',CAST(N'2023-06-16' AS DateTime),CAST(N'2023-06-17' AS DateTime),N'Ngày cả gia đình đi câu cá vui vẻ', 'Waiting'),
	  (2,N'Liên hoan chả vì lý do gì',CAST(N'2023-07-13' AS DateTime),CAST(N'2023-07-13' AS DateTime),N'Thích thì liên hoan đấy, thì sao nào', 'Waiting');

INSERT INTO UserJoin(EventID,UserID,Status,[View])
VALUES(1,2,'Join',1),
      (1,3,'Join',1),
	  (1,4,'Join',1),
	  (1,5,'absent',1),
	  (2,3,'absent',1);

GO
ALTER TABLE [dbo].[Relationship]  WITH CHECK ADD  CONSTRAINT [FK_Relationship_RelationshipDetail] FOREIGN KEY([RelationshipDetailID])
REFERENCES [dbo].[RelationshipDetail] ([RelationshipDetailID])
GO

ALTER TABLE [dbo].[Relationship] CHECK CONSTRAINT [FK_Relationship_RelationshipDetail]
GO
ALTER TABLE [dbo].[Relationship]  WITH CHECK ADD  CONSTRAINT [FK_Relationship_User] FOREIGN KEY([UserID_1])
REFERENCES [dbo].[User] ([UserID])
GO
ALTER TABLE [dbo].[Relationship] CHECK CONSTRAINT [FK_Relationship_User]
GO
ALTER TABLE [dbo].[Relationship]  WITH CHECK ADD  CONSTRAINT [FK_Relationship_User1] FOREIGN KEY([UserID_2])
REFERENCES [dbo].[User] ([UserID])
GO
ALTER TABLE [dbo].[Relationship] CHECK CONSTRAINT [FK_Relationship_User1]
GO
ALTER TABLE [dbo].[User]  WITH CHECK ADD  CONSTRAINT [FK_User_Family1] FOREIGN KEY([FamilyID])
REFERENCES [dbo].[Family] ([FamilyID])
GO
ALTER TABLE [dbo].[User] CHECK CONSTRAINT [FK_User_Family1]
GO
ALTER TABLE [dbo].[UserJoin]  WITH CHECK ADD  CONSTRAINT [FK_UserJoin_Event] FOREIGN KEY([EventID])
REFERENCES [dbo].[Event] ([EventID])
GO
ALTER TABLE [dbo].[UserJoin] CHECK CONSTRAINT [FK_UserJoin_Event]
GO
ALTER TABLE [dbo].[UserJoin]  WITH CHECK ADD  CONSTRAINT [FK_UserJoin_User] FOREIGN KEY([UserID])
REFERENCES [dbo].[User] ([UserID])
GO
ALTER TABLE [dbo].[UserJoin] CHECK CONSTRAINT [FK_UserJoin_User]
GO
USE [master]
GO
ALTER DATABASE [Tree] SET  READ_WRITE 
GO
