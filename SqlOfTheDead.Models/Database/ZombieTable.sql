create table [zombietable]
(
	[Id] uniqueidentifier not null,
	[Name] nvarchar(30) not null,
	[Version] rowversion not null,
	constraint idx_zombietable_id primary key clustered ([Id]),
);