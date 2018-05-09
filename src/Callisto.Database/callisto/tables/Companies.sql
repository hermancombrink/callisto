﻿CREATE TABLE [callisto].[Companies](
	[RefId] [bigint] IDENTITY(1,1) NOT NULL,
	[Id] [uniqueidentifier] NOT NULL,
	[CreatedAt] [datetime] NULL,
	[ModifiedAt] [datetime] NULL,
	[Name] [nvarchar](256) NULL,
	[Description] [nvarchar](512) NULL,
PRIMARY KEY CLUSTERED 
(
	[RefId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO

ALTER TABLE [callisto].[Companies] ADD  DEFAULT (newid()) FOR [Id]
GO

ALTER TABLE [callisto].[Companies] ADD  DEFAULT (getdate()) FOR [CreatedAt]
GO

ALTER TABLE [callisto].[Companies] ADD  DEFAULT (getdate()) FOR [ModifiedAt]
GO

CREATE UNIQUE NONCLUSTERED INDEX [IX_Companies] ON [callisto].[Companies]
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
