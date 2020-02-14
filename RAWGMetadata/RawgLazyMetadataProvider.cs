using Playnite.SDK.Metadata;
using Playnite.SDK.Plugins;
using Rawg.Api;
using RAWGMetadata.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RAWGMetadata
{
    public class RawgLazyMetadataProvider : OnDemandMetadataProvider
    {
        private readonly MetadataRequestOptions options;
        private readonly RawgMetadataPlugin plugin;
        private readonly ulong gameId = 0;
        private Rawg.Model.Game _game;
        private List<MetadataField> availableFields;

        public RawgLazyMetadataProvider(MetadataRequestOptions options, RawgMetadataPlugin plugin)
        {
            //Game object is in the options class
            //This class will search for the game once (name + platform), then use gameid on subsequent lookups to load each metadata field.
            this.options = options;
            this.plugin = plugin;
        }

        public RawgLazyMetadataProvider(ulong gameId, RawgMetadataPlugin plugin)
        {
            this.gameId = gameId;
            this.plugin = plugin;
        }
        
        private Rawg.Model.Game GetGame()
        {
            if (_game is null)
            {
                if (plugin.PlatformList.ContainsKey("NES"))
                {
                    var gamesApi = new GamesApi();
                    var gameList = gamesApi.GamesList(null, null, options.GameData.Name, null, plugin.PlatformList["NES"].ToString());
                    _game = gameList.Results.FirstOrDefault(game => game.Name.Sanitize().Equals(options.GameData.Name.Sanitize()));
                    if (_game == null)
                    {
                        _game = gameList.Results.FirstOrDefault();
                    }
                }

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
        /*
        public override List<string> GetGenres()
        {
            var game = GetGame();

            if (game != null)
            {
                if (!string.IsNullOrWhiteSpace(game.Genres))
                {
                    return game.Genres.Split(';').Select(genre => genre.Trim()).ToList();
                }
            }

            return base.GetGenres();
        }
        */
        
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
        /*
        public override List<string> GetDevelopers()
        {
            var game = GetGame();

            if (game != null)
            {
                if (!string.IsNullOrWhiteSpace(game.Developer))
                {
                    return game.Developer.Split(';').Select(developer => developer.Trim()).ToList();
                }
            }

            return base.GetDevelopers();
        }

        public override List<string> GetPublishers()
        {
            var game = GetGame();

            if (game != null)
            {
                if (!string.IsNullOrWhiteSpace(game.Publisher))
                {
                    return game.Publisher.Split(';').Select(publisher => publisher.Trim()).ToList();
                }
            }

            return base.GetPublishers();
        }


        public override string GetDescription()
        {
            var game = GetGame();

            if (game != null)
            {
                if (!string.IsNullOrWhiteSpace(game.Overview))
                {
                    return game.Overview;
                }
            }

            return base.GetDescription();
        }
        */
        public override int? GetCommunityScore()
        {
            var game = GetGame();

            if (game != null)
            {
                if (game.Rating != null)
                {
                    return (int)game.Rating;
                }
            }

            return base.GetCommunityScore();
        }
        
        public override MetadataFile GetCoverImage()
        {
            var game = GetGame();

            if (game != null)
            {
                if (!string.IsNullOrWhiteSpace(game.BackgroundImage))
                {
                    return new MetadataFile(game.BackgroundImage);
                }
            }

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
        /*
        public override List<Link> GetLinks()
        {
            var game = GetGame();

            if (game != null)
            {
                var links = new List<Link>
                {
                    new Link("LaunchBox", "https://gamesdb.launchbox-app.com/games/dbid/" + game.DatabaseID)
                };

                if (!string.IsNullOrWhiteSpace(game.WikipediaURL))
                {
                    links.Add(new Link("Wikipedia", game.WikipediaURL));
                }

                if (!string.IsNullOrWhiteSpace(game.VideoURL))
                {
                    links.Add(new Link("Video", game.VideoURL));
                }

                return links;
            }

            return base.GetLinks();
        }

        

        public override MetadataFile GetIcon()
        {
            using (MemoryStream ms = new MemoryStream())
            {
                LBGDBMetadata.Properties.Resources.launchbox.Save(ms);
                return new MetadataFile("LaunchBox", ms.ToArray());
            }
        }

        public override int? GetCriticScore()
        {
            return base.GetCriticScore();
        }

        public override List<string> GetTags()
        {
            return base.GetTags();
        }
        */

        public override List<MetadataField> AvailableFields
        {
            get
            {
                return plugin.SupportedFields;
            }
        }
    }
}
