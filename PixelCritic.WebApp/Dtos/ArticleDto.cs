using PixelCritic.WebApp.Models;
using System.Linq.Expressions;
using System.Linq;

namespace PixelCritic.WebApp.Dtos
{
    public class ArticleDto
    {
        public Guid Id { get; set; }
        public string ImageUrl { get; set; } = string.Empty;
        public string Title { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public DateOnly Date { get; set; }
        public string Author { get; set; } = string.Empty;

        public static ArticleDto MapToDto(Article article)
            => new ArticleDto
            {
                Id = article.Id,
                ImageUrl = article.ImageUrl,
                Title = article.Title,
                Description = article.Description,
                Date = article.Date,
                Author = article.Author,
            };
        public static IEnumerable<ArticleDto> MapToDto(IEnumerable<Article> articles)
            => articles.Select(MapToDto).ToList();
    }
}
