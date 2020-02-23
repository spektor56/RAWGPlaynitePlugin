using Playnite.SDK;
using Playnite.SDK.Metadata;
using Playnite.SDK.Models;
using Playnite.SDK.Plugins;
using Rawg.Api;
using RAWGMetadata.Extensions;
using RAWGMetadata.Model;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace RAWGMetadata
{
    public class RawgLazyMetadataProvider : OnDemandMetadataProvider
    {
        private readonly MetadataRequestOptions options;
        private readonly RawgMetadataPlugin plugin;
        private readonly ulong gameId = 0;
        private bool initialized = false;
        private Rawg.Model.Game _game;
        private Rawg.Model.GameSingle _gameInfo;
        private GamesApi _gamesApi = new GamesApi();

        public RawgLazyMetadataProvider(MetadataRequestOptions options, RawgMetadataPlugin plugin)
        {
            this.options = options;
            this.plugin = plugin;
        }

        public RawgLazyMetadataProvider(ulong gameId, RawgMetadataPlugin plugin)
        {
            this.gameId = gameId;
            this.plugin = plugin;
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
            if (_game is null && !initialized)
            {
                initialized = true;

                string platformId = null;
                var platform = options.GameData.Platform.Name;
                if (plugin.PlatformTranslationTable.ContainsKey(platform))
                {
                    platform = plugin.PlatformTranslationTable[platform];
                }
                
                if (plugin.PlatformList.ContainsKey(platform))
                {
                    platformId = plugin.PlatformList[platform].ToString();
                }

                var gameList = _gamesApi.GamesList(null, null, options.GameData.Name, null, platformId);
                var gameMatches = gameList.Results.Where(game => game.Name.Sanitize().Equals(options.GameData.Name.Sanitize()));

                if ((gameMatches == null || gameMatches.Count() != 1) && !options.IsBackgroundDownload)
                {
                    var selectedGame = plugin.PlayniteApi.Dialogs.ChooseItemWithSearch(new List<GenericItemOption>(gameMatches.Any() ? gameMatches.Select(game => new GameOption(game)) : gameList.Results.Select(game => new GameOption(game))), (a) =>
                    {
                        try
                        {
                            return new List<GenericItemOption>(_gamesApi.GamesList(null, null, a, null, platformId).Results.Select(game => new GameOption(game)));
                        }
                        catch (Exception e)
                        {
                            return new List<GenericItemOption>();
                        }
                    }, options.GameData.Name, string.Empty);

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
        
        public override string GetName()
        {
            GetGame();

            if (_game != null)
            {
                if (!string.IsNullOrWhiteSpace(_game.Name))
                {
                    return _game.Name;
                }
            }

            return base.GetName();
        }
        
        public override List<string> GetGenres()
        {
            GetGameInfo();

            if (_gameInfo != null)
            {
                if (_gameInfo.Genres != null)
                {
                    return _gameInfo.Genres.Select(genre => genre.Name).ToList();
                }
            }

            return base.GetGenres();
        }
        
        
        public override DateTime? GetReleaseDate()
        {
            GetGame();
            
            if (_game != null)
            {
                if (_game.Released != null)
                {
                    return _game.Released;
                }
            }

            return base.GetReleaseDate();
        }
        
        public override List<string> GetDevelopers()
        {
            GetGameInfo();

            if (_gameInfo != null)
            {
                if (_gameInfo.Developers != null)
                {
                    return _gameInfo.Developers.Select(developer => developer.Name).ToList();
                }
            }

            return base.GetDevelopers();
        }

        public override List<string> GetPublishers()
        {
            GetGameInfo();

            if (_gameInfo != null)
            {
                if (_gameInfo.Publishers != null)
                {
                    return _gameInfo.Publishers.Select(publisher => publisher.Name).ToList();
                }
            }

            return base.GetPublishers();
        }
        

        public override string GetDescription()
        {
            GetGameInfo();
            
            if (_gameInfo != null)
            {
                if (!string.IsNullOrWhiteSpace(_gameInfo.Description))
                {
                    return _gameInfo.Description;
                }
            }

            return base.GetDescription();
        }
        
        public override int? GetCommunityScore()
        {
            GetGame();

            if (_game != null)
            {
                if (_game.Rating != null)
                {
                    return (int)(_game.Rating*20);
                }
            }

            return base.GetCommunityScore();
        }
        
        public override MetadataFile GetCoverImage()
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
            return base.GetCoverImage();
        }
        
        public override MetadataFile GetBackgroundImage()
        {
            GetGame();

            if (_game != null)
            {
                if (!string.IsNullOrWhiteSpace(_game.BackgroundImage))
                {
                    return new MetadataFile(_game.BackgroundImage);
                }
            }

            return base.GetBackgroundImage();
        }
        
        public override List<Link> GetLinks()
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

            return base.GetLinks();
        }

        public override MetadataFile GetIcon()
        {
            /*
            using (MemoryStream ms = new MemoryStream())
            {
                RAWGMetadata.Properties.Resources.rawg.Save(ms);
                return new MetadataFile("RAWG", ms.ToArray());
            }
            */

            return base.GetIcon();
        }
        
        public override int? GetCriticScore()
        {
            GetGame();

            if (_game != null)
            {
                if (_game.Metacritic != null)
                {
                    return _game.Metacritic;
                }
            }

            return base.GetCriticScore();
        }
        
        public override List<string> GetTags()
        {
            GetGameInfo();

            if (_gameInfo != null)
            {
                if (_gameInfo.Tags != null)
                {
                    return _gameInfo.Tags.Select(tag => tag.Name).ToList();
                }
            }

            return base.GetTags();
        }
        

        public override List<MetadataField> AvailableFields
        {
            get
            {
                return plugin.SupportedFields;
            }
        }
    }
}
