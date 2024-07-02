using Microsoft.Extensions.Options;
using MyPlayMarket.Core.IServices;
using MyPlayMarket.Infrastructure.Data.IRepository;
using MyPlayMarket.Infrastructure.Entities;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace MyPlayMarket.Core.Services
{
    public class ApiService : IApiService
    {
        const string apiKey = "cf0d983c9d624cbd989ded847b1429f9";
        private readonly HttpClient _httpClient;
        private readonly IGameRepository _gameRepository;
        public ApiService(IGameRepository gameRepository, HttpClient httpClient)
        {
            _gameRepository = gameRepository;
            _httpClient = httpClient;
        }
        public async Task ImportGamesFromApiAsync()
        {
            var response = await _httpClient.GetAsync($"https://api.rawg.io/api/games?key={apiKey}");
            response.EnsureSuccessStatusCode();

            string json = await response.Content.ReadAsStringAsync();
            Dictionary<string, object> jsonData = JsonConvert.DeserializeObject<Dictionary<string, object>>(json);
            string next = jsonData["next"].ToString();
            var results = jsonData["results"] as JsonElement?;
            Random random = new Random();
            foreach(var jsonGame in results.Value.EnumerateArray())
            {
                Game game = new Game
                { Name = jsonGame.GetProperty("name").ToString(),
                    Cost = random.NextDouble() * 100,
                    Company = "No info",
                    UrlImage = jsonGame.GetProperty("background_image").ToString(),
                    Release = Convert.ToDateTime(jsonGame.GetProperty("released")),
                    GameGenres= jsonGame.GetProperty("genres").
            }
            }
            //foreach (var game in games)
            //{
            //    await _gameRepository.CreateGameAsync(game);
            //}
        }
        //public async Task ImportGenresFromApiAsync()
        //{
        //    var response = await _httpClient.GetAsync($"https://api.rawg.io/api/games?key={apiKey}");
        //    response.EnsureSuccessStatusCode();

        //    string json = await response.Content.ReadAsStringAsync();
        //    Dictionary<string, object> jsonData = JsonConvert.DeserializeObject<Dictionary<string, object>>(json);
        //    var results = jsonData["results"] as JsonElement?;
        //    foreach (var game in results.Value.EnumerateArray())
        //    {
        //        await
        //    }
        //}
        //public async Task ImportGenreFromApiAsync()
        //{

        //}
        //public async Task ImportPlatformFromApiAsync()
        //{

        //}
    }
}
