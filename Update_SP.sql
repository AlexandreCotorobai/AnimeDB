
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
    @Studio varchar(100) = NULL
AS
BEGIN
    DECLARE @IsAdmin bit
    DECLARE @StudioID int

    -- Check if the user is an admin
    SELECT @IsAdmin = dbo.IsAdmin(@UserID)

    IF @IsAdmin = 1
    BEGIN
        -- Check if the provided Studio name exists in the Studios table
        IF NOT EXISTS (SELECT 1 FROM Studio WHERE Name = @Studio)
        BEGIN
            PRINT 'Studio does not exist. Rolling back transaction.'
            ROLLBACK;
            RETURN;
        END;

        -- Get the Studio ID corresponding to the provided Studio name
        SELECT @StudioID = ID FROM Studio WHERE Name = @Studio;

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
    END
    ELSE
    BEGIN
        PRINT 'Access denied. User is not an admin.'
        ROLLBACK;
        RETURN; -- SE DER ERRO EM ALGUM LUGAR TLVZ SEJA DAQUI, FAVOR CHAMAR MK
    END
END;
GO
