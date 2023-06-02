
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
    @VoiceActor int
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
        (@CharacterID, @Name, @Description, @VoiceActor);

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

