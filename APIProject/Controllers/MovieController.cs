using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;

using System.Threading.Tasks;
using APIProject.Models;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace APIProject.Controllers
{
    public class MovieController : Controller
    {

        private readonly MovieFavoritesDbContext _context;
        private readonly HttpClient _client;

        public MovieController(MovieFavoritesDbContext context, IHttpClientFactory clientFactory)
        {
            _context = context;
            _client = clientFactory.CreateClient();
            _client.BaseAddress = new Uri("https://www.omdbapi.com/");
        }

        public IActionResult Index()
        {
            return View();
        }
        public async Task<IActionResult> AddToFavorites(string MovieId, string UserId) 
        {
            var response = await _client.GetAsync($"?i={MovieId}&apikey=ab73b87f");
            var movie = await response.Content.ReadAsAsync<MovieRoot>();

            FavList newFavMovie = new FavList(movie.Title, int.Parse(movie.Year), movie.Genre, movie.Runtime, movie.Actors, movie.Plot, UserId);
			var match = _context.FavList.ToList().Where(p => p.Title == newFavMovie.Title);
			if (match.Count() != 1)
			{
				_context.FavList.Add(newFavMovie);
				_context.SaveChanges();
			}
            return View("Index");
        }
		public IActionResult RemoveFromFavorites(int ListId)
		{
			var movie = _context.FavList.Find(ListId);
			_context.FavList.Remove(movie);
			_context.SaveChanges();
			return RedirectToAction("FavoritesList");
		}
        [HttpGet]
        public async Task<IActionResult> SearchMovies(string searchQuery, int pageNumber)
        {

			if (pageNumber == 0)
			{
				pageNumber = 1;
			}
			var response = await _client.GetAsync($"?s={searchQuery}&page={pageNumber}&apikey=ab73b87f");
            var movies = await response.Content.ReadAsAsync<SearchResultsRoot>();
			List<string> tempList = new List<string>();
			foreach (var item in _context.FavList)
			{
				tempList.Add(item.Title);
			}
			
			SearchListModel newListModel = new SearchListModel(movies, tempList);

            return View("Index", newListModel);
        }

        public IActionResult FavoritesList()
        {
            var list = _context.FavList.ToList();
            return View(list);
        }
    }
}