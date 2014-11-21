CREATE TABLE [users].[Table]
(
	[Id] INT NOT NULL identity(1,1), 
    [username] NCHAR(20) NOT NULL, 
    [password] NCHAR(20) NOT NULL, 
    [fullName] NCHAR(30) NULL, 
    [email] NCHAR(50) NULL, 
    [DOB] DATE NULL, 
    [played] INT NULL default (0), 
    [won] INT NULL default (0), 
    [rock] INT NULL default (0), 
    [scissors] INT NULL default (0), 
    [paper] INT NULL default (0), 
    [lizard] INT NULL default (0), 
    [spock] INT NULL default (0), 
    [dateJoined] DATETIME NULL, 
    [dateLastPlayed] DATETIME NULL
)
