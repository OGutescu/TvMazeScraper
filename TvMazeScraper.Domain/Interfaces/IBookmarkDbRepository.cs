using System.Threading.Tasks;
using TvMazeScraper.Domain.Entities;

namespace TvMazeScraper.Domain.Interfaces
{
    public interface IBookmarkDbRepository
    {
        Task<Bookmark> Add(Status status);
        Task Update(Bookmark bookmark, Status status);
        Task<Bookmark> GetFirstNotFinished();
    }
}