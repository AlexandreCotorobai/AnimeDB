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
        SELECT a.ID, a.Name + ' / ' + a.Alt_Name as Name, a.Score, a.Episodes, a.Aired_date, a.Finished_date, s.Name AS StudioName
            FROM Anime AS a
            JOIN Studio AS s ON a.FK_Studio_ID = s.ID
            WHERE (@Name IS NULL OR a.Name LIKE '%' + @Name + '%')
                AND (@MinScore IS NULL OR a.Score >= @MinScore)
                AND (@MinDate IS NULL OR a.Aired_date >= @MinDate)
                AND (@MaxDate IS NULL OR a.Aired_date <= @MaxDate)
        ) AS Subquery
        ORDER BY ID
        OFFSET @Offset ROWS FETCH NEXT 20 ROWS ONLY;
    END;
GO

-- Fazer table the apears_In nos detalhes
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
            LEFT JOIN Apears_in AS ai ON c.ID = ai.FK_CharacterID
            LEFT JOIN Anime AS a ON ai.FK_AnimeID = a.ID
            INNER JOIN Staff AS s ON c.FK_Voice_actor = s.ID
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
            SELECT u.*
            FROM Users AS u
            WHERE (@Name IS NULL OR u.Name LIKE '%' + @Name + '%')
                AND (@Sex IS NULL OR u.Sex = @Sex)
                AND (@Birthday IS NULL OR u.Birthday = @Birthday)
                AND (@CreatedAfter IS NULL OR u.Created_date > @CreatedAfter)
                AND (@CreatedBefore IS NULL OR u.Created_date < @CreatedBefore)
        ) AS Subquery
        ORDER BY ID
        OFFSET @Offset ROWS FETCH NEXT 20 ROWS ONLY;
    END;
GO


