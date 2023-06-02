
CREATE PROCEDURE RemoveAnime
    @AnimeID INT,
    @UserID INT
AS
BEGIN
    DECLARE @IsAdmin BIT

    -- Check if the user is an admin
    SELECT @IsAdmin = dbo.IsAdmin(@UserID)

    IF @IsAdmin != 1
        BEGIN
        RAISERROR ('Access denied. User is not an admin.', 11,1);
        RETURN;
    END

    -- Delete the anime from the Anime table
    DELETE FROM Anime
    WHERE ID = @AnimeID;

    PRINT 'Anime removed successfully.';
END;
GO

CREATE PROCEDURE RemoveCharacter
    @CharacterID INT,
    @UserID INT
AS
BEGIN
    DECLARE @IsAdmin BIT

    -- Check if the user is an admin
    SELECT @IsAdmin = dbo.IsAdmin(@UserID)

    IF @IsAdmin != 1
        BEGIN
        RAISERROR ('Access denied. User is not an admin.', 11,1);
        RETURN;
    END


    -- Delete the character from the Characters table
    DELETE FROM Characters
    WHERE ID = @CharacterID;

    PRINT 'Character removed successfully.';
END;
GO

CREATE PROCEDURE RemoveStudio
    @StudioID INT,
    @UserID INT
AS
BEGIN
    DECLARE @IsAdmin BIT

    -- Check if the user is an admin
    SELECT @IsAdmin = dbo.IsAdmin(@UserID)

    IF @IsAdmin != 1
        BEGIN
        RAISERROR ('Access denied. User is not an admin.', 11,1);
        RETURN;
    END

    -- Delete the studio from the Studios table
    DELETE FROM Studio
    WHERE ID = @StudioID;

    PRINT 'Studio removed successfully.';
END;
GO

CREATE PROCEDURE RemoveStaff
    @StaffID INT,
    @UserID INT
AS
BEGIN
    DECLARE @IsAdmin BIT

    -- Check if the user is an admin
    SELECT @IsAdmin = dbo.IsAdmin(@UserID)

    IF @IsAdmin != 1
        BEGIN
        RAISERROR ('Access denied. User is not an admin.', 11,1);
        RETURN;
    END

    -- Delete the staff member from the Staff table
    DELETE FROM Staff
    WHERE ID = @StaffID;

    PRINT 'Staff member removed successfully.';
END;
GO

CREATE PROCEDURE RemoveUser
    @UserID INT,
    @AdminID INT
AS
BEGIN
    DECLARE @IsAdmin BIT

    -- Check if the user is an admin
    SELECT @IsAdmin = dbo.IsAdmin(@AdminID)

    IF @IsAdmin != 1
        BEGIN
        RAISERROR ('Access denied. User is not an admin.', 11,1);
        RETURN;
    END

    -- Delete the user from the Users table
    DELETE FROM Users
    WHERE ID = @UserID;

    PRINT 'User removed successfully.';
END;
GO