USE [QLBHDienThoai]
GO
/****** Object:  Table [dbo].[BillOfSale]    Script Date: 18/06/2019 11:48:25 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[BillOfSale](
	[BillID] [int] IDENTITY(1,1) NOT NULL,
	[CartID] [int] NULL,
	[BuyDate] [date] NULL,
	[Status] [int] NULL,
	[TotalPrice] [decimal](18, 0) NULL,
	[EmployeeID] [int] NULL,
	[EmployeeDeliveryID] [int] NULL,
 CONSTRAINT [PK_BillOfSale] PRIMARY KEY CLUSTERED 
(
	[BillID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[BillSaleDetail]    Script Date: 18/06/2019 11:48:26 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[BillSaleDetail](
	[BillDetailID] [int] IDENTITY(1,1) NOT NULL,
	[BillID] [int] NULL,
	[Amount] [int] NULL,
	[PriceProduct] [decimal](18, 0) NULL,
	[ProductID] [int] NULL,
	[PromotionID] [int] NULL,
 CONSTRAINT [PK_BillSaleDetail] PRIMARY KEY CLUSTERED 
(
	[BillDetailID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Cart]    Script Date: 18/06/2019 11:48:26 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Cart](
	[TotalPrice] [decimal](18, 0) NULL,
	[VAT] [int] NULL,
	[CustomerID] [int] NULL,
	[Status] [int] NULL,
	[Date] [datetime] NULL,
	[CartID] [int] IDENTITY(1,1) NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[CartID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[CartDetail]    Script Date: 18/06/2019 11:48:26 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[CartDetail](
	[CartDetailID] [int] IDENTITY(1,1) NOT NULL,
	[CartID] [int] NULL,
	[Amount] [int] NULL,
	[ProductID] [int] NULL,
	[PromotionID] [int] NULL,
	[PriceProduct] [decimal](18, 0) NULL,
 CONSTRAINT [PK__CartDeta__01B6A6D4972666D3] PRIMARY KEY CLUSTERED 
(
	[CartDetailID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Comment]    Script Date: 18/06/2019 11:48:26 PM ******/
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
/****** Object:  Table [dbo].[Customer]    Script Date: 18/06/2019 11:48:26 PM ******/
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
/****** Object:  Table [dbo].[Employee]    Script Date: 18/06/2019 11:48:26 PM ******/
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
	[Type] [int] NULL,
 CONSTRAINT [PK_Employee] PRIMARY KEY CLUSTERED 
