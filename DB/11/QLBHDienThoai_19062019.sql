use QLBHDienThoai
ALTER TABLE dbo.Receipt drop column 
ALTER TABLE dbo.Receipt DROP PK_Receipt
ALTER TABLE dbo.ReceiptDetail ADD ReceiptDetailID INT PRIMARY KEY IDENTITY(1,1)
alter table Receipt add ReceiptID int pri 
alter table ReceiptDetail drop FK_ReceiptDetail_Receipt
alter table ReceiptDetail add   ReceiptDetailID INT PRIMARY KEY IDENTITY(1,1)

alter table ReceiptDetail add  ReceiptID INT REFERENCES dbo.Receipt(ReceiptID) 