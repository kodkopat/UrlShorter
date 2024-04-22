namespace UrlShorter.Application.Dtos
{
    public class UrlDto
    {
        public Guid Id { get; set; }
        public string Url { get; set; }
        public string ShortTitle { get; set; }
    }
}