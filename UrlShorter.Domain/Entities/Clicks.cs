namespace UrlShorter.Domain.Entities
{
    public class Clicks : BaseEntity
    {
        public string? UserAgentString { get; set; }
        public Guid UrlId { get; set; }
        public Urls Url { get; set; }
    }
}
