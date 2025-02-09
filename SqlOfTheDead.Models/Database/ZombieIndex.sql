create table [zombieindex]
(
	[Id] uniqueidentifier not null,
	[TableId] uniqueidentifier not null,
	[Name] nvarchar(200) not null,
	[Primary] bit not null,
	[Unique] bit not null,
	[Clustered] bit not null,
	[NonClustered] bit not null,
	[Version] rowversion not null,
	constraint idx_zombieindex_id primary key clustered ([Id]),
	index idx_zombieindex_tableid nonclustered ([TableId]),
);