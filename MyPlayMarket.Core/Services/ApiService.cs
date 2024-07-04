using Azure;
using Humanizer.Localisation;
using Microsoft.Build.Framework;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using MyPlayMarket.Core.IServices;
using MyPlayMarket.Infrastructure.Data.IRepository;
using MyPlayMarket.Infrastructure.Entities;
using Newtonsoft.Json;
using NuGet.Protocol;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Security.Policy;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace MyPlayMarket.Core.Services
{
    public class ApiService : IApiService
    {
        const string apiKey = "cf0d983c9d624cbd989ded847b1429f9";
        int count = 200;
        private readonly HttpClient _httpClient;
        private readonly IGameRepository _gameRepository;
        private readonly IGenericRepository<Tag> _tagRepository;
        private readonly IGenericRepository<Platform> _platformRepository;
        private readonly IGenericRepository<Genre> _genreRepository;
        private readonly IGenericRepository<GameScreenshot> _screenshotRepository;
        private readonly ILogger<ApiService> _logger;
        public ApiService(IGameRepository gameRepository, HttpClient httpClient, IGenericRepository<Tag> tagRepository, IGenericRepository<Platform> platformRepository, IGenericRepository<Genre> genreRepository, IGenericRepository<GameScreenshot> screenshotRepository, ILogger<ApiService> logger)
        {
            _gameRepository = gameRepository;
            _httpClient = httpClient;
            _tagRepository = tagRepository;
            _platformRepository = platformRepository;
            _genreRepository = genreRepository;
            _screenshotRepository = screenshotRepository;
            _logger = logger;
        }
        public async Task ImportGamesFromApiAsync()
        {
            try
            {
                while (count <= 700)
                {
                    var response = await _httpClient.GetAsync($"https://api.rawg.io/api/games/{count}?key={apiKey}");
                    if (response.IsSuccessStatusCode == true)
                    {
                        string json = await response.Content.ReadAsStringAsync();
                        JsonDocument jsonData = JsonDocument.Parse(json);
                        JsonElement root = jsonData.RootElement;
                        Random random = new Random();
                        Game game = new Game
                        {
                            Name = root.GetProperty("name").ToString(),
                            Description = root.GetProperty("description").ToString(),
                            Cost = Math.Round(random.NextDouble() * 100, 2),
                            Company = root.GetProperty("developers").EnumerateArray().ElementAt(0).GetProperty("name").ToString(),
                            UrlImage = root.GetProperty("background_image").GetString(),
                            Release = root.GetProperty("released").GetDateTime(),
                            GameGenres = new List<GameGenre>(),
                            GamePlatforms = new List<GamePlatform>(),
                            GameTags = new List<GameTag>(),
                            Screenshots = new List<GameScreenshot>()
                        };
                        if (count == 248) {
                            int s = 1;
                        }
                        game.Screenshots = await GetGameScreenshotsAsync(count);
                        game.GameGenres = await GetGenresAsync(root);
                        game.GamePlatforms = await GetPlatfromsAsync(root);
                        game.GameTags = await GetTagsAsync(root);

                        if (game != null)
                        {
                            await _gameRepository.CreateGameAsync(game);
                        }
                    }
                    count += 1;
                }
            }
            catch
            {

            }
        }
        private async Task<ICollection<GameScreenshot>> GetGameScreenshotsAsync(int id)
        {

            var response = await _httpClient.GetAsync($"https://api.rawg.io/api/games/{count}/screenshots?key={apiKey}");
            if (response.IsSuccessStatusCode== true)
            {
                string json = await response.Content.ReadAsStringAsync();
                JsonDocument jsonData = JsonDocument.Parse(json);
                JsonElement root = jsonData.RootElement;
                List<GameScreenshot> listScreenshots = new List<GameScreenshot>();

                foreach (JsonElement screenshot in root.GetProperty("results").EnumerateArray())
                {
                    var url = screenshot.GetProperty("image").GetString();
                    var dataScreenshots = await _screenshotRepository.GetEntity(q => q.Where(t => t.Url == url));
                    GameScreenshot gameScreenshot = dataScreenshots ?? new GameScreenshot { Url = screenshot.GetProperty("image").GetString() };
                    listScreenshots.Add(gameScreenshot);
                }
                return listScreenshots;
            }
            return null;
        }
        private async Task<ICollection<GameGenre>> GetGenresAsync(JsonElement root)
        {
            List<GameGenre> genres = new List<GameGenre>();
            foreach (JsonElement genre in root.GetProperty("genres").EnumerateArray())
            {
                var currentGenre = genre.GetProperty("name").GetString();
                var dataGenre = await _genreRepository.GetEntity(q => q.Where(t => t.Name == currentGenre));
                if (dataGenre == null)
                {
                    dataGenre = new Genre { Name = genre.GetProperty("name").GetString() };
                    await _genreRepository.AddAsync(dataGenre);
                }
                genres.Add(new GameGenre { Genre = dataGenre });
            }
            return genres;
        }
        private async Task<ICollection<GamePlatform>> GetPlatfromsAsync(JsonElement root)
        {
            List<GamePlatform> platforms = new List<GamePlatform>();
            foreach (JsonElement platform in root.GetProperty("platforms").EnumerateArray())
            {
                JsonElement platformDetails = platform.GetProperty("platform");
                var currentPlatform = platformDetails.GetProperty("name").GetString();
                var dataPlatform = await _platformRepository.GetEntity(q => q.Where(t => t.Name == currentPlatform));
                if (dataPlatform == null)
                {
                    dataPlatform = new Platform { Name = platformDetails.GetProperty("name").GetString() };
                    await _platformRepository.AddAsync(dataPlatform);
                }
                platforms.Add(new GamePlatform { Platform = dataPlatform });
            }
            return platforms;
        }
        private async Task<ICollection<GameTag>> GetTagsAsync(JsonElement root)
        {
            List<GameTag> tags = new List<GameTag>();
            foreach (JsonElement tag in root.GetProperty("tags").EnumerateArray())
            {
                var currentTag = tag.GetProperty("name").GetString();
                var dataTag = await _tagRepository.GetEntity(q => q.Where(t => t.Name == currentTag));
                if (dataTag == null)
                {
                    dataTag = new Tag { Name = tag.GetProperty("name").GetString() };
                    await _tagRepository.AddAsync(dataTag);
                }
                tags.Add(new GameTag { Tag = dataTag });
            }
            return tags;
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
