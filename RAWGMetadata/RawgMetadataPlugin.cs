using Playnite.SDK;
using Playnite.SDK.Models;
using Playnite.SDK.Plugins;
using Rawg.Api;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace RAWGMetadata
{
    public class RawgMetadataPlugin : MetadataPlugin
    {
        internal readonly RawgMetadataSettings Settings;
        public bool initializing { get; private set; } = true;
        public Dictionary<string,int> PlatformList { get; private set; }

        public RawgMetadataPlugin(IPlayniteAPI playniteAPI) : base(playniteAPI)
        {
            Settings = new RawgMetadataSettings(this);
            Task.Run(() => {
                try
                {
                    var platformApi = new PlatformsApi();
                    var platforms = platformApi.PlatformsList();
                    PlatformList = platforms.Results.ToDictionary(result => result.Name, result => (int)result.Id, StringComparer.OrdinalIgnoreCase);
                }
                finally
                {
                    initializing = false;
                }
            });
        }

        public override ISettings GetSettings(bool firstRunSettings)
        {
            return Settings;
        }

        public override UserControl GetSettingsView(bool firstRunView)
        {
            return new RawgMetadataSettingsView(this);
        }

        public override IEnumerable<ExtensionFunction> GetFunctions()
        {
            return base.GetFunctions();
        }

        public override void OnGameStarting(Game game)
        {
            base.OnGameStarting(game);
        }

        public override void OnGameStarted(Game game)
        {
            base.OnGameStarted(game);
        }

        public override void OnGameStopped(Game game, long ellapsedSeconds)
        {
            base.OnGameStopped(game, ellapsedSeconds);
        }

        public override void OnGameInstalled(Game game)
        {
            base.OnGameInstalled(game);
        }

        public override void OnGameUninstalled(Game game)
        {
            base.OnGameUninstalled(game);
        }

        public override void OnApplicationStarted()
        {
            base.OnApplicationStarted();
        }

        public override Guid Id => Guid.Parse("000001D9-DBD1-46C6-B5D0-B1BA557D10E4");
        public override OnDemandMetadataProvider GetMetadataProvider(MetadataRequestOptions options)
        {
            return new RawgLazyMetadataProvider(options, this);
        }

        public override string Name { get; } = "RAWG";

        public override List<MetadataField> SupportedFields { get; } = new List<MetadataField>
        {
            MetadataField.Name,
            MetadataField.Genres,
            MetadataField.ReleaseDate,
            MetadataField.Developers,
            MetadataField.Publishers,
            //MetadataField.Tags,
            MetadataField.Description,
            MetadataField.Links,
            MetadataField.CriticScore,
            MetadataField.CommunityScore,
            MetadataField.Icon,
            MetadataField.CoverImage,
            MetadataField.BackgroundImage

        };

    }
}
