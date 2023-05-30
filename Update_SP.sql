
----------------
-- UPDATE SPs --
----------------

-----------
-- USAGE --
-----------

-- DECLARE @AnimeID int = 1
-- DECLARE @UserID int = 3
-- DECLARE @Name varchar(100) = 'Updated Anime Name'
-- DECLARE @Alt_Name varchar(100) = 'Updated Alt Name'
-- DECLARE @AiredDate date = '2023-01-01'
-- DECLARE @FinishedDate date = '2023-03-31'
-- DECLARE @Episodes int = 12
-- DECLARE @Synopsis varchar(max) = 'Updated anime synopsis'
-- DECLARE @Studio varchar(100) = 'Bones'

-- EXEC UpdateAnime
--     @AnimeID = @AnimeID,
--     @UserID = @UserID,
--     @Name = @Name,
--     @Alt_Name = @Alt_Name,
--     @AiredDate = @AiredDate,
--     @FinishedDate = @FinishedDate,
--     @Episodes = @Episodes,
--     @Synopsis = @Synopsis,
--     @Studio = @Studio;


CREATE PROCEDURE UpdateAnime
    @AnimeID int,
    @UserID int,
    @Name varchar(100) = NULL,
    @Alt_Name varchar(100) = NULL,
    @AiredDate date = NULL,
    @FinishedDate date = NULL,
    @Episodes int = NULL,
    @Synopsis varchar(max) = NULL,
    @StudioID int
AS
BEGIN
    DECLARE @IsAdmin bit

    -- Check if the user is an admin
    SELECT @IsAdmin = dbo.IsAdmin(@UserID)

    IF @IsAdmin != 1
    BEGIN
        RAISERROR ('Access denied. User is not an admin.', 11,1);
    END;

    -- check if name already exists
    IF EXISTS (SELECT 1
    FROM Anime
    WHERE Name = @Name AND ID <> @AnimeID)
    BEGIN
        RAISERROR ('Anime name already exists. Rolling back transaction.', 11,1);
        RETURN;
    END;

    -- Update Anime table based on the provided Anime ID
    UPDATE Anime
    SET
        Name = ISNULL(@Name, Name),
        Alt_Name = ISNULL(@Alt_Name, Alt_Name),
        Aired_date = ISNULL(@AiredDate, Aired_date),
        Finished_date = ISNULL(@FinishedDate, Finished_date),
        Episodes = ISNULL(@Episodes, Episodes),
        Synopsis = ISNULL(@Synopsis, Synopsis),
        FK_Studio_ID = @StudioID
    WHERE ID = @AnimeID;
    PRINT 'Anime updated successfully.'

END;
GO







-- EXEC UpdateCharacter
--     @CharacterID = 1,
--     @UserID = 3,
--     @Name = 'New Character Name',
--     @Description = 'Updated character description',
--     @VoiceActor = 'Yuki Kaji',
--     @Anime = 'Created Anime';

-- Preciso Checkar se o type de Staff é Voice Actor
CREATE PROCEDURE UpdateCharacter
    @CharacterID INT,
    @UserID INT,
    @Name VARCHAR(100) = NULL,
    @Description VARCHAR(MAX) = NULL,
    @VoiceActor int = NULL
AS
BEGIN
    DECLARE @IsAdmin BIT
    DECLARE @AnimeID INT

    -- Check if the user is an admin
    SELECT @IsAdmin = dbo.IsAdmin(@UserID)

    IF @IsAdmin != 1
        BEGIN
        RAISERROR ('Access denied. User is not an admin.', 11,1);
        RETURN;
    END
    -- check if name already exists
    IF EXISTS (SELECT 1
    FROM Characters
    WHERE Name = @Name AND ID <> @CharacterID)
        BEGIN
        RAISERROR ('Character name already exists. Rolling back transaction.', 11,1);
        RETURN;
    END;

    -- Update Character table based on the provided Character ID


    UPDATE Characters
            SET
                Name = ISNULL(@Name, Name),
                Description = ISNULL(@Description, Description),
                FK_Voice_actor = @VoiceActorID
            WHERE ID = @CharacterID;

    PRINT 'Character updated successfully.'

