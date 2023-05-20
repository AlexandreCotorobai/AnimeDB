
CREATE TRIGGER CalculateAnimeScore
    ON Has_Watched
    AFTER INSERT, UPDATE
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
    AFTER INSERT
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