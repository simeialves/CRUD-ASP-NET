CREATE DATABASE db_standBy;

USE db_standBy;

CREATE TABLE [dbo].[clientes](
	[id] [int] NOT NULL primary key IDENTITY(1,1),
	[razao_social] [varchar](250) NULL,
	[cnpj] [varchar](18) NULL,
	[data_fundacao] [datetime] NULL,
	[capital] [decimal](18, 2) NULL,
	[quarentena] [bit] NULL,
	[status_cliente] [bit] NULL,
	[classificacao] [char](1) NULL
)