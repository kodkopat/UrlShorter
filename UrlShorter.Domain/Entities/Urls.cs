namespace UrlShorter.Domain.Entities
{
    public class Urls : BaseEntity
    {
        public string Url { get; set; }
        public string ShortUrl { get; set; }
        public int Coubt { get; set; }
    }
}
