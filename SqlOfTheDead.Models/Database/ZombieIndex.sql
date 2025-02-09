SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[zombieindex](
	[Id] [uniqueidentifier] NOT NULL,
	[TableId] [uniqueidentifier] NOT NULL,
	[Name] [nvarchar](200) NOT NULL,
	[Primary] [bit] NOT NULL,
	[Unique] [bit] NOT NULL,
	[Clustered] [bit] NOT NULL,
	[NonClustered] [bit] NOT NULL,
 CONSTRAINT [idx_zombieindex_id] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO


