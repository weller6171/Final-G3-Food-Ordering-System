CREATE TABLE [dbo].[Shops]
(
	[Id] INT NOT NULL PRIMARY KEY, 
    [ShopName] VARCHAR(50) NOT NULL, 
    [ShopOwner] INT NOT NULL, 
    [ShopAddress] VARCHAR(50) NOT NULL, 
    CONSTRAINT [FK_Shops_ToTable] FOREIGN KEY ([ShopOwner]) REFERENCES [dbo].[Users]([Id])
)
