USE [master]
GO
/****** Object:  Database [QLBHDienThoai]    Script Date: 6/7/2019 6:18:24 PM ******/
CREATE DATABASE [QLBHDienThoai]
 CONTAINMENT = NONE
 ON  PRIMARY 
( NAME = N'QLBHDienThoai', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL14.MSSQLSERVER\MSSQL\DATA\QLBHDienThoai.mdf' , SIZE = 8192KB , MAXSIZE = UNLIMITED, FILEGROWTH = 65536KB )
 LOG ON 
( NAME = N'QLBHDienThoai_log', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL14.MSSQLSERVER\MSSQL\DATA\QLBHDienThoai_log.ldf' , SIZE = 8192KB , MAXSIZE = 2048GB , FILEGROWTH = 65536KB )
GO
ALTER DATABASE [QLBHDienThoai] SET COMPATIBILITY_LEVEL = 140
GO
IF (1 = FULLTEXTSERVICEPROPERTY('IsFullTextInstalled'))
begin
EXEC [QLBHDienThoai].[dbo].[sp_fulltext_database] @action = 'enable'
end
GO
ALTER DATABASE [QLBHDienThoai] SET ANSI_NULL_DEFAULT OFF 
GO
ALTER DATABASE [QLBHDienThoai] SET ANSI_NULLS OFF 
GO
ALTER DATABASE [QLBHDienThoai] SET ANSI_PADDING OFF 
GO
ALTER DATABASE [QLBHDienThoai] SET ANSI_WARNINGS OFF 
GO
ALTER DATABASE [QLBHDienThoai] SET ARITHABORT OFF 
GO
ALTER DATABASE [QLBHDienThoai] SET AUTO_CLOSE OFF 
GO
ALTER DATABASE [QLBHDienThoai] SET AUTO_SHRINK OFF 
GO
ALTER DATABASE [QLBHDienThoai] SET AUTO_UPDATE_STATISTICS ON 
GO
ALTER DATABASE [QLBHDienThoai] SET CURSOR_CLOSE_ON_COMMIT OFF 
GO
ALTER DATABASE [QLBHDienThoai] SET CURSOR_DEFAULT  GLOBAL 
GO
ALTER DATABASE [QLBHDienThoai] SET CONCAT_NULL_YIELDS_NULL OFF 
GO
ALTER DATABASE [QLBHDienThoai] SET NUMERIC_ROUNDABORT OFF 
GO
ALTER DATABASE [QLBHDienThoai] SET QUOTED_IDENTIFIER OFF 
GO
ALTER DATABASE [QLBHDienThoai] SET RECURSIVE_TRIGGERS OFF 
GO
ALTER DATABASE [QLBHDienThoai] SET  ENABLE_BROKER 
GO
ALTER DATABASE [QLBHDienThoai] SET AUTO_UPDATE_STATISTICS_ASYNC OFF 
GO
ALTER DATABASE [QLBHDienThoai] SET DATE_CORRELATION_OPTIMIZATION OFF 
GO
ALTER DATABASE [QLBHDienThoai] SET TRUSTWORTHY OFF 
GO
ALTER DATABASE [QLBHDienThoai] SET ALLOW_SNAPSHOT_ISOLATION OFF 
GO
ALTER DATABASE [QLBHDienThoai] SET PARAMETERIZATION SIMPLE 
GO
ALTER DATABASE [QLBHDienThoai] SET READ_COMMITTED_SNAPSHOT OFF 
GO
ALTER DATABASE [QLBHDienThoai] SET HONOR_BROKER_PRIORITY OFF 
GO
ALTER DATABASE [QLBHDienThoai] SET RECOVERY FULL 
GO
ALTER DATABASE [QLBHDienThoai] SET  MULTI_USER 
GO
ALTER DATABASE [QLBHDienThoai] SET PAGE_VERIFY CHECKSUM  
GO
ALTER DATABASE [QLBHDienThoai] SET DB_CHAINING OFF 
GO
ALTER DATABASE [QLBHDienThoai] SET FILESTREAM( NON_TRANSACTED_ACCESS = OFF ) 
GO
ALTER DATABASE [QLBHDienThoai] SET TARGET_RECOVERY_TIME = 60 SECONDS 
GO
ALTER DATABASE [QLBHDienThoai] SET DELAYED_DURABILITY = DISABLED 
GO
EXEC sys.sp_db_vardecimal_storage_format N'QLBHDienThoai', N'ON'
GO
ALTER DATABASE [QLBHDienThoai] SET QUERY_STORE = OFF
GO
USE [QLBHDienThoai]
GO
/****** Object:  Table [dbo].[BillOfSale]    Script Date: 6/7/2019 6:18:24 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[BillOfSale](
	[BillID] [int] NOT NULL,
	[BuyDate] [date] NULL,
	[Status] [int] NULL,
	[TotalPrice] [decimal](18, 0) NULL,
	[EmployeeID] [int] NULL,
	[CartID] [int] NULL,
	[Delivery] [varchar](30) NULL,
 CONSTRAINT [PK_BillOfSale] PRIMARY KEY CLUSTERED 
(
	[BillID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[BillSaleDetail]    Script Date: 6/7/2019 6:18:24 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[BillSaleDetail](
	[BillDetailID] [int] NOT NULL,
	[Amount] [int] NULL,
	[PriceProduct] [decimal](18, 0) NULL,
	[BillID] [int] NULL,
	[ProductID] [int] NULL,
 CONSTRAINT [PK_BillSaleDetail] PRIMARY KEY CLUSTERED 
(
	[BillDetailID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Cart]    Script Date: 6/7/2019 6:18:24 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Cart](
	[CartID] [int] NOT NULL,
	[TotaiPrice] [decimal](18, 0) NULL,
	[VAT] [int] NULL,
	[CustomerID] [int] NULL,
	[PromotionID] [int] NULL,
	[Status] [int] NULL,
 CONSTRAINT [PK_Cart] PRIMARY KEY CLUSTERED 
(
	[CartID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[CartDetail]    Script Date: 6/7/2019 6:18:25 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[CartDetail](
	[CartDetailID] [int] NOT NULL,
	[Amount] [int] NULL,
	[Date] [date] NULL,
	[ProductID] [int] NULL,
	[CartID] [int] NULL,
 CONSTRAINT [PK_CartDetail] PRIMARY KEY CLUSTERED 
(
	[CartDetailID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Comment]    Script Date: 6/7/2019 6:18:25 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Comment](
	[CommentID] [int] NOT NULL,
	[Content] [nvarchar](200) NULL,
	[Date] [date] NULL,
	[CustomerID] [int] NULL,
	[ProductID] [int] NULL,
 CONSTRAINT [PK_Comment] PRIMARY KEY CLUSTERED 
(
	[CommentID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Customer]    Script Date: 6/7/2019 6:18:25 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Customer](
	[CustomerID] [int] IDENTITY(1,1) NOT NULL,
	[Fullname] [nvarchar](50) NULL,
	[Address] [nvarchar](1000) NULL,
	[Phone] [varchar](15) NULL,
	[Birthday] [date] NULL,
	[Gender] [bit] NOT NULL,
 CONSTRAINT [PK_Customer] PRIMARY KEY CLUSTERED 
(
	[CustomerID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Employee]    Script Date: 6/7/2019 6:18:25 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Employee](
	[EmployeeID] [int] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](50) NULL,
	[Birthday] [date] NULL,
	[Phone] [varchar](15) NULL,
	[RoleID] [int] NULL,
	[Type] [nvarchar](50) NULL,
 CONSTRAINT [PK_Employee] PRIMARY KEY CLUSTERED 
(
	[EmployeeID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[OrderInvoice]    Script Date: 6/7/2019 6:18:25 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[OrderInvoice](
	[OrderInvoiceID] [int] NOT NULL,
	[OrderDate] [date] NULL,
	[SupplierID] [int] NULL,
	[EmployeeID] [int] NULL,
 CONSTRAINT [PK_OrderInvoice] PRIMARY KEY CLUSTERED 
(
	[OrderInvoiceID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[OrderInvoiceDetail]    Script Date: 6/7/2019 6:18:25 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[OrderInvoiceDetail](
	[InvoiceDetailID] [int] NOT NULL,
	[ProductID] [int] NULL,
	[Amount] [nchar](10) NULL,
	[PriceProduct] [decimal](18, 0) NULL,
	[OrderInvoiceID] [int] NULL,
 CONSTRAINT [PK_OrderInvoiceDetail] PRIMARY KEY CLUSTERED 
(
	[InvoiceDetailID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Pay]    Script Date: 6/7/2019 6:18:25 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Pay](
	[PayID] [int] NOT NULL,
	[Date] [date] NULL,
	[EmployeeID] [int] NULL,
	[TotalPrice] [decimal](18, 0) NULL,
 CONSTRAINT [PK_Pay] PRIMARY KEY CLUSTERED 
(
	[PayID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[PayDetail]    Script Date: 6/7/2019 6:18:25 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[PayDetail](
	[PayDetailID] [int] NOT NULL,
	[PayID] [int] NULL,
	[Amount] [int] NULL,
	[Price] [decimal](18, 0) NULL,
	[Describe] [nvarchar](200) NULL,
	[Receiver] [nvarchar](50) NULL,
 CONSTRAINT [PK_PayDetail] PRIMARY KEY CLUSTERED 
(
	[PayDetailID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Product]    Script Date: 6/7/2019 6:18:25 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Product](
	[ProductID] [int] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](50) NULL,
	[Amount] [int] NULL,
	[PriceProduct] [decimal](18, 0) NULL,
	[Images] [varchar](100) NULL,
	[SupplierID] [int] NULL,
	[TypeProductID] [int] NULL,
 CONSTRAINT [PK_Product] PRIMARY KEY CLUSTERED 
(
	[ProductID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Promotion]    Script Date: 6/7/2019 6:18:25 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Promotion](
	[PromotionID] [int] NOT NULL,
	[PromotionName] [nvarchar](50) NULL,
	[TypePromotion] [nvarchar](50) NULL,
	[TypeProductID] [int] NULL,
	[ProductID] [int] NULL,
	[StartTime] [date] NULL,
	[EndTime] [date] NULL,
 CONSTRAINT [PK_Promotion] PRIMARY KEY CLUSTERED 
(
	[PromotionID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Receipt]    Script Date: 6/7/2019 6:18:25 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Receipt](
	[ReceiptID] [int] NOT NULL,
	[Date] [date] NULL,
	[EmployeeID] [int] NULL,
	[CustomerID] [int] NULL,
	[BillID] [int] NULL,
 CONSTRAINT [PK_Receipt] PRIMARY KEY CLUSTERED 
(
	[ReceiptID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[ReceiptDetail]    Script Date: 6/7/2019 6:18:25 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[ReceiptDetail](
	[ReceiptDetailID] [int] NOT NULL,
	[ReceiptID] [int] NULL,
	[ProductID] [int] NULL,
	[Amount] [int] NULL,
	[Describe] [nvarchar](200) NULL,
 CONSTRAINT [PK_ReceiptDetail] PRIMARY KEY CLUSTERED 
(
	[ReceiptDetailID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Role]    Script Date: 6/7/2019 6:18:25 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Role](
	[RoleID] [int] NOT NULL,
	[Name] [nvarchar](50) NULL,
 CONSTRAINT [PK_Role] PRIMARY KEY CLUSTERED 
(
	[RoleID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Supplier]    Script Date: 6/7/2019 6:18:25 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Supplier](
	[SupplierID] [int] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](50) NULL,
	[Phone] [int] NULL,
	[Address] [nvarchar](100) NULL,
 CONSTRAINT [PK_Supplier] PRIMARY KEY CLUSTERED 
(
	[SupplierID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[TypeProduct]    Script Date: 6/7/2019 6:18:25 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[TypeProduct](
	[TypeProductID] [int] IDENTITY(1,1) NOT NULL,
	[TypeProductName] [nvarchar](50) NULL,
 CONSTRAINT [PK_TypeProduct] PRIMARY KEY CLUSTERED 
(
	[TypeProductID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[User]    Script Date: 6/7/2019 6:18:25 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[User](
	[Username] [varchar](100) NOT NULL,
	[Password] [varchar](100) NOT NULL,
	[EmployeeId] [int] NULL,
	[CustormerId] [int] NULL,
	[Status] [bit] NOT NULL,
	[RoleId] [int] NOT NULL,
	[FullName] [nvarchar](250) NOT NULL,
	[Email] [varchar](250) NOT NULL,
	[EmailConfirmed] [bit] NOT NULL,
	[Code] [nvarchar](250) NULL,
 CONSTRAINT [PK_tblUser] PRIMARY KEY CLUSTERED 
(
	[Username] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET IDENTITY_INSERT [dbo].[Customer] ON 

INSERT [dbo].[Customer] ([CustomerID], [Fullname], [Address], [Phone], [Birthday], [Gender]) VALUES (1, N'Nguyễn Thu Hà', N'Hà Nội', N'396201695', CAST(N'2019-06-08' AS Date), 0)
SET IDENTITY_INSERT [dbo].[Customer] OFF
SET IDENTITY_INSERT [dbo].[Employee] ON 

INSERT [dbo].[Employee] ([EmployeeID], [Name], [Birthday], [Phone], [RoleID], [Type]) VALUES (9, N'Lê Anh Đức', CAST(N'2019-06-08' AS Date), N'91612325', NULL, NULL)
SET IDENTITY_INSERT [dbo].[Employee] OFF
SET IDENTITY_INSERT [dbo].[Product] ON 

INSERT [dbo].[Product] ([ProductID], [Name], [Amount], [PriceProduct], [Images], [SupplierID], [TypeProductID]) VALUES (4, N'Iphone 7 32G màu trắng', 6, CAST(10000000 AS Decimal(18, 0)), N'/FileUploads/images/item-02.jpg', 1, 1)
INSERT [dbo].[Product] ([ProductID], [Name], [Amount], [PriceProduct], [Images], [SupplierID], [TypeProductID]) VALUES (5, N'Iphone 7 16g màu đỏ', 5, CAST(70000 AS Decimal(18, 0)), N'/FileUploads/images/item-03.jpg', 1, 1)
INSERT [dbo].[Product] ([ProductID], [Name], [Amount], [PriceProduct], [Images], [SupplierID], [TypeProductID]) VALUES (6, N'sam sung j7', 3, CAST(20000 AS Decimal(18, 0)), N'/FileUploads/images/item-04.jpg', 1, 2)
INSERT [dbo].[Product] ([ProductID], [Name], [Amount], [PriceProduct], [Images], [SupplierID], [TypeProductID]) VALUES (7, N'sam sung j3', 3, CAST(20000 AS Decimal(18, 0)), N'/FileUploads/images/opa37.jpg', 1, 2)
INSERT [dbo].[Product] ([ProductID], [Name], [Amount], [PriceProduct], [Images], [SupplierID], [TypeProductID]) VALUES (8, N'iphone', 3, CAST(20000 AS Decimal(18, 0)), N'/FileUploads/images/ipx.jpg', 1, 1)
INSERT [dbo].[Product] ([ProductID], [Name], [Amount], [PriceProduct], [Images], [SupplierID], [TypeProductID]) VALUES (9, N'Samsung galaxy s6 plus', 10, CAST(6500000 AS Decimal(18, 0)), N'/FileUploads/images/sss8.jpg', 1, NULL)
INSERT [dbo].[Product] ([ProductID], [Name], [Amount], [PriceProduct], [Images], [SupplierID], [TypeProductID]) VALUES (10, N'Samsung galaxy s7 plus', 10, CAST(8500000 AS Decimal(18, 0)), N'/FileUploads/images/hwy7.jpg', 1, NULL)
INSERT [dbo].[Product] ([ProductID], [Name], [Amount], [PriceProduct], [Images], [SupplierID], [TypeProductID]) VALUES (11, N'Redmi note 4', 10, CAST(4300000 AS Decimal(18, 0)), N'/FileUploads/images/xiaomi-redmi-5a-xam-1-400x460.png', 1, 7)
INSERT [dbo].[Product] ([ProductID], [Name], [Amount], [PriceProduct], [Images], [SupplierID], [TypeProductID]) VALUES (12, N'Xiaomi redmi 5a', 10, CAST(10000000 AS Decimal(18, 0)), N'/FileUploads/images/xiaomi-redmi-5a-xam-1-400x460.png', 1, 3)
INSERT [dbo].[Product] ([ProductID], [Name], [Amount], [PriceProduct], [Images], [SupplierID], [TypeProductID]) VALUES (13, N'IPhone8', 20, CAST(16000000 AS Decimal(18, 0)), N'/FileUploads/images/ip8.jpg', 1, 1)
INSERT [dbo].[Product] ([ProductID], [Name], [Amount], [PriceProduct], [Images], [SupplierID], [TypeProductID]) VALUES (17, N'SamSung J2', 3, CAST(2300000 AS Decimal(18, 0)), N'/FileUploads/images/ssj2.jpg', 1, 2)
INSERT [dbo].[Product] ([ProductID], [Name], [Amount], [PriceProduct], [Images], [SupplierID], [TypeProductID]) VALUES (19, N'SamSungM50', 8, CAST(8000000 AS Decimal(18, 0)), N'/FileUploads/images/ssm50.jpg', 1, 2)
INSERT [dbo].[Product] ([ProductID], [Name], [Amount], [PriceProduct], [Images], [SupplierID], [TypeProductID]) VALUES (20, N'SamSung Note7', 9, CAST(12000000 AS Decimal(18, 0)), N'/FileUploads/images/ssn7.jpg', 1, 2)
INSERT [dbo].[Product] ([ProductID], [Name], [Amount], [PriceProduct], [Images], [SupplierID], [TypeProductID]) VALUES (21, N'SamSung S8', 5, CAST(15000000 AS Decimal(18, 0)), N'/FileUploads/images/sss8.jpg', 1, 2)
SET IDENTITY_INSERT [dbo].[Product] OFF
INSERT [dbo].[Role] ([RoleID], [Name]) VALUES (1, N'Admin')
INSERT [dbo].[Role] ([RoleID], [Name]) VALUES (2, N'Employee')
INSERT [dbo].[Role] ([RoleID], [Name]) VALUES (3, N'Customer')
INSERT [dbo].[Role] ([RoleID], [Name]) VALUES (4, N'User')
SET IDENTITY_INSERT [dbo].[Supplier] ON 

INSERT [dbo].[Supplier] ([SupplierID], [Name], [Phone], [Address]) VALUES (1, N'Bé Gấu', 12345678, N'hà')
SET IDENTITY_INSERT [dbo].[Supplier] OFF
SET IDENTITY_INSERT [dbo].[TypeProduct] ON 

INSERT [dbo].[TypeProduct] ([TypeProductID], [TypeProductName]) VALUES (1, N'Iphone')
INSERT [dbo].[TypeProduct] ([TypeProductID], [TypeProductName]) VALUES (2, N'Sam sung')
INSERT [dbo].[TypeProduct] ([TypeProductID], [TypeProductName]) VALUES (3, N'Xiaomi')
INSERT [dbo].[TypeProduct] ([TypeProductID], [TypeProductName]) VALUES (7, N'Redmi')
SET IDENTITY_INSERT [dbo].[TypeProduct] OFF
INSERT [dbo].[User] ([Username], [Password], [EmployeeId], [CustormerId], [Status], [RoleId], [FullName], [Email], [EmailConfirmed], [Code]) VALUES (N'kutyboy98', N'kutyboy9898', NULL, NULL, 1, 1, N'Trần Trọng Long', N'darkjker@gmail.com', 1, N'19008198')
INSERT [dbo].[User] ([Username], [Password], [EmployeeId], [CustormerId], [Status], [RoleId], [FullName], [Email], [EmailConfirmed], [Code]) VALUES (N'phucnd', N'123', NULL, NULL, 1, 3, N'Đình Phúc', N'phund@gmmail.com', 1, N'19008198')
ALTER TABLE [dbo].[User] ADD  CONSTRAINT [DF_tblUser_password]  DEFAULT ((123456)) FOR [Password]
GO
ALTER TABLE [dbo].[User] ADD  DEFAULT ((1)) FOR [Status]
GO
ALTER TABLE [dbo].[BillOfSale]  WITH CHECK ADD  CONSTRAINT [FK_BillOfSale_Cart] FOREIGN KEY([CartID])
REFERENCES [dbo].[Cart] ([CartID])
GO
ALTER TABLE [dbo].[BillOfSale] CHECK CONSTRAINT [FK_BillOfSale_Cart]
GO
ALTER TABLE [dbo].[BillOfSale]  WITH CHECK ADD  CONSTRAINT [FK_BillOfSale_Employee] FOREIGN KEY([EmployeeID])
REFERENCES [dbo].[Employee] ([EmployeeID])
GO
ALTER TABLE [dbo].[BillOfSale] CHECK CONSTRAINT [FK_BillOfSale_Employee]
GO
ALTER TABLE [dbo].[BillSaleDetail]  WITH CHECK ADD  CONSTRAINT [FK_BillSaleDetail_BillOfSale] FOREIGN KEY([BillID])
REFERENCES [dbo].[BillOfSale] ([BillID])
GO
ALTER TABLE [dbo].[BillSaleDetail] CHECK CONSTRAINT [FK_BillSaleDetail_BillOfSale]
GO
ALTER TABLE [dbo].[BillSaleDetail]  WITH CHECK ADD  CONSTRAINT [FK_BillSaleDetail_Product] FOREIGN KEY([ProductID])
REFERENCES [dbo].[Product] ([ProductID])
GO
ALTER TABLE [dbo].[BillSaleDetail] CHECK CONSTRAINT [FK_BillSaleDetail_Product]
GO
ALTER TABLE [dbo].[Cart]  WITH CHECK ADD  CONSTRAINT [FK_Cart_Customer] FOREIGN KEY([CustomerID])
REFERENCES [dbo].[Customer] ([CustomerID])
GO
ALTER TABLE [dbo].[Cart] CHECK CONSTRAINT [FK_Cart_Customer]
GO
ALTER TABLE [dbo].[Cart]  WITH CHECK ADD  CONSTRAINT [FK_Cart_Promotion] FOREIGN KEY([PromotionID])
REFERENCES [dbo].[Promotion] ([PromotionID])
GO
ALTER TABLE [dbo].[Cart] CHECK CONSTRAINT [FK_Cart_Promotion]
GO
ALTER TABLE [dbo].[CartDetail]  WITH CHECK ADD  CONSTRAINT [FK_CartDetail_Cart] FOREIGN KEY([CartID])
REFERENCES [dbo].[Cart] ([CartID])
GO
ALTER TABLE [dbo].[CartDetail] CHECK CONSTRAINT [FK_CartDetail_Cart]
GO
ALTER TABLE [dbo].[Comment]  WITH CHECK ADD  CONSTRAINT [FK_Comment_Customer] FOREIGN KEY([CustomerID])
REFERENCES [dbo].[Customer] ([CustomerID])
GO
ALTER TABLE [dbo].[Comment] CHECK CONSTRAINT [FK_Comment_Customer]
GO
ALTER TABLE [dbo].[Comment]  WITH CHECK ADD  CONSTRAINT [FK_Comment_Product] FOREIGN KEY([ProductID])
REFERENCES [dbo].[Product] ([ProductID])
GO
ALTER TABLE [dbo].[Comment] CHECK CONSTRAINT [FK_Comment_Product]
GO
ALTER TABLE [dbo].[Employee]  WITH CHECK ADD  CONSTRAINT [FK_Employee_Role] FOREIGN KEY([RoleID])
REFERENCES [dbo].[Role] ([RoleID])
GO
ALTER TABLE [dbo].[Employee] CHECK CONSTRAINT [FK_Employee_Role]
GO
ALTER TABLE [dbo].[OrderInvoice]  WITH CHECK ADD  CONSTRAINT [FK_OrderInvoice_Employee] FOREIGN KEY([EmployeeID])
REFERENCES [dbo].[Employee] ([EmployeeID])
GO
ALTER TABLE [dbo].[OrderInvoice] CHECK CONSTRAINT [FK_OrderInvoice_Employee]
GO
ALTER TABLE [dbo].[OrderInvoice]  WITH CHECK ADD  CONSTRAINT [FK_OrderInvoice_Supplier] FOREIGN KEY([SupplierID])
REFERENCES [dbo].[Supplier] ([SupplierID])
GO
ALTER TABLE [dbo].[OrderInvoice] CHECK CONSTRAINT [FK_OrderInvoice_Supplier]
GO
ALTER TABLE [dbo].[OrderInvoiceDetail]  WITH CHECK ADD  CONSTRAINT [FK_OrderInvoiceDetail_OrderInvoice] FOREIGN KEY([OrderInvoiceID])
REFERENCES [dbo].[OrderInvoice] ([OrderInvoiceID])
GO
ALTER TABLE [dbo].[OrderInvoiceDetail] CHECK CONSTRAINT [FK_OrderInvoiceDetail_OrderInvoice]
GO
ALTER TABLE [dbo].[OrderInvoiceDetail]  WITH CHECK ADD  CONSTRAINT [FK_OrderInvoiceDetail_Product] FOREIGN KEY([ProductID])
REFERENCES [dbo].[Product] ([ProductID])
GO
ALTER TABLE [dbo].[OrderInvoiceDetail] CHECK CONSTRAINT [FK_OrderInvoiceDetail_Product]
GO
ALTER TABLE [dbo].[Pay]  WITH CHECK ADD  CONSTRAINT [FK_Pay_Employee] FOREIGN KEY([EmployeeID])
REFERENCES [dbo].[Employee] ([EmployeeID])
GO
ALTER TABLE [dbo].[Pay] CHECK CONSTRAINT [FK_Pay_Employee]
GO
ALTER TABLE [dbo].[PayDetail]  WITH CHECK ADD  CONSTRAINT [FK_PayDetail_Pay] FOREIGN KEY([PayID])
REFERENCES [dbo].[Pay] ([PayID])
GO
ALTER TABLE [dbo].[PayDetail] CHECK CONSTRAINT [FK_PayDetail_Pay]
GO
ALTER TABLE [dbo].[Product]  WITH CHECK ADD  CONSTRAINT [FK_Product_Supplier] FOREIGN KEY([SupplierID])
REFERENCES [dbo].[Supplier] ([SupplierID])
GO
ALTER TABLE [dbo].[Product] CHECK CONSTRAINT [FK_Product_Supplier]
GO
ALTER TABLE [dbo].[Product]  WITH CHECK ADD  CONSTRAINT [FK_Product_TypeProduct] FOREIGN KEY([TypeProductID])
REFERENCES [dbo].[TypeProduct] ([TypeProductID])
GO
ALTER TABLE [dbo].[Product] CHECK CONSTRAINT [FK_Product_TypeProduct]
GO
ALTER TABLE [dbo].[Receipt]  WITH CHECK ADD  CONSTRAINT [FK_Receipt_BillOfSale] FOREIGN KEY([BillID])
REFERENCES [dbo].[BillOfSale] ([BillID])
GO
ALTER TABLE [dbo].[Receipt] CHECK CONSTRAINT [FK_Receipt_BillOfSale]
GO
ALTER TABLE [dbo].[Receipt]  WITH CHECK ADD  CONSTRAINT [FK_Receipt_Employee] FOREIGN KEY([EmployeeID])
REFERENCES [dbo].[Employee] ([EmployeeID])
GO
ALTER TABLE [dbo].[Receipt] CHECK CONSTRAINT [FK_Receipt_Employee]
GO
ALTER TABLE [dbo].[ReceiptDetail]  WITH CHECK ADD  CONSTRAINT [FK_ReceiptDetail_Receipt] FOREIGN KEY([ReceiptID])
REFERENCES [dbo].[Receipt] ([ReceiptID])
GO
ALTER TABLE [dbo].[ReceiptDetail] CHECK CONSTRAINT [FK_ReceiptDetail_Receipt]
GO
ALTER TABLE [dbo].[User]  WITH CHECK ADD  CONSTRAINT [FK__User__CustormerI__6FE99F9F] FOREIGN KEY([CustormerId])
REFERENCES [dbo].[Customer] ([CustomerID])
GO
ALTER TABLE [dbo].[User] CHECK CONSTRAINT [FK__User__CustormerI__6FE99F9F]
GO
ALTER TABLE [dbo].[User]  WITH CHECK ADD  CONSTRAINT [FK__User__EmployeeId__70DDC3D8] FOREIGN KEY([EmployeeId])
REFERENCES [dbo].[Employee] ([EmployeeID])
GO
ALTER TABLE [dbo].[User] CHECK CONSTRAINT [FK__User__EmployeeId__70DDC3D8]
GO
USE [master]
GO
ALTER DATABASE [QLBHDienThoai] SET  READ_WRITE 
GO
