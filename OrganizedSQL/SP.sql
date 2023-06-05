CREATE PROCEDURE FilterAnime
    @Name varchar(50) = NULL,
    @MinScore decimal(3, 2) = NULL,
    @MinDate date = NULL,
    @MaxDate date = NULL,
    @Offset int = 0
    AS
    BEGIN
        SELECT *
        FROM (
        SELECT a.ID, a.Name as Name, a.Score, a.Episodes, a.Aired_date, a.Finished_date, s.Name AS StudioName
            FROM Anime AS a
            LEFT JOIN Studio AS s ON a.FK_Studio_ID = s.ID
            WHERE (@Name IS NULL OR a.Name LIKE '%' + @Name + '%')
                AND (@MinScore IS NULL OR a.Score >= @MinScore)
                AND (@MinDate IS NULL OR a.Aired_date >= @MinDate)
                AND (@MaxDate IS NULL OR a.Aired_date <= @MaxDate)
        ) AS Subquery
        ORDER BY ID
        OFFSET @Offset ROWS FETCH NEXT 20 ROWS ONLY;
    END;
GO

-- Fazer table the appears_In nos detalhes
-- Confia no ROW NUMBER, Its cool
CREATE PROCEDURE FilterCharacter
    @Name varchar(50) = NULL,
    @AnimeID int = NULL,
    @VAID int = NULL,
    @Offset int = 0
    AS
    BEGIN
        SELECT *
        FROM (
            SELECT c.ID as ID, c.Name as Name, a.Name AS AnimeName, s.Name as VA , ROW_NUMBER() OVER (PARTITION BY c.ID ORDER BY c.ID) AS RowNum
            FROM Characters AS c
            LEFT JOIN Appears_in AS ai ON c.ID = ai.FK_CharacterID
            LEFT JOIN Anime AS a ON ai.FK_AnimeID = a.ID
            LEFT JOIN Staff AS s ON c.FK_Voice_actor = s.ID
            WHERE (@Name IS NULL OR c.Name LIKE '%' + @Name + '%')
                AND (@AnimeID IS NULL OR ai.FK_AnimeID = @AnimeID)
                AND (@VAID IS NULL OR c.FK_Voice_actor = @VAID)
            
        ) AS Subquery
        WHERE RowNum = 1
        ORDER BY ID
        OFFSET @Offset ROWS FETCH NEXT 20 ROWS ONLY;
    END;
GO

CREATE PROCEDURE FilterStudio
    @Name varchar(50) = NULL,
    @EstablishedBefore date = NULL,
    @EstablishedAfter date = NULL,
    @Offset int = 0
    AS
    BEGIN
        SELECT *
        FROM (
            SELECT s.ID, s.Name, s.Alt_Name, s.Established_At
            FROM Studio AS s
            WHERE (@Name IS NULL OR s.Name LIKE '%' + @Name + '%')
                AND (@EstablishedBefore IS NULL OR s.Established_at < @EstablishedBefore)
                AND (@EstablishedAfter IS NULL OR s.Established_at > @EstablishedAfter)
        ) AS Subquery
        ORDER BY ID
        OFFSET @Offset ROWS FETCH NEXT 20 ROWS ONLY;
    END;
GO

CREATE PROCEDURE FilterStaff
    @Name varchar(50) = NULL,
    @Type varchar(50) = NULL,
    @Offset int = 0
    AS
    BEGIN
        SELECT *
        FROM (
            SELECT s.*
            FROM Staff AS s
            WHERE (@Name IS NULL OR s.Name LIKE '%' + @Name + '%')
                AND (@Type IS NULL OR s.[Type] = @Type)
        ) AS Subquery
        ORDER BY ID
        OFFSET @Offset ROWS FETCH NEXT 20 ROWS ONLY;
    END;
GO

CREATE PROCEDURE FilterUser
    @Name varchar(50) = NULL,
    @Sex varchar(10) = NULL,
    @Birthday date = NULL,
    @CreatedAfter datetime = NULL,
    @CreatedBefore datetime = NULL,
    @Offset int = 0
    AS
    BEGIN
        SELECT *
        FROM (
            SELECT u.ID, u.Name, u.Sex, u.Created_Date, u.Birthday
            FROM Users AS u
            WHERE (@Name IS NULL OR u.Name LIKE '%' + @Name + '%')
                AND (@Sex IS NULL OR u.Sex = @Sex)
                AND (@Birthday IS NULL OR MONTH(u.Birthday) = MONTH(@Birthday) AND DAY(u.Birthday) = DAY(@Birthday))
                AND (@CreatedAfter IS NULL OR u.Created_date > @CreatedAfter)
                AND (@CreatedBefore IS NULL OR u.Created_date < @CreatedBefore)
        ) AS Subquery
        ORDER BY ID
        OFFSET @Offset ROWS FETCH NEXT 20 ROWS ONLY;
    END;
GO



-- EXEC CreateAnime
--     @UserID = 3,
--     @Name = 'Created Anime',
--     @Alt_Name = 'Created Anime Alt Name',
--     @Episodes = 999,
--     @Studio = 'Bones',
--     @AiredDate = '2017-01-01',
--     @FinishedDate = '2017-01-01',
--     @Synopsis = 'Created Anime Synopsis';


