using System.ComponentModel.DataAnnotations;

namespace UrlShorter.Domain.Entities
{
    public class BaseEntity
    {
        [Key]
        public Guid Id { get; set; }
        public DateTime CreateAt { get; set; } = DateTime.Now;
    }
}
