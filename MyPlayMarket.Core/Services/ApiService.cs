using Azure;
using Humanizer.Localisation;
using Microsoft.Build.Framework;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using MyPlayMarket.Core.IServices;
using MyPlayMarket.Core.IRepository;
using MyPlayMarket.Core.Entities;
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
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

namespace MyPlayMarket.Core.Services
{
    public class ApiService : IApiService
    {
        const string apiKey = "cf0d983c9d624cbd989ded847b1429f9";
        int count = 500;
        private readonly HttpClient _httpClient;
        private readonly IGameRepository _gameRepository;
        private readonly IGenericService<Tag> _tagService;
        private readonly IGenericService<Platform> _platformService;
        private readonly IGenericService<Genre> _genreService;
        private readonly IGenericService<GameScreenshot> _screenshotService;
        private readonly ILogger<ApiService> _logger;
        public ApiService(IGameRepository gameRepository, 
            HttpClient httpClient,
            IGenericService<Tag> tagService,
            IGenericService<Platform> platformService,
            IGenericService<Genre> genreService,
            IGenericService<GameScreenshot> _screenshotService, 
            ILogger<ApiService> logger)
        {
            _gameRepository = gameRepository;
            _httpClient = httpClient;
            _tagService = tagService;
            _platformService = platformService;
            _genreService = genreService;
            _screenshotService = _screenshotService;
            _logger = logger;
        }
        public async Task ImportGamesFromApiAsync()
        {
            try
            {
                //List of Best Games
                //List<int> ints = new List<int> { 3498, 3328, 58175, 4200, 28, 4291, 802, 4062, 12020, 3439, 5679, 13537, 1030, 5286, 32, 3070, 13536, 3939, 2454, 4286 };
                while (1200 > count)
                {
                    var response = await _httpClient.GetAsync($"https://api.rawg.io/api/games/{count}?key={apiKey}");
                    if (response.IsSuccessStatusCode == true)
                    {
                        string json = await response.Content.ReadAsStringAsync();
                        JsonDocument jsonData = JsonDocument.Parse(json);
                        JsonElement root = jsonData.RootElement;
                        Random random = new Random();
                        var description = root.GetProperty("description").ToString();
                        var url = root.GetProperty("background_image").ToString();
                        var company = "Unknown Developer";
                        if (root.TryGetProperty("developers", out JsonElement developersElement) && developersElement.GetArrayLength() > 0)
                        {
                            company = developersElement[0].GetProperty("name").GetString();
                        }
                        Game game = new Game
                        {
                            Name = root.GetProperty("name").ToString(),
                            Description = string.IsNullOrEmpty(description) ? "no info" : description,
                            Cost = Math.Round(random.NextDouble() * 100, 2),
                            Company = company,
                            UrlImage = string.IsNullOrEmpty(url) ? "no info" : url,
                            Release = root.GetProperty("released").GetDateTime(),
                            GameGenres = new List<GameGenre>(),
                            GamePlatforms = new List<GamePlatform>(),
                            GameTags = new List<GameTag>(),
                            Screenshots = new List<GameScreenshot>()
                        };

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
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
            }
        }
        private async Task<ICollection<GameScreenshot>> GetGameScreenshotsAsync(int id)
        {

            var response = await _httpClient.GetAsync($"https://api.rawg.io/api/games/{id}/screenshots?key={apiKey}");
            List<GameScreenshot> listScreenshots = new List<GameScreenshot>();
            if (response.IsSuccessStatusCode == true)
            {
                string json = await response.Content.ReadAsStringAsync();
                JsonDocument jsonData = JsonDocument.Parse(json);
                JsonElement root = jsonData.RootElement;
                

                foreach (JsonElement screenshot in root.GetProperty("results").EnumerateArray())
                {
                    var url = screenshot.GetProperty("image").GetString();

                    if (!string.IsNullOrEmpty(url))
                    {
                        GameScreenshot gameScreenshot =  new GameScreenshot { Url = url };
                        listScreenshots.Add(gameScreenshot);
                    }
                }             
            }
            return listScreenshots;
        }
        private async Task<ICollection<GameGenre>> GetGenresAsync(JsonElement root)
        {
            List<GameGenre> genres = new List<GameGenre>();
            foreach (JsonElement genre in root.GetProperty("genres").EnumerateArray())
            {
                var currentGenre = genre.GetProperty("name").GetString();
                if (!string.IsNullOrEmpty(currentGenre))
                {
                    var dataGenre = await _genreService.GetEntityAsync(q => q.Where(t => t.Name == currentGenre));

                    if (dataGenre == null)
                    {
                        dataGenre = new Genre { Name = currentGenre };
                        await _genreService.AddAsync(dataGenre);
                    }

                    genres.Add(new GameGenre { Genre = dataGenre });
                }
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
                if (!string.IsNullOrEmpty(currentPlatform))
                {
                    var dataPlatform = await _platformService.GetEntityAsync(q => q.Where(t => t.Name == currentPlatform));
                    if (dataPlatform == null)
                    {
                        dataPlatform = new Platform { Name = currentPlatform };
                        await _platformService.AddAsync(dataPlatform);
                    }
                    platforms.Add(new GamePlatform { Platform = dataPlatform });
                }
            }
            return platforms;
        }
        private async Task<ICollection<GameTag>> GetTagsAsync(JsonElement root)
        {
            List<GameTag> tags = new List<GameTag>();
            foreach (JsonElement tag in root.GetProperty("tags").EnumerateArray())
            {
                var currentTag = tag.GetProperty("name").GetString();
                if (!string.IsNullOrEmpty(currentTag))
                {
                    var dataTag = await _tagService.GetEntityAsync(q => q.Where(t => t.Name == currentTag));
                    if (dataTag == null)
                    {
                        dataTag = new Tag { Name = currentTag };
                        await _tagService.AddAsync(dataTag);
                    }
                    tags.Add(new GameTag { Tag = dataTag });
                }
            }

            return tags;
        }

    }
}
