USE [RentalSystem]
GO
/****** Object:  StoredProcedure [dbo].[Proc_Registration]    Script Date: 06-11-2024 10:18:40 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO



ALTER PROCEDURE [dbo].[Proc_Registration]
    @Code INT,
    @FirstName NVARCHAR(50),
    @MiddleName NVARCHAR(50),
    @LastName NVARCHAR(50),
    @LoginName NVARCHAR(50),
    @LoginPassword NVARCHAR(50), 
    @MobileNo NVARCHAR(15),
    @Email NVARCHAR(100),
    @DOB DATE,
    @Status NVARCHAR(10)
AS
BEGIN

    INSERT INTO Users (Code, FirstName, MiddleName, LastName, LoginName, LoginPassword, MobileNo, Email, DOB, Status)
    VALUES (@Code, @FirstName, @MiddleName, @LastName, @LoginName, @LoginPassword, @MobileNo, @Email, @DOB, @Status)

    SELECT 'true' success, 200 as statusCode, 'User Registered Successfully ...' as Message, Code from dbo.Users where code = @Code
END


