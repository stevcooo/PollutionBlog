--exec sp_GetDailyAverage @days = 3
IF EXISTS (SELECT * FROM SYS.procedures where Name = 'sp_GetDailyAverage')
	DROP PROCEDURE dbo.sp_GetDailyAverage
go
CREATE PROCEDURE dbo.sp_GetDailyAverage
(
	@days int = 7
)
as
BEGIN	
	
	declare @beginDate date = DATEADD(day, -(@days-1), getdate())
	select 
		ROW_NUMBER() OVER(ORDER BY CONVERT(date, InsertDateTime) DESC) AS Id,  
		0 as [Hour], 
		CONVERT(date, InsertDateTime) as [Date], 
		SSID, 
		CONVERT(DECIMAL(10,2),avg(PM10)) as PM10, 
		CONVERT(DECIMAL(10,2),avg(PM25)) as PM25, 
		count(*) as TotalEntries
	from Pollution.Entries
	where InsertDateTime> @beginDate
	group by CONVERT(date, InsertDateTime),SSID
--	order by 1 desc
END