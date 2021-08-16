USE [db_standBy]
GO

/****** Object:  Table [dbo].[__EFMigrationsHistory]    Script Date: 12/11/2019 17:09:23 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[__EFMigrationsHistory](
	[MigrationId] [nvarchar](150) NOT NULL,
	[ProductVersion] [nvarchar](32) NOT NULL
)

GO

/****** Object:  Table [dbo].[Agenda]    Script Date: 12/11/2019 17:09:26 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[Agenda](
	[AgendaId] [int] IDENTITY(1,1) NOT NULL,
	[ClienteId] [int] NOT NULL,
	[valor_bruto] [decimal](18, 2) NOT NULL,
	[valor_liquido] [decimal](18, 2) NOT NULL,
	[taxa] [decimal](18, 2) NOT NULL,
	[data_liquidacao] [datetime2](7) NOT NULL,
	[num_registro] [nvarchar](max) NULL
)

GO

/****** Object:  Table [dbo].[Cliente]    Script Date: 12/11/2019 17:09:26 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[Cliente](
	[ClienteId] [int] IDENTITY(1,1) NOT NULL,
	[razao_social] [nvarchar](max) NULL,
	[data_fundacao] [datetime2](7) NOT NULL,
	[cnpj] [nvarchar](max) NULL,
	[capital] [decimal](18, 2) NOT NULL,
	[quarentena] [bit] NOT NULL,
	[status_cliente] [bit] NOT NULL,
	[classificacao] [nvarchar](max) NULL
)

GO


