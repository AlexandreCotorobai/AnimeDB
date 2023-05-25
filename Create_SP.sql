
-- EXEC CreateAnime
--     @UserID = 3,
--     @Name = 'Created Anime',
--     @Alt_Name = 'Created Anime Alt Name',
--     @Episodes = 999,
--     @Studio = 'Bones',
--     @AiredDate = '2017-01-01',
--     @FinishedDate = '2017-01-01',
--     @ImageFileName = 'anime.jpg',
--     @Synopsis = 'Created Anime Synopsis';


CREATE PROCEDURE CreateAnime
    @UserID int,
    @Name varchar(100),
    @Alt_Name varchar(100),
    @Episodes int,
    @Studio varchar(100),
    @AiredDate date,
    @FinishedDate date,
    @ImageFileName varchar(100),
    @Synopsis varchar(max)
AS
BEGIN
    DECLARE @IsAdmin bit
    DECLARE @StudioID int
    DECLARE @AnimeID int

    -- Check if the user is an admin
    SELECT @IsAdmin = dbo.IsAdmin(@UserID)

    IF @IsAdmin = 1
    BEGIN
        -- Check if the provided Studio name exists in the Studios table
        IF NOT EXISTS (SELECT 1 FROM Studio WHERE Name = @Studio)
        BEGIN
            PRINT 'Studio does not exist. Creation aborted.'
            RETURN;
        END;

        -- Check if the provided Name already exists in the Anime table
        IF EXISTS (SELECT 1 FROM Anime WHERE Name = @Name)
        BEGIN
            PRINT 'Anime with the provided name already exists. Creation aborted.'
            RETURN;
        END;

        -- Get the Studio ID corresponding to the provided Studio name
        SELECT @StudioID = ID FROM Studio WHERE Name = @Studio;

        
        -- Get biggest ID and increment it by 1
        SELECT @AnimeID = ISNULL(MAX(ID), 0) + 1
        FROM Anime;

        INSERT INTO Anime (ID, Image, Name, Alt_name, Synopsis, Episodes, Score, Aired_date, Finished_date, Season, FK_Studio_ID)
        VALUES (@AnimeID, @ImageFileName, @Name, @Alt_Name, @Synopsis, @Episodes, NULL, @AiredDate, @FinishedDate, NULL, @StudioID);


        PRINT 'Anime created successfully. New ID is: ' + CAST(@AnimeID AS varchar(10))
        
    END
    ELSE
    BEGIN
        PRINT 'Access denied. User is not an admin.'
    END
END;
GO



CREATE PROCEDURE CreateCharacter
    @UserID INT,
    @Name VARCHAR(100),
    @Description VARCHAR(MAX),
    @Image VARCHAR(100),
    @Anime VARCHAR(100),
    @VoiceActor VARCHAR(100)
AS
BEGIN
    DECLARE @IsAdmin BIT
    DECLARE @AnimeID INT
    DECLARE @CharacterID INT
    DECLARE @VoiceActorID INT

    -- Check if the user is an admin
    SELECT @IsAdmin = dbo.IsAdmin(@UserID)

    IF @IsAdmin = 1
    BEGIN
        -- Check if the provided Anime exists in the Anime table
        IF NOT EXISTS (SELECT 1 FROM Anime WHERE Name = @Anime)
        BEGIN
            PRINT 'Anime does not exist. Rolling back transaction.'
            ROLLBACK;
            RETURN;
        END;

        -- Check if the provided VoiceActor exists in the Staff table
        IF NOT EXISTS (SELECT 1 FROM Staff WHERE Name = @VoiceActor)
        BEGIN
            PRINT 'VoiceActor does not exist. Rolling back transaction.'
            ROLLBACK;
            RETURN;
        END;

        -- Check if the provided Character name already exists in the Character table
        IF EXISTS (SELECT 1 FROM Characters WHERE Name = @Name)
        BEGIN
            PRINT 'Character name already exists. Rolling back transaction.'
            ROLLBACK;
            RETURN;
        END;

        -- Get the Anime ID corresponding to the provided Anime name
        SELECT @AnimeID = ID FROM Anime WHERE Name = @Anime;

        -- Calculate the new Character ID
        SELECT @CharacterID = ISNULL(MAX(ID), 0) + 1 FROM Characters;

        -- Get the VoiceActor ID corresponding to the provided VoiceActor name
        SELECT @VoiceActorID = ID FROM Staff WHERE Name = @VoiceActor;

        -- Insert the new character into the Character table
        INSERT INTO Characters (ID, Name, Description, Image, FK_Voice_actor)
        VALUES (@CharacterID, @Name, @Description, @Image, @VoiceActorID);

        -- Insert the relationship between the character and the anime into the Apears_In table
        INSERT INTO Apears_In (FK_CharacterID, FK_AnimeID)
        VALUES (@CharacterID, @AnimeID);

        PRINT 'Character created successfully.'
    END
    ELSE
    BEGIN
        PRINT 'Access denied. User is not an admin.'
    END
