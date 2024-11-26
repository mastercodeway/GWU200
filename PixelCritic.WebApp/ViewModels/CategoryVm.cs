using PixelCritic.WebApp.Models;

namespace PixelCritic.WebApp.ViewModels
{
    public class CategoryVm
    {
        public string ImgUrl { get; }
        public string Platform { get; }
        public List<(RatedGame,string)> Games { get; }
        public CategoryVm(string platform, List<(RatedGame,string)> games, string imgUrl)
        {
            Platform = platform;
            Games = games;
            ImgUrl = imgUrl;
        }

    }
}