CREATE PROCEDURE CreateAnime
    @UserID int,
    @Name varchar(100),
    @Alt_Name varchar(100),
    @Episodes int,
    @StudioID int,
    @AiredDate date,
    @FinishedDate date,
    @Synopsis varchar(max)
AS
BEGIN
    DECLARE @IsAdmin bit
    DECLARE @AnimeID int

    -- Check if the user is an admin
    SELECT @IsAdmin = dbo.IsAdmin(@UserID)

    IF @IsAdmin != 1
    BEGIN
        RAISERROR('Access denied. User is not an admin.', 11, 1);
        RETURN;
    END;

    -- Check if the provided Name already exists in the Anime table
    IF EXISTS (SELECT 1
    FROM Anime
    WHERE Name = @Name)
    BEGIN
        RAISERROR('Anime with the provided name already exists.', 11, 1);
        RETURN;
    END;

    -- Get biggest ID and increment it by 1
    SELECT @AnimeID = ISNULL(MAX(ID), 0) + 1
    FROM Anime;

    INSERT INTO Anime
        (ID, Name, Alt_name, Synopsis, Episodes, Score, Aired_date, Finished_date, Season, FK_Studio_ID)
    VALUES
        (@AnimeID, @Name, @Alt_Name, @Synopsis, @Episodes, NULL, @AiredDate, @FinishedDate, NULL, @StudioID);


    PRINT 'Anime created successfully. New ID is: ' + CAST(@AnimeID AS varchar(10))

END;
GO



CREATE PROCEDURE CreateCharacter
    @UserID INT,
    @Name VARCHAR(100),
    @Description VARCHAR(MAX),
    @VoiceActorID int
AS
BEGIN
    DECLARE @IsAdmin BIT
    DECLARE @AnimeID INT
    DECLARE @CharacterID INT

    -- Check if the user is an admin
    SELECT @IsAdmin = dbo.IsAdmin(@UserID)

    IF @IsAdmin != 1
    BEGIN
        RAISERROR('Access denied. User is not an admin.',11,1)
        RETURN;
    END;

    -- Check if the provided Character name already exists in the Character table
    IF EXISTS (SELECT 1
    FROM Characters
    WHERE Name = @Name)
        BEGIN
        RAISERROR('Character name already exists. Rolling back transaction.',11,1)
        RETURN;
    END;

    -- Calculate the new Character ID
    SELECT @CharacterID = ISNULL(MAX(ID), 0) + 1
    FROM Characters;

    -- Insert the new character into the Character table
    INSERT INTO Characters
        (ID, Name, Description, FK_Voice_actor)
    VALUES
        (@CharacterID, @Name, @Description, @VoiceActorID);

    PRINT 'Character created successfully.'
END;
GO



CREATE PROCEDURE CreateStudio
    @UserID INT,
    @Name VARCHAR(100),
    @Alt_Name VARCHAR(100),
    @Description VARCHAR(MAX),
    @Established_At DATE
AS
BEGIN
    DECLARE @IsAdmin BIT
    DECLARE @StudioID INT

    -- Check if the user is an admin
    SELECT @IsAdmin = dbo.IsAdmin(@UserID)

    IF @IsAdmin != 1
    BEGIN
        RAISERROR('Access denied. User is not an admin.',11,1)
        RETURN;
    END;

    -- Check if the provided Studio name already exists in the Studios table
    IF EXISTS (SELECT 1
    FROM Studio
    WHERE Name = @Name)
            BEGIN
        PRINT 'Studio name already exists. Rolling back transaction.'
        ROLLBACK;
        RETURN;
    END;

    -- Calculate the index for the new entry
    SELECT @StudioID = ISNULL(MAX(ID), 0) + 1
    FROM Studio;

    -- Insert new entry into Studios table
    INSERT INTO Studio
        (ID, Name, Alt_Name, Description, Established_At)
    VALUES
        (@StudioID, @Name, @Alt_Name, @Description, @Established_At);

    PRINT 'Studio created successfully.'

END;
GO


CREATE PROCEDURE CreateStaff
    @UserID INT,
    @Name VARCHAR(100),
    @Type VARCHAR(50),
    @Birthday DATE
AS
BEGIN
    DECLARE @IsAdmin BIT
    DECLARE @StaffID INT

    -- Check if the user is an admin
    SELECT @IsAdmin = dbo.IsAdmin(@UserID)

    IF @IsAdmin != 1
    BEGIN
        RAISERROR('Access denied. User is not an admin.',11,1)
        RETURN;
    END;
    -- Check if the provided name already exists in the Staff table
    IF EXISTS (SELECT 1
    FROM Staff
    WHERE Name = @Name)
        BEGIN
        PRINT 'Staff name already exists. Rolling back transaction.'
        ROLLBACK;
        RETURN;
    END;

    -- Get the next available Staff ID
    SELECT @StaffID = ISNULL(MAX(ID), 0) + 1
    FROM Staff;

    -- Insert the new entry into the Staff table
    INSERT INTO Staff
        (ID, Name, Type, Birthday)
    VALUES
        (@StaffID, @Name, @Type, @Birthday);

    PRINT 'Staff created successfully.'