END;
GO



CREATE PROCEDURE CreateStudio
    @UserID INT,
    @Name VARCHAR(100),
    @Alt_Name VARCHAR(100),
    @Description VARCHAR(MAX),
    @Image VARCHAR(100),
    @Established_At DATE
    AS
    BEGIN
        DECLARE @IsAdmin BIT
        DECLARE @StudioID INT

        -- Check if the user is an admin
        SELECT @IsAdmin = dbo.IsAdmin(@UserID)

        IF @IsAdmin = 1
        BEGIN
            -- Check if the provided Studio name already exists in the Studios table
            IF EXISTS (SELECT 1 FROM Studio WHERE Name = @Name)
            BEGIN
                PRINT 'Studio name already exists. Rolling back transaction.'
                ROLLBACK;
                RETURN;
            END;

            -- Calculate the index for the new entry
            SELECT @StudioID = ISNULL(MAX(ID), 0) + 1 FROM Studio;

            -- Insert new entry into Studios table
            INSERT INTO Studio (ID, Name, Alt_Name, Description, Image, Established_At)
            VALUES (@StudioID, @Name, @Alt_Name, @Description, @Image, @Established_At);

            PRINT 'Studio created successfully.'
        END
        ELSE
        BEGIN
            PRINT 'Access denied. User is not an admin.'
        END
    END;
GO


CREATE PROCEDURE CreateStaff
    @UserID INT,
    @Name VARCHAR(100),
    @Type VARCHAR(50),
    @Birthday DATE,
    @Image VARCHAR(100)
AS
BEGIN
    DECLARE @IsAdmin BIT
    DECLARE @StaffID INT

    -- Check if the user is an admin
    SELECT @IsAdmin = dbo.IsAdmin(@UserID)

    IF @IsAdmin = 1
    BEGIN
        -- Check if the provided name already exists in the Staff table
        IF EXISTS (SELECT 1 FROM Staff WHERE Name = @Name)
        BEGIN
            PRINT 'Staff name already exists. Rolling back transaction.'
            ROLLBACK;
            RETURN;
        END;

        -- Get the next available Staff ID
        SELECT @StaffID = ISNULL(MAX(ID), 0) + 1 FROM Staff;

        -- Insert the new entry into the Staff table
        INSERT INTO Staff (ID, Name, Type, Birthday, Image)
        VALUES (@StaffID, @Name, @Type, @Birthday, @Image);

        PRINT 'Staff created successfully.'
    END
    ELSE
    BEGIN
        PRINT 'Access denied. User is not an admin.'
    END
END;
GO


CREATE PROCEDURE CreateUser
    @Name VARCHAR(100),
    @Location VARCHAR(100),
    @Sex VARCHAR(10),
    @Birthday DATE,
    @IsAdmin BIT,
    @Image VARCHAR(100)
    AS
    BEGIN

        DECLARE @UserID INT

        -- Check if the provided Name already exists in the Users table
        IF EXISTS (SELECT 1 FROM Users WHERE Name = @Name)
        BEGIN
            PRINT 'User with the provided name already exists.';
            RETURN;
        END;

        -- Get the next available Staff ID
            SELECT @UserID = ISNULL(MAX(ID), 0) + 1 FROM Users;

        -- Insert a new record into the Users table
        INSERT INTO Users (ID, Image, Name, Sex, Created_date, Birthday, Location, Is_admin)
        VALUES (@UserID, @Image, @Name, @Sex, GETDATE(), @Birthday, @Location, @IsAdmin);

        PRINT 'User created successfully.';
    END;
GO

