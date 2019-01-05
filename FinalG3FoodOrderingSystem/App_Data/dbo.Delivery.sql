CREATE TABLE [dbo].[Delivery]
(
	[Id] INT NOT NULL PRIMARY KEY, 
    [OrderId] INT NOT NULL, 
    [CustomerId] INT NOT NULL, 
    [DeliveryStatus] VARCHAR(50) NOT NULL, 
    [DeliverBoyId] INT NOT NULL, 
    CONSTRAINT [FK_Delivery_Users] FOREIGN KEY ([CustomerId]) REFERENCES [Users]([Id]),
	CONSTRAINT [FK_Delivery_Users2] FOREIGN KEY ([DeliverBoyId]) REFERENCES [Users]([Id]),
	CONSTRAINT [FK_Delivery_Orders] FOREIGN KEY ([OrderId]) REFERENCES [Orders]([Id])
)
