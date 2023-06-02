
CREATE TRIGGER CalculateAnimeScore
    ON Has_Watched
    AFTER INSERT, UPDATE, DELETE
    AS
        DECLARE ID_CURSOR CURSOR FOR SELECT DISTINCT FK_AnimeID FROM INSERTED;
        DECLARE @FK_AnimeID INT;
        DECLARE @avg_score DECIMAL(3, 1)
        OPEN ID_CURSOR;
        FETCH NEXT FROM ID_CURSOR INTO @FK_AnimeID;
        WHILE  @@FETCH_STATUS = 0
        BEGIN
             
            SET @avg_score = CAST(
                (SELECT AVG(Given_score)
                FROM Has_watched
                WHERE FK_AnimeID = @FK_AnimeID)
                AS DECIMAL (3, 1)
                );
            
            UPDATE Anime
            SET Score = @avg_score
            WHERE ID = @FK_AnimeID;
        
            -- SELECT @FK_AnimeID;
            -- SELECT @avg_score;

            FETCH NEXT FROM ID_CURSOR INTO @FK_AnimeID; 


        END;
        CLOSE ID_CURSOR;

GO 


CREATE TRIGGER AddSeason
    ON Anime
    AFTER INSERT, UPDATE
    AS
    BEGIN
        UPDATE Anime
        SET Season = CASE 
            WHEN MONTH(Aired_date) IN (12, 1, 2) THEN 'Winter'
            WHEN MONTH(Aired_date) IN (3, 4, 5) THEN 'Spring'
            WHEN MONTH(Aired_date) IN (6, 7, 8) THEN 'Summer'
            WHEN MONTH(Aired_date) IN (9, 10, 11) THEN 'Fall'
            ELSE 'Unknown'
        END
        WHERE ID IN (SELECT ID FROM inserted);

    END;
GO 



CREATE TRIGGER TR_Anime_Delete
ON Anime
INSTEAD OF DELETE
AS
BEGIN
    SET NOCOUNT ON;

    -- Delete rows from Has_watched table
    DELETE FROM Has_watched
    WHERE FK_AnimeID IN (SELECT ID FROM deleted);

    -- Delete rows from Related_animes table
    DELETE FROM Related_animes
    WHERE FK_AnimeID IN (SELECT ID FROM deleted) OR FK_AnimeID2 IN (SELECT ID FROM deleted);

    -- Delete rows from Comment table
    DELETE FROM Comment
    WHERE FK_AnimeID IN (SELECT ID FROM deleted);

    -- Delete rows from Worked_on table
    DELETE FROM Worked_on
    WHERE FK_AnimeID IN (SELECT ID FROM deleted);

    -- Delete rows from Appears_in table
    DELETE FROM Apears_in
    WHERE FK_AnimeID IN (SELECT ID FROM deleted);

    -- Delete rows from Is_genre table
    DELETE FROM Is_genre
    WHERE FK_AnimeID IN (SELECT ID FROM deleted);

    -- Delete the anime from the Anime table
    DELETE FROM Anime
    WHERE ID IN (SELECT ID FROM deleted);

    PRINT 'Anime removed successfully.';
END;
GO

CREATE TRIGGER TR_Character_Delete
ON Characters
INSTEAD OF DELETE
AS
BEGIN
    SET NOCOUNT ON;

    -- Delete rows from Appears_in table
    DELETE FROM Apears_in
    WHERE FK_CharacterID IN (SELECT ID FROM deleted);

    -- Delete the character from the Characters table
    DELETE FROM Characters
    WHERE ID IN (SELECT ID FROM deleted);

    PRINT 'Character removed successfully.';
END;
GO


CREATE TRIGGER TR_Studio_Delete
ON Studio
INSTEAD OF DELETE
AS
BEGIN
    SET NOCOUNT ON;

    -- Set FK_Studio_ID to NULL for the associated anime records
    UPDATE Anime
    SET FK_Studio_ID = NULL
    WHERE FK_Studio_ID IN (SELECT ID FROM deleted);

    -- Delete the studio from the Studios table
    DELETE FROM Studio
    WHERE ID IN (SELECT ID FROM deleted);

    PRINT 'Studio removed successfully.';
END;
GO

CREATE TRIGGER TR_Staff_Delete
ON Staff
INSTEAD OF DELETE
AS
BEGIN
    SET NOCOUNT ON;

    DECLARE @DeletedStaff TABLE (ID INT);

    -- Insert the deleted staff IDs into a temporary table
    INSERT INTO @DeletedStaff (ID)
    SELECT ID FROM deleted;

    -- Remove entries from Worked_On table for the specified StaffID
    DELETE FROM Worked_On
    WHERE FK_StaffID IN (SELECT ID FROM @DeletedStaff);

    -- Set Character FK_Voice_actor to NULL for the associated staff members
    UPDATE Characters
    SET FK_Voice_actor = NULL
    WHERE FK_Voice_actor IN (SELECT ID FROM @DeletedStaff);

    -- Delete the staff members from the Staff table
    DELETE FROM Staff
    WHERE ID IN (SELECT ID FROM @DeletedStaff);

    PRINT 'Staff member removed successfully.';
END;
GO


CREATE TRIGGER TR_User_Delete
ON Users
INSTEAD OF DELETE
AS
BEGIN
    SET NOCOUNT ON;

    DECLARE @DeletedUsers TABLE (ID INT);

    -- Insert the deleted user IDs into a temporary table
    INSERT INTO @DeletedUsers (ID)
    SELECT ID FROM deleted;

    -- Delete entries from Is_friend table for the specified UserID
    DELETE FROM Is_friend
    WHERE UserID1 IN (SELECT ID FROM @DeletedUsers)
        OR UserID2 IN (SELECT ID FROM @DeletedUsers);

    -- Delete comment entries for the specified UserID
    DELETE FROM Comment
    WHERE FK_UserID IN (SELECT ID FROM @DeletedUsers);

    -- Delete Has_watched entries for the specified UserID
    DELETE FROM Has_watched
    WHERE FK_UserID IN (SELECT ID FROM @DeletedUsers);

    -- Delete the user from the Users table
    DELETE FROM Users
    WHERE ID IN (SELECT ID FROM @DeletedUsers);

    PRINT 'User removed successfully.';
END;
GO
