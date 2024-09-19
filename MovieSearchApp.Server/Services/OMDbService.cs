using MovieSearchApp.Server.DTOs.Response;
using Newtonsoft.Json;

namespace MovieSearchApp.Server.Services
{
    public class OMDbService : IOMDbService
    {
        private readonly HttpClient _httpClient;
        private readonly string _apiKey;
        private readonly string _baseUrl;

        public OMDbService(HttpClient httpClient, IConfiguration config)
        {
            _httpClient = httpClient;
            _apiKey = config["OMDb:ApiKey"];
            _baseUrl = config["OMDb:BaseUrl"];
        }

        public async Task<MovieSearchResponse> SearchMovieByTitleAsync(string title)
        {
            var response = await _httpClient.GetStringAsync($"{_baseUrl}?s={title}&apikey={_apiKey}");
            var searchResponse = JsonConvert.DeserializeObject<MovieSearchResponse>(response);
            return searchResponse;
        }

        public async Task<OMDbMovieResponse> GetMovieDetailsAsync(string imdbId)
        {
            var response = await _httpClient.GetAsync($"{_baseUrl}?i={imdbId}&apikey={_apiKey}");
            response.EnsureSuccessStatusCode();
            var content = await response.Content.ReadAsStringAsync();

            return JsonConvert.DeserializeObject<OMDbMovieResponse>(content);
        }

    }
}
