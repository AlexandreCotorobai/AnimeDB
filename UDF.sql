-------------------
-- USAGE EXAMPLE --
-------------------
-- DECLARE @IsAdmin bit
-- SELECT @IsAdmin = dbo.IsAdmin(3)

-- IF @IsAdmin = 1
--     PRINT 'User is an admin'
-- ELSE
--     PRINT 'User is not an admin'


CREATE FUNCTION IsAdmin(@UserID int)
RETURNS bit
AS
BEGIN
    DECLARE @IsAdmin bit

    SELECT @IsAdmin = Is_admin
    FROM [User]
    WHERE ID = @UserID

    RETURN @IsAdmin
END;
GO
