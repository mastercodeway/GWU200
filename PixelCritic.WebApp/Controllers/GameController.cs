using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using PixelCritic.WebApp.Dtos;
using PixelCritic.WebApp.Models;
using PixelCritic.WebApp.Repositories;
using PixelCritic.WebApp.Services;
using PixelCritic.WebApp.ViewModels;
using System.Security.Claims;



namespace PixelCritic.WebApp.Controllers
{
    public class GameController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IGameService _gameService;

        public GameController(IGameService gameService, IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
            _gameService = gameService;

        }
        public IActionResult Index()
        {
            return View();
        }
        // hämtar api data som sedans hämtas av javascript. 
        public async Task<IActionResult> CallGamesApi()
        {
            var jsonData = await _gameService.CallApiAsync("?sort-by=relevance");

            var games = JsonConvert.DeserializeObject<List<Game>>(jsonData) ?? new List<Game>();

            var apiGameIds = games.Select(game => game.Id).ToList();
            var storedGames = await _unitOfWork.RatedGameRepo.ReadAllAsync(game => apiGameIds.Contains(game.Id));

            var newGames = games.Where(apiGame => !storedGames.Any(stored => stored.Id == apiGame.Id));

            var newRatedGames = RatedGame.MapToRatedGame(newGames);

            await _unitOfWork.RatedGameRepo.AddRangeAsync(newRatedGames);

            await _unitOfWork.SaveAsync();

            storedGames.AddRange(newRatedGames);

            storedGames.Sort((a, b) => b.NumOfReviews.CompareTo(a.NumOfReviews));

            var viewCards = JsonConvert.SerializeObject(storedGames);

            await Console.Out.WriteLineAsync(viewCards);

            if (string.IsNullOrEmpty(jsonData))
            {
                return Json(new { success = false, message = "Något gick fel" });
            }
            else
            {
                return Content(viewCards, "application/json");
            }

        }
        [HttpGet]
        public async Task<IActionResult> Category(string category)
        {
            var imgUrl = string.Empty;
            switch (category)
            {
                case "Strategy":
                    imgUrl = "~/Images/Leonardo_Phoenix_A_futuristic_highenergy_illustration_inspired_1.jpg";
                    break;
                case "Shooter":
                    imgUrl = "~/Images/ai-generated-9058364_1280.jpg";
                    break;
                case "MOBA":
                    imgUrl = "/Images/Leonardo_Phoenix_A_vibrant_actionpacked_illustration_inspired_1.jpg";
                    break;
                case "MMORPG":
                    imgUrl = "/Images/Leonardo_Phoenix_I_envision_a_dark_fantasy_scene_inspired_by_M_1.jpg";
                    break;
                case "Card Game":
                    imgUrl = "/Images/Leonardo_Phoenix_Create_a_captivating_digital_illustration_ins_1.jpg";
                    break;
                case "Web Browser":
                    imgUrl = "/Images/webBrowser.png";
                    break;
                case "PC (Windows)":
                    imgUrl = "/Images/pcneon.png";
                    break;
                case "Playstation":
                    imgUrl = "/Images/playstationneon.png";
                    break;
                case "Xbox":
                    imgUrl = "/Images/xboxneon.png";
                    break;
            }
            List<RatedGame> games;
            if(category == "Xbox" || category == "Playstation" || category == "PC (Windows)" || category == "Web Browser")
            {
                games = await _unitOfWork.RatedGameRepo.ReadAllAsync(game => game.Platform.Contains(category));
            }
            else
            {
                games = await _unitOfWork.RatedGameRepo.ReadAllAsync(game => game.Genre.Contains(category));
            }
             
            var gameJsonTuples = new List<(RatedGame, string)>();
            games.ForEach(game => gameJsonTuples.Add((game, JsonConvert.SerializeObject(game))));
            var vm = new CategoryVm(category, gameJsonTuples, imgUrl);
            return View(vm);
        }

