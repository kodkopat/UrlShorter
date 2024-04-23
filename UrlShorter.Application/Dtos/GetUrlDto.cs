namespace UrlShorter.Application.Dtos
{
    public class GetUrlDto
    {
        /// <summary>
        /// Gets the original URL.
        /// </summary>
        public string Url { get; set; }

        /// <summary>
        /// Gets the shortened URL.
        /// </summary>
        public string ShortUrl { get; set; }

        /// <summary>
        /// Gets the total count of clicks on the shortened URL.
        /// </summary>
        public int Count { get; set; }
    }
}