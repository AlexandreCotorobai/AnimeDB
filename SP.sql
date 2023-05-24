CREATE PROCEDURE FilterAnime
    @Name varchar(50) = NULL,
    @MinScore decimal(3, 2) = NULL,
    @Genre varchar(50) = NULL,
    @MinDate date = NULL,
    @MaxDate date = NULL
    AS
    BEGIN
        SELECT *
        FROM Anime AS a
        INNER JOIN Is_genre AS ig ON a.ID = ig.FK_AnimeID
        INNER JOIN Genre AS g ON ig.FK_GenreID = g.ID
        WHERE (@Name IS NULL OR a.Name LIKE '%' + @Name + '%')
        AND (@MinScore IS NULL OR a.Score >= @MinScore)
        AND (@Genre IS NULL OR g.Name = @Genre)
        AND (@MinDate IS NULL OR a.Aired_date >= @MinDate)
        AND (@MaxDate IS NULL OR a.Aired_date <= @MaxDate);
    END;
GO

CREATE PROCEDURE FilterCharacter
    @Name varchar(50) = NULL,
    @Anime varchar(50) = NULL,
    @Voice varchar(50) = NULL
    AS
    BEGIN
        SELECT *
        FROM Characters AS c
        INNER JOIN Apears_in AS ai ON c.ID = ai.FK_CharacterID
        INNER JOIN Anime AS a ON ai.FK_AnimeID = a.ID
        INNER JOIN Staff AS s ON c.FK_Voice_actor = s.ID
        WHERE (@Name IS NULL OR c.Name LIKE '%' + @Name + '%')
        AND (@Anime IS NULL OR a.Name LIKE '%' + @Anime + '%')
        AND (@Voice IS NULL OR s.Name LIKE '%' + @Voice + '%');
    END;
GO

CREATE PROCEDURE FilterStudio
    @Name varchar(50) = NULL,
    @EstablishedBefore datetime = NULL,
    @EstablishedAfter datetime = NULL
    AS
    BEGIN
        SELECT *
        FROM Studio
        WHERE (@Name IS NULL OR Name LIKE '%' + @Name + '%')
        AND (@EstablishedBefore IS NULL OR Established_at < @EstablishedBefore)
        AND (@EstablishedAfter IS NULL OR Established_at > @EstablishedAfter);
    END;
GO

CREATE PROCEDURE FilterStaff
    @Name varchar(50) = NULL,
    @Type varchar(50) = NULL
    AS
    BEGIN
        SELECT *
        FROM Staff
        WHERE (@Name IS NULL OR Name LIKE '%' + @Name + '%')
        AND (@Type IS NULL OR [Type] = @Type);
    END;
GO

CREATE PROCEDURE FilterUser
    @Name varchar(50) = NULL,
    @Sex varchar(10) = NULL,
    @Birthday date = NULL,
    @CreatedAfter datetime = NULL,
    @CreatedBefore datetime = NULL
    AS
    BEGIN
        SELECT *
        FROM [User]
        WHERE (@Name IS NULL OR Name LIKE '%' + @Name + '%')
        AND (@Sex IS NULL OR Sex = @Sex)
        AND (@Birthday IS NULL OR Birthday = @Birthday)
        AND (@CreatedAfter IS NULL OR Created_date > @CreatedAfter)
        AND (@CreatedBefore IS NULL OR Created_date < @CreatedBefore);
    END;
GO

