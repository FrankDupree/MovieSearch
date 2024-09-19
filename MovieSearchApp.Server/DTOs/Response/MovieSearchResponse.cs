namespace MovieSearchApp.Server.DTOs.Response
{
    public class MovieSearchResponse
    {
        public List<Movie> Search { get; set; }
        public string TotalResults { get; set; }
        public string Response { get; set; }
    }
}
