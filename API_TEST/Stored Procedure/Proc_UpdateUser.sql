USE [RentalSystem]
GO
/****** Object:  StoredProcedure [dbo].[Proc_UpdateUser]    Script Date: 06-11-2024 10:19:11 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO


ALTER PROCEDURE [dbo].[Proc_UpdateUser]
    @Code INT,
    @FirstName NVARCHAR(50),
    @MiddleName NVARCHAR(50),
    @LastName NVARCHAR(50),
    @LoginName NVARCHAR(50),
    @LoginPassword NVARCHAR(50),
    @MobileNo NVARCHAR(15),
    @Email NVARCHAR(100),
    @DOB DATE,
    @Status NVARCHAR(20)
AS
BEGIN
    UPDATE Users
    SET 
        FirstName = @FirstName,
        MiddleName = @MiddleName,
        LastName = @LastName,
        LoginName = @LoginName,
        LoginPassword = @LoginPassword,
        MobileNo = @MobileNo,
        Email = @Email,
        DOB = @DOB,
        Status = @Status
    WHERE Code = @Code
END

