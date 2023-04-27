USE [master]
GO
/****** Object:  Database [aspnet-WebNhaHang-20230414061241]    Script Date: 28-Apr-23 12:33:37 AM ******/
CREATE DATABASE [aspnet-WebNhaHang-20230414061241]
 CONTAINMENT = NONE
 ON  PRIMARY 
( NAME = N'aspnet-WebNhaHang-20230414061241.mdf', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL16.SQLEXPRESS04\MSSQL\DATA\aspnet-WebNhaHang-20230414061241.mdf' , SIZE = 8192KB , MAXSIZE = UNLIMITED, FILEGROWTH = 65536KB )
 LOG ON 
( NAME = N'aspnet-WebNhaHang-20230414061241_log.ldf', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL16.SQLEXPRESS04\MSSQL\DATA\aspnet-WebNhaHang-20230414061241_log.ldf' , SIZE = 8192KB , MAXSIZE = 2048GB , FILEGROWTH = 65536KB )
 WITH CATALOG_COLLATION = DATABASE_DEFAULT, LEDGER = OFF
GO
ALTER DATABASE [aspnet-WebNhaHang-20230414061241] SET COMPATIBILITY_LEVEL = 160
GO
IF (1 = FULLTEXTSERVICEPROPERTY('IsFullTextInstalled'))
begin
EXEC [aspnet-WebNhaHang-20230414061241].[dbo].[sp_fulltext_database] @action = 'enable'
end
GO
ALTER DATABASE [aspnet-WebNhaHang-20230414061241] SET ANSI_NULL_DEFAULT OFF 
GO
ALTER DATABASE [aspnet-WebNhaHang-20230414061241] SET ANSI_NULLS OFF 
GO
ALTER DATABASE [aspnet-WebNhaHang-20230414061241] SET ANSI_PADDING OFF 
GO
ALTER DATABASE [aspnet-WebNhaHang-20230414061241] SET ANSI_WARNINGS OFF 
GO
ALTER DATABASE [aspnet-WebNhaHang-20230414061241] SET ARITHABORT OFF 
GO
ALTER DATABASE [aspnet-WebNhaHang-20230414061241] SET AUTO_CLOSE ON 
GO
ALTER DATABASE [aspnet-WebNhaHang-20230414061241] SET AUTO_SHRINK OFF 
GO
ALTER DATABASE [aspnet-WebNhaHang-20230414061241] SET AUTO_UPDATE_STATISTICS ON 
GO
ALTER DATABASE [aspnet-WebNhaHang-20230414061241] SET CURSOR_CLOSE_ON_COMMIT OFF 
GO
ALTER DATABASE [aspnet-WebNhaHang-20230414061241] SET CURSOR_DEFAULT  GLOBAL 
GO
ALTER DATABASE [aspnet-WebNhaHang-20230414061241] SET CONCAT_NULL_YIELDS_NULL OFF 
GO
ALTER DATABASE [aspnet-WebNhaHang-20230414061241] SET NUMERIC_ROUNDABORT OFF 
GO
ALTER DATABASE [aspnet-WebNhaHang-20230414061241] SET QUOTED_IDENTIFIER OFF 
GO
ALTER DATABASE [aspnet-WebNhaHang-20230414061241] SET RECURSIVE_TRIGGERS OFF 
GO
ALTER DATABASE [aspnet-WebNhaHang-20230414061241] SET  DISABLE_BROKER 
GO
ALTER DATABASE [aspnet-WebNhaHang-20230414061241] SET AUTO_UPDATE_STATISTICS_ASYNC OFF 
GO
ALTER DATABASE [aspnet-WebNhaHang-20230414061241] SET DATE_CORRELATION_OPTIMIZATION OFF 
GO
ALTER DATABASE [aspnet-WebNhaHang-20230414061241] SET TRUSTWORTHY OFF 
GO
ALTER DATABASE [aspnet-WebNhaHang-20230414061241] SET ALLOW_SNAPSHOT_ISOLATION OFF 
GO
ALTER DATABASE [aspnet-WebNhaHang-20230414061241] SET PARAMETERIZATION SIMPLE 
GO
ALTER DATABASE [aspnet-WebNhaHang-20230414061241] SET READ_COMMITTED_SNAPSHOT ON 
GO
ALTER DATABASE [aspnet-WebNhaHang-20230414061241] SET HONOR_BROKER_PRIORITY OFF 
GO
ALTER DATABASE [aspnet-WebNhaHang-20230414061241] SET RECOVERY SIMPLE 
GO
ALTER DATABASE [aspnet-WebNhaHang-20230414061241] SET  MULTI_USER 
GO
ALTER DATABASE [aspnet-WebNhaHang-20230414061241] SET PAGE_VERIFY CHECKSUM  
GO
ALTER DATABASE [aspnet-WebNhaHang-20230414061241] SET DB_CHAINING OFF 
GO
ALTER DATABASE [aspnet-WebNhaHang-20230414061241] SET FILESTREAM( NON_TRANSACTED_ACCESS = OFF ) 
GO
ALTER DATABASE [aspnet-WebNhaHang-20230414061241] SET TARGET_RECOVERY_TIME = 60 SECONDS 
GO
ALTER DATABASE [aspnet-WebNhaHang-20230414061241] SET DELAYED_DURABILITY = DISABLED 
GO
ALTER DATABASE [aspnet-WebNhaHang-20230414061241] SET ACCELERATED_DATABASE_RECOVERY = OFF  
GO
ALTER DATABASE [aspnet-WebNhaHang-20230414061241] SET QUERY_STORE = ON
GO
ALTER DATABASE [aspnet-WebNhaHang-20230414061241] SET QUERY_STORE (OPERATION_MODE = READ_WRITE, CLEANUP_POLICY = (STALE_QUERY_THRESHOLD_DAYS = 30), DATA_FLUSH_INTERVAL_SECONDS = 900, INTERVAL_LENGTH_MINUTES = 60, MAX_STORAGE_SIZE_MB = 1000, QUERY_CAPTURE_MODE = AUTO, SIZE_BASED_CLEANUP_MODE = AUTO, MAX_PLANS_PER_QUERY = 200, WAIT_STATS_CAPTURE_MODE = ON)
GO
USE [aspnet-WebNhaHang-20230414061241]
GO
/****** Object:  Table [dbo].[__MigrationHistory]    Script Date: 28-Apr-23 12:33:37 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[__MigrationHistory](
	[MigrationId] [nvarchar](150) NOT NULL,
	[ContextKey] [nvarchar](300) NOT NULL,
	[Model] [varbinary](max) NOT NULL,
	[ProductVersion] [nvarchar](32) NOT NULL,
 CONSTRAINT [PK_dbo.__MigrationHistory] PRIMARY KEY CLUSTERED 
(
	[MigrationId] ASC,
	[ContextKey] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[AspNetRoles]    Script Date: 28-Apr-23 12:33:37 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[AspNetRoles](
	[Id] [nvarchar](128) NOT NULL,
	[Name] [nvarchar](256) NOT NULL,
 CONSTRAINT [PK_dbo.AspNetRoles] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[AspNetUserClaims]    Script Date: 28-Apr-23 12:33:37 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[AspNetUserClaims](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[UserId] [nvarchar](128) NOT NULL,
	[ClaimType] [nvarchar](max) NULL,
	[ClaimValue] [nvarchar](max) NULL,
 CONSTRAINT [PK_dbo.AspNetUserClaims] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[AspNetUserLogins]    Script Date: 28-Apr-23 12:33:37 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[AspNetUserLogins](
	[LoginProvider] [nvarchar](128) NOT NULL,
	[ProviderKey] [nvarchar](128) NOT NULL,
	[UserId] [nvarchar](128) NOT NULL,
 CONSTRAINT [PK_dbo.AspNetUserLogins] PRIMARY KEY CLUSTERED 
(
	[LoginProvider] ASC,
	[ProviderKey] ASC,
	[UserId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[AspNetUserRoles]    Script Date: 28-Apr-23 12:33:37 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[AspNetUserRoles](
	[UserId] [nvarchar](128) NOT NULL,
	[RoleId] [nvarchar](128) NOT NULL,
 CONSTRAINT [PK_dbo.AspNetUserRoles] PRIMARY KEY CLUSTERED 
(
	[UserId] ASC,
	[RoleId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[AspNetUsers]    Script Date: 28-Apr-23 12:33:37 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[AspNetUsers](
	[Id] [nvarchar](128) NOT NULL,
	[Email] [nvarchar](256) NULL,
	[EmailConfirmed] [bit] NOT NULL,
	[PasswordHash] [nvarchar](max) NULL,
	[SecurityStamp] [nvarchar](max) NULL,
	[PhoneNumber] [nvarchar](max) NULL,
	[PhoneNumberConfirmed] [bit] NOT NULL,
	[TwoFactorEnabled] [bit] NOT NULL,
	[LockoutEndDateUtc] [datetime] NULL,
	[LockoutEnabled] [bit] NOT NULL,
	[AccessFailedCount] [int] NOT NULL,
	[UserName] [nvarchar](256) NOT NULL,
 CONSTRAINT [PK_dbo.AspNetUsers] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Tb_Aplication]    Script Date: 28-Apr-23 12:33:37 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Tb_Aplication](
	[ApplicantID] [int] IDENTITY(1,1) NOT NULL,
	[FullName] [nvarchar](250) NULL,
	[Email] [nvarchar](250) NOT NULL,
	[Phone] [nvarchar](max) NULL,
	[Address] [nvarchar](500) NOT NULL,
	[Position] [nvarchar](max) NULL,
	[Experience] [nvarchar](max) NULL,
	[CreateBy] [nvarchar](max) NULL,
	[CreateDate] [datetime] NOT NULL,
	[ModifieDate] [datetime] NOT NULL,
	[ModifieBy] [nvarchar](max) NULL,
	[Code] [nvarchar](max) NOT NULL,
 CONSTRAINT [PK_dbo.Tb_Aplication] PRIMARY KEY CLUSTERED 
(
	[ApplicantID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Tb_Category]    Script Date: 28-Apr-23 12:33:37 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Tb_Category](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[Title] [nvarchar](150) NOT NULL,
	[Alias] [nvarchar](max) NULL,
	[Description] [nvarchar](max) NULL,
	[SeoTitle] [nvarchar](150) NULL,
	[SeoDescription] [nvarchar](250) NULL,
	[SeoKeywords] [nvarchar](150) NULL,
	[Position] [int] NOT NULL,
	[IsActive] [bit] NOT NULL,
	[CreateBy] [nvarchar](max) NULL,
	[CreateDate] [datetime] NOT NULL,
	[ModifieDate] [datetime] NOT NULL,
	[ModifieBy] [nvarchar](max) NULL,
 CONSTRAINT [PK_dbo.Tb_Category] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Tb_Contact]    Script Date: 28-Apr-23 12:33:37 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Tb_Contact](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](150) NOT NULL,
	[Email] [nvarchar](max) NULL,
	[Website] [nvarchar](max) NULL,
	[Message] [nvarchar](4000) NULL,
	[Isread] [int] NOT NULL,
 CONSTRAINT [PK_dbo.Tb_Contact] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Tb_Order]    Script Date: 28-Apr-23 12:33:37 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Tb_Order](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Code] [nvarchar](max) NOT NULL,
	[CustomerName] [nvarchar](max) NOT NULL,
	[Phone] [nvarchar](max) NOT NULL,
	[Address] [nvarchar](max) NOT NULL,
	[Mail] [nvarchar](max) NULL,
	[TotalAmount] [decimal](18, 2) NOT NULL,
	[Quantity] [int] NOT NULL,
	[TypePayment] [int] NOT NULL,
	[CreateBy] [nvarchar](max) NULL,
	[CreateDate] [datetime] NOT NULL,
	[ModifieDate] [datetime] NOT NULL,
	[ModifieBy] [nvarchar](max) NULL,
 CONSTRAINT [PK_dbo.Tb_Order] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Tb_OrderDetail]    Script Date: 28-Apr-23 12:33:37 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Tb_OrderDetail](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[OrderId] [int] NOT NULL,
	[ProductId] [int] NOT NULL,
	[Price] [decimal](18, 2) NOT NULL,
	[Quantity] [int] NOT NULL,
 CONSTRAINT [PK_dbo.Tb_OrderDetail] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Tb_ProductCategory]    Script Date: 28-Apr-23 12:33:37 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Tb_ProductCategory](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[Title] [nvarchar](150) NOT NULL,
	[Alias] [nvarchar](150) NOT NULL,
	[Description] [nvarchar](max) NULL,
	[Icon] [nvarchar](250) NULL,
	[IsActive] [bit] NOT NULL,
	[SeoTitle] [nvarchar](250) NULL,
	[SeoDescription] [nvarchar](500) NULL,
	[SeoKeywords] [nvarchar](250) NULL,
	[CreateBy] [nvarchar](max) NULL,
	[CreateDate] [datetime] NOT NULL,
	[ModifieDate] [datetime] NOT NULL,
	[ModifieBy] [nvarchar](max) NULL,
 CONSTRAINT [PK_dbo.Tb_ProductCategory] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Tb_ProductImage]    Script Date: 28-Apr-23 12:33:37 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Tb_ProductImage](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[ProductId] [int] NOT NULL,
	[Image] [nvarchar](max) NULL,
	[IsDefault] [bit] NOT NULL,
 CONSTRAINT [PK_dbo.Tb_ProductImage] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Tb_Productt]    Script Date: 28-Apr-23 12:33:37 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Tb_Productt](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[Title] [nvarchar](250) NOT NULL,
	[Alias] [nvarchar](250) NULL,
	[ProductCode] [nvarchar](50) NULL,
	[ProductCategoryID] [int] NOT NULL,
	[Description] [nvarchar](max) NULL,
	[Detail] [nvarchar](max) NULL,
	[Image] [nvarchar](250) NULL,
	[Price] [decimal](18, 2) NOT NULL,
	[PriceSale] [decimal](18, 2) NULL,
	[IsHome] [bit] NOT NULL,
	[IsSale] [bit] NOT NULL,
	[IsActive] [bit] NOT NULL,
	[IsHot] [bit] NOT NULL,
	[IsFeature] [bit] NOT NULL,
	[Quantity] [int] NOT NULL,
	[SeoTitle] [nvarchar](250) NULL,
	[SeoDescription] [nvarchar](500) NULL,
	[SeoKeywords] [nvarchar](250) NULL,
	[CreateBy] [nvarchar](max) NULL,
	[CreateDate] [datetime] NOT NULL,
	[ModifieDate] [datetime] NOT NULL,
	[ModifieBy] [nvarchar](max) NULL,
 CONSTRAINT [PK_dbo.Tb_Productt] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Tb_Reservation]    Script Date: 28-Apr-23 12:33:37 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Tb_Reservation](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](max) NULL,
	[Email] [nvarchar](100) NULL,
	[Room] [nvarchar](250) NULL,
	[NumberOfPeople] [nvarchar](max) NULL,
	[DateTime] [datetime] NOT NULL,
	[Phone] [nvarchar](max) NULL,
	[CreateBy] [nvarchar](max) NULL,
	[CreateDate] [datetime] NOT NULL,
	[ModifieDate] [datetime] NOT NULL,
	[ModifieBy] [nvarchar](max) NULL,
	[Address] [nvarchar](max) NULL,
 CONSTRAINT [PK_dbo.Tb_Reservation] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
SET ANSI_PADDING ON
GO
/****** Object:  Index [RoleNameIndex]    Script Date: 28-Apr-23 12:33:37 AM ******/
CREATE UNIQUE NONCLUSTERED INDEX [RoleNameIndex] ON [dbo].[AspNetRoles]
(
	[Name] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
SET ANSI_PADDING ON
GO
/****** Object:  Index [IX_UserId]    Script Date: 28-Apr-23 12:33:37 AM ******/
CREATE NONCLUSTERED INDEX [IX_UserId] ON [dbo].[AspNetUserClaims]
(
	[UserId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
SET ANSI_PADDING ON
GO
/****** Object:  Index [IX_UserId]    Script Date: 28-Apr-23 12:33:37 AM ******/
CREATE NONCLUSTERED INDEX [IX_UserId] ON [dbo].[AspNetUserLogins]
(
	[UserId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
SET ANSI_PADDING ON
GO
/****** Object:  Index [IX_RoleId]    Script Date: 28-Apr-23 12:33:37 AM ******/
CREATE NONCLUSTERED INDEX [IX_RoleId] ON [dbo].[AspNetUserRoles]
(
	[RoleId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
SET ANSI_PADDING ON
GO
/****** Object:  Index [IX_UserId]    Script Date: 28-Apr-23 12:33:37 AM ******/
CREATE NONCLUSTERED INDEX [IX_UserId] ON [dbo].[AspNetUserRoles]
(
	[UserId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
SET ANSI_PADDING ON
GO
/****** Object:  Index [UserNameIndex]    Script Date: 28-Apr-23 12:33:37 AM ******/
CREATE UNIQUE NONCLUSTERED INDEX [UserNameIndex] ON [dbo].[AspNetUsers]
(
	[UserName] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
/****** Object:  Index [IX_OrderId]    Script Date: 28-Apr-23 12:33:37 AM ******/
CREATE NONCLUSTERED INDEX [IX_OrderId] ON [dbo].[Tb_OrderDetail]
(
	[OrderId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
/****** Object:  Index [IX_ProductId]    Script Date: 28-Apr-23 12:33:37 AM ******/
CREATE NONCLUSTERED INDEX [IX_ProductId] ON [dbo].[Tb_OrderDetail]
(
	[ProductId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
/****** Object:  Index [IX_ProductId]    Script Date: 28-Apr-23 12:33:37 AM ******/
CREATE NONCLUSTERED INDEX [IX_ProductId] ON [dbo].[Tb_ProductImage]
(
	[ProductId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
/****** Object:  Index [IX_ProductCategoryID]    Script Date: 28-Apr-23 12:33:37 AM ******/
CREATE NONCLUSTERED INDEX [IX_ProductCategoryID] ON [dbo].[Tb_Productt]
(
	[ProductCategoryID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
ALTER TABLE [dbo].[Tb_Aplication] ADD  DEFAULT ('') FOR [Code]
GO
ALTER TABLE [dbo].[Tb_Reservation] ADD  DEFAULT ('1900-01-01T00:00:00.000') FOR [DateTime]
GO
ALTER TABLE [dbo].[Tb_Reservation] ADD  DEFAULT ('1900-01-01T00:00:00.000') FOR [CreateDate]
GO
ALTER TABLE [dbo].[Tb_Reservation] ADD  DEFAULT ('1900-01-01T00:00:00.000') FOR [ModifieDate]
GO
ALTER TABLE [dbo].[AspNetUserClaims]  WITH CHECK ADD  CONSTRAINT [FK_dbo.AspNetUserClaims_dbo.AspNetUsers_UserId] FOREIGN KEY([UserId])
REFERENCES [dbo].[AspNetUsers] ([Id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[AspNetUserClaims] CHECK CONSTRAINT [FK_dbo.AspNetUserClaims_dbo.AspNetUsers_UserId]
GO
ALTER TABLE [dbo].[AspNetUserLogins]  WITH CHECK ADD  CONSTRAINT [FK_dbo.AspNetUserLogins_dbo.AspNetUsers_UserId] FOREIGN KEY([UserId])
REFERENCES [dbo].[AspNetUsers] ([Id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[AspNetUserLogins] CHECK CONSTRAINT [FK_dbo.AspNetUserLogins_dbo.AspNetUsers_UserId]
GO
ALTER TABLE [dbo].[AspNetUserRoles]  WITH CHECK ADD  CONSTRAINT [FK_dbo.AspNetUserRoles_dbo.AspNetRoles_RoleId] FOREIGN KEY([RoleId])
REFERENCES [dbo].[AspNetRoles] ([Id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[AspNetUserRoles] CHECK CONSTRAINT [FK_dbo.AspNetUserRoles_dbo.AspNetRoles_RoleId]
GO
ALTER TABLE [dbo].[AspNetUserRoles]  WITH CHECK ADD  CONSTRAINT [FK_dbo.AspNetUserRoles_dbo.AspNetUsers_UserId] FOREIGN KEY([UserId])
REFERENCES [dbo].[AspNetUsers] ([Id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[AspNetUserRoles] CHECK CONSTRAINT [FK_dbo.AspNetUserRoles_dbo.AspNetUsers_UserId]
GO
ALTER TABLE [dbo].[Tb_OrderDetail]  WITH CHECK ADD  CONSTRAINT [FK_dbo.Tb_OrderDetail_dbo.Tb_Order_OrderId] FOREIGN KEY([OrderId])
REFERENCES [dbo].[Tb_Order] ([Id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[Tb_OrderDetail] CHECK CONSTRAINT [FK_dbo.Tb_OrderDetail_dbo.Tb_Order_OrderId]
GO
ALTER TABLE [dbo].[Tb_OrderDetail]  WITH CHECK ADD  CONSTRAINT [FK_dbo.Tb_OrderDetail_dbo.Tb_Productt_ProductId] FOREIGN KEY([ProductId])
REFERENCES [dbo].[Tb_Productt] ([id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[Tb_OrderDetail] CHECK CONSTRAINT [FK_dbo.Tb_OrderDetail_dbo.Tb_Productt_ProductId]
GO
ALTER TABLE [dbo].[Tb_ProductImage]  WITH CHECK ADD  CONSTRAINT [FK_dbo.Tb_ProductImage_dbo.Tb_Productt_ProductId] FOREIGN KEY([ProductId])
REFERENCES [dbo].[Tb_Productt] ([id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[Tb_ProductImage] CHECK CONSTRAINT [FK_dbo.Tb_ProductImage_dbo.Tb_Productt_ProductId]
GO
ALTER TABLE [dbo].[Tb_Productt]  WITH CHECK ADD  CONSTRAINT [FK_dbo.Tb_Productt_dbo.Tb_ProductCategory_ProductCategoryID] FOREIGN KEY([ProductCategoryID])
REFERENCES [dbo].[Tb_ProductCategory] ([id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[Tb_Productt] CHECK CONSTRAINT [FK_dbo.Tb_Productt_dbo.Tb_ProductCategory_ProductCategoryID]
GO
USE [master]
GO
ALTER DATABASE [aspnet-WebNhaHang-20230414061241] SET  READ_WRITE 
GO
