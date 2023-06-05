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
