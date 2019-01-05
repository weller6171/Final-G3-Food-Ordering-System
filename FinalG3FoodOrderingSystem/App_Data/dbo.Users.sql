CREATE TABLE [dbo].[Users] (
    [Id]           INT          IDENTITY (1, 1) NOT NULL,
    [password]     VARCHAR (50) NOT NULL,
    [name]         VARCHAR (50) NOT NULL,
    [emailAddress] VARCHAR (50) NOT NULL,
    [hpno]         INT          NOT NULL,
    [position]     VARCHAR (50) NOT NULL,
    PRIMARY KEY CLUSTERED ([Id] ASC)
);

