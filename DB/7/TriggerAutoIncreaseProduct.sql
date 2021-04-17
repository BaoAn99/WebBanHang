USE QLBHDienThoai 
CREATE TRIGGER autoIncreaseProduct ON OrderInvoiceDetail AFTER  INSERT,UPDATE,DELETE
AS 
BEGIN
	DECLARE @productId INT,@newAmount INT,@oldAmount INT
	IF EXISTS (SELECT * FROM inserted) AND NOT EXISTS (SELECT * FROM deleted)
	BEGIN
		SELECT @productId = ProductID,@newAmount = Amount FROM inserted
		UPDATE dbo.Product SET Amount += @newAmount WHERE ProductID = @productId
	END
	IF EXISTS (SELECT * FROM inserted) AND EXISTS (SELECT * FROM deleted)
	BEGIN 
		SELECT @productId = ProductID,@newAmount = Amount FROM inserted
		SELECT @oldAmount = Amount FROM deleted
		UPDATE dbo.Product SET Amount += (@newAmount-@oldAmount) WHERE ProductID = @productId
	END
	IF NOT EXISTS (SELECT * FROM inserted) AND EXISTS (SELECT * FROM deleted)
	BEGIN
		SELECT @productId = ProductID,@oldAmount = Amount FROM deleted
		UPDATE dbo.Product SET Amount -= (@oldAmount) WHERE ProductID = @productId
	END
END


Create TRIGGER autoIncreaseProductFollowBill ON BillSaleDetail AFTER  INSERT,UPDATE,DELETE
AS 
BEGIN
	DECLARE @productId INT,@newAmount INT,@oldAmount INT,@Status int,@BillID int
	IF EXISTS (SELECT * FROM inserted) AND NOT EXISTS (SELECT * FROM deleted)
	BEGIN
		SELECT @productId = ProductID,@newAmount = Amount FROM inserted
		UPDATE dbo.Product SET Amount -= @newAmount WHERE ProductID = @productId
	END
	IF EXISTS (SELECT * FROM inserted) AND EXISTS (SELECT * FROM deleted)
	BEGIN 
		SELECT @productId = ProductID,@newAmount = Amount,@BillID = BillID FROM inserted
		SELECT @oldAmount = Amount FROM deleted
		SELECT @Status = Status From BillOfSale Where BillID = @BillID
		IF @Status = -1 
		begin
			UPDATE dbo.Product SET Amount += @oldAmount WHERE ProductID = @productId
		end
	END
END


Create TRIGGER autodecreaseProductFollowBill ON BillOfSale AFTER  INSERT,UPDATE,DELETE
AS 
BEGIN
	DECLARE @productId INT,@newAmount INT,@oldAmount INT,@Status int,@BillID int
	IF EXISTS (SELECT * FROM inserted) AND EXISTS (SELECT * FROM deleted)
	BEGIN 
		SELECT @BillID = BillID,@Status = Status FROM inserted
		SELECT @Status From BillOfSale Where BillID = @BillID
		Update BillSaleDetail set BillID = @BillID where BillID = @BillID
	END
END


Update BillSaleDetail set BillID = 1020 where BillID = 1020
