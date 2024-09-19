using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Moq;
using MovieSearchApp.Server.Controllers;
using MovieSearchApp.Server.DTOs.Response;
using MovieSearchApp.Server.Services;
using IConfiguration = Microsoft.Extensions.Configuration.IConfiguration;

namespace MovieTest
{
    public class OMDbServiceTests
    {
        private Mock<IOMDbService> _mockOmdbService;
        private Mock<IRecentSearchesService> _mockRecentSearchesService;
        private IConfiguration _configuration;

        public OMDbServiceTests()
        {
            _mockOmdbService = new Mock<IOMDbService>();
            _mockRecentSearchesService = new Mock<IRecentSearchesService>();

            var inMemorySettings = new Dictionary<string, string>
            {
                {"Logging:LogLevel:Default", "Information"},
                {"Logging:LogLevel:Microsoft.AspNetCore", "Warning"},
                {"AllowedHosts", "*"},
                {"OMDb:ApiKey", "3099a01b"},
                {"OMDb:BaseUrl", "http://www.omdbapi.com/"},
                {"RecentSearchesLimit", "5"}
            };

            _configuration = new ConfigurationBuilder()
                .AddInMemoryCollection(inMemorySettings)
                .Build();
        }


        [Test]
        public async Task Search_EmptyTitle_ReturnsBadRequest()
        {

            // Arrange
            var controller = new MovieController(_mockOmdbService.Object, _mockRecentSearchesService.Object);

            // Act
            var result = await controller.Search(string.Empty);

            // Assert
            Assert.That(result, Is.InstanceOf<BadRequestObjectResult>());
            var badRequestResult = result as BadRequestObjectResult;
            Assert.That(badRequestResult.Value, Is.EqualTo("Search title cannot be empty."));
        }

        [Test]
        public async Task Search_ValidTitle_ReturnsOkResultWithMovies()
        {
            // Arrange
            var searchResult = new MovieSearchResponse
            {
                Search =
                [
                    new Movie { Title = "Inception", Year = "2010", imdbID = "tt1375666" }
                ],
                Response = "True"
            };

            _mockOmdbService.Setup(service => service.SearchMovieByTitleAsync(It.IsAny<string>()))
                .ReturnsAsync(searchResult);

            var controller = new MovieController(_mockOmdbService.Object, _mockRecentSearchesService.Object);

            // Act
            var result = await controller.Search("Inception");

            // Assert
            Assert.That(result, Is.InstanceOf<OkObjectResult>());
            var okResult = result as OkObjectResult;
            Assert.That(okResult.Value, Is.EqualTo(searchResult));
            _mockOmdbService.Verify(service => service.SearchMovieByTitleAsync("Inception"), Times.Once);
        }


        [Test]
        public void GetRecentSearches_ReturnsOkResultWithRecentSearches()
        {
            // Arrange
            var recentSearches = new[] { "Inception", "Hulk" };
            _mockRecentSearchesService.Setup(service => service.GetRecentSearches())
                .Returns(recentSearches);

            var controller = new MovieController(_mockOmdbService.Object, _mockRecentSearchesService.Object);

            // Act
            var result = controller.GetRecentSearches();

            // Assert
            Assert.That(result, Is.InstanceOf<OkObjectResult>());
            var okResult = result as OkObjectResult;
            Assert.That(okResult.Value, Is.EqualTo(recentSearches));
            _mockRecentSearchesService.Verify(service => service.GetRecentSearches(), Times.Once);
        }


        [Test]
        public async Task GetDetails_ValidImdbId_ReturnsOkResultWithMovieDetails()
        {
            // Arrange
            var movieDetails = new OMDbMovieResponse
            {
                Title = "Inception",
                Year = "2010",
                ImdbID = "tt1375666",
                Director = "Christopher Nolan"
            };

            _mockOmdbService.Setup(service => service.GetMovieDetailsAsync(It.IsAny<string>()))
                .ReturnsAsync(movieDetails);

            var controller = new MovieController(_mockOmdbService.Object, _mockRecentSearchesService.Object);

            // Act
            var result = await controller.GetDetails("tt1375666");

            // Assert
            Assert.That(result, Is.InstanceOf<OkObjectResult>());
            var okResult = result as OkObjectResult;
            Assert.That(okResult.Value, Is.EqualTo(movieDetails));
            _mockOmdbService.Verify(service => service.GetMovieDetailsAsync("tt1375666"), Times.Once);
        }
    }
}