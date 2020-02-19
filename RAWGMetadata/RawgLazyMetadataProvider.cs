using Playnite.SDK.Metadata;
using Playnite.SDK.Models;
using Playnite.SDK.Plugins;
using Rawg.Api;
using RAWGMetadata.Extensions;
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

        private Rawg.Model.GameSingle GetGameInfo()
        {
            var game = GetGame();

            if (!(game is null) && _gameInfo is null)
            {
                _gameInfo = _gamesApi.GamesRead(game.Id.ToString());
                return _gameInfo;
            }
            else
            {
                return _gameInfo;
            }
        }

        private Rawg.Model.Game GetGame()
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
                _game = gameList.Results.FirstOrDefault(game => game.Name.Sanitize().Equals(options.GameData.Name.Sanitize()));
                /*
                if (_game == null)
                {
                    _game = gameList.Results.FirstOrDefault();
                }
                */
                return _game;
            }
            else
            {
                return _game;
            }
        }
        
        public override string GetName()
        {
            var game = GetGame();

            if (game != null)
            {
                if (!string.IsNullOrWhiteSpace(game.Name))
                {
                    return game.Name;
                }
            }

            return base.GetName();
        }
        
        public override List<string> GetGenres()
        {
            var gameInfo = GetGameInfo();

            if (gameInfo != null)
            {
                if (gameInfo.Genres != null)
                {
                    return gameInfo.Genres.Select(genre => genre.Name).ToList();
                }
            }

            return base.GetGenres();
        }
        
        
        public override DateTime? GetReleaseDate()
        {
            var game = GetGame();
            
            if (game != null)
            {
                if (game.Released != null)
                {
                    return game.Released;
                }
            }

            return base.GetReleaseDate();
        }
        
        public override List<string> GetDevelopers()
        {
            var gameInfo = GetGameInfo();

            if (gameInfo != null)
            {
                if (gameInfo.Developers != null)
                {
                    return gameInfo.Developers.Select(developer => developer.Name).ToList();
                }
            }

            return base.GetDevelopers();
        }

        public override List<string> GetPublishers()
        {
            var gameInfo = GetGameInfo();

            if (gameInfo != null)
            {
                if (gameInfo.Publishers != null)
                {
                    return gameInfo.Publishers.Select(publisher => publisher.Name).ToList();
                }
            }

            return base.GetPublishers();
        }
        

        public override string GetDescription()
        {
            var gameInfo = GetGameInfo();
            
            if (gameInfo != null)
            {
                if (!string.IsNullOrWhiteSpace(gameInfo.Description))
                {
                    return gameInfo.Description;
                }
            }

            return base.GetDescription();
        }
        
        public override int? GetCommunityScore()
        {
            var game = GetGame();

            if (game != null)
            {
                if (game.Rating != null)
                {
                    return (int)(game.Rating*20);
                }
            }

            return base.GetCommunityScore();
        }
        
        public override MetadataFile GetCoverImage()
        {
            /*
            var game = GetGame();

            if (game != null)
            {
                if (!string.IsNullOrWhiteSpace(game.BackgroundImage))
                {
                    return new MetadataFile(game.BackgroundImage);
                }
            }
            */
            return base.GetCoverImage();
        }
        
        public override MetadataFile GetBackgroundImage()
        {
            var game = GetGame();

            if (game != null)
            {
                if (!string.IsNullOrWhiteSpace(game.BackgroundImage))
                {
                    return new MetadataFile(game.BackgroundImage);
                }
            }

            return base.GetBackgroundImage();
        }
        
        public override List<Link> GetLinks()
        {
            var gameInfo = GetGameInfo();

            if (gameInfo != null)
            {
                var links = new List<Link>();

                if (!string.IsNullOrWhiteSpace(gameInfo.Website))
                {
                    links.Add(new Link("Website", gameInfo.Website));
                }

                if (!string.IsNullOrWhiteSpace(gameInfo.MetacriticUrl))
                {
                    links.Add(new Link("Metacritic", gameInfo.MetacriticUrl));
                }
                
                if (!string.IsNullOrWhiteSpace(gameInfo.RedditUrl))
                {
                    links.Add(new Link("Reddit", gameInfo.RedditUrl));
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
            using (MemoryStream ms = new MemoryStream())
            {
                RAWGMetadata.Properties.Resources.rawg.Save(ms);
                return new MetadataFile("RAWG", ms.ToArray());
            }
        }
        
        public override int? GetCriticScore()
        {
            var game = GetGame();

            if (game != null)
            {
                if (game.Metacritic != null)
                {
                    return game.Metacritic;
                }
            }

            return base.GetCriticScore();
        }
        
        public override List<string> GetTags()
        {
            var gameInfo = GetGameInfo();

            if (gameInfo != null)
            {
                if (gameInfo.Tags != null)
                {
                    return gameInfo.Tags.Select(tag => tag.Name).ToList();
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
