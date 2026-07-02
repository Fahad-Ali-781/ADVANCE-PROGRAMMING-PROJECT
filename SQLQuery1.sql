GO
USE SalatTrackDB;
GO

-- 1. Cities (first, others depend on it)
CREATE TABLE Cities (
    CityID INT PRIMARY KEY IDENTITY,
    CityName NVARCHAR(100) NOT NULL,
    Latitude FLOAT NOT NULL,
    Longitude FLOAT NOT NULL,
    TimeZone NVARCHAR(50) NOT NULL
);

-- 2. Users
CREATE TABLE Users (
    UserID INT PRIMARY KEY IDENTITY,
    Username NVARCHAR(100) NOT NULL UNIQUE,
    PasswordHash NVARCHAR(256) NOT NULL,
    CityID INT,
    CreatedAt DATETIME DEFAULT GETDATE(),
    FOREIGN KEY (CityID) REFERENCES Cities(CityID)
);

-- 3. PrayerTimes
CREATE TABLE PrayerTimes (
    PrayerTimeID INT PRIMARY KEY IDENTITY,
    CityID INT NOT NULL,
    Date DATE NOT NULL,
    Fajr TIME NOT NULL,
    Dhuhr TIME NOT NULL,
    Asr TIME NOT NULL,
    Maghrib TIME NOT NULL,
    Isha TIME NOT NULL,
    FOREIGN KEY (CityID) REFERENCES Cities(CityID)
);

-- 4. NotificationSettings
CREATE TABLE NotificationSettings (
    SettingID INT PRIMARY KEY IDENTITY,
    UserID INT NOT NULL,
    PrayerName NVARCHAR(20) NOT NULL,
    AlertType NVARCHAR(20) NOT NULL,
    IsEnabled BIT NOT NULL DEFAULT 1,
    FOREIGN KEY (UserID) REFERENCES Users(UserID)
);

-- 5. Logs
CREATE TABLE Logs (
    LogID INT PRIMARY KEY IDENTITY,
    UserID INT NOT NULL,
    PrayerName NVARCHAR(20) NOT NULL,
    LoggedAt DATETIME NOT NULL DEFAULT GETDATE(),
    Status NVARCHAR(20) NOT NULL,
    FOREIGN KEY (UserID) REFERENCES Users(UserID)
);
GO
USE SalatTrackDB;
GO

INSERT INTO Cities (CityName, Latitude, Longitude, TimeZone) VALUES
('Lahore', 31.5204, 74.3587, 'Asia/Karachi'),
('Karachi', 24.8607, 67.0011, 'Asia/Karachi'),
('Islamabad', 33.6844, 73.0479, 'Asia/Karachi'),
('Rawalpindi', 33.5651, 73.0169, 'Asia/Karachi'),
('Peshawar', 34.0151, 71.5249, 'Asia/Karachi'),
('Quetta', 30.1798, 66.9750, 'Asia/Karachi'),
('Multan', 30.1575, 71.5249, 'Asia/Karachi'),
('Faisalabad', 31.4504, 73.1350, 'Asia/Karachi');
GO