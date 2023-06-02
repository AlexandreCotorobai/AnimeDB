Use AnimeDB;
GO

USE AnimeDB

GO

INSERT INTO Users (ID, Name, Sex, Created_date, Birthday, Location, Is_admin)
VALUES
(1, 'John Doe', 'M', '2022-01-15', '1990-05-10', 'New York', 0),
(2, 'Jane Smith', 'F', '2022-03-20', '1995-09-22', 'London', 0),
(3, 'Alex Johnson', 'M', '2022-06-05', '1985-12-02', 'Tokyo', 1),
(4, 'Emily Davis', 'F', '2022-09-10', '1998-07-18', 'Los Angeles', 0),
(5, 'Michael Wilson', 'M', '2022-11-25', '1992-03-12', 'Sydney', 0),
(6, 'Sophia Thompson', 'F', '2023-02-03', '1996-09-29', 'Paris', 0),
(7, 'Daniel Lee', 'M', '2023-04-18', '1993-11-05', 'Seoul', 0),
(8, 'Olivia Garcia', 'F', '2023-06-26', '1997-01-25', 'Mexico City', 0);
GO

INSERT INTO Studio (ID, Name, Alt_Name, Description, Established_at)
VALUES
(1, 'Studio Pierrot', 'Pierrot Co. Ltd.', 'A renowned anime studio known for Naruto and Bleach.', '1979-09-20'),
(2, 'WIT Studio', 'Wit Studio Inc.', 'Notable for producing Attack on Titan.', '2012-06-01'),
(3, 'Bones', NULL, 'Known for My Hero Academia and Fullmetal Alchemist.', '1998-10-28'),
(4, 'A-1 Pictures', NULL, 'Produced Sword Art Online and Fairy Tail.', '2005-05-09'),
(5, 'Kyoto Animation', 'Kyoto Animation Co., Ltd.', 'Notable for Violet Evergarden and K-On!', '1981-07-15'),
(6, 'Production I.G', 'Production I.G, Inc.', 'Known for Attack on Titan and Psycho-Pass.', '1987-12-15'),
(7, 'Madhouse', 'Madhouse Inc.', 'Produced Death Note and One Punch Man.', '1972-10-20'),
(8, 'Studio Ghibli', 'Ghibli Inc.', 'Renowned for Spirited Away and My Neighbor Totoro.', '1985-06-15'),
(9, 'J.C.Staff', NULL, 'Known for Toradora! and A Certain Scientific Railgun.', '1986-01-25'),
(10, 'Studio Sunrise', 'Sunrise Inc.', 'Produced Mobile Suit Gundam and Code Geass.', '1972-09-24'),
(11, 'Toei Animation', 'Toei Animation Co., Ltd.', 'Known for producing Dragon Ball Z.', '1948-01-23');

GO

