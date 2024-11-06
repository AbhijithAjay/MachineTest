USE [RentalSystem]
GO

/****** Object:  Table [dbo].[Users]    Script Date: 06-11-2024 10:21:35 PM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[Users](
	[idUser] [int] IDENTITY(1,1) NOT NULL,
	[Code] [int] NOT NULL,
	[FirstName] [nvarchar](25) NOT NULL,
	[MiddleName] [nvarchar](25) NULL,
	[LastName] [nvarchar](25) NULL,
	[LoginName] [nvarchar](25) NOT NULL,
	[LoginPassword] [nvarchar](25) NOT NULL,
	[MobileNo] [nvarchar](25) NOT NULL,
	[Email] [nvarchar](30) NOT NULL,
	[DOB] [datetime] NOT NULL,
	[Status] [nvarchar](20) NOT NULL
) ON [PRIMARY]
GO


