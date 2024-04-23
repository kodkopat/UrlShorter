namespace UrlShorter.Domain.Entities
{
    public class Urls : BaseEntity
    {
        public string Url { get; set; }
        public string Key { get; set; }
        public HashSet<Clicks> Clicks { get; set; } = new();
    }
}
