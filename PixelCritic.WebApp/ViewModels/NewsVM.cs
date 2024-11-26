using PixelCritic.WebApp.Models;

namespace PixelCritic.WebApp.ViewModels
{
    public class NewsVM
    {
        
        public Guid Id { get; set; }
        public string ImageUrl { get; set; } = string.Empty;
        public string Title { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public DateOnly Date { get; set; } = DateOnly.FromDateTime(DateTime.Now);
        
        public static NewsVM MapToNewVm(Article article)
        {
            return new NewsVM
            {
                Id = article.Id,
                ImageUrl = article.ImageUrl,
                Title = article.Title,
                Description = article.Description,
                Date = article.Date
            };
        }
        public static IEnumerable<NewsVM> MapToNewVm(IEnumerable<Article> articles)
        {
                var result = new List<NewsVM>();
               foreach(var article in articles)
                {
                    result.Add(MapToNewVm(article));
                }
               return result;
        }
    }
}
