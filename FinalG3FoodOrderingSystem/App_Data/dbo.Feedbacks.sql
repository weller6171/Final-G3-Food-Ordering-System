CREATE TABLE [dbo].[Feedbacks]
(
	[Id] INT NOT NULL PRIMARY KEY IDENTITY, 
    [ShopId] INT NOT NULL,
	[message] VARCHAR(50) NOT NULL, 
    [rating] VARCHAR(50) NOT NULL, 
    CONSTRAINT [FK_Feedbacks_ToShops] FOREIGN KEY ([ShopId]) REFERENCES [Shops]([Id])
    
)
