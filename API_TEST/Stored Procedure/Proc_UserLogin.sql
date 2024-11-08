USE [RentalSystem]
GO
/****** Object:  StoredProcedure [dbo].[Proc_UserLogin]    Script Date: 06-11-2024 10:16:23 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO




ALTER PROCEDURE [dbo].[Proc_UserLogin]
    @Username NVARCHAR(50),
    @Password NVARCHAR(50)
AS
BEGIN
    SELECT 
        Code,
        FirstName,
        MiddleName,
        LastName,
        LoginName,
        LoginPassword,
        MobileNo,
        Email,
        DOB,
        Status
    FROM 
        Users
    WHERE 
        LoginName = @Username AND LoginPassword = @Password
END

