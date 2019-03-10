-- SQL Server script to use Logging feature
CREATE SCHEMA Logging
GO
CREATE TABLE Logging.ApplicationLog(
	ApplicationLogId bigint IDENTITY(1,1) PRIMARY KEY,
	LogGuid uniqueidentifier NOT NULL,
	DateTimeStamp datetime NOT NULL DEFAULT (GETDATE()),
	ApplicationName varchar(255) NOT NULL,
	LogMessage varchar(max) NULL
	)
GO
CREATE INDEX ix_ApplicationLog_LogGuid ON Logging.ApplicationLog(LogGuid)
GO
CREATE INDEX ix_ApplicationLog_DateTimeStamp ON Logging.ApplicationLog(DateTimeStamp)
GO

CREATE PROCEDURE Logging.ApplicationLogInsert @ApplicationName varchar(255), @LogGuid uniqueidentifier, @LogMessage varchar(max)
AS
INSERT Logging.ApplicationLog (ApplicationName, LogGuid, LogMessage) 
VALUES (@ApplicationName, @LogGuid, @LogMessage)
-- EXEC Logging.ApplicationLogInsert 'My Application','00000000-0000-0000-0000-000000000000','My Message'
GO
