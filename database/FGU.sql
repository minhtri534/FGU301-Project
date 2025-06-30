CREATE DATABASE FGU_GameDB;
GO

USE FGU_GameDB;
GO

CREATE TABLE PlayerProgress (
    Id INT PRIMARY KEY IDENTITY(1,1),
    PlayerName NVARCHAR(50) NOT NULL,
    LevelCompleted INT NOT NULL,
    TotalCoins INT NOT NULL,
    Score INT NOT NULL,
    PlayTimeSeconds INT NOT NULL,
    CompletedAt DATE DEFAULT CAST(GETDATE() AS DATE) 
);
GO
