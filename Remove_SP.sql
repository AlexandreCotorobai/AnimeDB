
CREATE PROCEDURE RemoveAnime
    @AnimeID INT
AS
BEGIN
    -- Delete rows from Has_watched table
    DELETE FROM Has_watched
    WHERE FK_AnimeID = @AnimeID;

    -- Delete rows from Related_animes table
    DELETE FROM Related_animes
    WHERE FK_AnimeID = @AnimeID OR FK_AnimeID2 = @AnimeID;

    -- Delete rows from Comment table
    DELETE FROM Comment
    WHERE FK_AnimeID = @AnimeID;

    -- Delete rows from Worked_on table
    DELETE FROM Worked_on
    WHERE FK_AnimeID = @AnimeID;

    -- Delete rows from Apears_In table
    DELETE FROM Apears_In
    WHERE FK_AnimeID = @AnimeID;

    -- Delete rows from Is_genre table
    DELETE FROM Is_genre
    WHERE FK_AnimeID = @AnimeID;

    -- Delete the anime from the Anime table
    DELETE FROM Anime
    WHERE ID = @AnimeID;

    PRINT 'Anime removed successfully.';
END;
GO

CREATE PROCEDURE RemoveCharacter
    @CharacterID INT
AS
BEGIN
    -- Delete rows from Apears_In table
    DELETE FROM Apears_In
    WHERE FK_CharacterID = @CharacterID;

    -- Delete the character from the Characters table
    DELETE FROM Characters
    WHERE ID = @CharacterID;

    PRINT 'Character removed successfully.';
END;
GO

CREATE PROCEDURE RemoveStudio
    @StudioID INT
AS
BEGIN
    -- Set FK_Studio_ID to NULL for the associated anime records
    UPDATE Anime
    SET FK_Studio_ID = NULL
    WHERE FK_Studio_ID = @StudioID;

    -- Delete the studio from the Studios table
    DELETE FROM Studio
    WHERE ID = @StudioID;

    PRINT 'Studio removed successfully.';
END;
GO

CREATE PROCEDURE RemoveStaff
    @StaffID INT
AS
BEGIN
    -- Remove entries from Worked_On table for the specified StaffID
    DELETE FROM Worked_On
    WHERE FK_StaffID = @StaffID;

    -- Set Character FK_Voice_actor to NULL for the associated staff member
    UPDATE Characters
    SET FK_Voice_actor = NULL
    WHERE FK_Voice_actor = @StaffID;

    -- Delete the staff member from the Staff table
    DELETE FROM Staff
    WHERE ID = @StaffID;

    PRINT 'Staff member removed successfully.';
END;
GO

CREATE PROCEDURE RemoveUser
    @UserID INT
AS
BEGIN
    -- Delete entries from Is_friend table for the specified UserID
    DELETE FROM Is_friend
    WHERE UserID1 = @UserID OR UserID2 = @UserID;

    -- Delete comment entries for the specified UserID
    DELETE FROM Comment
    WHERE FK_UserID = @UserID;

    -- Delete Has_watched entries for the specified UserID
    DELETE FROM Has_watched
    WHERE FK_UserID = @UserID;

    -- Delete the user from the Users table
    DELETE FROM Users
    WHERE ID = @UserID;

    PRINT 'User removed successfully.';
END;
GO