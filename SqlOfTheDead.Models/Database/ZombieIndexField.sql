create table [zombieindexfield]
(
	[Id] uniqueidentifier not null,
	[IndexId] uniqueidentifier not null,
	[Name] nvarchar(20) not null,
	[Order] int not null,
	[Version] rowversion not null,
	index idx_zombieindexfield_indexid nonclustered ([IndexId]),
	constraint idx_zombieindexfield_id primary key clustered ([Id]),
);