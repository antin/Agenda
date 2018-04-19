CREATE TABLE [dbo].[BILL] (
    [Id]             INT            IDENTITY (1, 1) NOT NULL,
    [name]           NVARCHAR (MAX) NOT NULL,
    [oknesset_url]   NVARCHAR (MAX) NULL,
    [committee_vote] INT            NULL,
    [date] DATETIME NOT NULL, 
    PRIMARY KEY CLUSTERED ([Id] ASC)
);

