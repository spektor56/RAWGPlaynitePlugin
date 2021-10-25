using Playnite.SDK;
using Playnite.SDK.Models;
using Playnite.SDK.Plugins;
using Rawg.Api;
using RAWGMetadata.Extensions;
using RAWGMetadata.Model;
using System;
using System.Collections.Generic;
using System.Linq;

namespace RAWGMetadata
{
    public class RawgLazyMetadataProvider : OnDemandMetadataProvider
    {
        private readonly MetadataRequestOptions _options;
        private readonly RawgMetadataPlugin _plugin;
        private bool _initialized = false;
        private Rawg.Model.Game _game;
        private Rawg.Model.GameSingle _gameInfo;
        private readonly GamesApi _gamesApi = new GamesApi();

        public RawgLazyMetadataProvider(MetadataRequestOptions options, RawgMetadataPlugin plugin)
        {
            this._options = options;
            this._plugin = plugin;
        }

        private void GetGameInfo()
        {
            if (_gameInfo is null)
            {
                GetGame();
                if (!(_game is null))
                {
                    _gameInfo = _gamesApi.GamesRead(_game.Id.ToString());
                }
            }
        }

        private void GetGame()
        {
            if (_game is null && !_initialized)
            {
                _initialized = true;

                string platformId = null;
                var platform = string.Empty;
                if (_options.GameData.Platforms != null && _options.GameData.Platforms.Count >= 1)
                {
                    platform = _options.GameData.Platforms[0].Name;
                }
                else
                {
                    return;
                }
                
                if (_plugin.PlatformTranslationTable.ContainsKey(platform))
                {
                    platform = _plugin.PlatformTranslationTable[platform];
                }
                
                if (_plugin.PlatformList.ContainsKey(platform))
                {
                    platformId = _plugin.PlatformList[platform].ToString();
                }

                var gameList = _gamesApi.GamesList(null, null, _options.GameData.Name, null, platformId);
                var gameMatches = gameList.Results.Where(game => game.Name.Sanitize().Equals(_options.GameData.Name.Sanitize()));

                if ((gameMatches == null || gameMatches.Count() != 1) && !_options.IsBackgroundDownload)
                {
                    var selectedGame = _plugin.PlayniteApi.Dialogs.ChooseItemWithSearch(new List<GenericItemOption>(gameMatches.Any() ? gameMatches.Select(game => new GameOption(game)) : gameList.Results.Select(game => new GameOption(game))), (a) =>
                    {
                        try
                        {
                            return new List<GenericItemOption>(_gamesApi.GamesList(null, null, a, null, platformId).Results.Select(game => new GameOption(game)));
                        }
                        catch (Exception e)
                        {
                            return new List<GenericItemOption>();
                        }
                    }, _options.GameData.Name, string.Empty);

                    if (selectedGame == null)
                    {
                        _game = null;
                    }
                    else
                    {
                        _game = ((GameOption)selectedGame).Game;
                    }
                }
                else
                {
                    _game = gameMatches.FirstOrDefault();
                }
            }
        }

        public override string GetName(GetMetadataFieldArgs args)
        {
            GetGame();

            if (_game != null)
            {
                if (!string.IsNullOrWhiteSpace(_game.Name))
                {
                    return _game.Name;
                }
            }

            return base.GetName(args);
        }

        public override IEnumerable<MetadataProperty> GetGenres(GetMetadataFieldArgs args)
        {
            GetGameInfo();

            if (_gameInfo != null)
            {
                if (_gameInfo.Genres != null)
                {
                    return _gameInfo.Genres.Select(genre => new MetadataNameProperty(genre.Name)).ToList();
                }
            }

            return base.GetGenres(args);
        }


        public override ReleaseDate? GetReleaseDate(GetMetadataFieldArgs args)
        {
            GetGame();
            
            if (_game != null)
            {
                if (_game.Released != null)
                {
                    return new ReleaseDate
                    (
                        _game.Released.Value.Year,
                        _game.Released.Value.Month,
                        _game.Released.Value.Day
                    );
                }
            }

            return base.GetReleaseDate(args);
        }

