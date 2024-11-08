USE [RentalSystem]
GO
/****** Object:  StoredProcedure [dbo].[Proc_GetUserByCode]    Script Date: 06-11-2024 10:15:47 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
ALTER PROCEDURE [dbo].[Proc_GetUserByCode]
    @Code INT
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
        Code = @Code
END