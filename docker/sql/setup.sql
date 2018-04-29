CREATE PROCEDURE ProvisionDatabase
 @dB_Name varchar(30),
 @dataPath nvarchar(2000),
 @user nvarchar(128),
 @pass nvarchar(200)
AS
BEGIN

DECLARE @stmt nvarchar(max)

DECLARE @mdf nvarchar(2000) = @dataPath + @DB_Name+'.mdf'
DECLARE @ldf nvarchar(2000) = @dataPath + @DB_Name+'.ldf'

IF (DB_ID(@dB_Name) IS NOT NULL)
BEGIN 
PRINT 'Db found...'

SET @stmt = 'DBCC SHRINKDATABASE (' + @dB_Name + ', TRUNCATEONLY);'
PRINT @stmt
EXEC sp_executesql @stmt

DBCC SHRINKDATABASE (@dB_Name, TRUNCATEONLY);
SET @stmt = 'ALTER DATABASE ' + @dB_Name + ' SET SINGLE_USER WITH ROLLBACK IMMEDIATE'
PRINT @stmt
EXEC sp_executesql @stmt

EXEC sp_detach_db @dbname = @dB_Name, @skipchecks = 'true'
END ELSE
BEGIN
PRINT 'Db not found...'
END

DECLARE @result INT
EXEC master.dbo.xp_fileexist @mdf, @result OUTPUT
PRINT @result
IF(@result = 0)
BEGIN 
	SET @stmt = N'CREATE DATABASE ' + @dB_Name + ' ON (NAME = '''+ @dB_Name + ''', FILENAME = '''+ @mdf + '''), (NAME = ''' + @ldf +'_log'', FILENAME = ''' + @ldf +''')'
	PRINT @stmt
	EXEC sp_executesql @stmt
END ELSE
BEGIN
	SET @stmt = N'CREATE DATABASE ' + @dB_Name + ' ON (FILENAME = '''+ @mdf + '''), (FILENAME = ''' + @ldf +''') FOR ATTACH'
	PRINT @stmt
	EXEC sp_executesql @stmt
END

SET @stmt = N'ALTER DATABASE ' + @dB_Name + ' SET MULTI_USER'
PRINT @stmt
EXEC sp_executesql @stmt

SET @stmt = N'USE ' + @dB_Name
PRINT @stmt
EXEC sp_executesql @stmt

SET @stmt = N'CREATE LOGIN ' + @user + ' WITH PASSWORD = ''' + @pass + ''''
PRINT @stmt
EXEC sp_executesql @stmt

SET @stmt = N'CREATE USER ' + @user + ' FOR LOGIN ' + @user
PRINT @stmt
EXEC sp_executesql @stmt

SET @stmt = N'ALTER USER ' + @user + ' WITH LOGIN = ' + @user
PRINT @stmt
EXEC sp_executesql @stmt

SET @stmt = N''+ @user +''
PRINT @stmt
EXEC sp_addrolemember 'db_owner', @stmt
END

SET @stmt = 'ALTER SERVER ROLE sysadmin ADD MEMBER [' + @user +']'
PRINT @stmt
EXEC sp_executesql @stmt
GO

EXEC ProvisionDatabase 'test', '/var/opt/mssql/data/', 'docker', 'Password!2'
GO