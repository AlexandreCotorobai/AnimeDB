SET NOCOUNT ON
GO

set nocount    on

USE master

GO


if exists (select * from sysdatabases where name='AnimeDB')
begin
  raiserror('Dropping existing Anime database ....',0,1)
  DROP database AnimeDB
end
GO

CHECKPOINT
go

raiserror('Creating Anime database....',0,1)
go


CREATE DATABASE AnimeDB
GO

USE AnimeDB

GO

CREATE TABLE Anime (
  ID INT PRIMARY KEY,
  Image VARCHAR(255),
  Name VARCHAR(255),
  Alt_name VARCHAR(255),
  Synopsis TEXT,
  Episodes INT,
  Score DECIMAL(3, 1),  -- Dps isto é calculado
  Aired_date DATE,
  Finished_date DATE,
  Season VARCHAR(255), -- Dps isto é calculado
  FK_Studio_ID INT

)

GO

CREATE TABLE Users (
  ID INT PRIMARY KEY,
  Image VARCHAR(255),
  Name VARCHAR(255),
  Sex CHAR(1),
  Created_date DATE,
  Birthday DATE,
  Location VARCHAR(255),
  Is_admin INT
)

GO

CREATE TABLE Is_friend (
  UserID1 INT,
  UserID2 INT,

  PRIMARY KEY (UserID1, UserID2),
  FOREIGN KEY (UserID1) REFERENCES Users(ID),
  FOREIGN KEY (UserID2) REFERENCES Users(ID)
);

GO

CREATE TABLE Has_Watched (
  FK_AnimeID INT,
  FK_UserID INT,
  Given_score DECIMAL(3, 1),
  PRIMARY KEY (FK_AnimeID, FK_UserID),
  FOREIGN KEY (FK_AnimeID) REFERENCES Anime(ID),
)

GO

CREATE TABLE Genre (
  ID INT PRIMARY KEY,
  Name VARCHAR(255)
)

GO

CREATE TABLE Is_genre (
  FK_AnimeID INT,
  FK_GenreID INT,

  PRIMARY KEY (FK_AnimeID, FK_GenreID),
  FOREIGN KEY (FK_AnimeID) REFERENCES Anime(ID),
  FOREIGN KEY (FK_GenreID) REFERENCES Genre(ID)
)

GO

CREATE TABLE Studio (
  ID INT PRIMARY KEY,
  Image VARCHAR(255),
  Name VARCHAR(255),
  Alt_Name VARCHAR(255),
  Description TEXT,
  Established_at DATE
)

GO

ALTER TABLE Anime ADD FOREIGN KEY (FK_Studio_ID) REFERENCES Studio(ID)

GO

CREATE TABLE Staff (
  ID INT PRIMARY KEY,
  Image VARCHAR(255),
  Type VARCHAR(255),
  Name VARCHAR(255),
  Birthday DATE
)

GO

CREATE TABLE Characters (
  ID INT PRIMARY KEY,
  Image VARCHAR(255),
  Name VARCHAR(255),
  Description TEXT,
  FK_Voice_actor INT,

  FOREIGN KEY (FK_Voice_actor) REFERENCES Staff(ID)
)

GO

CREATE TABLE Apears_In (
    FK_AnimeID INT,
    FK_CharacterID INT,

    PRIMARY KEY (FK_AnimeID, FK_CharacterID),
    FOREIGN KEY (FK_AnimeID) REFERENCES Anime(ID),
    FOREIGN KEY (FK_CharacterID) REFERENCES Characters(ID)
)

GO

CREATE TABLE Worked_On (
    FK_AnimeID INT,
    FK_StaffID INT,

    PRIMARY KEY (FK_AnimeID, FK_StaffID),
    FOREIGN KEY (FK_AnimeID) REFERENCES Anime(ID),
    FOREIGN KEY (FK_StaffID) REFERENCES Staff(ID)
)

GO

CREATE TABLE Related_animes (
    FK_AnimeID INT,
    FK_AnimeID2 INT,
    Relation VARCHAR(255),

    PRIMARY KEY (FK_AnimeID, FK_AnimeID2),
    FOREIGN KEY (FK_AnimeID) REFERENCES Anime(ID),
    FOREIGN KEY (FK_AnimeID2) REFERENCES Anime(ID)
)

GO

CREATE TABLE Comment (
  CommentID INT,
  FK_AnimeID INT,
  FK_UserID INT,
  Comment TEXT,
  
  PRIMARY KEY (CommentID, FK_AnimeID),
  FOREIGN KEY (FK_AnimeID) REFERENCES Anime(ID),
  FOREIGN KEY (FK_UserID) REFERENCES Users(ID)
);

GO