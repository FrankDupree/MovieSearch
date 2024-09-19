using System.Text.RegularExpressions;

namespace MovieSearchApp.Server.Helpers
{
    public static class ImdbIdValidator
    {
        private static readonly Regex ImdbIdRegex = new Regex(@"^tt\d{7}$", RegexOptions.IgnoreCase);

        public static bool IsValidImdbId(string imdbId)
        {
            if (string.IsNullOrWhiteSpace(imdbId))
            {
                return false;
            }

            return ImdbIdRegex.IsMatch(imdbId);
        }
    }
}
