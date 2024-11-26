using Microsoft.AspNetCore.Mvc;
using PixelCritic.WebApp.Dtos;
using PixelCritic.WebApp.Models;
using PixelCritic.WebApp.Repositories;
using System.Collections;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using static System.Net.Mime.MediaTypeNames;

namespace PixelCritic.WebApp.Controllers
{
    public class HomeController : Controller
    {

        private readonly IUnitOfWork _unitOfWork;
        public HomeController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<IActionResult> Index()
        {

           
            var news = await _unitOfWork.ArticleRepo.ReadAllAsync();
            foreach(var article in news)
            {
                string slicedTitel = article.Title.Length > 50 ? article.Title.Substring(0, article.Title.LastIndexOf(' ', 30)) : article.Title;
                article.Title = slicedTitel;
                string slicedText = article.Description.Length > 50 ? article.Description.Substring(0, article.Description.LastIndexOf(' ', 140)) : article.Description;
                article.Description = slicedText + "...";
            }
            
            var articles = ArticleDto.MapToDto(news);
            return View(articles);
        }
      

    }
}
