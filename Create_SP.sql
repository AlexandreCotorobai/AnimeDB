
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

