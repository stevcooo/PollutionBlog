using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Memory;
using PolutionSensor.v2.Data;
using PolutionSensor.v2.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PolutionSensor.v2.Services
{
    public interface IEntriesService
    {
        Task<IList<Entry>> GetAllAsync();
        Task<IList<Entry>> GetLast100Async();
        Task<int> AddAsync(Entry item);
        void InvalidateChache();
    }

    public class EntriesService : IEntriesService
    {
        IMemoryCache _cache;
        private ApplicationDbContext _context;
        protected readonly string AllItemsCacheKey = "entries-all-items-cache-key";
        protected readonly string Last100ItemsCacheKey = "entries-last-100-items-cache-key";

        public EntriesService(ApplicationDbContext context, IMemoryCache cache)
        {
            _cache = cache;
            _context = context;
        }

        public async Task<IList<Entry>> GetAllAsync()
        {
            if (_cache.TryGetValue(AllItemsCacheKey, out IList<Entry> items))
            {
                return items;
            }
            items = await _context.Entries.OrderByDescending(t => t.EntryId).ToListAsync();
            _cache.Set(AllItemsCacheKey, items);

            return items;
        }

        public async Task<IList<Entry>> GetLast100Async()
        {
            if (_cache.TryGetValue(Last100ItemsCacheKey, out IList<Entry> items))
            {
                return items;
            }
            items = await _context.Entries.OrderByDescending(t => t.EntryId).Take(100).ToListAsync();
            _cache.Set(Last100ItemsCacheKey, items);
            return items;
        }

        public async Task<int> AddAsync(Entry item)
        {
            item.InsertDateTime = DateTime.UtcNow;
            _context.Add(item);
            InvalidateChache();
            return await _context.SaveChangesAsync();
        }

        public void InvalidateChache()
        {
            _cache.Remove(AllItemsCacheKey);
        }
    }
}
