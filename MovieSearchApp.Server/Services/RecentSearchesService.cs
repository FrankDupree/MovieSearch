namespace MovieSearchApp.Server.Services
{
    public class RecentSearchesService : IRecentSearchesService
    {
        private readonly HashSet<string> _recentSearchesSet = new HashSet<string>(StringComparer.OrdinalIgnoreCase);
        private readonly List<string> _recentSearchesList = new List<string>();
        private readonly int _recentSearchesLimit;

        public RecentSearchesService(IConfiguration configuration)
        {
            _recentSearchesLimit = configuration.GetValue<int>("RecentSearchesLimit");
        }

        public void AddSearch(string title)
        {
            if (string.IsNullOrWhiteSpace(title))
                return;

            var normalizedTitle = title.Trim().ToLower();

            lock (_recentSearchesList)
            {
                if (!_recentSearchesSet.Contains(normalizedTitle))
                {
                    if (_recentSearchesList.Count == _recentSearchesLimit)
                    {
                        string removedTitle = _recentSearchesList[0];
                        _recentSearchesList.RemoveAt(0);
                        _recentSearchesSet.Remove(removedTitle.ToLower());
                    }

                    _recentSearchesSet.Add(normalizedTitle);
                    _recentSearchesList.Add(title);
                }
            }
        }

        public IEnumerable<string> GetRecentSearches()
        {
            return _recentSearchesList;
        }
    }
}