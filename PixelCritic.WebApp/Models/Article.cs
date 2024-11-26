using System.ComponentModel.DataAnnotations;

namespace PixelCritic.WebApp.Models
{
    public class Article
    {
        [Key]
        public Guid Id { get; set; }
        public string ImageUrl { get; set; } = string.Empty;
        public string Title { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public DateOnly Date { get; set; } = DateOnly.FromDateTime(DateTime.Now);
        public string Author { get; set; } = string.Empty;
    }
}
