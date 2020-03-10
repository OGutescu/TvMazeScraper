using System;
using System.Linq;
using System.Threading.Tasks;
using TvMazeScraper.Domain.Entities;
using TvMazeScraper.Domain.Interfaces;

namespace TvMazeScraper.Infrastructure.Repositories
{
    public class BookmarkDbRepository : IBookmarkDbRepository
    {
        private const int MaxCallTimeInMinutes = 3;
        readonly TvMazeDbContext _context;

        public BookmarkDbRepository(TvMazeDbContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }

        public async Task<Bookmark> Add(Status status)
        {
            var entity = new Bookmark
            {
                Status = status.ToCompleteString(),
                LastUpdate = DateTime.Now,
            };

            var newBookmark = await _context.Bookmarks.AddAsync(entity);
            await _context.SaveChangesAsync();
            return newBookmark.Entity;
        }

        public async Task Update(Bookmark bookmark, Status status)
        {
            bookmark.Status = status.ToCompleteString();
            bookmark.LastUpdate = DateTime.Now;

            _context.Bookmarks.Update(bookmark);
            await _context.SaveChangesAsync();
        }

        public async Task<Bookmark> GetFirstNotFinished()
        {
            var x =  _context.Bookmarks.AsEnumerable().FirstOrDefault(b =>
                b.Status == Status.Error.ToCompleteString() || 
                (b.Status == Status.InProgress.ToCompleteString() &&
                (DateTime.Now - b.LastUpdate).TotalMinutes > MaxCallTimeInMinutes));

            return x;
        }
    }

}
