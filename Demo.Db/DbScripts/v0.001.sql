CREATE TABLE [dbo].[Humans]
(
	[HumanID] INT   IDENTITY (1, 1) PRIMARY KEY,
	[Name]          VARCHAR(50) NOT NULL,
	[Description]   VARCHAR(200)
);

GO