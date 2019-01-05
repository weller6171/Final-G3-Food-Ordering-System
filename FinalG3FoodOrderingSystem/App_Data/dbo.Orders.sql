CREATE TABLE [dbo].[Orders] (
    [Id]         INT          IDENTITY (1, 1) NOT NULL,
    [CustomerId] INT          NOT NULL,
    [FoodId]     INT          NOT NULL,
    [Quantity]   INT          NOT NULL,
    [TotalPrice] FLOAT (53)   NOT NULL,
    [Status]     VARCHAR (50) NOT NULL,
    [ShopId]     INT          NOT NULL,
    [Address] VARCHAR(50) NULL, 
    PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_Orders_ToFoods] FOREIGN KEY ([FoodId]) REFERENCES [dbo].[Foods] ([Id]),
    CONSTRAINT [FK_Orders_ToShops] FOREIGN KEY ([ShopId]) REFERENCES [dbo].[Shops] ([Id]),
    CONSTRAINT [FK_Orders_ToUsers] FOREIGN KEY ([CustomerId]) REFERENCES [dbo].[Users] ([Id])
);

