USE AnimeDB

GO

INSERT INTO Users (ID, Image, Name, Sex, Created_date, Birthday, Location, Is_admin, Hashed_password)
VALUES
(1, 'user1.jpg', 'John Doe', 'M', '2022-01-15', '1990-05-10', 'New York', 0, 'hashedpass1'),
(2, 'user2.jpg', 'Jane Smith', 'F', '2022-03-20', '1995-09-22', 'London', 0, 'hashedpass2'),
(3, 'user3.jpg', 'Alex Johnson', 'M', '2022-06-05', '1985-12-02', 'Tokyo', 1, 'hashedpass3');

GO

INSERT INTO Studio (ID, Image, Name, Alt_Name, Description, Established_at)
VALUES
(1, 'studio1.jpg', 'Studio Pierrot', 'Pierrot Co. Ltd.', 'A renowned anime studio known for Naruto and Bleach.', '1979-09-20'),
(2, 'studio2.jpg', 'WIT Studio', 'Wit Studio Inc.', 'Notable for producing Attack on Titan.', '2012-06-01'),
(3, 'studio3.jpg', 'Bones', NULL, 'Known for My Hero Academia and Fullmetal Alchemist.', '1998-10-28');

GO

INSERT INTO Anime (ID, Image, Name, Alt_name, Synopsis, Episodes, Score, Aired_date, Finished_date, Season, FK_Studio_ID)
VALUES
(1, 'anime1.jpg', 'Naruto', 'Naruto: Shippuden', 'A story about a young ninja named Naruto.', 500, NULL, '2002-10-03', '2007-02-08', NULL, 1),
(2, 'anime2.jpg', 'Attack on Titan', 'Shingeki no Kyojin', 'Humans fight against giant humanoid creatures called Titans.', 75, NULL, '2013-04-07', '2021-03-28', NULL, 2),
(3, 'anime3.jpg', 'My Hero Academia', 'Boku no Hero Academia', 'A boy with no powers in a world of superheroes.', 100, NULL, '2016-04-03', NULL, NULL, 3),
(4, 'anime4.jpg', 'Attack on Titan: Season 2', 'Shingeki no Kyojin Season 2', 'Continuation of the battle against Titans.', 12, NULL, '2017-04-01', '2017-06-17', NULL, 2),
(5, 'anime5.jpg', 'Attack on Titan: Season 3', 'Shingeki no Kyojin Season 3', 'The uncovering of dark secrets within the walls.', 24, NULL, '2018-07-22', '2019-07-01', NULL, 2),
(6, 'anime6.jpg', 'Attack on Titan: Season 4', 'Shingeki no Kyojin Season 4', 'The final battle against Marley.', 16, NULL, '2020-12-07', '2021-03-29', NULL, 2);

GO

INSERT INTO Staff (ID, Image, Type, Name, Birthday)
VALUES
(1, 'staff1.jpg', 'Director', 'Hayao Miyazaki', '1941-01-05'),
(2, 'staff2.jpg', 'Animator', 'Yoshitaka Amano', '1952-03-26'),
(3, 'staff3.jpg', 'Writer', 'Gen Urobuchi', '1972-12-20'),
(4, 'staff4.jpg', 'Voice Actor', 'Maaya Sakamoto', '1980-03-31'),
(5, 'staff5.jpg', 'Voice Actor', 'Yuki Kaji', '1985-09-03');

GO

INSERT INTO Genre (ID, Name)
VALUES
(1, 'Action'),
(2, 'Drama'),
(3, 'Superpower'),
(4, 'Adventure'),
(5, 'Fantasy');

GO

INSERT INTO Is_genre (FK_AnimeID, FK_GenreID)
VALUES
(1, 1),
(1, 4),
(2, 2),
(2, 5),
(3, 3),
(3, 5);

GO


INSERT INTO Comment (CommentID, FK_AnimeID, FK_UserID, Comment)
VALUES
(1, 1, 2, 'Naruto is an amazing anime series with great characters and epic battles.'),
(2, 2, 1, 'Attack on Titan kept me on the edge of my seat with its intense storyline.'),
(3, 3, 3, 'My Hero Academia is an inspiring anime that portrays the journey of becoming a hero.');

GO


INSERT INTO Has_Watched (FK_AnimeID, FK_UserID, Given_score)
VALUES
(1, 1, 8),
(2, 1, 9),
(3, 1, 7),
(3, 2, 6)

GO

INSERT INTO Characters (ID, Image, Name, Description, FK_Voice_actor)
VALUES
(1, 'character1.jpg', 'Naruto Uzumaki', 'The main protagonist of Naruto series.', 4),
(2, 'character2.jpg', 'Eren Yeager', 'The main protagonist of Attack on Titan series.', 5),
(3, 'character3.jpg', 'Izuku Midoriya', 'The main protagonist of My Hero Academia series.', 5),
(4, 'character4.jpg', 'Sasuke Uchiha', 'A skilled ninja and rival of Naruto.', 4);

GO

INSERT INTO Is_friend (UserID1, UserID2)
VALUES
(1, 2),
(2, 1),
(2, 3),
(3, 2);

GO

INSERT INTO Related_animes (FK_AnimeID, FK_AnimeID2, Relation)
VALUES
(2, 4, 'Sequel'),
(4, 5, 'Sequel'),
(5, 6, 'Sequel');

GO

INSERT INTO Apears_In (FK_CharacterID, FK_AnimeID)
VALUES
(1, 1),
(2, 2),
(2, 4),
(2, 5),
(2, 6),
(3, 3);

GO

INSERT INTO Worked_On (FK_AnimeID, FK_StaffID)
VALUES
(1, 1),
(1, 2),
(2, 2),
(3, 3),
(4, 4),
(5, 4),
(6, 4);

GO