INSERT INTO Anime (ID, Name, Alt_name, Synopsis, Episodes, Score, Aired_date, Finished_date, Season, FK_Studio_ID)
VALUES
(1, 'Naruto', 'Naruto: Shippuden', 'A story about a young ninja named Naruto.', 500, NULL, '2002-10-03', '2007-02-08', NULL, 1),
(2, 'Attack on Titan', 'Shingeki no Kyojin', 'Humans fight against giant humanoid creatures called Titans.', 75, NULL, '2013-04-07', '2021-03-28', NULL, 2),
(3, 'My Hero Academia', 'Boku no Hero Academia', 'A boy with no powers in a world of superheroes.', 100, NULL, '2016-04-03', NULL, NULL, 3),
(4, 'Attack on Titan: Season 2', 'Shingeki no Kyojin Season 2', 'Continuation of the battle against Titans.', 12, NULL, '2017-04-01', '2017-06-17', NULL, 2),
(5, 'Attack on Titan: Season 3', 'Shingeki no Kyojin Season 3', 'The uncovering of dark secrets within the walls.', 24, NULL, '2018-07-22', '2019-07-01', NULL, 2),
(6, 'Attack on Titan: Season 4', 'Shingeki no Kyojin Season 4', 'The final battle against Marley.', 16, NULL, '2020-12-07', '2021-03-29', NULL, 2),
(7, 'Bleach', NULL, 'A story about a young boy who becomes a Soul Reaper.', 366, NULL, '2004-10-05', '2012-03-27', NULL, 1),
(8, 'Sword Art Online', 'SAO', 'Players trapped in a virtual reality MMORPG.', 49, NULL, '2012-07-07', '2012-12-22', NULL, 4),
(9, 'Fairy Tail', NULL, 'A guild of wizards and their adventures.', 328, NULL, '2009-10-12', '2019-09-29', NULL, 4),
(10, 'Violet Evergarden', NULL, 'A former soldiers journey as an Auto Memory Doll.', 13, NULL, '2018-01-11', '2018-04-05', NULL, 5),
(11, 'K-On!', NULL, 'High school girls forming a music club.', 39, NULL, '2009-04-03', '2010-06-26', NULL, 5),
(12, 'Attack on Titan: Season 1', 'Shingeki no Kyojin Season 1', 'The struggle for humanity against Titans begins.', 25, NULL, '2013-04-07', '2013-09-29', NULL, 2),
(13, 'My Neighbor Totoro', 'Tonari no Totoro', 'Two girls encounter friendly forest spirits.', 1, NULL, '1988-04-16', '1988-04-16', NULL, 8),
(14, 'Psycho-Pass', NULL, 'A futuristic society where crime can be predicted and prevented.', 22, NULL, '2012-10-12', '2013-03-22', NULL, 6),
(15, 'Death Note', NULL, 'A high school student gains the power to kill anyone by writing their name in a notebook.', 37, NULL, '2006-10-03', '2007-06-26', NULL, 7),
(16, 'One Punch Man', NULL, 'A superhero who can defeat any opponent with a single punch.', 24, NULL, '2015-10-04', '2015-12-20', NULL, 7),
(17, 'Spirited Away', 'Sen to Chihiro no Kamikakushi', 'A girl enters a magical world to save her parents.', 1, NULL, '2001-07-20', '2001-07-20', NULL, 8),
(18, 'Toradora!', NULL, 'A high school romantic comedy about two unlikely allies.', 25, NULL, '2008-10-02', '2009-03-26', NULL, 9),
(19, 'A Certain Scientific Railgun', 'Toaru Kagaku no Railgun', 'A girl with electromagnetic powers in a city of esper students.', 48, NULL, '2009-10-02', '2010-03-18', NULL, 9),
(20, 'Mobile Suit Gundam', 'Kidou Senshi Gundam', 'Giant robots and interstellar war in the Universal Century timeline.', 43, NULL, '1979-04-07', '1980-01-26', NULL, 10),
(21, 'Code Geass', 'Code Geass: Hangyaku no Lelouch', 'A high school student leading a rebellion against a powerful empire.', 50, NULL, '2006-10-05', '2007-07-28', NULL, 10),
(22, 'Fullmetal Alchemist', 'Hagane no Renkinjutsushi', '"''Two brothers seek the Philosophers Stone to restore their bodies.', 51, NULL, '2003-10-04', '2004-10-02', NULL, 3),
(23, 'Cowboy Bebop', NULL, 'Bounty hunters traveling through space in the year 2071.', 26, NULL, '1998-04-03', '1999-04-24', NULL, 7),
(24, 'Neon Genesis Evangelion', 'Shinseiki Evangelion', 'Pilots fighting against mysterious creatures known as Angels.', 26, NULL, '1995-10-04', '1996-03-27', NULL, 6),
(25, 'Dragon Ball Z', NULL, 'Goku and his friends defend the Earth against powerful villains.', 291, NULL, '1989-04-26', '1996-01-31', NULL, 11),
(26, 'Black Clover', NULL, 'A boy with no magic power aiming to become the Wizard King.', 170, NULL, '2017-10-03', NULL, NULL, 4);


GO

INSERT INTO Staff (ID, Type, Name, Birthday)
VALUES
(1, 'Director', 'Hayao Miyazaki', '1941-01-05'),
(2, 'Animator', 'Yoshitaka Amano', '1952-03-26'),
(3, 'Writer', 'Gen Urobuchi', '1972-12-20'),
(4, 'Voice Actor', 'Maaya Sakamoto', '1980-03-31'),
(5, 'Voice Actor', 'Yuki Kaji', '1985-09-03'),
(6, 'Voice Actor', 'Junko Takeuchi', '1972-04-05'), -- Voice actor for Naruto Uzumaki
(7, 'Voice Actor', 'Noriko Shitaya', '1982-03-20'), -- Voice actor for Sakura Haruno
(8, 'Voice Actor', 'Kazuhiko Inoue', '1954-03-26'), -- Voice actor for Kakashi Hatake
(9, 'Voice Actor', 'Nana Mizuki', '1980-01-21'), -- Voice actor for Hinata Hyuga
(10, 'Voice Actor', 'Kousuke Toriumi', '1973-05-31'), -- Voice actor for Shikamaru Nara
(11, 'Voice Actor', 'Akira Ishida', '1967-11-02'), -- Voice actor for Sasuke Uchiha
(12, 'Voice Actor', 'Yui Ishikawa', '1989-05-30'), -- Voice actor for Mikasa Ackerman
(13, 'Voice Actor', 'Marina Inoue', '1985-01-20'), -- Voice actor for Armin Arlert
(14, 'Voice Actor', 'Hiroshi Kamiya', '1975-01-28'), -- Voice actor for Levi Ackerman
(15, 'Voice Actor', 'Daisuke Ono', '1978-05-04'), -- Voice actor for Erwin Smith
(16, 'Voice Actor', 'Inoue Marina', '1985-01-20'), -- Voice actor for Historia Reiss
(17, 'Voice Actor', 'Kisho Taniyama', '1975-08-11'), -- Voice actor for Jean Kirstein
(18, 'Voice Actor', 'Nobuhiko Okamoto', '1986-10-24'), -- Voice actor for Katsuki Bakugo
(19, 'Voice Actor', 'Kenta Miyake', '1977-08-23'), -- Voice actor for All Might
(20, 'Voice Actor', 'Ayane Sakura', '1994-01-29'), -- Voice actor for Ochaco Uraraka
(21, 'Voice Actor', 'Kaito Ishikawa', '1993-10-13'), -- Voice actor for Tenya Iida
(22, 'Voice Actor', 'Yuuki Kaji', '1985-09-03'), -- Voice actor for Shoto Todoroki
(23, 'Voice Actor', 'Aoi Yuuki', '1992-03-27'), -- Voice actor for Tsuyu Asui
(24, 'Voice Actor', 'Masako Nozawa', '1936-10-25'), -- Voice actor for Goku
(25, 'Voice Actor', 'Ryo Horikawa', '1958-02-01'), -- Voice actor for Vegeta
(26, 'Voice Actor', 'Ryusei Nakao', '1951-02-05'), -- Voice actor for Frieza
(27, 'Voice Actor', 'Mayumi Tanaka', '1955-01-15'), -- Voice actor for Luffy
(28, 'Voice Actor', 'Kazuya Nakai', '1967-11-25'), -- Voice actor for Zoro
(29, 'Voice Actor', 'Akemi Okamura', '1969-03-12'), -- Voice actor for Nami
(30, 'Voice Actor', 'Megumi Han', '1989-06-03'), -- Voice actor for Gon Freecss
(31, 'Voice Actor', 'Mariya Ise', '1988-09-25'); -- Voice actor for Killua Zoldyck

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