        public override IEnumerable<MetadataProperty> GetDevelopers(GetMetadataFieldArgs args)
        {
            GetGameInfo();

            if (_gameInfo != null)
            {
                if (_gameInfo.Developers != null)
                {
                    return _gameInfo.Developers.Select(developer => new MetadataNameProperty(developer.Name)).ToList();
                }
            }

            return base.GetDevelopers(args);
        }

        public override IEnumerable<MetadataProperty> GetPublishers(GetMetadataFieldArgs args)
        {
            GetGameInfo();

            if (_gameInfo != null)
            {
                if (_gameInfo.Publishers != null)
                {
                    return _gameInfo.Publishers.Select(publisher => new MetadataNameProperty(publisher.Name)).ToList();
                }
            }

            return base.GetPublishers(args);
        }


        public override string GetDescription(GetMetadataFieldArgs args)
        {
            GetGameInfo();
            
            if (_gameInfo != null)
            {
                if (!string.IsNullOrWhiteSpace(_gameInfo.Description))
                {
                    return _gameInfo.Description;
                }
            }

            return base.GetDescription(args);
        }

        public override int? GetCommunityScore(GetMetadataFieldArgs args)
        {
            GetGame();

            if (_game != null)
            {
                if (_game.Rating != null)
                {
                    return (int)(_game.Rating*20);
                }
            }

            return base.GetCommunityScore(args);
        }

        public override MetadataFile GetCoverImage(GetMetadataFieldArgs args)
        {
            /*
            GetGame();

            if (_game != null)
            {
                if (!string.IsNullOrWhiteSpace(_game.BackgroundImage))
                {
                    return new MetadataFile(_game.BackgroundImage);
                }
            }
            */
            return base.GetCoverImage(args);
        }
        
        public override MetadataFile GetBackgroundImage(GetMetadataFieldArgs args)
        {
            GetGame();

            if (_game != null)
            {
                if (!string.IsNullOrWhiteSpace(_game.BackgroundImage))
                {
                    return new MetadataFile(_game.BackgroundImage);
                }
            }

            return base.GetBackgroundImage(args);
        }

        public override IEnumerable<Link> GetLinks(GetMetadataFieldArgs args)
        {
            GetGameInfo();

            if (_gameInfo != null)
            {
                var links = new List<Link>();

                if (!string.IsNullOrWhiteSpace(_gameInfo.Website))
                {
                    links.Add(new Link("Website", _gameInfo.Website));
                }

                if (!string.IsNullOrWhiteSpace(_gameInfo.MetacriticUrl))
                {
                    links.Add(new Link("Metacritic", _gameInfo.MetacriticUrl));
                }
                
                if (!string.IsNullOrWhiteSpace(_gameInfo.RedditUrl))
                {
                    links.Add(new Link("Reddit", _gameInfo.RedditUrl));
                }

                if (links.Count > 0)
                {
                    return links;
                }
            }

            return base.GetLinks(args);
        }

        public override MetadataFile GetIcon(GetMetadataFieldArgs args)
        {
            /*
            using (MemoryStream ms = new MemoryStream())
            {
                RAWGMetadata.Properties.Resources.rawg.Save(ms);
                return new MetadataFile("RAWG", ms.ToArray());
            }
            */

            return base.GetIcon(args);
        }
        
        public override int? GetCriticScore(GetMetadataFieldArgs args)
        {
            GetGame();

            if (_game != null)
            {
                if (_game.Metacritic != null)
                {
                    return _game.Metacritic;
                }
            }

            return base.GetCriticScore(args);
        }

        public override IEnumerable<MetadataProperty> GetTags(GetMetadataFieldArgs args)
        {
            GetGameInfo();

            if (_gameInfo != null)
            {
                if (_gameInfo.Tags != null)
                {
                    return _gameInfo.Tags.Select(tag => new MetadataNameProperty(tag.Name)).ToList();
                }
            }

            return base.GetTags(args);
        }
        

        public override List<MetadataField> AvailableFields
        {
            get
            {
                return _plugin.SupportedFields;
            }
        }
    }
}
