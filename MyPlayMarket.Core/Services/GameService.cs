using MyPlayMarket.Core.Entities;
using MyPlayMarket.Core;
using System.Collections;
using MyPlayMarket.Core.IServices;
using MyPlayMarket.Core.IRepository;
using MyPlayMarket.Core.Entities.DTO;
using Microsoft.Extensions.Logging;

namespace MyPlayMarket.Core.Services
{
    public class GameService : IGameService
    {
        private readonly IGameRepository _repository;
        private readonly IGenericService<Tag> _tagService;
        private readonly IGenericService<Platform> _platformService;
        private readonly IGenericService<Genre> _genreService;
        private readonly IGenericService<GameScreenshot> _screenService;
        private readonly ILogger<GameService> _logger;
        public GameService(IGameRepository repository,
            IGenericService<Platform> platformService,
            IGenericService<Tag> tagService,
            IGenericService<Genre> genreService,
            IGenericService<GameScreenshot> screenService,
            ILogger<GameService> logger)
        {
            _repository = repository;
            _tagService = tagService;
            _platformService = platformService;
            _genreService = genreService;
            _screenService = screenService;
            _logger = logger;
        }
        public async Task<IEnumerable> GetGamesAsync()
        {
            return await _repository.GetAllGamesAsync();
        }
        public async Task<List<Game>> GetGamesByQueryAsync()
        {
            var games = await _repository.GetFiltredGamesAsync(q => q.Take(24));
            return games;
        }
        public async Task<Game> GetGameAsync(int id)
        {
            return await _repository.GetGameAsync(id);
        }
        public async Task<bool> CreateGameAsync(CreateUpdateGameDTO createdGame)
        {
            var game = createdGame.Game;
            var dataGenre = await _genreService.GetEntitiesAsync(q => q.Where(t => createdGame.GenresDTO.Contains(t.Name)));
            var dataPlatforms = await _platformService.GetEntitiesAsync(q => q.Where(t => createdGame.PlatformsDTO.Contains(t.Name)));
            var dataTags = await _tagService.GetEntitiesAsync(q => q.Where(t => createdGame.TagsDTO.Contains(t.Name)));
            var dataScreenshots = await _screenService.GetEntitiesAsync(q => q.Where(t => createdGame.ScreenshotsDTO.Contains(t.Url)));

            var genresToAdd = new List<Genre>();
            foreach (var genreName in createdGame.GenresDTO)
            {
                var genre = dataGenre.FirstOrDefault(g => g.Name == genreName) ?? new Genre { Name = genreName };
                genresToAdd.Add(genre);
            }

            game.GameGenres = genresToAdd.Select(g => new GameGenre { Game = game, Genre = g }).ToList();
            var platformsToAdd = new List<Platform>();
            foreach (var platformName in createdGame.PlatformsDTO)
            {
                var platform = dataPlatforms.FirstOrDefault(p => p.Name == platformName) ?? new Platform { Name = platformName };
                platformsToAdd.Add(platform);
            }

            game.GamePlatforms = platformsToAdd.Select(p => new GamePlatform { Game = game, Platform = p }).ToList();

            var tagsToAdd = new List<Tag>();
            foreach (var tagName in createdGame.TagsDTO)
            {
                var tag = dataTags.FirstOrDefault(t => t.Name == tagName) ?? new Tag { Name = tagName };
                tagsToAdd.Add(tag);
            }

            game.GameTags = tagsToAdd.Select(t => new GameTag { Game = game, Tag = t }).ToList();

            var screenshotsToAdd = new List<GameScreenshot>();
            foreach (var screenshotUrl in createdGame.ScreenshotsDTO)
            {
                var screenshot = dataScreenshots.FirstOrDefault(s => s.Url == screenshotUrl) ?? new GameScreenshot { Url = screenshotUrl };
                screenshotsToAdd.Add(screenshot);
            }

            game.Screenshots = screenshotsToAdd.ToList();

            return await _repository.CreateGameAsync(game); ;
        }

        public async Task<bool> DeleteGameAsync(int id)
        {
            return await _repository.DeleteGameAsync(id);
        }
        public async Task<bool> UpdateGameAsync(CreateUpdateGameDTO createdGame)
        {
            var game = createdGame.Game;
            var dataGenre = await _genreService.GetEntitiesAsync(q => q.Where(t => createdGame.GenresDTO.Contains(t.Name)));
            var dataPlatforms = await _platformService.GetEntitiesAsync(q => q.Where(t => createdGame.PlatformsDTO.Contains(t.Name)));
            var dataTags = await _tagService.GetEntitiesAsync(q => q.Where(t => createdGame.TagsDTO.Contains(t.Name)));
            var dataScreenshots = await _screenService.GetEntitiesAsync(q => q.Where(t => createdGame.ScreenshotsDTO.Contains(t.Url)));

            var genresToAdd = new List<Genre>();
            foreach (var genreName in createdGame.GenresDTO)
            {
                var genre = dataGenre.FirstOrDefault(g => g.Name == genreName) ?? new Genre { Name = genreName };
                genresToAdd.Add(genre);
            }

            game.GameGenres = genresToAdd.Select(g => new GameGenre { Game = game, Genre = g }).ToList();
            var platformsToAdd = new List<Platform>();
            foreach (var platformName in createdGame.PlatformsDTO)
            {
                var platform = dataPlatforms.FirstOrDefault(p => p.Name == platformName) ?? new Platform { Name = platformName };
                platformsToAdd.Add(platform);
            }

            game.GamePlatforms = platformsToAdd.Select(p => new GamePlatform { Game = game, Platform = p }).ToList();

            var tagsToAdd = new List<Tag>();
            foreach (var tagName in createdGame.TagsDTO)
            {
                var tag = dataTags.FirstOrDefault(t => t.Name == tagName) ?? new Tag { Name = tagName };
                tagsToAdd.Add(tag);
            }

            game.GameTags = tagsToAdd.Select(t => new GameTag { Game = game, Tag = t }).ToList();

            var screenshotsToAdd = new List<GameScreenshot>();
            foreach (var screenshotUrl in createdGame.ScreenshotsDTO)
            {
                var screenshot = dataScreenshots.FirstOrDefault(s => s.Url == screenshotUrl) ?? new GameScreenshot { Url = screenshotUrl };
                screenshotsToAdd.Add(screenshot);
            }

            game.Screenshots = screenshotsToAdd;

            return await _repository.UpdateGameAsync(game); ;
        }

    }
}
