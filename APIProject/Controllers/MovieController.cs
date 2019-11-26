using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using APIProject.Models;
using Microsoft.AspNetCore.Mvc;

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
        
        [HttpGet]
        public async Task<IActionResult> Index(string searchQuery)
        {


            var response = await _client.GetAsync($"?t={searchQuery}&apikey=ab73b87f");
            var movies = await response.Content.ReadAsAsync<MovieRoot>();

            return View(movies);
        }

        public IActionResult FavoritesList() 
        {
            var list = _context.FavList.ToList();
            return View(list);
        }
    }
}