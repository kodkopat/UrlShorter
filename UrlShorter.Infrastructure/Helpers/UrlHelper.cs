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
    }
}
