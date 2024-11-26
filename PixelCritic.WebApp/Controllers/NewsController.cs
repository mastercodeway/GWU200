using Microsoft.AspNetCore.Mvc;
using PixelCritic.WebApp.Dtos;
using PixelCritic.WebApp.Repositories;
using PixelCritic.WebApp.ViewModels;

namespace PixelCritic.WebApp.Controllers
{
    public class NewsController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
        public NewsController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public async Task<IActionResult> Index()
        {

            var news = await _unitOfWork.ArticleRepo.ReadAllAsync();
            foreach (var article in news)
            {
                string slicedTitel = article.Title.Length > 50 ? article.Title.Substring(0, article.Title.LastIndexOf(' ', 30)) : article.Title;
                article.Title = slicedTitel;
                string slicedText = article.Description.Length > 50 ? article.Description.Substring(0, article.Description.LastIndexOf(' ', 100)) : article.Description;
                article.Description = slicedText + "...";
            }
            var newVm = NewsVM.MapToNewVm(news);
            return View(newVm);
        }
        [HttpGet]
        public async Task<IActionResult> ArticleDetails(Guid id)
        {
            var article = await  _unitOfWork.ArticleRepo.ReadAsync(id)??new();
            var dto = ArticleDto.MapToDto(article);
            return View(dto);
        }

    }
}
