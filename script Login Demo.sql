USE [Employee]
GO
/****** Object:  Table [dbo].[EmployeeLogin]    Script Date: 02-07-2019 17:29:40 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[EmployeeLogin](
	[Emp_Id] [int] IDENTITY(1,1) NOT NULL,
	[Emp_name] [varchar](50) NOT NULL,
	[Emp_Uname] [varchar](50) NOT NULL,
	[Emp_Email] [varchar](100) NOT NULL,
	[Emp_Password] [varchar](100) NOT NULL,
 CONSTRAINT [PK_EmployeeLogin] PRIMARY KEY CLUSTERED 
(
	[Emp_Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  StoredProcedure [dbo].[SP_Create_Employee]    Script Date: 02-07-2019 17:29:41 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[SP_Create_Employee](
@name varchar(50),
@username varchar(50),
@email varchar(100),
@password varchar(100)
)
AS
BEGIN
declare @LoginId int;
INSERT INTO [dbo].[EmployeeLogin]
           ([Emp_name]
           ,[Emp_Uname]
           ,[Emp_Email]
           ,[Emp_Password])
     VALUES
           (@name,@username,@email,@password);
	set @LoginId =(select SCOPE_IDENTITY() );
	select @LoginId as LoginId;
END
GO
