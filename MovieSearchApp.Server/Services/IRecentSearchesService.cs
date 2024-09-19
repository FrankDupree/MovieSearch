namespace MovieSearchApp.Server.Services
{
    public interface IRecentSearchesService
    {
        void AddSearch(string title);
        IEnumerable<string> GetRecentSearches();
    }
}
