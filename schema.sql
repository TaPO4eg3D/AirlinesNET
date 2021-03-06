/*    ==Scripting Parameters==

    Source Server Version : SQL Server 2016 (13.0.4001)
    Source Database Engine Edition : Microsoft SQL Server Enterprise Edition
    Source Database Engine Type : Standalone SQL Server

    Target Server Version : SQL Server 2017
    Target Database Engine Edition : Microsoft SQL Server Standard Edition
    Target Database Engine Type : Standalone SQL Server
*/
USE [master]
GO
/****** Object:  Database [Airlines]    Script Date: 6/18/2018 9:41:15 AM ******/
CREATE DATABASE [Airlines]
 CONTAINMENT = NONE
 ON  PRIMARY 
( NAME = N'Airlines', FILENAME = N'E:\SQL_server\MSSQL13.MSSQLSERVER\MSSQL\DATA\Airlines.mdf' , SIZE = 8192KB , MAXSIZE = UNLIMITED, FILEGROWTH = 65536KB )
 LOG ON 
( NAME = N'Airlines_log', FILENAME = N'E:\SQL_server\MSSQL13.MSSQLSERVER\MSSQL\DATA\Airlines_log.ldf' , SIZE = 8192KB , MAXSIZE = 2048GB , FILEGROWTH = 65536KB )
GO
ALTER DATABASE [Airlines] SET COMPATIBILITY_LEVEL = 130
GO
IF (1 = FULLTEXTSERVICEPROPERTY('IsFullTextInstalled'))
begin
EXEC [Airlines].[dbo].[sp_fulltext_database] @action = 'enable'
end
GO
ALTER DATABASE [Airlines] SET ANSI_NULL_DEFAULT OFF 
GO
ALTER DATABASE [Airlines] SET ANSI_NULLS OFF 
GO
ALTER DATABASE [Airlines] SET ANSI_PADDING OFF 
GO
ALTER DATABASE [Airlines] SET ANSI_WARNINGS OFF 
GO
ALTER DATABASE [Airlines] SET ARITHABORT OFF 
GO
ALTER DATABASE [Airlines] SET AUTO_CLOSE OFF 
GO
ALTER DATABASE [Airlines] SET AUTO_SHRINK OFF 
GO
ALTER DATABASE [Airlines] SET AUTO_UPDATE_STATISTICS ON 
GO
ALTER DATABASE [Airlines] SET CURSOR_CLOSE_ON_COMMIT OFF 
GO
ALTER DATABASE [Airlines] SET CURSOR_DEFAULT  GLOBAL 
GO
ALTER DATABASE [Airlines] SET CONCAT_NULL_YIELDS_NULL OFF 
GO
ALTER DATABASE [Airlines] SET NUMERIC_ROUNDABORT OFF 
GO
ALTER DATABASE [Airlines] SET QUOTED_IDENTIFIER OFF 
GO
ALTER DATABASE [Airlines] SET RECURSIVE_TRIGGERS OFF 
GO
ALTER DATABASE [Airlines] SET  DISABLE_BROKER 
GO
ALTER DATABASE [Airlines] SET AUTO_UPDATE_STATISTICS_ASYNC OFF 
GO
ALTER DATABASE [Airlines] SET DATE_CORRELATION_OPTIMIZATION OFF 
GO
ALTER DATABASE [Airlines] SET TRUSTWORTHY OFF 
GO
ALTER DATABASE [Airlines] SET ALLOW_SNAPSHOT_ISOLATION OFF 
GO
ALTER DATABASE [Airlines] SET PARAMETERIZATION SIMPLE 
GO
ALTER DATABASE [Airlines] SET READ_COMMITTED_SNAPSHOT OFF 
GO
ALTER DATABASE [Airlines] SET HONOR_BROKER_PRIORITY OFF 
GO
ALTER DATABASE [Airlines] SET RECOVERY FULL 
GO
ALTER DATABASE [Airlines] SET  MULTI_USER 
GO
ALTER DATABASE [Airlines] SET PAGE_VERIFY CHECKSUM  
GO
ALTER DATABASE [Airlines] SET DB_CHAINING OFF 
GO
ALTER DATABASE [Airlines] SET FILESTREAM( NON_TRANSACTED_ACCESS = OFF ) 
GO
ALTER DATABASE [Airlines] SET TARGET_RECOVERY_TIME = 60 SECONDS 
GO
ALTER DATABASE [Airlines] SET DELAYED_DURABILITY = DISABLED 
GO
EXEC sys.sp_db_vardecimal_storage_format N'Airlines', N'ON'
GO
ALTER DATABASE [Airlines] SET QUERY_STORE = OFF
GO
USE [Airlines]
GO
ALTER DATABASE SCOPED CONFIGURATION SET LEGACY_CARDINALITY_ESTIMATION = OFF;
GO
ALTER DATABASE SCOPED CONFIGURATION FOR SECONDARY SET LEGACY_CARDINALITY_ESTIMATION = PRIMARY;
GO
ALTER DATABASE SCOPED CONFIGURATION SET MAXDOP = 0;
GO
ALTER DATABASE SCOPED CONFIGURATION FOR SECONDARY SET MAXDOP = PRIMARY;
GO
ALTER DATABASE SCOPED CONFIGURATION SET PARAMETER_SNIFFING = ON;
GO
ALTER DATABASE SCOPED CONFIGURATION FOR SECONDARY SET PARAMETER_SNIFFING = PRIMARY;
GO
ALTER DATABASE SCOPED CONFIGURATION SET QUERY_OPTIMIZER_HOTFIXES = OFF;
GO
ALTER DATABASE SCOPED CONFIGURATION FOR SECONDARY SET QUERY_OPTIMIZER_HOTFIXES = PRIMARY;
GO
USE [Airlines]
GO
/****** Object:  Table [dbo].[Airport]    Script Date: 6/18/2018 9:41:16 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Airport](
	[AirportID] [int] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](110) NOT NULL,
	[Country] [nvarchar](110) NULL,
	[City] [nvarchar](110) NULL,
 CONSTRAINT [PK_Airport] PRIMARY KEY CLUSTERED 
(
	[AirportID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Company]    Script Date: 6/18/2018 9:41:16 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Company](
	[CompanyID] [int] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](110) NOT NULL,
	[PhoneNumber] [nvarchar](110) NULL,
 CONSTRAINT [PK_Company] PRIMARY KEY CLUSTERED 
(
	[CompanyID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[DocumentType]    Script Date: 6/18/2018 9:41:16 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[DocumentType](
	[DocumentTypeID] [int] NOT NULL,
	[Name] [nvarchar](110) NOT NULL,
 CONSTRAINT [PK_DocumentType] PRIMARY KEY CLUSTERED 
(
	[DocumentTypeID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Flight]    Script Date: 6/18/2018 9:41:16 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Flight](
	[FlightID] [int] IDENTITY(1,1) NOT NULL,
	[FlightName] [nvarchar](64) NOT NULL,
	[CompanyID] [int] NOT NULL,
	[StartPoint] [int] NOT NULL,
	[EndPoint] [int] NOT NULL,
	[DepartureTime] [datetime] NOT NULL,
	[ArriveTime] [datetime] NOT NULL,
	[Seats] [int] NOT NULL,
	[Price] [money] NOT NULL,
 CONSTRAINT [PK_Flight] PRIMARY KEY CLUSTERED 
(
	[FlightID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[PastFlight]    Script Date: 6/18/2018 9:41:16 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[PastFlight](
	[FlightID] [int] NOT NULL,
	[FlightName] [nvarchar](64) NOT NULL,
	[CompanyID] [int] NOT NULL,
	[StartPoint] [int] NOT NULL,
	[EndPoint] [int] NOT NULL,
	[DepartureTime] [datetime] NOT NULL,
	[ArriveTime] [datetime] NOT NULL,
	[Seats] [int] NOT NULL,
	[Price] [money] NOT NULL,
 CONSTRAINT [PK_PastFlight] PRIMARY KEY CLUSTERED 
(
	[FlightID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[PastPurchase]    Script Date: 6/18/2018 9:41:16 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[PastPurchase](
	[UserID] [int] NOT NULL,
	[FlightID] [int] NOT NULL,
	[Status] [bit] NOT NULL,
 CONSTRAINT [PK_PastPurchase] PRIMARY KEY CLUSTERED 
(
	[UserID] ASC,
	[FlightID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Profile]    Script Date: 6/18/2018 9:41:16 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Profile](
	[ProfileID] [int] IDENTITY(1,1) NOT NULL,
	[FullName] [nvarchar](110) NOT NULL,
	[DocumentType] [int] NOT NULL,
	[SeriesAndNumber] [nvarchar](110) NOT NULL,
	[Address] [nvarchar](255) NOT NULL,
	[PhoneNumber] [nvarchar](110) NOT NULL,
	[UserID] [int] NULL,
 CONSTRAINT [PK_Profile] PRIMARY KEY CLUSTERED 
(
	[ProfileID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Purchase]    Script Date: 6/18/2018 9:41:16 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Purchase](
	[UserID] [int] NOT NULL,
	[FlightID] [int] NOT NULL,
	[Status] [bit] NOT NULL,
 CONSTRAINT [PK_Purchase] PRIMARY KEY CLUSTERED 
(
	[UserID] ASC,
	[FlightID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[User]    Script Date: 6/18/2018 9:41:16 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[User](
	[UserID] [int] IDENTITY(1,1) NOT NULL,
	[Username] [nvarchar](110) NOT NULL,
	[Password] [nvarchar](110) NOT NULL,
	[Role] [int] NOT NULL,
 CONSTRAINT [PK_User] PRIMARY KEY CLUSTERED 
(
	[UserID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET IDENTITY_INSERT [dbo].[Airport] ON 

INSERT [dbo].[Airport] ([AirportID], [Name], [Country], [City]) VALUES (3, N'Ryoukbergs''s airport', N'Исландия', N'Рёокуберг')
INSERT [dbo].[Airport] ([AirportID], [Name], [Country], [City]) VALUES (4, N'Домодедово', N'Россия', N'Москва')
INSERT [dbo].[Airport] ([AirportID], [Name], [Country], [City]) VALUES (5, N'Шереметьево', N'Россия', N'Москва')
INSERT [dbo].[Airport] ([AirportID], [Name], [Country], [City]) VALUES (6, N'Казанский аеропорт', N'Россия', N'Казань')
INSERT [dbo].[Airport] ([AirportID], [Name], [Country], [City]) VALUES (7, N'Красняорский аэропорт', N'Россия', N'Красноярск')
SET IDENTITY_INSERT [dbo].[Airport] OFF
SET IDENTITY_INSERT [dbo].[Company] ON 

INSERT [dbo].[Company] ([CompanyID], [Name], [PhoneNumber]) VALUES (9, N'Аэрофлот', N'+79063678596')
INSERT [dbo].[Company] ([CompanyID], [Name], [PhoneNumber]) VALUES (10, N'AmericanExpress', N'+15987266595')
INSERT [dbo].[Company] ([CompanyID], [Name], [PhoneNumber]) VALUES (11, N'IrelandFly', N'+63258452359')
SET IDENTITY_INSERT [dbo].[Company] OFF
INSERT [dbo].[DocumentType] ([DocumentTypeID], [Name]) VALUES (0, N'Паспорт')
INSERT [dbo].[DocumentType] ([DocumentTypeID], [Name]) VALUES (1, N'Свидетельство о рождении')
SET IDENTITY_INSERT [dbo].[Flight] ON 

INSERT [dbo].[Flight] ([FlightID], [FlightName], [CompanyID], [StartPoint], [EndPoint], [DepartureTime], [ArriveTime], [Seats], [Price]) VALUES (3, N'312312', 9, 3, 6, CAST(N'2018-06-17T00:00:00.000' AS DateTime), CAST(N'2018-06-17T00:00:00.000' AS DateTime), 312312321, 21312.0000)
SET IDENTITY_INSERT [dbo].[Flight] OFF
INSERT [dbo].[PastFlight] ([FlightID], [FlightName], [CompanyID], [StartPoint], [EndPoint], [DepartureTime], [ArriveTime], [Seats], [Price]) VALUES (2, N'SD232', 9, 3, 7, CAST(N'2018-06-17T00:00:00.000' AS DateTime), CAST(N'2018-06-18T00:00:00.000' AS DateTime), 123, 5000.0000)
INSERT [dbo].[PastPurchase] ([UserID], [FlightID], [Status]) VALUES (7, 2, 1)
SET IDENTITY_INSERT [dbo].[Profile] ON 

INSERT [dbo].[Profile] ([ProfileID], [FullName], [DocumentType], [SeriesAndNumber], [Address], [PhoneNumber], [UserID]) VALUES (6, N'Фомин Денис Александрович', 0, N'228 14880', N'ул. Арбузова', N'+79059756353', 7)
INSERT [dbo].[Profile] ([ProfileID], [FullName], [DocumentType], [SeriesAndNumber], [Address], [PhoneNumber], [UserID]) VALUES (7, N'Тищенко Сергей Игоревич', 0, N'228 14881', N'Ачинск, ул. "На воклзале", д -, кв -', N'+79059756353', NULL)
SET IDENTITY_INSERT [dbo].[Profile] OFF
INSERT [dbo].[Purchase] ([UserID], [FlightID], [Status]) VALUES (7, 3, 1)
SET IDENTITY_INSERT [dbo].[User] ON 

INSERT [dbo].[User] ([UserID], [Username], [Password], [Role]) VALUES (7, N'1', N'6b86b273ff34fce19d6b804eff5a3f5747ada4eaa22f1d49c01e52ddb7875b4b', 1)
SET IDENTITY_INSERT [dbo].[User] OFF
ALTER TABLE [dbo].[User] ADD  CONSTRAINT [DF_User_Role]  DEFAULT ((0)) FOR [Role]
GO
ALTER TABLE [dbo].[Flight]  WITH CHECK ADD  CONSTRAINT [FK_Flight_Airport] FOREIGN KEY([StartPoint])
REFERENCES [dbo].[Airport] ([AirportID])
GO
ALTER TABLE [dbo].[Flight] CHECK CONSTRAINT [FK_Flight_Airport]
GO
ALTER TABLE [dbo].[Flight]  WITH CHECK ADD  CONSTRAINT [FK_Flight_Airport1] FOREIGN KEY([EndPoint])
REFERENCES [dbo].[Airport] ([AirportID])
GO
ALTER TABLE [dbo].[Flight] CHECK CONSTRAINT [FK_Flight_Airport1]
GO
ALTER TABLE [dbo].[Flight]  WITH CHECK ADD  CONSTRAINT [FK_Flight_Company] FOREIGN KEY([CompanyID])
REFERENCES [dbo].[Company] ([CompanyID])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[Flight] CHECK CONSTRAINT [FK_Flight_Company]
GO
ALTER TABLE [dbo].[PastFlight]  WITH CHECK ADD  CONSTRAINT [FK_PastFlight_Company] FOREIGN KEY([CompanyID])
REFERENCES [dbo].[Company] ([CompanyID])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[PastFlight] CHECK CONSTRAINT [FK_PastFlight_Company]
GO
ALTER TABLE [dbo].[PastPurchase]  WITH CHECK ADD  CONSTRAINT [FK_PastPurchase_PastFlight] FOREIGN KEY([FlightID])
REFERENCES [dbo].[PastFlight] ([FlightID])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[PastPurchase] CHECK CONSTRAINT [FK_PastPurchase_PastFlight]
GO
ALTER TABLE [dbo].[PastPurchase]  WITH CHECK ADD  CONSTRAINT [FK_PastPurchase_User] FOREIGN KEY([UserID])
REFERENCES [dbo].[User] ([UserID])
GO
ALTER TABLE [dbo].[PastPurchase] CHECK CONSTRAINT [FK_PastPurchase_User]
GO
ALTER TABLE [dbo].[Profile]  WITH CHECK ADD  CONSTRAINT [FK_Profile_DocumentType] FOREIGN KEY([DocumentType])
REFERENCES [dbo].[DocumentType] ([DocumentTypeID])
GO
ALTER TABLE [dbo].[Profile] CHECK CONSTRAINT [FK_Profile_DocumentType]
GO
ALTER TABLE [dbo].[Profile]  WITH CHECK ADD  CONSTRAINT [FK_Profile_User] FOREIGN KEY([UserID])
REFERENCES [dbo].[User] ([UserID])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[Profile] CHECK CONSTRAINT [FK_Profile_User]
GO
ALTER TABLE [dbo].[Purchase]  WITH CHECK ADD  CONSTRAINT [FK_Purchase_Flight] FOREIGN KEY([FlightID])
REFERENCES [dbo].[Flight] ([FlightID])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[Purchase] CHECK CONSTRAINT [FK_Purchase_Flight]
GO
ALTER TABLE [dbo].[Purchase]  WITH CHECK ADD  CONSTRAINT [FK_Purchase_User] FOREIGN KEY([UserID])
REFERENCES [dbo].[User] ([UserID])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[Purchase] CHECK CONSTRAINT [FK_Purchase_User]
GO
USE [master]
GO
ALTER DATABASE [Airlines] SET  READ_WRITE 
GO
