USE [RentalSystem]
GO
/****** Object:  StoredProcedure [dbo].[Proc_GetEmployees]    Script Date: 06-11-2024 10:16:47 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO




ALTER PROCEDURE [dbo].[Proc_GetUsers]
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
END

