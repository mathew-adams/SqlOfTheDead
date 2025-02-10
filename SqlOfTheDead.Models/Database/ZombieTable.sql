﻿create table [zombietable]
(
	[Id] uniqueidentifier not null,
	[Name] nvarchar(30) not null,
	[Created] datetime2(7) default '9999-12-31',
	[Version] rowversion not null,
	constraint idx_zombietable_id primary key clustered ([Id]),
);