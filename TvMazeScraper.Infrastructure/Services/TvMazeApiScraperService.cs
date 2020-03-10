using System;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore.Internal;
using TvMazeScraper.Domain.Entities;
using TvMazeScraper.Domain.Interfaces;

namespace TvMazeScraper.Infrastructure.Services
{
    public class TvMazeApiScraperService : IApiScraperService
    {
        private readonly ITvMazeService _tvMazeService;
        private readonly IBookmarkDbRepository _bookmarkDbRepository;
        private readonly ITvShowDbRepository _tvShowDbRepository;

        public TvMazeApiScraperService(IBookmarkDbRepository bookmarkDbRepository, ITvShowDbRepository tvShowDbRepository, ITvMazeService tvMazeService)
        {
            _bookmarkDbRepository = bookmarkDbRepository;
            _tvShowDbRepository = tvShowDbRepository;
            _tvMazeService = tvMazeService;
        }

        public async Task Start()
        {
            bool areMorePages;
            do
            {
                areMorePages = await ScrapOnePage();
                areMorePages = false; // for testing - so it won't take a lot of time
            } 
            while (areMorePages);
        }

        private async Task<bool> ScrapOnePage()
        {
            var bookmark = await GetPageNumber();
            try
            {
                var shows = await _tvMazeService.GetTvShows(bookmark.PageNumber);
                if (!shows.Any())
                    return false;

                foreach (var show in shows)
                {
                    show.Persons = await _tvMazeService.GetCastForShow(show.Id);
                }
                await _tvShowDbRepository.AddTvShowsWithPersons(shows);

                await _bookmarkDbRepository.Update(bookmark, Status.Finished);
            }
            catch (Exception e)
            {
                await _bookmarkDbRepository.Update(bookmark, Status.Error);
            }
            return true;
        }

        private async Task<Bookmark> GetPageNumber()
        {
            var lastBookmark = await _bookmarkDbRepository.GetFirstNotFinished();
            if (lastBookmark == null)
                lastBookmark = await _bookmarkDbRepository.Add(Status.InProgress);
            else
                await _bookmarkDbRepository.Update(lastBookmark, Status.InProgress);
            return lastBookmark;
        }
    }
}
