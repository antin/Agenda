CREATE TABLE [dbo].[ITEM2AGENDA] (
    [Id]     INT             IDENTITY (1, 1) NOT NULL,
    [item]   BIGINT             NOT NULL,
	[type]   INT             NOT NULL,
    [agenda] INT             NOT NULL,
    [score]  DECIMAL (18, 2) NOT NULL,
    [volume] DECIMAL (18, 2) NOT NULL,
	[description] NVARCHAR (MAX) NOT NULL,
	[lastUpdate]      DATETIME NOT NULL,
    PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_ITEM2AGENDA] FOREIGN KEY ([agenda]) REFERENCES [dbo].[AGENDA] ([Id]));

