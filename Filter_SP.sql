
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
        SELECT a.*, s.Name AS StudioName
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
    @Anime varchar(50) = NULL,
    @Voice varchar(50) = NULL,
    @Offset int = 0
    AS
    BEGIN
        SELECT *
        FROM (
            SELECT c.*, a.Name AS AnimeName , ROW_NUMBER() OVER (PARTITION BY c.ID ORDER BY c.ID) AS RowNum
            FROM Characters AS c
            INNER JOIN Apears_in AS ai ON c.ID = ai.FK_CharacterID
            INNER JOIN Anime AS a ON ai.FK_AnimeID = a.ID
            INNER JOIN Staff AS s ON c.FK_Voice_actor = s.ID
            WHERE (@Name IS NULL OR c.Name LIKE '%' + @Name + '%')
                AND (@Anime IS NULL OR a.Name LIKE '%' + @Anime + '%')
                AND (@Voice IS NULL OR s.Name LIKE '%' + @Voice + '%')
            
        ) AS Subquery
        WHERE RowNum = 1
        ORDER BY ID
        OFFSET @Offset ROWS FETCH NEXT 20 ROWS ONLY;
    END;
GO

CREATE PROCEDURE FilterStudio
    @Name varchar(50) = NULL,
    @EstablishedBefore datetime = NULL,
    @EstablishedAfter datetime = NULL,
    @Offset int = 0
    AS
    BEGIN
        SELECT *
        FROM (
            SELECT s.*
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


