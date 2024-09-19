using MovieSearchApp.Server.DTOs.Response;

namespace MovieSearchApp.Server.Services
{
    public interface IOMDbService
    {
        Task<MovieSearchResponse> SearchMovieByTitleAsync(string title);
        Task<OMDbMovieResponse> GetMovieDetailsAsync(string imdbId);
    }
}
