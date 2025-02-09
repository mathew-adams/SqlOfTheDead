create table [zombiefield]
(
	[Id] uniqueidentifier not null,
	[TableId] uniqueidentifier not null,
	[Name] nvarchar(30) not null,
	[Type] nvarchar(20) not null,
	[Length] nvarchar(20) not null,
	[DefaultValue] nvarchar(20) not null,
	[AllowNulls] bit not null,
	[IsIdentity] bit not null,
	[Order] int not null,
	[Version] rowversion not null,
	constraint idx_zombiefield_id primary key clustered ([Id]),
	index idx_zombiefield_tableid nonclustered ([TableId]),
);