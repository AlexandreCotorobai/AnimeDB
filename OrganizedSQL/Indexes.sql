
-- Create indexes for Anime table
CREATE INDEX IX_Anime_Name ON Anime (Name);
CREATE INDEX IX_Anime_Aired_date ON Anime (Aired_date);

-- Create indexes for Characters table
CREATE INDEX IX_Characters_Name ON Characters (Name);

-- Create indexes for Studios table
CREATE INDEX IX_Studio_Name ON Studio (Name);
CREATE INDEX IX_Studio_Established_at ON Studio (Established_at);

-- Create indexes for Staff table
CREATE INDEX IX_Staff_Name ON Staff (Name);
CREATE INDEX IX_Staff_Type ON Staff ([Type]);

-- Create indexes for Users table
CREATE INDEX IX_Users_Name ON Users (Name);
CREATE INDEX IX_Users_Birthday ON Users (Birthday);
CREATE INDEX IX_Users_Created_date ON Users (Created_date);
