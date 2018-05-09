
CREATE PROCEDURE [dbo].[GetNetTableStructure]
@tableName varchar(200)
AS
BEGIN
	SELECT c.name AS ColumnName, t.name as ColumnType, 
	CStatement = ' public ' + case t.name
		when 'bigint' then case when c.isnullable > 0 then 'Nullable<long>' else 'long' end
		when 'binary' then case when c.isnullable > 0 then 'Nullable<bool>' else 'bool' end 
		when 'bit' then case when c.isnullable > 0 then 'Nullable<bool>' else 'bool' end 
		when 'char' then 'char'
		when 'date' then case when c.isnullable > 0 then 'Nullable<DateTime>' else 'DateTime' end 
		when 'datetime' then case when c.isnullable > 0 then 'Nullable<DateTime>' else 'DateTime' end 
		when 'datetime2' then case when c.isnullable > 0 then 'Nullable<DateTime>' else 'DateTime' end 
		when 'datetimeoffset' then  case when c.isnullable > 0 then 'Nullable<decimal>' else 'decimal' end  
		when 'decimal' then  case when c.isnullable > 0 then 'Nullable<decimal>' else 'decimal' end  
		when 'float' then  case when c.isnullable > 0 then 'Nullable<decimal>' else 'decimal' end  
		when 'int' then  case when c.isnullable > 0 then 'Nullable<int>' else 'int' end  
		when 'money' then case when c.isnullable > 0 then 'Nullable<decimal>' else 'decimal' end  
		when 'nchar' then 'char'
		when 'numeric' then case when c.isnullable > 0 then 'Nullable<decimal>' else 'decimal' end  
		when 'nvarchar' then 'string'
		when 'real' then case when c.isnullable > 0 then 'Nullable<decimal>' else 'decimal' end  
		when 'smalldatetime' then case when c.isnullable > 0 then 'Nullable<DateTime>' else 'DateTime' end 
		when 'smallint' then case when c.isnullable > 0 then 'Nullable<short>' else 'short' end  
		when 'smallmoney' then case when c.isnullable > 0 then 'Nullable<decimal>' else 'decimal' end  
		when 'time' then case when c.isnullable > 0 then 'Nullable<DateTime>' else 'DateTime' end 
		when 'timestamp' then case when c.isnullable > 0 then 'Nullable<DateTime>' else 'DateTime' end 
		when 'tinyint' then case when c.isnullable > 0 then 'Nullable<short>' else 'short' end  
		when 'uniqueidentifier' then 'Guid'
		when 'varchar' then 'string'
		when 'xml' then 'string'
		else 'N/A' end +
	' ' + c.name + ' { get; set; }'
	FROM syscolumns c, systypes t
	WHERE c.xusertype = t.xusertype
and id = (SELECT id FROM sysobjects WHERE [Name] = @tableName)
END
