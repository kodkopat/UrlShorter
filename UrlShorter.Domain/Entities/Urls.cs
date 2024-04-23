namespace UrlShorter.Domain.Entities
{
    public class Urls : BaseEntity
    {
        public string Url { get; set; }
        public string Key { get; set; }
        public int Count { get; set; }
    }
}
