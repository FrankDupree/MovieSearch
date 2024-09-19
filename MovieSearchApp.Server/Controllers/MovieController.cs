using Microsoft.AspNetCore.Mvc;
using MovieSearchApp.Server.Helpers;
using MovieSearchApp.Server.Services;

namespace MovieSearchApp.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MovieController : ControllerBase
    {
        private readonly IOMDbService _omdbService;
        private readonly IRecentSearchesService _recentSearchesService;


        public MovieController(IOMDbService omdbService, IRecentSearchesService recentSearchesService)
        {
            _omdbService = omdbService;
            _recentSearchesService = recentSearchesService;
        }

        [HttpGet("search")]
        public async Task<IActionResult> Search(string title)
        {
            if (string.IsNullOrWhiteSpace(title))
            {
                return BadRequest("Search title cannot be empty.");
            }

            _recentSearchesService.AddSearch(title);

            var results = await _omdbService.SearchMovieByTitleAsync(title);
            return Ok(results);
        }

        [HttpGet("recent")]
        public IActionResult GetRecentSearches()
        {
            var recentSearches = _recentSearchesService.GetRecentSearches();
            return Ok(recentSearches);
        }

        [HttpGet("details")]
        public async Task<IActionResult> GetDetails(string imdbId)
        {
            if (!ImdbIdValidator.IsValidImdbId(imdbId))
            {
                return BadRequest("Invalid IMDb ID.");
            }
            var movieDetails = await _omdbService.GetMovieDetailsAsync(imdbId);
            return Ok(movieDetails);
        }
    }
}
