using Microsoft.EntityFrameworkCore;
using PolutionSensor.v2.Data;
using PolutionSensor.v2.ViewModels;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Globalization;
using System.Threading.Tasks;

namespace PolutionSensor.v2.Services
{
    public interface IStatisticsService
    {
        Task<IEnumerable<PolutionStatisticsViewModel>> GetDailyStatisticsAsync();
        Task<IEnumerable<PolutionStatisticsViewModel>> GetHourlyStatisticsAsync(string date);
    }

    public class StatisticsService : IStatisticsService
    {
        private ApplicationDbContext _context;

        public StatisticsService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<PolutionStatisticsViewModel>> GetDailyStatisticsAsync()
        {
            return await _context.PolutionStatisticsViewModel.FromSql("exec sp_GetDailyAverage").ToListAsync();
        }

        public async Task<IEnumerable<PolutionStatisticsViewModel>> GetHourlyStatisticsAsync(string date)
        {
            var actualDate = DateTime.ParseExact(date, "yyyyMMdd", CultureInfo.InvariantCulture);
            return await _context.PolutionStatisticsViewModel.FromSql("exec sp_GetHourlyAverage @date", new SqlParameter("@date", actualDate)).ToListAsync();
        }
    }
}
