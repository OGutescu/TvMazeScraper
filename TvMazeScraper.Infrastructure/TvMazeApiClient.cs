using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using Polly;
using TvMazeScraper.Infrastructure.Interfaces;
using TvMazeScraper.Infrastructure.Models;

namespace TvMazeScraper.Infrastructure
{
    public class TvMazeApiClient : ITvMazeApiClient
    {
        private readonly HttpClient _httpClient = new HttpClient();
        private readonly string _baseTvShowApiUrl;

        public TvMazeApiClient(IConfiguration configuration)
        {
            _baseTvShowApiUrl = configuration["TvMazeApiBaseUrl"];
        }

        public async Task<List<TvShowApiModel>> GetTvShowsFromApi(int pageNo)
        {
            var apiUrl = GetUrlForTvShows(pageNo);
            var content = await CallTvMazeApi(apiUrl);
            return JsonConvert.DeserializeObject<List<TvShowApiModel>>(content);
        }

        public async Task<List<CastApiModel>> GetCastFromApi(int tvShowId)
        {
            var apiUrl = GetUrlForCast(tvShowId);
            var content = await CallTvMazeApi(apiUrl);
            return JsonConvert.DeserializeObject<List<CastApiModel>>(content);
        }

        private string GetUrlForTvShows(int pageNo)
        {
            return $"{_baseTvShowApiUrl}?page={pageNo-1}";
        }

        private string GetUrlForCast(int tvShowId)
        {
            return $"{_baseTvShowApiUrl}/{tvShowId}/cast";
        }

        private async Task<string> CallTvMazeApi(string apiUrl)
        {
            HttpStatusCode[] httpStatusCodesWorthRetrying = {
                HttpStatusCode.TooManyRequests, //429
                HttpStatusCode.RequestTimeout, // 408
                HttpStatusCode.InternalServerError, // 500
                HttpStatusCode.BadGateway, // 502
                HttpStatusCode.ServiceUnavailable, // 503
                HttpStatusCode.GatewayTimeout // 504
            };

            var response = await Policy
                .Handle<HttpRequestException>()
                .OrResult<HttpResponseMessage>(r => httpStatusCodesWorthRetrying.Contains(r.StatusCode))
                .WaitAndRetryAsync(4, i => TimeSpan.FromSeconds(i*2))
                .ExecuteAsync(() => _httpClient.GetAsync(apiUrl));

            var content = await response.Content.ReadAsStringAsync();
            return content;
        }
    }
}
