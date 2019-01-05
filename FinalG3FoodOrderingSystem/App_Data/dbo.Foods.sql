CREATE TABLE [dbo].[Foods]
(
	[Id] INT NOT NULL PRIMARY KEY IDENTITY, 
    [FoodName] VARCHAR(50) NOT NULL, 
    [FoodPic] IMAGE NULL, 
    [FoodCategory] VARCHAR(50) NOT NULL, 
    [Price] FLOAT NOT NULL, 
    [ShopId] INT NOT NULL, 
    [Quantity] INT NOT NULL, 
    CONSTRAINT [FK_Table_ToShops] FOREIGN KEY ([ShopId]) REFERENCES [Shops]([Id])
)