        [HttpGet]
        public async Task<IActionResult> Review(Guid id)
        {
            try
            {
                var review = await _unitOfWork.ReviewRepo.ReadAsync(id);
                var user = await _unitOfWork.UserRepo.ReadAsync(review.UserRefId);
                var userReviewDto = UserReviewDto.Map((user, review));
                return View(userReviewDto);
            }
            catch (Exception ex)
            {
                await Console.Out.WriteLineAsync(ex.ToString());
                return View(new ReviewDto());
            }
        }

        [HttpPost]
        public async Task<IActionResult> AddReview(GameVM gameVm)
        {
            var reviewDto = gameVm.NewReview;
            reviewDto.Id = Guid.NewGuid();
            reviewDto.Posted = DateOnly.FromDateTime(DateTime.Now);
            var review = new Review
            {
                Id = reviewDto.Id,
                Posted = reviewDto.Posted,
                Description = reviewDto.Description,
                GameId = reviewDto.GameId,
                Score = reviewDto.Score,
                Titel = reviewDto.Titel,
                UserRefId = reviewDto.UserRefId,
            };
            var game = await _unitOfWork.RatedGameRepo.ReadAsync(gameVm.NewReview.GameId);

            var reviews = await _unitOfWork.ReviewRepo.ReadAllAsync(r => r.GameId == gameVm.NewReview.GameId);

            var sumOfRateings = reviews.Sum(r => r.Score)+reviewDto.Score;

            var numberOfReviews = reviews.Count() + 1;

            var mean = (double)(sumOfRateings / numberOfReviews);
            var rating = (int)Math.Round(mean, MidpointRounding.AwayFromZero);
            
            await _unitOfWork.RatedGameRepo.UpdateAsync(
                filter: g => g.Id == game.Id,
                updateExpr: g => g.SetProperty(update => update.NumOfReviews, numberOfReviews)
                                  .SetProperty(update => update.Rating, rating));

            _unitOfWork.ClearChangeTracker();

            await _unitOfWork.ReviewRepo.addAsync(review);

            await _unitOfWork.SaveAsync();

            return RedirectToAction("Game", "Game");
        }
        [HttpGet]
        public async Task<IActionResult> Game(string gameData)
        {

            try
            {

                if (!string.IsNullOrEmpty(gameData))
                {
                    TempData["SelectedGame"] = gameData;
                }
                else
                {
                    var redirectedData = TempData["SelectedGame"]?.ToString() ?? string.Empty;
                    TempData["SelectedGame"] = redirectedData;
                    gameData = redirectedData;
                }

                var game = JsonConvert.DeserializeObject<Game>(gameData) ?? throw new Exception("faild to deserialize json game data");

                var reviewDtos = await _unitOfWork.ReviewRepo.ReadAllAsync(
                    filter: review => review.GameId == game.Id,
                    orderBy: review => review.OrderByDescending(r => r.Posted));

                var cards = await PopulateCards(reviewDtos);

                var gameVm = new GameVM()
                {
                    GameReviews = GameReviewDto.Map(game, cards)
                };

                if (User.Identity.IsAuthenticated)
                {
                    gameVm = PopulateNewReview(gameVm);
                }

                return View(gameVm);
            }
            catch (Exception ex)
            {
                await Console.Out.WriteLineAsync(ex.ToString());
                return View(default);
            }
        }

        private GameVM PopulateNewReview(GameVM gameVm)
        {
            var userIdClaim = User.FindFirst(ClaimTypes.NameIdentifier);
            if (userIdClaim != null && Guid.TryParse(userIdClaim.Value, out Guid userId))
            {
                if (gameVm.GameReviews.Id is null) throw new Exception("Game has no Id");

                gameVm.NewReview = new ReviewDto
                {
                    UserRefId = userId,
                    GameId = (int)gameVm.GameReviews.Id,
                };
            }
            return gameVm;
        }

        private async Task<List<(User, Review)>> PopulateCards(IEnumerable<Review> reviewDtos)
        {
            var cards = new List<(User, Review)>();

            foreach (var review in reviewDtos)
            {
                var user = await _unitOfWork.UserRepo.ReadAsync(review.UserRefId);
                if (user != null)
                {
                    cards.Add((user, review));
                }
            }

            return cards;
        }

    }
}
