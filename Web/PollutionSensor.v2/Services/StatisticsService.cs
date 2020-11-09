using Microsoft.EntityFrameworkCore;
using PollutionSensor.v2.Data;
using PollutionSensor.v2.ViewModels;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Globalization;
using System.Threading.Tasks;

namespace PollutionSensor.v2.Services
{
    public interface IStatisticsService
    {
        Task<IEnumerable<PollutionStatisticsViewModel>> GetDailyStatisticsAsync();
        Task<IEnumerable<PollutionStatisticsViewModel>> GetHourlyStatisticsAsync(string date);
    }

    public class StatisticsService : IStatisticsService
    {
        private ApplicationDbContext _context;

        public StatisticsService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<PollutionStatisticsViewModel>> GetDailyStatisticsAsync()
        {
            return await _context.PollutionStatisticsViewModel.FromSql("exec sp_GetDailyAverage").ToListAsync();
        }

        public async Task<IEnumerable<PollutionStatisticsViewModel>> GetHourlyStatisticsAsync(string date)
        {
            var actualDate = DateTime.ParseExact(date, "yyyyMMdd", CultureInfo.InvariantCulture);
            return await _context.PollutionStatisticsViewModel.FromSql("exec sp_GetHourlyAverage @date", new SqlParameter("@date", actualDate)).ToListAsync();
        }
    }
}