END;
GO



CREATE PROCEDURE UpdateStudio
    @StudioID INT,
    @UserID INT,
    @Name VARCHAR(100) = NULL,
    @Description VARCHAR(MAX) = NULL,
    @Alt_Name VARCHAR(100) = NULL,
    @Established_at DATE = NULL
AS
BEGIN
    DECLARE @IsAdmin BIT

    -- Check if the user is an admin
    SELECT @IsAdmin = dbo.IsAdmin(@UserID)

    IF @IsAdmin = 1
    BEGIN
        RAISERROR ('Access denied. User is not an admin.', 11,1);
        RETURN;
    END;
    -- Check if the provided Studio name already exists in the Studios table
    IF EXISTS (SELECT 1
    FROM Studio
    WHERE Name = @Name AND ID <> @StudioID)
        BEGIN
        PRINT 'Studio name already exists. Rolling back transaction.'
        ROLLBACK;
        RETURN;
    END;

    -- Update Studios table based on the provided Studio ID
    UPDATE Studio
        SET
            Name = ISNULL(@Name, Name),
            Description = ISNULL(@Description, Description),
            Alt_Name = ISNULL(@Alt_Name, Alt_Name),
            Established_at = ISNULL(@Established_at, Established_at)
        WHERE ID = @StudioID;

    PRINT 'Studio updated successfully.'
END;
GO


CREATE PROCEDURE UpdateStaff
    @StaffID INT,
    @UserID INT,
    @Name VARCHAR(100) = NULL,
    @Type VARCHAR(50) = NULL,
    @Birthday DATE = NULL
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


    -- Check if the provided Staff name already exists in the Staff table
    IF EXISTS (SELECT 1
    FROM Staff
    WHERE Name = @Name AND ID <> @StaffID)
            BEGIN
        RAISERROR ('Staff name already exists. Rolling back transaction.', 11,1);
        ROLLBACK;
        RETURN;
    END;

    -- Update Staff table based on the provided Staff ID
    UPDATE Staff
            SET
                Name = ISNULL(@Name, Name),
                [Type] = ISNULL(@Type, [Type]),
                Birthday = ISNULL(@Birthday, Birthday)
            WHERE ID = @StaffID;

    PRINT 'Staff updated successfully.'

END;
GO


CREATE PROCEDURE UpdateUser
    @UserID INT,
    @Name VARCHAR(100) = NULL,
    @Location VARCHAR(100) = NULL,
    @Birthday DATE = NULL,
    @Sex VARCHAR(10) = NULL
AS
BEGIN
    -- Update Users table based on the provided User ID
    UPDATE Users
        SET
            Name = ISNULL(@Name, Name),
            Location = ISNULL(@Location, Location),
            Birthday = ISNULL(@Birthday, Birthday),
            Sex = ISNULL(@Sex, Sex)
        WHERE ID = @UserID;

    PRINT 'User updated successfully.'
END;
GO

CREATE PROCEDURE AddAnimeGenre
    @AnimeID INT,
    @UserID INT,
    @GenreID INT
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

    -- Check if the provided Anime ID and Genre ID already exists in the Anime_Genres table
    IF EXISTS (SELECT 1
    FROM Is_Genre
    WHERE FK_AnimeID = @AnimeID AND FK_GenreID = @GenreID)
        BEGIN
        PRINT 'Anime and Genre already exist. Rolling back transaction.'
        ROLLBACK;
        RETURN;
    END;

    -- Insert Anime_Genres table based on the provided Anime ID and Genre ID
    INSERT INTO Is_Genre
        (FK_AnimeID, FK_GenreID)
    VALUES
        (@AnimeID, @GenreID);
    PRINT 'Anime_Genres updated successfully.'
END;
GO

