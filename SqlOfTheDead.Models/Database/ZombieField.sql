GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[zombiefield](
	[Id] [uniqueidentifier] NOT NULL,
	[TableId] [uniqueidentifier] NOT NULL,
	[Name] [nvarchar](30) NOT NULL,
	[Type] [nvarchar](20) NOT NULL,
	[Length] [nvarchar](20) NOT NULL,
	[DefaultValue] [nvarchar](20) NOT NULL,
	[AllowNulls] [bit] NOT NULL,
	[IsIdentity] [bit] NOT NULL,
	[Order] [int] NOT NULL,
 CONSTRAINT [idx_zombiefield_id] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO