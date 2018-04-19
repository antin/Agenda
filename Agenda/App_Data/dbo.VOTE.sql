CREATE TABLE [dbo].[VOTE] (
    [Id]      INT      IDENTITY (1, 1) NOT NULL,
    [meeting] DATETIME NOT NULL,
    [bill]    INT      NULL,
    [mk]      INT      NULL,
    [vote]    INT      NOT NULL,
    [source] INT NULL, 
    PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_VOTE_To_BILL] FOREIGN KEY ([bill]) REFERENCES [dbo].[BILL] ([Id]),
    CONSTRAINT [FK_VOTE_To_MK] FOREIGN KEY ([mk]) REFERENCES [dbo].[MK] ([Id])
);


GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'1-public vote 2-from committee',
    @level0type = N'SCHEMA',
    @level0name = N'dbo',
    @level1type = N'TABLE',
    @level1name = N'VOTE',
    @level2type = N'COLUMN',
    @level2name = N'source'