END;
GO


CREATE PROCEDURE CreateUser
    @Name VARCHAR(100),
    @Location VARCHAR(100),
    @Sex VARCHAR(10),
    @Birthday DATE,
    @IsAdmin BIT
AS
BEGIN

    DECLARE @UserID INT

    -- Check if the provided Name already exists in the Users table
    IF EXISTS (SELECT 1
    FROM Users
    WHERE Name = @Name)
        BEGIN
        PRINT 'User with the provided name already exists.';
        RETURN;
    END;

    -- Get the next available User ID
    SELECT @UserID = ISNULL(MAX(ID), 0) + 1
    FROM Users;

    -- Insert a new record into the Users table
    INSERT INTO Users
        (ID, Name, Sex, Created_date, Birthday, Location, Is_admin)
    VALUES
        (@UserID, @Name, @Sex, GETDATE(), @Birthday, @Location, @IsAdmin);

    PRINT 'User created successfully.';
END;
GO



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
--     @VoiceActorID = 'Yuki Kaji',
--     @Anime = 'Created Anime';

-- Preciso Checkar se o type de Staff é Voice Actor
CREATE PROCEDURE UpdateCharacter
    @CharacterID INT,
    @UserID INT,
    @Name VARCHAR(100) = NULL,
    @Description VARCHAR(MAX) = NULL,
    @VoiceActorID int = NULL
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

    IF @IsAdmin != 1
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

GO

CREATE PROCEDURE GetAnimeGenres
    @AnimeID INT
    AS
    BEGIN
        -- Retrieve the genres associated with the given AnimeID
        SELECT g.Name, g.ID
        FROM Is_Genre AS ig
        INNER JOIN Genre AS g ON ig.FK_GenreID = g.ID
        WHERE ig.FK_AnimeID = @AnimeID;

    END;
GO

CREATE PROCEDURE GetComments
    @AnimeID INT
    AS
    BEGIN
        -- Retrieve the comments associated with the given AnimeID
        SELECT c.CommentID, u.Name, c.Comment
        FROM Comment AS c
        INNER JOIN Users AS u ON c.FK_UserID = u.ID
        WHERE c.FK_AnimeID = @AnimeID;

    END;
GO

CREATE PROCEDURE GetRelatedAnimes
    @AnimeID INT
AS
BEGIN
    -- Retrieve the related animes for the given AnimeID
    SELECT ra.FK_AnimeID2, ra.Relation, a2.Name AS AnimeName
    FROM Related_Animes AS ra
    INNER JOIN Anime AS a2 ON ra.FK_AnimeID2 = a2.ID
    WHERE ra.FK_AnimeID = @AnimeID;

END;
GO


CREATE PROCEDURE GetCharacterAnimes
    @CharacterID INT
AS
BEGIN
    SELECT A.FK_CharacterID AS CharacterID, A.FK_AnimeID AS AnimeID, AN.Name AS AnimeName
    FROM Appears_In A
    JOIN Anime AN ON A.FK_AnimeID = AN.ID
    WHERE A.FK_CharacterID = @CharacterID;
END;
GO


CREATE PROCEDURE GetStaffAnimes
    @StaffID INT
AS
BEGIN
    SELECT WO.FK_StaffID AS StaffID, WO.FK_AnimeID AS AnimeID, AN.Name AS AnimeName
    FROM Worked_On WO
    JOIN Anime AN ON WO.FK_AnimeID = AN.ID
    WHERE WO.FK_StaffID = @StaffID;
END;
GO

CREATE PROCEDURE GetFriends
    @UserID INT
AS
BEGIN
    SELECT F.UserID2 AS FriendID, U.Name AS FriendName
    FROM Is_friend F
    JOIN Users U ON F.UserID2 = U.ID
    WHERE F.UserID1 = @UserID;
END;
GO


CREATE PROCEDURE GetWatchedAnimes
    @UserID INT
AS
BEGIN
    SELECT H.FK_UserID AS UserID, H.FK_AnimeID AS AnimeID, A.Name AS AnimeName, H.Given_Score
    FROM Has_watched H
    JOIN Anime A ON H.FK_AnimeID = A.ID
    WHERE H.FK_UserID = @UserID;
END;
GO

CREATE PROCEDURE GetAnime
    @AnimeID INT
AS
BEGIN
    SELECT A.ID, A.Name, A.Alt_Name, A.Synopsis, A.Score, A.Aired_date, A.Finished_date, A.Episodes, A.Season, S.Name AS StudioName
    FROM Anime A
    JOIN Studio S ON A.FK_Studio_ID = S.ID
    WHERE A.ID = @AnimeID;
END;
GO

CREATE PROCEDURE GetCharacter
    @CharacterID INT
AS
BEGIN
    SELECT C.ID, C.Name, C.Description, S.Name AS VA, S.ID AS VAID
    FROM Characters C
    JOIN Staff S ON C.FK_Voice_actor = S.ID
    WHERE C.ID = @CharacterID;
END;
GO
