--exec sp_GetHourlyAverage '2019-01-12'
IF EXISTS (SELECT * FROM SYS.procedures where Name = 'sp_GetHourlyAverage')
	DROP PROCEDURE dbo.sp_GetHourlyAverage
go
CREATE PROCEDURE dbo.sp_GetHourlyAverage
(
	@date datetime
)
as
BEGIN	
	print @date
	select 
		9999999 AS Id,  
		DATEPART(HOUR, InsertDateTime) as [Hour], 
		@date as [Date],
		SSID, 
		CONVERT(DECIMAL(10,2),avg(PM10)) as PM10, 
		CONVERT(DECIMAL(10,2),avg(PM25)) as PM25, 
		count(*) as TotalEntries
	from polution.Entries	
	where CONVERT(date, InsertDateTime) = @date
	group by DATEPART(HOUR, InsertDateTime), SSID
	order by 1 desc
END