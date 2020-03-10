using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EFCore.BulkExtensions;
using Microsoft.EntityFrameworkCore;
using TvMazeScraper.Domain.Entities;
using TvMazeScraper.Domain.Interfaces;

namespace TvMazeScraper.Infrastructure.Repositories
{
    public class TvShowDbRepository : ITvShowDbRepository
    {
        readonly TvMazeDbContext _context;

        public TvShowDbRepository(TvMazeDbContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }

        public async Task<IEnumerable<TvShow>> GetTvShowsForPage(int pageNo, int pageSize)
        {
            var lastIndex = pageSize * (pageNo - 1);

            var list = await _context.TvShows
                .AsNoTracking()
                .Include(s => s.Persons)
                .Skip(lastIndex)
                .Take(pageSize)
                .ToListAsync();
            return list;
        }

        public async Task AddTvShowsWithPersons(List<TvShow> tvShows)
        {
            await _context.BulkInsertAsync(tvShows);
            await _context.SaveChangesAsync();

            foreach (var tvShow in tvShows.Where(tvShow => tvShow.Persons != null && tvShow.Persons.Any()))
            {
                tvShow.Persons.ForEach(p => p.TvShowIdFk = tvShow.Id);
                await _context.BulkInsertAsync(tvShow.Persons.OrderByDescending(p => p.Birthday).ToList());
            }

            await _context.SaveChangesAsync();
        }
    }
}
