namespace UrlShorter.Application.Dtos
{
    public class DailyStatisticsDto
    {
        /// <summary>
        /// Date of the statistics.
        /// </summary>
        public DateTime Date { get; set; }

        /// <summary>
        /// Total click count for the specified date.
        /// </summary>
        public int ClickCount { get; set; }

        /// <summary>
        /// List of User-Agent strings for the specified date.
        /// </summary>
        public List<string> UserAgents { get; set; } = new(); // Default to an empty list

    }
}
