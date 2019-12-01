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

            _context.FavList.Add(newFavMovie);
            _context.SaveChanges();

            return View("Index");
        }
        [HttpGet]
        public async Task<IActionResult> SearchMovies(string searchQuery)
        {


            var response = await _client.GetAsync($"?s={searchQuery}&apikey=ab73b87f");
            var movies = await response.Content.ReadAsAsync<SearchResultsRoot>();

            return View("Index", movies);
        }

        public IActionResult FavoritesList()
        {
            var list = _context.FavList.ToList();
            return View(list);
        }
    }
}