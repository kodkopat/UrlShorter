namespace UrlShorter.Infrastructure.Helpers
{
    public class UrlHelper
    {
        public static bool IsValidUrl(string url)
        {
            if (string.IsNullOrWhiteSpace(url))
                return false;

            bool isValidUri = Uri.TryCreate(url, UriKind.Absolute, out var resultUri)
                && (resultUri.Scheme == Uri.UriSchemeHttp || resultUri.Scheme == Uri.UriSchemeHttps);

            return isValidUri;
        }

        public static string ExtractKeyFromUrl(string url)
        {
            if (string.IsNullOrWhiteSpace(url))
                throw new ArgumentException("URL cannot be null or empty.");

            var uri = new Uri(url);
            var segments = uri.AbsolutePath.Split('/');
            var key = segments.LastOrDefault(s => !string.IsNullOrEmpty(s));

            if (key == null)
                throw new InvalidOperationException("Key not found in URL.");

            return key;
        }
    }
}