CREATE PROCEDURE RemoveAnimeGenre
    @AnimeID INT,
    @UserID INT,
    @GenreID INT
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

    -- Delete Anime_Genres table based on the provided Anime ID and Genre ID
    DELETE FROM Is_Genre
        WHERE FK_AnimeID = @AnimeID AND FK_GenreID = @GenreID;
    PRINT 'Anime_Genres updated successfully.'
END;
GO

CREATE PROCEDURE AddAnimeRelation
    @AnimeID INT,
    @UserID INT,
    @RelatedAnimeID INT,
    @RelationType VARCHAR(50)
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

    IF @AnimeID = @RelatedAnimeID
        BEGIN
        RAISERROR('Anime and Related Anime are the same. Rolling back transaction.', 11,1);
        RETURN;
    END;

    -- Check if the provided Anime ID and Related Anime ID already exists in the Anime_Relations table

    IF EXISTS (SELECT 1
    FROM Related_animes
    WHERE FK_AnimeID = @AnimeID AND FK_AnimeID2 = @RelatedAnimeID)
        BEGIN
        RAISERROR ( 'Anime and Related Anime already exist. Rolling back transaction.', 11,1)
        RETURN;
    END;

    -- Insert Anime_Relations table based on the provided Anime ID and Related Anime ID
    INSERT INTO Related_animes
        (FK_AnimeID, FK_AnimeID2, Relation)
    VALUES
        (@AnimeID, @RelatedAnimeID, @RelationType);
    PRINT 'Anime_Relations updated successfully.'
END;
GO

CREATE PROCEDURE RemoveAnimeRelation
    @AnimeID INT,
    @UserID INT,
    @RelatedAnimeID INT
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

    -- Delete Anime_Relations table based on the provided Anime ID and Related Anime ID
    DELETE FROM Related_animes
        WHERE FK_AnimeID = @AnimeID AND FK_AnimeID2 = @RelatedAnimeID;
    PRINT 'Anime_Relations updated successfully.'
END;
GO

CREATE PROCEDURE CreateComment
    @UserID INT,
    @AnimeID INT,
    @Comment VARCHAR(MAX)
AS
BEGIN
    DECLARE @CommentID INT

    -- Calculate the new CommentID
    SELECT @CommentID = ISNULL(MAX(CommentID), 0) + 1
    FROM Comment;

    -- Insert the new comment into the Comment table
    INSERT INTO Comment
        (CommentID, FK_AnimeID, FK_UserID, Comment)
    VALUES
        (@CommentID, @AnimeID, @UserID, @Comment);

    PRINT 'Comment created successfully.';
END;
GO

CREATE PROCEDURE RemoveComment
    @UserID INT,
    @CommentID INT
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

    -- Delete Comment table based on the provided Comment ID
    DELETE FROM Comment
        WHERE CommentID = @CommentID;
    PRINT 'Comment deleted successfully.'
END;
GO

CREATE PROCEDURE AddCharacterAppearsIn
    @UserID INT,
    @CharacterID INT,
    @AnimeID INT
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

    -- Check if the provided Character ID and Anime ID already exists in the Appears_in table
    IF EXISTS (SELECT 1
    FROM Appears_in
    WHERE FK_CharacterID = @CharacterID AND FK_AnimeID = @AnimeID)
        BEGIN
        RAISERROR ('Character and Anime already exist. Rolling back transaction.', 11,1);
        RETURN;
    END;

    -- Insert Appears_in table based on the provided Character ID and Anime ID
    INSERT INTO Appears_in
        (FK_CharacterID, FK_AnimeID)
    VALUES
        (@CharacterID, @AnimeID);
    PRINT 'Appears_in updated successfully.'
END;

GO

CREATE PROCEDURE RemoveCharacterAppearsIn
    @UserID INT,
    @CharacterID INT,
    @AnimeID INT
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

    -- Delete Appears_in table based on the provided Character ID and Anime ID
    DELETE FROM Appears_in
        WHERE FK_CharacterID = @CharacterID AND FK_AnimeID = @AnimeID;
    PRINT 'Appears_in updated successfully.'
END;