(
	[EmployeeID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[OrderInvoice]    Script Date: 18/06/2019 11:48:26 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[OrderInvoice](
	[OrderInvoiceID] [int] IDENTITY(1,1) NOT NULL,
	[OrderDate] [datetime2](7) NULL,
	[SupplierID] [int] NULL,
	[EmployeeID] [int] NULL,
 CONSTRAINT [PK_OrderInvoice] PRIMARY KEY CLUSTERED 
(
	[OrderInvoiceID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[OrderInvoiceDetail]    Script Date: 18/06/2019 11:48:26 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[OrderInvoiceDetail](
	[InvoiceDetailID] [int] IDENTITY(1,1) NOT NULL,
	[OrderInvoiceID] [int] NULL,
	[ProductID] [int] NULL,
	[Amount] [int] NULL,
	[PriceProduct] [decimal](18, 0) NULL,
 CONSTRAINT [PK_OrderInvoiceDetail] PRIMARY KEY CLUSTERED 
(
	[InvoiceDetailID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Pay]    Script Date: 18/06/2019 11:48:26 PM ******/
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
/****** Object:  Table [dbo].[PayDetail]    Script Date: 18/06/2019 11:48:26 PM ******/
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
/****** Object:  Table [dbo].[Product]    Script Date: 18/06/2019 11:48:26 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Product](
	[ProductID] [int] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](50) NULL,
	[PriceProduct] [decimal](18, 0) NULL,
	[Images] [varchar](100) NULL,
	[SupplierID] [int] NULL,
	[TypeProductID] [int] NULL,
	[Amount] [int] NULL,
 CONSTRAINT [PK_Product] PRIMARY KEY CLUSTERED 
(
	[ProductID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Promotion]    Script Date: 18/06/2019 11:48:26 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Promotion](
	[PromotionID] [int] IDENTITY(1,1) NOT NULL,
	[PromotionName] [nvarchar](50) NULL,
	[TypePromotion] [nvarchar](50) NULL,
	[TypeProductID] [int] NOT NULL,
	[ProductID] [int] NULL,
	[StartTime] [date] NULL,
	[EndTime] [date] NULL,
	[SaleOff] [float] NULL,
 CONSTRAINT [PK__Promotio__52C42F2FD1EA56E9] PRIMARY KEY CLUSTERED 
(
	[PromotionID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Receipt]    Script Date: 18/06/2019 11:48:26 PM ******/
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
/****** Object:  Table [dbo].[ReceiptDetail]    Script Date: 18/06/2019 11:48:26 PM ******/
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
/****** Object:  Table [dbo].[Role]    Script Date: 18/06/2019 11:48:26 PM ******/
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
/****** Object:  Table [dbo].[Supplier]    Script Date: 18/06/2019 11:48:26 PM ******/
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
/****** Object:  Table [dbo].[TypeProduct]    Script Date: 18/06/2019 11:48:27 PM ******/
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
/****** Object:  Table [dbo].[User]    Script Date: 18/06/2019 11:48:27 PM ******/
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
SET IDENTITY_INSERT [dbo].[BillOfSale] ON 

INSERT [dbo].[BillOfSale] ([BillID], [CartID], [BuyDate], [Status], [TotalPrice], [EmployeeID], [EmployeeDeliveryID]) VALUES (1, 6, CAST(N'2019-06-13' AS Date), -1, CAST(47300000 AS Decimal(18, 0)), 9, 10)
INSERT [dbo].[BillOfSale] ([BillID], [CartID], [BuyDate], [Status], [TotalPrice], [EmployeeID], [EmployeeDeliveryID]) VALUES (2, 5, CAST(N'2019-06-13' AS Date), 0, CAST(15000000 AS Decimal(18, 0)), 9, 10)
INSERT [dbo].[BillOfSale] ([BillID], [CartID], [BuyDate], [Status], [TotalPrice], [EmployeeID], [EmployeeDeliveryID]) VALUES (3, 4, CAST(N'2019-06-13' AS Date), 0, CAST(22500000 AS Decimal(18, 0)), 9, 10)
INSERT [dbo].[BillOfSale] ([BillID], [CartID], [BuyDate], [Status], [TotalPrice], [EmployeeID], [EmployeeDeliveryID]) VALUES (4, 7, CAST(N'2019-06-13' AS Date), 1, CAST(2340000 AS Decimal(18, 0)), 9, 10)
INSERT [dbo].[BillOfSale] ([BillID], [CartID], [BuyDate], [Status], [TotalPrice], [EmployeeID], [EmployeeDeliveryID]) VALUES (5, 12, CAST(N'2019-06-15' AS Date), 0, CAST(6500000 AS Decimal(18, 0)), 9, 10)
INSERT [dbo].[BillOfSale] ([BillID], [CartID], [BuyDate], [Status], [TotalPrice], [EmployeeID], [EmployeeDeliveryID]) VALUES (6, 9, CAST(N'2019-06-15' AS Date), 0, CAST(6500000 AS Decimal(18, 0)), 9, 10)
INSERT [dbo].[BillOfSale] ([BillID], [CartID], [BuyDate], [Status], [TotalPrice], [EmployeeID], [EmployeeDeliveryID]) VALUES (7, 15, CAST(N'2019-06-15' AS Date), -1, CAST(10000000 AS Decimal(18, 0)), 9, 10)
INSERT [dbo].[BillOfSale] ([BillID], [CartID], [BuyDate], [Status], [TotalPrice], [EmployeeID], [EmployeeDeliveryID]) VALUES (8, 16, CAST(N'2019-06-16' AS Date), -1, CAST(9600000 AS Decimal(18, 0)), 9, 10)
INSERT [dbo].[BillOfSale] ([BillID], [CartID], [BuyDate], [Status], [TotalPrice], [EmployeeID], [EmployeeDeliveryID]) VALUES (9, 21, CAST(N'2019-06-16' AS Date), 0, CAST(928528 AS Decimal(18, 0)), 13, 14)
INSERT [dbo].[BillOfSale] ([BillID], [CartID], [BuyDate], [Status], [TotalPrice], [EmployeeID], [EmployeeDeliveryID]) VALUES (10, 20, CAST(N'2019-06-18' AS Date), 0, CAST(928528 AS Decimal(18, 0)), 9, 10)
INSERT [dbo].[BillOfSale] ([BillID], [CartID], [BuyDate], [Status], [TotalPrice], [EmployeeID], [EmployeeDeliveryID]) VALUES (11, 22, CAST(N'2019-06-18' AS Date), 0, CAST(6500000 AS Decimal(18, 0)), 11, 10)
SET IDENTITY_INSERT [dbo].[BillOfSale] OFF
SET IDENTITY_INSERT [dbo].[BillSaleDetail] ON 

INSERT [dbo].[BillSaleDetail] ([BillDetailID], [BillID], [Amount], [PriceProduct], [ProductID], [PromotionID]) VALUES (1, 1, 1, CAST(16000000 AS Decimal(18, 0)), 13, NULL)
INSERT [dbo].[BillSaleDetail] ([BillDetailID], [BillID], [Amount], [PriceProduct], [ProductID], [PromotionID]) VALUES (2, 1, 1, CAST(15000000 AS Decimal(18, 0)), 21, NULL)
INSERT [dbo].[BillSaleDetail] ([BillDetailID], [BillID], [Amount], [PriceProduct], [ProductID], [PromotionID]) VALUES (3, 1, 1, CAST(12000000 AS Decimal(18, 0)), 20, NULL)
INSERT [dbo].[BillSaleDetail] ([BillDetailID], [BillID], [Amount], [PriceProduct], [ProductID], [PromotionID]) VALUES (4, 1, 1, CAST(4300000 AS Decimal(18, 0)), 11, NULL)
INSERT [dbo].[BillSaleDetail] ([BillDetailID], [BillID], [Amount], [PriceProduct], [ProductID], [PromotionID]) VALUES (5, 2, 1, CAST(15000000 AS Decimal(18, 0)), 21, NULL)
INSERT [dbo].[BillSaleDetail] ([BillDetailID], [BillID], [Amount], [PriceProduct], [ProductID], [PromotionID]) VALUES (6, 3, 2, CAST(15000000 AS Decimal(18, 0)), 21, 3)
INSERT [dbo].[BillSaleDetail] ([BillDetailID], [BillID], [Amount], [PriceProduct], [ProductID], [PromotionID]) VALUES (7, 4, 1, CAST(20000 AS Decimal(18, 0)), 6, NULL)
INSERT [dbo].[BillSaleDetail] ([BillDetailID], [BillID], [Amount], [PriceProduct], [ProductID], [PromotionID]) VALUES (8, 4, 1, CAST(20000 AS Decimal(18, 0)), 7, NULL)
INSERT [dbo].[BillSaleDetail] ([BillDetailID], [BillID], [Amount], [PriceProduct], [ProductID], [PromotionID]) VALUES (9, 4, 1, CAST(2300000 AS Decimal(18, 0)), 17, NULL)
INSERT [dbo].[BillSaleDetail] ([BillDetailID], [BillID], [Amount], [PriceProduct], [ProductID], [PromotionID]) VALUES (10, 5, 1, CAST(6500000 AS Decimal(18, 0)), 9, NULL)
INSERT [dbo].[BillSaleDetail] ([BillDetailID], [BillID], [Amount], [PriceProduct], [ProductID], [PromotionID]) VALUES (11, 6, 1, CAST(6500000 AS Decimal(18, 0)), 9, NULL)
INSERT [dbo].[BillSaleDetail] ([BillDetailID], [BillID], [Amount], [PriceProduct], [ProductID], [PromotionID]) VALUES (12, 7, 1, CAST(10000000 AS Decimal(18, 0)), 12, NULL)
INSERT [dbo].[BillSaleDetail] ([BillDetailID], [BillID], [Amount], [PriceProduct], [ProductID], [PromotionID]) VALUES (13, 8, 1, CAST(16000000 AS Decimal(18, 0)), 13, 1)
INSERT [dbo].[BillSaleDetail] ([BillDetailID], [BillID], [Amount], [PriceProduct], [ProductID], [PromotionID]) VALUES (14, 9, 4, CAST(232132 AS Decimal(18, 0)), 23, NULL)
INSERT [dbo].[BillSaleDetail] ([BillDetailID], [BillID], [Amount], [PriceProduct], [ProductID], [PromotionID]) VALUES (15, 10, 4, CAST(232132 AS Decimal(18, 0)), 23, NULL)
INSERT [dbo].[BillSaleDetail] ([BillDetailID], [BillID], [Amount], [PriceProduct], [ProductID], [PromotionID]) VALUES (16, 11, 1, CAST(6500000 AS Decimal(18, 0)), 9, NULL)
SET IDENTITY_INSERT [dbo].[BillSaleDetail] OFF
SET IDENTITY_INSERT [dbo].[Cart] ON 

INSERT [dbo].[Cart] ([TotalPrice], [VAT], [CustomerID], [Status], [Date], [CartID]) VALUES (CAST(16000000 AS Decimal(18, 0)), 0, 6, -1, CAST(N'2019-06-10T01:58:12.467' AS DateTime), 2)
INSERT [dbo].[Cart] ([TotalPrice], [VAT], [CustomerID], [Status], [Date], [CartID]) VALUES (CAST(22500000 AS Decimal(18, 0)), 0, 6, 1, CAST(N'2019-06-10T02:13:35.867' AS DateTime), 4)
INSERT [dbo].[Cart] ([TotalPrice], [VAT], [CustomerID], [Status], [Date], [CartID]) VALUES (CAST(15000000 AS Decimal(18, 0)), 0, 7, 1, CAST(N'2019-06-12T10:07:30.847' AS DateTime), 5)
INSERT [dbo].[Cart] ([TotalPrice], [VAT], [CustomerID], [Status], [Date], [CartID]) VALUES (CAST(47300000 AS Decimal(18, 0)), 0, 7, 1, CAST(N'2019-06-12T15:52:31.533' AS DateTime), 6)
INSERT [dbo].[Cart] ([TotalPrice], [VAT], [CustomerID], [Status], [Date], [CartID]) VALUES (CAST(2340000 AS Decimal(18, 0)), 0, 7, 1, CAST(N'2019-06-13T16:05:11.023' AS DateTime), 7)
INSERT [dbo].[Cart] ([TotalPrice], [VAT], [CustomerID], [Status], [Date], [CartID]) VALUES (CAST(2300000 AS Decimal(18, 0)), 0, 6, 0, CAST(N'2019-06-15T20:13:19.260' AS DateTime), 8)
INSERT [dbo].[Cart] ([TotalPrice], [VAT], [CustomerID], [Status], [Date], [CartID]) VALUES (CAST(6500000 AS Decimal(18, 0)), 0, 6, 1, CAST(N'2019-06-15T20:47:58.340' AS DateTime), 9)
INSERT [dbo].[Cart] ([TotalPrice], [VAT], [CustomerID], [Status], [Date], [CartID]) VALUES (CAST(10000000 AS Decimal(18, 0)), 0, 6, 0, CAST(N'2019-06-15T20:49:17.943' AS DateTime), 10)
INSERT [dbo].[Cart] ([TotalPrice], [VAT], [CustomerID], [Status], [Date], [CartID]) VALUES (CAST(8500000 AS Decimal(18, 0)), 0, 6, 0, CAST(N'2019-06-15T20:50:10.677' AS DateTime), 11)
INSERT [dbo].[Cart] ([TotalPrice], [VAT], [CustomerID], [Status], [Date], [CartID]) VALUES (CAST(6500000 AS Decimal(18, 0)), 0, 6, 1, CAST(N'2019-06-15T20:50:54.567' AS DateTime), 12)
INSERT [dbo].[Cart] ([TotalPrice], [VAT], [CustomerID], [Status], [Date], [CartID]) VALUES (CAST(4300000 AS Decimal(18, 0)), 0, 8, 0, CAST(N'2019-06-15T22:57:54.043' AS DateTime), 13)
INSERT [dbo].[Cart] ([TotalPrice], [VAT], [CustomerID], [Status], [Date], [CartID]) VALUES (CAST(16000000 AS Decimal(18, 0)), 0, 9, -1, CAST(N'2019-06-15T23:03:12.083' AS DateTime), 14)
INSERT [dbo].[Cart] ([TotalPrice], [VAT], [CustomerID], [Status], [Date], [CartID]) VALUES (CAST(10000000 AS Decimal(18, 0)), 0, 10, 1, CAST(N'2019-06-15T23:11:09.267' AS DateTime), 15)
INSERT [dbo].[Cart] ([TotalPrice], [VAT], [CustomerID], [Status], [Date], [CartID]) VALUES (CAST(9600000 AS Decimal(18, 0)), 0, 10, 1, CAST(N'2019-06-15T23:15:41.643' AS DateTime), 16)
INSERT [dbo].[Cart] ([TotalPrice], [VAT], [CustomerID], [Status], [Date], [CartID]) VALUES (CAST(928528 AS Decimal(18, 0)), 0, 10, 0, CAST(N'2019-06-16T21:59:42.690' AS DateTime), 17)
INSERT [dbo].[Cart] ([TotalPrice], [VAT], [CustomerID], [Status], [Date], [CartID]) VALUES (CAST(928528 AS Decimal(18, 0)), 0, 10, 0, CAST(N'2019-06-16T21:59:50.820' AS DateTime), 18)
INSERT [dbo].[Cart] ([TotalPrice], [VAT], [CustomerID], [Status], [Date], [CartID]) VALUES (CAST(928528 AS Decimal(18, 0)), 0, 10, 0, CAST(N'2019-06-16T21:59:58.540' AS DateTime), 19)
INSERT [dbo].[Cart] ([TotalPrice], [VAT], [CustomerID], [Status], [Date], [CartID]) VALUES (CAST(928528 AS Decimal(18, 0)), 0, 10, 1, CAST(N'2019-06-16T22:00:03.670' AS DateTime), 20)
INSERT [dbo].[Cart] ([TotalPrice], [VAT], [CustomerID], [Status], [Date], [CartID]) VALUES (CAST(928528 AS Decimal(18, 0)), 0, 10, 1, CAST(N'2019-06-16T22:00:23.450' AS DateTime), 21)
INSERT [dbo].[Cart] ([TotalPrice], [VAT], [CustomerID], [Status], [Date], [CartID]) VALUES (CAST(6500000 AS Decimal(18, 0)), 0, 7, 1, CAST(N'2019-06-18T23:17:36.330' AS DateTime), 22)
INSERT [dbo].[Cart] ([TotalPrice], [VAT], [CustomerID], [Status], [Date], [CartID]) VALUES (CAST(6500000 AS Decimal(18, 0)), 0, 7, 0, CAST(N'2019-06-18T23:20:24.037' AS DateTime), 23)
INSERT [dbo].[Cart] ([TotalPrice], [VAT], [CustomerID], [Status], [Date], [CartID]) VALUES (CAST(9600000 AS Decimal(18, 0)), 0, 7, 0, CAST(N'2019-06-18T23:21:39.877' AS DateTime), 24)
SET IDENTITY_INSERT [dbo].[Cart] OFF
SET IDENTITY_INSERT [dbo].[CartDetail] ON 

INSERT [dbo].[CartDetail] ([CartDetailID], [CartID], [Amount], [ProductID], [PromotionID], [PriceProduct]) VALUES (2, 2, 1, 13, NULL, CAST(16000000 AS Decimal(18, 0)))
INSERT [dbo].[CartDetail] ([CartDetailID], [CartID], [Amount], [ProductID], [PromotionID], [PriceProduct]) VALUES (4, 4, 2, 21, NULL, CAST(15000000 AS Decimal(18, 0)))
INSERT [dbo].[CartDetail] ([CartDetailID], [CartID], [Amount], [ProductID], [PromotionID], [PriceProduct]) VALUES (5, 5, 1, 21, NULL, CAST(15000000 AS Decimal(18, 0)))
INSERT [dbo].[CartDetail] ([CartDetailID], [CartID], [Amount], [ProductID], [PromotionID], [PriceProduct]) VALUES (6, 6, 1, 13, NULL, CAST(16000000 AS Decimal(18, 0)))
INSERT [dbo].[CartDetail] ([CartDetailID], [CartID], [Amount], [ProductID], [PromotionID], [PriceProduct]) VALUES (7, 6, 1, 21, NULL, CAST(15000000 AS Decimal(18, 0)))
INSERT [dbo].[CartDetail] ([CartDetailID], [CartID], [Amount], [ProductID], [PromotionID], [PriceProduct]) VALUES (8, 6, 1, 20, NULL, CAST(12000000 AS Decimal(18, 0)))
INSERT [dbo].[CartDetail] ([CartDetailID], [CartID], [Amount], [ProductID], [PromotionID], [PriceProduct]) VALUES (9, 6, 1, 11, NULL, CAST(4300000 AS Decimal(18, 0)))
INSERT [dbo].[CartDetail] ([CartDetailID], [CartID], [Amount], [ProductID], [PromotionID], [PriceProduct]) VALUES (10, 7, 1, 6, NULL, CAST(20000 AS Decimal(18, 0)))
INSERT [dbo].[CartDetail] ([CartDetailID], [CartID], [Amount], [ProductID], [PromotionID], [PriceProduct]) VALUES (11, 7, 1, 7, NULL, CAST(20000 AS Decimal(18, 0)))
INSERT [dbo].[CartDetail] ([CartDetailID], [CartID], [Amount], [ProductID], [PromotionID], [PriceProduct]) VALUES (12, 7, 1, 17, NULL, CAST(2300000 AS Decimal(18, 0)))
INSERT [dbo].[CartDetail] ([CartDetailID], [CartID], [Amount], [ProductID], [PromotionID], [PriceProduct]) VALUES (13, 8, 1, 17, NULL, CAST(2300000 AS Decimal(18, 0)))
INSERT [dbo].[CartDetail] ([CartDetailID], [CartID], [Amount], [ProductID], [PromotionID], [PriceProduct]) VALUES (14, 9, 1, 9, NULL, CAST(6500000 AS Decimal(18, 0)))
INSERT [dbo].[CartDetail] ([CartDetailID], [CartID], [Amount], [ProductID], [PromotionID], [PriceProduct]) VALUES (15, 10, 1, 4, NULL, CAST(10000000 AS Decimal(18, 0)))
INSERT [dbo].[CartDetail] ([CartDetailID], [CartID], [Amount], [ProductID], [PromotionID], [PriceProduct]) VALUES (16, 11, 1, 10, NULL, CAST(8500000 AS Decimal(18, 0)))
INSERT [dbo].[CartDetail] ([CartDetailID], [CartID], [Amount], [ProductID], [PromotionID], [PriceProduct]) VALUES (17, 12, 1, 9, NULL, CAST(6500000 AS Decimal(18, 0)))
INSERT [dbo].[CartDetail] ([CartDetailID], [CartID], [Amount], [ProductID], [PromotionID], [PriceProduct]) VALUES (18, 13, 1, 11, NULL, CAST(4300000 AS Decimal(18, 0)))
INSERT [dbo].[CartDetail] ([CartDetailID], [CartID], [Amount], [ProductID], [PromotionID], [PriceProduct]) VALUES (19, 14, 2, 19, NULL, CAST(8000000 AS Decimal(18, 0)))
INSERT [dbo].[CartDetail] ([CartDetailID], [CartID], [Amount], [ProductID], [PromotionID], [PriceProduct]) VALUES (20, 15, 1, 12, NULL, CAST(10000000 AS Decimal(18, 0)))
INSERT [dbo].[CartDetail] ([CartDetailID], [CartID], [Amount], [ProductID], [PromotionID], [PriceProduct]) VALUES (21, 16, 1, 13, 1, CAST(16000000 AS Decimal(18, 0)))
INSERT [dbo].[CartDetail] ([CartDetailID], [CartID], [Amount], [ProductID], [PromotionID], [PriceProduct]) VALUES (22, 17, 4, 23, NULL, CAST(232132 AS Decimal(18, 0)))
INSERT [dbo].[CartDetail] ([CartDetailID], [CartID], [Amount], [ProductID], [PromotionID], [PriceProduct]) VALUES (23, 18, 4, 23, NULL, CAST(232132 AS Decimal(18, 0)))
INSERT [dbo].[CartDetail] ([CartDetailID], [CartID], [Amount], [ProductID], [PromotionID], [PriceProduct]) VALUES (24, 19, 4, 23, NULL, CAST(232132 AS Decimal(18, 0)))
INSERT [dbo].[CartDetail] ([CartDetailID], [CartID], [Amount], [ProductID], [PromotionID], [PriceProduct]) VALUES (25, 20, 4, 23, NULL, CAST(232132 AS Decimal(18, 0)))
INSERT [dbo].[CartDetail] ([CartDetailID], [CartID], [Amount], [ProductID], [PromotionID], [PriceProduct]) VALUES (26, 21, 4, 23, NULL, CAST(232132 AS Decimal(18, 0)))
INSERT [dbo].[CartDetail] ([CartDetailID], [CartID], [Amount], [ProductID], [PromotionID], [PriceProduct]) VALUES (27, 22, 1, 9, NULL, CAST(6500000 AS Decimal(18, 0)))
INSERT [dbo].[CartDetail] ([CartDetailID], [CartID], [Amount], [ProductID], [PromotionID], [PriceProduct]) VALUES (28, 23, 1, 9, NULL, CAST(6500000 AS Decimal(18, 0)))
INSERT [dbo].[CartDetail] ([CartDetailID], [CartID], [Amount], [ProductID], [PromotionID], [PriceProduct]) VALUES (29, 24, 1, 13, 1, CAST(16000000 AS Decimal(18, 0)))
SET IDENTITY_INSERT [dbo].[CartDetail] OFF
SET IDENTITY_INSERT [dbo].[Customer] ON 

INSERT [dbo].[Customer] ([CustomerID], [Fullname], [Address], [Phone], [Birthday], [Gender]) VALUES (1, N'Nguyễn Thu Hà', N'Hà Nội', N'396201695', CAST(N'2019-06-08' AS Date), 0)
INSERT [dbo].[Customer] ([CustomerID], [Fullname], [Address], [Phone], [Birthday], [Gender]) VALUES (6, N'Trần Trọng Long', N'Cổ Nhuế, Hà Nội', N'0112459878', CAST(N'1998-12-09' AS Date), 1)
INSERT [dbo].[Customer] ([CustomerID], [Fullname], [Address], [Phone], [Birthday], [Gender]) VALUES (7, N'Đình Phúc', N'Ngõ 1 Phạm Văn Đồng Hà Nội', N'973642632', CAST(N'1997-01-20' AS Date), 1)
INSERT [dbo].[Customer] ([CustomerID], [Fullname], [Address], [Phone], [Birthday], [Gender]) VALUES (8, N'Nguyễn Anh Minh', N'Hà Nội', N'09563287462', CAST(N'1998-01-01' AS Date), 1)
INSERT [dbo].[Customer] ([CustomerID], [Fullname], [Address], [Phone], [Birthday], [Gender]) VALUES (9, N'Trần Trọng Long', N'Bắc Từ Liêm, Hà Nội', N'0222222222', CAST(N'1998-01-01' AS Date), 1)
INSERT [dbo].[Customer] ([CustomerID], [Fullname], [Address], [Phone], [Birthday], [Gender]) VALUES (10, N'nguyễn thu hà', N'hn', N'235989623', CAST(N'1996-01-01' AS Date), 0)
SET IDENTITY_INSERT [dbo].[Customer] OFF
SET IDENTITY_INSERT [dbo].[Employee] ON 

INSERT [dbo].[Employee] ([EmployeeID], [Name], [Birthday], [Phone], [RoleID], [Type]) VALUES (9, N'Lê Anh Đức', CAST(N'2019-06-08' AS Date), N'91612325', NULL, 0)
INSERT [dbo].[Employee] ([EmployeeID], [Name], [Birthday], [Phone], [RoleID], [Type]) VALUES (10, N'Phúc NV', CAST(N'1970-01-01' AS Date), N'0125422', NULL, 1)
INSERT [dbo].[Employee] ([EmployeeID], [Name], [Birthday], [Phone], [RoleID], [Type]) VALUES (11, N'bé gấu', CAST(N'1996-07-11' AS Date), N'0396201695', NULL, 0)
INSERT [dbo].[Employee] ([EmployeeID], [Name], [Birthday], [Phone], [RoleID], [Type]) VALUES (12, N'ha12', CAST(N'1996-07-11' AS Date), N'011111111111', NULL, 0)
INSERT [dbo].[Employee] ([EmployeeID], [Name], [Birthday], [Phone], [RoleID], [Type]) VALUES (13, N'nguyễn thu hà', CAST(N'1996-07-11' AS Date), N'088888888', NULL, 0)
INSERT [dbo].[Employee] ([EmployeeID], [Name], [Birthday], [Phone], [RoleID], [Type]) VALUES (14, N'minh táo', CAST(N'1999-01-01' AS Date), N'04554646', NULL, 1)
INSERT [dbo].[Employee] ([EmployeeID], [Name], [Birthday], [Phone], [RoleID], [Type]) VALUES (15, N'minh', CAST(N'1999-12-12' AS Date), N'43254545', NULL, 0)
INSERT [dbo].[Employee] ([EmployeeID], [Name], [Birthday], [Phone], [RoleID], [Type]) VALUES (16, N'he', CAST(N'1997-03-31' AS Date), N'016562624', NULL, 0)
SET IDENTITY_INSERT [dbo].[Employee] OFF
SET IDENTITY_INSERT [dbo].[OrderInvoice] ON 

INSERT [dbo].[OrderInvoice] ([OrderInvoiceID], [OrderDate], [SupplierID], [EmployeeID]) VALUES (4, CAST(N'2019-06-14T00:00:00.0000000' AS DateTime2), 1, 9)
INSERT [dbo].[OrderInvoice] ([OrderInvoiceID], [OrderDate], [SupplierID], [EmployeeID]) VALUES (5, CAST(N'2019-06-14T00:00:00.0000000' AS DateTime2), 1, 9)
INSERT [dbo].[OrderInvoice] ([OrderInvoiceID], [OrderDate], [SupplierID], [EmployeeID]) VALUES (6, CAST(N'2019-06-14T00:00:00.0000000' AS DateTime2), 1, 9)
INSERT [dbo].[OrderInvoice] ([OrderInvoiceID], [OrderDate], [SupplierID], [EmployeeID]) VALUES (7, CAST(N'2019-06-14T00:00:00.0000000' AS DateTime2), 1, 9)
INSERT [dbo].[OrderInvoice] ([OrderInvoiceID], [OrderDate], [SupplierID], [EmployeeID]) VALUES (8, CAST(N'2019-06-15T00:00:00.0000000' AS DateTime2), 1, 9)
INSERT [dbo].[OrderInvoice] ([OrderInvoiceID], [OrderDate], [SupplierID], [EmployeeID]) VALUES (9, CAST(N'2019-06-15T22:42:32.9477995' AS DateTime2), 1, 10)
INSERT [dbo].[OrderInvoice] ([OrderInvoiceID], [OrderDate], [SupplierID], [EmployeeID]) VALUES (10, CAST(N'2019-06-15T00:00:00.0000000' AS DateTime2), 1, 10)
INSERT [dbo].[OrderInvoice] ([OrderInvoiceID], [OrderDate], [SupplierID], [EmployeeID]) VALUES (11, CAST(N'2019-06-15T00:00:00.0000000' AS DateTime2), 1, 10)
INSERT [dbo].[OrderInvoice] ([OrderInvoiceID], [OrderDate], [SupplierID], [EmployeeID]) VALUES (12, CAST(N'2019-06-16T00:00:00.0000000' AS DateTime2), 1, 11)
INSERT [dbo].[OrderInvoice] ([OrderInvoiceID], [OrderDate], [SupplierID], [EmployeeID]) VALUES (13, CAST(N'2019-06-16T00:00:00.0000000' AS DateTime2), 1, 9)
INSERT [dbo].[OrderInvoice] ([OrderInvoiceID], [OrderDate], [SupplierID], [EmployeeID]) VALUES (14, CAST(N'2019-06-16T00:00:00.0000000' AS DateTime2), 4, 9)
INSERT [dbo].[OrderInvoice] ([OrderInvoiceID], [OrderDate], [SupplierID], [EmployeeID]) VALUES (15, CAST(N'2019-06-16T00:00:00.0000000' AS DateTime2), 1, 9)
INSERT [dbo].[OrderInvoice] ([OrderInvoiceID], [OrderDate], [SupplierID], [EmployeeID]) VALUES (16, CAST(N'2019-06-16T00:00:00.0000000' AS DateTime2), 1, 9)
SET IDENTITY_INSERT [dbo].[OrderInvoice] OFF
SET IDENTITY_INSERT [dbo].[OrderInvoiceDetail] ON 

INSERT [dbo].[OrderInvoiceDetail] ([InvoiceDetailID], [OrderInvoiceID], [ProductID], [Amount], [PriceProduct]) VALUES (5, 4, 21, 20, CAST(15000000 AS Decimal(18, 0)))
INSERT [dbo].[OrderInvoiceDetail] ([InvoiceDetailID], [OrderInvoiceID], [ProductID], [Amount], [PriceProduct]) VALUES (6, 5, 21, 10, CAST(15000000 AS Decimal(18, 0)))
INSERT [dbo].[OrderInvoiceDetail] ([InvoiceDetailID], [OrderInvoiceID], [ProductID], [Amount], [PriceProduct]) VALUES (7, 5, 20, 10, CAST(10000000 AS Decimal(18, 0)))
INSERT [dbo].[OrderInvoiceDetail] ([InvoiceDetailID], [OrderInvoiceID], [ProductID], [Amount], [PriceProduct]) VALUES (8, 6, 4, 20, CAST(15000000 AS Decimal(18, 0)))
INSERT [dbo].[OrderInvoiceDetail] ([InvoiceDetailID], [OrderInvoiceID], [ProductID], [Amount], [PriceProduct]) VALUES (10, NULL, 20, 10, CAST(15000000 AS Decimal(18, 0)))
INSERT [dbo].[OrderInvoiceDetail] ([InvoiceDetailID], [OrderInvoiceID], [ProductID], [Amount], [PriceProduct]) VALUES (12, 7, 21, 10, CAST(13000000 AS Decimal(18, 0)))
INSERT [dbo].[OrderInvoiceDetail] ([InvoiceDetailID], [OrderInvoiceID], [ProductID], [Amount], [PriceProduct]) VALUES (13, 8, 21, 15, CAST(12000000 AS Decimal(18, 0)))
INSERT [dbo].[OrderInvoiceDetail] ([InvoiceDetailID], [OrderInvoiceID], [ProductID], [Amount], [PriceProduct]) VALUES (14, 8, 20, 5, CAST(10000000 AS Decimal(18, 0)))
INSERT [dbo].[OrderInvoiceDetail] ([InvoiceDetailID], [OrderInvoiceID], [ProductID], [Amount], [PriceProduct]) VALUES (15, 9, 13, 10, CAST(15000000 AS Decimal(18, 0)))
INSERT [dbo].[OrderInvoiceDetail] ([InvoiceDetailID], [OrderInvoiceID], [ProductID], [Amount], [PriceProduct]) VALUES (16, 10, 21, 20, CAST(14500000 AS Decimal(18, 0)))
INSERT [dbo].[OrderInvoiceDetail] ([InvoiceDetailID], [OrderInvoiceID], [ProductID], [Amount], [PriceProduct]) VALUES (17, 11, 13, 10, CAST(15000000 AS Decimal(18, 0)))
INSERT [dbo].[OrderInvoiceDetail] ([InvoiceDetailID], [OrderInvoiceID], [ProductID], [Amount], [PriceProduct]) VALUES (18, 11, 21, 20, CAST(14000000 AS Decimal(18, 0)))
INSERT [dbo].[OrderInvoiceDetail] ([InvoiceDetailID], [OrderInvoiceID], [ProductID], [Amount], [PriceProduct]) VALUES (19, 12, 19, 12, CAST(12 AS Decimal(18, 0)))
INSERT [dbo].[OrderInvoiceDetail] ([InvoiceDetailID], [OrderInvoiceID], [ProductID], [Amount], [PriceProduct]) VALUES (20, 13, 19, 10, CAST(13 AS Decimal(18, 0)))
INSERT [dbo].[OrderInvoiceDetail] ([InvoiceDetailID], [OrderInvoiceID], [ProductID], [Amount], [PriceProduct]) VALUES (21, 14, 22, 123, CAST(12333 AS Decimal(18, 0)))
INSERT [dbo].[OrderInvoiceDetail] ([InvoiceDetailID], [OrderInvoiceID], [ProductID], [Amount], [PriceProduct]) VALUES (22, 15, 24, 10, CAST(123456 AS Decimal(18, 0)))
INSERT [dbo].[OrderInvoiceDetail] ([InvoiceDetailID], [OrderInvoiceID], [ProductID], [Amount], [PriceProduct]) VALUES (23, 15, 23, 10, CAST(1111111111 AS Decimal(18, 0)))
INSERT [dbo].[OrderInvoiceDetail] ([InvoiceDetailID], [OrderInvoiceID], [ProductID], [Amount], [PriceProduct]) VALUES (24, 16, 24, 12, CAST(123456789 AS Decimal(18, 0)))
SET IDENTITY_INSERT [dbo].[OrderInvoiceDetail] OFF
SET IDENTITY_INSERT [dbo].[Product] ON 

INSERT [dbo].[Product] ([ProductID], [Name], [PriceProduct], [Images], [SupplierID], [TypeProductID], [Amount]) VALUES (4, N'Iphone 7 32G màu trắng', CAST(10000000 AS Decimal(18, 0)), N'/FileUploads/images/item-02.jpg', 1, 1, 10)
INSERT [dbo].[Product] ([ProductID], [Name], [PriceProduct], [Images], [SupplierID], [TypeProductID], [Amount]) VALUES (5, N'Iphone 7 16g màu đỏ', CAST(70000 AS Decimal(18, 0)), N'/FileUploads/images/item-03.jpg', 1, 1, 10)
INSERT [dbo].[Product] ([ProductID], [Name], [PriceProduct], [Images], [SupplierID], [TypeProductID], [Amount]) VALUES (6, N'sam sung j7', CAST(20000 AS Decimal(18, 0)), N'/FileUploads/images/item-04.jpg', 1, 2, 10)
INSERT [dbo].[Product] ([ProductID], [Name], [PriceProduct], [Images], [SupplierID], [TypeProductID], [Amount]) VALUES (7, N'sam sung j3', CAST(20000 AS Decimal(18, 0)), N'/FileUploads/images/opa37.jpg', 1, 2, 10)
INSERT [dbo].[Product] ([ProductID], [Name], [PriceProduct], [Images], [SupplierID], [TypeProductID], [Amount]) VALUES (8, N'iphone', CAST(20000 AS Decimal(18, 0)), N'/FileUploads/images/ipx.jpg', 1, 1, 10)
INSERT [dbo].[Product] ([ProductID], [Name], [PriceProduct], [Images], [SupplierID], [TypeProductID], [Amount]) VALUES (9, N'Samsung galaxy s6 plus', CAST(6500000 AS Decimal(18, 0)), N'/FileUploads/images/sss8.jpg', 1, NULL, 10)
INSERT [dbo].[Product] ([ProductID], [Name], [PriceProduct], [Images], [SupplierID], [TypeProductID], [Amount]) VALUES (10, N'Samsung galaxy s7 plus', CAST(8500000 AS Decimal(18, 0)), N'/FileUploads/images/hwy7.jpg', 1, NULL, 10)
INSERT [dbo].[Product] ([ProductID], [Name], [PriceProduct], [Images], [SupplierID], [TypeProductID], [Amount]) VALUES (11, N'Redmi note 4', CAST(4300000 AS Decimal(18, 0)), N'/FileUploads/images/xiaomi-redmi-5a-xam-1-400x460.png', 1, 7, 10)
INSERT [dbo].[Product] ([ProductID], [Name], [PriceProduct], [Images], [SupplierID], [TypeProductID], [Amount]) VALUES (12, N'Xiaomi redmi 5a', CAST(10000000 AS Decimal(18, 0)), N'/FileUploads/images/xiaomi-redmi-5a-xam-1-400x460.png', 1, 3, 10)
INSERT [dbo].[Product] ([ProductID], [Name], [PriceProduct], [Images], [SupplierID], [TypeProductID], [Amount]) VALUES (13, N'IPhone8', CAST(16000000 AS Decimal(18, 0)), N'/FileUploads/images/ip8.jpg', 1, 1, 10)
INSERT [dbo].[Product] ([ProductID], [Name], [PriceProduct], [Images], [SupplierID], [TypeProductID], [Amount]) VALUES (17, N'SamSung J2', CAST(2300000 AS Decimal(18, 0)), N'/FileUploads/images/ssj2.jpg', 1, 2, 10)
INSERT [dbo].[Product] ([ProductID], [Name], [PriceProduct], [Images], [SupplierID], [TypeProductID], [Amount]) VALUES (19, N'SamSungM50', CAST(8000000 AS Decimal(18, 0)), N'/FileUploads/images/ssm50.jpg', 1, 2, 10)
INSERT [dbo].[Product] ([ProductID], [Name], [PriceProduct], [Images], [SupplierID], [TypeProductID], [Amount]) VALUES (20, N'SamSung Note7', CAST(12000000 AS Decimal(18, 0)), N'/FileUploads/images/ssn7.jpg', 1, 2, 10)
INSERT [dbo].[Product] ([ProductID], [Name], [PriceProduct], [Images], [SupplierID], [TypeProductID], [Amount]) VALUES (21, N'SamSung S8', CAST(15000000 AS Decimal(18, 0)), N'/FileUploads/images/sss8.jpg', 1, 2, 10)
INSERT [dbo].[Product] ([ProductID], [Name], [PriceProduct], [Images], [SupplierID], [TypeProductID], [Amount]) VALUES (22, N'bé gấu 1', CAST(1111111 AS Decimal(18, 0)), N'/FileUploads/files/hinh-nen-de-thuong-danh-cho-may-tinh2.jpg', NULL, 8, 10)
INSERT [dbo].[Product] ([ProductID], [Name], [PriceProduct], [Images], [SupplierID], [TypeProductID], [Amount]) VALUES (23, N'hihi', CAST(232132 AS Decimal(18, 0)), N'/FileUploads/files/hinh-nen-de-thuong-danh-cho-may-tinh2.jpg', 1, 10, 10)
INSERT [dbo].[Product] ([ProductID], [Name], [PriceProduct], [Images], [SupplierID], [TypeProductID], [Amount]) VALUES (24, N'aaaaaaaaaaaaaaaaaaaaaa', CAST(123 AS Decimal(18, 0)), N'/FileUploads/files/hinh-nen-de-thuong-danh-cho-may-tinh2.jpg', 1, 2, 0)
INSERT [dbo].[Product] ([ProductID], [Name], [PriceProduct], [Images], [SupplierID], [TypeProductID], [Amount]) VALUES (25, N'fdsfd', CAST(10000 AS Decimal(18, 0)), N'/FileUploads/files/hinh-nen-de-thuong-danh-cho-may-tinh2.jpg', 1, 2, 10)
SET IDENTITY_INSERT [dbo].[Product] OFF
SET IDENTITY_INSERT [dbo].[Promotion] ON 

INSERT [dbo].[Promotion] ([PromotionID], [PromotionName], [TypePromotion], [TypeProductID], [ProductID], [StartTime], [EndTime], [SaleOff]) VALUES (1, N'WL0QHAE0QX', N'1', 1, NULL, CAST(N'2019-06-07' AS Date), CAST(N'2019-06-30' AS Date), 40)
INSERT [dbo].[Promotion] ([PromotionID], [PromotionName], [TypePromotion], [TypeProductID], [ProductID], [StartTime], [EndTime], [SaleOff]) VALUES (3, N'Q3C2JFKGS2', N'2', 0, 21, CAST(N'2019-06-07' AS Date), CAST(N'2019-06-30' AS Date), 25)
SET IDENTITY_INSERT [dbo].[Promotion] OFF
INSERT [dbo].[Role] ([RoleID], [Name]) VALUES (1, N'Admin')
INSERT [dbo].[Role] ([RoleID], [Name]) VALUES (2, N'Employee')
INSERT [dbo].[Role] ([RoleID], [Name]) VALUES (3, N'Customer')
INSERT [dbo].[Role] ([RoleID], [Name]) VALUES (4, N'User')
SET IDENTITY_INSERT [dbo].[Supplier] ON 

INSERT [dbo].[Supplier] ([SupplierID], [Name], [Phone], [Address]) VALUES (1, N'Bé Gấu', 12345678, N'hà')
INSERT [dbo].[Supplier] ([SupplierID], [Name], [Phone], [Address]) VALUES (4, N'Bé Gấu', 12345678, N'hà')
SET IDENTITY_INSERT [dbo].[Supplier] OFF
SET IDENTITY_INSERT [dbo].[TypeProduct] ON 

INSERT [dbo].[TypeProduct] ([TypeProductID], [TypeProductName]) VALUES (1, N'Iphone')
INSERT [dbo].[TypeProduct] ([TypeProductID], [TypeProductName]) VALUES (2, N'Sam sung')
INSERT [dbo].[TypeProduct] ([TypeProductID], [TypeProductName]) VALUES (3, N'Xiaomi')
INSERT [dbo].[TypeProduct] ([TypeProductID], [TypeProductName]) VALUES (7, N'Redmi')
INSERT [dbo].[TypeProduct] ([TypeProductID], [TypeProductName]) VALUES (8, N'bé gấu')
INSERT [dbo].[TypeProduct] ([TypeProductID], [TypeProductName]) VALUES (10, N'gấu 1')
SET IDENTITY_INSERT [dbo].[TypeProduct] OFF
INSERT [dbo].[User] ([Username], [Password], [EmployeeId], [CustormerId], [Status], [RoleId], [FullName], [Email], [EmailConfirmed], [Code]) VALUES (N'anhminh', N'123456', NULL, 8, 1, 0, N'Nguyễn Anh Minh', N'long2trn429@gmail.com', 1, N'')
INSERT [dbo].[User] ([Username], [Password], [EmployeeId], [CustormerId], [Status], [RoleId], [FullName], [Email], [EmailConfirmed], [Code]) VALUES (N'darkjker', N'123456', NULL, 9, 1, 4, N'Trần Trọng Long', N'qgd23964@cndps.com', 1, N'123456')
INSERT [dbo].[User] ([Username], [Password], [EmployeeId], [CustormerId], [Status], [RoleId], [FullName], [Email], [EmailConfirmed], [Code]) VALUES (N'hant', N'123', 13, NULL, 1, 2, N'thu hà', N'thuha@g.com', 1, NULL)
INSERT [dbo].[User] ([Username], [Password], [EmployeeId], [CustormerId], [Status], [RoleId], [FullName], [Email], [EmailConfirmed], [Code]) VALUES (N'hihi', N'123', 16, NULL, 1, 2, N'hi', N'h@g.com', 1, NULL)
INSERT [dbo].[User] ([Username], [Password], [EmployeeId], [CustormerId], [Status], [RoleId], [FullName], [Email], [EmailConfirmed], [Code]) VALUES (N'kutyboy98', N'123456', NULL, 6, 1, 1, N'Trần Trọng Long', N'darkjker@gmail.com', 1, N'19008198')
INSERT [dbo].[User] ([Username], [Password], [EmployeeId], [CustormerId], [Status], [RoleId], [FullName], [Email], [EmailConfirmed], [Code]) VALUES (N'minhsuc', N'123456', NULL, 1, 1, 3, N'Lê Minh Sức', N'utu83713@bcaoo.com', 1, N'')
INSERT [dbo].[User] ([Username], [Password], [EmployeeId], [CustormerId], [Status], [RoleId], [FullName], [Email], [EmailConfirmed], [Code]) VALUES (N'phucnd', N'123', NULL, 7, 1, 3, N'Đình Phúc', N'phund@gmmail.com', 1, N'19008198')
INSERT [dbo].[User] ([Username], [Password], [EmployeeId], [CustormerId], [Status], [RoleId], [FullName], [Email], [EmailConfirmed], [Code]) VALUES (N'phucnd123', N'123', NULL, NULL, 1, 1, N'Đình Phúc', N'phucnd@123gmail.com', 1, NULL)
INSERT [dbo].[User] ([Username], [Password], [EmployeeId], [CustormerId], [Status], [RoleId], [FullName], [Email], [EmailConfirmed], [Code]) VALUES (N'thuha1', N'1234', NULL, 10, 1, 0, N'nguyễn thu hà', N'thuha110796@outlook.com', 1, N'')
ALTER TABLE [dbo].[CartDetail] ADD  CONSTRAINT [DF__CartDetai__Price__3D5E1FD2]  DEFAULT ((0)) FOR [PriceProduct]
GO
ALTER TABLE [dbo].[Product] ADD  DEFAULT ((0)) FOR [Amount]
GO
ALTER TABLE [dbo].[User] ADD  CONSTRAINT [DF_tblUser_password]  DEFAULT ((123456)) FOR [Password]
GO
ALTER TABLE [dbo].[User] ADD  CONSTRAINT [DF__User__Status__6E01572D]  DEFAULT ((1)) FOR [Status]
GO
ALTER TABLE [dbo].[BillOfSale]  WITH CHECK ADD  CONSTRAINT [FK__BillOfSal__CartI__5CD6CB2B] FOREIGN KEY([CartID])
REFERENCES [dbo].[Cart] ([CartID])
GO
ALTER TABLE [dbo].[BillOfSale] CHECK CONSTRAINT [FK__BillOfSal__CartI__5CD6CB2B]
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
ALTER TABLE [dbo].[BillSaleDetail]  WITH CHECK ADD  CONSTRAINT [FK_BillSaleDetail_Promotion] FOREIGN KEY([PromotionID])
REFERENCES [dbo].[Promotion] ([PromotionID])
GO
ALTER TABLE [dbo].[BillSaleDetail] CHECK CONSTRAINT [FK_BillSaleDetail_Promotion]
GO
ALTER TABLE [dbo].[Cart]  WITH CHECK ADD  CONSTRAINT [FK_Cart_Customer] FOREIGN KEY([CustomerID])
REFERENCES [dbo].[Customer] ([CustomerID])
GO
ALTER TABLE [dbo].[Cart] CHECK CONSTRAINT [FK_Cart_Customer]
GO
ALTER TABLE [dbo].[CartDetail]  WITH CHECK ADD  CONSTRAINT [FK__CartDetai__CartI__619B8048] FOREIGN KEY([CartID])
REFERENCES [dbo].[Cart] ([CartID])
GO
ALTER TABLE [dbo].[CartDetail] CHECK CONSTRAINT [FK__CartDetai__CartI__619B8048]
GO
ALTER TABLE [dbo].[CartDetail]  WITH CHECK ADD  CONSTRAINT [FK__CartDetai__Promo__628FA481] FOREIGN KEY([PromotionID])
REFERENCES [dbo].[Promotion] ([PromotionID])
GO
ALTER TABLE [dbo].[CartDetail] CHECK CONSTRAINT [FK__CartDetai__Promo__628FA481]
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