INSERT INTO Characters (ID, Name, Description, FK_Voice_actor)
VALUES
(1, 'Naruto Uzumaki', 'The main protagonist of Naruto series.', 4),
(2, 'Eren Yeager', 'The main protagonist of Attack on Titan series.', 5),
(3, 'Izuku Midoriya', 'The main protagonist of My Hero Academia series.', 5),
(4, 'Sasuke Uchiha', 'A skilled ninja and rival of Naruto.', 4),
(5, 'Sakura Haruno', 'A skilled kunoichi and member of Team 7.', 6),
(6, 'Kakashi Hatake', 'A skilled ninja and mentor of Team 7.', 7),
(7, 'Hinata Hyuga', 'A shy but determined member of the Hyuga clan.', 8),
(8, 'Shikamaru Nara', 'A lazy yet strategic member of Team 10.', 9),
(9, 'Gaara', 'A former antagonist turned ally with sand-based powers.', 10),
(10, 'Itachi Uchiha', 'Sasuke Uchihas older brother and former member of Akatsuki.', 11),
(11, 'Mikasa Ackerman', 'Erens adoptive sister and skilled fighter.', 12),
(12, 'Armin Arlert', 'Erens childhood friend and strategist.', 13),
(13, 'Levi Ackerman', 'The captain of the Special Operations Squad.', 14),
(14, 'Erwin Smith', 'The commander of the Scout Regiment.', 15),
(15, 'Historia Reiss', 'A member of the royal family with a mysterious past.', 16),
(16, 'Jean Kirstein', 'A member of the Scout Regiment with a strong sense of justice.', 17),
(17, 'Katsuki Bakugo', 'Izuku Midoriyas childhood friend and rival.', 18),
(18, 'All Might', 'The Symbol of Peace and the No. 1 hero.', 19),
(19, 'Ochaco Uraraka', 'A cheerful girl with gravity manipulation powers.', 20),
(20, 'Tenya Iida', 'A disciplined student with super-speed abilities.', 21),
(21, 'Shoto Todoroki', 'A student with fire and ice manipulation powers.', 22),
(22, 'Tsuyu Asui', 'A student with frog-like abilities and a friendly personality.', 23),
(23, 'Goku', 'The main protagonist of Dragon Ball series.', 24),
(24, 'Vegeta', 'A Saiyan prince and rival of Goku.', 25),
(25, 'Frieza', 'One of the primary antagonists in Dragon Ball series.', 26),
(26, 'Luffy', 'The main protagonist of One Piece series.', 27),
(27, 'Zoro', 'A skilled swordsman and member of the Straw Hat Pirates.', 28),
(28, 'Nami', 'A talented navigator and member of the Straw Hat Pirates.', 29),
(29, 'Gon Freecss', 'The main protagonist of Hunter x Hunter series.', 30),
(30, 'Killua Zoldyck', 'Gons best friend and a skilled assassin.', 31);


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
(5, 6, 'Sequel'),
(4, 2, 'Prequel'),
(5, 4, 'Prequel'),
(6, 5, 'Prequel');

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

